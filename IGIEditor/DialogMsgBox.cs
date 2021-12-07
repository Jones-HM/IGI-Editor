using System;
using System.Windows.Forms;
using System.Threading;

namespace IGIEditor
{
    public partial class DialogMsgBox : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        public DialogMsgBox()
        {
            InitializeComponent();
            AnimWorker aw = new AnimWorker();
            aw.FormFadeInEffect(this);
        }

        static public DialogResult ShowBox(string boxTitle, string messageContent, MsgBoxButtons boxButton = MsgBoxButtons.Ok)
        {
            if (boxButton == MsgBoxButtons.Ok)
            {
                DialogMsgBox msg = new DialogMsgBox();
                msg.dialogBoxTitle.Text = boxTitle;
                msg.dialogBoxMsg.Text = messageContent;
                msg.dialogBoxBtnYes.Visible = false;
                return msg.ShowDialog();
            }
            else
            {
                DialogMsgBox msg = new DialogMsgBox();
                msg.dialogBoxTitle.Text = boxTitle;
                msg.dialogBoxMsg.Text = messageContent;
                msg.dialogBoxBtnNo.Text = "No";
                msg.dialogBoxBtnNo.DialogResult = DialogResult.No;
                msg.dialogBoxBtnYes.Text = "Yes";
                msg.dialogBoxBtnYes.DialogResult = DialogResult.Yes;
                return msg.ShowDialog();
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void dialogBoxPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void dialogBoxBtnYes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dialogBoxBtnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class AnimWorker
    {
        Form formInTarget;
        System.Windows.Forms.Timer tmr2 = new System.Windows.Forms.Timer();

        public void FormFadeInEffect(Form targetForm)
        {
            formInTarget = targetForm;
            targetForm.Opacity = 0;
            tmr2.Interval = 1;
            tmr2.Tick += Tmr_Tick1;
            tmr2.Enabled = true;
        }

        private void Tmr_Tick1(object sender, EventArgs e)
        {
            formInTarget.Opacity += 0.05;
            if (formInTarget.Opacity == 0.95)
            {
                tmr2.Stop();
                tmr2.Enabled = false;
            }
            Thread.Sleep(1);
        }
    }

    public enum MsgBoxButtons
    {
        Ok,
        YesNo
    }
}
