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
    public partial class frmSetting : Form
    {
        TWM_Licence.TWM_SAP encrypter = new TWM_Licence.TWM_SAP(new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 10, 73, 1, 5, 75, 1, 8 });
        bool bModified = false;

        //[Company Detail]
        String DatabaseName = "UNZA";
        String ServerName = "localhost";
        String LicenseServer = "localhost";
        String LicensePort = "30000";
        String User = "manager";
        String Password = "manager";
        bool UseTrusted = true;
        String DBPassword = "sa";
        String DBUser = "sa";
        SAPbobsCOM.BoDataServerTypes dbType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;

        private struct ConfigLine
        {
            public String Group;
            public String Key;
            public String Value;
        }

        System.Collections.Generic.List<ConfigLine> ConfigLines = new System.Collections.Generic.List<ConfigLine>();
        System.Collections.Specialized.NameValueCollection odbTypes = new System.Collections.Specialized.NameValueCollection();
        public enum WriteType { OverWrite, Update, Append };

        public frmSetting()
        {
            InitializeComponent();
        }

        private void frmSetting_Load(System.Object sender, System.EventArgs e)
        {
            //Fill the cboDbType values
            int[] DBTypes = (int[])System.Enum.GetValues(typeof(SAPbobsCOM.BoDataServerTypes));
            cboDbType.Items.Clear();
            foreach (int dbType in DBTypes)
            {
                String sName = System.Enum.GetName(typeof(SAPbobsCOM.BoDataServerTypes), dbType);
                cboDbType.Items.Add(sName);
                odbTypes.Add(sName, dbType.ToString());
            }

            //Populate the configs into ConfigLines
            PopulateConfigLines();

            //Check if file Config Exists
            if (!System.IO.File.Exists(SBOAddon.WorkingDirectory + "\\" + SBOAddon.sConfig_File))
                //If not create one with default values
                CreateConfigFile(ConfigLines);

            FillConfigLineToScreen();
            bModified = false;
        }

        private void txtDBPassword_TextChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        private void txtDBUser_TextChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        private void chbTrusted_CheckedChanged(object sender, EventArgs e)
        {
            bModified = true;
            if (chbTrusted.Checked)
                grpTrusted.Enabled = false;
            else
                grpTrusted.Enabled = true;
        }

        private void cboDbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        private void btnTestCon_Click(object sender, EventArgs e)
        {
            frmTestCon oTest = new frmTestCon();
            oTest.ServerName = this.txtSrvrName.Text;

            oTest.LicenseServer = this.txtLcnsSrvr.Text + ":" + this.txtLcnsPort.Text;
            oTest.UserName = this.txtUserName.Text;
            oTest.Password = this.txtPassword.Text;
            oTest.CompanyDB = this.txtDBName.Text;
            oTest.dbServerType = (SAPbobsCOM.BoDataServerTypes)(this.cboDbType.SelectedIndex + 1);
            oTest.UseTrusted = this.chbTrusted.Checked;
            oTest.DBUser = this.txtDBUser.Text;
            oTest.DBPassword = this.txtDBPassword.Text;
            oTest.Left = this.Left + 100;
            oTest.Top = this.Top + 300;


            oTest.ShowDialog();
        }

        private void txtLcnsPort_TextChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        private void txtLcnsSrvr_TextChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        private void txtSrvrName_TextChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        private void txtDBName_TextChanged(object sender, EventArgs e)
        {
            bModified = true;
        }

        public static String GetConfigLine(String sFilename, String RowID)
        {
            String[] sFileContent;
            sFilename = SBOAddon.WorkingDirectory + "\\" + sFilename;
            //string sDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + SBOAddon.gcAddOnName ;
            //sFilename = sDir + "\\" + sFilename;
            eCommon.WriteEventLog(string.Format("Retrieving {0} from : {1}", RowID, sFilename));
            if (!System.IO.File.Exists(sFilename))
            {
                eCommon.WriteEventLog("File Not Found");
                return null;
            }
            else
            {
                sFileContent = System.IO.File.ReadAllLines(sFilename);
                foreach (String row in sFileContent)
                {
                    if (row.Contains(":="))
                    {
                        if (row.Length > 0)
                        {
                            if (RowID == row.Substring(0, row.IndexOf(":=")))
                            {
                                return row.Substring(row.IndexOf(":=") + 2);
                            }
                        }
                    }
                }
            }
            eCommon.WriteEventLog(string.Format("Value : {0}", "NULL"));
            return null;
        }

        public static void WriteConfigFile(String FileName, String RowID, String Value, WriteType method)
    {
        String[] Rows =null;
        bool bReplaced  = false;
        int i ;
        FileName = SBOAddon.WorkingDirectory  + "\\" + FileName;

        if (method == WriteType.Append | method == WriteType.Update)
        {
            if (System.IO.File.Exists(FileName))
                Rows = System.IO.File.ReadAllLines(FileName);
            

            if (method == WriteType.Update)
            {
                if (Rows != null)
                {
                    if (Rows.Length > 0)
                    {
                        i = 0;
                        foreach(String row in Rows)
                        {
                            if(row.Substring(0, row.IndexOf( ":=") - 1).Trim()==RowID)
                            {
                                    Rows[i] = RowID + ":=" + Value;
                                    bReplaced = true;
                            }
                            i++;
                        }
                    }
                    if(!bReplaced) Array.Resize(ref Rows,Rows.Length);
                }
                else
                {
                    Array.Resize(ref Rows,1);
                    Rows[Rows.Length - 1] = RowID + ":=" + Value;
                }
            }
            else
            {
                if (Rows !=null)
                    Array.Resize(ref Rows,  Rows.Length);
                else
                    Array.Resize(ref Rows, 1);
                
                Rows[Rows.Length - 1] = RowID + ":=" + Value;

            }
        }
        if (method == WriteType.OverWrite)
        {
            Array.Resize(ref Rows,1);
            Rows[0] = RowID + ":=" + Value;
        }
        if (System.IO.File.Exists(FileName)) System.IO.File.Delete(FileName) ; 
        System.IO.File.WriteAllLines(FileName, Rows);
        }

        private bool BuildPath(String sFullName) 
    {
        String[] sPaths;
        String sDir = "";

        sPaths = sFullName.Split('\\');
        
        try
        {
            foreach(String root in sPaths)
            {
                sDir = sDir + root + "\\";
                if (System.IO.Directory.Exists(sDir + "\\"))
                    continue;
                else
                    System.IO.Directory.CreateDirectory(sDir + "\\");
                
            }
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
            return false;
        }
        return true;
        }

        private void PopulateConfigLines()
        {
            ConfigLine ConfigItem = new ConfigLine();

            //[Company Detail]
            ConfigItem.Group = "Company";
            ConfigItem.Key = "dbServerType";
            ConfigItem.Value = ((int)dbType).ToString();
            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "DatabaseName";
            ConfigItem.Value = DatabaseName;
            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "ServerName";
            ConfigItem.Value = ServerName;
            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "LicenseServer";
            ConfigItem.Value = LicenseServer + ":" + LicensePort;
            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "User";
            ConfigItem.Value = User;
            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "Password";
            ConfigItem.Value = Password;
            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "UseTrusted";
            ConfigItem.Value = UseTrusted.ToString();
            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "DBPassword";
            ConfigItem.Value = DBPassword;
            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "DBUser";
            ConfigItem.Value = DBUser;
            ConfigLines.Add(ConfigItem);

        }

        private void FillConfigLineToScreen()
        {
            String sValue = "";
            ConfigLine ConfigItem = new ConfigLine();

            for (int i = 0; i < ConfigLines.Count; i++)
            {
                sValue = "";
                sValue = GetConfigLine(SBOAddon.sConfig_File, ConfigLines[i].Key);

                ConfigItem.Group = ConfigLines[i].Group;
                ConfigItem.Key = ConfigLines[i].Key;
                if (sValue != null)
                    ConfigItem.Value = sValue;
                else
                    ConfigItem.Value = "";

                ConfigLines[i] = ConfigItem;
                switch (ConfigItem.Key.ToUpper())
                {
                    case "DBSERVERTYPE":
                        if (ConfigItem.Value == "") ConfigItem.Value = "0";
                        cboDbType.SelectedIndex = int.Parse(ConfigItem.Value) - 1;
                        break;

                    case "DATABASENAME":
                        if (sValue == null)
                            txtDBName.Text = "Default : " + DatabaseName;
                        else
                            txtDBName.Text = ConfigItem.Value;
                        break;

                    case "SERVERNAME":
                        if (sValue == null)
                            txtSrvrName.Text = "Default : " + ServerName;
                        else
                            txtSrvrName.Text = ConfigItem.Value;
                        break;

                    case "LICENSESERVER":
                        String[] License = ConfigItem.Value.Split(':');
                        if (sValue == null)
                        {
                            txtLcnsSrvr.Text = "Default : " + LicenseServer;
                            txtLcnsPort.Text = LicensePort;
                        }
                        else
                        {
                            if (License.Length > 1)
                            {
                                txtLcnsSrvr.Text = License[0];
                                txtLcnsPort.Text = License[1];
                            }
                            else if (License.Length == 1)
                            {
                                txtLcnsSrvr.Text = License[0];
                            }
                        }
                        break;

                    case "USER":
                        if (sValue == null)
                            txtUserName.Text = "Default : " + User;
                        else
                            txtUserName.Text = ConfigItem.Value;

                        break;

                    case "PASSWORD":
                        if (sValue == null)
                            txtPassword.Text = "Default : " + Password;
                        else
                        {
                            String DecryptedString = "";
                            try
                            {
                                DecryptedString = encrypter.Decrypt(sValue);
                                txtPassword.Text = DecryptedString;
                            }
                            catch
                            {
                                MessageBox.Show("Password is not in expected format \n\r Please fill in correct password in 'Company tab'", "Password error", MessageBoxButtons.OK);
                                txtPassword.Text = "";
                            }
                        }
                        break;
                    case "USETRUSTED":
                        if (sValue == null)
                        {
                            if (ConfigItem.Value == "1")
                                chbTrusted.Checked = true;
                            else
                                chbTrusted.Checked = false;
                        }
                        else
                        {
                            if (sValue.ToUpper() == "Y" || sValue == "1")
                                chbTrusted.Checked = true;
                            else
                                chbTrusted.Checked = false;

                        }

                        if (chbTrusted.Checked)
                            grpTrusted.Enabled = false;
                        else
                            grpTrusted.Enabled = true;

                        break;

                    case "DBUSER":
                        if (sValue == null)
                            txtDBUser.Text = "Default : " + DBUser;
                        else
                            txtDBUser.Text = sValue;

                        break;

                    case "DBPASSWORD":
                        if (sValue == null)
                        {
                            txtDBPassword.Text = "Default : " + DBPassword;
                        }
                        else
                        {
                            String DecryptedString = "";
                            try
                            {
                                DecryptedString = encrypter.Decrypt(sValue);
                                txtDBPassword.Text = DecryptedString;
                            }
                            catch 
                            {
                                MessageBox.Show("Password is not in expected format \r\nPlease fill in correct password.", "Password error", MessageBoxButtons.OK);
                                txtPassword.Text = "";
                            }
                        }
                        break;
                }
            }
        }
        private void CreateConfigFile(System.Collections.Generic.List<ConfigLine> ConfigLines)
        {
            System.IO.StreamWriter oConfigFile;

            if (System.IO.File.Exists(SBOAddon.WorkingDirectory + "\\" + SBOAddon.sConfig_File))
                System.IO.File.Delete(SBOAddon.WorkingDirectory + "\\" + SBOAddon.sConfig_File);


            oConfigFile = new System.IO.StreamWriter(SBOAddon.WorkingDirectory + "\\" + SBOAddon.sConfig_File);
            oConfigFile.AutoFlush = true;

            String group = "";

            foreach (ConfigLine item in ConfigLines)
            {
                if (group.ToUpper() != item.Group.ToUpper())
                {
                    if (group != "")
                        oConfigFile.WriteLine("");

                    group = item.Group;
                    oConfigFile.WriteLine("[" + item.Group + "]");
                }
                if (item.Key.ToUpper() == "PASSWORD" || item.Key.ToUpper() == "DBPASSWORD")
                {
                    String EncryptedPassword = encrypter.Encrypt(item.Value);
                    oConfigFile.WriteLine(item.Key + ":=" + EncryptedPassword);
                }
                else
                    oConfigFile.WriteLine(item.Key + ":=" + item.Value);

            }
            oConfigFile.Close();

        }

        private void SortConfigLines(System.Collections.Generic.List<ConfigLine> ConfigLines)
        {
            ConfigLine item = new ConfigLine();

            for (int i = ConfigLines.Count - 1; i >= 0; i--)
            {
                for (int j = ConfigLines.Count - 1; j >= 0; j--)
                {
                    if (ConfigLines[j].Group.CompareTo(ConfigLines[i].Group) < 0)
                    {
                        item.Group = ConfigLines[j].Group;
                        item.Key = ConfigLines[j].Key;
                        item.Value = ConfigLines[j].Value;

                        ConfigLines[j] = ConfigLines[i];
                        ConfigLines[i] = item;
                    }
                }
            }
        }

        private void ReadConfigLinesFromScreen()
        {
            ConfigLines.Clear();
            ConfigLine ConfigItem = new ConfigLine();


            //[Company Detail]
            ConfigItem.Group = "Company";
            ConfigItem.Key = "dbServerType";
            String sTemSrvType = "";
            if (txtDBName.Text.Contains("Default : "))
                sTemSrvType = cboDbType.Text.Replace("Default : ", "");
            else
                sTemSrvType = cboDbType.Text;


            ConfigItem.Value = odbTypes[cboDbType.Text];

            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "DatabaseName";
            if (txtDBName.Text.Contains("Default : "))
                ConfigItem.Value = txtDBName.Text.Replace("Default : ", "");
            else
                ConfigItem.Value = txtDBName.Text;

            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "ServerName";
            if (txtSrvrName.Text.Contains("Default : "))
                ConfigItem.Value = txtSrvrName.Text.Replace("Default : ", "");
            else
                ConfigItem.Value = txtSrvrName.Text;

            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "LicenseServer";
            if (txtLcnsSrvr.Text.Contains("Default : "))
                ConfigItem.Value = txtLcnsSrvr.Text.Replace("Default : ", "") + ":" + txtLcnsPort.Text;
            else
                ConfigItem.Value = txtLcnsSrvr.Text + ":" + txtLcnsPort.Text;

            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "User";
            if (txtUserName.Text.Contains("Default : "))
                ConfigItem.Value = txtUserName.Text.Replace("Default : ", "");
            else
                ConfigItem.Value = txtUserName.Text;

            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "Password";
            if (txtPassword.Text.Contains("Default : "))
                ConfigItem.Value = txtPassword.Text.Replace("Default : ", "");
            else
                ConfigItem.Value = txtPassword.Text;

            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "UseTrusted";
            if (chbTrusted.Checked)
                ConfigItem.Value = "1";
            else
                ConfigItem.Value = "0";

            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "DBUser";
            if (txtDBUser.Text.Contains("Default : "))
                ConfigItem.Value = txtDBUser.Text.Replace("Default : ", "");
            else
                ConfigItem.Value = txtDBUser.Text;

            ConfigLines.Add(ConfigItem);

            ConfigItem.Group = "Company";
            ConfigItem.Key = "DBPassword";
            if (txtDBPassword.Text.Contains("Default : "))
                ConfigItem.Value = txtDBPassword.Text.Replace("Default : ", "");
            else
                ConfigItem.Value = txtDBPassword.Text;

            ConfigLines.Add(ConfigItem);


        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Write the config file
            try
            {
                ReadConfigLinesFromScreen();
                CreateConfigFile(ConfigLines);
                bModified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to update config file. \n\r" + ex.Message);
            }

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bModified)
            {
                if (MessageBox.Show("Exit and discard unsaved changes ?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    this.Dispose();
            }
            else
                this.Dispose();
        }
    }
}
