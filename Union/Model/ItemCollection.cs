using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingControlSys.Model
{
   public static class ItemCollection
    {
       public static String OpcUnionServer = "S7:[UnionConnection]";
       /// <summary>
       /// 获取任务item
       /// </summary>
       /// <returns></returns>
       public static List<string> GetTaskItem()
       {
           List<string> list = new List<string>();
           list.Add(OpcUnionServer+"DB1,W2");
           list.Add(OpcUnionServer+"DB1,W6");
           list.Add(OpcUnionServer+"DB1,W8");
           list.Add(OpcUnionServer+"DB1,W10");
           list.Add(OpcUnionServer+"DB1,W12");
           list.Add(OpcUnionServer+"DB1,W14");
           list.Add(OpcUnionServer+"DB1,W16");
           list.Add(OpcUnionServer+"DB1,W18");
           list.Add(OpcUnionServer+"DB1,W20");
           list.Add(OpcUnionServer+"DB1,W22");
           list.Add(OpcUnionServer+"DB1,W24");
           list.Add(OpcUnionServer+"DB1,W26");
           list.Add(OpcUnionServer+"DB1,W28");
           list.Add(OpcUnionServer+"DB1,W30");
           list.Add(OpcUnionServer+"DB1,W32");
           list.Add(OpcUnionServer+"DB1,W34");
           list.Add(OpcUnionServer+"DB1,W36");
           list.Add(OpcUnionServer+"DB1,W38");
           list.Add(OpcUnionServer+"DB1,W40");
           list.Add(OpcUnionServer+"DB1,W42");
           list.Add(OpcUnionServer+"DB1,W44");
           list.Add(OpcUnionServer+"DB1,W46");
           list.Add(OpcUnionServer+"DB1,W48");
           list.Add(OpcUnionServer+"DB1,W50");
           list.Add(OpcUnionServer+"DB1,W52");
           list.Add(OpcUnionServer+"DB1,W0");
           return list;
       }

       public static List<string> GetTaskStatusItem3()//C01-C27
       {
           List<string> list = new List<string>();
           for (int i = 1; i <= 27; i++)
           {
               list.Add(OpcUnionServer+"DB4,W" + i * 2);
           }
           return list;
       }

       public static List<string> GetTaskStatusItem4()//E01-E27
       {
           List<string> list = new List<string>();
           for (int i = 31; i <= 72; i++)
           {
               list.Add(OpcUnionServer+"DB4,W" + i * 2);
           }
           return list;
       }


       public static List<string> GetTaskStatusItem10()
       {
           List<string> list = new List<string>();
           list.Add(OpcUnionServer+"DB1,W2");
           list.Add(OpcUnionServer+"DB1,W4");
           list.Add(OpcUnionServer+"DB1,W6");
           list.Add(OpcUnionServer+"DB1,W8");
           list.Add(OpcUnionServer+"DB1,W10");
           list.Add(OpcUnionServer+"DB1,W12");
           list.Add(OpcUnionServer+"DB1,W14");
           list.Add(OpcUnionServer+"DB1,W16");
           list.Add(OpcUnionServer+"DB1,W18");
           list.Add(OpcUnionServer+"DB1,W20");
           list.Add(OpcUnionServer+"DB1,W22");
           list.Add(OpcUnionServer+"DB1,W24");
           list.Add(OpcUnionServer+"DB1,W0");
           return list;
       }
       public static List<string> GetTaskStatusItem11()
       {
           List<string> list = new List<string>();
           list.Add(OpcUnionServer+"DB30,W0");
           list.Add(OpcUnionServer+"DB30,W2");
           list.Add(OpcUnionServer+"DB30,W4");
           list.Add(OpcUnionServer+"DB30,W6");
           list.Add(OpcUnionServer+"DB30,W8");
           list.Add(OpcUnionServer+"DB30,W10");
           list.Add(OpcUnionServer+"DB30,W12");
           list.Add(OpcUnionServer+"DB30,W14");
           list.Add(OpcUnionServer+"DB30,W16");
           list.Add(OpcUnionServer+"DB30,W18");
           list.Add(OpcUnionServer+"DB30,W20");
           list.Add(OpcUnionServer+"DB30,W22");
           list.Add(OpcUnionServer+"DB30,W24");
           list.Add(OpcUnionServer+"DB30,W26");
           list.Add(OpcUnionServer+"DB30,W28");
           list.Add(OpcUnionServer+"DB30,W30");
           list.Add(OpcUnionServer+"DB30,W32");
           list.Add(OpcUnionServer+"DB30,W34");
           list.Add(OpcUnionServer+"DB30,W36");
           list.Add(OpcUnionServer+"DB30,W38");
           list.Add(OpcUnionServer+"DB30,W40");
           list.Add(OpcUnionServer+"DB30,W42");
           list.Add(OpcUnionServer+"DB30,W44");
           list.Add(OpcUnionServer+"DB30,W46");
           list.Add(OpcUnionServer+"DB30,W48");
           list.Add(OpcUnionServer+"DB30,W50");
           list.Add(OpcUnionServer+"DB30,W52");
           list.Add(OpcUnionServer+"DB30,W54");
           list.Add(OpcUnionServer+"DB30,W56");
           list.Add(OpcUnionServer+"DB30,W58");
           list.Add(OpcUnionServer+"DB30,W60");
           list.Add(OpcUnionServer+"DB30,W62");
           list.Add(OpcUnionServer+"DB30,W64");
           list.Add(OpcUnionServer+"DB30,W66");
           list.Add(OpcUnionServer+"DB30,W68");
           list.Add(OpcUnionServer+"DB30,W70");
           list.Add(OpcUnionServer+"DB30,W72");
           list.Add(OpcUnionServer+"DB30,W74");
           list.Add(OpcUnionServer+"DB30,W76");
           list.Add(OpcUnionServer+"DB30,W78");
           list.Add(OpcUnionServer+"DB30,W80");
           list.Add(OpcUnionServer+"DB30,W82");
           list.Add(OpcUnionServer+"DB30,W84");
           list.Add(OpcUnionServer+"DB30,W86");
           list.Add(OpcUnionServer+"DB30,W88");
           list.Add(OpcUnionServer+"DB30,W90");
           list.Add(OpcUnionServer+"DB30,W92");
           list.Add(OpcUnionServer+"DB30,W94");
           list.Add(OpcUnionServer+"DB30,W96");
           list.Add(OpcUnionServer+"DB30,W98");
           list.Add(OpcUnionServer+"DB30,W100");
           list.Add(OpcUnionServer+"DB30,W102");
           list.Add(OpcUnionServer+"DB30,W104");
           list.Add(OpcUnionServer+"DB30,W106");
           list.Add(OpcUnionServer+"DB30,W108");
           list.Add(OpcUnionServer+"DB30,W110");
           list.Add(OpcUnionServer+"DB30,W112");
           list.Add(OpcUnionServer+"DB30,W114");
           list.Add(OpcUnionServer+"DB30,W116");
           list.Add(OpcUnionServer+"DB30,W118");
           list.Add(OpcUnionServer+"DB30,W120");
           list.Add(OpcUnionServer+"DB30,W122");
           list.Add(OpcUnionServer+"DB30,W124");
           list.Add(OpcUnionServer+"DB30,W126");
           list.Add(OpcUnionServer+"DB30,W128");
           list.Add(OpcUnionServer+"DB30,W130");
           list.Add(OpcUnionServer+"DB30,W132");
           list.Add(OpcUnionServer+"DB30,W134");
           list.Add(OpcUnionServer+"DB30,W136");
           list.Add(OpcUnionServer+"DB30,W138");
           list.Add(OpcUnionServer+"DB30,W140");
           list.Add(OpcUnionServer+"DB30,W142");
           list.Add(OpcUnionServer+"DB30,W144");
           list.Add(OpcUnionServer+"DB30,W146");
           list.Add(OpcUnionServer+"DB30,W148");
           list.Add(OpcUnionServer+"DB30,W150");
           list.Add(OpcUnionServer+"DB30,W152");
           list.Add(OpcUnionServer+"DB30,W154");
           list.Add(OpcUnionServer+"DB30,W156");
           list.Add(OpcUnionServer+"DB30,W158");
          
           return list;
       }
       public static List<string> GetTaskStatusItem12()
       {
           List<string> list = new List<string>();
           list.Add(OpcUnionServer+"DB30,W200");
           list.Add(OpcUnionServer+"DB30,W202");
           list.Add(OpcUnionServer+"DB30,W204");
           list.Add(OpcUnionServer+"DB30,W206");
           list.Add(OpcUnionServer+"DB30,W208");
           list.Add(OpcUnionServer+"DB30,W210");
           list.Add(OpcUnionServer+"DB30,W212");
           list.Add(OpcUnionServer+"DB30,W214");
           list.Add(OpcUnionServer+"DB30,W216");
           list.Add(OpcUnionServer+"DB30,W218");
           list.Add(OpcUnionServer+"DB30,W220");
           list.Add(OpcUnionServer+"DB30,W222");
           list.Add(OpcUnionServer+"DB30,W224");
           list.Add(OpcUnionServer+"DB30,W226");
           list.Add(OpcUnionServer+"DB30,W228");
           list.Add(OpcUnionServer+"DB30,W230");
           list.Add(OpcUnionServer+"DB30,W232");
           list.Add(OpcUnionServer+"DB30,W234");
           list.Add(OpcUnionServer+"DB30,W236");
           list.Add(OpcUnionServer+"DB30,W238");
           list.Add(OpcUnionServer+"DB30,W240");
           list.Add(OpcUnionServer+"DB30,W242");
           list.Add(OpcUnionServer+"DB30,W244");
           list.Add(OpcUnionServer+"DB30,W246");
           list.Add(OpcUnionServer+"DB30,W248");
           list.Add(OpcUnionServer+"DB30,W250");
           list.Add(OpcUnionServer+"DB30,W252");
           list.Add(OpcUnionServer+"DB30,W254");
           list.Add(OpcUnionServer+"DB30,W256");
           list.Add(OpcUnionServer+"DB30,W258");
           list.Add(OpcUnionServer+"DB30,W260");
           list.Add(OpcUnionServer+"DB30,W262");
           list.Add(OpcUnionServer+"DB30,W264");
           list.Add(OpcUnionServer+"DB30,W266");
           list.Add(OpcUnionServer+"DB30,W268");
           list.Add(OpcUnionServer+"DB30,W270");
           list.Add(OpcUnionServer+"DB30,W272");
           list.Add(OpcUnionServer+"DB30,W274");
           list.Add(OpcUnionServer+"DB30,W276");
           list.Add(OpcUnionServer+"DB30,W278");
           list.Add(OpcUnionServer+"DB30,W280");
           list.Add(OpcUnionServer+"DB30,W282");
           list.Add(OpcUnionServer+"DB30,W284");
           list.Add(OpcUnionServer+"DB30,W286");
           list.Add(OpcUnionServer+"DB30,W288");
           list.Add(OpcUnionServer+"DB30,W290");
           list.Add(OpcUnionServer+"DB30,W292");
           list.Add(OpcUnionServer+"DB30,W294");
           list.Add(OpcUnionServer+"DB30,W296");
           list.Add(OpcUnionServer+"DB30,W298");
           list.Add(OpcUnionServer+"DB30,W300");
           list.Add(OpcUnionServer+"DB30,W302");
           list.Add(OpcUnionServer+"DB30,W304");
           list.Add(OpcUnionServer+"DB30,W306");
           list.Add(OpcUnionServer+"DB30,W308");
           list.Add(OpcUnionServer+"DB30,W310");
           list.Add(OpcUnionServer+"DB30,W312");
           list.Add(OpcUnionServer+"DB30,W314");
           list.Add(OpcUnionServer+"DB30,W316");
           list.Add(OpcUnionServer+"DB30,W318");
           list.Add(OpcUnionServer+"DB30,W320");
           list.Add(OpcUnionServer+"DB30,W322");
           list.Add(OpcUnionServer+"DB30,W324");
           list.Add(OpcUnionServer+"DB30,W326");
           list.Add(OpcUnionServer+"DB30,W328");
           list.Add(OpcUnionServer+"DB30,W330");
           list.Add(OpcUnionServer+"DB30,W332");
           list.Add(OpcUnionServer+"DB30,W334");
           list.Add(OpcUnionServer+"DB30,W336");
           list.Add(OpcUnionServer+"DB30,W338");
           list.Add(OpcUnionServer+"DB30,W340");
           list.Add(OpcUnionServer+"DB30,W342");
           list.Add(OpcUnionServer+"DB30,W344");
           list.Add(OpcUnionServer+"DB30,W346");
           list.Add(OpcUnionServer+"DB30,W348");
           list.Add(OpcUnionServer+"DB30,W350");
           list.Add(OpcUnionServer+"DB30,W352");
           list.Add(OpcUnionServer+"DB30,W354");
           list.Add(OpcUnionServer+"DB30,W356");
           list.Add(OpcUnionServer+"DB30,W358");
           return list;
       }
    }
}
