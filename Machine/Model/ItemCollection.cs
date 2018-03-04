using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingControlSys.Model
{
    public static class ItemCollection
    {

        public static String OpcMachineServer = "S7:[FJConnection]";
        public static List<string> GetTaskErrStatusItem()//
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 22; i++)
            {
                list.Add(OpcMachineServer+"DB4,W" + i * 2);
            }
            return list;
        }
        public static List<string> GetTaskStatusItem1()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD2");
            list.Add(OpcMachineServer+"DB1,DWORD6");
            list.Add(OpcMachineServer+"DB1,W10");
            list.Add(OpcMachineServer + "DB1,DWORD12");
            list.Add(OpcMachineServer+"DB1,W0");

            return list;
        }
        public static List<string> GetTaskStatusItem2()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD18");
            list.Add(OpcMachineServer+"DB1,DWORD22");
            list.Add(OpcMachineServer+"DB1,W26");
            list.Add(OpcMachineServer + "DB1,DWORD28");
            list.Add(OpcMachineServer+"DB1,W16");
            return list;
        }
        public static List<string> GetTaskStatusItem3()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD34");
            list.Add(OpcMachineServer+"DB1,DWORD38");
            list.Add(OpcMachineServer+"DB1,W42");
            list.Add(OpcMachineServer + "DB1,DWORD44");
            list.Add(OpcMachineServer+"DB1,W32");
            return list;
        }
        public static List<string> GetTaskStatusItem4()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD50");
            list.Add(OpcMachineServer+"DB1,DWORD54");
            list.Add(OpcMachineServer+"DB1,W58");
            list.Add(OpcMachineServer + "DB1,DWORD60");
            list.Add(OpcMachineServer+"DB1,W48");
            return list;
        }
        public static List<string> GetTaskStatusItem5()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD66");
            list.Add(OpcMachineServer+"DB1,DWORD70");
            list.Add(OpcMachineServer+"DB1,W74");
            list.Add(OpcMachineServer + "DB1,DWORD76");
            list.Add(OpcMachineServer+"DB1,W64");
            return list;
        }
        public static List<string> GetTaskStatusItem6()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD82");
            list.Add(OpcMachineServer+"DB1,DWORD86");
            list.Add(OpcMachineServer+"DB1,W90");
            list.Add(OpcMachineServer + "DB1,DWORD92");
            list.Add(OpcMachineServer+"DB1,W80");
            return list;
        }
        public static List<string> GetTaskStatusItem7()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD98");
            list.Add(OpcMachineServer+"DB1,DWORD102");
            list.Add(OpcMachineServer+"DB1,W106");
            list.Add(OpcMachineServer + "DB1,DWORD108");
            list.Add(OpcMachineServer+"DB1,W96");
            return list;
        }
        public static List<string> GetTaskStatusItem8()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD114");
            list.Add(OpcMachineServer+"DB1,DWORD118");
            list.Add(OpcMachineServer+"DB1,W122");
            list.Add(OpcMachineServer + "DB1,DWORD124");
            list.Add(OpcMachineServer+"DB1,W112");
            return list;
        }
        public static List<string> GetTaskStatusItem9()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD130");
            list.Add(OpcMachineServer+"DB1,DWORD134");
            list.Add(OpcMachineServer+"DB1,W138");
            list.Add(OpcMachineServer + "DB1,DWORD140");
            list.Add(OpcMachineServer+"DB1,W128");
            return list;
        }
        public static List<string> GetTaskStatusItem10()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD146");
            list.Add(OpcMachineServer+"DB1,DWORD150");
            list.Add(OpcMachineServer+"DB1,W154");
            list.Add(OpcMachineServer + "DB1,DWORD156");
            list.Add(OpcMachineServer+"DB1,W144");
            return list;
        }
        public static List<string> GetTaskStatusItem11()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD162");
            list.Add(OpcMachineServer+"DB1,DWORD166");
            list.Add(OpcMachineServer+"DB1,W170");
            list.Add(OpcMachineServer + "DB1,DWORD172");
            list.Add(OpcMachineServer+"DB1,W160");
            return list;
        }
        public static List<string> GetTaskStatusItem12()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD178");
            list.Add(OpcMachineServer+"DB1,DWORD182");
            list.Add(OpcMachineServer+"DB1,W186");
            list.Add(OpcMachineServer + "DB1,DWORD188");
            list.Add(OpcMachineServer+"DB1,W176");
            //list.Add("S7:[FJConnectionGroup1]DB250,DWORD2");
            //list.Add("S7:[FJConnectionGroup1]DB250,DWORD6");
            //list.Add("S7:[FJConnectionGroup1]DB250,W10");
            //list.Add("S7:[FJConnectionGroup1]DB250,W0");

            return list;
        }
        public static List<string> GetTaskStatusItem13()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD194");
            list.Add(OpcMachineServer+"DB1,DWORD198");
            list.Add(OpcMachineServer+"DB1,W202");
            list.Add(OpcMachineServer + "DB1,DWORD204");
            list.Add(OpcMachineServer+"DB1,W192");
            return list;
        }
        public static List<string> GetTaskStatusItem14()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD210");
            list.Add(OpcMachineServer+"DB1,DWORD214");
            list.Add(OpcMachineServer+"DB1,W218");
            list.Add(OpcMachineServer + "DB1,DWORD220");
            list.Add(OpcMachineServer+"DB1,W208");
            return list;
        }
        public static List<string> GetTaskStatusItem15()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD226");
            list.Add(OpcMachineServer+"DB1,DWORD230");
            list.Add(OpcMachineServer+"DB1,W234");
            list.Add(OpcMachineServer + "DB1,DWORD236");
            list.Add(OpcMachineServer+"DB1,W224");
            return list;
        }
        public static List<string> GetTaskStatusItem16()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD242");
            list.Add(OpcMachineServer+"DB1,DWORD246");
            list.Add(OpcMachineServer+"DB1,W250");
            list.Add(OpcMachineServer + "DB1,DWORD252");
            list.Add(OpcMachineServer+"DB1,W240");
            return list;
        }
        public static List<string> GetTaskStatusItem17()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD258");
            list.Add(OpcMachineServer+"DB1,DWORD262");
            list.Add(OpcMachineServer+"DB1,W266");
            list.Add(OpcMachineServer + "DB1,DWORD268");
            list.Add(OpcMachineServer+"DB1,W256");
            return list;
        }
        public static List<string> GetTaskStatusItem18()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD274");
            list.Add(OpcMachineServer+"DB1,DWORD278");
            list.Add(OpcMachineServer+"DB1,W282");
            list.Add(OpcMachineServer + "DB1,DWORD284");
            list.Add(OpcMachineServer+"DB1,W272");
            return list;
        }
        public static List<string> GetTaskStatusItem19()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD290");
            list.Add(OpcMachineServer+"DB1,DWORD294");
            list.Add(OpcMachineServer+"DB1,W298");
            list.Add(OpcMachineServer + "DB1,DWORD300");
            list.Add(OpcMachineServer+"DB1,W288");
            return list;
        }
        public static List<string> GetTaskStatusItem20()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD306");
            list.Add(OpcMachineServer+"DB1,DWORD310");
            list.Add(OpcMachineServer+"DB1,W314");
            list.Add(OpcMachineServer + "DB1,DWORD316");
            list.Add(OpcMachineServer+"DB1,W304");
            return list;
        }
        public static List<string> GetTaskStatusItem21()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD322");
            list.Add(OpcMachineServer+"DB1,DWORD326");
            list.Add(OpcMachineServer+"DB1,W330");
            list.Add(OpcMachineServer + "DB1,DWORD332");
            list.Add(OpcMachineServer+"DB1,W320");
            return list;
        }
        public static List<string> GetTaskStatusItem22()
        {
            List<string> list = new List<string>();

            list.Add(OpcMachineServer+"DB1,DWORD338");
            list.Add(OpcMachineServer+"DB1,DWORD342");
            list.Add(OpcMachineServer+"DB1,W346");
            list.Add(OpcMachineServer + "DB1,DWORD348");
            list.Add(OpcMachineServer+"DB1,W336");
            return list;
        }
        public static List<string> GetTaskStatusItem23()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W88");
            list.Add(OpcMachineServer+"DB1,W90");
            return list;
        }
        public static List<string> GetTaskStatusItem24()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W92");
            list.Add(OpcMachineServer+"DB1,W94");
            return list;
        }
        public static List<string> GetTaskStatusItem25()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W96");
            list.Add(OpcMachineServer+"DB1,W98");
            return list;
        }
        public static List<string> GetTaskStatusItem26()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W100");
            list.Add(OpcMachineServer+"DB1,W102");
            return list;
        }
        public static List<string> GetTaskStatusItem27()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W104");
            list.Add(OpcMachineServer+"DB1,W106");
            return list;
        }
        public static List<string> GetTaskStatusItem28()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W108");
            list.Add(OpcMachineServer+"DB1,W110");
            return list;
        }
        public static List<string> GetTaskStatusItem29()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W112");
            list.Add(OpcMachineServer+"DB1,W114");
            return list;
        }
        public static List<string> GetTaskStatusItem30()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W116");
            list.Add(OpcMachineServer+"DB1,W118");
            return list;
        }
        public static List<string> GetTaskStatusItem31()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W120");
            list.Add(OpcMachineServer+"DB1,W122");
            return list;
        }
        public static List<string> GetTaskStatusItem32()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W124");
            list.Add(OpcMachineServer+"DB1,W126");
            return list;
        }
        public static List<string> GetTaskStatusItem33()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W128");
            list.Add(OpcMachineServer+"DB1,W130");
            return list;
        }
        public static List<string> GetTaskStatusItem34()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W132");
            list.Add(OpcMachineServer+"DB1,W134");
            return list;
        }



        public static List<string> GetTaskStatusItem35()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W136");
            list.Add(OpcMachineServer+"DB1,W138");
            return list;
        }
        public static List<string> GetTaskStatusItem36()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W140");
            list.Add(OpcMachineServer+"DB1,W142");
            return list;
        }
        public static List<string> GetTaskStatusItem37()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W144");
            list.Add(OpcMachineServer+"DB1,W146");
            return list;
        }
        public static List<string> GetTaskStatusItem38()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W148");
            list.Add(OpcMachineServer+"DB1,W150");
            return list;
        }
        public static List<string> GetTaskStatusItem39()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W152");
            list.Add(OpcMachineServer+"DB1,W154");
            return list;
        }
        public static List<string> GetTaskStatusItem40()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W156");
            list.Add(OpcMachineServer+"DB1,W158");
            return list;
        }
        public static List<string> GetTaskStatusItem41()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W160");
            list.Add(OpcMachineServer+"DB1,W162");
            return list;
        }
        public static List<string> GetTaskStatusItem42()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W164");
            list.Add(OpcMachineServer+"DB1,W166");
            return list;
        }
        public static List<string> GetTaskStatusItem43()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W168");
            list.Add(OpcMachineServer+"DB1,W170");
            return list;
        }
        public static List<string> GetTaskStatusItem44()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W172");
            list.Add(OpcMachineServer+"DB1,W174");
            return list;
        }
        public static List<string> GetTaskStatusItem45()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W176");
            list.Add(OpcMachineServer+"DB1,W178");
            return list;
        }
        public static List<string> GetTaskStatusItem46()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W180");
            list.Add(OpcMachineServer+"DB1,W182");
            return list;
        }
        public static List<string> GetTaskStatusItem47()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W184");
            list.Add(OpcMachineServer+"DB1,W186");
            return list;
        }
        public static List<string> GetTaskStatusItem48()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W188");
            list.Add(OpcMachineServer+"DB1,W190");
            return list;
        }
        public static List<string> GetTaskStatusItem49()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W192");
            list.Add(OpcMachineServer+"DB1,W194");
            return list;
        }
        public static List<string> GetTaskStatusItem50()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W196");
            list.Add(OpcMachineServer+"DB1,W198");
            return list;
        }
        public static List<string> GetTaskStatusItem51()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W200");
            list.Add(OpcMachineServer+"DB1,W202");
            return list;
        }
        public static List<string> GetTaskStatusItem52()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W204");
            list.Add(OpcMachineServer+"DB1,W206");
            return list;
        }
        public static List<string> GetTaskStatusItem53()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W208");
            list.Add(OpcMachineServer+"DB1,W210");
            return list;
        }
        public static List<string> GetTaskStatusItem54()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W212");
            list.Add(OpcMachineServer+"DB1,W214");
            return list;
        }
        public static List<string> GetTaskStatusItem55()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W216");
            list.Add(OpcMachineServer+"DB1,W218");
            return list;
        }
        public static List<string> GetTaskStatusItem56()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W220");
            list.Add(OpcMachineServer+"DB1,W222");
            return list;
        }
        public static List<string> GetTaskStatusItem57()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W224");
            list.Add(OpcMachineServer+"DB1,W226");
            return list;
        }
        public static List<string> GetTaskStatusItem58()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W228");
            list.Add(OpcMachineServer+"DB1,W230");
            return list;
        }
        public static List<string> GetTaskStatusItem59()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W232");
            list.Add(OpcMachineServer+"DB1,W234");
            return list;
        }
        public static List<string> GetTaskStatusItem60()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W236");
            list.Add(OpcMachineServer+"DB1,W238");
            return list;
        }
        public static List<string> GetTaskStatusItem61()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W240");
            list.Add(OpcMachineServer+"DB1,W242");
            return list;
        }
        public static List<string> GetTaskStatusItem62()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W244");
            list.Add(OpcMachineServer+"DB1,W246");
            return list;
        }
        public static List<string> GetTaskStatusItem63()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W248");
            list.Add(OpcMachineServer+"DB1,W250");
            return list;
        }
        public static List<string> GetTaskStatusItem64()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W252");
            list.Add(OpcMachineServer+"DB1,W254");
            return list;
        }
        public static List<string> GetTaskStatusItem65()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W256");
            list.Add(OpcMachineServer+"DB1,W258");
            return list;
        }
        public static List<string> GetTaskStatusItem66()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W260");
            list.Add(OpcMachineServer+"DB1,W262");
            return list;
        }
        public static List<string> GetTaskStatusItem67()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W264");
            list.Add(OpcMachineServer+"DB1,W266");
            return list;
        }
        public static List<string> GetTaskStatusItem68()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W268");
            list.Add(OpcMachineServer+"DB1,W270");
            return list;
        }
        public static List<string> GetTaskStatusItem69()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W272");
            list.Add(OpcMachineServer+"DB1,W274");
            return list;
        }
        public static List<string> GetTaskStatusItem70()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W276");
            list.Add(OpcMachineServer+"DB1,W278");
            return list;
        }
        public static List<string> GetTaskStatusItem71()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W280");
            list.Add(OpcMachineServer+"DB1,W282");
            return list;
        }
        public static List<string> GetTaskStatusItem72()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W284");
            list.Add(OpcMachineServer+"DB1,W286");
            return list;
        }
        public static List<string> GetTaskStatusItem73()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W288");
            list.Add(OpcMachineServer+"DB1,W290");
            return list;
        }
        public static List<string> GetTaskStatusItem74()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W292");
            list.Add(OpcMachineServer+"DB1,W294");
            return list;
        }
        public static List<string> GetTaskStatusItem75()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W296");
            list.Add(OpcMachineServer+"DB1,W298");
            return list;
        }
        public static List<string> GetTaskStatusItem76()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W300");
            list.Add(OpcMachineServer+"DB1,W302");
            return list;
        }
        public static List<string> GetTaskStatusItem77()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W304");
            list.Add(OpcMachineServer+"DB1,W306");
            return list;
        }
        public static List<string> GetTaskStatusItem78()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W308");
            list.Add(OpcMachineServer+"DB1,W310");
            return list;
        }
        public static List<string> GetTaskStatusItem79()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W312");
            list.Add(OpcMachineServer+"DB1,W314");
            return list;
        }
        public static List<string> GetTaskStatusItem80()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W316");
            list.Add(OpcMachineServer+"DB1,W318");
            return list;
        }
        public static List<string> GetTaskStatusItem81()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W320");
            list.Add(OpcMachineServer+"DB1,W322");
            return list;
        }
        public static List<string> GetTaskStatusItem82()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W324");
            list.Add(OpcMachineServer+"DB1,W326");
            return list;
        }
        public static List<string> GetTaskStatusItem83()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W328");
            list.Add(OpcMachineServer+"DB1,W330");
            return list;
        }
        public static List<string> GetTaskStatusItem84()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W332");
            list.Add(OpcMachineServer+"DB1,W334");
            return list;
        }
        public static List<string> GetTaskStatusItem85()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W336");
            list.Add(OpcMachineServer+"DB1,W338");
            return list;
        }
        public static List<string> GetTaskStatusItem86()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W340");
            list.Add(OpcMachineServer+"DB1,W342");
            return list;
        }
        public static List<string> GetTaskStatusItem87()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W344");
            list.Add(OpcMachineServer+"DB1,W346");
            return list;
        }
        public static List<string> GetTaskStatusItem88()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W348");
            list.Add(OpcMachineServer+"DB1,W350");
            return list;
        }
        public static List<string> GetTaskStatusItem100()
        {
            List<string> list = new List<string>();
            list.Add(OpcMachineServer+"DB1,W0");
            list.Add(OpcMachineServer+"DB1,W2");
            list.Add(OpcMachineServer+"DB1,W4");
            list.Add(OpcMachineServer+"DB1,W6");
            list.Add(OpcMachineServer+"DB1,W8");
            list.Add(OpcMachineServer+"DB1,W10");
            list.Add(OpcMachineServer+"DB1,W12");
            list.Add(OpcMachineServer+"DB1,W14");
            list.Add(OpcMachineServer+"DB1,W16");
            list.Add(OpcMachineServer+"DB1,W18");
            list.Add(OpcMachineServer+"DB1,W20");
            list.Add(OpcMachineServer+"DB1,W22");
            list.Add(OpcMachineServer+"DB1,W24");
            list.Add(OpcMachineServer+"DB1,W26");
            list.Add(OpcMachineServer+"DB1,W28");
            list.Add(OpcMachineServer+"DB1,W30");
            list.Add(OpcMachineServer+"DB1,W32");
            list.Add(OpcMachineServer+"DB1,W34");
            list.Add(OpcMachineServer+"DB1,W36");
            list.Add(OpcMachineServer+"DB1,W38");
            list.Add(OpcMachineServer+"DB1,W40");
            list.Add(OpcMachineServer+"DB1,W42");
            list.Add(OpcMachineServer+"DB1,W44");
            list.Add(OpcMachineServer+"DB1,W46");
            list.Add(OpcMachineServer+"DB1,W48");
            list.Add(OpcMachineServer+"DB1,W50");
            list.Add(OpcMachineServer+"DB1,W52");
            list.Add(OpcMachineServer+"DB1,W54");
            list.Add(OpcMachineServer+"DB1,W56");
            list.Add(OpcMachineServer+"DB1,W58");
            list.Add(OpcMachineServer+"DB1,W60");
            list.Add(OpcMachineServer+"DB1,W62");
            list.Add(OpcMachineServer+"DB1,W64");
            list.Add(OpcMachineServer+"DB1,W66");
            list.Add(OpcMachineServer+"DB1,W68");
            list.Add(OpcMachineServer+"DB1,W70");
            list.Add(OpcMachineServer+"DB1,W72");
            list.Add(OpcMachineServer+"DB1,W74");
            list.Add(OpcMachineServer+"DB1,W76");
            list.Add(OpcMachineServer+"DB1,W78");
            list.Add(OpcMachineServer+"DB1,W80");
            list.Add(OpcMachineServer+"DB1,W82");
            list.Add(OpcMachineServer+"DB1,W84");
            list.Add(OpcMachineServer+"DB1,W86");
            list.Add(OpcMachineServer+"DB1,W88");
            list.Add(OpcMachineServer+"DB1,W90");
            list.Add(OpcMachineServer+"DB1,W92");
            list.Add(OpcMachineServer+"DB1,W94");
            list.Add(OpcMachineServer+"DB1,W96");
            list.Add(OpcMachineServer+"DB1,W98");
            list.Add(OpcMachineServer+"DB1,W100");
            list.Add(OpcMachineServer+"DB1,W102");
            list.Add(OpcMachineServer+"DB1,W104");
            list.Add(OpcMachineServer+"DB1,W106");
            list.Add(OpcMachineServer+"DB1,W108");
            list.Add(OpcMachineServer+"DB1,W110");
            list.Add(OpcMachineServer+"DB1,W112");
            list.Add(OpcMachineServer+"DB1,W114");
            list.Add(OpcMachineServer+"DB1,W116");
            list.Add(OpcMachineServer+"DB1,W118");
            list.Add(OpcMachineServer+"DB1,W120");
            list.Add(OpcMachineServer+"DB1,W122");
            list.Add(OpcMachineServer+"DB1,W124");
            list.Add(OpcMachineServer+"DB1,W126");
            list.Add(OpcMachineServer+"DB1,W128");
            list.Add(OpcMachineServer+"DB1,W130");
            list.Add(OpcMachineServer+"DB1,W132");
            list.Add(OpcMachineServer+"DB1,W134");
            list.Add(OpcMachineServer+"DB1,W136");
            list.Add(OpcMachineServer+"DB1,W138");
            list.Add(OpcMachineServer+"DB1,W140");
            list.Add(OpcMachineServer+"DB1,W142");
            list.Add(OpcMachineServer+"DB1,W144");
            list.Add(OpcMachineServer+"DB1,W146");
            list.Add(OpcMachineServer+"DB1,W148");
            list.Add(OpcMachineServer+"DB1,W150");
            list.Add(OpcMachineServer+"DB1,W152");
            list.Add(OpcMachineServer+"DB1,W154");
            list.Add(OpcMachineServer+"DB1,W156");
            list.Add(OpcMachineServer+"DB1,W158");
            list.Add(OpcMachineServer+"DB1,W160");
            list.Add(OpcMachineServer+"DB1,W162");
            list.Add(OpcMachineServer+"DB1,W164");
            list.Add(OpcMachineServer+"DB1,W166");
            list.Add(OpcMachineServer+"DB1,W168");
            list.Add(OpcMachineServer+"DB1,W170");
            list.Add(OpcMachineServer+"DB1,W172");
            list.Add(OpcMachineServer+"DB1,W174");
            list.Add(OpcMachineServer+"DB1,W176");
            list.Add(OpcMachineServer+"DB1,W178");
            list.Add(OpcMachineServer+"DB1,W180");
            list.Add(OpcMachineServer+"DB1,W182");
            list.Add(OpcMachineServer+"DB1,W184");
            list.Add(OpcMachineServer+"DB1,W186");
            list.Add(OpcMachineServer+"DB1,W188");
            list.Add(OpcMachineServer+"DB1,W190");
            list.Add(OpcMachineServer+"DB1,W192");
            list.Add(OpcMachineServer+"DB1,W194");
            list.Add(OpcMachineServer+"DB1,W196");
            list.Add(OpcMachineServer+"DB1,W198");
            list.Add(OpcMachineServer+"DB1,W200");
            list.Add(OpcMachineServer+"DB1,W202");
            list.Add(OpcMachineServer+"DB1,W204");
            list.Add(OpcMachineServer+"DB1,W206");
            list.Add(OpcMachineServer+"DB1,W208");
            list.Add(OpcMachineServer+"DB1,W210");
            list.Add(OpcMachineServer+"DB1,W212");
            list.Add(OpcMachineServer+"DB1,W214");
            list.Add(OpcMachineServer+"DB1,W216");
            list.Add(OpcMachineServer+"DB1,W218");
            list.Add(OpcMachineServer+"DB1,W220");
            list.Add(OpcMachineServer+"DB1,W222");
            list.Add(OpcMachineServer+"DB1,W224");
            list.Add(OpcMachineServer+"DB1,W226");
            list.Add(OpcMachineServer+"DB1,W228");
            list.Add(OpcMachineServer+"DB1,W230");
            list.Add(OpcMachineServer+"DB1,W232");
            list.Add(OpcMachineServer+"DB1,W234");
            list.Add(OpcMachineServer+"DB1,W236");
            list.Add(OpcMachineServer+"DB1,W238");
            list.Add(OpcMachineServer+"DB1,W240");
            list.Add(OpcMachineServer+"DB1,W242");
            list.Add(OpcMachineServer+"DB1,W244");
            list.Add(OpcMachineServer+"DB1,W246");
            list.Add(OpcMachineServer+"DB1,W248");
            list.Add(OpcMachineServer+"DB1,W250");
            list.Add(OpcMachineServer+"DB1,W252");
            list.Add(OpcMachineServer+"DB1,W254");
            list.Add(OpcMachineServer+"DB1,W256");
            list.Add(OpcMachineServer+"DB1,W258");
            list.Add(OpcMachineServer+"DB1,W260");
            list.Add(OpcMachineServer+"DB1,W262");
            list.Add(OpcMachineServer+"DB1,W264");
            list.Add(OpcMachineServer+"DB1,W266");
            list.Add(OpcMachineServer+"DB1,W268");
            list.Add(OpcMachineServer+"DB1,W270");
            list.Add(OpcMachineServer+"DB1,W272");
            list.Add(OpcMachineServer+"DB1,W274");
            list.Add(OpcMachineServer+"DB1,W276");
            list.Add(OpcMachineServer+"DB1,W278");
            list.Add(OpcMachineServer+"DB1,W280");
            list.Add(OpcMachineServer+"DB1,W282");
            list.Add(OpcMachineServer+"DB1,W284");
            list.Add(OpcMachineServer+"DB1,W286");
            list.Add(OpcMachineServer+"DB1,W288");
            list.Add(OpcMachineServer+"DB1,W290");
            list.Add(OpcMachineServer+"DB1,W292");
            list.Add(OpcMachineServer+"DB1,W294");
            list.Add(OpcMachineServer+"DB1,W296");
            list.Add(OpcMachineServer+"DB1,W298");
            list.Add(OpcMachineServer+"DB1,W300");
            list.Add(OpcMachineServer+"DB1,W302");
            list.Add(OpcMachineServer+"DB1,W304");
            list.Add(OpcMachineServer+"DB1,W306");
            list.Add(OpcMachineServer+"DB1,W308");
            list.Add(OpcMachineServer+"DB1,W310");
            list.Add(OpcMachineServer+"DB1,W312");
            list.Add(OpcMachineServer+"DB1,W314");
            list.Add(OpcMachineServer+"DB1,W316");
            list.Add(OpcMachineServer+"DB1,W318");
            list.Add(OpcMachineServer+"DB1,W320");
            list.Add(OpcMachineServer+"DB1,W322");
            list.Add(OpcMachineServer+"DB1,W324");
            list.Add(OpcMachineServer+"DB1,W326");
            list.Add(OpcMachineServer+"DB1,W328");
            list.Add(OpcMachineServer+"DB1,W330");
            list.Add(OpcMachineServer+"DB1,W332");
            list.Add(OpcMachineServer+"DB1,W334");
            list.Add(OpcMachineServer+"DB1,W336");
            list.Add(OpcMachineServer+"DB1,W338");
            list.Add(OpcMachineServer+"DB1,W340");
            list.Add(OpcMachineServer+"DB1,W342");
            list.Add(OpcMachineServer+"DB1,W344");
            list.Add(OpcMachineServer+"DB1,W346");
            list.Add(OpcMachineServer+"DB1,W348");
            list.Add(OpcMachineServer+"DB1,W350");
            return list;

        }
    }
}
