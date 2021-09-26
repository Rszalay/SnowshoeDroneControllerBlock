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
using Snowshoe_Drone_Controller_Block.Drivers;
using Snowshoe_Drone_Controller_Block.API;

namespace Snowshoe_Drone_Controller_Block.DroneComponents
{
    class Drone
    {
        GyroDriver myGyroDriver;
        ThrustDriver myThrustDriver;
        public List<IMyCubeBlock> AllBlocks = new List<IMyCubeBlock>();
        DroneController thisDroneController;
        Settings settings;
        WcApi wcApi;
        

        public Drone(DroneController droneController, Settings droneSettings, WcApi api)
        {
            wcApi = api;
            thisDroneController = droneController;
            settings = droneSettings;
            Purge();
            myGyroDriver = new GyroDriver(AllBlocks, thisDroneController, settings);
            myThrustDriver = new ThrustDriver(AllBlocks, thisDroneController, settings);
            myGyroDriver.Purge(AllBlocks);
            myThrustDriver.Purge(AllBlocks);
        }

        public void Purge()
        {
            //remove any destroyed/damaged blocks. Add any new ones
            IMyCubeGrid parentGrid = thisDroneController.Block.CubeGrid as IMyCubeGrid;
            AllBlocks.Clear();
            List<IMySlimBlock> temp = new List<IMySlimBlock>();
            parentGrid.GetBlocks(temp);
            foreach (var block in temp) if (block.FatBlock != null) AllBlocks.Add(block.FatBlock);
            temp.Clear();
            myGyroDriver?.Purge(AllBlocks);
            myThrustDriver?.Purge(AllBlocks);
        }

        //<----Basic Control Section---->//
        public void Point(Vector3D tangent, Vector3D normal)
        {
            myGyroDriver.Load(tangent, normal);
        }

        public void Thrust(Vector3D sp, Vector3D pv, double speed)
        {
            myThrustDriver.Load(sp, pv, speed);
        }

        public void Run()
        {
            myGyroDriver.Run();
            myThrustDriver.Run();
        }
    }
}
