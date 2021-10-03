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
using Snowshoe_Drone_Controller_Block;
using Snowshoe_Drone_Controller_Block.DroneComponents;
using Snowshoe_Drone_Controller_Block.TargetComp;
using Snowshoe_Drone_Controller_Block.Session.WorldAssets;

namespace Snowshoe_Drone_Controller_Block.DroneComponents
{

    class WeaponSet
    {
        public Dictionary<WeaponType, List<IMyCubeBlock>> Map = new Dictionary<WeaponType, List<IMyCubeBlock>>();
        public List<MyDefinitionId> _Tmp = new List<MyDefinitionId>();
        DroneController MyDroneController;

        public WeaponSet(DroneController droneController)
        {
            MyDroneController = droneController;
        }

        public void Purge(MyCubeGrid grid)
        {
            DroneControlSession.Assets.Weapons.GetWeaponTypeMap(grid, ref Map);
            
            _Tmp.Clear();
        }
    }
}
