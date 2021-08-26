using System;
using System.Collections.Generic;


namespace IGIEditor
{
    enum WEAPONCFG
    {
        //ID(s) in //Int32
        WEAPON_ID = 3,
        //Properties in String256
        SCRIPT_ID,
        WEAPON_NAME,
        WEAPON_MANUFACTURER,
        WEAPON_DESCRIPTION,

        //Properties in EnumInt32
        WEAPON_TYPE_ENUM,
        WEAPON_CROSSHAIR_TYPE,
        AMMO_DISPLAY_TYPE,

        //Properties in int32.
        WEAPON_MASS,//in grams.
        WEAPON_CALIBER_ID,

        //Properties in Real32.
        WEAPON_DAMAGE,
        WEAPON_POWER,
        WEAPON_RELOAD_TIME,
        WEAPON_MUZZLE_VEL,//Muzzle velocity.

        //Properties in Int32.
        WEAPON_BULLETS,
        WEAPON_RPM, //Rounds per minute.
        WEAPON_CLIPS, //Magazine
        WEAPON_BURST,

        //Properties in Real32.
        WEAPON_MIN_RAND_SPEED,
        WEAPON_MAX_RAND_SPEED,
        WEAPON_FIX_VIEW_CHANGE_X, //Fix view X-Axis
        WEAPON_FIX_VIEW_CHANGE_Z, //Fix view Z-Axis
        WEAPON_RAND_VIEW_CHANGE_X, //Rand view Z-Axis
        WEAPON_RAND_VIEW_CHANGE_Z, //Rand view Z-Axis

        WEAPON_TYPE_STR, //String256
        WEAPON_RANGE,//Real32
        WEAPON_USERS,//String256

        //Properties in int32.
        WEAPON_LENGTH,
        BARREL_LENGTH,

        //Properties in String16
        WEAPON_GUN_MODEL,
        WEAPON_CASING_MODEL,

        //Animations in int32.
        WEAPON_STAND_ANIM,
        WEAPON_MOVE_ANIM,
        WEAPON_FIRE_ANIM1,
        WEAPON_FIRE_ANIM2,
        WEAPON_FIRE_ANIM3,
        WEAPON_RELOAD_ANIM,
        WEAPON_UPPERBODYSTAND_ANIM,
        WEAPON_UPPERBODYWALK_ANIM,
        WEAPON_UPPERBODYCROUCH_ANIM,
        WEAPON_UPPERBODYCROUCHRUN_ANIM,
        WEAPON_UPPERBODYRUN_ANIM,
        WEAPON_UPPERBODYFIRE_ANIM,
        WEAPON_UPPERBODYRELOAD_ANIM,

        //Sound in String16
        WEAPON_SOUND_SINGLE,
        WEAPON_SOUND_LOOP,

        WEAPON_DETECTION_RANGE,//EnumReal32
        WEAPON_TASK_TYPE,//EnumInt32
        WEAPON_EMPTY_ON_CLEAR,//bool8
    }

    public class Weapon
    {
        //Properties in int32.
        public Int32 weapon_id, mass, caliber_id, bullets, rpm, clips, burst, weapon_length, barrel_length,
            anim_stand, anim_move, anim_fire1, anim_fire2, anim_fire3, anim_reload, anim_upperbodystand,
            anim_upperbodywalk, anim_, anim_upperbodycrouch, anim_upperbodycrouchrun, anim_upperbodyrun, anim_upperbodyfire, anim_upperbodyreload;

        //Properties in Real32.
        public float damage, power, reload_time, muzzle_velocity, min_rand_speed, max_range_speed, range, fix_view_x, fix_view_z, rand_view_x, rand_view_z;

        //Properties in String256
        public string script_id, name, manufacturer, description, type_str, users, gun_model, casing_model, sound_single, sound_loop;

        //Properties in EnumInt32
        public string type_enum, crosshair_type, ammo_disp_type, task_type;

        //Properties in EnumReal32
        public string detection_range;

        //Properties in bool8
        public bool empty_on_clear;
    };

    class QWeapon
    {
        private static string weapons_config_in = QUtils.currPath + QUtils.inputQscPath + QUtils.weaponsDirPath + "\\" + QUtils.weaponConfigQSC;
        private static string weapons_config_out = QUtils.gameAbsPath + QUtils.weaponsDirPath;
        private static string weapon_cfg_task = "Task_New(-1, \"WeaponConfig\",";

        internal static List<Weapon> GetWeaponTaskList(bool advance_data = false)
        {
            var qsc_data = LoadWeaponsConfig();
            var weapon_task_list = ParseWeaponConfig(qsc_data, advance_data);
            return weapon_task_list;
        }

        private static string LoadWeaponsConfig()
        {
            QUtils.AddLog("LoadWeaponsConfig() : weapons_config_in : " + weapons_config_in);
            QUtils.AddLog("LoadWeaponsConfig() : weapons_config_out : " + weapons_config_out);
            return QUtils.LoadFile(weapons_config_in);
        }

