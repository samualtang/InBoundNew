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
                


                    var result3 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 20 && x.TROUGHTYPE == 10).GroupBy(x => x.GROUPNO).OrderBy(x=>x.Key).ToList();
                    foreach (var item in result3)
                    {
                        comboBox_group.Items.Add(item.Key);
                    }
                    comboBox_group.SelectedIndex = 0 ;

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
                comboBox4.Items.Clear();
                comboBox5.Items.Clear();
                using (Entities et = new Entities())
                {
                    var result4 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 20 && x.TROUGHTYPE == 10 && x.GROUPNO == txt).Select(x => new { x.MACHINESEQ, x.TROUGHNUM, x.GROUPNO,x.STATE }).OrderBy(x => x.MACHINESEQ).ToList();
                    foreach (var item in result4)
                    {
                        comboBox4.Items.Add(item.TROUGHNUM);
                        if (item.STATE=="0")
                        {
                            comboBox5.Items.Add(item.TROUGHNUM);
                        } 
                    }
                    comboBox4.SelectedIndex = -1;
                    comboBox5.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex>-1)
            {
                try
                {
                    string txt = comboBox4.Text;
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
            if (comboBox5.SelectedIndex > -1)
            {
                try
                {
                    string txt = comboBox5.Text;
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
            if (comboBox5.Text.Length <= 0)
            {
                MessageBox.Show("请选择原通道");
                return;
            }
            if (comboBox4.Text.Length <= 0)
            {
                MessageBox.Show("请选择目标通道");
                return;
            }

            DialogResult re = MessageBox.Show("请确认：分拣组" + comboBox_group.Text + "\r" + comboBox5.Text + "" + label18.Text + "\r与\r" + comboBox4.Text + "" + label22.Text + "\r换道？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (re == DialogResult.OK)
            {
                try
                {
                    ProducePokeService.FetchPokeByTroughNo(comboBox5.Text, comboBox4.Text);
                    MessageBox.Show("换道成功，请检查数据");
                }
                catch (Exception)
                {
                    MessageBox.Show("换道失败，请重试");
                }
              
            }
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
            }
            else
            {
                MessageBox.Show(errmsg);
            }
        }
    }
}
