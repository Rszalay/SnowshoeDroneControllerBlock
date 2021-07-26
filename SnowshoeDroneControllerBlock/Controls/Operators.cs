using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowshoe_Drone_Controller_Block.Controls
{
    class Proportional : ControllerBase
    {
        double _kp = 0;

        public Proportional(double kp)
        {
            _kp = kp;
        }
        public override double Run(double error)
        {
            return error * _kp;
        }
    }

    class Integral : ControllerBase
    {
        double _ki = 0;
        double acc = 0;

        public Integral(double ki)
        {
            _ki = ki;
        }
        public override double Run(double error)
        {
            acc += error;
            return acc * _ki * ticksSinceLastRun;
        }
        public override void Reset()
        {
            acc = 0;
        }
    }

    class Derivative : ControllerBase
    {
        double _kd = 0;
        double last = 0;

        public Derivative(double kd)
        {
            _kd = kd;
        }
        public override double Run(double error)
        {
            double derivative = error - last;
            last = error;
            return (derivative * _kd) / ticksSinceLastRun;
        }
    }
}
