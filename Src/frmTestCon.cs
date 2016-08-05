using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TWM_3D_Integration
{
    public partial class frmTestCon : Form
    {
        int ProgValue = 0;
        String sMessage = "";
        public String ServerName = "";
        public String LicenseServer = "";
        public String UserName = "";
        public String Password = "";
        public String CompanyDB = "";
        public SAPbobsCOM.BoDataServerTypes dbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2005;
        public bool UseTrusted = false;
        public String DBUser = "";
        public String DBPassword = "";

        public frmTestCon()
        {
            InitializeComponent();
        }

        private void frmTestCon_Load(object sender, EventArgs e)
        {
            Timer1.Enabled = true;
            this.btnClose.Enabled = false;
            this.UseWaitCursor = true;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(TestConnection));
            thread.Start();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            ProgValue++;
            this.ProgressBar1.Value = ProgValue;
            if (ProgValue == 100)
                sMessage = "Connection time out. \r\nPlease check your setting and try again";

            if (sMessage != "")
            {
                this.ProgressBar1.Value = 100;
                this.Timer1.Enabled = false;
                this.Label1.Text = sMessage;
                this.btnClose.Enabled = true;
                this.UseWaitCursor = false;
            }

        }

        private void TestConnection()
        {
            SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            ///Open the connection to sbo

            oCompany.Server = ServerName;
            oCompany.LicenseServer = LicenseServer;
            oCompany.UserName = UserName;
            oCompany.Password = Password;
            oCompany.CompanyDB = CompanyDB;
            oCompany.UseTrusted = UseTrusted;
            if (!UseTrusted)
            {
                oCompany.DbUserName = DBUser;
                oCompany.DbPassword = DBPassword;
            }
            oCompany.DbServerType = dbServerType;


            int err = oCompany.Connect();

            if (err != 0)
                sMessage = "Failed Connecting to SAP\n\r" + oCompany.GetLastErrorDescription();
            else
            {
                sMessage = "Test connection successful";
                oCompany.Disconnect();
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oCompany);
            GC.Collect();

        }
    }
}
