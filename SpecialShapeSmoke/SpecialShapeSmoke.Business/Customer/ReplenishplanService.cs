using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialShapeSmoke.Business.Customer
{
   public class ReplenishplanService
    {
        
        public List<HUNHEVIEW> GetBeginTask(int troughNum,int number)
        {
            DataEntities data = new DataEntities();
            var query = from item in data.HUNHEVIEW where  item.MACHINESEQ == troughNum && item.STATUS<18  orderby item.POKEID ascending select item;
            return query.ToList().Take(number).ToList();
        }

        public List<HUNHEVIEW> GetBeginTask(int troughNum)
        {
            DataEntities data = new DataEntities();
            var query = from item in data.HUNHEVIEW where item.MACHINESEQ == troughNum && item.STATUS < 18 orderby item.POKEID ascending select item;
            return query.ToList();
        }
        public List<HUNHEVIEW> GetBeginTask()
        {
            DataEntities data = new DataEntities();
            var query = from item in data.HUNHEVIEW where item.STATUS < 18 orderby item.POKEID ascending select item;
            return query.ToList();
        }
        public List<HUNHEVIEW> GetALLTask(int pageSize,int pageIndex,out int totalCount)
        {
            DataEntities data = new DataEntities();
            var query = from item in data.HUNHEVIEW orderby item.POKEID  select item;
            totalCount = query.Count();
            return query.Skip(pageSize*(pageIndex-1)).Take(pageSize).ToList();
        }
        public int GetFinishCount()
        {
            DataEntities data = new DataEntities();
            var query = from item in data.HUNHEVIEW  where item.STATUS>=18  select item;
           int totalCount = query.Count();
           return totalCount + 1;
        }
        public List<HUNHEVIEW> GetALLTask(int num, int pageSize, int pageIndex, out int totalCount)
        {
            DataEntities data = new DataEntities();
            var query = from item in data.HUNHEVIEW where item.MACHINESEQ==num orderby item.POKEID ascending select item;
            totalCount = query.Count();
            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
        }
    }
}
