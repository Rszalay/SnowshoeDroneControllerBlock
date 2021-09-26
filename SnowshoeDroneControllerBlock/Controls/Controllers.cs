using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snowshoe_Drone_Controller_Block.DroneComponents;

namespace Snowshoe_Drone_Controller_Block.Controls
{
    enum ControllerType
    {
        Ideal, PID, PI, Proportional
    }

    class Ideal : ControllerBase
    {
        public Ideal(IdealSettings settings)
        {
            controllers.Add(new Proportional(settings.Kp));
            controllers.Add(new Integral(settings.Ki));
            controllers.Add(new Derivative(settings.Kd));
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
