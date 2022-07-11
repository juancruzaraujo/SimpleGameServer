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

        public string DataIn(EventParameters socketEventParameters,ref bool forAll)
        {

            string result = "";
            forAll = false;

            if (Protocol.ConnectionProtocol.TCP == socketEventParameters.GetProtocol)
            {
               
                result = TcpMessage(socketEventParameters.GetData);
                if (result=="")
                {
                    result = TcpMessageForAll(socketEventParameters.GetData);
                    forAll = true;
                }

                
            }

            if (Protocol.ConnectionProtocol.UDP == socketEventParameters.GetProtocol)
            {
                //con todo ya establecido
                //me manda <mpos> x,y|
                //todo ok, le mando a todos <ppos>x,y
                result = UdpMessage(socketEventParameters.GetData);
                if (result == "")
                {
                    result = UdpMessageForAll(socketEventParameters.GetData);
                    forAll = true;
                }
                
            }
            return result;
        }

        private string TcpMessage(string message)
        {
            string result = "";

            if (message.Contains(GameComunication.DATAIN_CONNECTION_OK))
            {
                //hacer que mande el tamaño del nivel
                result = GameComunication.DATASEND_CREATE_GRID + "|" + ConstantValues.LEVEL_SIZE_X + "|" + ConstantValues.LEVEL_SIZE_Y + "|" + ConstantValues.LEVEL_SIZE_Z + "|" + ConstantValues.LEVEL_COORDS_2D;
            }
            
            return result;
        }

        private string UdpMessage(string message)
        {
            string result = "";

            return result;
        }

        private string TcpMessageForAll(string message)
        {
            string result = "";

            return result;
        }

        private string UdpMessageForAll(string message)
        {
            string result = "";

            return result;
        }
    }
}
