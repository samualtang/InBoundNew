using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpcRcw.Da;
using WCFService.Modle;

namespace WCFService
{
    public static class OpcServer
    {

       public static IOPCServer pIOPCServer;  //定义opcServer对象
       internal const string SERVER_NAME = "OPC.SimaticNET";
       public static Group UnionTaskGroup1,MachineGroup, UnionTaskGroup2, UnionTaskGroup3, UnionTaskGroup4, UnionMachineTaskGroup, UnionMachineNowTaskGroup;
       internal const int LOCALE_ID = 0x409;  
          public static  List<Group> listUnionTaskGroup = new List<Group>();
        public static void Connect()
        {
            if(pIOPCServer==null)
            {
               Type svrComponenttyp;
               Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
               svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
              AddUnionTaskGroup();
            }
        }
      
        public static void AddUnionTaskGroup()
        {
            UnionTaskGroup1 = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);//一号主皮带
            UnionTaskGroup2 = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID);//二号主皮带
            UnionTaskGroup3 = new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID);//三号主皮带
            UnionTaskGroup4 = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID);//四号主皮带
            UnionMachineTaskGroup = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID);//合流机械手任务号抓数
            UnionMachineNowTaskGroup = new Group(pIOPCServer, 6, "group6", 1, LOCALE_ID);//合流机械手当前任务号和抓数
            MachineGroup= new Group(pIOPCServer, 7, "group7", 1, LOCALE_ID);//合流机械手当前任务号和抓数
            

            UnionTaskGroup1.addItem(ItemCollection.GetTaskGroupItem1());
            UnionTaskGroup2.addItem(ItemCollection.GetTaskGroupItem2());
            UnionTaskGroup3.addItem(ItemCollection.GetTaskGroupItem3());
            UnionTaskGroup4.addItem(ItemCollection.GetTaskGroupItem4());
            UnionMachineTaskGroup.addItem(ItemCollection.getUnionTaskItem());
            UnionMachineNowTaskGroup.addItem(ItemCollection.GetUnionMachinNowTaskeItem());
            MachineGroup.addItem(ItemCollection.GetMachineGroup());

          //  UnionMachineTaskGroup.callback += OnDataChange;
            //UnionMachineNowTaskGroup.callback += OnDataChange;
          


            listUnionTaskGroup.Add(UnionTaskGroup1);//0
            listUnionTaskGroup.Add(UnionTaskGroup2);//1
            listUnionTaskGroup.Add(UnionTaskGroup3);//2
            listUnionTaskGroup.Add(UnionTaskGroup4);//3
            listUnionTaskGroup.Add(UnionMachineTaskGroup);//4
            listUnionTaskGroup.Add(UnionMachineNowTaskGroup);//5
            listUnionTaskGroup.Add(MachineGroup);//6
         
        }
    }
}