

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;


namespace InBound
{
    /// <summary>
    ///     通用类型扩展方法类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败返回类型的默认值 </returns>
        public static T CastTo<T>(this object value)
        {
            object result;
            Type type = typeof (T);
            try
            {
                if (type.IsEnum)
                {
                    result = Enum.Parse(type, value.ToString());
                }
                else if (type == typeof (Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = System.Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = default(T);
            }

            return (T)result;
        }

        /// <summary>
        ///     把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            object result;
            Type type = typeof (T);
            try
            {
                result = type.IsEnum ? Enum.Parse(type, value.ToString()) : System.Convert.ChangeType(value, type);
            }
            catch
            {
                result = defaultValue;
            }
            return (T)result;
        }

        /// <summary>
        /// 双缓冲，解决闪烁问题  
        /// </summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="flag">默认True</param>
        public static void DoubleBufferedDataGirdView(this DataGridView dgv, bool flag)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, flag, null);
        }

        /// <summary>  
        /// 双缓冲，解决闪烁问题  
        /// </summary>  
        /// <param name="lv"></param>  
        /// <param name="flag"></param>  
        public static void DoubleBufferedListView(this  ListView lv, bool flag)
        {
            Type lvType = lv.GetType();
            PropertyInfo pi = lvType.GetProperty("DoubleBuffered",  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(lv, flag, null);
        }

    }
}