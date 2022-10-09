using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SimpleGameServer
{
    [DataContract]
    class BodyMessage
    {
        [DataMember]
        public string messageTag { get; set; }

        [DataMember]
        public string messageBody { get; set; }
    }
}
