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
        WriteLog writeLog = WriteLog.GetLog();
        public string GetMainBelt(int mainBelt)
        {
            WriteLog writeLog = WriteLog.GetLog();
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
            if (ListmbInfo.Count>0)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<MainBeltInfo>));
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.WriteObject(ms, ListmbInfo);
                    string s = Encoding.UTF8.GetString(ms.ToArray());
                    s = s.Replace("\\", "");
                    writeLog.Write("\r查询合流：" + mainBelt + "号主皮带\r");
                    return s;
                }
            }
            else
            {
                writeLog.Write("GetMainBelt(" + mainBelt + ")查询结果集为空");
                return "当前主皮带无数据,请重新查询!";
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
                return "与电控连接失败,请检查网络";
            }
            decimal[] sortnumAndXYnum = new decimal[4];
            sortnumAndXYnum[0] = OpcServer.listUnionTaskGroup[5].ReadD(((MachineNo * 2) - 2)).CastTo<int>(-1);//当前任务号
            sortnumAndXYnum[1] = OpcServer.listUnionTaskGroup[4].ReadD(((MachineNo * 2) - 1)).CastTo<int>(-1);//累计总数数量 
            sortnumAndXYnum[2] = OpcServer.listUnionTaskGroup[5].ReadD(((MachineNo * 2) - 1)).CastTo<int>(-1);//当前吸烟数量  
            sortnumAndXYnum[3] = OpcServer.listUnionTaskGroup[6].ReadD(  MachineNo   + 31 ).CastTo<int>(-1);//当前机械手累计放烟数量
            writeLog.Write("\r查询合流：" + MachineNo + "号机械手\r当前任务号：" + sortnumAndXYnum[0] + "\r累计抓烟数量:" + sortnumAndXYnum[1] + "\r当前吸烟数量：" + sortnumAndXYnum[2] + "\r当前累计放烟数量：" + sortnumAndXYnum[3]);
            if (sortnumAndXYnum.Sum() >= 0)
            {
                if (sortnumAndXYnum[3] == sortnumAndXYnum[1])
                {
                    return "当前机械手无抓烟数据";


                }

                else
                {
                    //List<FollowTaskDeail> list = FolloTaskService.GetUnionMachineInfo(123903, 3, 5, 10);
                    List<FollowTaskDeail> list = FolloTaskService.GetUnionMachineInfo(sortnumAndXYnum[0], GetMainBeltNo(MachineNo), GetGroupNo(MachineNo), sortnumAndXYnum[1], sortnumAndXYnum[2]) ;

                    if (list != null && list.Count > 0)
                    {
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<FollowTaskDeail>));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            ser.WriteObject(ms, list.Take(10));
                            string s = Encoding.UTF8.GetString(ms.ToArray());
                            s = s.Replace("\\", "");
                            writeLog.Write("\r查询合流：" + MachineNo + "号机械手\r");
                            return s;
                        }
                    }
                    else
                    {
                        writeLog.Write("GetUnionMachine(" + MachineNo + ")查询结果集为空");
                        return "当前机械手无抓烟数据,请重新查询!";
                    }
                }
            }
            else
            {
                writeLog.Write("GetUnionMachine(" + MachineNo + ")读取DB块数据异常");
                return "从电控获取数据失败，请重新查询!";
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
            sortnumAndXYnum[0] = OpcServer.listUnionTaskGroup[4].ReadD(((MachineNo * 2) - 2)).CastTo<int>(-1);//当前任务号
            sortnumAndXYnum[1] = OpcServer.listUnionTaskGroup[4].ReadD(((MachineNo * 2) - 1)).CastTo<int>(-1);//当前吸烟数量 
            writeLog.Write("\r查询合流缓存，任务号：" + sortnumAndXYnum[0] + "\r当前抓烟数量：" + sortnumAndXYnum[1]);
            if (sortnumAndXYnum.Sum()>=0)
            {
                List<FollowTaskDeail> date = FolloTaskService.getUnionCache(GetGroupNo(MachineNo), GetMainBeltNo(MachineNo), sortnumAndXYnum[0], sortnumAndXYnum[1]);
                if (date != null && date.Count > 0)
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<FollowTaskDeail>));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ser.WriteObject(ms, date);
                        string s = Encoding.UTF8.GetString(ms.ToArray());
                        s = s.Replace("\\", "");
                        writeLog.Write("\r查询合流：" + MachineNo + "号S缓存\r");
                        return s;
                    }

                }
                else
                {
                    writeLog.Write("GetUnionCaChe(" + MachineNo + ")查询结果集为空");
                    return "当前S缓存无数据,请重新查询!";
                }
            }
            else
            {
                writeLog.Write("GetUnionCaChe(" + MachineNo + ")读取DB块数据异常");
                return "从电控获取数据失败，请重新查询!";
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
            string conncetionGroupStr = "S7:[FJConnectionGroup1]";//默认为第一组
            if (GroupNo == 1 || GroupNo == 2)
            {
                conncetionGroupStr = "S7:[FJConnectionGroup1]";
            }
            else if (GroupNo == 3 || GroupNo == 4)
            {
                conncetionGroupStr = "S7:[FJConnectionGroup2]";
            }
            else if (GroupNo == 5 || GroupNo == 6)
            {
                conncetionGroupStr = "S7:[FJConnectionGroup3]";
            }
            else if (GroupNo == 7 || GroupNo == 8)
            {
                conncetionGroupStr = "S7:[FJConnectionGroup4]";
            } 
            try
            {
                OpcServer.Connect(1,conncetionGroupStr);
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
                for (int i = 0; i < 40; i++)//从电控读取数据 填充 listmbinfo
                {
                    decimal Sortnum = OpcServer.listUnionTaskGroup[8].ReadD(ReadIndex).CastTo<int>(0);//任务号
               
                    if (Sortnum > 0)//任务号不为0
                    {
                        MainBeltInfo info = new MainBeltInfo();
                        info.SortNum = Sortnum;//任务号
                        info.Place = (OpcServer.listUnionTaskGroup[8].ReadD((ReadIndex + 1)).CastTo<decimal>(-1) / 1000);//位置(米)
                        info.Quantity = OpcServer.listUnionTaskGroup[8].ReadD((ReadIndex + 2)).CastTo<int>(-1);//数量

                        info.GroupNO = GroupNo;//组号
                        ListmbInfo.Add(info);
                    }
                    ReadIndex = ReadIndex + 4;
                }
            }

        
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
                        writeLog.Write("\r查询分拣：第" + GroupNo + "组\r");
                        return s;
                    }
                }
                else
                {
                    writeLog.Write("GetSortBelt(" + GroupNo + ")查询结果集为空");
                    return "当前分拣组无数据,请重新查询!"; 
                }
            }
            else
            {
                writeLog.Write("GetSortBelt(" + GroupNo + ")读取DB块数据异常");
                return "从电控获取数据失败，请重新查询";
            }




        }
        decimal GetSortingMachineGroup(int machineno,int biggroup)
        {
            decimal groupno = 0;
            if (Convert.ToInt32(machineno) - biggroup < 12)
            {
                groupno = 1;
            }
            else
            {
                groupno = 2;
            }
            return groupno;
        }
        public string GetSortMachine(int machineNo)
        {
            string conncetionGroupStr = "S7:[FJConnectionGroup1]";//默认为第一组
            decimal groupno = 0;
            decimal MachineNo = 0;
            if (machineNo.ToString().Substring(0, 1) == "1")
            {
                conncetionGroupStr = "S7:[FJConnectionGroup1]";
                groupno = GetSortingMachineGroup(machineNo, 1000);
                MachineNo = machineNo - 1000;
            }
            else if (machineNo.ToString().Substring(0, 1) == "2")
            {
                conncetionGroupStr = "S7:[FJConnectionGroup2]";
                groupno = GetSortingMachineGroup(machineNo, 2000);
                MachineNo = machineNo -2000;
            }
            else if (machineNo.ToString().Substring(0, 1) == "3")
            {
                conncetionGroupStr = "S7:[FJConnectionGroup3]";
                groupno = GetSortingMachineGroup(machineNo, 3000);
                MachineNo = machineNo - 3000;
            }
            else if (machineNo.ToString().Substring(0, 1) == "4")
            {
                conncetionGroupStr = "S7:[FJConnectionGroup4]";
                groupno = GetSortingMachineGroup(machineNo, 4000);
                MachineNo = machineNo - 4000;
            }
            else
            {
                return "请勿输入错的设备号！";
            }
            try
            {
                OpcServer.Connect(2, conncetionGroupStr);
            }
            catch (Exception ex)
            {
                return "远程连接失败,请检查网络";
                
            }
            decimal sortnum = 0;
            decimal xynum = 0;
            if (groupno == 1)
            {
              sortnum=  OpcServer.listUnionTaskGroup[9].ReadD((((int)MachineNo * 2) - 2)).CastTo<decimal>(-1); //A 任务号
              xynum = OpcServer.listUnionTaskGroup[9].ReadD((((int)MachineNo * 2) - 1)).CastTo<decimal>(-1); //A 数量
            }
            else
            {
                sortnum = OpcServer.listUnionTaskGroup[10].ReadD((((int)MachineNo * 2) - 2)).CastTo<decimal>(-1); //B 任务号
                xynum = OpcServer.listUnionTaskGroup[10].ReadD((((int)MachineNo * 2) - 1)).CastTo<decimal>(-1); //B 数量
            }
            var machineinfo = FolloTaskService.GetSrotingMachineInfo(MachineNo).FirstOrDefault();
           
             
            if (machineinfo != null)
            {
                List<string> list = new List<string>();
                string str = "数量:" + xynum + "，卷烟名称:" + machineinfo.CIGARETTDENAME;
                list.Add(str);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<MainBeltInfo>));
                using (MemoryStream ms = new MemoryStream())
                {
                    ser.WriteObject(ms, list);
                    string s = Encoding.UTF8.GetString(ms.ToArray());
                    s = s.Replace("\\", "");
                    writeLog.Write("\r查询预分拣：第" + MachineNo + "号机械手\r");
                    return s;
                } 
            }
            else
            { 
                return "未读取到数据！";
            }

        }

        

    }
}
