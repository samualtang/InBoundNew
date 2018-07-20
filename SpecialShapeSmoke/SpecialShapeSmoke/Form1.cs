using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;

using System.Xml;
using InBound;

namespace SpecialShapeSmoke
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label2.BackColor = Color.LightCyan;
        }

        public void CreateXML()
        {
                //创建XmlDocument对象
                XmlDocument xmlDoc = new XmlDocument();
                //XML的声明<?xml version="1.0" encoding="gb2312"?> 
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                 //追加xmldecl位置
                xmlDoc.AppendChild(xmlSM);
                //添加一个名为HunHeDate的根节点
                XmlElement xml = xmlDoc.CreateElement("1", "HunHeDate", "");
                //追加HunHeDate的根节点位置
                xmlDoc.AppendChild(xml);
                //添加另一个节点,与HunHeDate所匹配，查找<HunHeDate>
                XmlNode HunHeDate = xmlDoc.SelectSingleNode("HunHeDate");
                
                

                //添加一个名为<DateList>的节点   
                XmlElement DateList = xmlDoc.CreateElement("DateList");
                int PokeNum = 0;
                //查询出1001的所有pokeid;
                string[] it = null;
                using (Entities et = new Entities())
                {
                    var query = et.T_UN_POKE.Where(x=>x.MACHINESEQ==1001).OrderBy(x=>x.SORTNUM).Select(x => x.POKEID).ToList();
                    int i = 0;
                    it=new string[query.Count];
                    foreach (var item in query)
                    {
                        it[i] = item.ToString();
                        i++;
                    }
                    PokeNum = query.Count;

                }
                DateList.SetAttribute("CreateTime", DateTime.Now.ToString());
              
                //为<DateList>节点的属性
                DateList.SetAttribute("PokeNum", PokeNum.ToString());
                for (int i = 0; i < PokeNum; i++)
                {
                    XmlElement PokeSort = xmlDoc.CreateElement("PokeSort"+(i+1)); 
                    DateList.AppendChild(PokeSort);

                    XmlElement Info1 = xmlDoc.CreateElement("Info1");
                    Info1.InnerText = it[i];
                    PokeSort.AppendChild(Info1);

                    XmlElement Info2 = xmlDoc.CreateElement("Info2");
                    Info2.InnerText = it[i];
                    PokeSort.AppendChild(Info2);

                } 


                HunHeDate.AppendChild(DateList);//添加到<Gen>节点中   
                //保存好创建的XML文档
                xmlDoc.Save("HunHeDate.XML");
 
        } 
        private void button1_Click(object sender, EventArgs e)
        {
             
        }
    }
}
