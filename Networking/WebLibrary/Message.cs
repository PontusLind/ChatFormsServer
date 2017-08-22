using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary
{
    enum Action
    {
        Exit,
        SentToChatBox
    }

    public class Message
    {
        public string Version { get; set; }
        public string Action { get; set; }
        public string UserName { get; set; }
        public string UserMessage { get; set; }
    }
}
