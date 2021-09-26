using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowshoe_Drone_Controller_Block.DroneComponents
{
    class Settings
    {
        public IdealSettings Thrust { get; private set; }
        public IdealSettings Yaw { get; private set; }
        public IdealSettings Pitch { get; private set; }
        public IdealSettings Roll { get; private set; }

        public Settings()
        {
            Thrust = new IdealSettings(0, 0, 0);
            Yaw = new IdealSettings(0, 0, 0);
            Pitch = new IdealSettings(0, 0, 0);
            Roll = new IdealSettings(0, 0, 0);
        }

        public void SetThrust(float kp, float ki, float kd, bool antiWindup = false)
        {
            Thrust = new IdealSettings(kp, ki, kd, antiWindup);
        }
        public void SetYaw(float kp, float ki, float kd, bool antiWindup = false)
        {
            Yaw = new IdealSettings(kp, ki, kd, antiWindup);
        }
        public void SetPitch(float kp, float ki, float kd, bool antiWindup = false)
        {
            Pitch = new IdealSettings(kp, ki, kd, antiWindup);
        }
        public void SetRoll(float kp, float ki, float kd, bool antiWindup = false)
        {
            Roll = new IdealSettings(kp, ki, kd, antiWindup);
        }
    }

    public class IdealSettings
    {
        public float Kp { get; set; }
        public float Ki { get; set; }
        public float Kd { get; set; }
        public bool AntiWindup = false;

        public IdealSettings(float kp, float ki, float kd, bool antiWindup = false)
        {
            Kp = kp;
            Ki = ki;
            Kd = kd;
            AntiWindup = antiWindup;
        }
    }
}
