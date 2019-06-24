using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using InBound.Model;
namespace InBound.Business
{
    public class TBJDataSchdule
    {
        public void CallBackTBJ2(decimal packageno)
        {
            
            using (Entities en = new Entities())
            {
              //  OracleParameter[] para = new OracleParameter[3];
              //  OracleParameter inpara = new OracleParameter();
              //inpara.Direction = ParameterDirection.Input;
              //inpara.ParameterName = "p_packageNo";
              //inpara.Value = packageno;


              //OracleParameter outpara1 = new OracleParameter();
              //outpara1.Direction = ParameterDirection.Input;
              //outpara1.ParameterName = "p_ErrCode";
              //OracleParameter outpara2 = new OracleParameter();
              //outpara2.Direction = ParameterDirection.Input;
              //outpara2.ParameterName = "p_ErrMsg";
              //para[0] = inpara;
              //para[1] = outpara1;
              //para[2] = outpara2;
              //en.ExecuteStoreCommand("P_PACKAGE_CALLBACK", para);
             //获取当前包装机的数据

                //en.Configuration.AutoDetectChangesEnabled = false;
                //en.Configuration.ValidateOnSaveEnabled = false;
                //获取当前包装机最大条烟流水号
                var cALLBACKs = (from item in en.T_PACKAGE_CALLBACK where item.EXPORT == packageno select item).ToList();
                decimal maxCigNum = 1;
                decimal maxSortnum = 0;
                if (cALLBACKs.Any())
                {
                    maxSortnum = cALLBACKs.Max(a => a.SORTNUM);//获取最大的任务号

                }
              //System.Data.EntityClient.EntityConnection entityConnection = (System.Data.EntityClient.EntityConnection)en.Connection;
              //entityConnection.Open();
              //System.Data.Common.DbConnection storeConnection = entityConnection.StoreConnection;
              //System.Data.Common.DbCommand cmd = storeConnection.CreateCommand();
              //cmd.CommandType = System.Data.CommandType.StoredProcedure;
              //cmd.CommandText = 
           String sql=@"select t.billcode,
           t.regioncode,
           PACKTASKNUM,
           normalqty,
           t.PACKAGESEQ,
           CIGARETTECODE,
           CIGARETTENAME,
           CIGTYPE,
           PACKAGEQTY,
           ORDERDATE,
           MIANBELT,
           ORDERQTY,
           ALLPACKAGESEQ,
           SORTNUM,
           synseq,var_orderPagNum,var_shaednum,var_ordercount,var_NormalTPagNum,
      var_UnnormalTPagNum,pCount,var_UnionTPagNum
      from t_package_task t left join
      (  select  a.regioncode,a.PACKAGENO,a.billcode,var_orderPagNum,decode(var_shaednum,null,0,var_shaednum)var_shaednum,var_ordercount,
      decode(var_NormalTPagNum,null,0,var_NormalTPagNum)var_NormalTPagNum,
      decode(var_UnnormalTPagNum,null,0,var_UnnormalTPagNum)var_UnnormalTPagNum,pCount,decode(var_UnionTPagNum,null,0,var_UnionTPagNum)var_UnionTPagNum from
     (
          select regioncode,PACKAGENO,billcode, max(PACKAGESEQ)var_orderPagNum from t_package_task group by regioncode,PACKAGENO,billcode
        ) a left join (select regioncode,PACKAGENO,billcode, sum(normalqty)var_shaednum from t_package_task
          
          where CIGTYPE = '2' group by regioncode,PACKAGENO,billcode)b
          
         on a.regioncode=b.regioncode and a.PACKAGENO=b.PACKAGENO and a.billcode=b.billcode
        left join ( select regioncode,count(distinct billcode)var_ordercount from t_package_task
          group by regioncode)c 
          on c.regioncode=a.regioncode  left join (select regioncode,PACKAGENO,billcode, count(distinct PACKTASKNUM ) var_NormalTPagNum from t_package_task
          
          where CIGTYPE = '1' group by regioncode,PACKAGENO,billcode)e
          on a.regioncode=e.regioncode and a.PACKAGENO=e.PACKAGENO and a.billcode=e.billcode
          left join (select regioncode,PACKAGENO,billcode, count(distinct PACKTASKNUM ) var_UnnormalTPagNum from t_package_task
          
          where CIGTYPE = '2' group by regioncode,PACKAGENO,billcode)f
          on a.regioncode=f.regioncode and a.PACKAGENO=f.PACKAGENO and a.billcode=f.billcode
           left join (select regioncode,PACKAGENO,billcode, count(distinct PACKTASKNUM ) pCount from t_package_task
          
           group by regioncode,PACKAGENO,billcode)g
          on a.regioncode=g.regioncode and a.PACKAGENO=g.PACKAGENO and a.billcode=g.billcode
          left join (select regioncode,PACKAGENO,billcode,count(1) var_UnionTPagNum from(   
select * from
(
select distinct regioncode,PACKAGENO,billcode,  PACKTASKNUM from t_package_task
          
          where CIGTYPE = '1' group by regioncode,PACKAGENO,billcode,PACKTASKNUM)
          intersect
          (
          
          select distinct regioncode,PACKAGENO,billcode,  PACKTASKNUM from t_package_task
          
          where CIGTYPE = '2' group by regioncode,PACKAGENO,billcode,PACKTASKNUM
)) group by regioncode,PACKAGENO,billcode) h
   on a.regioncode=h.regioncode and a.PACKAGENO=h.PACKAGENO and a.billcode=h.billcode
          ) d
          on t.regioncode=d.regioncode and t.packageno=d.PACKAGENO and t.billcode=d.billcode
       where t.packageNo = "+packageno+@"
     and sortnum > "+maxSortnum+@"
     order by sortnum, packtasknum, cigtype, cigseq, SYNSEQ";
         List<TBJModel> list=  en.ExecuteStoreQuery<TBJModel>(sql, null).ToList();
        var needInfo = (from item in en.V_PRODUCE_PACKAGEINFO     orderby item.TASKNUM select item).ToList();
        int mCopunt = 0, cigseq = 0;
                decimal temppackagenum = 0;
        V_PRODUCE_PACKAGEINFO firstTask = null;
        String tempbillcode = "";

//        var sql_text = @"  insert into T_PACKAGE_CALLBACK(BILLCODE, ROUTEPACKAGENUM, ORDERPACKAGENUM, PACKAGESEQ, CIGARETTEQTY, SHAPEDNUM, CIGARETTECODE,
//                CIGARETTENAME, CIGARETTETYPE, ROUTECODE, PACKAGEQTY, ORDERDATE, LINECODE, ORDERCOUNT, ORDERSEQ, CIGSEQ, EXPORT, PACKAGENUM,
//                ORDERQUANTITY, ADDRESS, CUSTOMERNAME, CUSTOMERNO, ORDERURL, ORDERAMOUNT, PAYFLAG, SEQ, NORMALPACKAGENUM, UNNORMALPACKAGENUM,
//                UNIONTASKPACKAGENUM, SORTNUM, CIGNUM, SYNSEQ)";
        StringBuilder sql_text = new StringBuilder(@"insert into T_PACKAGE_CALLBACK(BILLCODE, ROUTEPACKAGENUM, ORDERPACKAGENUM, PACKAGESEQ, CIGARETTEQTY, SHAPEDNUM, CIGARETTECODE,
                CIGARETTENAME, CIGARETTETYPE, ROUTECODE, PACKAGEQTY, ORDERDATE, LINECODE, ORDERCOUNT, ORDERSEQ, CIGSEQ, EXPORT, PACKAGENUM,
                ORDERQUANTITY, ADDRESS, CUSTOMERNAME, CUSTOMERNO, ORDERURL, ORDERAMOUNT, PAYFLAG, SEQ, NORMALPACKAGENUM, UNNORMALPACKAGENUM,
                UNIONTASKPACKAGENUM, SORTNUM, CIGNUM, SYNSEQ)");
        System.Data.EntityClient.EntityConnection entityConnection = null;
        System.Data.Common.DbConnection storeConnection = null;
        System.Data.Common.DbCommand cmd = null;
         foreach (var item in list)
         {
             //if (tempbillcode == "")
             //{
             //    tempbillcode = item.billcode;
             //}
             //else if (tempbillcode != item.billcode)
             //{
             //    needInfo = needInfo.Where(x => x.BILLCODE != tempbillcode).ToList();
             //    tempbillcode = item.billcode;
             //}
              firstTask = needInfo.Where(a => a.BILLCODE == item.billcode).FirstOrDefault();
            
             for (int i = 1; i <= item.normalqty; i++)//
             {
                 if (temppackagenum == 0 || temppackagenum != item.PACKTASKNUM)
                 { temppackagenum = item.PACKTASKNUM; cigseq = 0; }
                 
                 cigseq++;
                 mCopunt++;


                 //T_PACKAGE_CALLBACK tb = new T_PACKAGE_CALLBACK();
                 //tb.BILLCODE = item.billcode;//订单
                 //tb.ROUTEPACKAGENUM = item.pCount;//车组总包数
                 //tb.ORDERPACKAGENUM = item.var_orderPagNum;//订单总包数
                 //tb.PACKAGESEQ = item.PACKAGESEQ;//订单内包序
                 //tb.CIGARETTEQTY = 1;//品牌条烟数
                 //tb.SHAPEDNUM = item.var_shaednum;//订单异型烟数量
                 //tb.CIGARETTECODE = item.CIGARETTECODE;//卷烟编码
                 //tb.CIGARETTENAME = item.CIGARETTENAME;//卷烟名称
                 //tb.CIGARETTETYPE = item.CIGTYPE;//卷烟类型
                 //tb.ROUTECODE = item.regioncode;//车组编号
                 //tb.PACKAGEQTY = item.PACKAGEQTY;//包内条烟数量
                 //tb.ORDERDATE = item.ORDERDATE;//订单日期
                 //tb.LINECODE = item.MIANBELT.ToString();//线路编号
                 //tb.ORDERCOUNT = item.var_ordercount;  //车组内订单数
                 //tb.ORDERSEQ = firstTask.SORTSEQ;//订单户序 firstTask.SORTSEQ 
                 ////tb.CIGSEQ = cigseq++;//条烟顺序
                 //tb.EXPORT = packageno;//出口号（包装机号）
                 //tb.PACKAGENUM = packageno;// 包装机号    
                 //tb.ORDERQUANTITY = item.ORDERQTY;//订单总数
                 //tb.ADDRESS = firstTask.CONTACTADDRESS;//订单地址
                 //tb.CUSTOMERNAME = firstTask.CUSTOMERNAME;//客户名称
                 //tb.CUSTOMERNO = firstTask.CUSTOMERCODE;//客户编码                          
                 //tb.ORDERURL = firstTask.URL;//客户URL   
                 //tb.ORDERAMOUNT = firstTask.TOTALAMOUNT;//订单总金额；
                 //tb.PAYFLAG = firstTask.CUSTTYPE;//结算状态  
                 //tb.SEQ = item.ALLPACKAGESEQ;//整齐包序
                 //tb.UNIONTASKPACKAGENUM = item.var_UnionTPagNum;//合包总包数  
                 //tb.NORMALPACKAGENUM = item.var_NormalTPagNum;//常规烟总包数
                 //tb.UNNORMALPACKAGENUM = item.var_UnnormalTPagNum;//异型烟总包数  
                 //tb.SORTNUM = item.SORTNUM;//流水号
                 //tb.CIGNUM = maxCigNum++;// 每台包装机从1 增长 
                 //tb.SYNSEQ = item.synseq;//批次号   
                 //en.T_PACKAGE_CALLBACK.AddObject(tb);

                // sql_text=sql_text +" select '" + item.billcode +"',"+ item.pCount+","+ item.var_orderPagNum+"," +item.PACKAGESEQ+ ","+
                // 1+"," + item.var_shaednum+",'" + item.CIGARETTECODE+"','"+ item.CIGARETTENAME+"','"+ item.CIGTYPE+"','"+ item.regioncode+"',"+
                //  item.PACKAGEQTY + ",to_date('" + item.ORDERDATE.ToString("yyyy-MM-dd") + "','yyyy-mm-dd'),'" + item.MIANBELT.ToString() + "'," + item.var_ordercount + "," + firstTask.SORTSEQ + "," + 1 + "," + packageno + ","
                // + packageno+","+ item.ORDERQTY+",'"+
                //firstTask.CONTACTADDRESS+"','" +firstTask.CUSTOMERNAME+"','"+ firstTask.CUSTOMERCODE+"','" +firstTask.URL+"',"+ firstTask.TOTALAMOUNT+",'"+  firstTask.CUSTTYPE+"'," +item.ALLPACKAGESEQ+"," +item.var_NormalTPagNum+"," 
                //+item.var_UnnormalTPagNum+","+ item.var_UnionTPagNum+","+
                // item.SORTNUM+","+ (maxCigNum++)+","+ item.synseq +" from dual union";
                 sql_text = sql_text.Append(" select '").Append(item.billcode).Append("',").Append(item.pCount).Append(",").Append(item.var_orderPagNum).Append(",").Append(item.PACKAGESEQ).Append(",").Append(
               1).Append(",").Append(item.var_shaednum).Append(",'").Append(item.CIGARETTECODE).Append("','").Append(item.CIGARETTENAME).Append("','").Append(item.CIGTYPE).Append("','").Append(item.regioncode).Append("',").Append(
                item.PACKAGEQTY).Append(",to_date('").Append(item.ORDERDATE.ToString("yyyy-MM-dd")).Append("','yyyy-mm-dd'),'").Append(item.MIANBELT.ToString()).Append("',").Append(item.var_ordercount).Append(",").Append(firstTask.SORTSEQ).Append(",").Append(1).Append(",").Append(packageno).Append(","
               ).Append(packageno).Append(",").Append(item.ORDERQTY).Append(",'").Append(
              firstTask.CONTACTADDRESS).Append("','").Append(firstTask.CUSTOMERNAME).Append("','").Append(firstTask.CUSTOMERCODE).Append("','").Append(firstTask.URL).Append("',").Append(firstTask.TOTALAMOUNT).Append(",'").Append(firstTask.CUSTTYPE).Append("',").Append(item.ALLPACKAGESEQ).Append(",").Append(item.var_NormalTPagNum).Append(","
              ).Append(item.var_UnnormalTPagNum).Append(",").Append(item.var_UnionTPagNum).Append(",").Append(
               item.SORTNUM).Append(",").Append(maxCigNum++).Append(",").Append(item.synseq).Append(" from dual union");
                 if (mCopunt >= 200)
                 {

                     entityConnection = (System.Data.EntityClient.EntityConnection)en.Connection;
                     entityConnection.Open();
                     storeConnection = entityConnection.StoreConnection;
                     cmd = storeConnection.CreateCommand();
                     cmd.CommandType = System.Data.CommandType.Text;
                     cmd.CommandText = sql_text.ToString().Substring(0, sql_text.Length - 5);
                     cmd.ExecuteNonQuery();
                     cmd.Dispose();
                     cmd = null;

                     entityConnection.Close();
                     entityConnection = null;
                     storeConnection = null;
                     sql_text.Clear();
                     GC.Collect();
                     mCopunt = 0;
                     sql_text.Append(@"  insert into T_PACKAGE_CALLBACK(BILLCODE, ROUTEPACKAGENUM, ORDERPACKAGENUM, PACKAGESEQ, CIGARETTEQTY, SHAPEDNUM, CIGARETTECODE,
                CIGARETTENAME, CIGARETTETYPE, ROUTECODE, PACKAGEQTY, ORDERDATE, LINECODE, ORDERCOUNT, ORDERSEQ, CIGSEQ, EXPORT, PACKAGENUM,
                ORDERQUANTITY, ADDRESS, CUSTOMERNAME, CUSTOMERNO, ORDERURL, ORDERAMOUNT, PAYFLAG, SEQ, NORMALPACKAGENUM, UNNORMALPACKAGENUM,
                UNIONTASKPACKAGENUM, SORTNUM, CIGNUM, SYNSEQ)");
                     //en.SaveChanges();
                 }
                 
             }
             firstTask = null;
         }

         if (sql_text.ToString().Contains("union"))
         {
             entityConnection = (System.Data.EntityClient.EntityConnection)en.Connection;
             entityConnection.Open();
             storeConnection = entityConnection.StoreConnection;
             cmd = storeConnection.CreateCommand();
             cmd.CommandType = System.Data.CommandType.Text;
             cmd.CommandText = sql_text.ToString().Substring(0, sql_text.Length - 5);
             cmd.ExecuteNonQuery();
             cmd.Dispose();
             cmd = null;
             entityConnection.Close();
             entityConnection = null;
             storeConnection = null;
         }
         sql_text.Clear();
         //en.SaveChanges();
             //  DbParameter inpara= cmd.CreateParameter();
             //  inpara.Direction = ParameterDirection.Input;
             //  inpara.ParameterName = "p_packageNo";
             //  inpara.Value = packageno;
             ////  inpara.DbType = DbType.VarNumeric;
             //  DbParameter outpara1 = cmd.CreateParameter();
             //  outpara1.Direction = ParameterDirection.Input;
             //  outpara1.ParameterName = "p_ErrCode";
             //  outpara1.DbType = DbType.String;
             //  DbParameter outpara2 = cmd.CreateParameter();
             //  outpara2.Direction = ParameterDirection.Output;
             //  outpara2.ParameterName = "p_ErrMsg";
             //  outpara2.DbType = DbType.String;
             //  cmd.Parameters.Add(inpara);
             //  cmd.Parameters.Add(outpara1);
             //  cmd.Parameters.Add(outpara2);
             //  cmd.ExecuteNonQuery();
             //  cmd.Dispose();
             }
            
        }

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
                var cALLBACKs = (from item in en.T_PACKAGE_CALLBACK where item.EXPORT == packageno select item).ToList();
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
                    maxCigNum += 1;
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

