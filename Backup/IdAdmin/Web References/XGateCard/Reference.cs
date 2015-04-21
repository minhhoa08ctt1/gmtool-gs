﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5448
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.5448.
// 
#pragma warning disable 1591

namespace IDAdmin.XGateCard {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="XGOSoap", Namespace="http://sunsoft.vn")]
    public partial class XGO : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback TopupOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTransactionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public XGO() {
            this.Url = global::IDAdmin.Properties.Settings.Default.IDAdmin_XGateCard_XGO;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event TopupCompletedEventHandler TopupCompleted;
        
        /// <remarks/>
        public event GetTransactionCompletedEventHandler GetTransactionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sunsoft.vn/Topup", RequestNamespace="http://sunsoft.vn", ResponseNamespace="http://sunsoft.vn", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Topup(string PartnerID, string User_ID, string Trace, string Serial, string Pincode, string Type, string Signature) {
            object[] results = this.Invoke("Topup", new object[] {
                        PartnerID,
                        User_ID,
                        Trace,
                        Serial,
                        Pincode,
                        Type,
                        Signature});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TopupAsync(string PartnerID, string User_ID, string Trace, string Serial, string Pincode, string Type, string Signature) {
            this.TopupAsync(PartnerID, User_ID, Trace, Serial, Pincode, Type, Signature, null);
        }
        
        /// <remarks/>
        public void TopupAsync(string PartnerID, string User_ID, string Trace, string Serial, string Pincode, string Type, string Signature, object userState) {
            if ((this.TopupOperationCompleted == null)) {
                this.TopupOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTopupOperationCompleted);
            }
            this.InvokeAsync("Topup", new object[] {
                        PartnerID,
                        User_ID,
                        Trace,
                        Serial,
                        Pincode,
                        Type,
                        Signature}, this.TopupOperationCompleted, userState);
        }
        
        private void OnTopupOperationCompleted(object arg) {
            if ((this.TopupCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TopupCompleted(this, new TopupCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sunsoft.vn/GetTransaction", RequestNamespace="http://sunsoft.vn", ResponseNamespace="http://sunsoft.vn", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetTransaction(string PartnerID, string Trace, string Signature) {
            object[] results = this.Invoke("GetTransaction", new object[] {
                        PartnerID,
                        Trace,
                        Signature});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetTransactionAsync(string PartnerID, string Trace, string Signature) {
            this.GetTransactionAsync(PartnerID, Trace, Signature, null);
        }
        
        /// <remarks/>
        public void GetTransactionAsync(string PartnerID, string Trace, string Signature, object userState) {
            if ((this.GetTransactionOperationCompleted == null)) {
                this.GetTransactionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTransactionOperationCompleted);
            }
            this.InvokeAsync("GetTransaction", new object[] {
                        PartnerID,
                        Trace,
                        Signature}, this.GetTransactionOperationCompleted, userState);
        }
        
        private void OnGetTransactionOperationCompleted(object arg) {
            if ((this.GetTransactionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTransactionCompleted(this, new GetTransactionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void TopupCompletedEventHandler(object sender, TopupCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TopupCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TopupCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    public delegate void GetTransactionCompletedEventHandler(object sender, GetTransactionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.5420")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTransactionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTransactionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591