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
        WcApi wcApi;


        public TargetSelector(WcApi api)
        {
            wcApi = api;
        }

        public IMyEntity SimpleSelect(Dictionary<IMyEntity, float> detectedEntities)
        {
            List<IMyEntity> validTargets = new List<IMyEntity>();
            foreach(var candidate in detectedEntities)
            {
               //if()
            }
            if (detectedEntities.Count > 0) return detectedEntities.First().Key;
            else return null;
        }
    }
}
