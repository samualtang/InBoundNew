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
                string s = Encoding.UTF8.GetString(ms.ToArray());
                s = s.Replace("\\", "");
                return s;
            }


        }

        /*******************************************************************/


        /// <summary>
        /// 合流机械手
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <returns></returns>
        public string GetUnionMachine(int MachineNo)
        {
            try
            {
                OpcServer.Connect();
            }
            catch (Exception ex)
            {
                return "远程连接失败,请检查网络";
            }
            decimal[] sortnumAndXYnum = new decimal[2];
            sortnumAndXYnum[0] = OpcServer.listUnionTaskGroup[5].ReadD(((MachineNo * 2) - 2)).CastTo<int>(-1);//当前任务号
            sortnumAndXYnum[1] = OpcServer.listUnionTaskGroup[5].ReadD(((MachineNo * 2) - 1)).CastTo<int>(-1);//当前吸烟数量 
            if (sortnumAndXYnum.Sum() > 0)
            {
                //List<FollowTaskDeail> list = FolloTaskService.GetUnionMachineInfo(109891, 1, 1, 10);
                List<FollowTaskDeail> list = FolloTaskService.GetUnionMachineInfo(sortnumAndXYnum[0], GetMainBeltNo(MachineNo), GetGroupNo(MachineNo), sortnumAndXYnum[1]);
                if (list != null && list.Count > 0)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<FollowTaskDeail>));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ser.WriteObject(ms, list);
                        string s = Encoding.UTF8.GetString(ms.ToArray());
                        s = s.Replace("\\", "");
                        return s;
                    }
                }
                else
                {
                    return "未读取到数据!";
                }
            }
            else
            {
                return "DB块读取异常";
            }
        }


        /// <summary>
        /// 合流S缓存
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <returns></returns>
        public string GetUnionCaChe(int MachineNo)
        {
            try
            {
                OpcServer.Connect();
            }
            catch (Exception ex)
            {
                return "远程连接失败,请检查网络";
            }
            decimal[] sortnumAndXYnum = new decimal[2];
            sortnumAndXYnum[0] = OpcServer.listUnionTaskGroup[5].ReadD(((MachineNo * 2) - 2)).CastTo<int>(-1);//当前任务号
            sortnumAndXYnum[1] = OpcServer.listUnionTaskGroup[5].ReadD(((MachineNo * 2) - 1)).CastTo<int>(-1);//当前吸烟数量 
            List<FollowTaskDeail> date = FolloTaskService.getUnionCache(GetGroupNo(MachineNo), GetMainBeltNo(MachineNo), sortnumAndXYnum[0], sortnumAndXYnum[1]);
            if (date != null && date.Count > 0)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<FollowTaskDeail>));
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.WriteObject(ms, date);
                    string s = Encoding.UTF8.GetString(ms.ToArray());
                    s = s.Replace("\\", "");
                    return s;
                }

            }
            else
            {
                return "未读取到数据!";
            }
        }
        /// <summary>
        /// 获取组号
        /// </summary>
        /// <param name="machineNo">机械手号</param>
        /// <returns></returns>4   
        int GetGroupNo(int machineNo)
        {
            int GroupNo;
            if (machineNo >= 8)
            {
                GroupNo = machineNo % 8;// Convert.ToDecimal(Math.IEEERemainder(machineNo, 8));//取余获得组号
            }
            else
            {
                GroupNo = machineNo;
            }
            if (GroupNo == 0)
            {
                GroupNo = 8;
            }
            return GroupNo;
        }
        /// <summary>
        /// 获取主皮带号
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <returns>主皮带</returns>
        int GetMainBeltNo(decimal MachineNo)
        {
            int mainbelt = 0;
            if (MachineNo <= 8)
            {
                mainbelt = 1;
            }
            else if (MachineNo >= 9 && MachineNo <= 16)
            {
                mainbelt = 2;
            }
            else if (MachineNo >= 17 && MachineNo <= 24)
            {
                mainbelt = 3;
            }
            else if (MachineNo >= 25 && MachineNo <= 32)
            {
                mainbelt = 4;
            }
            return mainbelt;
        }
          

        
        /// <summary>
        /// 预分拣皮带
        /// </summary>
        /// <param name="GroupNo"></param>
        /// <returns></returns>
        public string GetSortBelt(int GroupNo)
        {
            if (GroupNo == 1 || GroupNo == 2 )
            {
                OpcServer.FJConnectionGroup = "S7:[FJConnectionGroup1]";
            }
            else if (GroupNo == 3 || GroupNo == 4 )
            {
                OpcServer.FJConnectionGroup = "S7:[FJConnectionGroup2]";
            }
            else if (GroupNo == 5 || GroupNo == 6)
            {
                OpcServer.FJConnectionGroup = "S7:[FJConnectionGroup3]";
            }
            else if (GroupNo == 7 || GroupNo == 8)
            {
                OpcServer.FJConnectionGroup = "S7:[FJConnectionGroup4]";
            }

            try
            {
                OpcServer.Connect();
            }
            catch (Exception ex)
            {
                return "远程连接失败,请检查网络";
            }
            List<MainBeltInfo> ListmbInfo = new List<MainBeltInfo>();
            if (GroupNo == 1 || GroupNo == 3 || GroupNo == 5 || GroupNo == 7)
            {
                int ReadIndex = 0;
                for (int i = 0; i < 40; i++)//从电控读取数据 填充 listmbinfo
                {
                    decimal Sortnum = OpcServer.listUnionTaskGroup[7].ReadD(ReadIndex).CastTo<int>(0);//任务号

                    if (Sortnum > 0)//任务号不为0
                    {
                        MainBeltInfo info = new MainBeltInfo();
                        info.SortNum = Sortnum;//任务号
                        info.Place = (OpcServer.listUnionTaskGroup[7].ReadD((ReadIndex + 1)).CastTo<decimal>(-1) / 1000);//位置(米)
                        info.Quantity = OpcServer.listUnionTaskGroup[7].ReadD((ReadIndex + 2)).CastTo<int>(-1);//数量
                        info.GroupNO = GroupNo;//组号
                        ListmbInfo.Add(info);
                    }
                    ReadIndex = ReadIndex + 4;
                }
            }
            else
            {
                int ReadIndex = 0;
                for (int i = 0; i < 1; i++)//从电控读取数据 填充 listmbinfo
                {
                    //decimal Sortnum = OpcServer.listUnionTaskGroup[8].ReadD(ReadIndex).CastTo<int>(0);//任务号
                    decimal Sortnum = 109934;
                    if (Sortnum > 0)//任务号不为0
                    {
                        MainBeltInfo info = new MainBeltInfo();
                        info.SortNum = Sortnum;//任务号
                        //info.Place = (OpcServer.listUnionTaskGroup[8].ReadD((ReadIndex + 1)).CastTo<decimal>(-1) / 1000);//位置(米)
                        //info.Quantity = OpcServer.listUnionTaskGroup[8].ReadD((ReadIndex + 2)).CastTo<int>(-1);//数量

                        info.Place = 21;
                        info.Quantity = 5;

                        info.GroupNO = GroupNo;//组号
                        ListmbInfo.Add(info);
                    }
                    ReadIndex = ReadIndex + 4;
                }
            }

            //decimal Sortnum = 114769;
            //MainBeltInfo info = new MainBeltInfo();
            //info.SortNum = Sortnum;//任务号
            //info.Place = 21;
            //info.Quantity = 6;
            //info.GroupNO = GroupNo;//组号
            //ListmbInfo.Add(info);

            //for (int i = 0; i < 39; i++)
            //{
            //      MainBeltInfo ii = new MainBeltInfo();
            //      ii.SortNum = 0;//任务号
            //      ii.Place = 0;
            //      ii.Quantity = 0;
            //      ii.GroupNO = GroupNo;//组号
            //      ListmbInfo.Add(ii);
            //}
        
            MainBeltInfoService.GetSortMainBeltInfo(ListmbInfo); //填充完成之后传进方法 计算 ，
            ListmbInfo = ListmbInfo.OrderBy(a => a.Place).ToList();//对距离任务号进行排序
             
            if (ListmbInfo.Count > 0)
            {
                if (ListmbInfo != null && ListmbInfo.Count > 0)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<MainBeltInfo>));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ser.WriteObject(ms, ListmbInfo);
                        string s = Encoding.UTF8.GetString(ms.ToArray());
                        s = s.Replace("\\", "");
                        return s;
                    }
                }
                else
                {
                    return "未读取到数据!";
                }
            }
            else
            {
                return "DB块读取异常";
            }




        }



        

    }
}