        public string CheckData()
        {
            string str = "";
            using (Entities et = new Entities())
            {
                //总数量 V_PRODUCE_PACKAGEINFO
                decimal allqty = 0;
                var AllRegicode = et.T_PACKAGE_TASK.GroupBy(x=> new {x.REGIONCODE,x.PACKAGENO}).Select(x=>x.Key).ToList();
                foreach (var item in AllRegicode)
                {
                    allqty += (et.V_PRODUCE_PACKAGEINFO.Where(x => x.REGIONCODE == item.REGIONCODE && x.EXPORT == item.PACKAGENO).Sum(x => x.QUANTITY) ?? 0);
                }     
                decimal PackageQty = et.T_PACKAGE_TASK.Sum(x => x.NORMALQTY) ?? 0;
                decimal CallBackQty = et.T_PACKAGE_CALLBACK.Sum(x => x.CIGARETTEQTY) ?? 0;
                str += "分拣视图总条烟数：" + allqty + ";\r\n包装机数据总条烟数：" + PackageQty + ";\r\n贴标机数据总条烟数：" + CallBackQty + ";\r\n";
                if (allqty != PackageQty)
                {
                    str += "分拣视图与包装机数据相差：" + (allqty - PackageQty) + "\r\n";
                }
                if (PackageQty != CallBackQty)
                {
                    str += "包装机与贴标机数据相差：" + (PackageQty - CallBackQty) + "\r\n";
                }
            }
            return str;
        }
    }
}
