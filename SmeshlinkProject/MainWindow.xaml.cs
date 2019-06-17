using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;
using SmeshLink.Misty.Entity;
using SmeshLink.Misty.Formatter;
using SmeshLink.Misty.Service;
using SmeshlinkProject.Utils;

namespace SmeshlinkProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow:MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
         
            this.Loaded += MainWindow_Loaded;       
        }
       
        byte[] bts;
        Timer timer;
        TcpClient tcp;
        //string url = "http://api.smeshlink.com/fengxi/81B28F01E524227F.json";
        string url = ConfigurationManager.AppSettings["url"];
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new Timer(6 * 10 * 1000);//60s
            timer.Elapsed += Timer_Elapsed;
            //bts = MathHelper.ToBytes("7E000B7D23000000000000330A0081B28F01E524228F2D000000017E00010400FFFFFFFFD4007C010000");           
            bts = MathHelper.ToBytes("7E000B7D23000000000000330A00810BC600E524227B1D000000017E00010400FFFFFFFFD4007C010000");
            var t = "7E000B7D1C000003000000330A0F81EAA500E524223A0A030000034C0010000000";
        }
        





        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {            
            QXZ q = new QXZ();
            using (Stream retval = HttpUtil.HttpGet(url))
            {
                JsonFormatter jfmt = new JsonFormatter();
                var feed = jfmt.ParseFeed(retval);
                foreach (var item in feed.Children.ElementAt(0).Children.ElementAt(0).Children)
                {                    
                    switch (item.Name)
                    {
                        //case "rain":                          
                        //    q.rain = Convert.ToDouble(item.CurrentValue);
                        //    if (rain == -1)
                        //    {
                        //        rain = q.rain;
                        //    }
                        //    if (q.rain != rain)  //说明已经开始降雨,要搞事情了~
                        //    {                              
                        //        rainBegin = DateTime.Now;
                        //    }
                        //    //有可能出现负值~ 负值一定是-25.?
                        //    double temp = q.rain;                            
                        //    q.rain = q.rain - rain;
                        //    rain = temp;
                        //    if (q.rain < 0)
                        //    {
                        //        q.rain = 25.6 + q.rain;
                        //    }
                        //    //要计算累积雨量.超过3小时累积清零
                        //    if ((DateTime.Now - rainBegin) > TimeSpan.FromHours(3))
                        //    {
                        //        sum = 0;
                        //    }
                        //    sum +=  (int)Math.Round(q.rain*10);
                        //    q.sum = sum;                                                                                        
                        //    break;
                        case "rain":
                            q.rain = Convert.ToDouble(item.CurrentValue);
                            break;
                        case "windspeed":
                            q.windspeed = Convert.ToInt32(item.CurrentValue);
                            break;
                        case "winddirection":
                            q.winddirection = Convert.ToInt32(item.CurrentValue);
                            break;
                        case "airtemp":
                            q.airtemp = Convert.ToDouble(item.CurrentValue) - 1 + new Random().NextDouble();
                            break;
                        case "airhumid":
                            q.airhumid = Convert.ToDouble(item.CurrentValue) - 1 + new Random().NextDouble();
                            break;
                    }
                }
            }            
            SendQ(q);
        }
        DateTime rainBegin = DateTime.Now; //3小时
        double rain = -1;
        int sum = 0;
        void SendQ(QXZ q)
        {           
            //SetVal(q.sum,40,2);
            int index = 27;                                              
            SetVal((int)(Math.Round(q.rain, 1) * 10), index, 2);
            index += 2;
            SetVal(q.windspeed, index,1);
            index += 1;
            SetVal(q.winddirection, index,2);
            index += 6;
            SetVal((int)(q.airtemp * 10), index,2);
            index += 2;
            SetVal((int)(q.airhumid * 10), index,2);
            //SET OK SEND                                                
            byte[] packet = MathHelper.WrapTOSPacket(bts);
            TcpSendBts(packet);     
        }


        void TcpSendBts(byte[] packet)
        {
            if (tcp == null)
            {
                tcp = new TcpClient();
            }
            if (!tcp.Connected)
            {
                try
                {
                    tcp.Connect(ConfigurationManager.AppSettings["ip"], Int32.Parse(ConfigurationManager.AppSettings["port"]));
                }
                catch (Exception ex)
                {
                    tcp = null;
                    WriteEx(ex);
                }

            }
            try
            {
                NetworkStream streamToServer = tcp.GetStream();
                streamToServer.Write(packet, 0, packet.Length);
            }
            catch (Exception ex)
            {
                WriteEx(ex);
            }
        }

        void WriteEx(Exception ex)
        {
            File.AppendAllText("error.txt", ex.Message + "\t" + DateTime.Now.ToShortDateString() + "\r\n");
        }

        void SetVal(int tvalue, int index, int length)
        {
            var btvalue = MathHelper.ToBytesL(tvalue, length);
            Array.Copy(btvalue, 0, bts, index, btvalue.Length);          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ks = "开始";
            string tz = "停止";
            if (btn.Content.ToString() == ks)
            {
                btn.Content = tz;
                timer.Start();
                Timer_Elapsed(null, null);
            }
            else if (btn.Content.ToString() == tz)
            {
                btn.Content = ks;
                timer.Stop();
            }
        }

        class QXZ {            
            public Double rain;
            public Int32 windspeed;
            public Int32 winddirection;
            public Double airtemp;
            public Double airhumid;            
            public Int32 sum;
        }

        private void tt_Click(object sender, RoutedEventArgs e)
        {
            QXZ q = new QXZ();

            q.rain = Convert.ToDouble(tb.Text);
            if (rain == -1)
            {
                rain = q.rain;
            }
            if (q.rain != rain)  //说明已经开始降雨,要搞事情了~
            {
                rainBegin = DateTime.Now;
            }
            //有可能出现负值~ 负值一定是-25.?
            double temp = q.rain;
            q.rain = q.rain - rain;
            rain = temp;
            if (q.rain < 0)
            {
                q.rain = 25.6 + q.rain;
            }
            //要计算累积雨量.超过3小时累积清零
            if ((DateTime.Now - rainBegin) > TimeSpan.FromHours(3))
            {
                sum = 0;
            }
            sum += (int)Math.Round(q.rain * 10);
            q.sum = sum;

            q.winddirection = 88;
            q.windspeed = 88;
            q.airtemp = 88;
            q.airhumid = 88;
            
            SendQ(q);
        }
    }
}
