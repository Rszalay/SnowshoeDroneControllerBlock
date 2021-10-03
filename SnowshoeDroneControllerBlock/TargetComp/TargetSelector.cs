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
using Snowshoe_Drone_Controller_Block;
using Snowshoe_Drone_Controller_Block.API;

namespace Snowshoe_Drone_Controller_Block.TargetComp
{
    class TargetSelector
    {
        List<MyDetectedEntityInfo> DetectedEntities = new List<MyDetectedEntityInfo>();
        List<MyDetectedEntityInfo> ValidTargets = new List<MyDetectedEntityInfo>();
        DroneController droneController;


        public TargetSelector(DroneController controller)
        {
            droneController = controller;
        }

        public IMyEntity SimpleSelect(Dictionary<IMyEntity, float> detectedEntities, ref List<IMyEntity> validTargets)
        {
            validTargets.Clear();
            foreach(var candidate in detectedEntities)
            {
                long blockOwner = droneController.Block.OwnerId;
                long targetOwner = (candidate.Key as IMyCubeGrid).BigOwners.First();
                if (MyIDModule.GetRelationPlayerPlayer(blockOwner, targetOwner) == MyRelationsBetweenPlayers.Enemies) validTargets.Add(candidate.Key);
            }
            if (detectedEntities.Count > 0) return validTargets.First();
            else return null;
        }
    }
}
