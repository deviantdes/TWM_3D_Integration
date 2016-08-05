using System;
using System.Collections.Generic;
using System.Text;

namespace TWM_3D_Integration
{
    using SAPbobsCOM;
    using SAPbouiCOM;
    using B1WizardBase;
    using System;
    public class SBOAddon_DB:B1Db
    {
        public SBOAddon_DB():base() {
            SAPbobsCOM.Recordset ors = eCommon.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;

            try
            {
                //Check for required UDF, UDT and UDOs
                ors.DoQuery("SELECT TOP 1 U_TWM_ADDID FROM CRD1");
                ors.DoQuery("SELECT TOP 1 U_TWM_SSOUTLET FROM OCRD");
                ors.DoQuery("SELECT TOP 1 U_TWM_B2BRC, U_TWM_DFIBC, U_TWM_PODTE FROM OINV");
                ors.DoQuery("SELECT TOP 1 U_TWM_CSITC FROM INV1");
                ors.DoQuery("SELECT TOP 1 U_TWM_B2BC, U_TWM_FLNM, U_TWM_DRCT, U_TWM_OBTP, U_TWM_STTS, U_TWM_RMRK FROM [@TWM_OB2BL]");
                ors.DoQuery("SELECT TOP 1 U_TWM_OBTP, U_TWM_ENTR, U_TWM_LNUM, U_TWM_SSEQ, U_TWM_STTS, U_TWM_QATY, U_TWM_RMRK FROM [@TWM_B2BL1]");
                ors.DoQuery("SELECT TOP 1 U_TWM_CARDC, U_TWM_CARDN, U_TWM_XCUST, U_TWM_XVEND, U_TWM_RMRKS FROM [@TWM_OB2BS]");
                ors.DoQuery("SELECT TOP 1 U_TWM_ITYPE, U_TWM_PRTCL, U_TWM_ADDRS, U_TWM_PORT, U_TWM_USERN, U_TWM_USRPW, U_TWM_FOLDR, U_TWM_ARCHF, U_TWM_RFLDR FROM [@TWM_B2BS1]");
                ors.DoQuery("SELECT TOP 1 U_ADDON, U_Key, U_Dcrption, U_KeyValue, U_UpdateOn, U_UpdateBy FROM [@TWM_CSTSET]");
                ors.DoQuery("SELECT TOP 1 U_TWM_CSITM FROM [@TWM_DFICN]");

                ors.DoQuery("SELECT Count(*) FROM  OUDO WHERE Code in ('TWM_OB2BL', 'TWM_OB2BS', 'TWM_DFICN')");
                if ((int)ors.Fields.Item(0).Value < 2)
                {
                    throw new Exception("UDO");
                }
            }
            catch 
            {
                //One or more metadata not found. try to recreate them.
                if (ors != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ors);
                    ors = null;
                    GC.Collect();
                }

                eCommon.SBO_Application.StatusBar.SetText(SBOAddon.gcAddOnName + " - Setting User Objects.", BoMessageTime.bmt_Long, BoStatusBarMessageType.smt_Warning);
                Tables = new B1DbTable[] { 
                                new B1DbTable("@TWM_OB2BL", "B2B Log", BoUTBTableType.bott_Document)
                                , new B1DbTable("@TWM_B2BL1", "B2B Detail Log", BoUTBTableType.bott_DocumentLines)
                                , new B1DbTable("@TWM_OB2BS", "B2B Master Data", BoUTBTableType.bott_MasterData)
                                , new B1DbTable("@TWM_B2BS1", "B2B Folders", BoUTBTableType.bott_MasterDataLines)
                                , new B1DbTable("@TWM_CSTSET", "TWM Customisation setting", BoUTBTableType.bott_NoObject)
                                , new B1DbTable("@TWM_DFICN", "DFI Catalog Number", BoUTBTableType.bott_MasterData)
                        };

                Columns = new B1DbColumn[] 
                       {    new B1DbColumn("@TWM_CSTSET", "ADDON", "Add On Name", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_CSTSET", "Key", "Setting Key", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1WizardBase.B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_CSTSET", "Dcrption", "Setting Description", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1WizardBase.B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_CSTSET", "KeyValue", "Key Value", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1WizardBase.B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_CSTSET", "UpdateOn", "Last Update On", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 15, true, new B1WizardBase.B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_CSTSET", "UpdateBy", "Last Update By", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1WizardBase.B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_OB2BL", "TWM_B2BC", "B2BCode", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1WizardBase.B1DbValidValue[0], -1) 
                            , new B1DbColumn("@TWM_OB2BL", "TWM_FLNM", "FileName", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1WizardBase.B1DbValidValue[0], -1) 
                            , new B1DbColumn("@TWM_OB2BL", "TWM_DRCT", "Direction", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 1, true, new B1WizardBase.B1DbValidValue[] {new B1WizardBase.B1DbValidValue("E","Export"), new B1WizardBase.B1DbValidValue("I","Import")},-1 )
                            , new B1DbColumn("@TWM_OB2BL", "TWM_OBTP", "Object Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 15, true, new B1WizardBase.B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_OB2BL", "TWM_STTS", "Status", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 1, true, new B1WizardBase.B1DbValidValue[2] {new B1WizardBase.B1DbValidValue("F","Failed"), new B1WizardBase.B1DbValidValue("S","Success")},-1 )
                            , new B1DbColumn("@TWM_OB2BL", "TWM_RMRK", "Remark", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1WizardBase.B1DbValidValue[0], -1) 
                            , new B1DbColumn("@TWM_B2BL1", "TWM_OBTP", "Object Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 21, true, new B1WizardBase.B1DbValidValue[0], -1) 
                            , new B1DbColumn("@TWM_B2BL1", "TWM_ENTR", "DocEntry", BoFieldTypes.db_Numeric, BoFldSubTypes.st_None, 8, true, new B1WizardBase.B1DbValidValue[0], -1) 
                            , new B1DbColumn("@TWM_B2BL1", "TWM_LNUM", "LineNum", BoFieldTypes.db_Numeric, BoFldSubTypes.st_None, 8, true, new B1WizardBase.B1DbValidValue[0], -1) 
                            , new B1DbColumn("@TWM_B2BL1", "TWM_SSEQ", "Source Line", BoFieldTypes.db_Numeric, BoFldSubTypes.st_None, 8, true, new B1WizardBase.B1DbValidValue[0], -1) 
                            , new B1DbColumn("@TWM_B2BL1", "TWM_QATY", "Quantity", BoFieldTypes.db_Float, BoFldSubTypes.st_Quantity, 25, true, new B1WizardBase.B1DbValidValue[0], -1) 
                            , new B1DbColumn("@TWM_B2BL1", "TWM_STTS", "Status", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 1, true, new B1WizardBase.B1DbValidValue[] {new B1WizardBase.B1DbValidValue("F","Failed"), new B1WizardBase.B1DbValidValue("S","Success")},-1 )
                            , new B1DbColumn("@TWM_B2BL1", "TWM_RMRK", "Remark", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1WizardBase.B1DbValidValue[0],-1 )
                            , new B1DbColumn("@TWM_OB2BS", "TWM_CARDC", "CardCode", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 25, true, new B1WizardBase.B1DbValidValue[0],-1)
                            , new B1DbColumn("@TWM_OB2BS", "TWM_CARDN", "CardName", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 100, true, new B1WizardBase.B1DbValidValue[0],-1)
                            , new B1DbColumn("@TWM_OB2BS", "TWM_XCUST", "External Cust Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1WizardBase.B1DbValidValue[0],-1)
                            , new B1DbColumn("@TWM_OB2BS", "TWM_XVEND", "External Vend Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 30, true, new B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_OB2BS", "TWM_RMRKS", "Remarks", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1DbValidValue[0],-1)
                            , new B1DbColumn("@TWM_B2BS1", "TWM_ITYPE", "Int Type", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 1, true, new B1DbValidValue[] {new B1DbValidValue("I","Inbound"), new B1DbValidValue("O","OutBound")}, 0)
                            , new B1DbColumn("@TWM_B2BS1", "TWM_PRTCL", "Protocol", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 1, true, new B1DbValidValue[] {new B1DbValidValue("0","Local"), new B1DbValidValue("1", "FTP"), new B1DbValidValue("2", "sFTP")},0)
                            , new B1DbColumn("@TWM_B2BS1", "TWM_FOLDR", "Folder", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1DbValidValue[0],-1) 
                            , new B1DbColumn("@TWM_B2BS1", "TWM_ARCHF", "Archive Folder", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1DbValidValue[0],-1) 
                            , new B1DbColumn("@TWM_B2BS1", "TWM_ADDRS", "Address", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_B2BS1", "TWM_PORT", "Port", BoFieldTypes.db_Numeric, BoFldSubTypes.st_None, 4, true, new B1DbValidValue[0], -1)
                            , new B1DbColumn("@TWM_B2BS1", "TWM_USERN", "UserName", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1DbValidValue[0],-1)
                            , new B1DbColumn("@TWM_B2BS1", "TWM_USRPW", "Password", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1DbValidValue[0],-1)
                            , new B1DbColumn("@TWM_B2BS1", "TWM_RFLDR", "Remote Folder", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 254, true, new B1DbValidValue[0],-1) 
                            , new B1DbColumn("CRD1", "TWM_ADDID", "Xternal ID", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 25, true, new B1DbValidValue[0],-1) 
                            , new B1DbColumn("OCRD", "TWM_SSOUTLET", "Shengsiong Branch Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 10, true, new B1DbValidValue[0],-1) 
                            , new B1DbColumn("OINV", "TWM_B2BRC", "B2B Record", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 1, true, new B1DbValidValue[] {new B1DbValidValue("N","No"), new B1DbValidValue("Y","Yes")}, 0)
                            , new B1DbColumn("OINV", "TWM_DFIBC", "B2B: DFI Business Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1DbValidValue[0],-1) 
                            , new B1DbColumn("OINV", "TWM_PODTE", "B2B: Cust PO Date", BoFieldTypes.db_Date, BoFldSubTypes.st_None, 8, true, new B1DbValidValue[0],-1) 
                            , new B1DbColumn("INV1", "TWM_CSITC", "Customer ItemCode", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 50, true, new B1DbValidValue[0],-1) 
                            , new B1DbColumn("@TWM_DFICN", "TWM_CSITM", "CSGroup Item Code", BoFieldTypes.db_Alpha, BoFldSubTypes.st_None, 20, true, new B1DbValidValue[0],-1) 
                       };

                Udos = new B1Udo[] 
                        { 
                            new B1Udo("TWM_OB2BL", "B2B Log", "TWM_OB2BL", new string[] {"TWM_B2BL1"}, BoUDOObjType.boud_Document, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO, null, new string[] {"DocEntry"}, new string[] {"DocNum"}) 
                            , new B1Udo("TWM_OB2BS", "B2B Master", "TWM_OB2BS", new string[] {"TWM_B2BS1"}, BoUDOObjType.boud_MasterData, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO, null, new string[] {"Code"}, new string[] {"Name"}) 
                            , new B1Udo("TWM_DFICN", "DFI Catalog Number", "TWM_DFICN", new string[0], BoUDOObjType.boud_MasterData, BoYesNoEnum.tYES, BoYesNoEnum.tYES, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tNO, BoYesNoEnum.tYES, BoYesNoEnum.tNO, "ATWM_DFICN", new string[] {"Code"}, new string[] {"Name"}) 
                        };

                eCommon.SBO_Application.MetadataAutoRefresh = false;
                try
                {
                    this.Add(eCommon.oCompany);
                }
                catch (Exception Ex)
                {
                    eCommon.SBO_Application.StatusBar.SetText(Ex.Message, BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Error);
                }
                finally
                {
                    eCommon.SBO_Application.MetadataAutoRefresh = true;
                }
            }

            if (ors != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ors);
                ors = null;
                GC.Collect();
            }


        }
    }
}
