using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
 public  class AtsCellInfoService:BaseService
{
     
      public static void InsertAtsCellInfo(T_WMS_ATSCELLINFO info)
      {
          using(Entities data=new Entities())
          {
                 decimal id = 0;
                 id = data.ExecuteStoreQuery<decimal>("select S_t_wms_atscellinfo.nextval from dual").First();
                 info.ID = id;
                 data.AddToT_WMS_ATSCELLINFO(info);
                 data.SaveChanges();
              
          }
      }
      public static void InsertAtsCellInfo(T_WMS_ATSCELLINFO info,Entities data)
      {
          
              decimal id = 0;
              id = data.ExecuteStoreQuery<decimal>("select S_t_wms_atscellinfo.nextval from dual").First();
              info.ID = id;
              data.AddToT_WMS_ATSCELLINFO(info);
              //data.SaveChanges();

          
      }
      public static T_WMS_ATSCELLINFO GetCellInfo(string cellno)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ATSCELLINFO where item.CELLNO == cellno select item).FirstOrDefault();
              return query;
          }
      }
      public static T_WMS_ATSCELLINFO GetCellInfoByBarCode(string barcode)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ATSCELLINFO where item.PALLETNO == barcode select item).FirstOrDefault();
              return query;
          }
      }
      public static T_WMS_ATSCELLINFO GetCellInfo(string cellno, Entities entity)
      {
          
              var query = (from item in entity.T_WMS_ATSCELLINFO where item.CELLNO == cellno select item).FirstOrDefault();
              return query;
          
      }
      public static List<T_WMS_ATSCELLINFO_DETAIL> GetAllUnFullPallet()
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ATSCELLINFO
                           join item2 in entity.T_WMS_ATSCELL
                           on item.CELLNO equals item2.CELLNO
                           join item3 in entity.T_WMS_ATSCELLINFO_DETAIL
                           on item.CELLNO equals item3.CELLNO
                           join item4 in entity.T_WMS_LANEWAY
                           on item2.LANEWAYNO equals item4.LANEWAYNO
                           join item5 in entity.T_PRODUCE_SORTTROUGH
                           on item3.CIGARETTECODE equals item5.CIGARETTECODE
                           where item.STATUS == 30 && item2.WORKSTATUS == 10 && (item2.STATUS == 10 || item2.STATUS == 20)
                            && (item4.STATUS == 10 || item4.STATUS == 20) && item5.TROUGHTYPE == 10 && item5.CIGARETTETYPE == 20 &&  item5.STATE == "10" && item3.QTY != item5.BOXCOUNT
                           select item3).Distinct().ToList();
              return query;
          }
      }

      public static List<T_WMS_ATSCELLINFO_DETAIL> GetDetail(String code,decimal qty)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ATSCELLINFO
                           join item2 in entity.T_WMS_ATSCELL
                           on item.CELLNO equals item2.CELLNO
                           join item3 in entity.T_WMS_ATSCELLINFO_DETAIL
                           on item.CELLNO equals item3.CELLNO
                           join item4 in entity.T_WMS_LANEWAY
                           on item2.LANEWAYNO equals item4.LANEWAYNO
                           join item5 in entity.T_PRODUCE_SORTTROUGH
                           on item3.CIGARETTECODE equals item5.CIGARETTECODE
                           where item.STATUS == 30 && item2.WORKSTATUS == 20 && (item2.STATUS == 10 || item2.STATUS == 20)
                            && (item4.STATUS == 10 || item4.STATUS == 20) && item5.TROUGHTYPE == 10 && item5.CIGARETTETYPE == 20 && item5.STATE == "10" && item3.QTY == qty && item5.CIGARETTECODE==code
                           select item3).Distinct().ToList();
              return query;
          }
      }
      public static T_WMS_ATSCELLINFO CheckPalletExist(String palletNo)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ATSCELLINFO where item.PALLETNO == palletNo select item).FirstOrDefault();
              return query;
          }
      }
      public static void delete(String cellno)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ATSCELLINFO where item.CELLNO == cellno select item).FirstOrDefault();
              if (query != null)
              {
                  entity.T_WMS_ATSCELLINFO.DeleteObject(query);
                  WriteLog.GetLog().Write("删除储位T_WMS_ATSCELLINFO信息:" + query.CELLNO);
              }
              entity.SaveChanges();
             
          }
      }
      public static void delete(String cellno, Entities entity)
      {
       
              var query = (from item in entity.T_WMS_ATSCELLINFO where item.CELLNO == cellno select item).FirstOrDefault();
              if (query != null)
              {
                  entity.T_WMS_ATSCELLINFO.DeleteObject(query);
              }
             
      }
}
}
