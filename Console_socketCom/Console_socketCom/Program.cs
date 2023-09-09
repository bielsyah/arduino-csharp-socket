using SuperSimpleTcp;
using System;
using System.Text;

namespace socketcom
{
    class socketcommunication
    {
        static SimpleTcpClient client;
        public static void Main(string[] args)
        {

            string IpPort = "192.168.100.158:5050";

            client = new SimpleTcpClient(IpPort);
            client.Events.Connected += Connected;
            client.Events.Disconnected += Disconnected;
            client.Events.DataReceived += DataReceived;

            Connect();

            while(true)
            {
                try
                {
                    string input = Console.ReadLine();
                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                    byte[] newlineBytes = Encoding.UTF8.GetBytes(Environment.NewLine);
                    byte[] dataBytes = inputBytes.Concat(newlineBytes).ToArray();
                    client.Send(dataBytes);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
            Disconnect();
        }
        private static void Connect()
        {
            try
            {
                client.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void Disconnect()
        {
            client.Disconnect();
        }
        private static void Connected(object sender, EventArgs e)
        {
            Console.WriteLine("Connected to the server.");
        }
        private static void Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnected from the server. Trying to reconnect...");
            Connect();
        }
        private static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.Write($"{Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");
        }
    }
}
