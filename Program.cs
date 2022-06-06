using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sockets;
using ConsoleOutputFormater;
using ShowAndLogMessage;


namespace SimpleGameServer
{
    class Program
    {

        #if DEBUG
            private static bool debug_Mode = true;
        #else
            private static bool debug_Mode = false;
        #endif

        static Socket _socketsTCP;
        static Socket _socketsUDP;
        static OutputFormatter outputFormater;

        static GameServerManager _gameServerManager;

        static int portNumber;
        static bool keepRuning;
        static int maxCon;
        static LogInfo _msg;

        static void Main(string[] args)
        {
            outputFormater = new ConsoleOutputFormater.OutputFormatter();
            OutputFormatterAttributes attr = new OutputFormatterAttributes();
            _msg = LogInfo.GetInstance();
            try
            { 
                if (args.Count() != 2)
                {
                    //attr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Red);
                    _msg.ShowMessage(" \r\nparams[port number] [max connections]", null, LogInfo.typeMsg.ERROR);

                    if (debug_Mode)
                    {
                        Console.ReadLine();
                    }
                    System.Environment.Exit(0);
                }

                portNumber = int.Parse(args[0]);
                maxCon = int.Parse(args[1]);

                _gameServerManager = new GameServerManager();

                _socketsTCP = new Socket();
                _socketsUDP = new Socket();

                _socketsTCP.SetServer(portNumber, Protocol.ConnectionProtocol.TCP, maxCon);
                _socketsUDP.SetServer(portNumber, Protocol.ConnectionProtocol.UDP);

                _socketsTCP.Event_Socket += SocketsTCP_Event_Socket;
                _socketsUDP.Event_Socket += SocketsUDP_Event_Socket;

                _gameServerManager = new GameServerManager();


                _msg.ShowMessage("Simple Game Server ", null, LogInfo.typeMsg.NO_TYPE);


                _msg.ShowMessage("tcp port " + portNumber, null, LogInfo.typeMsg.OK);
                _msg.ShowMessage("udp port " + portNumber, null, LogInfo.typeMsg.OK);


                _socketsTCP.StartServer();
                _msg.ShowMessage("tcp start ", null, LogInfo.typeMsg.OK);

                _socketsUDP.StartServer();
                _msg.ShowMessage("udp start ", null, LogInfo.typeMsg.OK);

                OutputFormatterAttributes atrr = new OutputFormatterAttributes();
                attr.SetColorFG(OutputFormatterAttributes.TextColorFG.Bright_Blue).SetColorBG(OutputFormatterAttributes.TextColorBG.Black);
                _msg.ShowMessage(_msg.CustomType("INFO", attr) + " max connections " + maxCon);

                keepRuning = true;

                while (keepRuning)
                {

                }
            }
            catch (Exception err)
            {
                //WriteMessage(err.Message);
                _msg.ShowMessage(err.Message, null, LogInfo.typeMsg.ERROR);
            }

        }

        private static void SocketsUDP_Event_Socket(EventParameters eventParameters)
        {
            switch (eventParameters.GetEventType)
            {
                case EventParameters.EventType.DATA_IN:
                    DataIn(eventParameters);
                    //socketsUDP.Send(eventParameters.GetConnectionNumber, "MSG UDP RECIVED OK ");
                    break;
            }
        }

        private static void SocketsTCP_Event_Socket(EventParameters eventParameters)
        {
            switch (eventParameters.GetEventType)
            {
                case EventParameters.EventType.SERVER_ACCEPT_CONNECTION:
                    //DataIn("Accept connection from > " + eventParameters.GetClientIp + " connection number " + eventParameters.GetConnectionNumber);
                    break;

                case EventParameters.EventType.DATA_IN:
                    DataIn(eventParameters);
                    //socketsTCP.Send(eventParameters.GetConnectionNumber, "MSG TCP RECIVED OK ");
                    break;

                case EventParameters.EventType.ERROR:
                    _msg.ShowMessage(eventParameters.GetData + " " + eventParameters.GetConnectionNumber, null, LogInfo.typeMsg.ERROR);
                    break;

                case EventParameters.EventType.END_CONNECTION:
                    _msg.ShowMessage("end connection " + eventParameters.GetConnectionNumber, null, LogInfo.typeMsg.OK);
                    break;


            }
        }

        private static void DataIn(EventParameters eventParameters)
        {
            //_msg.ShowMessage(message);
            string answer = _gameServerManager.DataIn(eventParameters);
            if (eventParameters.GetProtocol == Protocol.ConnectionProtocol.TCP)
            {
                _socketsTCP.SendAll(answer);
            }
            else
            {
                _socketsUDP.SendAll(answer);
            }
        }

    }
}
