using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using InBound.Business;
using InBound.Model;
using InBound;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
namespace WebService
{
    /// <summary>
    /// GetMainBelt 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class GetMainBelt : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetMainBeltInfo(int mainBelt)
        {
            OpcServer.Connect();
            List<MainBeltInfo> ListmbInfo = new List<MainBeltInfo>();
           
            int ReadIndex = 0;
            double[] nowplace = new double[40];
            for (int i = 0; i < 40; i++)//从电控读取数据 填充 listmbinfo
            {
                decimal Sortnum = OpcServer.listUnionTaskGroup[mainBelt - 1].ReadD(ReadIndex).CastTo<decimal>(0);//任务号
             
              
                if (Sortnum > 0)//任务号不为0
                {
                    MainBeltInfo info = new MainBeltInfo();
                    nowplace[i] = (OpcServer.listUnionTaskGroup[mainBelt - 1].ReadD((ReadIndex + 1)).CastTo<double>(-1) / 1000);//位置(米)
                    info.SortNum = Sortnum;//任务号
                    info.Place = Convert.ToDecimal(nowplace[i]);//(listMainBelt[mainbelt - 1].ReadD((ReadIndex + 1)).CastTo<int>(-1) / 1000000);//位置(米)
                    info.Quantity = Convert.ToDecimal(OpcServer.listUnionTaskGroup[mainBelt - 1].ReadD((ReadIndex + 2)).CastTo<int>(-1));//数量
                    info.mainbelt = mainBelt.ToString();//主皮带
                    ListmbInfo.Add(info);
                }
                ReadIndex = ReadIndex + 3;
            }
            MainBeltInfoService.GetMainBeltInfo(ListmbInfo); //填充完成之后传进方法 计算 ，
  
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof( List<MainBeltInfo>));
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, ListmbInfo);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        
          
        }
    }
}
