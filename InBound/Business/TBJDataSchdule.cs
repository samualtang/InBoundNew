using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
    public class TBJDataSchdule
    {
        decimal routCPagNum = 0, orderPagNum = 0, shaednum = 0, ordercount = 0, UNIONTASKPACKAGENUM = 0, NORMALPACKAGENUM = 0, UNNORMALPACKAGENUM = 0;
        /// <summary>
        /// 根据包装机号 生成 贴标机数据
        /// </summary>
        /// <param name="packageno"></param>
        public void CallBackTBJ(decimal packageno)
        {
            using (Entities en = new Entities())
            {
                //获取当前包装机的数据

                //en.Configuration.AutoDetectChangesEnabled = false;
                //en.Configuration.ValidateOnSaveEnabled = false;
                //获取当前包装机最大条烟流水号
                var cALLBACKs = (from item in en.T_PACKAGE_CALLBACK where item.PACKAGENUM == packageno select item).ToList();
                decimal maxCigNum = 1;
                decimal maxSortnum = 0;
                if (cALLBACKs.Any())
                {
                    maxSortnum = cALLBACKs.Max(a => a.SORTNUM);//获取最大的任务号

                }
                //根据最大任务号开始获取数据（作用：如果在掉电或者断网的情况，无需全部重新生成，接着生成就OK了）
                var pagTask = (from item in en.T_PACKAGE_TASK where item.PACKAGENO == packageno && item.SORTNUM >= maxSortnum orderby item.SORTNUM, item.PACKTASKNUM,item.CIGTYPE, item.CIGSEQ select item).ToList();
                if (!pagTask.Any())//如果不包含任何数据
                {
                    return;
                }

                //避免重复生成 
                foreach (var item in cALLBACKs.Where(a => a.SORTNUM == maxSortnum).ToList())
                {
                    en.T_PACKAGE_CALLBACK.DeleteObject(item);
                    cALLBACKs.Remove(item);
                }
                en.SaveChanges();
                if (cALLBACKs.Any())
                {
                    maxCigNum = cALLBACKs.Max(a => a.CIGNUM);

                }
                //获取包装机视图
                var needInfo = (from item in en.V_PRODUCE_PACKAGEINFO     orderby item.TASKNUM select item).ToList();
            
                T_PACKAGE_CALLBACK tb;
                string billcode = "";//存放订单 
                decimal packtasknum = 0;
                decimal cigseq =1  ;
                try
                {
                    var regioncode  =pagTask.Select(a=> new { regioncode =  a.REGIONCODE}).Distinct().ToList();//获取所有未生成的车组
                    foreach (var region in regioncode)//单个车组循环
                    {
                        var oneRegion = pagTask.Where(a => a.REGIONCODE == region.regioncode).ToList();//获取一个车组的数据

                        billcode = string.Empty;//一个车组后重置
                        packtasknum = 0;
                       
                        foreach (var item in oneRegion)//循环单个车组的数据
                        {
                            tb = new T_PACKAGE_CALLBACK();
                            if (!item.BILLCODE.Equals(billcode))//存入新的订单号 ,一个订单插入一次数据
                            {
                                //en.SaveChanges(); 
                                routCPagNum = pagTask.Where(a => a.REGIONCODE == item.REGIONCODE).Max(a => a.ALLPACKAGESEQ) ?? 0;//车组总包数
                                orderPagNum = pagTask.Where(a => a.BILLCODE == item.BILLCODE).Max(a => a.PACKAGESEQ) ?? 0; //订单总包数
                                shaednum = pagTask.Where(a => a.BILLCODE == item.BILLCODE && a.CIGTYPE == "2").Sum(a => a.NORMALQTY) ?? 0;//订单异型烟数量
                                ordercount = needInfo.Where(a => a.REGIONCODE == item.REGIONCODE).Select(a => new { billcode = a.BILLCODE }).Distinct().Count();//车组内订单数
                                UNIONTASKPACKAGENUM = GetBillPackNum(en, item.BILLCODE, 0);//合包总包数  
                                NORMALPACKAGENUM = GetBillPackNum(en, item.BILLCODE, 1);//常规烟总包数
                                UNNORMALPACKAGENUM = GetBillPackNum(en, item.BILLCODE, 2);//异型烟总包数  
                            }
                            if (!item.PACKTASKNUM.Equals(packtasknum))
                            {
                                packtasknum = item.PACKTASKNUM ?? 0;
                                cigseq = 1;
                            }
                            var firstTask = needInfo.Where(a => a.BILLCODE == item.BILLCODE).FirstOrDefault();//订单信息 
                            if (firstTask == null)
                            {
                                throw new Exception("未找到订单" + item.BILLCODE);

                            }
                            if (item.NORMALQTY > 1)//如果条烟数量大于1 则需要拆分成一条一条的记录
                            {
                                for (int i = 1; i <= item.NORMALQTY; i++)//
                                {

                                    tb = new T_PACKAGE_CALLBACK();
                                    tb.BILLCODE = item.BILLCODE;//订单
                                    tb.ROUTEPACKAGENUM = routCPagNum;//车组总包数
                                    tb.ORDERPACKAGENUM = orderPagNum;//订单总包数
                                    tb.PACKAGESEQ = item.PACKAGESEQ;//订单内包序
                                    tb.CIGARETTEQTY = 1;//品牌条烟数
                                    tb.SHAPEDNUM = shaednum;//订单异型烟数量
                                    tb.CIGARETTECODE = item.CIGARETTECODE;//卷烟编码
                                    tb.CIGARETTENAME = item.CIGARETTENAME;//卷烟名称
                                    tb.CIGARETTETYPE = item.CIGTYPE;//卷烟类型
                                    tb.ROUTECODE = firstTask.REGIONCODE;//车组编号
                                    tb.PACKAGEQTY = item.PACKAGEQTY;//包内条烟数量
                                    tb.ORDERDATE = item.ORDERDATE;//订单日期
                                    tb.LINECODE = item.MIANBELT.ToString();//线路编号
                                    tb.ORDERCOUNT = ordercount;  //车组内订单数
                                    tb.ORDERSEQ = firstTask.SORTSEQ;//订单户序 firstTask.SORTSEQ 
                                    tb.CIGSEQ = cigseq++;//条烟顺序
                                    tb.EXPORT = item.PACKAGENO ?? 0;//出口号（包装机号）
                                    tb.PACKAGENUM = item.PACKAGENO;// 包装机号    
                                    tb.ORDERQUANTITY = item.ORDERQTY;//订单总数
                                    tb.ADDRESS = firstTask.CONTACTADDRESS;//订单地址
                                    tb.CUSTOMERNAME = firstTask.CUSTOMERNAME;//客户名称
                                    tb.CUSTOMERNO = firstTask.CUSTOMERCODE;//客户编码                          
                                    tb.ORDERURL = firstTask.URL;//客户URL   
                                    tb.ORDERAMOUNT = firstTask.TOTALAMOUNT;//订单总金额；
                                    tb.PAYFLAG = firstTask.CUSTTYPE;//结算状态  
                                    tb.SEQ = item.ALLPACKAGESEQ;//整齐包序
                                    tb.UNIONTASKPACKAGENUM = UNIONTASKPACKAGENUM;//合包总包数  
                                    tb.NORMALPACKAGENUM = NORMALPACKAGENUM;//常规烟总包数
                                    tb.UNNORMALPACKAGENUM = UNNORMALPACKAGENUM;//异型烟总包数  
                                    tb.SORTNUM = item.SORTNUM ?? 0;//流水号
                                    tb.CIGNUM = maxCigNum++;// 每台包装机从1 增长 
                                    tb.SYNSEQ = item.SYNSEQ;//批次号   
                                    en.T_PACKAGE_CALLBACK.AddObject(tb);
                                }
                            }
                            else
                            {
                                tb.BILLCODE = item.BILLCODE;//订单
                                tb.ROUTEPACKAGENUM = routCPagNum;//车组总包数
                                tb.ORDERPACKAGENUM = orderPagNum;//订单总包数
                                tb.PACKAGESEQ = item.PACKAGESEQ;//订单内包序
                                tb.CIGARETTEQTY = 1;//品牌条烟数
                                tb.SHAPEDNUM = shaednum;//订单异型烟数量
                                tb.CIGARETTECODE = item.CIGARETTECODE;//卷烟编码
                                tb.CIGARETTENAME = item.CIGARETTENAME;//卷烟名称
                                tb.CIGARETTETYPE = item.CIGTYPE;//卷烟类型
                                tb.ROUTECODE = firstTask.REGIONCODE;//车组编号
                                tb.PACKAGEQTY = item.PACKAGEQTY;//包内条烟数量
                                tb.ORDERDATE = item.ORDERDATE;//订单日期
                                tb.LINECODE = item.MIANBELT.ToString();//线路编号
                                tb.ORDERCOUNT = ordercount;  //车组内订单数
                                tb.ORDERSEQ = firstTask.SORTSEQ;//订单户序 firstTask.SORTSEQ 
                                tb.CIGSEQ = cigseq++;//条烟顺序
                                tb.EXPORT = item.PACKAGENO ?? 0;//出口号（包装机号）
                                tb.PACKAGENUM = item.PACKAGENO;// 包装机号    
                                tb.ORDERQUANTITY = item.ORDERQTY;//订单总数
                                tb.ADDRESS = firstTask.CONTACTADDRESS;//订单地址
                                tb.CUSTOMERNAME = firstTask.CUSTOMERNAME;//客户名称
                                tb.CUSTOMERNO = firstTask.CUSTOMERCODE;//客户编码                          
                                tb.ORDERURL = firstTask.URL;//客户URL   
                                tb.ORDERAMOUNT = firstTask.TOTALAMOUNT;//订单总金额；
                                tb.PAYFLAG = firstTask.CUSTTYPE;//结算状态  
                                tb.SEQ = item.ALLPACKAGESEQ;//整齐包序
                                tb.UNIONTASKPACKAGENUM = UNIONTASKPACKAGENUM;//合包总包数  
                                tb.NORMALPACKAGENUM = NORMALPACKAGENUM;//常规烟总包数
                                tb.UNNORMALPACKAGENUM = UNNORMALPACKAGENUM;//异型烟总包数  
                                tb.SORTNUM = item.SORTNUM ?? 0;//流水号
                                tb.CIGNUM = maxCigNum++;// 每台包装机从1 增长 
                                tb.SYNSEQ = item.SYNSEQ;//批次号   
                                en.T_PACKAGE_CALLBACK.AddObject(tb);

                            }

                            en.SaveChanges();
                            billcode = item.BILLCODE;

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    //en.Configuration.AutoDetectChangesEnabled = true;
                    //en.Configuration.ValidateOnSaveEnabled = true;
                }
            }

        }
        int BackHash2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="en"></param>
        /// <param name="billcode"></param>
        /// <param name="flag">0 合包，1 常规烟， 2 异型烟</param>
        /// <returns></returns>
        decimal GetBillPackNum(Entities en, string billcode, int flag)
        {
            var bill = (from item in en.T_PACKAGE_TASK where item.BILLCODE == billcode orderby item.PACKAGESEQ, item.CIGSEQ select item).ToList();
            var grouppack = bill.Select(a => new { packtasknum = a.PACKTASKNUM }).Distinct().ToList();/// .GroupBy(a => new { packtasknum = a.PACKTASKNUM }).ToList();//一共多少包
            int index = 0;
            switch (flag)
            {
                case 0://合包
                    index = 0;
                    foreach (var item in grouppack)
                    {
                        if (bill.Where(a => a.PACKTASKNUM == item.packtasknum && a.CIGTYPE == "1").Count() > 0 && bill.Where(a => a.PACKTASKNUM == item.packtasknum && a.CIGTYPE == "2").Count() > 0)
                        {
                            index++;
                        }
                    }
                    return index;
                case 1: //常规烟
                    index = 0;
                    foreach (var item in grouppack)
                    {
                        if (bill.Where(a => a.PACKTASKNUM == item.packtasknum && a.CIGTYPE == "2").Count() == 0)
                        {
                            index++;
                        }
                    }
                    return index;

                case 2://异型烟
                    index = 0;
                    foreach (var item in grouppack)
                    {
                        if (bill.Where(a => a.PACKTASKNUM == item.packtasknum && a.CIGTYPE == "1").Count() == 0)
                        {
                            index++;
                        }
                    }
                    return index;
                default:
                    return 0;

            }

        }

        int GetMainBelt(int packageNO)
        {
            if (packageNO == 1 || packageNO == 2)
            {
                return 1;
            }
            else if (packageNO == 3 || packageNO == 4)
            {

                return 2;
            }
            else if (packageNO == 5 || packageNO == 6)
            {

                return 3;
            }
            else if( packageNO == 7 || packageNO ==8){

                return 4;

            }
            return 0;

        }
        public List<int> foreachdata()
        {
            List<int> index = new List<int>();
            using (Entities et=new Entities())
            {
               foreach (var item in et.T_PACKAGE_TASK.GroupBy(x=>x.PACKAGENO).Select(x=>x.Key).ToList())
               {
                   index.Add(int.Parse(item.Value.ToString()));
               }
            }
            return index;
        }


    }
}
