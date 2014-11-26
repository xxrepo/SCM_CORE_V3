﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5466
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectSmartCargoManager.ServiceReference2 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestBase", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.CargoRequestBase))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.CargoResponseSimple))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.CargoRequestSimple))]
    public partial class RequestBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string AirlineIdentifierField;
        
        private string FlightNumberField;
        
        private System.DateTime STDField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string AirlineIdentifier {
            get {
                return this.AirlineIdentifierField;
            }
            set {
                if ((object.ReferenceEquals(this.AirlineIdentifierField, value) != true)) {
                    this.AirlineIdentifierField = value;
                    this.RaisePropertyChanged("AirlineIdentifier");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string FlightNumber {
            get {
                return this.FlightNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.FlightNumberField, value) != true)) {
                    this.FlightNumberField = value;
                    this.RaisePropertyChanged("FlightNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.DateTime STD {
            get {
                return this.STDField;
            }
            set {
                if ((this.STDField.Equals(value) != true)) {
                    this.STDField = value;
                    this.RaisePropertyChanged("STD");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CargoRequestBase", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.CargoResponseSimple))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.CargoRequestSimple))]
    public partial class CargoRequestBase : ProjectSmartCargoManager.ServiceReference2.RequestBase {
        
        private string ArrivalField;
        
        private string DepartureField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Arrival {
            get {
                return this.ArrivalField;
            }
            set {
                if ((object.ReferenceEquals(this.ArrivalField, value) != true)) {
                    this.ArrivalField = value;
                    this.RaisePropertyChanged("Arrival");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Departure {
            get {
                return this.DepartureField;
            }
            set {
                if ((object.ReferenceEquals(this.DepartureField, value) != true)) {
                    this.DepartureField = value;
                    this.RaisePropertyChanged("Departure");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CargoResponseSimple", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    public partial class CargoResponseSimple : ProjectSmartCargoManager.ServiceReference2.CargoRequestBase {
        
        private string ErrorCodeField;
        
        private string ErrorMessageField;
        
        private ProjectSmartCargoManager.ServiceReference2.CargoResponseStatus ResponseStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UploadMessageField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ErrorCode {
            get {
                return this.ErrorCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorCodeField, value) != true)) {
                    this.ErrorCodeField = value;
                    this.RaisePropertyChanged("ErrorCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProjectSmartCargoManager.ServiceReference2.CargoResponseStatus ResponseStatus {
            get {
                return this.ResponseStatusField;
            }
            set {
                if ((this.ResponseStatusField.Equals(value) != true)) {
                    this.ResponseStatusField = value;
                    this.RaisePropertyChanged("ResponseStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UploadMessage {
            get {
                return this.UploadMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.UploadMessageField, value) != true)) {
                    this.UploadMessageField = value;
                    this.RaisePropertyChanged("UploadMessage");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CargoRequestSimple", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    public partial class CargoRequestSimple : ProjectSmartCargoManager.ServiceReference2.CargoRequestBase {
        
        private ProjectSmartCargoManager.ServiceReference2.Cargo[] CargoItemsField;
        
        private string PasswordField;
        
        private string UserField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProjectSmartCargoManager.ServiceReference2.Cargo[] CargoItems {
            get {
                return this.CargoItemsField;
            }
            set {
                if ((object.ReferenceEquals(this.CargoItemsField, value) != true)) {
                    this.CargoItemsField = value;
                    this.RaisePropertyChanged("CargoItems");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Cargo", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    public partial class Cargo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private ProjectSmartCargoManager.ServiceReference2.CargoType CargoTypeField;
        
        private int CheckWeightField;
        
        private string DestinationField;
        
        private string IdentifierField;
        
        private string LoadInfoCodeField;
        
        private int NrUnitsField;
        
        private string OriginField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RemarksField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SpecialInfoCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TareWeightField;
        
        private string TypeCodeField;
        
        private string ULDNumberField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public ProjectSmartCargoManager.ServiceReference2.CargoType CargoType {
            get {
                return this.CargoTypeField;
            }
            set {
                if ((this.CargoTypeField.Equals(value) != true)) {
                    this.CargoTypeField = value;
                    this.RaisePropertyChanged("CargoType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int CheckWeight {
            get {
                return this.CheckWeightField;
            }
            set {
                if ((this.CheckWeightField.Equals(value) != true)) {
                    this.CheckWeightField = value;
                    this.RaisePropertyChanged("CheckWeight");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Destination {
            get {
                return this.DestinationField;
            }
            set {
                if ((object.ReferenceEquals(this.DestinationField, value) != true)) {
                    this.DestinationField = value;
                    this.RaisePropertyChanged("Destination");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Identifier {
            get {
                return this.IdentifierField;
            }
            set {
                if ((object.ReferenceEquals(this.IdentifierField, value) != true)) {
                    this.IdentifierField = value;
                    this.RaisePropertyChanged("Identifier");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string LoadInfoCode {
            get {
                return this.LoadInfoCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.LoadInfoCodeField, value) != true)) {
                    this.LoadInfoCodeField = value;
                    this.RaisePropertyChanged("LoadInfoCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int NrUnits {
            get {
                return this.NrUnitsField;
            }
            set {
                if ((this.NrUnitsField.Equals(value) != true)) {
                    this.NrUnitsField = value;
                    this.RaisePropertyChanged("NrUnits");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Origin {
            get {
                return this.OriginField;
            }
            set {
                if ((object.ReferenceEquals(this.OriginField, value) != true)) {
                    this.OriginField = value;
                    this.RaisePropertyChanged("Origin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Remarks {
            get {
                return this.RemarksField;
            }
            set {
                if ((object.ReferenceEquals(this.RemarksField, value) != true)) {
                    this.RemarksField = value;
                    this.RaisePropertyChanged("Remarks");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecialInfoCode {
            get {
                return this.SpecialInfoCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.SpecialInfoCodeField, value) != true)) {
                    this.SpecialInfoCodeField = value;
                    this.RaisePropertyChanged("SpecialInfoCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TareWeight {
            get {
                return this.TareWeightField;
            }
            set {
                if ((this.TareWeightField.Equals(value) != true)) {
                    this.TareWeightField = value;
                    this.RaisePropertyChanged("TareWeight");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string TypeCode {
            get {
                return this.TypeCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeCodeField, value) != true)) {
                    this.TypeCodeField = value;
                    this.RaisePropertyChanged("TypeCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ULDNumber {
            get {
                return this.ULDNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ULDNumberField, value) != true)) {
                    this.ULDNumberField = value;
                    this.RaisePropertyChanged("ULDNumber");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CargoResponseStatus", Namespace="http://Flyware.net/eLS/2008/10")]
    public enum CargoResponseStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Success = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CargoType", Namespace="http://schemas.datacontract.org/2004/07/Flyware.eLSEntities.ExtServices.ExtCargo")]
    public enum CargoType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pallet = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Container = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        LooseUnit = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FaultContractBase", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.LogicFault))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.SecurityFault))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.SystemFault))]
    public partial class FaultContractBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string ErrorCodeField;
        
        private string ErrorTypeField;
        
        private string InnerMessageField;
        
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ErrorCode {
            get {
                return this.ErrorCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorCodeField, value) != true)) {
                    this.ErrorCodeField = value;
                    this.RaisePropertyChanged("ErrorCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ErrorType {
            get {
                return this.ErrorTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorTypeField, value) != true)) {
                    this.ErrorTypeField = value;
                    this.RaisePropertyChanged("ErrorType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string InnerMessage {
            get {
                return this.InnerMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.InnerMessageField, value) != true)) {
                    this.InnerMessageField = value;
                    this.RaisePropertyChanged("InnerMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LogicFault", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    public partial class LogicFault : ProjectSmartCargoManager.ServiceReference2.FaultContractBase {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SecurityFault", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    public partial class SecurityFault : ProjectSmartCargoManager.ServiceReference2.FaultContractBase {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SystemFault", Namespace="http://Flyware.net/eLS/2008/10")]
    [System.SerializableAttribute()]
    public partial class SystemFault : ProjectSmartCargoManager.ServiceReference2.FaultContractBase {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Flyware.net/eLS/2008/10", ConfigurationName="ServiceReference2.ICargoControlService")]
    public interface ICargoControlService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Flyware.net/eLS/2008/10/ICargoControlService/UploadCargo", ReplyAction="http://Flyware.net/eLS/2008/10/ICargoControlService/UploadCargoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.SystemFault), Action="http://Flyware.net/eLS/2008/10/ICargoControlService/UploadCargoSystemFaultFault", Name="SystemFault")]
        [System.ServiceModel.FaultContractAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.LogicFault), Action="http://Flyware.net/eLS/2008/10/ICargoControlService/UploadCargoLogicFaultFault", Name="LogicFault")]
        [System.ServiceModel.FaultContractAttribute(typeof(ProjectSmartCargoManager.ServiceReference2.SecurityFault), Action="http://Flyware.net/eLS/2008/10/ICargoControlService/UploadCargoSecurityFaultFault" +
            "", Name="SecurityFault")]
        ProjectSmartCargoManager.ServiceReference2.CargoResponseSimple UploadCargo(ProjectSmartCargoManager.ServiceReference2.CargoRequestSimple cargoRequestSimple);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ICargoControlServiceChannel : ProjectSmartCargoManager.ServiceReference2.ICargoControlService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class CargoControlServiceClient : System.ServiceModel.ClientBase<ProjectSmartCargoManager.ServiceReference2.ICargoControlService>, ProjectSmartCargoManager.ServiceReference2.ICargoControlService {
        
        public CargoControlServiceClient() {
        }
        
        public CargoControlServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CargoControlServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CargoControlServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CargoControlServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ProjectSmartCargoManager.ServiceReference2.CargoResponseSimple UploadCargo(ProjectSmartCargoManager.ServiceReference2.CargoRequestSimple cargoRequestSimple) {
            return base.Channel.UploadCargo(cargoRequestSimple);
        }
    }
}
