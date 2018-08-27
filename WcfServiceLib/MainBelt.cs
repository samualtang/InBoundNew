using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebService;
using InBound.Model;
using InBound;
using InBound.Business;
using System.Runtime.Serialization.Json;
using System.IO;
using WebService.Modle;
namespace WcfServiceLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class MainBelt : IMainBelt
    {
        public string GetMainBelt(int mainBelt)
        {
            try
            {
                OpcServer.Connect();
            }
            catch (Exception ex)
            {
                return "远程连接失败,请检查网络";
            }
            List<MainBeltInfo> ListmbInfo = new List<MainBeltInfo>();

            int ReadIndex = 0;
            double[] nowplace = new double[40];
            List<decimal> SortNumList = new List<decimal>();
            List<decimal> QuantityList = new List<decimal>();
            for (int i = (mainBelt - 1) * 8; i < mainBelt * 8; i++)
            {
                SortNumList.Add(OpcServer.MachineGroup.ReadD(i).CastTo<decimal>(0));//任务号
                QuantityList.Add(OpcServer.MachineGroup.ReadD(32 + i).CastTo<decimal>(0));//任务号
            }
            for (int i = 0; i < 40; i++)//从电控读取数据 填充 listmbinfo
            {
                decimal Sortnum = OpcServer.listUnionTaskGroup[mainBelt - 1].ReadD(ReadIndex).CastTo<decimal>(0);//任务号


                if (Sortnum > 0)//任务号不为0
                {
                    MainBeltInfo info = new MainBeltInfo();

                    //info.Place = (listMainBelt[mainbelt - 1].ReadD((ReadIndex + 1)).CastTo<int>(-1) / 1000);//位置(米)
                    info.SortNum = Sortnum;//任务号
                    info.Place = Convert.ToDecimal((OpcServer.listUnionTaskGroup[mainBelt - 1].ReadD((ReadIndex + 1)).CastTo<double>(-1) / 1000));//(listMainBelt[mainbelt - 1].ReadD((ReadIndex + 1)).CastTo<int>(-1) / 1000000);//位置(米)
                    info.Quantity = Convert.ToDecimal(OpcServer.listUnionTaskGroup[mainBelt - 1].ReadD((ReadIndex + 2)).CastTo<int>(-1));//数量
                    info.mainbelt = mainBelt.ToString();//主皮带
                    info.SortNumList = SortNumList;
                    info.QuantityList = QuantityList;
                    ListmbInfo.Add(info);

                }
                ReadIndex = ReadIndex + 3;
            }
            MainBeltInfoService.GetMainBeltInfo(ListmbInfo); //填充完成之后传进方法 计算 ，
            ListmbInfo = ListmbInfo.OrderBy(x => x.Place).ToList();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<MainBeltInfo>));
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, ListmbInfo);
                string s= Encoding.UTF8.GetString(ms.ToArray());
                s=s.Replace("\\","");
                return s;
            }
        
          
        }

        /*******************************************************************/



        public string GetUnionMachine(int MachineNo)
        { 
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<MainBeltInfo>));
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, GetUnionMachineInfo(MachineNo));
                string s = Encoding.UTF8.GetString(ms.ToArray());
                s = s.Replace("\\", "");
                return s;
            }
        }



        public string GetUnionCaChe(int MachineNo)
        {
             

            
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<MainBeltInfo>));
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms,MachineNo);
                string s = Encoding.UTF8.GetString(ms.ToArray());
                s = s.Replace("\\", "");
                return s;
            }
        }



        //public string GetSortBelt(int MachineNo)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<MainBeltInfo>));
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        ser.WriteObject(ms, GetSortBeltInfo(MachineNo));
        //        string s = Encoding.UTF8.GetString(ms.ToArray());
        //        s = s.Replace("\\", "");
        //        return s;
        //    }
        //}




