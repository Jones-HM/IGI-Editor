using QLibc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace IGIEditor
{
    class QHuman
    {

        internal static string AddWeapon(string weapon, int ammo, bool autoModel)
        {
            string qscData = QUtils.LoadFile();
            if (!autoModel)
                weapon = QUtils.weaponId + weapon;

            QUtils.AddLog("AddWeapon()  Trying to add weapon : " + weapon + " with ammo : " + ammo);

            string idIndexStr = "Task_New(0";
            string gunIndexStr = "Task_New(-1, \"Gun\"";
            int idIndex = qscData.IndexOf(idIndexStr);
            int gunIndex = qscData.IndexOf(gunIndexStr, idIndex);

            if (CheckWeaponExist(weapon))
            {
                QUtils.ShowError("Weapon : " + weapon + " already exist for human");
                QUtils.AddLog("AddWeapon() Weapon : " + weapon + " does exist for human");
                return null;
            }

            string gun = Weapon(weapon, ammo);
            qscData = qscData.Insert(gunIndex, gun);
            return qscData;
        }


        internal static string Weapon(string weapon, int ammo)
        {
            //Primary ammo slot.
            string ammoIdPrimary = GetAmmo4Weapon(weapon);
            string ammoIdSecondary = null;

            //Secondary ammo slot.
            if (weapon.Contains("M16A2"))
                ammoIdSecondary = GetAmmo4Weapon("M203");

            string gunStr = "Gun", weaponStr = (weapon.Replace(QUtils.weaponId, String.Empty));

            //Exceptions for special weapons like Dragunov,MP5 with zoom functionality.
            if (weapon.Contains("MP5SD") || weapon.Contains("DRAGUNOV")
                || weapon.Contains("M16A2") || weapon.Contains("SPAS12"))
                gunStr += weaponStr;

            //For Mine types.
            if (weapon.Contains("PROXIMITYMINE"))
                gunStr = "ProximityMine";

            //For Binocular.
            if (weapon.Contains("BINOCULARS"))
                gunStr = "Binocular";

            string gunTask = "Task_New(-1, \"" + gunStr + "\", \"WEAPON\"," + "\"" + weapon + "\"" + ",0)," + "\n";
            string weaponTask = gunTask;

            //If ammo not found then don't add ammo task.
            if (!String.IsNullOrEmpty(ammoIdPrimary))
            {
                string ammoTaskPrimary = "Task_New(-1, \"AddAmmo\", \"AMMO\"," + "\"" + ammoIdPrimary + "\"" + "," + ammo + ")," + "\n";
                string ammoTaskSecondary = "Task_New(-1, \"AddAmmo\", \"AMMO\"," + "\"" + ammoIdSecondary + "\"" + "," + ammo + ")," + "\n";
                weaponTask += ammoTaskPrimary;

                if (!String.IsNullOrEmpty(ammoIdSecondary))
                    weaponTask += ammoTaskSecondary;
            }

            return weaponTask;
        }

        internal static string RemoveWeapon(string weapon, bool autoModel)
        {
            string qscData = QUtils.LoadFile();
            if (!autoModel)
                weapon = QUtils.weaponId + weapon;

            QUtils.AddLog("RemoveWeapon()  Trying to remove weapon : " + weapon);
            if (!CheckWeaponExist(weapon))
            {
                QUtils.ShowError("Weapon : " + weapon + " does not exist for human");
                QUtils.AddLog("RemoveWeapon() Weapon : " + weapon + " does not exist for human");
                return null;
            }

            QUtils.AddLog("RemoveWeapon()  Weapon found to remove weapon : " + weapon);
            string idIndexStr = "Task_New(0";
            int idIndex = qscData.IndexOf(idIndexStr);
            var qscTemp = qscData.Substring(idIndex).Split('\n');
            string gunSubStr = null;

            foreach (var data in qscTemp)
            {
                if (data.Contains(weapon))
                    gunSubStr = data;
            }

            QUtils.AddLog("RemoveWeapon()  Weapon string : " + gunSubStr);
            var gunIndex = qscData.LastIndexOf(gunSubStr);

            qscData = qscData.Remove(gunIndex, gunSubStr.Length);
            qscData = Regex.Replace(qscData, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            qscData = qscData.Replace("\t", String.Empty);

            if (!String.IsNullOrEmpty(qscData))
                IGIEditorUI.editorRef.SetStatusText("Weapon removed successfully");
            return qscData;
        }


        private static string GetAmmo4Weapon(string weapon)
        {
            string ammoId = null;
            if (weapon == null) { QUtils.ShowError("GetAmmo4Weapon : Weapon name not provided"); return null; }
            weapon = weapon.Replace(QUtils.weaponId, String.Empty);

            foreach (var ammo in QUtils.ammoList)
            {
                if (ammo.Key.Contains(weapon))
                {
                    ammoId = ammo.Value;
                    break;
                }
            }
            QUtils.AddLog("GetAmmo4Weapon() weapon : " + weapon + " with ammo : " + ammoId);

            return ammoId;
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

            var humanData = GetHumanTaskList();
            bool found = false;
            foreach (var humanWeapon in humanData.weaponsList)
            {
                QUtils.AddLog("CheckWeaponExist() : Weapon_List : " + humanWeapon);
                if (humanWeapon.Contains(weapon))
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
            htask.weaponsList = new List<string>();

            Real32 orientation = new Real32();
            Real64 position = new Real64();

            string qscData = QUtils.LoadFile();

            string idIndexStr = "Task_New(0";
            int idIndex = qscData.IndexOf(idIndexStr);
            string qscTemp = qscData.Substring(idIndex);
            string[] taskNew = qscTemp.Split(',');

            //Parse all the data.
            position.x = Double.Parse(taskNew[(int)QTASKINFO.QTASK_POSX]);
            position.y = Double.Parse(taskNew[(int)QTASKINFO.QTASK_POSY]);
            position.z = Double.Parse(taskNew[(int)QTASKINFO.QTASK_POSZ]);
            orientation.alpha = float.Parse(taskNew[(int)QTASKINFO.QTASK_ALPHA]);
            htask.team = Convert.ToInt32(taskNew[(int)QTASKINFO.QTASK_GAMMA].Trim());

            //Adding position and orientation to qtask.
            htask.qtask.position = position;
            htask.qtask.orientation = orientation;

            string weaponRegex = "[A-Z]{6}_[A-Z]{2}_[A-Z0-9]*";
            var qscSub = qscData.Substring(idIndex).Split('\n');
            int weaponsIndex = 0;
            int maxWeapons = 0xA;

            foreach (var data in qscSub)
            {
                var matchData = Regex.Match(data, weaponRegex);
                if (matchData.Success)
                    htask.weaponsList.Add(matchData.Value);

                //Break after reaching max weapons limit.
                if (weaponsIndex > maxWeapons) break;
                weaponsIndex++;
            }
            return htask;
        }

        static internal Real32 GetPositionAuto(bool addLog = true)
        {
            uint posBaseAddr = (uint)QMemory.GetHumanBaseAddress(false) + (uint)0x24;

            IntPtr xPosAddr = (IntPtr)posBaseAddr + 0x0;
            IntPtr yPosAddr = (IntPtr)posBaseAddr + 0x8;
            IntPtr zPosAddr = (IntPtr)posBaseAddr + 0x10;

            var xpos = GT.GT_ReadFloat(xPosAddr);
            var ypos = GT.GT_ReadFloat(yPosAddr);
            var zpos = GT.GT_ReadFloat(zPosAddr);

            var position = new Real32(xpos, ypos, zpos);

            if (addLog)
            {
                QUtils.AddLog("GetPositionAuto() : posBaseAddr : " + posBaseAddr);
                QUtils.AddLog("GetPositionAuto() xpos : " + xpos);
                QUtils.AddLog("GetPositionAuto() ypos : " + ypos);
                QUtils.AddLog("GetPositionAuto() zpos : " + zpos);
                QUtils.AddLog("GetPositionAuto() : position: " + position);
            }
            return position;
        }

        static internal Real64 GetPositionInMeter(bool addLog = true)
        {
            uint posBaseAddr = (uint)0x005CA138;
            IntPtr xPosAddr = (IntPtr)posBaseAddr + 0x0;
            IntPtr yPosAddr = (IntPtr)posBaseAddr + 0x8;
            IntPtr zPosAddr = (IntPtr)posBaseAddr + 0x10;

            var xpos = GT.GT_ReadDouble(xPosAddr);
            var ypos = GT.GT_ReadDouble(yPosAddr);
            var zpos = GT.GT_ReadDouble(zPosAddr);

            double x = Convert.ToDouble(Decimal.Truncate(Convert.ToDecimal(xpos)));
            double y = Convert.ToDouble(Decimal.Truncate(Convert.ToDecimal(ypos)));
            double z = Convert.ToDouble(Decimal.Truncate(Convert.ToDecimal(zpos)));

            //Fix this angle for Ground reference.
            var position = new Real64(x, y, z - QMemory.deltaToGround);
            if (addLog)
            {
                QUtils.AddLog("GetPositionInMeter() : xpos: " + xpos + " ypos: " + ypos + " zpos: " + zpos);
                QUtils.AddLog("GetPositionInMeter() : x: " + x + " y: " + y + " z: " + z);
                QUtils.AddLog("GetPositionInMeter() : position: " + position);
            }

            return position;
        }

        internal static string UpdatePositionInMeter(Real64 position, float angle = 0.0f)
        {
            var humanData = GetHumanTaskList();
            string qscData = QUtils.LoadFile();

            QUtils.AddLog("Human UpdatePositionInMeter() called with position : X:" + position.x + " Y: " + position.y + " Z: " + position.z + ", Alpha : " + angle);
            string humanAngle = angle == 0.0f ? humanData.qtask.orientation.alpha.ToString() : angle.ToString("0.0");

            string humanTaskId = "Task_New(0";
            int qtaskIndex = qscData.IndexOf(humanTaskId);
            int newlineIndex = qscData.IndexOf("\n", qtaskIndex);

            string humanTask = "Task_New(0,\"HumanPlayer\",\"Jones\"," + position.x + "," + position.y + "," + position.z + "," + humanAngle + ",\"000_01_1\",0,";
            qscData = qscData.Remove(qtaskIndex, newlineIndex - qtaskIndex).Insert(qtaskIndex, humanTask);
            return qscData;
        }

        internal static string UpdatePositionOffset(Real64 position, float alpha = 0.0f)
        {
            var humanData = GetHumanTaskList();
            bool xVal = (position.x == 0.0f) ? false : true;
            bool yVal = (position.y == 0.0f) ? false : true;
            bool zVal = (position.z == 0.0f) ? false : true;

            int xLen = xVal ? position.x.ToString().Length : 0;
            int yLen = yVal ? position.y.ToString().Length : 0;
            int zLen = zVal ? position.z.ToString().Length : 0;

            QUtils.AddLog("Human UpdatePositionOffset()  length : X:" + xLen + " Y: " + yLen + " Z: " + zLen);
            QUtils.AddLog("Human UpdatePositionOffset() called with offset : X:" + position.x + " Y: " + position.y + " Z: " + position.z);


            //Check for length error.
            if (xLen > 3 || yLen > 3 || zLen > 3)
                throw new ArgumentOutOfRangeException("Offsets are out of range");

            int[] meterOffsets = { 100000, 1000000, 10000000 };

            //Add meter offset to distance. (M/S) .
            if (xVal) position.x = humanData.qtask.position.x + meterOffsets[xLen - 1];
            if (yVal) position.y = humanData.qtask.position.y + meterOffsets[yLen - 1];
            if (zVal) position.z = humanData.qtask.position.z + meterOffsets[zLen - 1];

            string humanXPos = position.x == 0.0f ? humanData.qtask.position.x.ToString("0.0") : position.x.ToString("0.0");
            string humanYPos = position.y == 0.0f ? humanData.qtask.position.y.ToString("0.0") : position.y.ToString("0.0");
            string humanZPos = position.z == 0.0f ? humanData.qtask.position.z.ToString("0.0") : position.z.ToString("0.0");
            string humanAlpha = alpha == 0.0f ? humanData.qtask.orientation.alpha.ToString() : alpha.ToString("0.0");


            string qscData = QUtils.LoadFile();

            QUtils.AddLog("Human UpdatePositionOffset() calculated positions with offsets : X:" + position.x + " Y: " + position.y + " Z: " + position.z);

            string humanTaskId = "Task_New(0";
            int qtaskIndex = qscData.IndexOf(humanTaskId);
            int newlineIndex = qscData.IndexOf("\n", qtaskIndex);

            string humanTask = "Task_New(0,\"HumanPlayer\",\"Jones\"," + humanXPos + "," + humanYPos + "," + humanZPos + "," + humanAlpha + ",\"000_01_1\",0,";
            qscData = qscData.Remove(qtaskIndex, newlineIndex - qtaskIndex).Insert(qtaskIndex, humanTask);
            return qscData;
        }

        internal static string UpdateOrientation(float alpha)
        {
            var humanData = GetHumanTaskList();
            QUtils.AddLog("Human UpdateOrientation() called with alpha : " + alpha);
            return UpdatePositionInMeter(humanData.qtask.position, alpha);
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
            var humanPlayerFile = QUtils.cfgHumanplayerPath + @"\humanplayer" + QUtils.qscExt;
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
