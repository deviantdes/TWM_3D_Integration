using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TWM_3D_Integration
{
    class ReadAndUpdateDB
    {
        private bool readSuccessful = true;
        public ReadAndUpdateDB()
        {
            
            SAPbobsCOM.Recordset oRSTableInvHeader = eCommon.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            SAPbobsCOM.Recordset oRSTableInvDetail= eCommon.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            SAPbobsCOM.Recordset oRSTablePayment = eCommon.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
            SAPbobsCOM.Recordset oRSTableItemView = eCommon.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;

            // Get Data
            oRSTableInvHeader = readTableInvHeader();
            oRSTableInvDetail = readTableInvDetail();
            oRSTablePayment = readTablePayment();
            oRSTableItemView = readTableItemView();

            // Update Data
            if (readSuccessful)
            {

            }
        }

        private SAPbobsCOM.Recordset readTableInvHeader()
        {
            try
            {
                SAPbobsCOM.Recordset oRS = eCommon.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
                String sSQL = String.Format("select * from [SBO_3D_Integration].[dbo].[Inv_Header]");
                oRS.DoQuery(sSQL);
                if (oRS.RecordCount < 1)
                    return null;
                else
                    return oRS;           
            }
            catch (Exception Ex)
            {
                readSuccessful = false;
                eCommon.WriteEventLog("Error reading from Inv_Header table. " + Ex.Message);
            }
            return null;

        }

        private SAPbobsCOM.Recordset readTableInvDetail()
        {
            try
            {
                SAPbobsCOM.Recordset oRS = eCommon.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
                String sSQL = String.Format("select * from [SBO_3D_Integration].[dbo].[Inv_Detail]");
                oRS.DoQuery(sSQL);
                if (oRS.RecordCount < 1)
                    return null;
                else
                    return oRS;
            }
            catch (Exception Ex)
            {
                readSuccessful = false;
                eCommon.WriteEventLog("Error reading from Inv_Detail table. " + Ex.Message);
            }
            return null;
        }

        private SAPbobsCOM.Recordset readTablePayment()
        {
            try
            {
                SAPbobsCOM.Recordset oRS = eCommon.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
                String sSQL = String.Format("select * from [SBO_3D_Integration].[dbo].[Payment]");
                oRS.DoQuery(sSQL);
                if (oRS.RecordCount < 1)
                    return null;
                else
                    return oRS;
            }
            catch (Exception Ex)
            {
                readSuccessful = false;
                eCommon.WriteEventLog("Error reading from Payment table. " + Ex.Message);
            }
            return null;
        }

        private SAPbobsCOM.Recordset readTableItemView()
        {
            try
            {
                SAPbobsCOM.Recordset oRS = eCommon.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset) as SAPbobsCOM.Recordset;
                String sSQL = String.Format("select * from [SBO_3D_Integration].[dbo].[Item_View]");
                oRS.DoQuery(sSQL);
                if (oRS.RecordCount < 1)
                    return null;
                else
                    return oRS;
            }
            catch (Exception Ex)
            {
                readSuccessful = false;
                eCommon.WriteEventLog("Error reading from Item_View table. " + Ex.Message);
            }
            return null;
        }
    }
}
