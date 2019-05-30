﻿using System;

namespace GovernmentParse.Helpers
{
    public class NetHelper
    {
        public string GetHostIp()
        {

            String myHostName = GetHostName();

            System.Net.IPHostEntry myiphost = System.Net.Dns.GetHostEntry(myHostName);

            foreach (System.Net.IPAddress myipadd in myiphost.AddressList)
            {
                if (myipadd.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return myipadd.ToString();
            }
            throw new Exception("Не вдається встановити Ip-адресу компьютера");
        }

        public string GetHostName()
        {
            var host = System.Net.Dns.GetHostName();
            if (string.IsNullOrEmpty(host))
                throw new Exception("Не вдається встановити мережеве ім'я компьютера");
            return host;
        }
    }
}
