using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IMainBelt
    {
        [OperationContract]
        string GetMainBelt(int value);

        /// <summary>
        /// 获取合流机械手的数据
        /// </summary>
        /// <param name="mainbelt">主皮带</param>
        /// <param name="MachineNo"></param>
        /// <returns></returns>
        [OperationContract]
        string GetUnionMachine(int MachineNo);


        [OperationContract]
        string GetUnionCaChe(int MachineNo);


        [OperationContract]
        string GetSortBelt(int MachineNo);

        


        // TODO: 在此添加您的服务操作
    }

   
    
}
