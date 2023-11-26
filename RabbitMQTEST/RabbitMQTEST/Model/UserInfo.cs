using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQTEST.Model
{
    /// <summary>
    /// User Info Basic (protected)
    /// </summary>
    public class UserInfo
    {
        protected string userName = "guest";

        protected string password = "guest";

        protected string hostName = "localhost";
    }

    /// <summary>
    /// User Info
    /// </summary>
    public class MyUserInfo : UserInfo
    {
        public string UserName { get { return userName; } }

        public string PassWord { get { return password; } }

        public string HostName { get { return hostName; } }
    }
}