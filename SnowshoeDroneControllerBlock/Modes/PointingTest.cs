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
using Sandbox.ModAPI.Ingame;
using Snowshoe_Drone_Controller_Block.DroneComponents;
using Snowshoe_Drone_Controller_Block.TargetComp;

namespace Snowshoe_Drone_Controller_Block.Modes
{
    class PointingTest
    {
        Drone myDrone;
        TargetSelector selector;
        DroneController droneController;
        List<IMyEntity> validTargets = new List<IMyEntity>();
        Dictionary<IMyEntity, long> sortedThreats = new Dictionary<IMyEntity, long>();

        public void Configure(Drone drone, DroneController controller)
        {
            myDrone = drone;
            droneController = controller;
            selector = new TargetSelector(droneController);
        }

        public void Prepare()
        {
            //get the entities list from wc
            //DroneControlSession.wcApi.GetSortedThreats(0, sortedThreats);
        }

        public void Run()
        {

        }
    }
}
