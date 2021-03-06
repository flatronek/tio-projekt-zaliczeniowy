﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantService.TokenService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TokenObject", Namespace="http://schemas.datacontract.org/2004/07/TokenService.WcfService")]
    [System.SerializableAttribute()]
    public partial class TokenObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TokenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValidityDateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token {
            get {
                return this.TokenField;
            }
            set {
                if ((object.ReferenceEquals(this.TokenField, value) != true)) {
                    this.TokenField = value;
                    this.RaisePropertyChanged("Token");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ValidityDate {
            get {
                return this.ValidityDateField;
            }
            set {
                if ((object.ReferenceEquals(this.ValidityDateField, value) != true)) {
                    this.ValidityDateField = value;
                    this.RaisePropertyChanged("ValidityDate");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TokenService.ITokenService")]
    public interface ITokenService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITokenService/createTokenForUser", ReplyAction="http://tempuri.org/ITokenService/createTokenForUserResponse")]
        RestaurantService.TokenService.TokenObject createTokenForUser(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITokenService/createTokenForUser", ReplyAction="http://tempuri.org/ITokenService/createTokenForUserResponse")]
        System.Threading.Tasks.Task<RestaurantService.TokenService.TokenObject> createTokenForUserAsync(int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITokenService/findUserToken", ReplyAction="http://tempuri.org/ITokenService/findUserTokenResponse")]
        RestaurantService.TokenService.TokenObject findUserToken(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITokenService/findUserToken", ReplyAction="http://tempuri.org/ITokenService/findUserTokenResponse")]
        System.Threading.Tasks.Task<RestaurantService.TokenService.TokenObject> findUserTokenAsync(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITokenServiceChannel : RestaurantService.TokenService.ITokenService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TokenServiceClient : System.ServiceModel.ClientBase<RestaurantService.TokenService.ITokenService>, RestaurantService.TokenService.ITokenService {
        
        public TokenServiceClient() {
        }
        
        public TokenServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TokenServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TokenServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TokenServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public RestaurantService.TokenService.TokenObject createTokenForUser(int userId) {
            return base.Channel.createTokenForUser(userId);
        }
        
        public System.Threading.Tasks.Task<RestaurantService.TokenService.TokenObject> createTokenForUserAsync(int userId) {
            return base.Channel.createTokenForUserAsync(userId);
        }
        
        public RestaurantService.TokenService.TokenObject findUserToken(string token) {
            return base.Channel.findUserToken(token);
        }
        
        public System.Threading.Tasks.Task<RestaurantService.TokenService.TokenObject> findUserTokenAsync(string token) {
            return base.Channel.findUserTokenAsync(token);
        }
    }
}
