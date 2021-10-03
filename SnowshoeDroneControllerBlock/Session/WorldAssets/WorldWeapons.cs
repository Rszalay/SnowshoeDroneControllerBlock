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
using Snowshoe_Drone_Controller_Block;
using Snowshoe_Drone_Controller_Block.DroneComponents;
using Snowshoe_Drone_Controller_Block.TargetComp;

namespace Snowshoe_Drone_Controller_Block.Session.WorldAssets
{
    public enum WeaponType
    {
        Turret, Fixed, Launcher, None
    }
    public class WorldWeapons
    {
        public List<API.WcApiDef.WeaponDefinition> AllWcWeapons { get; private set; }
        public List<API.WcApiDef.WeaponDefinition> Turrets { get; private set; }
        public List<API.WcApiDef.WeaponDefinition> Fixed { get; private set; }
        public List<API.WcApiDef.WeaponDefinition> Launchers { get; private set; }

        public void GetAllWeapons()
        {
            AllWcWeapons = DroneControlSession.wcApi.WeaponDefinitions;
            foreach (var weapon in AllWcWeapons)
            {
                AllWcWeapons = new List<API.WcApiDef.WeaponDefinition>();
                Turrets = new List<API.WcApiDef.WeaponDefinition>();  //<subtypeName, list<entityId>>
                Fixed = new List<API.WcApiDef.WeaponDefinition>();
                Launchers = new List<API.WcApiDef.WeaponDefinition>();
                bool smart = false;
                float totalAzimuth = Math.Abs(weapon.HardPoint.HardWare.MaxAzimuth - weapon.HardPoint.HardWare.MinAzimuth);
                float totalElevation = Math.Abs(weapon.HardPoint.HardWare.MaxAzimuth - weapon.HardPoint.HardWare.MinAzimuth);
                float totalSlew = Math.Max(totalElevation, totalAzimuth);
                string AzimuthPartId = weapon.Assignments.MountPoints.First().AzimuthPartId;
                string ElevationPartId = weapon.Assignments.MountPoints.First().ElevationPartId;
                //determine if this ia a fixed weapon
                if (totalSlew < MathHelperD.ToRadians(20) || (AzimuthPartId == "None" && ElevationPartId == "None"))
                {
                    //if it is a fixed weapon check if it's a launcher
                    foreach (var ammo in weapon.Ammos)
                    {
                        if (ammo.Trajectory.Guidance == API.WcApiDef.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType.Smart)
                        {
                            smart = true;
                            break;
                        }
                    }
                    if (smart) Launchers.Add(weapon);
                    else Fixed.Add(weapon);
                }
                else Turrets.Add(weapon);
            }
        }

        public bool CheckIfWeapon(IMyCubeBlock block)
        {
            if (block is IMyTerminalBlock)
            {
                IMyTerminalBlock tBlock = block as IMyTerminalBlock;
                if (DroneControlSession.wcApi.HasCoreWeapon(tBlock)) return true;
                return false;
            }
            return false;
        }

        public void GetWeaponsOnGrid(MyCubeGrid grid, ref List<MyCubeBlock> weapons)
        {
            weapons.Clear();
            weapons = new List<MyCubeBlock>();
            foreach(var fatBlock in grid.GetFatBlocks())
            {
                if (CheckIfWeapon(fatBlock)) weapons.Add(fatBlock);
            }
        }

        public WeaponType GetWeaponType(MyCubeBlock block)
        {
            if (block is IMyTerminalBlock)
            {
                IMyTerminalBlock tblock = block as IMyTerminalBlock;
                if (!DroneControlSession.wcApi.HasCoreWeapon(tblock)) return WeaponType.None;
                foreach(var weapon in Turrets)
                {
                    foreach(var mount in weapon.Assignments.MountPoints)
                    {
                        if (mount.SubtypeId == tblock.GetObjectBuilderCubeBlock().SubtypeName) return WeaponType.Turret;
                    }
                }
                foreach (var weapon in Fixed)
                {
                    foreach (var mount in weapon.Assignments.MountPoints)
                    {
                        if (mount.SubtypeId == tblock.GetObjectBuilderCubeBlock().SubtypeName) return WeaponType.Fixed;
                    }
                }
                foreach (var weapon in Launchers)
                {
                    foreach (var mount in weapon.Assignments.MountPoints)
                    {
                        if (mount.SubtypeId == tblock.GetObjectBuilderCubeBlock().SubtypeName) return WeaponType.Launcher;
                    }
                }
            }
            return WeaponType.None;
        }

        public void GetWeaponTypeMap(MyCubeGrid grid, ref Dictionary<WeaponType, List<MyCubeBlock>> map)
        {
            if (!map.ContainsKey(WeaponType.Fixed)) map.Add(WeaponType.Fixed, new List<MyCubeBlock>());
            if (!map.ContainsKey(WeaponType.Launcher)) map.Add(WeaponType.Launcher, new List<MyCubeBlock>());
            if (!map.ContainsKey(WeaponType.Turret)) map.Add(WeaponType.Turret, new List<MyCubeBlock>());
            foreach (var fatBlock in grid.GetFatBlocks())
            {
                WeaponType type = GetWeaponType(fatBlock);
                if (type != WeaponType.None) map[type].Add(fatBlock);
            }
        }
    }
}
