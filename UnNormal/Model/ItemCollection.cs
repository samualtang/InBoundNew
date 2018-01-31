﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingControlSys.Model
{
   public static class ItemCollection
    {
       /// <summary>
       /// 获取任务item
       /// </summary>
       /// <returns></returns>
       public static List<string> GetTaskItem()
       {
           List<string> list = new List<string>();
           for (int i = 0; i <= 900; i++)
           {
               list.Add("S7:[UnnormalConnection]DB30,DINT"+i);
               i += 3;
           }
           //    list.Add("S7:[UnnormalConnection]DB30,DINT0");
           //list.Add("S7:[UnnormalConnection]DB30,DINT4");
           //list.Add("S7:[UnnormalConnection]DB30,DINT8");
           //list.Add("S7:[UnnormalConnection]DB30,DINT12");
           //list.Add("S7:[UnnormalConnection]DB30,DINT16");
           //list.Add("S7:[UnnormalConnection]DB30,DINT20");
           //list.Add("S7:[UnnormalConnection]DB30,DINT24");
           //list.Add("S7:[UnnormalConnection]DB30,DINT28");
           //list.Add("S7:[UnnormalConnection]DB30,DINT32");
           //list.Add("S7:[UnnormalConnection]DB30,DINT36");
           //list.Add("S7:[UnnormalConnection]DB30,DINT40");
           //list.Add("S7:[UnnormalConnection]DB30,DINT44");
           //list.Add("S7:[UnnormalConnection]DB30,DINT48");
           //list.Add("S7:[UnnormalConnection]DB30,DINT52");
           //list.Add("S7:[UnnormalConnection]DB30,DINT56");
           //list.Add("S7:[UnnormalConnection]DB30,DINT60");
           //list.Add("S7:[UnnormalConnection]DB30,DINT64");
           //list.Add("S7:[UnnormalConnection]DB30,DINT68");
           //list.Add("S7:[UnnormalConnection]DB30,DINT72");
           //list.Add("S7:[UnnormalConnection]DB30,DINT76");
           //list.Add("S7:[UnnormalConnection]DB30,DINT80");
           //list.Add("S7:[UnnormalConnection]DB30,DINT84");
           //list.Add("S7:[UnnormalConnection]DB30,DINT88");
           //list.Add("S7:[UnnormalConnection]DB30,DINT92");
           //list.Add("S7:[UnnormalConnection]DB30,DINT96");
           //list.Add("S7:[UnnormalConnection]DB30,DINT100");
           //list.Add("S7:[UnnormalConnection]DB30,DINT104");
           //list.Add("S7:[UnnormalConnection]DB30,DINT108");
           //list.Add("S7:[UnnormalConnection]DB30,DINT112");
           //list.Add("S7:[UnnormalConnection]DB30,DINT116");
           //list.Add("S7:[UnnormalConnection]DB30,DINT120");
           //list.Add("S7:[UnnormalConnection]DB30,DINT124");
           //list.Add("S7:[UnnormalConnection]DB30,DINT128");
           //list.Add("S7:[UnnormalConnection]DB30,DINT132");
           //list.Add("S7:[UnnormalConnection]DB30,DINT136");
           //list.Add("S7:[UnnormalConnection]DB30,DINT140");
           //list.Add("S7:[UnnormalConnection]DB30,DINT144");
           //list.Add("S7:[UnnormalConnection]DB30,DINT148");
           //list.Add("S7:[UnnormalConnection]DB30,DINT152");
           //list.Add("S7:[UnnormalConnection]DB30,DINT156");
           //list.Add("S7:[UnnormalConnection]DB30,DINT160");
           //list.Add("S7:[UnnormalConnection]DB30,DINT164");
           //list.Add("S7:[UnnormalConnection]DB30,DINT168");
           //list.Add("S7:[UnnormalConnection]DB30,DINT172");
           //list.Add("S7:[UnnormalConnection]DB30,DINT176");
           //list.Add("S7:[UnnormalConnection]DB30,DINT180");
           //list.Add("S7:[UnnormalConnection]DB30,DINT184");
           //list.Add("S7:[UnnormalConnection]DB30,DINT188");
           //list.Add("S7:[UnnormalConnection]DB30,DINT192");
           //list.Add("S7:[UnnormalConnection]DB30,DINT196");
           //list.Add("S7:[UnnormalConnection]DB30,DINT200");
           //list.Add("S7:[UnnormalConnection]DB30,DINT204");
           //list.Add("S7:[UnnormalConnection]DB30,DINT208");
           //list.Add("S7:[UnnormalConnection]DB30,DINT212");
           //list.Add("S7:[UnnormalConnection]DB30,DINT216");
           //list.Add("S7:[UnnormalConnection]DB30,DINT220");
           //list.Add("S7:[UnnormalConnection]DB30,DINT224");
           //list.Add("S7:[UnnormalConnection]DB30,DINT228");
           //list.Add("S7:[UnnormalConnection]DB30,DINT232");
           //list.Add("S7:[UnnormalConnection]DB30,DINT236");
           //list.Add("S7:[UnnormalConnection]DB30,DINT240");
           //list.Add("S7:[UnnormalConnection]DB30,DINT244");
           //list.Add("S7:[UnnormalConnection]DB30,DINT248");
           //list.Add("S7:[UnnormalConnection]DB30,DINT252");
           //list.Add("S7:[UnnormalConnection]DB30,DINT256");
           //list.Add("S7:[UnnormalConnection]DB30,DINT260");
           //list.Add("S7:[UnnormalConnection]DB30,DINT264");
           //list.Add("S7:[UnnormalConnection]DB30,DINT268");
           //list.Add("S7:[UnnormalConnection]DB30,DINT272");
           //list.Add("S7:[UnnormalConnection]DB30,DINT276");
           //list.Add("S7:[UnnormalConnection]DB30,DINT280");
           //list.Add("S7:[UnnormalConnection]DB30,DINT284");
           //list.Add("S7:[UnnormalConnection]DB30,DINT288");
           //list.Add("S7:[UnnormalConnection]DB30,DINT292");
           //list.Add("S7:[UnnormalConnection]DB30,DINT296");
           //list.Add("S7:[UnnormalConnection]DB30,DINT300");
           //list.Add("S7:[UnnormalConnection]DB30,DINT304");
           //list.Add("S7:[UnnormalConnection]DB30,DINT308");
           //list.Add("S7:[UnnormalConnection]DB30,DINT312");
           //list.Add("S7:[UnnormalConnection]DB30,DINT316");
           //list.Add("S7:[UnnormalConnection]DB30,DINT320");
           //list.Add("S7:[UnnormalConnection]DB30,DINT324");
           //list.Add("S7:[UnnormalConnection]DB30,DINT328");
           //list.Add("S7:[UnnormalConnection]DB30,DINT332");
           //list.Add("S7:[UnnormalConnection]DB30,DINT336");
           //list.Add("S7:[UnnormalConnection]DB30,DINT340");
           //list.Add("S7:[UnnormalConnection]DB30,DINT344");
           //list.Add("S7:[UnnormalConnection]DB30,DINT348");
           //list.Add("S7:[UnnormalConnection]DB30,DINT352");
           //list.Add("S7:[UnnormalConnection]DB30,DINT356");
           //list.Add("S7:[UnnormalConnection]DB30,DINT360");
           //list.Add("S7:[UnnormalConnection]DB30,DINT364");
           //list.Add("S7:[UnnormalConnection]DB30,DINT368");
           //list.Add("S7:[UnnormalConnection]DB30,DINT372");
           //list.Add("S7:[UnnormalConnection]DB30,DINT376");
           //list.Add("S7:[UnnormalConnection]DB30,DINT380");
           //list.Add("S7:[UnnormalConnection]DB30,DINT384");
           //list.Add("S7:[UnnormalConnection]DB30,DINT388");
           //list.Add("S7:[UnnormalConnection]DB30,DINT392");
           //list.Add("S7:[UnnormalConnection]DB30,DINT396");
           //list.Add("S7:[UnnormalConnection]DB30,DINT400");
           //list.Add("S7:[UnnormalConnection]DB30,DINT404");
           //list.Add("S7:[UnnormalConnection]DB30,DINT408");
           //list.Add("S7:[UnnormalConnection]DB30,DINT412");
           //list.Add("S7:[UnnormalConnection]DB30,DINT416");
           //list.Add("S7:[UnnormalConnection]DB30,DINT420");
           //list.Add("S7:[UnnormalConnection]DB30,DINT424");
           //list.Add("S7:[UnnormalConnection]DB30,DINT428");
           //list.Add("S7:[UnnormalConnection]DB30,DINT432");
           //list.Add("S7:[UnnormalConnection]DB30,DINT436");
           //list.Add("S7:[UnnormalConnection]DB30,DINT440");
           //list.Add("S7:[UnnormalConnection]DB30,DINT444");
           //list.Add("S7:[UnnormalConnection]DB30,DINT448");
           //list.Add("S7:[UnnormalConnection]DB30,DINT452");
           //list.Add("S7:[UnnormalConnection]DB30,DINT456");
           //list.Add("S7:[UnnormalConnection]DB30,DINT460");
           //list.Add("S7:[UnnormalConnection]DB30,DINT464");
           //list.Add("S7:[UnnormalConnection]DB30,DINT468");
           //list.Add("S7:[UnnormalConnection]DB30,DINT472");
           //list.Add("S7:[UnnormalConnection]DB30,DINT476");
           //list.Add("S7:[UnnormalConnection]DB30,DINT480");
           //list.Add("S7:[UnnormalConnection]DB30,DINT484");
           //list.Add("S7:[UnnormalConnection]DB30,DINT488");
           //list.Add("S7:[UnnormalConnection]DB30,DINT492");
           //list.Add("S7:[UnnormalConnection]DB30,DINT496");
           //list.Add("S7:[UnnormalConnection]DB30,DINT500");
           //list.Add("S7:[UnnormalConnection]DB30,DINT504");
           //list.Add("S7:[UnnormalConnection]DB30,DINT508");
           //list.Add("S7:[UnnormalConnection]DB30,DINT512");
           //list.Add("S7:[UnnormalConnection]DB30,DINT516");
           //list.Add("S7:[UnnormalConnection]DB30,DINT520");
           //list.Add("S7:[UnnormalConnection]DB30,DINT524");
           //list.Add("S7:[UnnormalConnection]DB30,DINT528");
           //list.Add("S7:[UnnormalConnection]DB30,DINT532");
           //list.Add("S7:[UnnormalConnection]DB30,DINT536");
           //list.Add("S7:[UnnormalConnection]DB30,DINT540");
           //list.Add("S7:[UnnormalConnection]DB30,DINT544");
           //list.Add("S7:[UnnormalConnection]DB30,DINT548");
           //list.Add("S7:[UnnormalConnection]DB30,DINT552");
           //list.Add("S7:[UnnormalConnection]DB30,DINT556");
           //list.Add("S7:[UnnormalConnection]DB30,DINT560");
           //list.Add("S7:[UnnormalConnection]DB30,DINT564");
           //list.Add("S7:[UnnormalConnection]DB30,DINT568");
           //list.Add("S7:[UnnormalConnection]DB30,DINT572");
           //list.Add("S7:[UnnormalConnection]DB30,DINT576");
           //list.Add("S7:[UnnormalConnection]DB30,DINT580");
           //list.Add("S7:[UnnormalConnection]DB30,DINT584");
           //list.Add("S7:[UnnormalConnection]DB30,DINT588");
           //list.Add("S7:[UnnormalConnection]DB30,DINT592");
           //list.Add("S7:[UnnormalConnection]DB30,DINT596");
           //list.Add("S7:[UnnormalConnection]DB30,DINT600");
           //list.Add("S7:[UnnormalConnection]DB30,DINT604");
           //list.Add("S7:[UnnormalConnection]DB30,DINT608");
           //list.Add("S7:[UnnormalConnection]DB30,DINT612");
           //list.Add("S7:[UnnormalConnection]DB30,DINT616");
           //list.Add("S7:[UnnormalConnection]DB30,DINT620");
           //list.Add("S7:[UnnormalConnection]DB30,DINT624");
           //list.Add("S7:[UnnormalConnection]DB30,DINT628");
           //list.Add("S7:[UnnormalConnection]DB30,DINT632");
           //list.Add("S7:[UnnormalConnection]DB30,DINT636");
           //list.Add("S7:[UnnormalConnection]DB30,DINT640");
           //list.Add("S7:[UnnormalConnection]DB30,DINT644");
           //list.Add("S7:[UnnormalConnection]DB30,DINT648");
           //list.Add("S7:[UnnormalConnection]DB30,DINT652");
           //list.Add("S7:[UnnormalConnection]DB30,DINT656");
           //list.Add("S7:[UnnormalConnection]DB30,DINT660");
           //list.Add("S7:[UnnormalConnection]DB30,DINT664");
           //list.Add("S7:[UnnormalConnection]DB30,DINT668");
           //list.Add("S7:[UnnormalConnection]DB30,DINT672");
           //list.Add("S7:[UnnormalConnection]DB30,DINT676");
           //list.Add("S7:[UnnormalConnection]DB30,DINT680");
           //list.Add("S7:[UnnormalConnection]DB30,DINT684");
           //list.Add("S7:[UnnormalConnection]DB30,DINT688");
           //list.Add("S7:[UnnormalConnection]DB30,DINT692");
           //list.Add("S7:[UnnormalConnection]DB30,DINT696");
           //list.Add("S7:[UnnormalConnection]DB30,DINT700");
           //list.Add("S7:[UnnormalConnection]DB30,DINT704");
           //list.Add("S7:[UnnormalConnection]DB30,DINT708");
           //list.Add("S7:[UnnormalConnection]DB30,DINT712");
           //list.Add("S7:[UnnormalConnection]DB30,DINT716");
           //list.Add("S7:[UnnormalConnection]DB30,DINT720");
           //list.Add("S7:[UnnormalConnection]DB30,DINT724");
           //list.Add("S7:[UnnormalConnection]DB30,DINT728");
           //list.Add("S7:[UnnormalConnection]DB30,DINT732");
           //list.Add("S7:[UnnormalConnection]DB30,DINT736");
           //list.Add("S7:[UnnormalConnection]DB30,DINT740");
           //list.Add("S7:[UnnormalConnection]DB30,DINT744");
           //list.Add("S7:[UnnormalConnection]DB30,DINT748");
           //list.Add("S7:[UnnormalConnection]DB30,DINT752");
           //list.Add("S7:[UnnormalConnection]DB30,DINT756");
           //list.Add("S7:[UnnormalConnection]DB30,DINT760");
           //list.Add("S7:[UnnormalConnection]DB30,DINT764");
           //list.Add("S7:[UnnormalConnection]DB30,DINT768");
           //list.Add("S7:[UnnormalConnection]DB30,DINT772");
           //list.Add("S7:[UnnormalConnection]DB30,DINT776");
           //list.Add("S7:[UnnormalConnection]DB30,DINT780");
           //list.Add("S7:[UnnormalConnection]DB30,DINT784");
           //list.Add("S7:[UnnormalConnection]DB30,DINT788");
           //list.Add("S7:[UnnormalConnection]DB30,DINT792");
           //list.Add("S7:[UnnormalConnection]DB30,DINT796");
           //list.Add("S7:[UnnormalConnection]DB30,DINT800");
           //list.Add("S7:[UnnormalConnection]DB30,DINT804");
           //list.Add("S7:[UnnormalConnection]DB30,DINT808");
           //list.Add("S7:[UnnormalConnection]DB30,DINT812");
           //list.Add("S7:[UnnormalConnection]DB30,DINT816");
           //list.Add("S7:[UnnormalConnection]DB30,DINT820");
           //list.Add("S7:[UnnormalConnection]DB30,DINT824");
           //list.Add("S7:[UnnormalConnection]DB30,DINT828");
           //list.Add("S7:[UnnormalConnection]DB30,DINT832");
           //list.Add("S7:[UnnormalConnection]DB30,DINT836");
           //list.Add("S7:[UnnormalConnection]DB30,DINT840");
           //list.Add("S7:[UnnormalConnection]DB30,DINT844");
           //list.Add("S7:[UnnormalConnection]DB30,DINT848");
           //list.Add("S7:[UnnormalConnection]DB30,DINT852");
           //list.Add("S7:[UnnormalConnection]DB30,DINT856");
           //list.Add("S7:[UnnormalConnection]DB30,DINT860");
           //list.Add("S7:[UnnormalConnection]DB30,DINT864");
           //list.Add("S7:[UnnormalConnection]DB30,DINT868");
           //list.Add("S7:[UnnormalConnection]DB30,DINT872");
           //list.Add("S7:[UnnormalConnection]DB30,DINT876");
           //list.Add("S7:[UnnormalConnection]DB30,DINT880");
           //list.Add("S7:[UnnormalConnection]DB30,DINT884");
           //list.Add("S7:[UnnormalConnection]DB30,DINT888");
           //list.Add("S7:[UnnormalConnection]DB30,DINT892");
           //list.Add("S7:[UnnormalConnection]DB30,DINT896");
           //list.Add("S7:[UnnormalConnection]DB30,DINT900");
   
           return list;
       }

       public static List<string> GetTaskItem1()
       {
           List<string> list = new List<string>();
           for (int i = 0; i <= 900; i++)
           {
               list.Add("S7:[UnnormalConnection]DB31,DINT" + i);
               i += 3;
           }
           //list.Add("S7:[UnnormalConnection]DB31,DINT0");
           //list.Add("S7:[UnnormalConnection]DB31,DINT4");
           //list.Add("S7:[UnnormalConnection]DB31,DINT8");
           //list.Add("S7:[UnnormalConnection]DB31,DINT12");
           //list.Add("S7:[UnnormalConnection]DB31,DINT16");
           //list.Add("S7:[UnnormalConnection]DB31,DINT20");
           //list.Add("S7:[UnnormalConnection]DB31,DINT24");
           //list.Add("S7:[UnnormalConnection]DB31,DINT28");
           //list.Add("S7:[UnnormalConnection]DB31,DINT32");
           //list.Add("S7:[UnnormalConnection]DB31,DINT36");
           //list.Add("S7:[UnnormalConnection]DB31,DINT40");
           //list.Add("S7:[UnnormalConnection]DB31,DINT44");
           //list.Add("S7:[UnnormalConnection]DB31,DINT48");
           //list.Add("S7:[UnnormalConnection]DB31,DINT52");
           //list.Add("S7:[UnnormalConnection]DB31,DINT56");
           //list.Add("S7:[UnnormalConnection]DB31,DINT60");
           //list.Add("S7:[UnnormalConnection]DB31,DINT64");
           //list.Add("S7:[UnnormalConnection]DB31,DINT68");
           //list.Add("S7:[UnnormalConnection]DB31,DINT72");
           //list.Add("S7:[UnnormalConnection]DB31,DINT76");
           //list.Add("S7:[UnnormalConnection]DB31,DINT80");
           //list.Add("S7:[UnnormalConnection]DB31,DINT84");
           //list.Add("S7:[UnnormalConnection]DB31,DINT88");
           //list.Add("S7:[UnnormalConnection]DB31,DINT92");
           //list.Add("S7:[UnnormalConnection]DB31,DINT96");
           //list.Add("S7:[UnnormalConnection]DB31,DINT100");
           //list.Add("S7:[UnnormalConnection]DB31,DINT104");
           //list.Add("S7:[UnnormalConnection]DB31,DINT108");
           //list.Add("S7:[UnnormalConnection]DB31,DINT112");
           //list.Add("S7:[UnnormalConnection]DB31,DINT116");
           //list.Add("S7:[UnnormalConnection]DB31,DINT120");
           //list.Add("S7:[UnnormalConnection]DB31,DINT124");
           //list.Add("S7:[UnnormalConnection]DB31,DINT128");
           //list.Add("S7:[UnnormalConnection]DB31,DINT132");
           //list.Add("S7:[UnnormalConnection]DB31,DINT136");
           //list.Add("S7:[UnnormalConnection]DB31,DINT140");
           //list.Add("S7:[UnnormalConnection]DB31,DINT144");
           //list.Add("S7:[UnnormalConnection]DB31,DINT148");
           //list.Add("S7:[UnnormalConnection]DB31,DINT152");
           //list.Add("S7:[UnnormalConnection]DB31,DINT156");
           //list.Add("S7:[UnnormalConnection]DB31,DINT160");
           //list.Add("S7:[UnnormalConnection]DB31,DINT164");
           //list.Add("S7:[UnnormalConnection]DB31,DINT168");
           //list.Add("S7:[UnnormalConnection]DB31,DINT172");
           //list.Add("S7:[UnnormalConnection]DB31,DINT176");
           //list.Add("S7:[UnnormalConnection]DB31,DINT180");
           //list.Add("S7:[UnnormalConnection]DB31,DINT184");
           //list.Add("S7:[UnnormalConnection]DB31,DINT188");
           //list.Add("S7:[UnnormalConnection]DB31,DINT192");
           //list.Add("S7:[UnnormalConnection]DB31,DINT196");
           //list.Add("S7:[UnnormalConnection]DB31,DINT200");
           //list.Add("S7:[UnnormalConnection]DB31,DINT204");
           //list.Add("S7:[UnnormalConnection]DB31,DINT208");
           //list.Add("S7:[UnnormalConnection]DB31,DINT212");
           //list.Add("S7:[UnnormalConnection]DB31,DINT216");
           //list.Add("S7:[UnnormalConnection]DB31,DINT220");
           //list.Add("S7:[UnnormalConnection]DB31,DINT224");
           //list.Add("S7:[UnnormalConnection]DB31,DINT228");
           //list.Add("S7:[UnnormalConnection]DB31,DINT232");
           //list.Add("S7:[UnnormalConnection]DB31,DINT236");
           //list.Add("S7:[UnnormalConnection]DB31,DINT240");
           //list.Add("S7:[UnnormalConnection]DB31,DINT244");
           //list.Add("S7:[UnnormalConnection]DB31,DINT248");
           //list.Add("S7:[UnnormalConnection]DB31,DINT252");
           //list.Add("S7:[UnnormalConnection]DB31,DINT256");
           //list.Add("S7:[UnnormalConnection]DB31,DINT260");
           //list.Add("S7:[UnnormalConnection]DB31,DINT264");
           //list.Add("S7:[UnnormalConnection]DB31,DINT268");
           //list.Add("S7:[UnnormalConnection]DB31,DINT272");
           //list.Add("S7:[UnnormalConnection]DB31,DINT276");
           //list.Add("S7:[UnnormalConnection]DB31,DINT280");
           //list.Add("S7:[UnnormalConnection]DB31,DINT284");
           //list.Add("S7:[UnnormalConnection]DB31,DINT288");
           //list.Add("S7:[UnnormalConnection]DB31,DINT292");
           //list.Add("S7:[UnnormalConnection]DB31,DINT296");
           //list.Add("S7:[UnnormalConnection]DB31,DINT300");
           //list.Add("S7:[UnnormalConnection]DB31,DINT304");
           //list.Add("S7:[UnnormalConnection]DB31,DINT308");
           //list.Add("S7:[UnnormalConnection]DB31,DINT312");
           //list.Add("S7:[UnnormalConnection]DB31,DINT316");
           //list.Add("S7:[UnnormalConnection]DB31,DINT320");
           //list.Add("S7:[UnnormalConnection]DB31,DINT324");
           //list.Add("S7:[UnnormalConnection]DB31,DINT328");
           //list.Add("S7:[UnnormalConnection]DB31,DINT332");
           //list.Add("S7:[UnnormalConnection]DB31,DINT336");
           //list.Add("S7:[UnnormalConnection]DB31,DINT340");
           //list.Add("S7:[UnnormalConnection]DB31,DINT344");
           //list.Add("S7:[UnnormalConnection]DB31,DINT348");
           //list.Add("S7:[UnnormalConnection]DB31,DINT352");
           //list.Add("S7:[UnnormalConnection]DB31,DINT356");
           //list.Add("S7:[UnnormalConnection]DB31,DINT360");
           //list.Add("S7:[UnnormalConnection]DB31,DINT364");
           //list.Add("S7:[UnnormalConnection]DB31,DINT368");
           //list.Add("S7:[UnnormalConnection]DB31,DINT372");
           //list.Add("S7:[UnnormalConnection]DB31,DINT376");
           //list.Add("S7:[UnnormalConnection]DB31,DINT380");
           //list.Add("S7:[UnnormalConnection]DB31,DINT384");
           //list.Add("S7:[UnnormalConnection]DB31,DINT388");
           //list.Add("S7:[UnnormalConnection]DB31,DINT392");
           //list.Add("S7:[UnnormalConnection]DB31,DINT396");
           //list.Add("S7:[UnnormalConnection]DB31,DINT400");
           //list.Add("S7:[UnnormalConnection]DB31,DINT404");
           //list.Add("S7:[UnnormalConnection]DB31,DINT408");
           //list.Add("S7:[UnnormalConnection]DB31,DINT412");
           //list.Add("S7:[UnnormalConnection]DB31,DINT416");
           //list.Add("S7:[UnnormalConnection]DB31,DINT420");
           //list.Add("S7:[UnnormalConnection]DB31,DINT424");
           //list.Add("S7:[UnnormalConnection]DB31,DINT428");
           //list.Add("S7:[UnnormalConnection]DB31,DINT432");
           //list.Add("S7:[UnnormalConnection]DB31,DINT436");
           //list.Add("S7:[UnnormalConnection]DB31,DINT440");
           //list.Add("S7:[UnnormalConnection]DB31,DINT444");
           //list.Add("S7:[UnnormalConnection]DB31,DINT448");
           //list.Add("S7:[UnnormalConnection]DB31,DINT452");
           //list.Add("S7:[UnnormalConnection]DB31,DINT456");
           //list.Add("S7:[UnnormalConnection]DB31,DINT460");
           //list.Add("S7:[UnnormalConnection]DB31,DINT464");
           //list.Add("S7:[UnnormalConnection]DB31,DINT468");
           //list.Add("S7:[UnnormalConnection]DB31,DINT472");
           //list.Add("S7:[UnnormalConnection]DB31,DINT476");
           //list.Add("S7:[UnnormalConnection]DB31,DINT480");
           //list.Add("S7:[UnnormalConnection]DB31,DINT484");
           //list.Add("S7:[UnnormalConnection]DB31,DINT488");
           //list.Add("S7:[UnnormalConnection]DB31,DINT492");
           //list.Add("S7:[UnnormalConnection]DB31,DINT496");
           //list.Add("S7:[UnnormalConnection]DB31,DINT500");
           //list.Add("S7:[UnnormalConnection]DB31,DINT504");
           //list.Add("S7:[UnnormalConnection]DB31,DINT508");
           //list.Add("S7:[UnnormalConnection]DB31,DINT512");
           //list.Add("S7:[UnnormalConnection]DB31,DINT516");
           //list.Add("S7:[UnnormalConnection]DB31,DINT520");
           //list.Add("S7:[UnnormalConnection]DB31,DINT524");
           //list.Add("S7:[UnnormalConnection]DB31,DINT528");
           //list.Add("S7:[UnnormalConnection]DB31,DINT532");
           //list.Add("S7:[UnnormalConnection]DB31,DINT536");
           //list.Add("S7:[UnnormalConnection]DB31,DINT540");
           //list.Add("S7:[UnnormalConnection]DB31,DINT544");
           //list.Add("S7:[UnnormalConnection]DB31,DINT548");
           //list.Add("S7:[UnnormalConnection]DB31,DINT552");
           //list.Add("S7:[UnnormalConnection]DB31,DINT556");
           //list.Add("S7:[UnnormalConnection]DB31,DINT560");
           //list.Add("S7:[UnnormalConnection]DB31,DINT564");
           //list.Add("S7:[UnnormalConnection]DB31,DINT568");
           //list.Add("S7:[UnnormalConnection]DB31,DINT572");
           //list.Add("S7:[UnnormalConnection]DB31,DINT576");
           //list.Add("S7:[UnnormalConnection]DB31,DINT580");
           //list.Add("S7:[UnnormalConnection]DB31,DINT584");
           //list.Add("S7:[UnnormalConnection]DB31,DINT588");
           //list.Add("S7:[UnnormalConnection]DB31,DINT592");
           //list.Add("S7:[UnnormalConnection]DB31,DINT596");
           //list.Add("S7:[UnnormalConnection]DB31,DINT600");
           //list.Add("S7:[UnnormalConnection]DB31,DINT604");
           //list.Add("S7:[UnnormalConnection]DB31,DINT608");
           //list.Add("S7:[UnnormalConnection]DB31,DINT612");
           //list.Add("S7:[UnnormalConnection]DB31,DINT616");
           //list.Add("S7:[UnnormalConnection]DB31,DINT620");
           //list.Add("S7:[UnnormalConnection]DB31,DINT624");
           //list.Add("S7:[UnnormalConnection]DB31,DINT628");
           //list.Add("S7:[UnnormalConnection]DB31,DINT632");
           //list.Add("S7:[UnnormalConnection]DB31,DINT636");
           //list.Add("S7:[UnnormalConnection]DB31,DINT640");
           //list.Add("S7:[UnnormalConnection]DB31,DINT644");
           //list.Add("S7:[UnnormalConnection]DB31,DINT648");
           //list.Add("S7:[UnnormalConnection]DB31,DINT652");
           //list.Add("S7:[UnnormalConnection]DB31,DINT656");
           //list.Add("S7:[UnnormalConnection]DB31,DINT660");
           //list.Add("S7:[UnnormalConnection]DB31,DINT664");
           //list.Add("S7:[UnnormalConnection]DB31,DINT668");
           //list.Add("S7:[UnnormalConnection]DB31,DINT672");
           //list.Add("S7:[UnnormalConnection]DB31,DINT676");
           //list.Add("S7:[UnnormalConnection]DB31,DINT680");
           //list.Add("S7:[UnnormalConnection]DB31,DINT684");
           //list.Add("S7:[UnnormalConnection]DB31,DINT688");
           //list.Add("S7:[UnnormalConnection]DB31,DINT692");
           //list.Add("S7:[UnnormalConnection]DB31,DINT696");
           //list.Add("S7:[UnnormalConnection]DB31,DINT700");
           //list.Add("S7:[UnnormalConnection]DB31,DINT704");
           //list.Add("S7:[UnnormalConnection]DB31,DINT708");
           //list.Add("S7:[UnnormalConnection]DB31,DINT712");
           //list.Add("S7:[UnnormalConnection]DB31,DINT716");
           //list.Add("S7:[UnnormalConnection]DB31,DINT720");
           //list.Add("S7:[UnnormalConnection]DB31,DINT724");
           //list.Add("S7:[UnnormalConnection]DB31,DINT728");
           //list.Add("S7:[UnnormalConnection]DB31,DINT732");
           //list.Add("S7:[UnnormalConnection]DB31,DINT736");
           //list.Add("S7:[UnnormalConnection]DB31,DINT740");
           //list.Add("S7:[UnnormalConnection]DB31,DINT744");
           //list.Add("S7:[UnnormalConnection]DB31,DINT748");
           //list.Add("S7:[UnnormalConnection]DB31,DINT752");
           //list.Add("S7:[UnnormalConnection]DB31,DINT756");
           //list.Add("S7:[UnnormalConnection]DB31,DINT760");
           //list.Add("S7:[UnnormalConnection]DB31,DINT764");
           //list.Add("S7:[UnnormalConnection]DB31,DINT768");
           //list.Add("S7:[UnnormalConnection]DB31,DINT772");
           //list.Add("S7:[UnnormalConnection]DB31,DINT776");
           //list.Add("S7:[UnnormalConnection]DB31,DINT780");
           //list.Add("S7:[UnnormalConnection]DB31,DINT784");
           //list.Add("S7:[UnnormalConnection]DB31,DINT788");
           //list.Add("S7:[UnnormalConnection]DB31,DINT792");
           //list.Add("S7:[UnnormalConnection]DB31,DINT796");
           //list.Add("S7:[UnnormalConnection]DB31,DINT800");
           //list.Add("S7:[UnnormalConnection]DB31,DINT804");
           //list.Add("S7:[UnnormalConnection]DB31,DINT808");
           //list.Add("S7:[UnnormalConnection]DB31,DINT812");
           //list.Add("S7:[UnnormalConnection]DB31,DINT816");
           //list.Add("S7:[UnnormalConnection]DB31,DINT820");
           //list.Add("S7:[UnnormalConnection]DB31,DINT824");
           //list.Add("S7:[UnnormalConnection]DB31,DINT828");
           //list.Add("S7:[UnnormalConnection]DB31,DINT832");
           //list.Add("S7:[UnnormalConnection]DB31,DINT836");
           //list.Add("S7:[UnnormalConnection]DB31,DINT840");
           //list.Add("S7:[UnnormalConnection]DB31,DINT844");
           //list.Add("S7:[UnnormalConnection]DB31,DINT848");
           //list.Add("S7:[UnnormalConnection]DB31,DINT852");
           //list.Add("S7:[UnnormalConnection]DB31,DINT856");
           //list.Add("S7:[UnnormalConnection]DB31,DINT860");
           //list.Add("S7:[UnnormalConnection]DB31,DINT864");
           //list.Add("S7:[UnnormalConnection]DB31,DINT868");
           //list.Add("S7:[UnnormalConnection]DB31,DINT872");
           //list.Add("S7:[UnnormalConnection]DB31,DINT876");
           //list.Add("S7:[UnnormalConnection]DB31,DINT880");
           //list.Add("S7:[UnnormalConnection]DB31,DINT884");
           //list.Add("S7:[UnnormalConnection]DB31,DINT888");
           //list.Add("S7:[UnnormalConnection]DB31,DINT892");
           //list.Add("S7:[UnnormalConnection]DB31,DINT896");
           //list.Add("S7:[UnnormalConnection]DB31,DINT900");
          
           return list;
       }

       public static List<string> GetTaskItem2()
       {
           List<string> list = new List<string>();
           for (var i = 0; i < 800; i++)
           {
               list.Add("S7:[UnnormalConnection]DB400,DINT"+i);
               i += 3;
           }
          
           return list;
       }
       public static List<string> GetTaskItem3()
       {
           List<string> list = new List<string>();

           for (int i = 0; i < 6000; i++)
           {
               list.Add("S7:[UnnormalConnection]DB401,byte" + i);
           }

           

           return list;
       }
    }
}
