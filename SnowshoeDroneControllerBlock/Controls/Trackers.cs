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
    class Tracker
    {
        public long TargetId = 0;
        public Vector3D TargetPosition = Vector3D.Zero;
        public Vector3D SelfPosition = Vector3D.Zero;
        public Vector3D TargetVelocity = Vector3D.Zero;
        protected Vector3D TargetLastPosition = Vector3D.Zero;

        public bool FindTarget(ICollection<MyTuple<IMyEntity, float>> collection, MyEntity self)
        {
            bool targetFound = false;
            SelfPosition = self.WorldMatrix.Translation;
            if (TargetId != 0)
            {
                foreach (var candidate in collection)
                {
                    if (candidate.Item1.EntityId == TargetId)
                    {
                        TargetPosition = candidate.Item1.WorldMatrix.Translation;
                        targetFound = true;
                    }
                }
                if (targetFound)
                {
                    TargetVelocity = TargetPosition - TargetLastPosition;
                    TargetLastPosition = TargetPosition;
                }
            }
            else targetFound = true;
            return targetFound;
        }

        public Vector3D Displacement()
        {
            return TargetPosition - SelfPosition;
        }

        public double Range()
        {
            return Displacement().Length();
        }
    }

    class TargetTracker : Tracker
    {
        public TargetTracker(MyEntity newTarget)
        {
            TargetId = newTarget.EntityId;
            TargetPosition = newTarget.WorldMatrix.Translation;
            TargetVelocity = Vector3D.Zero;
            TargetLastPosition = Vector3D.Zero;
         }
    }

    class ObjectiveTracker : Tracker
    {
        public ObjectiveTracker(Vector3D position)
        {
            TargetId = 0;
            TargetPosition = position;
        }
    }
}
