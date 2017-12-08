using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService
{
    /// <summary>
    /// WmsInOut 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class WmsInOut : System.Web.Services.WebService
    {

        [WebMethod]
        public string getCellNo(string cigarettecode,int qty)
        {
            return InBound.Business.AtsCellOutService.getCellNo(cigarettecode, qty);
            //return "Hello World";
        }
        [WebMethod]
        public string getCellNoBig(string cigarettecode, int qty)
        {
            return InBound.Business.AtsCellOutService.getCellNoBig(cigarettecode, qty);
            //return "Hello World";
        }
        [WebMethod]
        public string getCellNoEqual(string cigarettecode, int qty)
        {
            return InBound.Business.AtsCellOutService.getCellNoEqual(cigarettecode, qty);
            //return "Hello World";
        }
        [WebMethod]
        public string getCellNoMath(string cigarettecode, int qty)
        {
            return InBound.Business.AtsCellOutService.getCellNoMath(cigarettecode, qty);
            //return "Hello World";
        }
        [WebMethod]
        public string getCellNoSmall(string cigarettecode, int qty)
        {
            return InBound.Business.AtsCellOutService.getCellNoSmall(cigarettecode, qty);
            //return "Hello World";
        }
        [WebMethod]
        public string getCellNoByTime(string cigarettecode)
        {
            return InBound.Business.AtsCellOutService.getCellNoByTime(cigarettecode);
            //return "Hello World";
        }
       
    }
}