/*********************************************************************************************************************/
        #region  获取合流机械手的数据

        List<Group> Listmachine = new List<Group>();
        decimal[] sortnumAndXYnum1 = new decimal[2];
        /// <summary>
        /// 读取DB当前任务号和当前抓数
        /// </summary>
        /// <param name="mainbelt">主皮带</param>
        /// <param name="machineno">机械手号</param>
        /// <returns></returns>
        decimal[] ReadDbInFo(int mainbelt, int machineno)
        { 
            sortnumAndXYnum1[0] = Listmachine[5].ReadD(((2 * machineno) - 2)).CastTo<int>(-1);//0  2   4
            sortnumAndXYnum1[1] = Listmachine[5].ReadD(((2 * machineno) - 1)).CastTo<int>(-1);//1   3  5
            return sortnumAndXYnum1;
        }
        /// <summary>
        /// 获取合流机械手的数据
        /// </summary>
        /// <param name="mainbelt">主皮带</param>
        /// <param name="MachineNo">机械手</param> 
        /// <returns></returns>
        private List<FollowTaskDeail> GetUnionMachineInfo(int MachineNo)
        {
            int mainbelt = 0;
            if (MachineNo <= 8)
            {
                mainbelt = 1;
            }
            else if (MachineNo >= 9 || MachineNo <= 16)
            {
                mainbelt = 2;
            }
            else if (MachineNo >= 17 || MachineNo <= 24)
            {
                mainbelt = 3;
            }
            else if (MachineNo >= 25 || MachineNo <= 32)
            {
                mainbelt = 4;
            }

            int groupno;
            if (MachineNo >= 8)
            {
                groupno = MachineNo % 8;// Convert.ToDecimal(Math.IEEERemainder(machineNo, 8));//取余获得组号
            }
            else
            {
                groupno = MachineNo;
            }
            if (groupno == 0)
            {
                groupno = 8;
            }

            decimal sortnum = ReadDbInFo(mainbelt, MachineNo)[0];//当前任务号
            decimal xynum = ReadDbInFo(mainbelt, MachineNo)[1];   //当前抓烟数 

            using (Entities dataentity = new Entities())
            {
                if (groupno == 4) { groupno = 3; } else if (groupno == 3) { groupno = 4; }
                //由于机械手第7组分出来的烟对应的是合流第八组机械手，这里7和8组对应有个对调
                if (groupno == 8) { groupno = 7; } else if (groupno == 7) { groupno = 8; }
                //由于机械手第7组分出来的烟对应的是合流第八组机械手，这里7和8组对应有个对调
                var query = (from item in dataentity.T_PRODUCE_POKE
                             join item2 in dataentity.T_PRODUCE_SORTTROUGH
                             on item.TROUGHNUM equals item2.TROUGHNUM
                             where item.SORTNUM == sortnum && item.MAINBELT == mainbelt && item.GROUPNO == groupno
                             orderby item.SORTNUM
                             select new FollowTaskDeail()
                             {
                                 SortNum = item.SORTNUM ?? 0,
                                 MERAGENUM = item.MERAGENUM ?? 0,
                                 POKENUM = item.POKENUM ?? 0,
                                 MainBelt = item.MAINBELT ?? 0,
                                 CIGARETTDECODE = item2.CIGARETTECODE,
                                 CIGARETTDENAME = item2.CIGARETTENAME,
                                 GroupNO = item.GROUPNO ?? 0,
                                 Machineseq = item.MACHINESEQ ?? 0
                             }).ToList();
                if (query != null)
                {
                    List<FollowTaskDeail> newlist = new List<FollowTaskDeail>();
                    if (query.Count > 0 && query != null)
                    {
                        foreach (var item in query)
                        {
                            if (item.POKENUM > 1)
                            {
                                for (int i = 0; i < item.POKENUM; i++)
                                {
                                    xynum--;
                                    newlist.Add(item);
                                    if (xynum < 1)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                newlist.Add(item);
                                xynum--;
                            }
                        }
                        foreach (var item in newlist)
                        {
                            item.POKENUM = 1;
                        }
                        return newlist.OrderBy(a => a.SortNum).ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion


        #region  获取缓存数据

        List<Group> listUnionMachine = new List<Group>();
        decimal[] sortnumAndXYnum = new decimal[2];
        /// <summary>
        /// 读取DB当前任务号和当前抓数
        /// </summary>
        /// <param name="mainbelt">主皮带号</param>
        /// <param name="machineno">当前机械手吸烟数量</param>
        /// <returns>一个数组[0]是当前任务号 [1]是当前吸烟数量</returns>
        void ReadDbInFo_cache( int machineno)
        { 
            sortnumAndXYnum[0] = listUnionMachine[5].ReadD(((machineno * 2) - 2)).CastTo<int>(-1);//当前任务号
            sortnumAndXYnum[1] = listUnionMachine[5].ReadD(((machineno * 2) - 1)).CastTo<int>(-1);//当前吸烟数量  
        }
         
        List<FollowTaskDeail> det=new List<FollowTaskDeail>();
        public List<FollowTaskDeail> GetUnionCaCheInfo(int MachineNo)
        {

            int mainbelt = 0;
            if (MachineNo <= 8)
            {
                mainbelt = 1;
            }
            else if (MachineNo >= 9 || MachineNo <= 16)
            {
                mainbelt = 2;
            }
            else if (MachineNo >= 17 || MachineNo <= 24)
            {
                mainbelt = 3;
            }
            else if (MachineNo >= 25 || MachineNo <= 32)
            {
                mainbelt = 4;
            }

            int groupno;
            if (MachineNo >= 8)
            {
                groupno = MachineNo % 8;// Convert.ToDecimal(Math.IEEERemainder(machineNo, 8));//取余获得组号
            }
            else
            {
                groupno = MachineNo;
            }
            if (groupno == 0)
            {
                groupno = 8;
            }
            try
            {
                using (Entities dataentity = new Entities())
                {
                    //由于机械手第三组分出来的烟对应的是合流第四组机械手，这里3和4组对应有个对调
                    if (groupno == 4) { groupno = 3; } else if (groupno == 3) { groupno = 4; }
                    //由于机械手第7组分出来的烟对应的是合流第八组机械手，这里7和8组对应有个对调
                    if (groupno == 8) { groupno = 7; } else if (groupno == 7) { groupno = 8; }
                    var query = (from p in dataentity.T_PRODUCE_POKE
                                 join t in dataentity.T_PRODUCE_SORTTROUGH
                                 on p.TROUGHNUM equals t.TROUGHNUM
                                 where t.GROUPNO == groupno && p.MAINBELT == mainbelt && t.CIGARETTETYPE == 20 && t.TROUGHTYPE == 10 && p.SORTNUM >= sortnumAndXYnum[0] && p.SORTSTATE == 20
                                 orderby p.SORTNUM, p.MACHINESEQ
                                 select new FollowTaskDeail() { CIGARETTDECODE = t.CIGARETTECODE, CIGARETTDENAME = t.CIGARETTENAME, POKENUM = p.POKENUM ?? 0, Machineseq = p.MACHINESEQ ?? 0, POKEID = p.POKEID, MainBelt = p.MAINBELT ?? 0, SortNum = p.SORTNUM ?? 0, GroupNO = t.GROUPNO ?? 0 }).ToList();
                    if (query != null)
                    {
                        //获取当前抓烟任务的订单总烟数
                        decimal pokenumTotail = query.Where(a => a.SortNum == sortnumAndXYnum[0] && a.GroupNO == groupno && a.MainBelt == mainbelt).Select(z => z.POKENUM).Sum();//注意:数量可能为空(null) 原因:订单数据异常

                        return GetUnionCacheByPokenum(query.ToList(), sortnumAndXYnum[0], sortnumAndXYnum[1], pokenumTotail);
                    }
                    else { return null; }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private List<FollowTaskDeail> GetUnionCacheByPokenum(List<FollowTaskDeail> list, decimal machineTaskExcuting, decimal machinePokeNum, decimal pokenumTotail)
        {
            decimal TotailmachinePokeNum = Math.Ceiling(pokenumTotail / 10);//总数抓数 
            if (pokenumTotail < 10 * machinePokeNum || TotailmachinePokeNum == machinePokeNum)//如果当前抓烟任务的订单总烟数 小于十   去掉前抓烟任务 (包括最后一个任务)
            {
                list.RemoveAll(a => a.SortNum == machineTaskExcuting);
            }
            else
            {
                for (int Count = 1; Count <= machinePokeNum; Count++)//根据当前抓数抓数
                {
                    var pokenum = list.Where(c => c.POKEID == list[0].POKEID).Select(a => new { pokeid = a.POKEID, pokenum = a.POKENUM }).FirstOrDefault();//获取当前任务抓烟数  
                    if (pokenum.pokenum == 10)//如果有一抓等于10  直接去掉 
                    {
                        list.RemoveAll(a => a.SortNum == machineTaskExcuting && a.POKEID == list[0].POKEID);//
                    }
                    else if (pokenum.pokenum < 10)//当第一个任务pokenum小于10 就和下一个任务的pokenum相加 直到大于等于10
                    {
                        decimal[] Listmachineseq = new decimal[10];//存放品牌 和 对应品牌的抓取数小于10得品牌   --- //存放pokeid
                        decimal sum = 0;//和 
                        decimal lastNum = 0;//上一次 
                        decimal endNum = 0;//最后一次
                        for (int i = 0; i <= Listmachineseq.Length; i++)
                        {
                            sum += list[i].POKENUM;
                            if (sum == 10)
                            {
                                Listmachineseq[i] = list[i].POKEID;
                                for (int j = i; j >= 0; j--)
                                {
                                    list.RemoveAll(a => a.POKEID == Listmachineseq[j]);
                                }
                                break;
                            }
                            else if (sum > 10)
                            {
                                endNum = list.Find(x => x.POKEID == list[i - 1].POKEID).POKENUM -= (list[i - 1].POKENUM - (10 - lastNum)); //对相加大于10最后一个pokenum的值 所取的数量 相减
                                list.Find(z => z.POKEID == list[i].POKEID).POKENUM -= endNum;
                                for (int j = i; j >= 0; j--)
                                {
                                    list.RemoveAll(a => a.POKEID == Listmachineseq[j]);
                                }
                                //return list;
                                break;
                            }
                            lastNum = sum;
                            Listmachineseq[i] = list[i].POKEID;//存入当前pokeid
                        }
                    }
                }
            }
            return list;
        }


        #endregion


        public string GetSortBelt(int MachineNo)
        {
            throw new NotImplementedException();
        }
    }
}
