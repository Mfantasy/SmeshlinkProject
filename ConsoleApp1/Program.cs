using SmeshlinkProject.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static byte[] btsGZ;
        static byte[] btsWS;
        static string url = ConfigurationManager.AppSettings["url"];
        static TcpClient tcp;
        static void Main(string[] args)
        {            
            //中央绿廊var t = "7E000B7D1C000003000000330A0F81EAA500E524223A0A030000034C0010000000";                                    
            var t0 = "7E000B7D1C000003000000330A0F819AB600E52422900A030000034C0010000000";//仁寿土壤1402 //27_2水分*10 29_2温度*10 
            var t1 = "7E000B7D1A000002000000330A00819AB600E5242290050400000400B01C00"; //仁寿光照1501 //27_4 光照/10
            btsGZ = MathHelper.ToBytes(t1);
            btsWS = MathHelper.ToBytes(t0);
            Work();
            //SetVal(176,27,2);
            //SetVal(93, 29, 2);
            //SetVal((int)(21.6*10),27,4);
            //TcpSendBts(bts);
            while (true)
            {
                Console.ReadKey();
            }
            
            //using (Stream retval = HttpUtil.HttpGet(url))
            //{
            //    JsonFormatter jfmt = new JsonFormatter();
            //    var feed = jfmt.ParseFeed(retval);
            //    foreach (var item in feed.Children.ElementAt(0).Children.ElementAt(0).Children)
            //        }
        }

        static int shidu = 466;
        static int wendu = 88;
        static int guangzhao = 36806; //500~50000~                
        static void Work()
        {
            bool add = false;        
            int MWD = 61;
            int MGZ = 15000;
            Random rsd = new Random();
            Random rwd = new Random();
            Random rgz = new Random();
            Action act = () => {
                if (shidu < 111)
                {
                    shidu += rsd.Next(20);
                }
                else if (shidu > 333)
                {
                    shidu -= rsd.Next(20);
                }
                else
                {
                    shidu += rsd.Next(-20, 20);
                }
                if (add)
                {
                    if (wendu >= MWD)
                    {
                        wendu -= rwd.Next(5);
                    }
                    else
                    {
                        wendu += rwd.Next(5);
                    }
                    if (guangzhao >= MGZ)
                    {
                        guangzhao -= rgz.Next(3000);
                    }
                    else
                    {
                        guangzhao += rgz.Next(3000);
                    }
                }
                else
                {
                    if (wendu <= MWD)
                    {
                        wendu += rwd.Next(5);
                    }
                    else
                    {
                        wendu -= rwd.Next(5);
                    }
                    if (guangzhao <= MGZ)
                    {
                        guangzhao += rgz.Next(3000);
                    }
                    else
                    {
                        guangzhao -= rgz.Next(3000);
                    }
                }
            };
            while (true)
            {
                switch (DateTime.Now.Hour)
                {
                    //2 6.1
                    //5 4.6
                    //8 5.3
                    //11 7.2
                    //14 9.7
                    //17 8.8
                    //20 8.7
                    case 23: case 0: case 1:
                        add = false;                     
                        MWD = 61;
                        MGZ = 8500;
                        break;
                    case 2: case 3: case 4:
                        add = false;
                        MWD = 46;
                        MGZ = 8500;
                        break;
                    case 5: case 6: case 7:
                        add = true;
                        MWD = 53;
                        MGZ = 17000;
                        break;
                    case 8: case 9: case 10:
                        add = true;
                        MWD = 72;
                        MGZ = 43000;
                        break;    
                    case 11: case 12: case 13:
                        add = true;
                        MWD = 97;
                        MGZ = 55000;
                        break;
                    case 14: case 15: case 16:
                        add = false;
                        MWD = 88;
                        MGZ = 33000;
                        break;
                    case 17: case 18: case 19:
                        add = false;
                        MWD = 87;
                        MGZ = 17000;
                        break;
                    case 20: case 21: case 22:
                        add = false;
                        MWD = 61;
                        MGZ = 8500;
                        break;                 
                }
                act();
                SetVal(btsWS,shidu,27,2);
                SetVal(btsWS,wendu, 29, 2);
                SetVal(btsGZ,guangzhao,27,4);
                TcpSendBts(btsWS);
                TcpSendBts(btsGZ);
                Console.WriteLine("{0} 温度{1} 湿度{2} 光照{3}",DateTime.Now,wendu,shidu,guangzhao);
                Thread.Sleep(TimeSpan.FromMinutes(5));
                
            }
       
        }


        static void SetVal(byte[] bts,int tvalue, int index, int length)
        {
            var btvalue = MathHelper.ToBytesL(tvalue, length);
            Array.Copy(btvalue, 0, bts, index, btvalue.Length);
        }

        static void TcpSendBts(byte[] packet)
        {
            packet = MathHelper.WrapTOSPacket(packet);
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
                    Console.WriteLine(ex.Message);
                }

            }
            try
            {
                NetworkStream streamToServer = tcp.GetStream();
                streamToServer.Write(packet, 0, packet.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
