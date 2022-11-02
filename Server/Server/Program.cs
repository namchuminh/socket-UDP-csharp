using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Bước 1: Tạo IpEndPoint server UDP
                IPEndPoint s_iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                //Bước 2: Tạo socket UDP
                Socket serverSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                //Bước 3: Đăng ký địa chỉ s_iep cho server socket UDP
                serverSocket.Bind(s_iep);
                //Bước 4: Gửi nhận gói tin

                while (true)
                {
                    //Nhận tin được gửi đến từ client
                    byte[] dataReceive = new byte[1024];
                    //Lấy địa chỉ EndPoint của client để thực hiện gửi lại tin cho client đó
                    EndPoint c_iep = new IPEndPoint(IPAddress.None, 0); //Ban đầu địa chỉ EndPoint của client là rỗng và port là 0
                    serverSocket.ReceiveFrom(dataReceive, ref c_iep);//Lấy data của clinet gửi lên và gán vào dataReceive và c_iep
                    string messageReceive = ASCIIEncoding.ASCII.GetString(dataReceive);
                    Console.Write("<client>: " + messageReceive);

                    //Gửi lại tin cho client
                    Console.Write("Nhap chuoi can gui ve Client: \n");
                    string messageSend = Console.ReadLine();
                    byte[] dataSend = ASCIIEncoding.ASCII.GetBytes(messageSend);
                    serverSocket.SendTo(dataSend, c_iep);

                    if (messageReceive.ToLower() == "thoat")
                    {
                        //Bước 5: Đóng kết nối
                        serverSocket.Close();
                        return;
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            Console.ReadLine();
        }
    }
}
