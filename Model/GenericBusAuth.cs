using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GenericBusAuth
    {
        public string entity_id { get; set; }
        public string entity_name { get; set; }
        public string entity_description { get; set; }
        public string entity_publicKey { get; set; }
        public string entity_PrivateKey { get; set; }
        public string entity_user { get; set; }
        public string entity_password { get; set; }
        public string entity_adminKey { get; set; }
    }
}
