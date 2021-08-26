using QLibc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace IGIEditor
{
    class QHuman
    {

        internal static string AddWeapon(string weapon, int ammo, bool auto_model)
        {
            string qsc_data = QUtils.LoadFile();
            if (!auto_model)
                weapon = QUtils.weaponId + weapon;

            QUtils.AddLog("AddWeapon()  Trying to add weapon : " + weapon + " with ammo : " + ammo);

            string id_index_str = "Task_New(0";
            string gun_index_str = "Task_New(-1, \"Gun\"";
            int id_index = qsc_data.IndexOf(id_index_str);
            int gun_index = qsc_data.IndexOf(gun_index_str, id_index);

            if (CheckWeaponExist(weapon))
            {
                QUtils.ShowError("Weapon : " + weapon + " already exist for human");
                QUtils.AddLog("AddWeapon() Weapon : " + weapon + " does exist for human");
                return null;
            }

            string gun = Weapon(weapon, ammo);
            qsc_data = qsc_data.Insert(gun_index, gun);
            return qsc_data;
        }


        internal static string Weapon(string weapon, int ammo)
        {
            //Primary ammo slot.
            string ammo_id_primary = GetAmmo4Weapon(weapon);
            string ammo_id_secondary = null;

            //Secondary ammo slot.
            if (weapon.Contains("M16A2"))
                ammo_id_secondary = GetAmmo4Weapon("M203");

            string gun_str = "Gun", weapon_str = (weapon.Replace(QUtils.weaponId, String.Empty));

            //Exceptions for special weapons like Dragunov,MP5 with zoom functionality.
            if (weapon.Contains("MP5SD") || weapon.Contains("DRAGUNOV")
                || weapon.Contains("M16A2") || weapon.Contains("SPAS12"))
                gun_str += weapon_str;

            //For Mine types.
            if (weapon.Contains("PROXIMITYMINE"))
                gun_str = "ProximityMine";

            //For Binocular.
            if (weapon.Contains("BINOCULARS"))
                gun_str = "Binocular";

            string gun_task = "Task_New(-1, \"" + gun_str + "\", \"WEAPON\"," + "\"" + weapon + "\"" + ",0)," + "\n";
            string weapon_task = gun_task;

            //If ammo not found then don't add ammo task.
            if (!String.IsNullOrEmpty(ammo_id_primary))
            {
                string ammo_task_primary = "Task_New(-1, \"AddAmmo\", \"AMMO\"," + "\"" + ammo_id_primary + "\"" + "," + ammo + ")," + "\n";
                string ammo_task_secondary = "Task_New(-1, \"AddAmmo\", \"AMMO\"," + "\"" + ammo_id_secondary + "\"" + "," + ammo + ")," + "\n";
                weapon_task += ammo_task_primary;

                if (!String.IsNullOrEmpty(ammo_id_secondary))
                    weapon_task += ammo_task_secondary;
            }

            return weapon_task;
        }

        internal static string RemoveWeapon(string weapon, bool auto_model)
        {
            string qsc_data = QUtils.LoadFile();
            if (!auto_model)
                weapon = QUtils.weaponId + weapon;

            QUtils.AddLog("RemoveWeapon()  Trying to remove weapon : " + weapon);
            if (!CheckWeaponExist(weapon))
            {
                QUtils.ShowError("Weapon : " + weapon + " does not exist for human");
                QUtils.AddLog("RemoveWeapon() Weapon : " + weapon + " does not exist for human");
                return null;
            }

            QUtils.AddLog("RemoveWeapon()  Weapon found to remove weapon : " + weapon);
            string id_index_str = "Task_New(0";
            int id_index = qsc_data.IndexOf(id_index_str);
            var qsc_temp = qsc_data.Substring(id_index).Split('\n');
            string gun_sub_str = null;

            foreach (var data in qsc_temp)
            {
                if (data.Contains(weapon))
                    gun_sub_str = data;
            }

            QUtils.AddLog("RemoveWeapon()  Weapon string : " + gun_sub_str);
            var gun_index = qsc_data.LastIndexOf(gun_sub_str);

            qsc_data = qsc_data.Remove(gun_index, gun_sub_str.Length);
            qsc_data = Regex.Replace(qsc_data, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            qsc_data = qsc_data.Replace("\t", String.Empty);

            if (!String.IsNullOrEmpty(qsc_data))
                IGIEditorUI.editorRef.SetStatusText("Weapon removed successfully");
            return qsc_data;
        }


        private static string GetAmmo4Weapon(string weapon)
        {
            string ammo_id = null;
            if (weapon == null) { QUtils.ShowError("GetAmmo4Weapon : Weapon name not provided"); return null; }
            weapon = weapon.Replace(QUtils.weaponId, String.Empty);

            foreach (var ammo in QUtils.ammoList)
            {
                if (ammo.Key.Contains(weapon))
                {
                    ammo_id = ammo.Value;
                    break;
                }
            }
            QUtils.AddLog("GetAmmo4Weapon() weapon : " + weapon + " with ammo : " + ammo_id);

            return ammo_id;
        }

        internal static List<Dictionary<string, string>> GetWeaponsList()
        {
            var weaponsList = new List<Dictionary<string, string>>();
            foreach (var weapon in QUtils.weaponsList)
            {
                var weaponObj = new Dictionary<string, string>();
                weaponObj.Add(weapon, QUtils.weaponId + weapon);
                weaponsList.Add(weaponObj);
            }
            return weaponsList;
        }

        private static bool CheckWeaponExist(string weapon)
        {
            weapon = weapon.Replace(QUtils.weaponId, String.Empty);
            QUtils.AddLog("CheckWeaponExist() : checking for weapon : " + weapon);

            var human_data = GetHumanTaskList();
            bool found = false;
            foreach (var human_weapon in human_data.weapons_list)
            {
                QUtils.AddLog("CheckWeaponExist() : Weapon_List : " + human_weapon);
                if (human_weapon.Contains(weapon))
                {
                    found = true;
                    break;
                }
            }
            QUtils.AddLog("CheckWeaponExist() returned : " + found.ToString());
            return found;
        }

        internal static QUtils.HTask GetHumanTaskList()
        {
            //Declare types to store position to qtask.
            QUtils.HTask htask = new QUtils.HTask();
            htask.qtask = new QUtils.QTask();
            htask.weapons_list = new List<string>();

            Real32 orientation = new Real32();
            Real64 position = new Real64();

            string qsc_data = QUtils.LoadFile();

            string id_index_str = "Task_New(0";
            int id_index = qsc_data.IndexOf(id_index_str);
            string qsc_temp = qsc_data.Substring(id_index);
            string[] task_new = qsc_temp.Split(',');

            //Parse all the data.
            position.x = Double.Parse(task_new[(int)QTASKINFO.QTASK_POSX]);
            position.y = Double.Parse(task_new[(int)QTASKINFO.QTASK_POSY]);
            position.z = Double.Parse(task_new[(int)QTASKINFO.QTASK_POSZ]);
            orientation.alpha = float.Parse(task_new[(int)QTASKINFO.QTASK_ALPHA]);
            htask.team = Convert.ToInt32(task_new[(int)QTASKINFO.QTASK_GAMMA].Trim());

            //Adding position and orientation to qtask.
            htask.qtask.position = position;
            htask.qtask.orientation = orientation;

            string weapon_regex = "[A-Z]{6}_[A-Z]{2}_[A-Z0-9]*";
            var qsc_sub = qsc_data.Substring(id_index).Split('\n');
            int weapons_index = 0;
            int max_weapons = 0xA;

            foreach (var data in qsc_sub)
            {
                var match_data = Regex.Match(data, weapon_regex);
                if (match_data.Success)
                    htask.weapons_list.Add(match_data.Value);

                //Break after reaching max weapons limit.
                if (weapons_index > max_weapons) break;
                weapons_index++;
            }
            return htask;
        }

        internal static string UpdatePositionNoOffset(Real64 position, float angle = 0.0f)
        {
            var human_data = GetHumanTaskList();
            string qsc_data = QUtils.LoadFile();

            QUtils.AddLog("UpdateHumanPositionNoOffset() called with position : X:" + position.x + " Y: " + position.y + " Z: " + position.z + ", Alpha : " + angle);
            string human_angle = angle == 0.0f ? human_data.qtask.orientation.alpha.ToString() : angle.ToString("0.0");

            string human_task_id = "Task_New(0";
            int qtask_index = qsc_data.IndexOf(human_task_id);
            int newline_index = qsc_data.IndexOf("\n", qtask_index);

            string human_task = "Task_New(0,\"HumanPlayer\",\"Jones\"," + position.x + "," + position.y + "," + position.z + "," + human_angle + ",\"000_01_1\",0,";
            qsc_data = qsc_data.Remove(qtask_index, newline_index - qtask_index).Insert(qtask_index, human_task);
            return qsc_data;
        }

        internal static string UpdatePositionOffset(Real64 position, float alpha = 0.0f)
        {
            var human_data = GetHumanTaskList();
            bool x_val = (position.x == 0.0f) ? false : true;
            bool y_val = (position.y == 0.0f) ? false : true;
            bool z_val = (position.z == 0.0f) ? false : true;

            int x_len = x_val ? position.x.ToString().Length : 0;
            int y_len = y_val ? position.y.ToString().Length : 0;
            int z_len = z_val ? position.z.ToString().Length : 0;

            QUtils.AddLog("Update PositionOffset()  length : X:" + x_len + " Y: " + y_len + " Z: " + z_len);
            QUtils.AddLog("Update PositionOffset() called with offset : X:" + position.x + " Y: " + position.y + " Z: " + position.z);


            //Check for length error.
            if (x_len > 3 || y_len > 3 || z_len > 3)
                throw new ArgumentOutOfRangeException("Offsets are out of range");

            int[] meter_offsets = { 100000, 1000000, 10000000 };

            //Add meter offset to distance. (M/S) .
            if (x_val) position.x = human_data.qtask.position.x + meter_offsets[x_len - 1];
            if (y_val) position.y = human_data.qtask.position.y + meter_offsets[y_len - 1];
            if (z_val) position.z = human_data.qtask.position.z + meter_offsets[z_len - 1];

            string human_x_pos = position.x == 0.0f ? human_data.qtask.position.x.ToString("0.0") : position.x.ToString("0.0");
            string human_y_pos = position.y == 0.0f ? human_data.qtask.position.y.ToString("0.0") : position.y.ToString("0.0");
            string human_z_pos = position.z == 0.0f ? human_data.qtask.position.z.ToString("0.0") : position.z.ToString("0.0");
            string human_alpha = alpha == 0.0f ? human_data.qtask.orientation.alpha.ToString() : alpha.ToString("0.0");


            string qsc_data = QUtils.LoadFile();

            QUtils.AddLog("Update PositionOffset() calculated positions with offsets : X:" + position.x + " Y: " + position.y + " Z: " + position.z);

            string human_task_id = "Task_New(0";
            int qtask_index = qsc_data.IndexOf(human_task_id);
            int newline_index = qsc_data.IndexOf("\n", qtask_index);

            string human_task = "Task_New(0,\"HumanPlayer\",\"Jones\"," + human_x_pos + "," + human_y_pos + "," + human_z_pos + "," + human_alpha + ",\"000_01_1\",0,";
            qsc_data = qsc_data.Remove(qtask_index, newline_index - qtask_index).Insert(qtask_index, human_task);
            return qsc_data;
        }

        internal static string UpdateOrientation(float alpha)
        {
            var human_data = GetHumanTaskList();
            QUtils.AddLog("UpdateOrientation() called with alpha : " + alpha);
            return UpdatePositionNoOffset(human_data.qtask.position, alpha);
        }

        internal static void UpdateHumanPlayerSpeed(double speedX = 1.75f, double speedY = 17.5f, double speedZ = 27)
        {
            QUtils.AddLog("UpdateHumanPlayerSpeed() : speedX: " + speedX + " speedY: " + speedY + " speedZ: " + speedZ);
            UpdateHumanPlayerParams(speedX, speedY, speedZ, QUtils.inAirVel1, QUtils.inAirVel2, QUtils.healthScale1, QUtils.healthScale2, QUtils.healthScale3);
        }

        internal static void UpdateHumanPlayerAirVel(double inAirVel1 = 0.5f, double inAirVel2 = 0.8500000238418579f)
        {
            QUtils.AddLog("UpdateHumanPlayerAirVel() inAirVel1: " + inAirVel1 + " inAirVel2: " + inAirVel2);
            UpdateHumanPlayerParams(QUtils.speedX, QUtils.speedY, QUtils.speedZ, inAirVel1, inAirVel2);
        }

        internal static void UpdateHumanPlayerHealth(double healthScale1 = 3.0f, double healthScale2 = 0.5f, double healthScale3 = 0.5f)
        {
            QUtils.AddLog("UpdateHumanPlayerHealth() : healthScale1: " + healthScale1 + " healthScale2: " + healthScale2 + " healthScale3: " + healthScale3);
            UpdateHumanPlayerParams(QUtils.speedX, QUtils.speedY, QUtils.speedZ, QUtils.inAirVel1, QUtils.inAirVel2, healthScale1, healthScale2, healthScale3);
        }

        internal static void UpdateHumanPlayerParams(double speedX = 1.75f, double speedY = 17.5f, double speedZ = 27, double inAirVel1 = 0.5f, double inAirVel2 = 0.8500000238418579f, double healthScale1 = 3.0f, double healthScale2 = 0.5f, double healthScale3 = 0.5f)
        {
            var humanPlayerFile = QUtils.cfgInputHumanplayerPath + @"\humanplayer" + QUtils.qscExt;
            string humanPlayerData = QCryptor.Decrypt(humanPlayerFile);
            QUtils.AddLog("UpdateHumanPlayerParams() : speedX: " + speedX + " speedY: " + speedY + " speedZ: " + speedZ + " inAirVel1: " + inAirVel1 + " inAirVel2: " + inAirVel2 + " healthScale1: " + healthScale1 + " healthScale2: " + healthScale2 + " healthScale3: " + healthScale3);

            //Add speedX param.
            if (humanPlayerData.Contains(QUtils.speedXMask))
                humanPlayerData = humanPlayerData.ReplaceFirst(QUtils.speedXMask, speedX.ToString());

            //Add speedY param.
            if (humanPlayerData.Contains(QUtils.speedYMask))
                humanPlayerData = humanPlayerData.ReplaceFirst(QUtils.speedYMask, speedY.ToString());

            //Add speedZ param.
            if (humanPlayerData.Contains(QUtils.speedZMask))
                humanPlayerData = humanPlayerData.ReplaceFirst(QUtils.speedZMask, speedZ.ToString());

            //Add inAirVel1 param.
            if (humanPlayerData.Contains(QUtils.inAir1Mask))
                humanPlayerData = humanPlayerData.ReplaceFirst(QUtils.inAir1Mask, inAirVel1.ToString());

            //Add inAirVel2 param.
            if (humanPlayerData.Contains(QUtils.inAir2Mask))
                humanPlayerData = humanPlayerData.ReplaceFirst(QUtils.inAir2Mask, inAirVel2.ToString());

            //Add healthScale1 param.
            if (humanPlayerData.Contains(QUtils.health1Mask))
                humanPlayerData = humanPlayerData.ReplaceFirst(QUtils.health1Mask, healthScale1.ToString());

            //Add healthScale2 param.
            if (humanPlayerData.Contains(QUtils.health2Mask))
                humanPlayerData = humanPlayerData.ReplaceFirst(QUtils.health2Mask, healthScale2.ToString());

            //Add healthScale3 param.
            if (humanPlayerData.Contains(QUtils.health3Mask))
                humanPlayerData = humanPlayerData.ReplaceFirst(QUtils.health3Mask, healthScale3.ToString());

            string humanFileName = "humanplayer.qsc";
            var outputHumanplayerPath = QUtils.gameAbsPath + "\\humanplayer\\";

            QUtils.SaveFile(humanFileName, humanPlayerData);
            bool status = QCompiler.Compile(humanFileName, outputHumanplayerPath, 0x0);
            System.IO.File.Delete(humanFileName);

            if (status)
            {
                Thread.Sleep(1000);
                GT.GT_SendKeys2Process(QMemory.gameName, "^h", true);
                QMemory.SetStatusMsgText("Human parameters set success");
            }
        }
    }
}
