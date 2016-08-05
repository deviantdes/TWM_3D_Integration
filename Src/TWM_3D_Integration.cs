using System;
using System.Collections.Generic;
using System.Text;



namespace TWM_3D_Integration
{
    [Form("View Log", true, "View Log", SBOAddon.gcAddOnName, 1)]
    class TWM_3D_Integration
    {
        SAPbouiCOM.Form _oForm = null;

        public TWM_3D_Integration()
        {
            try
            {
                //Draw the form
                String sFileName = string.Format("{0}.xml", this.GetType().Name);
                bool bSuccess = true;
                if (System.IO.File.Exists(Environment.CurrentDirectory + "\\" + sFileName))
                {
                    System.Xml.XmlDocument oXml = new System.Xml.XmlDocument();
                    oXml.Load(Environment.CurrentDirectory + "\\" + sFileName);
                    String sXml = eCommon.ModifySize(oXml.InnerXml);
                    try
                    {
                        eCommon.SBO_Application.LoadBatchActions(ref sXml);
                    }
                    catch
                    {
                        bSuccess = false;
                    }
                }
                else
                {
                    String ResourceName = string.Format("{0}.Src.Resource.{1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, sFileName);

                    String sXml = eCommon.ModifySize(eCommon.GetXMLResource(ResourceName));
                    try
                    {
                        eCommon.SBO_Application.LoadBatchActions(ref sXml);
                    }
                    catch {
                        bSuccess = false;
                    }
                }

                if (bSuccess)
                {
                    _oForm = eCommon.SBO_Application.Forms.ActiveForm;
                    if (_oForm.TypeEx.StartsWith("-"))
                    {
                        //a UDF form is opened. Close it.
                        String UDFFormUID = _oForm.UniqueID;
                        String ParentFormUID = eCommon.GetParentFormUID(_oForm);
                        _oForm.Close();

                        _oForm = eCommon.SBO_Application.Forms.ActiveForm;
                        if (_oForm.UniqueID != ParentFormUID)
                            _oForm = eCommon.SBO_Application.Forms.Item(ParentFormUID);
                    }
                    _oForm.EnableMenu("6913", false);

                    GetItemReferences();
                    // Initform can be use if you want to fill anything or color anything
                    InitForm();
                    if (!SBOAddon.oOpenForms.Contains(_oForm.UniqueID))
                        SBOAddon.oOpenForms.Add(_oForm.UniqueID, this);

                    _oForm.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public TWM_3D_Integration(SAPbouiCOM.Form oForm)
        {
            _oForm = oForm;
            GetItemReferences();
            if (!SBOAddon.oOpenForms.Contains(_oForm.UniqueID))
                SBOAddon.oOpenForms.Add(_oForm.UniqueID, this);
        }

        public TWM_3D_Integration(String FormUID)
        {
            _oForm = eCommon.SBO_Application.Forms.Item(FormUID);
            GetItemReferences();
            if (!SBOAddon.oOpenForms.Contains(_oForm.UniqueID))
                SBOAddon.oOpenForms.Add(_oForm.UniqueID, this);

        }

        private void InitForm()
        {
            _oForm.Freeze(true);
            try
            {
              
            }
            catch (Exception Ex)
            {
                eCommon.SBO_Application.MessageBox(Ex.Message);
            }
            finally
            {
                _oForm.Freeze(false);
            }
        }

        public void GetItemReferences()
        {
            try
            {
               

            }
            catch (Exception Ex)
            {
                eCommon.SBO_Application.MessageBox(Ex.Message);
            }
        }

        //[FormEvent("ResizeAfter",false)]
        public static void OnAfterFormResize(SAPbouiCOM.SBOItemEventArg pVal)
        {
            SAPbouiCOM.Form oForm = eCommon.SBO_Application.Forms.Item(pVal.FormUID);
            if (oForm.Items.Count > 0)
            {
                oForm.Items.Item("Item_1").Width = oForm.ClientWidth - 10;
                oForm.Items.Item("Item_1").Height = oForm.Items.Item("1").Top - 10 - oForm.Items.Item("Item_1").Top;
            }
        }

        //[FormEvent("CloseBefore",true)]
        public static void OnBeforeFormClose(SAPbouiCOM.SBOItemEventArg pVal, out bool Bubble)
        {
            try
            {
                SAPbouiCOM.Form form = eCommon.SBO_Application.Forms.Item(pVal.FormUID);
                for (int i = 0; i < form.DataSources.DataTables.Count; i++)
                {
                    form.DataSources.DataTables.Item(i).Clear();
                }
            }
            finally
            {
                if (SBOAddon.oOpenForms.Contains(pVal.FormUID))
                    SBOAddon.oOpenForms.Remove(pVal.FormUID);

                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            Bubble = true;
        }

    }

   
}
