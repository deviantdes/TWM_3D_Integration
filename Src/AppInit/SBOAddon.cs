using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SAPbouiCOM;
using SAPbobsCOM;

namespace TWM_3D_Integration
{
    class SBOAddon
    {
        public const string gcAddOnName = "TWM_3D_Integration";
        public const string gcAddonText = "TWM_3D_Integration";
        public const string sConfig_File = "TWM_3D_Integration.cfg";
        public static String WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + gcAddOnName;
        public static System.Collections.Hashtable Forms = new System.Collections.Hashtable();
        public static System.Collections.Specialized.OrderedDictionary oOpenForms = new System.Collections.Specialized.OrderedDictionary();
        public Boolean Connected = true;
        private static string[] ImportExtensions = new string[] {".txt", ".csv"};
        public static System.Collections.Generic.List<String> SupportedImportFiles = new List<string>(ImportExtensions);
        private static string[] ExportExtensions = new string[] {".txt"};
        public static System.Collections.Generic.List<String> SupportedExportFiles = new List<string>(ExportExtensions);
        public static String ParentUID = "";
        public static System.Resources.ResourceManager QueriesRM = new System.Resources.ResourceManager("TWM_3D_Integration.Src.Resource.Queries", System.Reflection.Assembly.GetExecutingAssembly());

