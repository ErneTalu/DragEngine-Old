using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DragEngine
{
    public class Server
    {
        public TcpListener listener;
        public Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public int port;

        public Server(int port)
        {
            this.port = port;
            InitServerData();

            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(AcceptClientCallBack), null);
            Debug.Log($"Server Started on {port}");
        }

        void AcceptClientCallBack(IAsyncResult result)
        {
            TcpClient client = listener.EndAcceptTcpClient(result);
            listener.BeginAcceptTcpClient(new AsyncCallback(AcceptClientCallBack), null);

            for (int i = 1; i <= 5; i++)
            {
                if (clients[i].tcp.client == null)
                {
                    clients[i].tcp.Connect(client);
                    return;
                }
            }

            Debug.Log($"{client.Client.RemoteEndPoint} Tried to join but the server is full");
        }

        void InitServerData()
        {
            for (int i = 1; i <= 5; i++)
                clients.Add(i, new Client(i));
        }
    }

}
