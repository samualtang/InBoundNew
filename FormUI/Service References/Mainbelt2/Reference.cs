﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FormUI.Mainbelt2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Mainbelt2.IMainBelt")]
    public interface IMainBelt {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMainBelt/GetMainBelt", ReplyAction="http://tempuri.org/IMainBelt/GetMainBeltResponse")]
        string GetMainBelt(int value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMainBeltChannel : FormUI.Mainbelt2.IMainBelt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MainBeltClient : System.ServiceModel.ClientBase<FormUI.Mainbelt2.IMainBelt>, FormUI.Mainbelt2.IMainBelt {
        
        public MainBeltClient() {
        }
        
        public MainBeltClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MainBeltClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MainBeltClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MainBeltClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetMainBelt(int value) {
            return base.Channel.GetMainBelt(value);
        }
    }
}
