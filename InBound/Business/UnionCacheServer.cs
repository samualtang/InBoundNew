﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using InBound.Model;

using System.Transactions;

namespace InBound.Business
{
    public class UnionCacheServer
    {
        public static List<UnionCaChe> GetAllData()
        {
            using (Entities et = new Entities())
            {
                var query = et.T_PRODUCE_CACHE.Select(x => new UnionCaChe { id = x.ID, mainbelt = x.MAILBELT, groupno = x.GROUPNO, cachesize = x.CACHESIZE, dispatchenum = x.DISPATCHENUM, dispatchesize = x.DISPATCHESIZE, state = x.STATE }).ToList();

                return query;
            }
        }

        public static List<UnionCaChe> GetSearchData(decimal? mainbelt, decimal? groupno)
        {
            using (Entities et = new Entities())
            {
                if (mainbelt == 0 && groupno == 0)
                {
                    return et.T_PRODUCE_CACHE.Select(x => new UnionCaChe { id = x.ID, mainbelt = x.MAILBELT, groupno = x.GROUPNO, cachesize = x.CACHESIZE, dispatchenum = x.DISPATCHENUM, dispatchesize = x.DISPATCHESIZE, state = x.STATE }).ToList();
                }
                else if (mainbelt == 0)
                {
                    return et.T_PRODUCE_CACHE.Where(y => y.GROUPNO == groupno).Select(x => new UnionCaChe { id = x.ID, mainbelt = x.MAILBELT, groupno = x.GROUPNO, cachesize = x.CACHESIZE, dispatchenum = x.DISPATCHENUM, dispatchesize = x.DISPATCHESIZE, state = x.STATE }).ToList();
                }
                else if (groupno == 0)
                {
                    return et.T_PRODUCE_CACHE.Where(y => y.MAILBELT == mainbelt).Select(x => new UnionCaChe { id = x.ID, mainbelt = x.MAILBELT, groupno = x.GROUPNO, cachesize = x.CACHESIZE, dispatchenum = x.DISPATCHENUM, dispatchesize = x.DISPATCHESIZE, state = x.STATE }).ToList();
                }
                else
                {
                    return et.T_PRODUCE_CACHE.Where(y => y.MAILBELT == mainbelt && y.GROUPNO == groupno).Select(x => new UnionCaChe { id = x.ID, mainbelt = x.MAILBELT, groupno = x.GROUPNO, cachesize = x.CACHESIZE, dispatchenum = x.DISPATCHENUM, dispatchesize = x.DISPATCHESIZE, state = x.STATE }).ToList();
                }

            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateData(UnionCaChe data)
        {
            using (Entities et = new Entities())
            {
                try
                {
                    var query = et.T_PRODUCE_CACHE.Where(y => y.MAILBELT == data.mainbelt && y.GROUPNO == data.groupno).Select(x => x).SingleOrDefault();

                    query.CACHESIZE = data.cachesize;
                    query.DISPATCHESIZE = data.dispatchesize;
                    query.DISPATCHENUM = data.dispatchenum;
                    query.STATE = data.state;

                    int i = et.SaveChanges();
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        /// <summary>
        /// 缓存上限增
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool dateplup(decimal num)
        {
            using (Entities et = new Entities())
            {
                try
                {
                    var query = et.T_PRODUCE_CACHE.Select(x => x).ToList();

                    foreach (var item in query)
                    {
                        item.CACHESIZE += num;
                    }
                    if (et.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return true;
                }  
            }
        }
        /// <summary>
        /// 缓存上限减
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool datepldown(decimal num)
        {
            using (Entities et = new Entities())
            {
                try
                {
                    var query = et.T_PRODUCE_CACHE.Select(x => x).ToList();

                    foreach (var item in query)
                    {
                        item.CACHESIZE -= num;
                    }
                    if (et.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 缓存空余
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool up_dispatchenum(decimal num)
        {
            using (Entities et = new Entities())
            {
                try
                {
                    var query = et.T_PRODUCE_CACHE.Select(x => x).ToList();

                    foreach (var item in query)
                    {
                        item.DISPATCHENUM = num;
                    }
                    if (et.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 缓存空余
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool up_dispatchesize(decimal num)
        {
            using (Entities et = new Entities())
            {
                try
                {
                    var query = et.T_PRODUCE_CACHE.Select(x => x).ToList();

                    foreach (var item in query)
                    {
                        item.DISPATCHESIZE = num;
                    }
                    if (et.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }






    }
}
