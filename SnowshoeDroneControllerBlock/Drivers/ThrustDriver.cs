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
using Snowshoe_Drone_Controller_Block.Controls;
using Snowshoe_Drone_Controller_Block.DroneComponents;

namespace Snowshoe_Drone_Controller_Block.Drivers
{
    class ThrustDriver
    {
        Vector3D _pv, _sp, currentPosition, lastPosition, velocitySp;
        public Vector3D velocityPv { get; private set; }
        Ideal ConX, ConY, ConZ;
        DroneController ThisController;
        public Dictionary<Base6Directions.Direction, List<IMyThrust>> thrusterSet;
        double xThrottle;
        double yThrottle;
        double zThrottle;
        double speedSp;
        int ticksSinceLastRun = 0;

        public ThrustDriver(List<IMyCubeBlock> blocks, DroneController thisController, Settings settings)
        {
            ConX = new Ideal(settings.Thrust);
            ConY = new Ideal(settings.Thrust);
            ConZ = new Ideal(settings.Thrust);
            ThisController = thisController;
            thrusterSet = new Dictionary<Base6Directions.Direction, List<IMyThrust>>();
            Purge(blocks);
        }

        public void Purge(List<IMyCubeBlock> blocks)
        {
            foreach (IMyTerminalBlock block in blocks)
            {
                if (block is IMyThrust)
                {
                    if (!thrusterSet.ContainsKey(block.Orientation.Forward))
                    {
                        thrusterSet.Add(block.Orientation.Forward, new List<IMyThrust>());
                        thrusterSet[block.Orientation.Forward].Add(block as IMyThrust);
                    }
                    else { thrusterSet[block.Orientation.Forward].Add(block as IMyThrust); }
                }
            }
            ThisController.Echo(thrusterSet.Count.ToString());
        }

        public void Load(Vector3D sp, Vector3D pv, double speed)
        {
            speedSp = speed;
            _pv = pv;
            _sp = sp;
            currentPosition = ThisController.Entity.WorldMatrix.Translation;
            Vector3D error = _sp - _pv;
            velocitySp = Vector3D.Normalize(error) * speedSp;
            velocityPv = (currentPosition - lastPosition) * (60.0 / ticksSinceLastRun);
            error = (velocitySp - velocityPv);
            ConX.Load(error.X);
            ConY.Load(error.Y);
            ConZ.Load(error.Z);
            lastPosition = currentPosition;
            ticksSinceLastRun = 0;
        }

        public void Run()
        {
            xThrottle = ConX.Run();
            yThrottle = ConY.Run();
            zThrottle = ConZ.Run();
            Vector3D thrustVector = new Vector3D(xThrottle, yThrottle, zThrottle);
            foreach (var orientation in thrusterSet)
            {
                var first = orientation.Value.First();
                if (orientation.Value.Count() > 0)//should probably sort by throttle settings first so you don't have to check orientations on thruster that should be off
                {
                    Vector3D thrusterWF = first.WorldMatrix.Forward;

                    double thrusterDot = Vector3D.Dot(first.WorldMatrix.Backward, thrustVector);
                    if (thrusterDot > 0)
                    {
                        foreach (var thruster in orientation.Value) { thruster.ThrustOverride = (float)thrusterDot; }
                    }
                    else
                    {
                        foreach (var thruster in orientation.Value) { thruster.ThrustOverride = 0; }
                    }
                }
            }
        }

        public void Tick() 
        { 
            ticksSinceLastRun++;
            ConX.Tick();
            ConY.Tick();
            ConZ.Tick();
        }
    }
}
