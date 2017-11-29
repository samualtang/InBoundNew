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
namespace FormUI
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
        }
      
        public void search()
        {

           
            dataGridView1.AutoGenerateColumns = false;
            
            dataGridView1.DataSource = InfJobDownLoadService.QueryError();;
            
        }
        private void QueryForm_Load(object sender, EventArgs e)
        {
            search();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {
                MessageBox.Show(jobid + errorCode);
                if (errorCode == "SrmActuator001")//空出
                {
                    AtsCellService.UpdateAtsCell1(source, 50);
                }
                else if (errorCode == "SrmActuator006")//重入
                {
                    AtsCellService.UpdateAtsCell1(target, 50);
                    INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                    load2.JOBTYPE = decimal.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    load2.PLANQTY = decimal.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
                    load2.SOURCE = target;
                    load2.TARGET = AtsCellInService.getCellNo(target,AtsCellService.GetAtsCell(target).LANEWAYNO);
                    if (load2.TARGET != null)
                    {
                        InfJobDownLoadService.InsertEntity(load2);
                    }
                }
                
                     INF_JOBDOWNLOAD load1=new INF_JOBDOWNLOAD();
                     load1.JOBID=jobid;
                     load1.JOBTYPE=97;
                     InfJobDownLoadService.InsertCancelTask(load1);
                
            }
            else {
                MessageBox.Show("请选择需要取消的任务");
            }
        }
        String jobid = "";
        String errorCode = "";
        String source = "";
        String target="";
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {

                jobid=dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                errorCode = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                source = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                target = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

    }
}
