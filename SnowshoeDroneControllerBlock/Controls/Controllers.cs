using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowshoe_Drone_Controller_Block.Controls
{
    enum ControllerType
    {
        Ideal, PID, PI, Proportional
    }

    class Ideal : ControllerBase
    {
        public Ideal(float kp, float ki, float kd)
        {
            controllers.Add(new Proportional(kp));
            controllers.Add(new Integral(ki));
            controllers.Add(new Derivative(kd));
        }

        public override double Run(double error)
        {
            double proportional = controllers[0].Run(error);
            double acc = 0;
            acc += proportional;
            acc += controllers[1].Run(proportional);//integral
            acc += controllers[2].Run(proportional);//derivative
            return acc;
        }
    }
}
