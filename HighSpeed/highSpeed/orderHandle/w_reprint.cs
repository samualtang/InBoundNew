using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
using InBound.Business;
using System.IO;
using highSpeed.PubFunc;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace highSpeed.orderHandle
{
    public partial class w_reprint : Form
    {
        public w_reprint()
        {
            InitializeComponent();
            gvdata.SelectionChanged += new EventHandler(gvdata_SelectionChanged);
            gvdata.ClearSelection();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            OnLoadTask();
        }

        private void OnLoadGroupNoData()
        {

        }

        public void OnLoadTask()
        {
            this.gvdata.BeginInvoke(new Action(() => { initdata(); }));
        }

        public void initdata()
        {
            gvdata.Rows.Clear();
            List<UITaskModel> list = TaskService.FetchTaskListByRegionCode().Select(s =>
            {
                UITaskModel model = new UITaskModel();
                model.Model = s;
                model.IsChecked = false;
                return model;
            }).ToList();

            if (list != null)
            {
                foreach (var row in list)
                {
                    int index = this.gvdata.Rows.Add();
                    this.gvdata.Rows[index].Cells[0].Value = row.IsChecked;
                    this.gvdata.Rows[index].Cells[1].Value = row.Model.REGIONCODE;
                }
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            var aa = ConfigurationManager.AppSettings.Count;
            string[] getVal = new string[] { };
            foreach (var item in cblist.CheckedItems)
            {
                PokeGroupUIModel f = item as PokeGroupUIModel;
                getVal = ConfigurationManager.AppSettings[f.ConfigKey].Split(',');

                if (getVal.Count() > 0)
                {
                    f.IpAddress = getVal[0];
                    f.Port = int.Parse(getVal[1]);
                }

                IPAddress address = IPAddress.Parse(f.IpAddress);
                IPEndPoint endpoint = new IPEndPoint(address, f.Port);
                Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketClient.Connect(endpoint);
                int i = SocketClientConnector.SendFile(socketClient, f.ZipFile, 10000, 1);
                if (i == 0)
                {
                    MessageBox.Show(f.ZipFile + "文件发送成功");
                }
                else
                {
                    MessageBox.Show(f.ZipFile + "文件发送失败,i=" + i);
                }
            }
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            foreach (var item in cblist.CheckedItems)
            {
                PokeGroupUIModel model = item as PokeGroupUIModel;
                if (model.GroupKind == "1")
                {
                    FetchProducePokeData(model);
                }
            }
            MessageBox.Show("导出成功！");
        }

        private void FetchProducePokeData(PokeGroupUIModel model)
        {
            List<T_PRODUCE_POKE> _pokeList = ProducePokeService.FetchProducePokeList(decimal.Parse(model.GroupNum));
            if (_pokeList.Count == 0) return;
            StringBuilder info = new StringBuilder();
            foreach (var item in _pokeList)
            {
                info.AppendFormat("{0},{1},{2},{3},{4},{5};\n", item.TASKNUM, item.TASKQTY, item.GROUPNO, item.POKENUM, item.TROUGHNUM, item.BILLCODE);
            }
            ExpToZipFile(info.ToString(), model);
        }

        private void ExpToZipFile(string info, PokeGroupUIModel model)
        {
            string filePath = Path.Combine(Application.StartupPath, "files");

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            DateTime dt = DateTime.Now;
            String time = string.Format("{0:yyyyMMddHHmmssfff}", dt);
            String filename = "RetailerOrder" + time;
            StreamWriter sw = new StreamWriter(filePath + "\\" + filename + ".Order", false, Encoding.UTF8);
            sw.WriteLine(info.Substring(0, info.Length - 1));
            sw.Close();//写入
            //ToZipFile.GetFileToZip(filePath + "\\" + filename + ".Order", filePath + "\\" + filename + ".zip", filename + ".Order");
            model.ZipFile = filePath + "\\" + filename + ".zip";
        }

        private void gvdata_SelectionChanged(object sender, EventArgs e)
        {
            if (gvdata.SelectedRows != null && gvdata.SelectedRows[0].Cells[1].Value == null) return;

            string regionCode = gvdata.SelectedRows[0].Cells[1].Value.ToString();
            List<PokeGroupUIModel> list = ProducePokeService.GetGroupNoByRegionCode(regionCode).
                Select(s => new PokeGroupUIModel
            {
                GroupKind = "1",
                GroupNum = s.GROUPNO.ToString(),
                GroupDesc = s.GROUPNO + "正常烟",
                ConfigKey = "1" + s.GROUPNO.ToString(),
            }).OrderBy(o => o.GroupNum).ToList();

            cblist.DataSource = list;
            cblist.ValueMember = "GroupNum";
            cblist.DisplayMember = "GroupDesc";
            for (int i = 0; i < cblist.Items.Count; i++)
            {
                cblist.SetItemChecked(i, true);
            }
        }
    }

    public class UITaskModel
    {
        public T_PRODUCE_TASK Model { get; set; }
        public bool IsChecked { get; set; }
    }
}
