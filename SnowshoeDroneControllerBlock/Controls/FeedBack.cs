using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowshoe_Drone_Controller_Block.Controls
{
    enum FeedBackType
    {
        Cubic, Exponential, Unity
    }

    class FeedBack
    {
        public virtual double Map(double error)
        {
            return error;
        }
    }

    class Unity : FeedBack
    {
        public override double Map(double error)
        {
            return base.Map(error);
        }
    }

    class Exponential : FeedBack
    {
        public override double Map(double error)
        {
            return Math.Exp(error);
        }
    }

    class Cubic : FeedBack
    {
        public override double Map(double error)
        {
            return error*error*error;
        }
    }
}
