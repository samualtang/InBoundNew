using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;


namespace WCFMainBeltInfo
{
    [ServiceContract]
   public interface IMainBeltService
    {
        [OperationContract]
        string GetMainBeltInfo(int MainBelt);
    }
}
