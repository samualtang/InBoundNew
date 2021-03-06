﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;
using System.Configuration;

namespace InBound.Business
{
    public class ProduceCacheService
    {

        public static T_PRODUCE_CACHE GetCache(decimal groupno, int mainbelt)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_CACHE
                             where item.GROUPNO == groupno && item.MAILBELT == mainbelt
                                 && item.STATE == 10
                             select item).FirstOrDefault();
                return query;
            }
        }


        public static T_UN_CACHE GetUnCache(decimal packagemachine)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_UN_CACHE
                             where item.PACKAGEMACHINE == packagemachine
                                 && item.STATE == 10
                             select item).FirstOrDefault();
                return query;
            }
        }
    }
}
