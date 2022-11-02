using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Bước 1: Lấy địa chỉ IpEndPoint của server UDP cần trao đổi tin
                IPEndPoint s_iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
                //Bước 2: Tạo socket UDP 
                Socket clientSocket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                //Bước 3: Trao đổi tin (gửi/nhận)

                while (true)
                {
                    //Gửi tin lên server UDP
                    Console.Write("Nhap chuoi can gui len Sever: \n");
                    string messageSend = Console.ReadLine();
                    byte[] dataSend = ASCIIEncoding.ASCII.GetBytes(messageSend);
                    clientSocket.SendTo(dataSend, s_iep);//Truyền tin data tới địa chỉ s_iep

                    //Nhận tin từ server trả về
                    byte[] dataReceive = new byte[1024];
                    EndPoint iep = new IPEndPoint(IPAddress.None,0);//Tạo iep rỗng với port 0
                    clientSocket.ReceiveFrom(dataReceive, ref iep);
                    string messageReceive = ASCIIEncoding.ASCII.GetString(dataReceive);
                    Console.Write("<server>: " + messageReceive);

                    if(messageSend.ToLower() == "thoat")
                    {
                        //Bước 4: Đóng kết nối
                        clientSocket.Close();
                        return;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            Console.ReadLine(); 
        }
    }
}
