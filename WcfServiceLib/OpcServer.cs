using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpcRcw.Da;
using WebService.Modle;

namespace WebService
{
    public static class OpcServer
    {

       public static IOPCServer pIOPCServer;  //定义opcServer对象
       internal const string SERVER_NAME = "OPC.SimaticNET";
       public static Group UnionTaskGroup1,MachineGroup, UnionTaskGroup2, UnionTaskGroup3, UnionTaskGroup4, UnionMachineTaskGroup, UnionMachineNowTaskGroup;
       public static Group SortGroupA, SortGroupB;
       internal const int LOCALE_ID = 0x409;  
          public static  List<Group> listUnionTaskGroup = new List<Group>();
          
        public static void Connect(string groupConnectionGroup ="S7:[FJConnectionGroup1]")
        {
            if (pIOPCServer == null)
            {
                Type svrComponenttyp;
                Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
                svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                AddUnionTaskGroup();
                AddSortGroup(groupConnectionGroup);
            }
            else
            { 
               listUnionTaskGroup[7].RemovedItem();//第二次调用的时候清除重新添加
               listUnionTaskGroup[8].RemovedItem();
               AddSortGroup(groupConnectionGroup);
            } 
        }
        public static string FJConnectionGroup { get; set; }
        public static void AddSortGroup(string groupConnectionGroup)
        {
            
            SortGroupA.addItem(ItemCollection.GetASortingItem(groupConnectionGroup));//A组预分拣
            SortGroupB.addItem(ItemCollection.GetASortingItem(groupConnectionGroup));//B组预分拣
             
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


            SortGroupA = new Group(pIOPCServer, 8, "group8", 1, LOCALE_ID);// A组预分拣
            SortGroupB = new Group(pIOPCServer, 9, "group9", 1, LOCALE_ID);// B组预分拣 
            /********************************************************************************/
            UnionTaskGroup1.addItem(ItemCollection.GetTaskGroupItem1());
            UnionTaskGroup2.addItem(ItemCollection.GetTaskGroupItem2());
            UnionTaskGroup3.addItem(ItemCollection.GetTaskGroupItem3());
            UnionTaskGroup4.addItem(ItemCollection.GetTaskGroupItem4());
            UnionMachineTaskGroup.addItem(ItemCollection.getUnionTaskItem());
            UnionMachineNowTaskGroup.addItem(ItemCollection.GetUnionMachinNowTaskeItem());
            MachineGroup.addItem(ItemCollection.GetMachineGroup());

           // SortGroupA.addItem(ItemCollection.GetASortingItem(FJConnectionGroup));//A组预分拣
           // SortGroupB.addItem(ItemCollection.GetASortingItem(FJConnectionGroup));//B组预分拣
           

             

            
          //  UnionMachineTaskGroup.callback += OnDataChange;
            //UnionMachineNowTaskGroup.callback += OnDataChange;
          


            listUnionTaskGroup.Add(UnionTaskGroup1);//0
            listUnionTaskGroup.Add(UnionTaskGroup2);//1
            listUnionTaskGroup.Add(UnionTaskGroup3);//2
            listUnionTaskGroup.Add(UnionTaskGroup4);//3
            listUnionTaskGroup.Add(UnionMachineTaskGroup);//4 累计抓烟数量
            listUnionTaskGroup.Add(UnionMachineNowTaskGroup);//5 当前抓烟数量
            listUnionTaskGroup.Add(MachineGroup);//6 累计放烟数量
            listUnionTaskGroup.Add(SortGroupA);//7
            listUnionTaskGroup.Add(SortGroupB);//8
         
        }
    }
}