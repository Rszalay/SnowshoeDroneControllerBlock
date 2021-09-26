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
using Snowshoe_Drone_Controller_Block.API;


namespace Snowshoe_Drone_Controller_Block
{
    [MySessionComponentDescriptor(MyUpdateOrder.AfterSimulation)]
    public class DroneControlSession : MySessionComponentBase
    {
        public static DroneControlSession Instance; // NOTE: this is the only acceptable static if you nullify it afterwards.
        public static WcApi wcApi { get; private set; }
        //public static ShieldApi SH_api { get; private set; }

        public Dictionary<long,DroneController> DroneControllers = new Dictionary<long, DroneController>();

        public override void LoadData()
        {
            Instance = this;
        }

        public override void BeforeStart()
        {
            wcApi = new WcApi();
            if (wcApi != null)
            {
                wcApi.Load();
            }
        }

        protected override void UnloadData()
        {
            Instance = null; // important to avoid this object instance from remaining in memory on world unload/reload
            wcApi = null;
        }

        new public virtual void UpdateBeforeSimulation()
        {
            
        }
    }
}