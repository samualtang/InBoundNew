using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace InBound.Pub
{
    public class DataCopy
    {
        /// <summary>
        /// 使用反射将一个对象的值赋值给另一个对象
        /// </summary>
        /// <param name="obj">原对象</param>
        /// <param name="newobj">新的对象</param>
        /// <returns></returns>
        public static T CopyToT<T>(object obj, object newobj)
        {
            T t = default(T);
            if (newobj == null)
            {
                return t;
            }
            t = (T)newobj;
            if (obj == null)
            {
                return t;
            }

            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();//得到原对象所有属性
            Type ty = t.GetType();
            if (propertyInfos.Length < 0)
            {
                return t;
            }
            foreach (PropertyInfo pi in propertyInfos)//循环对象属性
            {
                string name = pi.Name;
                object value = pi.GetValue(obj, null);
                BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;//忽略属性名称大小写
                var p = ty.GetProperty(name, flag);//根据原对象属性名称得到新对象属性
                if (p != null)
                {
                    p.SetValue(t, value, null);//赋值
                }
            }
            return t;
        }

        public static T_PACKAGE_TASK CopyToT<T_PACKAGE_TASK>(T_PACKAGE_TASK obj, T_PACKAGE_TASK newobj)
        {
            T_PACKAGE_TASK t = default(T_PACKAGE_TASK);
            if (newobj == null)
            {
                return t;
            }
            t = (T_PACKAGE_TASK)newobj;
            if (obj == null)
            {
                return t;
            }

            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();//得到原对象所有属性
            Type ty = t.GetType();
            if (propertyInfos.Length < 0)
            {
                return t;
            }
            foreach (PropertyInfo pi in propertyInfos)//循环对象属性
            {
                string name = pi.Name;
                if (name != "EntityKey" && name != "EntityState")
                {
                    object value = pi.GetValue(obj, null);
                    BindingFlags flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;//忽略属性名称大小写
                    var p = ty.GetProperty(name, flag);//根据原对象属性名称得到新对象属性
                    if (p != null)
                    {
                        p.SetValue(t, value, null);//赋值
                    }
                } 
            }
            return t;
        }
    }
}
