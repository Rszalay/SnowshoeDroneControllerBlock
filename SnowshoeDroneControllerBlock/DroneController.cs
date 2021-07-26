using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI;
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

namespace Snowshoe_Drone_Controller_Block
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_RemoteControl), true, "SnowShoeDroneController")]
    public class DroneController : MyGameLogicComponent
    {
        List<string> EchoStrings = new List<string>();
        private IMyRemoteControl Block;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            NeedsUpdate = MyEntityUpdateEnum.EACH_FRAME;
            Block = (IMyRemoteControl)Entity;
        }

        public override void UpdateOnceBeforeFrame() // first update of the block
        {
            if (Block.CubeGrid?.Physics == null) // ignore projected and other non-physical grids
                return;

            DroneControlSession.Instance?.DroneControllers.Add(Block.EntityId, new DroneController());

            Block.AppendingCustomInfo += AppendCustomInfo;
        }

        public override void Close() // called when block is removed for whatever reason (including ship despawn)
        {
            DroneControlSession.Instance?.DroneControllers.Remove(Entity.EntityId);
            Block.AppendingCustomInfo -= AppendCustomInfo;
        }

        public override void UpdateBeforeSimulation()
        {
            Echo("my ID is : " + Block.EntityId.ToString());
            Block.RefreshCustomInfo();
        }

        private void AppendCustomInfo(IMyTerminalBlock block, StringBuilder text)
        {
            if (block == null) return;
            text.AppendLine("* * * * * * * * * * * * *");
            foreach (var line in EchoStrings) text.AppendLine(line);
            EchoStrings.Clear();
        }

        private void Echo(string echoString)
        {
            EchoStrings.Add(echoString);
        }
    }
}
