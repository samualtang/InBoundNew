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
                InBound.INF_EQUIPMENTREQUEST request = new InBound.INF_EQUIPMENTREQUEST();
                request.BARCODE = load.BARCODE;
                request.REQUESTTYPE = 1;
                request.EQUIPMENTID = load.SOURCE;
                request.STATUS = 0;
                InfEquipmentRequestService.insert(request);
            }
        }



    }
}
