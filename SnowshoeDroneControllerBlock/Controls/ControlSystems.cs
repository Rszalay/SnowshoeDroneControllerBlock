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
    class ControlSystem : ControllerBase
    {
        FeedBack _feedBack;
        Tracker _tracker;
        ControllerBase Controller;

        public ControlSystem(ControllerBase controller, Tracker tracker, FeedBackType type)
        {
            _tracker = tracker;
            if (type == FeedBackType.Cubic) _feedBack = new Cubic();
            else if (type == FeedBackType.Exponential) _feedBack = new Exponential();
            else if (type == FeedBackType.Unity) _feedBack = new Unity();
            Controller = controller;
        }

        public void Load(ICollection<MyTuple<IMyEntity, float>> candidates, MyEntity self)
        {
            _tracker.FindTarget(candidates, self);
            double range = _tracker.Range();
            Load(range);
        }

        public override double Run(double error)
        {
            double FBError = _feedBack.Map(error);
            return Controller.Run(FBError);
        }

        public override void Tick()
        {
            Controller.Tick();
        }

        public override void Reset()
        {
            Controller.Reset();
        }
    }
}