        SAPbouiCOM.EventForm TWM_3D_IntegrationEvent = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] Args)
        {
            //Create an Event Log
            if (Args.Length == 0)
            {
                SAPbouiCOM.SboGuiApi oGUI = new SAPbouiCOM.SboGuiApi();
                oGUI.Connect(eCommon.SBOConnectionString);
                try
                {
                    eCommon.SetApplication(oGUI.GetApplication(-1), gcAddOnName, true);
                    //SBOAddon_DB addOnDb = new SBOAddon_DB();

                    SBOAddon oAddOn = new SBOAddon();
                    if (oAddOn.Connected)
                        System.Windows.Forms.Application.Run();
                }
                catch (Exception Ex)
                {
                    System.Windows.Forms.MessageBox.Show("ERROR - Connection failed: " + Ex.Message); ;
                }
            }
            else
            {
                switch (Args[0].ToUpper())
                {
                    case "READNUPDATE":
                            eCommon.CreateLogFileNoUI();
                            try
                            {
                                SAPbobsCOM.BoDataServerTypes ServerType = BoDataServerTypes.dst_MSSQL2012;
                                String Server = null;
                                String LicServer = null;
                                String CompDB = null;
                                String UserName = null;
                                String Password = null;
                                String UseTrusted = "0";
                                String DBUserName = null;
                                String DBPassword = null;
                                InitializeServerVariables(out ServerType, out CompDB, out Server, out LicServer, out UserName, out Password, out UseTrusted, out DBUserName, out DBPassword);
                                eCommon.InitCompany(ServerType, Server, LicServer, CompDB, UserName, Password);
                            }
                            catch (Exception Ex)
                            {
                                eCommon.WriteEventLog("Could not connect to company. " + Ex.Message);
                            }

                            if (eCommon.oCompany.Connected)
                            {
                                ReadAndUpdateDB _rnu = new ReadAndUpdateDB();
                            }

                        break;
                    case "SETTING":
                        frmSetting frm = new frmSetting();
                        frm.ShowDialog();
                        break;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SBOAddon()
        {
            try
            {
                //Application forms
                Forms = eCommon.CollectFormsAttribute();

                //--------------- remove and load menus -----------
                // Change if needed ---------------------------
                if (eCommon.SBO_Application.Menus.Exists(SBOAddon.gcAddOnName)) eCommon.SBO_Application.Menus.RemoveEx(SBOAddon.gcAddOnName);

                eCommon.SBO_Application.Menus.Item("43520").SubMenus.Add(SBOAddon.gcAddOnName, gcAddonText, BoMenuType.mt_POPUP, 99);
                // Change if needed ---------------------------

                foreach (string Key in Forms.Keys)
                {
                    FormAttribute oAttr = (FormAttribute)Forms[Key];
                    if (oAttr.HasMenu)
                    {
                        if (eCommon.SBO_Application.Menus.Exists(oAttr.FormType)) eCommon.SBO_Application.Menus.RemoveEx(oAttr.FormType);
                        eCommon.SBO_Application.Menus.Item(oAttr.ParentMenu).SubMenus.Add(oAttr.FormType, oAttr.MenuName, BoMenuType.mt_STRING, oAttr.Position);
                    }
                }

                try
                {
                    eCommon.SBO_Application.Menus.Item(SBOAddon.gcAddOnName).Image = Environment.CurrentDirectory + "\\Logo.JPG";
                }
                catch { }

                //Register Events
                RegisterAppEvents();
                RegisterFormEvents();

                //Register currently opened forms - initialized opened forms so it is ready to use.
                RegisterForms();

                //Create Authorization
                AddAuthorizationTree();

                //Notify the users the addon is ready to use.
                eCommon.SBO_Application.StatusBar.SetText("Addon " + SBOAddon.gcAddOnName + " is ready.", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);

            }
            catch (Exception ex)
            {
                Connected = false;
                MessageBox.Show("Failed initializing addon. " + ex.Message);
            }
            finally
            {
            }

        }
       
        public void RegisterAppEvents()
        {
            eCommon.SBO_Application.MenuEvent += new SAPbouiCOM._IApplicationEvents_MenuEventEventHandler(OnMenuEvent);
            eCommon.SBO_Application.AppEvent += new _IApplicationEvents_AppEventEventHandler(OnAppEvents);
        }

        public void RegisterFormEvents()
        {
            TWM_3D_IntegrationEvent = eCommon.SBO_Application.Forms.GetEventForm("TWM_3D_Integration");
            //twmB2B_IMPEvent.CloseBefore += new SAPbouiCOM._IEventFormEvents_CloseBeforeEventHandler(twmB2B_IMP.OnBeforeFormClose);
            //twmB2B_IMPEvent.ResizeAfter += new _IEventFormEvents_ResizeAfterEventHandler(twmB2B_IMP.OnAfterFormResize);
        }

        private void RegisterForms()
        {
            for (int i = 0; i < eCommon.SBO_Application.Forms.Count; i++)
            {
                if (!oOpenForms.Contains(eCommon.SBO_Application.Forms.Item(i).UniqueID))
                {
                    FormAttribute oAttrib = Forms[eCommon.SBO_Application.Forms.Item(i).TypeEx] as FormAttribute;
                    if (oAttrib != null)
                    {
                        try
                        {
                            //Execute the constructor
                            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                            Type oType = asm.GetType(oAttrib.TypeName);
                            System.Reflection.ConstructorInfo ctor = oType.GetConstructor(new Type[1] { typeof(String) });
                            if (ctor != null)
                            {
                                object oForm = ctor.Invoke(new Object[1] { eCommon.SBO_Application.Forms.Item(i).UniqueID });
                            }
                            else
                                throw new Exception("No constructor which accepts the formUID found for form type - " + oAttrib.FormType);
                        }
                        catch (Exception ex)
                        {
                            eCommon.SBO_Application.MessageBox(ex.Message);
                        }
                    }
                }
            }
        }

        public void OnAppEvents(BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case BoAppEventTypes.aet_CompanyChanged:
                    if (eCommon.SBO_Application.Menus.Exists(SBOAddon.gcAddOnName)) eCommon.SBO_Application.Menus.RemoveEx(SBOAddon.gcAddOnName);
                    System.Windows.Forms.Application.Exit();
                    break;
                case BoAppEventTypes.aet_FontChanged:
                    break;
                case BoAppEventTypes.aet_LanguageChanged:
                    break;
                case BoAppEventTypes.aet_ServerTerminition:
                    break;
                case BoAppEventTypes.aet_ShutDown:
                    if (eCommon.SBO_Application.Menus.Exists(SBOAddon.gcAddOnName)) eCommon.SBO_Application.Menus.RemoveEx(SBOAddon.gcAddOnName);
                    System.Windows.Forms.Application.Exit();
                    break;
            }


        }

        public void OnMenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool Bubble)
        {
            Bubble = true;
            try
            {
                if (pVal.BeforeAction == true)
                {
                    SAPbouiCOM.Form oForm = null;

                    oForm = eCommon.SBO_Application.Forms.ActiveForm;
                    String sXML = oForm.GetAsXML();
                    switch (pVal.MenuUID)
                    {
                        case "1293":
                            break;
                        case "1285":       
                            break;
                    }
                }
                else
                {
                    switch (pVal.MenuUID)
                    {
                        default:
                            FormAttribute oAttrib = Forms[pVal.MenuUID] as FormAttribute;
                            if (oAttrib != null)
                            {
                                try
                                {
                                    //Execute the constructor
                                    System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                                    Type oType = asm.GetType(oAttrib.TypeName);
                                    System.Reflection.ConstructorInfo ctor = oType.GetConstructor(new Type[0]);
                                    if (ctor != null)
                                    {
                                        object oForm = ctor.Invoke(new Object[0]);
                                    }
                                    else
                                        throw new Exception("No default constructor found for form type - " + oAttrib.FormType);
                                }
                                catch (Exception Ex)
                                {
                                    eCommon.SBO_Application.MessageBox(Ex.Message);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                eCommon.SBO_Application.StatusBar.SetText(Ex.Message, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
            }
        }

        private void AddAuthorizationTree()
        {
            try
            {
                SAPbobsCOM.UserPermissionTree oUserPer = (SAPbobsCOM.UserPermissionTree)eCommon.oCompany.GetBusinessObject(BoObjectTypes.oUserPermissionTree);
                int lErr = 0;

                if (oUserPer.GetByKey(SBOAddon.gcAddOnName) == false)
                {
                    oUserPer.PermissionID = SBOAddon.gcAddOnName;
                    oUserPer.Name = string.Format("Addon : {0}", SBOAddon.gcAddOnName);
                    oUserPer.Options = BoUPTOptions.bou_FullReadNone;
                    lErr = oUserPer.Add();
                    if (lErr != 0)
                        throw new Exception(eCommon.oCompany.GetLastErrorDescription());


                    System.Collections.Hashtable AuthTable = eCommon.CollectAuthorizationAttribute();
                    foreach (string FormType in AuthTable.Keys)
                    {
                        AuthorizationAttribute AuthAttrib = AuthTable[FormType] as AuthorizationAttribute;
                        oUserPer = (SAPbobsCOM.UserPermissionTree)eCommon.oCompany.GetBusinessObject(BoObjectTypes.oUserPermissionTree);
                        oUserPer.PermissionID = AuthAttrib.FormType;
                        oUserPer.Name = AuthAttrib.Name;
                        oUserPer.ParentID = AuthAttrib.ParentID;
                        oUserPer.Options = AuthAttrib.Options;
                        oUserPer.UserPermissionForms.FormType = AuthAttrib.FormType;
                        lErr = oUserPer.Add();
                        if (lErr != 0)
                            throw new Exception(eCommon.oCompany.GetLastErrorDescription());

                    }
                }


            }
            catch (Exception ex)
            {
                eCommon.oEventLog.WriteLine(DateTime.Now + eCommon.Filler + "Unable to create Authorization for " + SBOAddon.gcAddOnName + " Module. " + ex.Message);
            }
        }

        private static void InitializeServerVariables(out SAPbobsCOM.BoDataServerTypes dbType, out String DatabaseName, out String Server, out String LicenseServer, out String User, out String Password
                , out String sUseTrusted, out String dbUserName, out String dbPassword)
        {

            
            TWM_Licence.TWM_SAP oEncrypter = new TWM_Licence.TWM_SAP(new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 10, 73, 1, 5, 75, 1, 8 });
            //Initialize all the environment variable for config file.
            //If nonspecified strings, write the default to config file.

            //Get the dbServerType
            if (frmSetting.GetConfigLine(sConfig_File, "dbServerType") != null)
                dbType = (SAPbobsCOM.BoDataServerTypes)int.Parse(frmSetting.GetConfigLine(sConfig_File, "dbServerType"));
            else
            {
                dbType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2008;
                frmSetting.WriteConfigFile(sConfig_File, "dbServerType", ((int)dbType).ToString(), frmSetting.WriteType.Append);
            }


            //Get the DatabaseName
            if (frmSetting.GetConfigLine(sConfig_File, "DatabaseName") != null)
                DatabaseName = frmSetting.GetConfigLine(sConfig_File, "DatabaseName");
            else
            {
                DatabaseName = "UNZA";
                frmSetting.WriteConfigFile(sConfig_File, "DatabaseName", DatabaseName, frmSetting.WriteType.Append);
            }


            //Get the Server
            if (frmSetting.GetConfigLine(sConfig_File, "ServerName") != null)
                Server = frmSetting.GetConfigLine(sConfig_File, "ServerName");
            else
            {
                Server = "sapserver";
                frmSetting.WriteConfigFile(sConfig_File, "ServerName", Server, frmSetting.WriteType.Append);
            }


            //Get the License Server
            if (frmSetting.GetConfigLine(sConfig_File, "LicenseServer") != null)
                LicenseServer = frmSetting.GetConfigLine(sConfig_File, "LicenseServer");
            else
            {
                LicenseServer = "sapserver:30000";
                frmSetting.WriteConfigFile(sConfig_File, "LicenseServer", LicenseServer, frmSetting.WriteType.Append);
            }


            //Get the User
            if (frmSetting.GetConfigLine(sConfig_File, "User") != null)
                User = frmSetting.GetConfigLine(sConfig_File, "User");
            else
            {
                User = "manager";
                frmSetting.WriteConfigFile(sConfig_File, "User", User, frmSetting.WriteType.Append);
            }

            //Get the Password
            if (frmSetting.GetConfigLine(sConfig_File, "Password") != null)
            {
                Password = frmSetting.GetConfigLine(sConfig_File, "Password");
                Password = oEncrypter.Decrypt(Password);
            }
            else
            {
                Password = "manager";
                String EncryptedPass = oEncrypter.Encrypt(Password);
                frmSetting.WriteConfigFile(sConfig_File, "Password", EncryptedPass, frmSetting.WriteType.Append);
            }


            //Get the UseTrusted
            if (frmSetting.GetConfigLine(sConfig_File, "UseTrusted") != null)
                sUseTrusted = frmSetting.GetConfigLine(sConfig_File, "UseTrusted");
            else
            {
                sUseTrusted = "0";
                frmSetting.WriteConfigFile(sConfig_File, "UseTrusted", sUseTrusted, frmSetting.WriteType.Append);
            }


            //if Not UseTrusted, Get The DBUserName And DBPassword
            if (sUseTrusted != "1")
            {
                //Get the User
                if (frmSetting.GetConfigLine(sConfig_File, "DBUserName") != null)
                    dbUserName = frmSetting.GetConfigLine(sConfig_File, "DBUserName");
                else
                {
                    dbUserName = "sa";
                    frmSetting.WriteConfigFile(sConfig_File, "DBUserName", dbUserName, frmSetting.WriteType.Append);
                }


                //Get the Password
                if (frmSetting.GetConfigLine(sConfig_File, "DBPassword") != null)
                {
                    dbPassword = frmSetting.GetConfigLine(sConfig_File, "DBPassword");
                    dbPassword = oEncrypter.Decrypt(dbPassword);
                }
                else
                {
                    dbPassword = "sa";
                    String EncryptedPass = oEncrypter.Encrypt(dbPassword);
                    frmSetting.WriteConfigFile(sConfig_File, "DBPassword", EncryptedPass, frmSetting.WriteType.Append);
                }

            }
            else
            {
                dbUserName = "";
                dbPassword = "";
            }

        }

        internal static string GetCOGSAccount(SAPbobsCOM.Company oComp, string sItem)
        {
            SAPbobsCOM.Recordset oRS = oComp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            String sSQL = String.Format("SELECT T1.SaleCostAc FROM OITM T0 JOIN OITB T1 ON T0.ItmsGrpCod = T1.ItmsGrpCod WHERE ItemCode = '{0}' ", sItem);
            oRS.DoQuery(sSQL);
            if (oRS.EoF) return "";
            return oRS.Fields.Item(0).Value.ToString().Trim();

        }

    }
}