using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;

namespace FormUI
{
    public partial class ManualForm : Form
    {
        public ManualForm()
        {
            InitializeComponent();
        }
        InBound.INF_JOBDOWNLOAD load = null;
        List<InBound.INF_JOBDOWNLOAD> list = new List<InBound.INF_JOBDOWNLOAD>();
        public void search()
        {

            
            dataGridView1.AutoGenerateColumns = false;
            
            load = InfJobDownLoadService.QueryManual();
            if (load != null)
            {
                list.Add(load);
            }
            dataGridView1.DataSource =list;
            
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
            if (list != null && list.Count>0)
            {
                InBound.INF_JOBFEEDBACK request = new InBound.INF_JOBFEEDBACK();
                //request.ID = Guid.NewGuid().ToString("N");
                request.JOBID = jobid;
                request.FEEDBACKSTATUS = 99;
                request.ERRORCODE = "OK";
                request.ENTERDATE = DateTime.Now;
              //  request.STATUS = 0;
                InfFeedBackService.InsertEntity(request);
            }
        }
        String jobid = "0";
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {

                jobid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
               
            }
        }



    }
}
