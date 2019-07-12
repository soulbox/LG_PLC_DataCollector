using LGPLC.Properties;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LGPLC
{
   public class FormHelper:Form
    {

        public FormHelper()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Load += new System.EventHandler(this.FormHelper_Load);
            this.ResumeLayout(false);
        }

        private void FormHelper_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.Logo;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                this.Text = string.Format("LS PLC Reporter - v{0}",
                    ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4));
            }
            else
            {
                this.Text = "LS PLC Reporter";
            }
        }
    }
}
