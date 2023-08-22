using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Enums
{
    public class AppSettings
    {

        public Jwt Jwt { get; set; } = new Jwt();
        public SMTP SMTP { get; set; } = new SMTP();
        public string Environment { get; set; }
        
        public MysqlInfo MysqlInfo { get; set; } = new MysqlInfo();
    }
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

    }
    public class SMTP
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
        public string Mail { get; set; }


    }

    public class MysqlInfo
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }



}
