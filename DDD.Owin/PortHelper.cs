using System.Collections;
using System.Net;
using System.Net.NetworkInformation;

namespace DDD.Owin
{
    public class PortHelper
    {
        public static int GetFirstAvailablePort()
        {
            int MAX_PORT = 65535;
            int BEGIN_PORT = 5000;

            for (int i = BEGIN_PORT; i < MAX_PORT; i++)
            {
                if (PortIsAvailable(i)) return i;
            }

            return -1;
        }

        public static IList PortIsUsed()
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);

            return allPorts;
        }

        public static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;

            IList portUsed = PortIsUsed();

            foreach (int p in portUsed)
            {
                if (p == port)
                {
                    isAvailable = false; break;
                }
            }

            return isAvailable;
        }
    }
}