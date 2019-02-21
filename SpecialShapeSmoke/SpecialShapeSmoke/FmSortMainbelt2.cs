using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Model;
using SpecialShapeSmoke.Model;
using InBound;
using OpcRcw.Da;
using InBound.Business;

namespace SpecialShapeSmoke
{
    public partial class FmSortMainbelt2 : Form
    {
        bool plcstatetag = false;
        Group ShapeGroup;
        //传入的分拣线编号
        string linenum;
        Type svrComponenttyp;
        public FmSortMainbelt2(bool plcstatetag, IOPCServer pIOPCServer, int LOCALE_ID, string SERVER_NAME, string linenum)
        {
            this.plcstatetag = plcstatetag;
            this.linenum = linenum;
            if (plcstatetag)
            {
                svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);

                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);

                ShapeGroup = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);
                ShapeGroup.addItem(ItemCollection_new.GetSortBeltByShapeItem(linenum));
            }    
            InitializeComponent();

            timer1.Interval = 8000;
            timer1.Start();
           
        }
        //7个plc读取的数据
        int[] finishNo = new int[7];
        //第1个是未推烟品牌包号 接下来4个是已推烟皮带包号 第6个是与异型烟合流包号 第7个是与异型烟合流数量
        decimal[] dbIndex = new decimal[] { 1, 10, 19, 28, 37, 45, 46 };//出烟隔板顺序 5、4、3、2

        public void DataGetByPLC()
        {
            //finishNo[0] = 561367;
            //finishNo[1] = 561338;
            //finishNo[2] = 561335;
            //finishNo[3] = 561323;
            //finishNo[4] = 561322;
            //finishNo[5] = 0;
            //finishNo[6] = 0;

            if (plcstatetag)
            {
                finishNo[0] = ShapeGroup.ReadD((int)dbIndex[0]).CastTo<int>(-1);
                finishNo[1] = ShapeGroup.ReadD((int)dbIndex[1]).CastTo<int>(-1);
                finishNo[2] = ShapeGroup.ReadD((int)dbIndex[2]).CastTo<int>(-1);
                finishNo[3] = ShapeGroup.ReadD((int)dbIndex[3]).CastTo<int>(-1);
                finishNo[4] = ShapeGroup.ReadD((int)dbIndex[4]).CastTo<int>(-1);
                finishNo[5] = ShapeGroup.ReadD((int)dbIndex[5]).CastTo<int>(-1);
                finishNo[6] = ShapeGroup.ReadD((int)dbIndex[6]).CastTo<int>(-1);
            }
            else
            {
                label1.Text = "未能读取到plc数据，请检查网络连接！";
            }
        }
        //单条烟信息
        SPSortBeltInfo info = new SPSortBeltInfo();
        //一个隔板信息
        List<SPSortBeltInfo> infos = new List<SPSortBeltInfo>();
        //5个隔板信息
        List<SPSortBeltInfo>[] infolist = new List<SPSortBeltInfo>[5];
        
        private void button_refash_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            //获取数据库内容 （任务包号、分拣任务号、条烟名称、条烟编码、户名、专卖证号、户序、包装机） 
            //未推烟皮带内容 倒序判断 
            //button数绑定
            DataBind();

            timer1.Start();
            //MessageBox.Show(finishNo[0].ToString() + "\r\n" + finishNo[1].ToString() + "\r\n" + finishNo[2].ToString() + "\r\n" + finishNo[3].ToString() + "\r\n" + finishNo[4].ToString() + "\r\n" + finishNo[5].ToString() + "\r\n" + finishNo[6].ToString());
        }
        /// <summary>
        /// 按钮数据绑定
        /// </summary>
        public void DataBind()
        {
            //读取plc
            DataGetByPLC();
            //定义1个泛型集合 内有5个集合 每个集合内
            for (int index = 0; index < 5; index++)
            {
                infolist[index] = InBound.Business.HunHeService_new.GetCigBeltSort(finishNo[index]);
            }

            if (linenum == "3")
            {
                label7.Text = "<一一一一一一一";
            }
            GroupBox[] box = new GroupBox[] { groupBox1, groupBox2, groupBox3, groupBox4, groupBox5 };
            Button[] btn = new Button[] { 
                button1, button2, button3, button4, button5, button6, 
                button7, button8, button9, button10, button11, button12, 
                button13, button14, button15, button16, button17, button18, 
                button19, button20, button21, button22, button23, button24, 
                button25, button26, button27, button28, button29, button30};
            foreach (var item in btn)
            {
                item.Visible = false;
            }
            int i = 0;
            foreach (var item in infolist)
            {
                if (infolist[i].Count > 0)
                {
                    //包号绑定
                    if (infolist[i][0].SENDTASKNUM != 0 && infolist[i][0].SENDTASKNUM != null)
                    {
                        box[i].Text = infolist[i][0].SENDTASKNUM.ToString();
                    }
                    int j = 0;
                    //条烟名称绑定
                    while (j < infolist[i].Count() && j < 6)
                    {
                        btn[i * 6 + 6 - j - 1].Text = ChangeStr(infolist[i][j].CIGARETTENAME);//ChangeStr(infolist[i][infolist[i].Count() - 1 - j].CIGARETTENAME);
                        btn[i * 6 + 6 - j - 1].Visible = true;
                        j++; 
                    }
                }
                else
                {
                    box[i].Text = "0";
                }

                i++;
            }
            
            //根据加载的数据绑定对应的按钮
             
        }
        /// <summary>
        /// 转换括号为横向
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ChangeStr(string str)
        {
            string result = str.Replace("（", "︹");
            result = result.Replace("）", "︺");
            result = result.Replace("(", "︹");
            result = result.Replace(")", "︺");
            return result;
        }
         
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            decimal? sendtasknum =Convert.ToDecimal( btn.Parent.Text.ToString());
            int index = Convert.ToInt32(btn.Parent.Name.ToString().Remove(0,8));
            dataGridView1.DataSource = infolist[index - 1].Where(x => x.SENDTASKNUM == sendtasknum).ToList();
        }

        private void FmSortMainbelt1_Deactivate(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {  
            DataBind();
        }
    }
}
