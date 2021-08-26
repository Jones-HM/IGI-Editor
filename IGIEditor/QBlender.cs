using System;

namespace IGIEditor
{
    enum BLENDTYPE
    {
        BLEND,
        BLEND_FILE,
        BLEND_SCRIPT,
    }

    enum SCRIPTTYPE
    {
        SCRIPT_0,
        SCRIPT_90,
        SCRIPT_180,
        SCRIPT_360,
        SCRIPT_CUSTOM,
    }

    class QBlender
    {
        private static string blend_file = "", blend_models_path = @"Models\";
        private static string data_extractor_script = "py_scripts\\data_extractor.py", rotate_0_script = "py_scripts\\rotate_0.py", rotate_90_script = "py_scripts\\rotate_90.py", rotate_180_script = "py_scripts\\rotate_180.py", rotate_360_script = "py_scripts\\rotate_360.py", rotate_custom_script = "py_scripts\\rotate_custom.py";
        private static string run_blender = "blender";
        private static string run_blender_file, run_blender_script;
        private static string blender_data_file = "blender_data.txt";

        internal static void RunBlender(int blend_type, string blender_model, int script_type = 0)
        {
            string blend_cmd = null, rotate_script = null;
            if (String.IsNullOrEmpty(blender_model))
            {
                QUtils.ShowError("Blender : run parameter file name is empty");
                return;
            }

            blend_file = blender_model;
            var blend_script = data_extractor_script;

            if (script_type == (int)SCRIPTTYPE.SCRIPT_0) rotate_script = rotate_0_script;
            else if (script_type == (int)SCRIPTTYPE.SCRIPT_90) rotate_script = rotate_90_script;
            else if (script_type == (int)SCRIPTTYPE.SCRIPT_180) rotate_script = rotate_180_script;
            else if (script_type == (int)SCRIPTTYPE.SCRIPT_360) rotate_script = rotate_360_script;
            else if (script_type == (int)SCRIPTTYPE.SCRIPT_CUSTOM) blend_script = rotate_custom_script;

            //Append model path and blend/scripts files.
            run_blender_file = "blender " + blend_models_path + blend_file + " --python " + blend_models_path + rotate_script;
            run_blender_script = "blender " + blend_models_path + blend_file + ((blend_script == data_extractor_script) ? " --background" : "") + " --python " + blend_models_path + blend_script;

            if (blend_type == (int)BLENDTYPE.BLEND) blend_cmd = run_blender;
            else if (blend_type == (int)BLENDTYPE.BLEND_FILE)
            {
                blend_cmd = run_blender_file;
                QUtils.AddLog("RunBlender file cmd : " + run_blender_file);
            }
            else if (blend_type == (int)BLENDTYPE.BLEND_SCRIPT)
            {
                blend_cmd = run_blender_script;
                QUtils.AddLog("RunBlender script cmd : " + run_blender_script);
            }

            QUtils.AddLog("RunBlender called with blend type : " + (blend_type == 1 ? "FILE" : "SCRIPT") + " and model  : " + blender_model);

            if (!String.IsNullOrEmpty(blend_cmd))
                QUtils.ShellExec(blend_cmd);
        }

        internal static T ParseBlenderData<T>(bool position_extract, bool rotation_extract)
        {
            Real32 rotation = null;
            Real64 position = null;
            QUtils.AddLog("ParseBlenderData called with data PositionExtract : " + position_extract.ToString() + " and RotationExtract  : " + rotation_extract.ToString());

            var blender_data = QUtils.LoadFile(blend_models_path + blender_data_file);
            if (string.IsNullOrEmpty(blender_data)) QUtils.ShowError("Blender model data is empty", "Blender - Error");
            var blender_line = blender_data.Replace("=", String.Empty).Split('\n');

            foreach (var data in blender_line)
            {
                if (data.Contains("Position") && position_extract)
                {
                    var sub_str_pos = data.Slice(data.IndexOf('(') + 1, data.IndexOf(')'));
                    var pos_data = sub_str_pos.Split(',');
                    position = new Real64();

                    //Parse the positions.
                    position.x = double.Parse(pos_data[0].Trim());
                    position.y = double.Parse(pos_data[1].Trim());
                    position.z = double.Parse(pos_data[2].Trim());

                    QUtils.AddLog("ParseBlenderData  PositionExtract data : x : " + position.x.ToString() + ", y : " + position.y.ToString() + ", z : " + position.z.ToString());
                }

                else if (data.Contains("Rotation") && rotation_extract)
                {
                    var sub_str_rot = data.Slice(data.IndexOf('(') + 1, data.IndexOf(')'));
                    QUtils.AddLog("ParseBlenderData sub_str : " + sub_str_rot);
                    var pos_data = sub_str_rot.Split(',');
                    rotation = new Real32();

                    //Parse the rotation.
                    rotation.beta = float.Parse(pos_data[0].Trim().Replace("x", String.Empty));
                    rotation.alpha = float.Parse(pos_data[1].Trim().Replace("y", String.Empty));
                    rotation.gamma = float.Parse(pos_data[2].Trim().Replace("z", String.Empty));

                    QUtils.AddLog("ParseBlenderData  RotationExtract data : x : " + rotation.alpha.ToString() + ", y : " + rotation.beta.ToString() + ", z : " + rotation.gamma.ToString());

                }
            }


            object value = null;
            if (position_extract) value = position; else value = rotation;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        internal static void LoadModelRotation(string blender_model, Real32 rotation)
        {
            string rotate_custom_script =
                "import bpy\n" +
                 "obj = bpy.context.object\n" +
                 "obj.rotation_euler = (" + rotation.beta + "," + rotation.alpha + "," + rotation.gamma + ")";

            QUtils.SaveFile(blend_models_path + QBlender.rotate_custom_script, rotate_custom_script);
            QUtils.AddLog("LoadModelRotation() called with data model : " + blender_model + " and rotation alpha : " + rotation.alpha + ", beta : " + rotation.beta + ", gamma : " + rotation.gamma);

            RunBlender((int)BLENDTYPE.BLEND_SCRIPT, blender_model, (int)SCRIPTTYPE.SCRIPT_CUSTOM);
        }

    }
}
