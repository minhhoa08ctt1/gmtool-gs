using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IDAdmin.Lib.UI;
using IDAdmin.Lib.Utils;
using System.Collections.Generic;
using IDAdmin.Lib.DataLayer;
using System.Reflection;
using System.Globalization;

namespace IDAdmin.Pages
{
    static class ConverterWorkData
    {
        public static DateTime kindUnixTimeStampToKindDateTime(double unixTimeStamp, DateTimeKind kindUnixTime, DateTimeKind kindDateTime)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, kindUnixTime);
            if (kindDateTime.Equals(DateTimeKind.Local))
            {
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }
            else
                if (kindDateTime.Equals(DateTimeKind.Utc))
                {
                    dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
                    return dtDateTime;
                }
            return DateTime.Now;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public static double kindDateTimeToKindUnixTimestamp(DateTime date, DateTimeKind kind)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, kind);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /*Converts List To DataTable*/
        public static DataTable ToDataTable<TSource>(this IList<TSource> data, int type, int additionLength)
        {
            DataTable dataTable = new DataTable(typeof(TSource).Name);
            PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                    prop.PropertyType);
            }
            foreach (TSource item in data)
            {
                var values = new object[props.Length + additionLength];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                    if (type == 0)
                    {
                        if (i == 7)
                        {
                            WorkDataStatistics.ServerName = new List<string>();
                            List<Ccu> ccuServerList = (List<Ccu>)values[7];
                            int count = 8;
                            foreach (Ccu ccu in ccuServerList)
                            {
                                if (ccu != null && dataTable.Columns.Contains(ccu.GameZone) == false)
                                {
                                    dataTable.Columns.Add(ccu.GameZone);
                                    WorkDataStatistics.ServerName.Add(ccu.GameZone);
                                }
                                values[count] = ccu.Num;
                                count++;
                            }
                        }
                    }
                    else
                        if (type == 1)
                        {
                            if (i == 4)
                            {
                                WorkDataStatistics.ServerName = new List<string>();
                                List<Acu> ccuAverageList = (List<Acu>)values[4];
                                int count = 5;
                                foreach (Acu acu in ccuAverageList)
                                {
                                    if (acu != null && dataTable.Columns.Contains(acu.GameZone) == false)
                                    {
                                        dataTable.Columns.Add(acu.GameZone);
                                        WorkDataStatistics.ServerName.Add(acu.GameZone);
                                    }
                                    values[count] = acu.Average;
                                    count++;
                                }
                            }
                        }
                        else
                            if (type == 2)
                            {
                                if (i == 4)
                                {
                                    WorkDataStatistics.ServerName = new List<string>();
                                    List<Pcu> CcuMaxServerList = (List<Pcu>)values[4];
                                    int count = 5;
                                    foreach (Pcu pcu in CcuMaxServerList)
                                    {
                                        if (pcu != null && dataTable.Columns.Contains(pcu.GameZone) == false)
                                        {
                                            dataTable.Columns.Add(pcu.GameZone);
                                            WorkDataStatistics.ServerName.Add(pcu.GameZone);
                                        }
                                        values[count] = pcu.Max;
                                        count++;
                                    }
                                }
                            }
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        /*Converts DataTable To List*/
        public static List<TSource> ToList<TSource>(this DataTable dataTable) where TSource : new()
        {
            var dataList = new List<TSource>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                                 select new
                                 {
                                     Name = aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
                             aProp.PropertyType
                                 }).ToList();
            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new
                                     {
                                         Name = aHeader.ColumnName,
                                         Type = aHeader.DataType
                                     }).ToList();
            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var aTSource = new TSource();
                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value) ?
                    null : dataRow[aField.Name]; //if database field is nullable
                    propertyInfos.SetValue(aTSource, value, null);
                }
                dataList.Add(aTSource);
            }
            return dataList;
        }

    }
    public partial class WorkDataStatistics : System.Web.UI.Page
    {
        static List<Ccu> ccu;
        static List<Au> au;
        public static List<string> ServerName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFromDate.Text = "07/02/2015";
            txtFromDate.ReadOnly = true;
            //container.ActiveViewIndex=0;
            //MultiView.ActiveViewIndex = int.Parse(statisticsTypeDropDownList.SelectedValue);
        }

        protected void onlinePeppleNumLK_Click(object sender, EventArgs e)
        {
            container.ActiveViewIndex = 0;
            MultiView.ActiveViewIndex = 0;
        }
        public static DateTime convertStringToDateTime(string date)
        {
            string[] dateArrray = date.Split('/');
            DateTime dt = new DateTime(int.Parse(dateArrray[2]), int.Parse(dateArrray[1]), int.Parse(dateArrray[0]));
            return dt;
        }

        public List<DateTime> getDateListBetween(DateTime fromDate, DateTime toDate)
        {
            var dates = new List<DateTime>();

            for (var dt = fromDate; dt <= toDate; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }
            return dates;
        }
        protected void Count_Click(object sender, EventArgs e)
        {
            if (MultiView.ActiveViewIndex == 0)
            {
                //string strFormDate = String.Format("{0:yyyMMdd}", dtFormDate);
                //string strToDate = String.Format("{0:yyyMMdd}", dtToDate);
                //ccuAveragePanel.Controls.Clear();
                onlinePeoplePanelList.Controls.Clear();
                onlinePeoplePanelList.Controls.Add(getOnlinePeopleNumTabble(ccu));
            }
            else
                if (MultiView.ActiveViewIndex == 1)
                {
                    ccuAveragePanel.Controls.Clear();
                    ccuAveragePanel.Controls.Add(getCcuAverage(ccu));
                }
                else
                    if (MultiView.ActiveViewIndex == 2)
                    {
                        ccuMaxPanel.Controls.Clear();
                        ccuMaxPanel.Controls.Add(getCcuMax(ccu));
                    }
        }

        public string truncateTime(string timeStamp, string format)
        {
            //"dd/MM/yyyy HH:mm"
            if (string.IsNullOrEmpty(format))
            {
                format = "dd/MM/yyyy HH:mm";
            }
            // string strDt = ConverterWorkData.ConvertFromUnixTimestamp(double.Parse(timeStamp)).ToString(format);
            //DateTime dtTemp = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.ParseExact(strDt, format, CultureInfo.InvariantCulture));
            //string timeStampTemp = ConverterWorkData.ConvertToUnixTimestamp(dtTemp).ToString();
            string strlocalTime = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeStamp), DateTimeKind.Utc, DateTimeKind.Local).ToString(format);
            double localTimeStamp = ConverterWorkData.kindDateTimeToKindUnixTimestamp(DateTime.ParseExact(strlocalTime, format, CultureInfo.InvariantCulture), DateTimeKind.Local);
            return localTimeStamp.ToString();
        }

        private List<Ccu> getCcu(List<DateTime> dateTimeList)
        {
            return Lib.DataLayer.WebDB.getCcu(dateTimeList); ;
        }

        private List<Au> getAu(List<DateTime> dateTimeList)
        {
            return Lib.DataLayer.WebDB.getAu(dateTimeList);
        }

        private Table getCcuMax(List<Ccu> paramDt)
        {
            List<Ccu> oldList = paramDt;
            List<Ccu> dataList = new List<Ccu>(oldList.Count);

            oldList.ForEach((item) =>
            {
                dataList.Add(new Ccu(item));
            });

            //List<Ccu> dataList = new List<Ccu>(paramDt);
            foreach (Ccu ccuItem in dataList)
            {
                ccuItem.Timestamp = truncateTime(ccuItem.Timestamp, "dd/MM/yyyy");
            }
            var gameZoneList = dataList.Select(s => s.GameZone).Distinct();
            Dictionary<int, List<Pcu>> gameZoneDataDic = new Dictionary<int, List<Pcu>>();
            for (int i = 0; i < gameZoneList.Count(); i++)
            {
                List<Pcu> gameZoneData = (from tb in dataList where tb.GameZone.Equals(gameZoneList.ElementAt(i).ToString()) group tb by new { tb.GameZone, tb.Timestamp } into grp select new Pcu { GameZone = grp.Key.GameZone, Timestamp = grp.Key.Timestamp, Max = grp.Max(t => long.Parse(t.Num)).ToString() }).ToList(); //dataList.GroupBy(g => g.Timestamp, g => g.GameZone).Where(w => w.Key.Equals(gameZoneList.ElementAt(i))).OrderBy(o => o.Timestamp).ToList();
                gameZoneDataDic.Add(int.Parse(gameZoneList.ElementAt(i)), gameZoneData);
            }
            var timeList = dataList.OrderBy(o => o.Timestamp).Select(s => s.Timestamp).Distinct().ToList();
            //var acuSumList = dataList.GroupBy(c => c.Timestamp).Select(g => new { Timestamp = g.Key, Average = g.Average(h => int.Parse(h.Num)) }).OrderBy(o => o.Timestamp).ToList();

            List<CcuMax> pcuList = new List<CcuMax>();

            for (int i = 0; i < timeList.Count; i++)
            {
                //string strDt= ConverterWorkData.ConvertFromUnixTimestamp(long.Parse(timeList[i])).ToString("dd/MM/yyyy hh:mm");
                //DateTime dtTemp = DateTime.ParseExact(strDt, "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
                //string timeStampTemp = truncateSecond(timeList[i]);
                CcuMax cm = new CcuMax();
                foreach (KeyValuePair<int, List<Pcu>> entry in gameZoneDataDic)
                {
                    //var ccu = entry.Value.ToArray()[i];
                    Pcu pcu = null;
                    try
                    {
                        pcu = entry.Value.Where(w => w.Timestamp.Equals(timeList[i])).FirstOrDefault();
                        if (pcu == null)
                        {
                            pcu = new Pcu() { GameZone = entry.Key.ToString(), Timestamp = timeList[i], Max = "" };
                        }
                    }
                    catch (Exception ex)
                    {

                        pcu = new Pcu() { GameZone = entry.Key.ToString(), Timestamp = timeList[i], Max = "" };
                    }

                    cm.CcuMaxServerList.Add(pcu);
                }
                try
                {
                    var pcuSum = (from tb in cm.CcuMaxServerList group tb by new { tb.Timestamp } into grp select new { PcuSum = grp.Sum(t => long.Parse(t.Max)) }).FirstOrDefault();
                    cm.PcuSum = pcuSum.PcuSum.ToString();
                }
                catch (Exception ex)
                {
                    cm.PcuSum = "";
                }
                cm.DateTime = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                cm.Date = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local).ToString("dd/MM/yyyy");
                cm.TimeStamp = long.Parse(timeList[i]);
                pcuList.Add(cm);
            }
            string startTime = timeList[0];

            DataTable dt = ConverterWorkData.ToDataTable(pcuList, 2, gameZoneList.Count());

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;
            TableRow rowHeader = new TableRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Date",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Pcu Sum",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    //UIHelpers.CreateTableCell("CcuServerList",Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[5].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[6].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader")
                }
            );
            foreach (string gameZone in gameZoneList)
            {
                rowHeader.Cells.Add(UIHelpers.CreateTableCell(gameZone, Unit.Percentage(6), HorizontalAlign.Center, "cellHeader"));
            }
            table.Rows.Add(rowHeader);
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    TableRow rowEmpty = new TableRow();
                    rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 4));
                    table.Rows.Add(rowEmpty);
                }
                else
                {
                    //labelCountOfUser.Text = string.Format("{0:N0}", dt.Rows.Count);
                    string css;
                    int stt = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        stt += 1;
                        css = stt % 2 == 0 ? "cell1" : "cell2";
                        TableRow row = new TableRow();
                        row.Cells.AddRange
                        (
                            new TableCell[]
                           {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Date"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["PcuSum"]),HorizontalAlign.Left,css),
                                    //UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[5].ColumnName]),HorizontalAlign.Left,css),
                                    //UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[6].ColumnName]),HorizontalAlign.Left,css),

                           }
                        );
                        foreach (string gameZone in gameZoneList)
                        {
                            row.Cells.Add(UIHelpers.CreateTableCell(Converter.ToString(dr[gameZone]), HorizontalAlign.Left, css));
                        }
                        table.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return table;
        }

        private Table getCcuAverage(List<Ccu> paramDt)
        {
            List<Ccu> oldList = paramDt;
            List<Ccu> dataList = new List<Ccu>(oldList.Count);

            oldList.ForEach((item) =>
            {
                dataList.Add(new Ccu(item));
            });

            //List<Ccu> dataList = new List<Ccu>(paramDt);
            foreach (Ccu ccuItem in dataList)
            {
                ccuItem.Timestamp = truncateTime(ccuItem.Timestamp, "dd/MM/yyyy");
            }
            var gameZoneList = dataList.Select(s => s.GameZone).Distinct();
            Dictionary<int, List<Acu>> gameZoneDataDic = new Dictionary<int, List<Acu>>();
            for (int i = 0; i < gameZoneList.Count(); i++)
            {
                List<Acu> gameZoneData = (from tb in dataList where tb.GameZone.Equals(gameZoneList.ElementAt(i).ToString()) group tb by new { tb.GameZone, tb.Timestamp } into grp select new Acu { GameZone = grp.Key.GameZone, Timestamp = grp.Key.Timestamp, Average = grp.Average(t => long.Parse(t.Num)).ToString("0") }).ToList(); //dataList.GroupBy(g => g.Timestamp, g => g.GameZone).Where(w => w.Key.Equals(gameZoneList.ElementAt(i))).OrderBy(o => o.Timestamp).ToList();
                gameZoneDataDic.Add(int.Parse(gameZoneList.ElementAt(i)), gameZoneData);
            }
            var timeList = dataList.OrderBy(o => o.Timestamp).Select(s => s.Timestamp).Distinct().ToList();
            //var acuSumList = dataList.GroupBy(c => c.Timestamp).Select(g => new { Timestamp = g.Key, Average = g.Average(h => int.Parse(h.Num)) }).OrderBy(o => o.Timestamp).ToList();

            List<CcuAverage> ccuAverageList = new List<CcuAverage>();

            for (int i = 0; i < timeList.Count; i++)
            {
                //string strDt= ConverterWorkData.ConvertFromUnixTimestamp(long.Parse(timeList[i])).ToString("dd/MM/yyyy hh:mm");
                //DateTime dtTemp = DateTime.ParseExact(strDt, "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
                //string timeStampTemp = truncateSecond(timeList[i]);
                CcuAverage ca = new CcuAverage();
                foreach (KeyValuePair<int, List<Acu>> entry in gameZoneDataDic)
                {
                    //var ccu = entry.Value.ToArray()[i];
                    Acu acu = null;
                    try
                    {
                        acu = entry.Value.Where(w => w.Timestamp.Equals(timeList[i])).FirstOrDefault();
                        if (acu == null)
                        {
                            acu = new Acu() { GameZone = entry.Key.ToString(), Timestamp = timeList[i], Average = "" };
                        }
                    }
                    catch (Exception ex)
                    {

                        acu = new Acu() { GameZone = entry.Key.ToString(), Timestamp = timeList[i], Average = "" };
                    }

                    ca.AverageCcuServerList.Add(acu);
                }
                try
                {
                    var acuSum = (from tb in ca.AverageCcuServerList group tb by new { tb.Timestamp } into grp select new { Average = grp.Sum(t => long.Parse(t.Average)) }).FirstOrDefault();
                    ca.AcuSum = acuSum.Average.ToString();
                }
                catch (Exception ex)
                {
                    ca.AcuSum = "";
                }
                ca.DateTime = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                ca.Date = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local).ToString("dd/MM/yyyy");
                ca.TimeStamp = long.Parse(timeList[i]);
                ccuAverageList.Add(ca);
            }
            string startTime = timeList[0];

            DataTable dt = ConverterWorkData.ToDataTable(ccuAverageList, 1, gameZoneList.Count());

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;
            TableRow rowHeader = new TableRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Date",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Acu Sum",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    //UIHelpers.CreateTableCell("CcuServerList",Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[5].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[6].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader")
                }
            );
            foreach (string gameZone in gameZoneList)
            {
                rowHeader.Cells.Add(UIHelpers.CreateTableCell(gameZone, Unit.Percentage(6), HorizontalAlign.Center, "cellHeader"));
            }
            table.Rows.Add(rowHeader);
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    TableRow rowEmpty = new TableRow();
                    rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 4));
                    table.Rows.Add(rowEmpty);
                }
                else
                {
                    //labelCountOfUser.Text = string.Format("{0:N0}", dt.Rows.Count);
                    string css;
                    int stt = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        stt += 1;
                        css = stt % 2 == 0 ? "cell1" : "cell2";
                        TableRow row = new TableRow();
                        row.Cells.AddRange
                        (
                            new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Date"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["AcuSum"]),HorizontalAlign.Left,css),
                                   // UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[5].ColumnName]),HorizontalAlign.Left,css),
                                    //UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[6].ColumnName]),HorizontalAlign.Left,css),

                                }
                        );
                        foreach (string gameZone in gameZoneList)
                        {
                            row.Cells.Add(UIHelpers.CreateTableCell(Converter.ToString(dr[gameZone]), HorizontalAlign.Left, css));
                        }
                        table.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return table;
        }
        private Table getOnlinePeopleNumTabble(List<Ccu> paramDt)
        {
            //string _startDate = txtFromDate.Text;
            //string _endDate = txtToDate.Text;
            // IEnumerable<Ccu> ccuList=dt.AsEnumerable().ToList<Ccu>().;

            List<Ccu> oldList = paramDt;
            List<Ccu> dataList = new List<Ccu>(oldList.Count);

            oldList.ForEach((item) =>
            {
                dataList.Add(new Ccu(item));
            });

            //List<Ccu> dataList = new List<Ccu>(paramDt);//ConverterWorkData.ToList<Ccu>(paramDt).OrderBy(o=>o.Timestamp).ToList();
            foreach (Ccu ccuItem in dataList)
            {
                ccuItem.Timestamp = truncateTime(ccuItem.Timestamp, "dd/MM/yyyy HH:mm");
            }
            dataList = (from tb in dataList where ConverterWorkData.ConvertFromUnixTimestamp(double.Parse(tb.Timestamp)).Minute % 5 == 0 select tb).ToList();

            var gameZoneList = dataList.Select(s => s.GameZone).Distinct();
            Dictionary<int, List<Ccu>> gameZoneDataDic = new Dictionary<int, List<Ccu>>();
            for (int i = 0; i < gameZoneList.Count(); i++)
            {
                List<Ccu> gameZoneData = dataList.Where(w => w.GameZone.Equals(gameZoneList.ElementAt(i))).OrderBy(o => o.Timestamp).ToList();
                gameZoneDataDic.Add(int.Parse(gameZoneList.ElementAt(i)), gameZoneData);
            }

            var timeList = dataList.OrderBy(o => o.Timestamp).Select(s => s.Timestamp).Distinct().ToList();
            //timeList = (from tb in timeList
            //where ConverterWorkData.ConvertFromUnixTimestamp(double.Parse(tb)).Minute % 5 == 0
            //select tb).ToList();
            var ccuPresentList = dataList.GroupBy(c => c.Timestamp).Select(g => new { Timestamp = g.Key, Sum = g.Sum(h => int.Parse(h.Num)) }).OrderBy(o => o.Timestamp).ToList();

            //var ccuDayBefore = dataList.Where(w => w.Timestamp.Equals(ConverterWorkData.ConvertToUnixTimestamp(DateTime.ParseExact(ConverterWorkData.ConvertFromUnixTimestamp(long.Parse(w.Timestamp)).AddDays(-1).ToString("dd/MM/yyyy hh:mm"),"dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture)))).GroupBy(c => c.Timestamp).Select(g => new { Timestamp = (int.Parse(g.Key) + 86400).ToString(), Sum = g.Sum(h => int.Parse(h.Num)) }).OrderBy(o => o.Timestamp).ToList();
            //var CcuSevenDayBefore = dataList.Where(w => w.Timestamp.Equals((int.Parse(w.Timestamp) - 604800).ToString())).GroupBy(c => c.Timestamp).Select(g => new { Timestamp = (int.Parse(g.Key) + 604800).ToString(), Sum = g.Sum(h => int.Parse(h.Num)) }).OrderBy(o => o.Timestamp).ToList();
            //var CcuMonthBefore = dataList.Where(w => w.Timestamp.Equals((int.Parse(w.Timestamp) - 604800).ToString())).GroupBy(c => c.Timestamp).Select(g => new { Timestamp = (int.Parse(g.Key) + 604800).ToString(), Sum = g.Sum(h => int.Parse(h.Num)) }).OrderBy(o => o.Timestamp).ToList();

            List<CcuFiveMinuteOnce> CcuFiveMinuteOnceList = new List<CcuFiveMinuteOnce>();

            for (int i = 0; i < timeList.Count; i++)
            {
                //string strDt= ConverterWorkData.ConvertFromUnixTimestamp(long.Parse(timeList[i])).ToString("dd/MM/yyyy hh:mm");
                //DateTime dtTemp = DateTime.ParseExact(strDt, "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
                //string timeStampTemp = truncateSecond(timeList[i]);
                CcuFiveMinuteOnce cfmo = new CcuFiveMinuteOnce();
                foreach (KeyValuePair<int, List<Ccu>> entry in gameZoneDataDic)
                {
                    //var ccu = entry.Value.ToArray()[i];
                    Ccu ccu = null;
                    try
                    {
                        ccu = entry.Value.Where(w => w.Timestamp.Equals(timeList[i])).FirstOrDefault();
                        if (ccu == null)
                        {
                            ccu = new Ccu() { GameZone = entry.Key.ToString(), Timestamp = timeList[i], Num = "" };
                        }
                    }
                    catch (Exception ex)
                    {

                        ccu = new Ccu() { GameZone = entry.Key.ToString(), Timestamp = timeList[i], Num = "" };
                    }

                    cfmo.CcuServerList.Add(ccu);
                }
                try
                {
                    var ccuPresent = ccuPresentList.Where(w => w.Timestamp.Equals(timeList[i])).Single();
                    cfmo.SumCcuPresent = ccuPresent.Sum.ToString();
                }
                catch (Exception ex)
                {
                    cfmo.SumCcuPresent = "";
                }

                //if (ccuDayBefore.Count > 0)
                // {
                //cfmo.SumCcuDayBefore = ccuDayBefore.ToArray()[i].Sum.ToString();
                //cfmo.SumCcuDayBefore = ccuDayBefore.Where(w => w.Timestamp.Equals(timeList[i])).Select(s=>s.Sum).Single().ToString();

                //string strDateBeforeTs = truncateSecond(dayBeforeTs.ToString());
                try
                {
                    DateTime dayBeforedt = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local).AddDays(-1);
                    long dayBeforeTs = long.Parse(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dayBeforedt, DateTimeKind.Local).ToString());
                    var temp = dataList.Where(w => w.Timestamp.Equals(dayBeforeTs.ToString())).GroupBy(c => c.Timestamp).Select(g => new { Sum = g.Sum(h => int.Parse(h.Num)) }).Single();
                    cfmo.SumCcuDayBefore = temp.Sum.ToString();
                }
                catch (Exception ex)
                {
                    cfmo.SumCcuDayBefore = "";
                }

                try
                {
                    DateTime dayBeforedt = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local).AddDays(-7);
                    long sevenDayBeforeTs = long.Parse(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dayBeforedt, DateTimeKind.Local).ToString());
                    var temp = dataList.Where(w => w.Timestamp.Equals(sevenDayBeforeTs.ToString())).GroupBy(c => c.Timestamp).Select(g => new { Sum = g.Sum(h => int.Parse(h.Num)) }).Single();
                    cfmo.SumCcuSevenDayBefore = temp.Sum.ToString();
                }
                catch (Exception ex)
                {
                    cfmo.SumCcuSevenDayBefore = "";
                }

                try
                {
                    DateTime dayBeforedt = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local).AddMonths(-1);
                    long thirtyDayBeforeTs = long.Parse(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dayBeforedt, DateTimeKind.Local).ToString());
                    var temp = dataList.Where(w => w.Timestamp.Equals(thirtyDayBeforeTs.ToString())).GroupBy(c => c.Timestamp).Select(g => new { Sum = g.Sum(h => int.Parse(h.Num)) }).Single();
                    cfmo.SumCcuMonthBefore = temp.Sum.ToString();
                }
                catch (Exception ex)
                {
                    cfmo.SumCcuMonthBefore = "";
                }

                // }
                // if (CcuSevenDayBefore.Count > 0)
                // {
                // cfmo.SumCcuSevenDayBefore = CcuSevenDayBefore.ToArray()[i].Sum.ToString();
                // }
                // if (CcuMonthBefore.Count > 0)
                // {
                //cfmo.SumCcuMonthBefore = CcuMonthBefore.ToArray()[i].Sum.ToString();
                //}
                cfmo.DateTime = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                cfmo.Date = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local).ToString("dd/MM/yyyy HH:mm");
                cfmo.TimeStamp = long.Parse(timeList[i]);
                CcuFiveMinuteOnceList.Add(cfmo);
            }
            string startTime = timeList[0];
            //CcuFiveMinuteOnceList= CcuFiveMinuteOnceList.Where((w =>Math. w.TimeStamp - long.Parse(startTime)%300==0).ToList();
            // List<CcuFiveMinuteOnce> result = (from tb in CcuFiveMinuteOnceList
            //where tb.DateTime.Minute % 5 == 0
            // select tb).ToList();
            DataTable dt = ConverterWorkData.ToDataTable(CcuFiveMinuteOnceList, 0, gameZoneList.Count());
            // string str = ""; ;
            //foreach(var e in data)
            //{
            // str+=e.Timestamp+":"+e.Sum+"<br/>";
            //}
            //Label1.Text = str;
            //data.GroupBy(s => s.Timestamp).Select(g => g.Sum(c => c.Num));
            //var data = data.GroupBy(s => s.Timestamp).Select(g => new
            // {
            //     Timestamp = g.Timestamp,
            //     GameZone = g.GameZone,
            //     Num = g.Num
            // });
            // select new Ccu
            //{
            //    Timestamp = ccu.Field<string>("Timestamp"),
            //    GameZone=ccu.Field<string>("GameZone"),
            //    Num = ccu.Field<int>("Num"),
            //};
            //data.GroupBy(s => s.Timestamp).Select(n => n.Sum(m => m.Num));

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;
            TableRow rowHeader = new TableRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Date",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("SumCcuMonthBefore",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("SumCcuSevenDayBefore",Unit.Percentage(10),HorizontalAlign.Left, "cellHeader"),  
                    UIHelpers.CreateTableCell("SumCcuDayBefore",Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    UIHelpers.CreateTableCell("SumCcuPresent",Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell("CcuServerList",Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[8].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[9].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader")
                }
            );
            foreach (string gameZone in gameZoneList)
            {
                rowHeader.Cells.Add(UIHelpers.CreateTableCell(gameZone, Unit.Percentage(6), HorizontalAlign.Center, "cellHeader"));
            }
            table.Rows.Add(rowHeader);
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    TableRow rowEmpty = new TableRow();
                    rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 4));
                    table.Rows.Add(rowEmpty);
                }
                else
                {
                    //labelCountOfUser.Text = string.Format("{0:N0}", dt.Rows.Count);
                    string css;
                    int stt = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        stt += 1;
                        css = stt % 2 == 0 ? "cell1" : "cell2";
                        TableRow row = new TableRow();
                        row.Cells.AddRange
                        (
                            new TableCell[]
                                {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Date"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["SumCcuMonthBefore"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["SumCcuSevenDayBefore"]), HorizontalAlign.Left, css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["SumCcuDayBefore"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["SumCcuPresent"]),HorizontalAlign.Left,css),
                                   // UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[8].ColumnName]),HorizontalAlign.Left,css),
                                    //UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[9].ColumnName]),HorizontalAlign.Left,css),

                                }
                        );
                        foreach (string gameZone in gameZoneList)
                        {
                            row.Cells.Add(UIHelpers.CreateTableCell(Converter.ToString(dr[gameZone]), HorizontalAlign.Left, css));
                        }
                        table.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return table;
        }

        protected void statisticsTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = int.Parse(statisticsTypeDropDownList.SelectedValue);
        }

        protected void GetData_Click(object sender, EventArgs e)
        {
            DateTime dtFormDate = convertStringToDateTime(txtFromDate.Text);
            DateTime dtToDate = convertStringToDateTime(txtToDate.Text);
            List<DateTime> dateTimeList = getDateListBetween(dtFormDate, dtToDate);
            if (ccu != null && ccu.Count > 0)
            {
                ccu.Clear();
            }
            ccu = getCcu(dateTimeList);
        }

        protected void loginDataLK_Click(object sender, EventArgs e)
        {
            container.ActiveViewIndex = 1;
        }

        protected void GetLoginData_Click(object sender, EventArgs e)
        {
            DateTime dtFormDate = convertStringToDateTime(txtFromDate.Text);
            DateTime dtToDate = convertStringToDateTime(txtToDate.Text);
            List<DateTime> dateTimeList = getDateListBetween(dtFormDate, dtToDate);
            au = getAu(dateTimeList);
        }
        class DauAtTime
        {
            public string Timestamp { get; set; }
            public string Account { get; set; }
        }

        private Table getLoginData()
        {
            //System.GC.Collect();
            List<Au> auList = new List<Au>(au.Count);

            au.ForEach((item) =>
            {
                auList.Add(new Au(item));
            });

            List<Ccu> ccuList = new List<Ccu>(ccu.Count);

            ccu.ForEach((item) =>
            {
                ccuList.Add(new Ccu(item));
            });

            foreach (Au auItem in auList)
            {
                auItem.Timestamp = truncateTime(auItem.Timestamp, "dd/MM/yyyy");
            }
            foreach (Ccu ccuItem in ccuList)
            {
                ccuItem.Timestamp = truncateTime(ccuItem.Timestamp, "dd/MM/yyyy");
            }

            //var gameZoneList = dataList.Select(s => s.GameZone).Distinct();
            //Dictionary<int, List<Pcu>> gameZoneDataDic = new Dictionary<int, List<Pcu>>();
            //for (int i = 0; i < gameZoneList.Count(); i++)
            //{
            // List<Pcu> gameZoneData = (from tb in dataList where tb.GameZone.Equals(gameZoneList.ElementAt(i).ToString()) group tb by new { tb.GameZone, tb.Timestamp } into grp select new Pcu { GameZone = grp.Key.GameZone, Timestamp = grp.Key.Timestamp, Max = grp.Max(t => long.Parse(t.Num)).ToString() }).ToList(); //dataList.GroupBy(g => g.Timestamp, g => g.GameZone).Where(w => w.Key.Equals(gameZoneList.ElementAt(i))).OrderBy(o => o.Timestamp).ToList();
            //gameZoneDataDic.Add(int.Parse(gameZoneList.ElementAt(i)), gameZoneData);
            // }
            //var timeList = ((from tb1 in ccuList select tb1.Timestamp).Union(from tb2 in auList select tb2.Timestamp)).Distinct().ToList();
            //timeList.Sort();

            List<string> timeList = new List<string>();
            timeList.RemoveAt(0);
            foreach (var item in ccuList)
            {
                timeList.Add(item.Timestamp);
            }
            foreach (var item in auList)
            {
                timeList.Add(item.Timestamp);
            }
            timeList.Distinct().OrderBy(o => o);

            //var acuSumList = dataList.GroupBy(c => c.Timestamp).Select(g => new { Timestamp = g.Key, Average = g.Average(h => int.Parse(h.Num)) }).OrderBy(o => o.Timestamp).ToList();

            List<LoginData> loginDataList = new List<LoginData>();
            var pcuSumList = (from tb2 in (from tb in ccuList group tb by new { tb.GameZone, tb.Timestamp } into grp select new { grp.Key.GameZone, grp.Key.Timestamp, Max = grp.Max(m => long.Parse(m.Num)) }) group tb2 by new { tb2.Timestamp } into grp2 select new { grp2.Key.Timestamp, PcuSum = grp2.Sum(s => s.Max) }).ToList();
            //var pcuSumList = (from tb in ccuList group tb by new { tb.GameZone, tb.Timestamp } into grp select new { grp.Key.GameZone, grp.Key.Timestamp, Max = grp.Max(m => m.Num) }).ToList();
            //var temp = ccuList.Where(w => w.GameZone.Equals("2491372") && w.Timestamp.Equals("1423699200")).Max(m=>long.Parse(m.Num));
            var dauList = (from tb2 in (from tb in auList group tb by new { tb.Account, tb.Timestamp } into grp select new { grp.Key.Account, grp.Key.Timestamp }) group tb2 by new { tb2.Timestamp } into grp2 select new { grp2.Key.Timestamp, Count = grp2.Count() });
            //var nauList=(from tb in auList )
            for (int i = 0; i < timeList.Count; i++)
            {
                //string strDt= ConverterWorkData.ConvertFromUnixTimestamp(long.Parse(timeList[i])).ToString("dd/MM/yyyy hh:mm");
                //DateTime dtTemp = DateTime.ParseExact(strDt, "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
                //string timeStampTemp = truncateSecond(timeList[i]);
                LoginData ld = new LoginData();
                var nau = "";
                string A7 = "";
                string A30 = "";

                var accountBeforeAtTimeList = (from tb in auList where long.Parse(tb.Timestamp) < long.Parse(timeList[i]) select new { tb.Account }).Distinct().ToList();
                //var nauList = (from t1 in auList
                //where !(from t2 in accountBeforeAtTimeList select t2.Account).Contains(t1.Account) && t1.Timestamp.Equals(timeList[i])
                //select t1).GroupBy(g => g.Account);
                List<string> nauList = new List<string>();

                #region accountAtTimeList
                //var accountAtTimeList = (from tb in auList where tb.Timestamp.Equals(timeList[i]) select new { tb.Account }).Distinct().ToList();
                var accountAtTimeList = new[] { new { Account = "" } }.ToList();
                accountAtTimeList.RemoveAt(0);
                foreach (var item in auList)
                {
                    if (item.Timestamp.Equals(timeList[i]))
                    {
                        accountAtTimeList.Add(new { Account = item.Account });
                    }
                }
                accountAtTimeList.Distinct();

                #endregion

                if (accountBeforeAtTimeList.Count() != 0)
                {
                    foreach (var item in accountAtTimeList)
                    {
                        int numLoop = 0;
                        for (int k = 0; k < accountBeforeAtTimeList.Count(); k++)
                        {
                            if (item.Account.Equals(accountBeforeAtTimeList[k].Account))
                            {
                                break;
                            }
                            else
                            {
                                numLoop++;
                            }

                        }
                        if (numLoop == accountBeforeAtTimeList.Count())
                        {
                            nauList.Add(item.Account);
                        }
                    }

                }
                else
                    if (accountBeforeAtTimeList.Count() == 0)
                    {
                        foreach (var itemTemp in accountAtTimeList)
                        {
                            nauList.Add(itemTemp.Account);
                        }
                    }

                try
                {

                    //var accountAll = auList.Where(w=>w.Timestamp.Equals(timeList[i])).Select(s => s.Account);
                    //nau = auList.Where(w => !w.Account.Contains(accountBeforeLogin) && w.Timestamp.Equals(timeList[i])).GroupBy(g => new { g.Account }).Distinct().Count().ToString();
                    nau = nauList.Distinct().Count().ToString();
                    //foreach(var item in accountBeforeNotLogin)
                    //{

                    //}

                }
                catch (Exception ex)
                {
                    nau = "";
                }

                try
                {
                    var check = (from tb in timeList where tb.CompareTo(timeList[i]) < 0 select tb).Count().ToString();

                    if (int.Parse(check) >= 7)
                    {
                        DateTime dtTemp = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                        List<DateTime> sevenDayBeforeList = new List<DateTime>();
                        //sevenDayBeforeList.Add(dtTemp);
                        for (int j = 1; j <= 7; j++)
                        {
                            sevenDayBeforeList.Add(dtTemp.AddDays(-j));
                        }
                        List<List<string>> dauAtTimeList = new List<List<string>>();
                        var dauGroupAccountTimeStampList = (from tb in auList group tb by new { tb.Account, tb.Timestamp } into grp select new { grp.Key.Account, grp.Key.Timestamp });
                        foreach (DateTime dtItem in sevenDayBeforeList)
                        {
                            string dtCurrent = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dtItem, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                            dauAtTimeList.Add(dauGroupAccountTimeStampList.Where(w => w.Timestamp.Equals(dtCurrent)).Select(s => s.Account).ToList());
                        }

                        List<string> unionSevenDayBefore = new List<string>();
                        foreach (List<string> item in dauAtTimeList)
                        {
                            //var listTemp = (from tb in item select tb);//.Union(from tb1 in unionDaySevenDayBefore select tb1).ToList();
                            foreach (var itemi in item)
                            {
                                unionSevenDayBefore.Add(itemi);
                            }

                        }
                        A7 = unionSevenDayBefore.Distinct().Count().ToString();
                    }
                }
                catch (Exception ex)
                {
                    A7 = "";
                }

                try
                {
                    var check = (from tb in timeList where tb.CompareTo(timeList[i]) < 0 select tb).Count().ToString();

                    if (int.Parse(check) >= 30)
                    {
                        DateTime dtTemp = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                        List<DateTime> thirtyDayBeforeList = new List<DateTime>();
                        //sevenDayBeforeList.Add(dtTemp);
                        for (int j = 1; j <= 30; j++)
                        {
                            thirtyDayBeforeList.Add(dtTemp.AddDays(-j));
                        }
                        List<List<string>> dauAtTimeList = new List<List<string>>();
                        var dauGroupAccountTimeStampList = (from tb in auList group tb by new { tb.Account, tb.Timestamp } into grp select new { grp.Key.Account, grp.Key.Timestamp });
                        foreach (DateTime dtItem in thirtyDayBeforeList)
                        {
                            string dtCurrent = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dtItem, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                            dauAtTimeList.Add(dauGroupAccountTimeStampList.Where(w => w.Timestamp.Equals(dtCurrent)).Select(s => s.Account).ToList());
                        }

                        List<string> unionThirtyDayBefore = new List<string>();
                        foreach (List<string> item in dauAtTimeList)
                        {
                            //var listTemp = (from tb in item select tb);//.Union(from tb1 in unionDaySevenDayBefore select tb1).ToList();
                            foreach (var itemi in item)
                            {
                                unionThirtyDayBefore.Add(itemi);
                            }

                        }
                        A7 = unionThirtyDayBefore.Distinct().Count().ToString();
                    }
                }
                catch (Exception ex)
                {
                    A30 = "";
                }

                var accountAtTimelist = (from tb in auList where tb.Timestamp.Equals(timeList[i]) select new { tb.Account });
                var checkDayNum = (from tb in timeList where tb.CompareTo(timeList[i]) > 0 select tb).Count().ToString();
                if (int.Parse(checkDayNum) >= 7)
                {
                    DateTime dtTemp = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                    DateTime dtAtDaySeven = dtTemp.AddDays(-7);
                    string timeStampAtDaySeven = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dtAtDaySeven, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                    int count = 0;
                    foreach (var item in accountAtTimelist)
                    {
                        var hasLoginInSevenDayAfter = (from tb in auList where tb.Account.Equals(item) && tb.Timestamp.CompareTo(timeStampAtDaySeven) <= 0 select tb).Count().ToString();
                        if (int.Parse(hasLoginInSevenDayAfter) <= 0)
                        {
                            count++;
                        }
                    }
                    ld.LAU = count.ToString();
                }


                var pcuSum = pcuSumList.Where(w => w.Timestamp.Equals(timeList[i])).Select(s => s.PcuSum).FirstOrDefault().ToString();
                ld.PcuSum = pcuSum;

                ld.Nau = nau;
                try
                {
                    ld.Dau = dauList.Where(w => w.Timestamp.Equals(timeList[i])).FirstOrDefault().Count.ToString();
                }
                catch (Exception ex)
                {
                    ld.Dau = "";
                }

                if (string.IsNullOrEmpty(ld.Dau) == false && string.IsNullOrEmpty(ld.LAU) == false)
                {
                    ld.L7Percent = ((int.Parse(ld.Dau) * 100) / int.Parse(ld.LAU)).ToString() + "%";
                }
                else
                {
                    ld.L7Percent = "";
                }

                try
                {
                    var checkDayN = (from tb in timeList where tb.CompareTo(timeList[i]) < 0 select tb).Count().ToString();
                    //var sumNumAccountInStoreAtTime = (from tb in auList where tb.Timestamp.Equals(timeList[i]) select new { tb.Account }).Distinct().Count().ToString();
                    if (int.Parse(checkDayN) >= 7)
                    {
                        DateTime dtTemp = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                        List<DateTime> sevenDayBeforeList = new List<DateTime>();
                        //sevenDayBeforeList.Add(dtTemp);// has stratDate
                        for (int j = 1; j <= 7; j++)
                        {
                            sevenDayBeforeList.Add(dtTemp.AddDays(-j));
                        }

                        //List<List<string>> dauAtTimeList = new List<List<string>>();
                        //var dauGroupAccountTimeStampList = (from tb in auList group tb by new { tb.Account, tb.Timestamp } into grp select new { grp.Key.Account, grp.Key.Timestamp });
                        //var accountAtTimeList = (from tb in auList where tb.Timestamp.Equals(timeList[i]) select new { tb.Account }).Distinct().ToList();
                        int sumNumAccountLossAtTime = 0;
                        foreach (var account in accountAtTimeList)
                        {
                            int numLoop = 0;
                            foreach (DateTime dtItem in sevenDayBeforeList)
                            {

                                string dtCurrent = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dtItem, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                                var count = (from tb in auList where tb.Account.Equals(account.Account) && tb.Timestamp.Equals(dtCurrent) select tb.Account).Count().ToString();
                                if (int.Parse(count) <= 0)
                                {
                                    numLoop++;
                                }
                                //string dtCurrent = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dtItem, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                                //dauAtTimeList.Add(dauGroupAccountTimeStampList.Where(w => w.Timestamp.Equals(dtCurrent)).Select(s => s.Account).ToList()
                            }
                            if (numLoop.Equals(7))
                            {
                                sumNumAccountLossAtTime++;
                            }
                        }
                        ld.SumNumAccountLoss = sumNumAccountLossAtTime.ToString();
                    }
                }
                catch (Exception ex)
                {
                    ld.SumNumAccountLoss = "";
                }

                try
                {
                    var checkDayN = (from tb in timeList where tb.CompareTo(timeList[i]) > 0 select tb).Count().ToString();
                    //var sumNumAccountInStoreAtTime = (from tb in auList where tb.Timestamp.Equals(timeList[i]) select new { tb.Account }).Distinct().Count().ToString();
                    if (int.Parse(checkDayN) >= 7)
                    {
                        DateTime dtTemp = ConverterWorkData.kindUnixTimeStampToKindDateTime(double.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                        List<DateTime> sevenDayAfterList = new List<DateTime>();
                        //sevenDayBeforeList.Add(dtTemp);// has stratDate
                        for (int j = 1; j <= 7; j++)
                        {
                            sevenDayAfterList.Add(dtTemp.AddDays(j));
                        }

                        //List<List<string>> dauAtTimeList = new List<List<string>>();
                        //var dauGroupAccountTimeStampList = (from tb in auList group tb by new { tb.Account, tb.Timestamp } into grp select new { grp.Key.Account, grp.Key.Timestamp });
                        //var accountAtTimeList = (from tb in auList where tb.Timestamp.Equals(timeList[i]) select new { tb.Account }).Distinct().ToList();
                        int NumAccountLossIncreaseNewAtTime = 0;
                        foreach (var account in nauList)
                        {
                            int numLoop = 0;
                            foreach (DateTime dtItem in sevenDayAfterList)
                            {

                                string dtCurrent = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dtItem, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                                var count = (from tb in auList where tb.Account.Equals(account) && tb.Timestamp.Equals(dtCurrent) select tb.Account).Count().ToString();
                                if (int.Parse(count) <= 0)
                                {
                                    numLoop++;
                                }
                                //string dtCurrent = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(dtItem, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                                //dauAtTimeList.Add(dauGroupAccountTimeStampList.Where(w => w.Timestamp.Equals(dtCurrent)).Select(s => s.Account).ToList()
                            }
                            if (numLoop.Equals(7))
                            {
                                NumAccountLossIncreaseNewAtTime++;
                            }
                        }
                        ld.NumAccountLossIncreaseNew = NumAccountLossIncreaseNewAtTime.ToString();
                    }
                }
                catch (Exception ex)
                {

                }

                if (string.IsNullOrEmpty(ld.SumNumAccountLoss) == false && string.IsNullOrEmpty(ld.Dau) == false)
                {
                    ld.PercentAccountLoss = ((int.Parse(ld.SumNumAccountLoss) * 100) / (int.Parse(ld.Dau))).ToString() + "%";
                }
                else
                {
                    ld.PercentAccountLoss = "";
                }
                if (string.IsNullOrEmpty(ld.NumAccountLossIncreaseNew) == false && string.IsNullOrEmpty(ld.Nau) == false && ld.Nau.Equals("0") == false)
                {
                    ld.PercentAccountLossIncreaseNew = ((int.Parse(ld.NumAccountLossIncreaseNew) * 100) / (int.Parse(ld.Nau))).ToString() + "%";
                }
                else
                {
                    ld.PercentAccountLossIncreaseNew = "";
                }
                ld.A30 = A30;
                ld.A7 = A7;
                ld.DateTime = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                ld.Date = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local).ToString("dd/MM/yyyy");
                ld.TimeStamp = long.Parse(timeList[i]);
                loginDataList.Add(ld);
            }
            string startTime = timeList[0];

            DataTable dt = ConverterWorkData.ToDataTable(loginDataList, 3, 0);

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;
            TableRow rowHeader = new TableRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Date",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("pcu sum",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Dau",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Nau",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("A7",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                     UIHelpers.CreateTableCell("A30",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                      UIHelpers.CreateTableCell("LAU",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                      UIHelpers.CreateTableCell("L7Percent",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
            UIHelpers.CreateTableCell("SumNumAccountLoss",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
            UIHelpers.CreateTableCell("PercentAccountLoss",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                UIHelpers.CreateTableCell("NumAccountLossIncreaseNew",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("PercentAccountLossIncreaseNew",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader")
                    //UIHelpers.CreateTableCell("CcuServerList",Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[5].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[6].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader")
                }
            );

            table.Rows.Add(rowHeader);
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    TableRow rowEmpty = new TableRow();
                    rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 4));
                    table.Rows.Add(rowEmpty);
                }
                else
                {
                    //labelCountOfUser.Text = string.Format("{0:N0}", dt.Rows.Count);
                    string css;
                    int stt = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        stt += 1;
                        css = stt % 2 == 0 ? "cell1" : "cell2";
                        TableRow row = new TableRow();
                        row.Cells.AddRange
                        (
                            new TableCell[]
                           {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Date"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["PcuSum"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Dau"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Nau"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["A7"]),HorizontalAlign.Left,css),
                                     UIHelpers.CreateTableCell(Converter.ToString(dr["A30"]),HorizontalAlign.Left,css),
                                      UIHelpers.CreateTableCell(Converter.ToString(dr["LAU"]),HorizontalAlign.Left,css),
                                      UIHelpers.CreateTableCell(Converter.ToString(dr["L7Percent"]),HorizontalAlign.Left,css),
                        UIHelpers.CreateTableCell(Converter.ToString(dr["SumNumAccountLoss"]),HorizontalAlign.Left,css),
                        UIHelpers.CreateTableCell(Converter.ToString(dr["PercentAccountLoss"]),HorizontalAlign.Left,css),
                         UIHelpers.CreateTableCell(Converter.ToString(dr["NumAccountLossIncreaseNew"]),HorizontalAlign.Left,css),
                        UIHelpers.CreateTableCell(Converter.ToString(dr["PercentAccountLossIncreaseNew"]),HorizontalAlign.Left,css)
                                    //UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[5].ColumnName]),HorizontalAlign.Left,css),
                                    //UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[6].ColumnName]),HorizontalAlign.Left,css),

                           }
                        );

                        table.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return table;
        }

        private Table getLoginNewSave()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            List<Au> auList = new List<Au>(au.Count);

            au.ForEach((item) =>
            {
                auList.Add(new Au(item));
            });

            List<Ccu> ccuList = new List<Ccu>(ccu.Count);

            ccu.ForEach((item) =>
            {
                ccuList.Add(new Ccu(item));
            });

            foreach (Au auItem in auList)
            {
                auItem.Timestamp = truncateTime(auItem.Timestamp, "dd/MM/yyyy");
            }
            foreach (Ccu ccuItem in ccuList)
            {
                ccuItem.Timestamp = truncateTime(ccuItem.Timestamp, "dd/MM/yyyy");
            }

            #region timeList
            //var timeList = ((from tb1 in ccuList select tb1.Timestamp).Union(from tb2 in auList select tb2.Timestamp)).Distinct().ToList();
            //timeList.Sort();
            List<string> timeList = new List<string>();
            DateTime dtFormDate = convertStringToDateTime(txtFromDate.Text);
            DateTime dtToDate = convertStringToDateTime(txtToDate.Text);
            List<DateTime> dateTimeList = getDateListBetween(dtFormDate, dtToDate);
            foreach (DateTime item in dateTimeList)
            {
                string timestamp=truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(item, DateTimeKind.Local).ToString(),"dd/MM/yyyy");
                timeList.Add(timestamp);
            }
            #endregion
            List<LoginNewSave> loginNewSaveList = new List<LoginNewSave>();
          
            for (int i = 0; i < timeList.Count; i++)
            {
               
                LoginNewSave lns = new LoginNewSave();
                var nau = "";

                var accountBeforeAtTimeList = (from tb in auList where long.Parse(tb.Timestamp) < long.Parse(timeList[i]) select new { tb.Account }).Distinct().ToList();
               
                List<string> nauList = new List<string>();
                var accountAtTimeList = (from tb in auList where tb.Timestamp.Equals(timeList[i]) select new { tb.Account }).Distinct().ToList();

                if (accountBeforeAtTimeList.Count() != 0)
                {
                    foreach (var item in accountAtTimeList)
                    {
                        int numLoop = 0;
                        for (int k = 0; k < accountBeforeAtTimeList.Count(); k++)
                        {
                            if (item.Account.Equals(accountBeforeAtTimeList[k].Account))
                            {
                                break;
                            }
                            else
                            {
                                numLoop++;
                            }

                        }
                        if (numLoop == accountBeforeAtTimeList.Count())
                        {
                            nauList.Add(item.Account);
                        }
                    }

                }
                else
                    if (accountBeforeAtTimeList.Count() == 0)
                    {
                        foreach (var itemTemp in accountAtTimeList)
                        {
                            nauList.Add(itemTemp.Account);
                        }
                    }

                //try
                //{

               
                nau = nauList.Distinct().Count().ToString();

                // }
                //catch (Exception ex)
                //{
                // nau = "";
                //}

                lns.Nau = nau;

                lns.DateTime = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local);
                lns.Date = ConverterWorkData.kindUnixTimeStampToKindDateTime(long.Parse(timeList[i]), DateTimeKind.Local, DateTimeKind.Local).ToString("dd/MM/yyyy");
                lns.TimeStamp = long.Parse(timeList[i]);

                DateTime secondDay = lns.DateTime.AddDays(1);
                string secondDayTimeStamp = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(secondDay, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                var check = (from tb in timeList where tb.Equals(secondDayTimeStamp) select new { tb }).Count();

                if (check > 0)
                {
                    int num = 0;
                    foreach (var account in nauList)
                    {
                        var count = (from tb in auList where tb.Account.Equals(account) && tb.Timestamp.Equals(secondDayTimeStamp) select new { tb.Account }).Count();
                        if (count > 0)
                        {
                            num++;
                        }
                    }
                    lns.DaySecondSave = num.ToString();
                    lns.PercentDaySecondSave = ((num * 100 / int.Parse(nau))) + "%";
                }

                DateTime thirdDay = lns.DateTime.AddDays(2);
                string thirdDayTimeStamp = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(thirdDay, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                check = (from tb in timeList where tb.Equals(thirdDayTimeStamp) select new { tb }).Count();
                if (check > 0)
                {
                    int num = 0;
                    foreach (var account in nauList)
                    {
                        var count = (from tb in auList where tb.Account.Equals(account) && tb.Timestamp.Equals(thirdDayTimeStamp) select new { tb.Account }).Count();
                        if (count > 0)
                        {
                            num++;
                        }
                    }
                    lns.DayThirdSave = num.ToString();
                    lns.PercentDayThirdSave = ((num * 100 / int.Parse(lns.Nau))) + "%";
                }
                DateTime sevenDay = lns.DateTime.AddDays(7);
                string sevenDayTimeStamp = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(sevenDay, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                check = (from tb in timeList where tb.Equals(sevenDayTimeStamp) select new { tb }).Count();
                if (check > 0)
                {
                    int num = 0;
                    foreach (var account in nauList)
                    {
                        var count = (from tb in auList where tb.Account.Equals(account) && tb.Timestamp.Equals(sevenDayTimeStamp) select new { tb.Account }).Count();
                        if (count > 0)
                        {
                            num++;
                        }
                    }
                    lns.DaySevenSave = num.ToString();
                    lns.PercentDaySevenSave = ((num * 100 / int.Parse(lns.Nau))).ToString() + "%";
                }
                DateTime fourTeenDay = lns.DateTime.AddDays(14);
                string fourTeenDayTimeStamp = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(fourTeenDay, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                check = (from tb in timeList where tb.Equals(fourTeenDayTimeStamp) select new { tb }).Count();
                if (check > 0)
                {
                    int num = 0;
                    foreach (var account in nauList)
                    {
                        var count = (from tb in auList where tb.Account.Equals(account) && tb.Timestamp.Equals(fourTeenDayTimeStamp) select new { tb.Account }).Count();
                        if (count > 0)
                        {
                            num++;
                        }
                    }
                    lns.DayFourteenSave = num.ToString();
                    lns.PercentDayFourteenSave = ((num * 100 / int.Parse(lns.Nau))) + "%";
                }
                DateTime thirtyDay = lns.DateTime.AddDays(30);
                string thirtyDayTimeStamp = truncateTime(ConverterWorkData.kindDateTimeToKindUnixTimestamp(thirtyDay, DateTimeKind.Local).ToString(), "dd/MM/yyyy");
                check = (from tb in timeList where tb.Equals(thirtyDayTimeStamp) select new { tb }).Count();
                if (check > 0)
                {
                    int num = 0;
                    foreach (var account in nauList)
                    {
                        var count = (from tb in auList where tb.Account.Equals(account) && tb.Timestamp.Equals(thirtyDayTimeStamp) select new { tb.Account }).Count();
                        if (count > 0)
                        {
                            num++;
                        }
                    }
                    lns.DayThirtySave = num.ToString();
                    lns.PercentDayThirtySave = ((num * 100 / int.Parse(lns.Nau))) + "%";
                }
                loginNewSaveList.Add(lns);
            }
            string startTime = timeList[0];

            DataTable dt = ConverterWorkData.ToDataTable(loginNewSaveList, 3, 0);

            Table table = new Table();
            table.CssClass = "table1";
            table.CellSpacing = 1;
            TableRow rowHeader = new TableRow();
            rowHeader.Cells.AddRange
            (
                new TableCell[]
                {     
                    UIHelpers.CreateTableCell("STT",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Date",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Nau",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Day second save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Day third save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Day seven save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Day fourteen save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Day thirty save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                      UIHelpers.CreateTableCell("Percent day second save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Percent day third save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Percent day seven save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Percent day fourteen save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader"),
                    UIHelpers.CreateTableCell("Percent day thirty save",Unit.Percentage(5), HorizontalAlign.Center,"cellHeader")
                    //UIHelpers.CreateTableCell("CcuServerList",Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[5].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader"),  
                    //UIHelpers.CreateTableCell(dt.Columns[6].ColumnName,Unit.Percentage(6),HorizontalAlign.Center, "cellHeader")
                }
            );

            table.Rows.Add(rowHeader);
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    TableRow rowEmpty = new TableRow();
                    rowEmpty.Cells.Add(UIHelpers.CreateTableCell("<p>Không có dữ liệu!</p>", HorizontalAlign.Center, "cell1", 4));
                    table.Rows.Add(rowEmpty);
                }
                else
                {
                    //labelCountOfUser.Text = string.Format("{0:N0}", dt.Rows.Count);
                    string css;
                    int stt = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        stt += 1;
                        css = stt % 2 == 0 ? "cell1" : "cell2";
                        TableRow row = new TableRow();
                        row.Cells.AddRange
                        (
                            new TableCell[]
                           {
                                    UIHelpers.CreateTableCell(stt.ToString(),HorizontalAlign.Center,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Date"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["Nau"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["DaySecondSave"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["DayThirdSave"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["DaySevenSave"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["DayFourteenSave"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["DayThirtySave"]),HorizontalAlign.Left,css),
                                     UIHelpers.CreateTableCell(Converter.ToString(dr["PercentDaySecondSave"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["PercentDayThirdSave"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["PercentDaySevenSave"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["PercentDayFourteenSave"]),HorizontalAlign.Left,css),
                                    UIHelpers.CreateTableCell(Converter.ToString(dr["PercentDayThirtySave"]),HorizontalAlign.Left,css)
                                    //UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[5].ColumnName]),HorizontalAlign.Left,css),
                                    //UIHelpers.CreateTableCell(Converter.ToString(dr[dt.Columns[6].ColumnName]),HorizontalAlign.Left,css),

                           }
                        );

                        table.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            watch.Stop();
            double elapsedMS = watch.ElapsedMilliseconds;
            measureLB.Text = elapsedMS.ToString()+"miliseconds";
            return table;
        }


        protected void CountLoginData_Click(object sender, EventArgs e)
        {
            loginDataPanel.Controls.Add(getLoginData());
        }

        protected void LogiNewSaveButton_Click(object sender, EventArgs e)
        {
            LogiNewSavePanel.Controls.Add(getLoginNewSave());
        }

        protected void loginNewSaveLB_Click(object sender, EventArgs e)
        {
            container.ActiveViewIndex = 2;
        }


    }
}
