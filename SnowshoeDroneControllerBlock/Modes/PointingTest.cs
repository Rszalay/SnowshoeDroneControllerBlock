using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowshoe_Drone_Controller_Block.DroneComponents;

namespace Snowshoe_Drone_Controller_Block.Modes
{
    class PointingTest
    {
        Drone myDrone;
        Settings mySettings;

        public void Configure(Drone drone, Settings settings)
        {
            myDrone = drone;
            mySettings = settings;
        }

    }
}
