using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound
{
    public class UnionBit
    {
        public static List<OperationChar> Union(string oldVal, string newVal, out string compStrs)
        {
            List<OperationChar> list = new List<OperationChar>();
            list = Compent(oldVal, newVal);
            var compStr = string.Empty;
            list.ForEach(f => compStr += f.val);
            compStrs = compStr;
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        /// <returns></returns>
        private static List<OperationChar> Compent(string oldVal, string newVal)
        {
            if (string.IsNullOrWhiteSpace(newVal))
            {
                throw new Exception("设备报警数据错误,设备报警信息不能为空！");
            }

            List<OperationChar> list = new List<OperationChar>();
            OperationChar obj;

            if (string.IsNullOrWhiteSpace(oldVal))
            {
                for (int i = 0; i < newVal.Length; i++)
                {
                    obj = new OperationChar
                    {
                        bit = i,
                        op = newVal[i].ToString() == "0" ? Oper.None : Oper.Add,
                        val = newVal[i].ToString()
                    };
                    list.Add(obj);
                }
                return list;
            }

            int spilt = oldVal.Length - newVal.Length;
            if (spilt > 0)
            {
                for (int i = 0; i < newVal.Length; i++)
                {
                    obj = new OperationChar();
                    obj.bit = i;
                    if (oldVal[i].ToString() != newVal[i].ToString())
                    {
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.Update;
                    }
                    else
                    {
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.None;
                    }
                    list.Add(obj);
                }
                var skip = oldVal.Skip(newVal.Length).ToList();
                for (int i = 0; i < skip.Count; i++)
                {
                    if (skip[i].ToString() == "1")
                    {
                        obj = new OperationChar
                        {
                            val = "0",
                            bit = newVal.Length + i,
                            op = Oper.Update
                        };
                        list.Add(obj);
                    }
                    else
                    {
                        obj = new OperationChar
                        {
                            val = oldVal[newVal.Length + i].ToString(),
                            bit = newVal.Length + i,
                            op = Oper.None
                        };
                        list.Add(obj);
                    }
                }
            }
            else if (spilt < 0)
            {
                for (int i = 0; i < oldVal.Length; i++)
                {
                    obj = new OperationChar();
                    obj.bit = i;
                    if (oldVal[i].ToString() != newVal[i].ToString())
                    {

                        //comp += newVal[i].ToString();
                        obj.val = newVal[i].ToString();
                        obj.op = Oper.Update;
                    }
                    else
                    {
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.None;
                    }
                    list.Add(obj);
                }
                var skip = newVal.Skip(oldVal.Length).ToList();
                for (int i = 0; i < skip.Count; i++)
                {
                    if (skip[i].ToString() == "1")
                    {
                        obj = new OperationChar
                        {
                            val = "1",
                            bit = oldVal.Length + i,
                            op = Oper.Add
                        };
                        list.Add(obj);
                    }
                    else
                    {
                        obj = new OperationChar
                        {
                            val = newVal[oldVal.Length + i].ToString(),
                            bit = newVal.Length + i,
                            op = Oper.None
                        };
                        list.Add(obj);
                    }
                }
            }
            else
            {
                for (int i = 0; i < oldVal.Length; i++)
                {
                    obj = new OperationChar();
                    obj.bit = i;
                    if (oldVal[i].ToString() != newVal[i].ToString())
                    {

                        //comp += newVal[i].ToString();
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.Update;
                    }
                    else
                    {
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.None;
                    }
                    list.Add(obj);
                }
            }
            return list;
        }

    }

    public class OperationChar
    {
        public Oper op { get; set; }
        public int bit { get; set; }
        public string val { get; set; }
    }

    public enum Oper
    {
        /// <summary>
        /// 不变化
        /// </summary>
        None,
        /// <summary>
        /// 更新报警状态
        /// </summary>
        Update,
        /// <summary>
        /// 新增报警信息
        /// </summary>
        Add
    }
}
