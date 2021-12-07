using QLibc;
using System.IO;
using System.Threading;
using static QLibc.GT;

namespace IGIEditor
{
    class QInternals
    {
        private static string internalDataFile =  @"bin\IGI-Internals-data.txt";

        internal static string InternalDataRead()
        {
            string data = File.ReadAllText(internalDataFile);
            return data;
        }

        internal static string[] InternalDataReadLines()
        {
            var data = File.ReadAllLines(internalDataFile);
            return data;
        }

        internal static void InternalDataWrite(string data)
        {
            File.WriteAllText(internalDataFile, data);
        }

        internal static void InternalExec(string data, GT.VK[] keys)
        {
            if (!string.IsNullOrEmpty(data))
            {
                InternalDataWrite(data);
                QUtils.Sleep(0.5f);
            }
            GT.MultiKeyPress(keys);
        }

        internal static void InternalExec2(string data, GT.VK key, bool ctrl = false, bool alt = false, bool shift = false)
        {
            if (!string.IsNullOrEmpty(data))
            {
                InternalDataWrite(data);
                QUtils.Sleep(0.5f);
            }
            GT.ShowAppForeground(QMemory.gameName);
            if (ctrl)
                GT.MultiKeyPress(new VK[] { VK.CONTROL, key });
            else if (alt)
                GT.MultiKeyPress(new VK[] { VK.ALT, key });
            else if (shift)
                GT.MultiKeyPress(new VK[] { VK.SHIFT, key });
        }


        internal static void InternalExec(string data, GT.VK key, bool ctrl = false, bool alt = false, bool shift = false)
        {
            if (!string.IsNullOrEmpty(data))
            {
                InternalDataWrite(data);
                QUtils.Sleep(0.5f);
            }
            GT.GT_SendKeyStroke(key.ToString(), ctrl, alt, shift);
        }

        //Internal Keys Map for - Ctrl-F1-F12 Keys.
        internal static void DebugMode() { InternalExec(null, GT.VK.F1, true); }
        internal static void RestartLevel() { InternalExec(null, GT.VK.F2, true); }
        internal static void WeaponPickup(string weapon_id) { InternalExec(weapon_id, GT.VK.F3, true); }
        internal static void FramesSet(string frames) { InternalExec(frames, GT.VK.F4, true); }
        internal static void GameConfigRead() { InternalExec(null, GT.VK.F5, true); }
        internal static void GameConfigWrite() { InternalExec(null, GT.VK.F6, true); }
        internal static void WeaponConfigRead() { InternalExec(null, GT.VK.F7, true); }
        internal static void HumanplayerLoad() { InternalExec(null, GT.VK.F8, true); }
        internal static void HumanCameraView(string cam_view) { InternalExec(cam_view, GT.VK.F9, true); }
        internal static void HumanInputEnable() { InternalExec(null, GT.VK.F10, true); }
        internal static void HumanInputDisable() { InternalExec(null, GT.VK.F11, true); }
        internal static void HumanFreeCam() { InternalExec(null, GT.VK.F12, true); }


        //Internal Keys Map for - Alt-F1-F12 Keys.
        internal static void StartLevel(string level) { InternalExec(level, GT.VK.F1, false, true); }
        internal static void QuitLevel() { InternalExec(null, GT.VK.F2, false, true); }

        //Script Editor section.
        internal static void ScriptParser(string scriptFile) { InternalExec(scriptFile, GT.VK.F3, false, true); }
        internal static void ScriptAssemble(string scriptFile) { InternalExec(scriptFile, GT.VK.F4, false, true); }
        internal static void ScriptCompile(string scriptFile) { InternalExec(scriptFile, GT.VK.F5, false, true); }

        //Resource Editor section.
        internal static void ResourceLoad(string resourceFile) { InternalExec(resourceFile, GT.VK.F6, false, true); }
        internal static void ResourceUnload(string resourceFile) { InternalExec(resourceFile, GT.VK.F7, false, true); }
        internal static void ResourceUnpack(string resourceFile) { InternalExec(resourceFile, GT.VK.F8, false, true); }
        internal static void ResourceFlush(string resourceFile) { InternalExec(resourceFile, GT.VK.F9, false, true); }
        internal static void ResourceIsLoaded(string resourceFile) { InternalExec(resourceFile, GT.VK.F10, false, true); }
        internal static void ResourceFind(string resourceFile) { InternalExec(resourceFile, GT.VK.F11, false, true); }
        internal static void ResourceSaveInfo() { InternalExec(null, GT.VK.F12, false, true); }

        //MEF Editor section.
        internal static void MEF_ModelRemove(string model) { InternalExec(model, GT.VK.F1,false,false,true); }
        internal static void MEF_ModelRestore() { InternalExec(null, GT.VK.F2, false, false, true); }
        internal static void MEF_ModelExtract() { InternalExec(null, GT.VK.F3, false, false, true); }

        //QVM Editor section.
        internal static void QVM_Load(string qvmFile) { InternalExec(qvmFile, GT.VK.F4, false, false, true); }
        internal static void QVM_Read(string qvmFile) { InternalExec(qvmFile, GT.VK.F5, false, false, true); }
        internal static void QVM_Cleanup(string qvmFile) { InternalExec(qvmFile, GT.VK.F6, false, false, true); }

        //Music Editor section.
        internal static void MusicEnable() { InternalExec(null, GT.VK.F7, false, false, true); }
        internal static void MusicDisable() { InternalExec(null, GT.VK.F8, false, false, true); }
        internal static void MusicVolumeSet(string volume) { InternalExec(volume, GT.VK.F9, false, false, true); }
        internal static void MusicSFXVolumeSet(string volume) { InternalExec(volume, GT.VK.F10, false, false, true); }
        internal static void MusicVolumeUpdate() { InternalExec(null, GT.VK.F11, false, false, true); }
        internal static void GraphicsReset() { InternalExec(null, GT.VK.F12, false, false, true); }

        //Player Editor section.
        internal static void Player_ActiveMissionSet(string mission) { InternalExec(mission, GT.VK.NUMPAD0, true); }
        internal static void Player_ActiveNameSet(string name) { InternalExec(name, GT.VK.NUMPAD1, true); }
        internal static void Player_IndexMissionSet(string mission) { InternalExec(mission, GT.VK.NUMPAD2, true); }
        internal static void Player_IndexNameSet(string name) { InternalExec(name, GT.VK.NUMPAD3, true); }

        //Misc Editor section.
        internal static void GameMaterialLoad() { InternalExec(null, GT.VK.NUMPAD4, true); }
        internal static void MagicObjectLoad() { InternalExec(null, GT.VK.NUMPAD5, true); }
        internal static void PhysicsObjectLoad() { InternalExec(null, GT.VK.NUMPAD6, true); }
        internal static void AnimTriggerLoad() { InternalExec(null, GT.VK.NUMPAD7, true); }
        internal static void CutsceneRemove() { InternalExec(null, GT.VK.NUMPAD8, true); }
        internal static void StatusMessageShow(string statusMsg) { InternalExec(statusMsg, GT.VK.INSERT, true); }

        internal static void GraphNodeLinks(string graphId) { InternalExec(graphId, GT.VK.HOME, true); }
    }
}
