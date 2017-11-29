using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Net.Sockets;
using System.Net;
using InBound.Business;
using InBound;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Configuration;

namespace highSpeed.orderHandle
{
    public partial class w_senddata : Form
    {
        #region _Private
        Socket socketClient;
        List<PokeGroupUIModel> _unionAllPokeGroupList = new List<PokeGroupUIModel>();
        #endregion


        public w_senddata()
        {
            InitializeComponent();
            InitSocket();
        }

        private void InitSocket()
        {
            //IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress address = IPAddress.Parse("192.168.1.8");
            IPEndPoint endpoint = new IPEndPoint(address, 50001);
            //创建服务端负责监听的套接字，参数（使用IPV4协议，使用流式连接，使用Tcp协议传输数据）
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(endpoint);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            OnLoadGroup();
            OnLoadTask();
        }

        public void OnLoadTask()
        {
            this.gvdata.BeginInvoke(new Action(() => { initdata(); }));
        }

        public void OnLoadGroup()
        {
            this.cblist.BeginInvoke(new Action(() =>
            {
                cblist.Items.Clear();
                List<PokeGroupUIModel> _pokeList = ProducePokeService.GetGroupNo().Select(s => new PokeGroupUIModel
                 {
                     GroupKind = "1",
                     GroupNum = s.GROUPNO.ToString(),
                     GroupDesc = s.GROUPNO + "正常烟",
                     ConfigKey = "1" + s.GROUPNO.ToString(),
                 }).OrderBy(o => o.GroupNum).ToList();

                List<PokeGroupUIModel> _unpokeList = UnPokeService.GetLinenum().Select(s => new PokeGroupUIModel
                 {
                     GroupKind = "2",
                     GroupNum = s.LINENUM,
                     GroupDesc = s.LINENUM + "(异)",
                     ConfigKey = "2" + s.LINENUM
                 }).OrderBy(o => o.GroupNum).ToList();

                _unionAllPokeGroupList = _pokeList.Union(_unpokeList).ToList();
                cblist.DataSource = _unionAllPokeGroupList;
                cblist.ValueMember = "GroupNum";
                cblist.DisplayMember = "GroupDesc";
                for (int i = 0; i < cblist.Items.Count; i++)
                {
                    cblist.SetItemChecked(i, true);
                }
            }));
        }

        public void initdata()
        {
            gvdata.Rows.Clear();
            List<ProduceTask> list = ProduceTaskService.GetItem();
            if (list != null)
            {
                foreach (var row in list)
                {
                    int index = this.gvdata.Rows.Add();
                    this.gvdata.Rows[index].Cells[1].Value = row.batchcode;
                    this.gvdata.Rows[index].Cells[2].Value = row.qty;
                    this.gvdata.Rows[index].Cells[3].Value = row.cuscount;
                    this.gvdata.Rows[index].Cells[4].Value = row.synseq;
                }
            }
        }

        #region controlEvent

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
            }

            foreach (var item in cblist.CheckedItems)
            {
                PokeGroupUIModel f = item as PokeGroupUIModel;
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
                else
                {
                    FetchUnPokeData(model);
                }
            }
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


        private void FetchUnPokeData(PokeGroupUIModel model)
        {
            List<T_UN_POKE> _unpokeList = UnPokeService.FetchUnPokeList(model.GroupNum);
            if (_unpokeList.Count == 0) return;
            StringBuilder info = new StringBuilder();
            foreach (var item in _unpokeList)
            {
                info.AppendFormat("{0},{1},{2},{3},{4},{5};\n", item.TASKNUM, item.TASKQTY, item.LINENUM, item.POKENUM, item.TROUGHNUM, item.BILLCODE);
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
            GetFileToZip(filePath + "\\" + filename + ".Order", filePath + "\\" + filename + ".zip", filename + ".Order");
            model.ZipFile = filePath + "\\" + filename + ".zip";
        }


        private void GetFileToZip(string filepath, string zippath, String entryname)
        {

            FileStream fs = File.OpenRead(filepath);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            FileStream ZipFile = File.Create(zippath);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry(entryname);
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(6);

            ZipStream.Write(buffer, 0, buffer.Length);
            ZipStream.Finish();
            ZipStream.Close();
        }
        #endregion
    }



    public class PokeGroupUIModel
    {
        public string GroupNum { get; set; }
        public string GroupKind { get; set; }
        public string GroupDesc { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string ZipFile { get; set; }
        public string ConfigKey { get; set; }
    }


}
