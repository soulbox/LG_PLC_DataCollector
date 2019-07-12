using LGPLC.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LGPLC
{
    public static class extension
    {
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            // See Update 2 for edits Mike de Klerk suggests to insert here.

            if (control != null && control.InvokeRequired)
            {
                try
                {
                    control.Invoke(action);

                }
                catch (Exception)
                {


                }
            }
            else
            {
                action();
            }
        }
        //public static void StatusShow(string msg)
        //{
        //    if (Main.StatusList == null) return;
        //    Main.StatusList.Items.Add(msg);
        //    Main.StatusList.SetSelected(Main.StatusList.Items.Count - 1, true);
        //}
        public static void StatusShow(this TextBox text, string msg)
        {
            text.InvokeIfRequired(() =>
            {
                string str = DateTime.Now.ToString("HH:mm:ss") + " => " + msg.Replace('\n', ' ');
                if (string.IsNullOrEmpty(text.Text))
                    text.AppendText(str);
                else
                    text.AppendText(Environment.NewLine + str);


            });

        }

    }

}