        //Parse the Objects.
        private static List<Weapon> ParseWeaponConfig(string qsc_data, bool advance_data = false)
        {
            //Remove all whitespaces.
            qsc_data = qsc_data.Replace("\t", String.Empty);
            var qsc_data_split = qsc_data.Split('\n');

            QUtils.AddLog("ParseWeaponConfig() started ");

            var weapon_list = new List<Weapon>();
            foreach (var data in qsc_data_split)
            {
                if (data.Contains(QUtils.taskNew))
                {
                    var start_index = data.IndexOf(',') + 1;
                    var end_index = data.IndexOf(',', start_index);
                    var task_name = data.Slice(start_index, end_index).Trim().Replace("\"", String.Empty);

                    if (String.Compare(task_name, "WeaponConfig") == 0)
                    {
                        Weapon weapon = new Weapon();

                        var task_new = data.Split(',');
                        int task_index = 0;//Skip Task_New("Task_Id","TaskType", "Task_Note"

                        foreach (var task in task_new)
                        {
                            // QUtils.AddLog("ParseWeaponConfig() task_name : " + task_name + " task_index " + task_index + " data  : " + task.Trim());

                            if (task_index == (int)WEAPONCFG.WEAPON_ID)
                                weapon.weapon_id = Convert.ToInt32(task.Trim());

                            else if (task_index == (int)WEAPONCFG.SCRIPT_ID)
                                weapon.script_id = task.Trim();

                            else if (task_index == (int)WEAPONCFG.WEAPON_NAME)
                                weapon.name = task.Trim();

                            else if (task_index == (int)WEAPONCFG.WEAPON_MANUFACTURER)
                                weapon.manufacturer = task.Trim();

                            else if (task_index == (int)WEAPONCFG.WEAPON_TYPE_ENUM)
                                weapon.type_enum = task.Trim();

                            else if (task_index == (int)WEAPONCFG.AMMO_DISPLAY_TYPE)
                                weapon.ammo_disp_type = task.Trim();

                            else if (task_index == (int)WEAPONCFG.WEAPON_MASS)
                                weapon.mass = Convert.ToInt32(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_CALIBER_ID)
                                weapon.caliber_id = Convert.ToInt32(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_DAMAGE)
                                weapon.damage = float.Parse(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_POWER)
                                weapon.power = float.Parse(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_RELOAD_TIME)
                                weapon.reload_time = float.Parse(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_MUZZLE_VEL)
                                weapon.muzzle_velocity = float.Parse(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_DETECTION_RANGE)
                                weapon.range = float.Parse(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_BULLETS)
                                weapon.bullets = Convert.ToInt32(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_RPM)
                                weapon.rpm = Convert.ToInt32(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_CLIPS)
                                weapon.clips = Convert.ToInt32(task.Trim());

                            else if (task_index == (int)WEAPONCFG.WEAPON_BURST)
                                weapon.burst = Convert.ToInt32(task.Trim());

                            //Parse advance data such as sounds animations and models.
                            if (advance_data)
                            {
                                //Length and gun models and sounds.
                                if (task_index == (int)WEAPONCFG.WEAPON_LENGTH)
                                    weapon.weapon_length = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.BARREL_LENGTH)
                                    weapon.barrel_length = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_GUN_MODEL)
                                    weapon.gun_model = task.Trim();

                                else if (task_index == (int)WEAPONCFG.WEAPON_CASING_MODEL)
                                    weapon.casing_model = task.Trim();

                                else if (task_index == (int)WEAPONCFG.WEAPON_SOUND_SINGLE)
                                    weapon.sound_single = task.Trim();

                                else if (task_index == (int)WEAPONCFG.WEAPON_SOUND_LOOP)
                                    weapon.sound_loop = task.Trim();

                                //Speed & view change.
                                else if (task_index == (int)WEAPONCFG.WEAPON_MIN_RAND_SPEED)
                                    weapon.min_rand_speed = float.Parse(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_MAX_RAND_SPEED)
                                    weapon.max_range_speed = float.Parse(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_FIX_VIEW_CHANGE_X)
                                    weapon.fix_view_x = float.Parse(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_FIX_VIEW_CHANGE_Z)
                                    weapon.fix_view_z = float.Parse(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_RAND_VIEW_CHANGE_X)
                                    weapon.rand_view_x = float.Parse(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_RAND_VIEW_CHANGE_Z)
                                    weapon.rand_view_z = float.Parse(task.Trim());


                                //Animations of fire reload/walk etc.
                                else if (task_index == (int)WEAPONCFG.WEAPON_FIRE_ANIM1)
                                    weapon.anim_fire1 = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_FIRE_ANIM2)
                                    weapon.anim_fire2 = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_FIRE_ANIM3)
                                    weapon.anim_fire3 = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_STAND_ANIM)
                                    weapon.anim_stand = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_MOVE_ANIM)
                                    weapon.anim_move = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_RELOAD_ANIM)
                                    weapon.anim_reload = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_UPPERBODYWALK_ANIM)
                                    weapon.anim_upperbodywalk = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_UPPERBODYSTAND_ANIM)
                                    weapon.anim_upperbodystand = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_UPPERBODYRUN_ANIM)
                                    weapon.anim_upperbodyrun = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_UPPERBODYRELOAD_ANIM)
                                    weapon.anim_upperbodyreload = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_UPPERBODYFIRE_ANIM)
                                    weapon.anim_upperbodyfire = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_UPPERBODYCROUCH_ANIM)
                                    weapon.anim_upperbodycrouch = Convert.ToInt32(task.Trim());

                                else if (task_index == (int)WEAPONCFG.WEAPON_UPPERBODYCROUCHRUN_ANIM)
                                    weapon.anim_upperbodycrouchrun = Convert.ToInt32(task.Trim());

                                //Empty weapon selected.
                                else if (task_index == (int)WEAPONCFG.WEAPON_EMPTY_ON_CLEAR)
                                    weapon.empty_on_clear = Convert.ToBoolean(task.Trim());
                            }


                            task_index++;
                        }
                        weapon_list.Add(weapon);
                    }
                }
            }
            QUtils.AddLog("ParseWeaponConfig() returned list with length :  " + weapon_list.Count);
            return weapon_list;
        }
    }

}
