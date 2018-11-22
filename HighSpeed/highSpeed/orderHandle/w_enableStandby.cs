using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;
using InBound;
using highSpeed.PubFunc;
using System.Data.OracleClient;
using System.Threading;

namespace highSpeed.orderHandle
{
    public partial class w_enableStandby : Form
    {
        LoadDataHandler loadData;

        public w_enableStandby()
        {
            InitializeComponent();
            loadData = new LoadDataHandler();
            
        }
        PublicFun pf = new PublicFun(Application.StartupPath + "\\intfile.ini");
        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
          
            //loadData.LoadDatas<List<T_PRODUCE_SORTTROUGH>>(() =>
            //{
            //    var list = SortTroughService.GetTrough(10, 20).GroupBy(g => g.GROUPNO).Select(s => new T_PRODUCE_SORTTROUGH { GROUPNO = s.Key.Value }).OrderBy(x=>x.GROUPNO).ToList();
            //    return list;
            //}, (list) =>
            //{
            //    cmb_GroupList.DataSource = list;
            //    cmb_GroupList.DisplayMember = "GROUPNO";
            //    cmb_GroupList.ValueMember = "GROUPNO";
            //}, (exceptionInfo) =>
            //{
            //    MessageBox.Show(exceptionInfo);
            //});
            
            groupnoBox.SelectedIndex = 0;
        }

