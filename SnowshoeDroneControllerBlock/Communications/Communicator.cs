using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI;
using VRage;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using Sandbox.Common.ObjectBuilders;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using Sandbox.Game.Entities;
using Sandbox.Game.EntityComponents;
using SpaceEngineers.Game.ModAPI;
using VRage.Game;
using VRage.Game.Entity;
using VRage.Utils;
using VRageMath;
using ProtoBuf;

namespace Snowshoe_Drone_Controller_Block.Communications
{
    public class Comunicator
    {
        string Channel = "";
        public List<string> Messages = new List<string>();
        public IMyRadioAntenna Antenna;


        public Comunicator(string channel, IMyRadioAntenna antenna) 
        {
            Antenna = antenna;
            Channel = channel;
            //receiver = antenna.Components.Get<MyDataReceiver>();
            //broadcaster = antenna.Components.Get<MyDataBroadcaster>();
        }

        private void Send(Message message)
        {
            
        }
    }
}
