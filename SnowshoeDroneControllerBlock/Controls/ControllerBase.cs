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

namespace Snowshoe_Drone_Controller_Block.Controls
{
    class ControllerBase
    {
        protected List<ControllerBase> controllers = new List<ControllerBase>();
        protected int ticksSinceLastRun = 0;
        protected double _pv = 0;
        protected double _sp = 0;
        public virtual void Load(double position)
        {
            _pv = position;
        }
        public virtual void Set(double setpoint)
        {
            _sp = setpoint;
        }
        public double Run()
        {
            return Run(_sp - _pv);
        }
        public virtual double Run(double error)
        {
            return 0;
        }
        public virtual void Tick()
        {
            ticksSinceLastRun++;
            if (controllers.Count != 0) foreach (var controller in controllers) controller.Tick();
        }
        public virtual void Reset()
        {
            ticksSinceLastRun = 0;
            if (controllers.Count != 0) foreach (var controller in controllers) controller.Reset();
        }
    }
}
