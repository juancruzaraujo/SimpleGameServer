using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets;

namespace SimpleGameServer
{
    public class GameServerManager
    {
        public GameServerManager()
        {

        }

        public string DataIn(EventParameters socketEventParameters)
        {

            string result = "";
            if (Protocol.ConnectionProtocol.TCP == socketEventParameters.GetProtocol)
            {
                //se conecta y me manda <hello>
                //le mando <spos> x,y
                result = TcpMessage(socketEventParameters.GetData);
                
            }

            if (Protocol.ConnectionProtocol.UDP == socketEventParameters.GetProtocol)
            {
                //con todo ya establecido
                //me manda <mpos> x,y|
                //todo ok, le mando a todos <ppos>x,y
                result = UdpMessage(socketEventParameters.GetData);
                
            }
            return result;
        }

        private string TcpMessage(string message)
        {
            string result = "";

            if (message.Contains("<hello>"))
            {
                Random r = new Random();
                int xPos = r.Next(0, 100); //ver bien el tamaño del nivel
                int yPos = r.Next(0, 100);
                result = "<spos>" + xPos + "," + yPos; 
            }
            
            return result;
        }

        private string UdpMessage(string message)
        {
            string result = "";
            if (message.Contains("<mpos>"))
            {
                string[] positions = message.Split('|');
            }

            return result;
        }
    }
}
