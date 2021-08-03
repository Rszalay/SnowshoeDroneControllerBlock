using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowshoe_Drone_Controller_Block.Communications
{
    class Message
    {
        string Channel = "default";
        long Transmitter = 0;
        long Receiver = 0;
        object Content;
        string Type = "text";

        public Message(string channel, long receiver, DroneController controller, string type, object content)
        {
            Channel = channel;
            Receiver = receiver;
            Transmitter = controller.Entity.EntityId;
            Type = type;
            Content = content;
        }
    }
}