        private void cmb_GroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            T_PRODUCE_SORTTROUGH obj = cmb_GroupList.SelectedItem as T_PRODUCE_SORTTROUGH;
            loadData.LoadDatas<List<T_PRODUCE_SORTTROUGH>>(() =>
            {
                var list = SortTroughService.GetTrough(10, 20).Where(w => w.GROUPNO == obj.GROUPNO).OrderBy(ord => Decimal.Parse(ord.TROUGHNUM.Trim())).ToList();
                return list;
            }, (list) =>
            {
                cbsource.DataSource = list;
                cbsource.DisplayMember = "TROUGHNUM";
                cbsource.ValueMember = "TROUGHNUM";

                var chagelist = SortTroughService.GetBackTrough(10, 20).Where(w => w.GROUPNO == obj.GROUPNO).OrderBy(ord => Decimal.Parse(ord.TROUGHNUM.Trim())).ToList();
                cbStandby.DataSource = chagelist;
                cbStandby.DisplayMember = "TROUGHNUM";
                cbStandby.ValueMember = "TROUGHNUM";

            }, (exceptionInfo) =>
            {
                MessageBox.Show(exceptionInfo);
            });
        }

        private void cbsource_SelectedIndexChanged(object sender, EventArgs e)
        {
            T_PRODUCE_SORTTROUGH selectedItem = cbsource.SelectedItem as T_PRODUCE_SORTTROUGH;
            cbciagrettcode.Text = selectedItem.CIGARETTENAME;
        }


        private void btnEnableStandby_Click(object sender, EventArgs e)
        {
            T_PRODUCE_SORTTROUGH selectedSourceItem, selectedStandbyItem;
            selectedSourceItem = cbsource.SelectedItem as T_PRODUCE_SORTTROUGH;
            selectedStandbyItem = cbStandby.SelectedItem as T_PRODUCE_SORTTROUGH;


            if (selectedSourceItem == null)
            {
                MessageBox.Show("请选择源通道！");
                return;
            }
            if (selectedStandbyItem == null)
            {
                MessageBox.Show("请选择备用通道！");
                return;
            }
            if (!string.IsNullOrWhiteSpace(selectedStandbyItem.CIGARETTECODE))
            {
                MessageBox.Show(string.Format("该通道已经设置了{0}品牌！", selectedStandbyItem.CIGARETTENAME));
                return;
            }
            try
            {
                //获取该通道已经分拣完成的任务
                ProducePokeService.FetchTaskByTroughNo(selectedSourceItem.TROUGHNUM, selectedStandbyItem.TROUGHNUM);
                MessageBox.Show("更换成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region  换烟仓
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox3.Items.Clear();
            if (groupnoBox.SelectedIndex == 0)
            {
                groupno.Text = "1";
            }
            else if (groupnoBox.SelectedIndex == 1)
            {
                groupno.Text = "2";
            } 
            decimal groupnum = Convert.ToDecimal(groupno.Text);
            List<string> machineseqs = new List<string>();
            using (Entities et = new Entities())
            {
                var lists = et.T_PRODUCE_SORTTROUGH.Where(x => x.GROUPNO == groupnum && x.CIGARETTETYPE == 30 && x.STATE == "10").Select(x => new { x.MACHINESEQ , x.CIGARETTENAME ,x.CIGARETTECODE}).OrderBy(x=>x.MACHINESEQ).ToList();
                foreach (var item in lists)
                {
                    comboBox1.Items.Add(item.MACHINESEQ);
                    comboBox3.Items.Add(item.MACHINESEQ); 
                }
            }
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = -1;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal groupnum = Convert.ToDecimal(groupno.Text);
            //原通道 品牌 编码
            decimal MACHINESEQ_old = 0;
            string TROUGHNUM_old = "";
            string CIGARETTENAME_old = "";
            string CIGARETTECODE_old = "";
            try
            {
                MACHINESEQ_old = Convert.ToDecimal(comboBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("请选择正确的原通道");
                return;
            } 
            
            using (Entities et = new Entities())
            {
                var lists = et.T_PRODUCE_SORTTROUGH.Where(x => x.GROUPNO == groupnum && x.CIGARETTETYPE == 30 && x.STATE == "10").Select(x => new { x.MACHINESEQ, x.CIGARETTENAME,x.CIGARETTECODE,x.TROUGHNUM}).OrderBy(x => x.MACHINESEQ).ToList();
                foreach (var item in lists)
                {
                    if (comboBox1.Text == item.MACHINESEQ.ToString())
                    {
                        CIGARETTENAME_old = item.CIGARETTENAME;
                        CIGARETTECODE_old = item.CIGARETTECODE;
                        TROUGHNUM_old = item.TROUGHNUM;
                    }
                }
            }
            //目标通道 品牌 编码 
            string CIGARETTENAME_new = "";
            string CIGARETTECODE_new = "";
            decimal MACHINESEQ_new = 0;
            string TROUGHNUM_new = "";
            try
            {
                MACHINESEQ_new = Convert.ToDecimal(comboBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("请选择正确的目标通道");
                return;
            }  
            using (Entities et = new Entities())
            {
                var lists = et.T_PRODUCE_SORTTROUGH.Where(x => x.GROUPNO == groupnum && x.CIGARETTETYPE == 30 && x.STATE == "10").Select(x => new { x.MACHINESEQ, x.CIGARETTENAME, x.CIGARETTECODE,x.TROUGHNUM }).OrderBy(x => x.MACHINESEQ).ToList();
                foreach (var item in lists)
                {
                    if (comboBox3.Text == item.MACHINESEQ.ToString())
                    {
                        CIGARETTENAME_new = item.CIGARETTENAME;
                        CIGARETTECODE_new = item.CIGARETTECODE;
                        TROUGHNUM_new=item.TROUGHNUM;
                    }
                }
            }

            DialogResult result =  MessageBox.Show(MACHINESEQ_old+"-"+CIGARETTENAME_old+"-"+CIGARETTECODE_old+"\r与\r"+MACHINESEQ_new+"-"+CIGARETTENAME_new+"-"+CIGARETTECODE_new+"\r互换,并修改分拣数据？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result==DialogResult.OK)
            {
                //通道表换品牌  poke表换通道号
                using (Entities et=new Entities())
                {
                    var oldtrough  = et.T_PRODUCE_SORTTROUGH.Where(x => x.MACHINESEQ == MACHINESEQ_old).FirstOrDefault(); 
                    var newtrough  = et.T_PRODUCE_SORTTROUGH.Where(x => x.MACHINESEQ == MACHINESEQ_new).FirstOrDefault();
                     
                    oldtrough.CIGARETTECODE = CIGARETTECODE_new;
                    oldtrough.CIGARETTENAME = CIGARETTENAME_new;

                    newtrough.CIGARETTECODE = CIGARETTECODE_old;
                    newtrough.CIGARETTENAME = CIGARETTENAME_old;
                     
                    var poketrough1 = et.T_UN_POKE.Where(x => x.CIGARETTECODE == CIGARETTECODE_old).ToList();
                    if (poketrough1.Count > 0)
                    {
                        foreach (var item in poketrough1)
                        {
                            item.MACHINESEQ = MACHINESEQ_new;
                            item.TROUGHNUM = TROUGHNUM_new;
                        }
                    }

                    var poketrough2 = et.T_UN_POKE.Where(x => x.CIGARETTECODE == CIGARETTECODE_new).ToList();
                    if (poketrough2.Count > 0)
                    {
                        foreach (var item in poketrough2)
                        {
                            item.MACHINESEQ = MACHINESEQ_old;
                            item.TROUGHNUM = TROUGHNUM_old;
                        }
                    }
                    if (et.SaveChanges()>0)
                    {
                        MessageBox.Show("换道成功，请检查通道设置并校验分拣数据！");
                        label10.Text = "";
                        label11.Text = ""; 

                    }
                    else
                    {
                        MessageBox.Show("换道失败！");
                    }
                } 
            } 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal groupnum = Convert.ToDecimal(groupno.Text);
            using (Entities et = new Entities())
            {
                var lists = et.T_PRODUCE_SORTTROUGH.Where(x => x.GROUPNO == groupnum && x.CIGARETTETYPE == 30 && x.STATE == "10").Select(x => new { x.MACHINESEQ, x.CIGARETTENAME }).OrderBy(x => x.MACHINESEQ).ToList();
                foreach (var item in lists)
                {
                    if (comboBox1.Text == item.MACHINESEQ.ToString())
                    {
                        label10.Text = item.CIGARETTENAME;
                    }
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal groupnum = Convert.ToDecimal(groupno.Text);
            using (Entities et = new Entities())
            {
                var lists = et.T_PRODUCE_SORTTROUGH.Where(x => x.GROUPNO == groupnum && x.CIGARETTETYPE == 30 && x.STATE == "10").Select(x => new { x.MACHINESEQ, x.CIGARETTENAME }).OrderBy(x => x.MACHINESEQ).ToList();
                foreach (var item in lists)
                {
                    if (comboBox3.Text == item.MACHINESEQ.ToString())
                    {
                        label11.Text = item.CIGARETTENAME;
                    }
                }
            }
        }
        #endregion

        #region  烟柜换烟仓
        private void button2_Click(object sender, EventArgs e)
        {
           DialogResult dia = MessageBox.Show("更换烟柜：\r"+comboBox_yg.Text+" "+lab1.Text+"\r到"+comboBox_yc1.Text+"和"+comboBox_yc2.Text
               ,"提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
           if (dia==DialogResult.OK)
           {
               try
               {
                   decimal machine =Convert.ToDecimal(comboBox_yg.Text);
                   decimal machine1 =Convert.ToDecimal(comboBox_yc1.Text);
                   decimal machine2 =Convert.ToDecimal(comboBox_yc2.Text);
                   
                   //禁用烟柜 添加烟柜品牌入1，2线烟仓
                   using (Entities et=new Entities())
                   {
                       var ygdata = et.T_PRODUCE_SORTTROUGH.Where(x => x.GROUPNO == 3 && x.MACHINESEQ == machine).FirstOrDefault();
                       var yc1data = et.T_PRODUCE_SORTTROUGH.Where(x => x.GROUPNO == 1 && x.MACHINESEQ == machine1).FirstOrDefault();
                       var yc2data = et.T_PRODUCE_SORTTROUGH.Where(x => x.GROUPNO == 2 && x.MACHINESEQ == machine2).FirstOrDefault();
                       var alldata1 = et.T_UN_POKE.Where(x => x.MACHINESEQ == machine &&x.LINENUM =="1" ).ToList();
                       var alldata2 = et.T_UN_POKE.Where(x => x.MACHINESEQ == machine &&x.LINENUM =="2" ).ToList();

                       ygdata.STATE = "0";
                       yc1data.CIGARETTECODE = ygdata.CIGARETTECODE;
                       yc1data.CIGARETTENAME = ygdata.CIGARETTENAME;

                       yc2data.CIGARETTECODE = ygdata.CIGARETTECODE;
                       yc2data.CIGARETTENAME = ygdata.CIGARETTENAME;

                       //修改poke表 CTYPE=1 TROUGHNUM MACHINESEQ
                       foreach (var item in alldata1)
                       {
                           item.CTYPE = 1;
                           item.MACHINESEQ = yc1data.MACHINESEQ;
                           item.TROUGHNUM = yc1data.TROUGHTYPE.ToString(); 
                       }
                       foreach (var item in alldata2)
                       {
                           item.CTYPE = 1;
                           item.MACHINESEQ = yc2data.MACHINESEQ;
                           item.TROUGHNUM = yc2data.TROUGHTYPE.ToString();
                       }
                       if (et.SaveChanges() > 0)
                       {
                           MessageBox.Show("换道完成，请检验数据与通道设置！");
                       }
                       else
                       {
                           MessageBox.Show("换道失败！");
                       } 
                   }
               }
               catch (Exception)
               {
                   MessageBox.Show("数据操作失败！");
               }
           } 
        }

        private void w_enableStandby_Load(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < 4; i++)
                {
                    LsitMainSN.Add(0);
                }
                System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false; 
                using (Entities et = new Entities())
                {
                    var result = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.GROUPNO == 3).Select(x => new { x.MACHINESEQ, x.CIGARETTENAME, x.CIGARETTECODE }).OrderBy(x=>x.MACHINESEQ).ToList();
                    foreach (var item in result)
                    {
                        comboBox_yg.Items.Add(item.MACHINESEQ);
                    }
                    comboBox_yg.SelectedIndex = 0; 
               
                    var result1 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.GROUPNO == 1).Select(x => new { x.MACHINESEQ, x.CIGARETTENAME, x.CIGARETTECODE }).OrderBy(x => x.MACHINESEQ).ToList();
                    foreach (var item in result1)
                    {
                        comboBox_yc1.Items.Add(item.MACHINESEQ);
                    }
                    comboBox_yc1.SelectedIndex = -1;
                
                    var result2 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.GROUPNO == 2).Select(x => new { x.MACHINESEQ, x.CIGARETTENAME, x.CIGARETTECODE }).OrderBy(x => x.MACHINESEQ).ToList();
                    foreach (var item in result2)
                    {
                        comboBox_yc2.Items.Add(item.MACHINESEQ);
                    }
                    comboBox_yc2.SelectedIndex = -1;
                


                    //var result3 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 20 && x.TROUGHTYPE == 10).GroupBy(x => x.GROUPNO).OrderBy(x=>x.Key).ToList();
                    //foreach (var item in result3)
                    //{
                    //    comboBox_group.Items.Add(item.Key);
                    //}
                    //comboBox_group.SelectedIndex = 0 ;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据加载失败");
            }
        }

        private void comboBox_yg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (Entities et = new Entities())
                {
                    var result = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.GROUPNO == 3).Select(x => new { x.MACHINESEQ, x.CIGARETTENAME, x.CIGARETTECODE }).ToList();

                    foreach (var item in result)
                    {
                        if (item.MACHINESEQ.ToString()==comboBox_yg.Text)
                        {
                            lab1.Text = item.CIGARETTENAME;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据加载失败");
            }
           
        }

        private void comboBox_yc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (Entities et = new Entities())
                {
                    var result = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.GROUPNO == 1).Select(x => new { x.MACHINESEQ, x.CIGARETTENAME, x.CIGARETTECODE }).ToList();

                    foreach (var item in result)
                    {
                        if (item.MACHINESEQ.ToString() == comboBox_yc1.Text)
                        {
                            lab2.Text = item.CIGARETTENAME;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据加载失败");
            }
        }

        private void comboBox_yc2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (Entities et = new Entities())
                {
                    var result = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.GROUPNO == 2).Select(x => new { x.MACHINESEQ, x.CIGARETTENAME, x.CIGARETTECODE }).ToList();

                    foreach (var item in result)
                    {
                        if (item.MACHINESEQ.ToString() == comboBox_yc2.Text)
                        {
                            lab3.Text = item.CIGARETTENAME;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据加载失败");
            }
        }






        private void button2_Click_1(object sender, EventArgs e)
        {
            int num1 = -1;
            int num2 = -1;
            if (comboBox_yc1.Text=="")
	        {
		        MessageBox.Show("请选择1线目标烟仓！");
                return;
	        }
            if (comboBox_yc2.Text=="")
	        {
		        MessageBox.Show("请选择2线目标烟仓！");
                return;
	        }
            try
            {
                decimal machine1 = Convert.ToDecimal(comboBox_yc1.Text);
                decimal machine2 = Convert.ToDecimal(comboBox_yc2.Text);
                using (Entities et=new Entities())
                {
                   num1 = et.T_UN_POKE.Where(x => x.MACHINESEQ == machine1  ).Where(x => x.STATUS != 20).Count();
                   num2 = et.T_UN_POKE.Where(x => x.MACHINESEQ == machine1  ).Where(x => x.STATUS != 20).Count();
                }
                if (num1 > 0)
                {
                    MessageBox.Show("目标1线烟仓还有未完成分拣任务！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (num2 > 0)
                {
                    MessageBox.Show("目标2线烟仓还有未完成分拣任务！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (num1 == 0 && num2 == 0)
                {
                    MessageBox.Show("目标烟仓无未完成任务，可以更换");
                }
            }
            catch (Exception)
            {
                if (num1 == -1 || num2 == -1)
                {
                    MessageBox.Show("请选择目标烟仓！");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            if (comboBox3.Text == "")
            {
                MessageBox.Show("请选择目标烟仓！");
                return;
            }        
            try
            {
                int num;
                decimal machine = Convert.ToDecimal(comboBox3.Text); 
                using (Entities et = new Entities())
                {
                    num = et.T_UN_POKE.Where(x => x.MACHINESEQ == machine).Where(x => x.STATUS != 20).Count(); 
                }
                if (num > 0)
                {
                    MessageBox.Show("目标烟仓还有未完成分拣任务！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                }
                else
                {
                    MessageBox.Show("目标烟仓无未完成任务，可以更换");
                } 
            }
            catch (Exception)
            {  
                    MessageBox.Show("验证失败！"); 
            }
        }
        DataBase Db = new DataBase();
        delegate void HandleSort(); 
        private void button5_Click(object sender, EventArgs e)
        { 
            HandleSort task1 = datacheck; //新的
            task1.BeginInvoke(null, null);
        } 
        private void datacheck()
        {
            Db.Open();
            OracleParameter[] sqlpara;
            sqlpara = new OracleParameter[2];

            sqlpara[0] = new OracleParameter("p_ErrCode", OracleType.VarChar, 1000);
            sqlpara[1] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 2000);

            sqlpara[0].Direction = ParameterDirection.Output;
            sqlpara[1].Direction = ParameterDirection.Output;

            Db.ExecuteNonQueryWithProc("p_produce_schedulevalidation", sqlpara);

            string errcode = sqlpara[0].Value == null ? "" : sqlpara[0].Value.ToString();
            string errmsg = sqlpara[1].Value == null ? "" : sqlpara[1].Value.ToString();
            Db.Close();
           
            MessageBox.Show(errmsg); 
        }
        private void troughcheck()
        {
            Db.Open();
            OracleParameter[] sqlpara;
            sqlpara = new OracleParameter[2];

            sqlpara[0] = new OracleParameter("p_ErrCode", OracleType.VarChar, 1000);
            sqlpara[1] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 2000);

            sqlpara[0].Direction = ParameterDirection.Output;
            sqlpara[1].Direction = ParameterDirection.Output;

            Db.ExecuteNonQueryWithProc("p_produce_validation", sqlpara);

            string errcode = sqlpara[0].Value == null ? "" : sqlpara[0].Value.ToString();
            string errmsg = sqlpara[1].Value == null ? "" : sqlpara[1].Value.ToString();
            Db.Close();
            MessageBox.Show(errmsg); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            troughcheck();
        }

        #endregion


        #region  常规烟烟柜
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                decimal txt = Convert.ToDecimal(comboBox_group.Text).CastTo(0);
                cmbTagYG.Items.Clear();
                cmbSoucreYG.Items.Clear();
                using (Entities et = new Entities())
                {
                    var result4 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 20 && x.TROUGHTYPE == 10 && x.GROUPNO == txt).Select(x => new { x.MACHINESEQ, x.TROUGHNUM, x.GROUPNO,x.STATE }).OrderBy(x => x.MACHINESEQ).ToList();
                    foreach (var item in result4)
                    {
                        cmbTagYG.Items.Add(item.TROUGHNUM);
                        if (item.STATE=="0")
                        {
                            cmbSoucreYG.Items.Add(item.TROUGHNUM);
                        } 
                    }
                    cmbTagYG.SelectedIndex = -1;
                    cmbSoucreYG.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTagYG.SelectedIndex>-1)
            {
                try
                {
                    string txt = cmbTagYG.Text;
                    using (Entities et = new Entities())
                    {
                        var result4 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 20 && x.TROUGHTYPE == 10 && x.TROUGHNUM == txt).Select(x => new { x.MACHINESEQ, x.TROUGHNUM, x.CIGARETTENAME }).FirstOrDefault();
                        label22.Text = result4.CIGARETTENAME;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                } 
            } 
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSoucreYG.SelectedIndex > -1)
            {
                try
                {
                    string txt = cmbSoucreYG.Text;
                    using (Entities et = new Entities())
                    {
                        var result4 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 20 && x.TROUGHTYPE == 10 && x.TROUGHNUM == txt).Select(x => new { x.MACHINESEQ, x.TROUGHNUM, x.CIGARETTENAME }).FirstOrDefault();
                        label18.Text = result4.CIGARETTENAME;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            } 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox_group.SelectedIndex > -1 && cmbSoucreYG.SelectedIndex > -1 && cmbTagGroup.SelectedIndex > -1 && cmbTagYG.SelectedIndex > -1)
            {
                DialogResult re = MessageBox.Show("请确认：分拣组" + comboBox_group.Text + "\r" + cmbSoucreYG.Text + "" + label18.Text + "\r与\r" + cmbTagYG.Text + "\r" + label22.Text + "\r换道？", "提示",  
                                                        MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                        MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                        MessageBoxDefaultButton.Button2);//定义对话框的按钮式样);
                if (re == DialogResult.Yes)
                {
                    button7.Enabled = false;
                    try
                    {
                        ProducePokeService.FetchPokeTroughByTroughNo(cmbSoucreYG.Text, cmbTagYG.Text);
                        pf.IniWriteValue("分拣换柜", "isTrue", "4"); //写入4时,为换道成功
                        WriteLog.GetLog().Write("写入4时,为换道成功,通道" + cmbSoucreYG.Text + "与" + cmbTagYG.Text + "交换");
                        MessageBox.Show("换道成功，请检查数据");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("换道失败，请重试");
                    }
                    finally
                    {
                        button7.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("请把烟柜填写完整!");
            }
            //if (cmbSoucreYG.Text.Length <= 0)
            //{
            //    MessageBox.Show("请选择原通道");
            //    return;
            //}
            //if (cmbTagYG.Text.Length <= 0)
            //{
            //    MessageBox.Show("请选择目标通道");
            //    return;
            //}

            //DialogResult re = MessageBox.Show("请确认：分拣组" + comboBox_group.Text + "\r" + cmbSoucreYG.Text + "烟柜\r与\r" + cmbTagYG.Text + "烟柜\r换道？", "提示" ,  
            //                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
            //                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
            //                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样);
            //if (re == DialogResult.OK)
            //{
            //    try
            //    {
            //        ProducePokeService.FetchPokeByTroughNo(cmbSoucreYG.Text, cmbTagYG.Text);
            //        MessageBox.Show("换道成功，请检查数据");
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("换道失败，请重试");
            //    }

            //}
        }




        #endregion

        private void button6_Click(object sender, EventArgs e)
        {

            Db.Open();
            OracleParameter[] sqlpara;
            sqlpara = new OracleParameter[2];

            sqlpara[0] = new OracleParameter("p_ErrCode", OracleType.VarChar, 1000);
            sqlpara[1] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 2000);

            sqlpara[0].Direction = ParameterDirection.Output;
            sqlpara[1].Direction = ParameterDirection.Output;

            Db.ExecuteNonQueryWithProc("p_produce_wms_sorttrough", sqlpara);

            string errcode = sqlpara[0].Value == null ? "" : sqlpara[0].Value.ToString();
            string errmsg = sqlpara[1].Value == null ? "" : sqlpara[1].Value.ToString();
            Db.Close();
            if (errcode == "1")
            {
                MessageBox.Show("数据已同步");
                pf.IniWriteValue("分拣换柜", "isTrue", "3");//写入3时,表示同步烟柜已经完成
                WriteLog.GetLog().Write("写入3,表示同步烟柜已经完成");
            }
            else
            {
                MessageBox.Show(errmsg);
            }

        }
        List<decimal> LsitMainSN = new List<decimal>();
       
        private void btnSorntum_Click(object sender, EventArgs e)
        {
             
            try
            {
                btnSorntum.Enabled = false;
                if (pf.IniReadValue("分拣换柜", "flag") == "1")//从本地文件获取任务号
                {
                   
                    for (int i = 0; i < 4; i++)
                    {
                        LsitMainSN[i] = Convert.ToDecimal(pf.IniReadValue("分拣换柜", " maxSortNum" + (i + 1)));//保存的任务号
                    }
                    WriteLog.GetLog().Write("从本地文件获取任务号\r\n1号主皮带任务号:" + txtSortnum1.Text + "\r\n2号主皮带任务号:" + txtSortnum2.Text + "\r\n3号主皮带任务号:" + txtSortnum3.Text + "\r\n4号主皮带任务号:" + txtSortnum4.Text);
                    if (pf.IniReadValue("分拣换柜", "isTrue") == "0")//当更新换柜完成之后 重置本地存放任务号
                    {
                        pf.IniWriteValue("分拣换柜", "flag", "0");//重置获取标志
                    }
                 
                }
                else
                {
                    LsitMainSN = ProducePokeService.GetSortnumByNotCalcu();//获取最大的没有发送且已计算的任务号
                    pf.IniWriteValue("分拣换柜", "maxSortNum1", LsitMainSN[0].ToString());
                    pf.IniWriteValue("分拣换柜", "maxSortNum2", LsitMainSN[1].ToString());
                    pf.IniWriteValue("分拣换柜", "maxSortNum3", LsitMainSN[2].ToString());
                    pf.IniWriteValue("分拣换柜", "maxSortNum4", LsitMainSN[3].ToString());
                    pf.IniWriteValue("分拣换柜", "isTrue", "1");//写入1时,为第一次获取
                    pf.IniWriteValue("分拣换柜", "flag", "1");//写入1时,为第一次获取
                    WriteLog.GetLog().Write("获取最大的没有发送且已计算的任务号\r\n1号主皮带任务号:" + txtSortnum1.Text + "\r\n2号主皮带任务号:" + txtSortnum2.Text + "\r\n3号主皮带任务号:" + txtSortnum3.Text + "\r\n4号主皮带任务号:" + txtSortnum4.Text);
                    WriteLog.GetLog().Write("写入1 ,为第一次获取最大的没有发送且已计算的任务号");
                }
                txtSortnum1.Text = LsitMainSN[0].ToString();//一号主皮带任务号
                txtSortnum2.Text = LsitMainSN[1].ToString();//二号主皮带任务号
                txtSortnum3.Text = LsitMainSN[2].ToString();//三号主皮带任务号
                txtSortnum4.Text = LsitMainSN[3].ToString();//四号主皮带任务号
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取失败,请查看日志");
                WriteLog.GetLog().Write("自动获取任务失败,错误记录:" + ex.Message);
            }
            finally
            {

                btnSorntum.Enabled = true;
            }
        }

        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string throwString ="";
            try
            {
                if (!string.IsNullOrWhiteSpace(txtSortnum1.Text) && !string.IsNullOrWhiteSpace(txtSortnum2.Text) && !string.IsNullOrWhiteSpace(txtSortnum3.Text) && !string.IsNullOrWhiteSpace(txtSortnum4.Text))
                {
                    DialogResult MsgBoxResult = MessageBox.Show("确定要更新任务?",//对话框的显示内容 
                                                            "操作提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                    if (MsgBoxResult == DialogResult.Yes)
                    {
                        decimal status = 0;
                        btnUpdate.Enabled = false;
                        if (LsitMainSN.Sum() == 0)
                        {
                            MessageBox.Show("请自动获取任务号");
                            btnUpdate.Enabled = true;
                            return;
                        }
                        if (rbNew.Checked)//新增
                        {
                            status = 10;
                            for (int i = 0; i < 4; i++)
                            {
                                LsitMainSN[i] = Convert.ToDecimal(pf.IniReadValue("分拣换柜", " maxSortNum" + (i + 1)));//保存的任务号
                            }
                            if (pf.IniReadValue("分拣换柜", "isTrue") == "4")//当完成烟柜转移备用烟柜的时,才清除本地任务号,增添容错率
                            {
                                txtSortnum1.Clear(); txtSortnum2.Clear(); txtSortnum3.Clear(); txtSortnum4.Clear();
                                //新增是最后一步 ,将清掉数据
                                pf.IniWriteValue("分拣换柜", "maxSortNum1", "0");//写入0时,为第二次更新最大任务号后面的为10 更新成功 最后一步
                                pf.IniWriteValue("分拣换柜", "maxSortNum2", "0");
                                pf.IniWriteValue("分拣换柜", "maxSortNum3", "0");
                                pf.IniWriteValue("分拣换柜", "maxSortNum4", "0");
                                pf.IniWriteValue("分拣换柜", "isTrue", "0");
                                pf.IniWriteValue("分拣换柜", "flag", "0");
                                btnReTiaoyan.Visible = true;
                                WriteLog.GetLog().Write("写入0,为第二次更新最大任务号后面的为10 更新成功,条烟顺序重新生成");
                            }
                        }
                        else if (rbEnd.Checked)//完成
                        {
                            status = 20;
                            pf.IniWriteValue("分拣换柜", "isTrue", "2"); //写入2时,为第一次更新最大任务号后面的为20 更新成功 
                            WriteLog.GetLog().Write("写入2时,为第一次更新最大任务号后面的为20 更新成功");
                        }
                        else
                        {
                            MessageBox.Show("请先选择任务状态! ");
                            return;
                        }
                        foreach (var item in LsitMainSN)//判断任务号是否有效
                        {
                            if (item > 0)
                            {
                                if (!ProducePokeService.GetExistSortnum(item))
                                {
                                    MessageBox.Show("更新失败,无效的任务号:" + item);
                                    WriteLog.GetLog().Write("无效的任务号:" + item);
                                    return;
                                }
                            }
                        }
                        throwString = "更新完成\r\n1号主皮带任务号:" + txtSortnum1.Text +
                                                    "\r\n2号主皮带任务号:" + txtSortnum2.Text +
                                                    "\r\n3号主皮带任务号:" + txtSortnum3.Text +
                                                    "\r\n4号主皮带任务号:" + txtSortnum4.Text +
                                                    "\r\n状态更新为" + status;
                        label25.Visible = true;
                        myThread my = new myThread(LsitMainSN, status, throwString, label25, btnUpdate);
                        Thread t = new Thread(new ThreadStart(my.ThreadToUpdate));
                        t.Start();
                        
                        //ThreadToUpdate(LsitMainSN, status, throwString);
                    }

                }
                else
                {
                    MessageBox.Show("请输入任务号");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新失败,未知异常,请查看日志");
                WriteLog.GetLog().Write(throwString + "更新失败," +
                                            "\r\n异常:" + ex.Message);
            }
          

        }

        ToolTip p = new ToolTip();
    
        private void btnTips_Click(object sender, EventArgs e)
        {
             
           p.SetToolTip(btnTips, "帮助");
         
        }

        private void comboBox_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSoucreYG.Items.Clear();
            cmbBind(comboBox_group, cmbSoucreYG);
          
        }
        void cmbBind(ComboBox source, ComboBox tag)
        {
            switch (source.SelectedIndex)
            {
                case 0:
                    ProducePokeService.GetYGtroughnum(1, 2).ForEach(f => tag.Items.Add(f));
                    break;
                case 1:
                   ProducePokeService.GetYGtroughnum(3, 4).ForEach(f => tag.Items.Add( f) );
                    break;
                case 2:
                    ProducePokeService.GetYGtroughnum(5, 6).ForEach(f => tag.Items.Add(f));
                    break;
                case 3:
                    ProducePokeService.GetYGtroughnum(7, 8).ForEach(f => tag.Items.Add(f));
                    break;

            }
        }
        private void cmbTagGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTagYG.Items.Clear();
            cmbBind(cmbTagGroup, cmbTagYG);
        }

        private void btnSorntum_MouseEnter(object sender, EventArgs e)
        {
            p.SetToolTip(btnSorntum,"自动获取每根主皮带上已经发送任务号的最大任务号\r\n并且存入本地\r\n");
        }

        private void txtSortnum1_TextChanged(object sender, EventArgs e)
        {

            //TextBox txt = ((TextBox)sender);//获取选中输入框的对象
            //int index = Convert.ToInt32(txt.Name.Substring(txt.Name.Length - 1)) - 1;//获取索引
            //LsitMainSN[index] = Convert.ToDecimal(txt.Text);

        }
        delegate void HandleSortPokeseq();
        private void btnReTiaoyan_Click(object sender, EventArgs e)
        {
            if (LsitMainSN.Sum() == 0)
            {
                MessageBox.Show("请自动获取任务号");
                return;
            }
            WriteLog.GetLog().Write("进行条烟顺序生成");
            btnReTiaoyan.Enabled = false;
            button7.Enabled = false;
            btnUpdate.Enabled = false;
            panel3.Visible = true;
            HandleSortPokeseq task = ThreadSortPokeseq;
            task.BeginInvoke(null, null);
        }
        void ThreadSortPokeseq()
        {
            try
            {
                ProducePokeService.RefSortByTiaoyan(LsitMainSN);
                UnionTaskInfoService.InsertPokeseqInfo();
                panel3.Visible = false;
                MessageBox.Show("条烟顺序生成成功！");
            }
            catch (DataException date)
            {
                MessageBox.Show("条烟顺序失败：" + date.Message);
                label24.Text = "条烟顺序失败：" + date.Message;
            }
            catch (Exception ex)
            {
                MessageBox.Show("条烟顺序失败：" + ex.Message);
                label24.Text = "条烟顺序失败：" + ex.Message;
            }
            finally
            {
                btnReTiaoyan.Enabled = true;
                button7.Enabled = true;
                btnUpdate.Enabled = true;
                LsitMainSN.Clear();
                WriteLog.GetLog().Write("条烟顺序生成结束");
            }


        }
     
    }

    public class myThread //用于线程处理
    {
        public List<decimal> List;
        public decimal Status;
        public string Info;
        public Label Lbl;
        public Button Btn;
        public myThread()
        {

        }
        public myThread(List<decimal> list, decimal status, string info, Label lbl, Button btn)//构造函数传参
        {
            List = list;
            Status = status;
            Info = info;
            Lbl = lbl;
            Btn = btn;

        }
        public void ThreadToUpdate()//线程方法
        {
            try
            {
                ProducePokeService.UpdateAfterBySortnum(this.List, this.Status);
                MessageBox.Show(this.Info);
                WriteLog.GetLog().Write(Info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Lbl.Visible = false;
                Btn.Enabled = true;
            }


        }
    }
}
