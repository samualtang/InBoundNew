﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFService;
using InBound.Model;
using WCFInBound;
using System.Runtime.Serialization.Json;
using InBound.Business;
using System.IO;


namespace WCFMainBeltInfo
{
   public class MainBeltService:IMainBeltService
    {
        public string GetMainBeltInfo(int mainBelt)
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
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
