using ClosedXML.Excel;
using CsvHelper;
using IronPdf;
using Microsoft.AspNetCore.Http;
using FiyiRequirements.Areas.Requirement.Models;
using FiyiRequirements.Areas.Requirement.Protocols;
using FiyiRequirements.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2022
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

//Last modification on: 24/12/2022 6:47:32

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 24/12/2022 6:47:32
    /// </summary>
    public partial class ClientService : ClientProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ClientService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public ClientModel Select1ByClientIdToModel(int ClientId)
        {
            return new ClientModel().Select1ByClientIdToModel(ClientId);
        }

        public List<ClientModel> SelectAllToList()
        {
            return new ClientModel().SelectAllToList();
        }

        public clientModelQuery SelectAllPagedToModel(clientModelQuery clientModelQuery)
        {
            return new ClientModel().SelectAllPagedToModel(clientModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(ClientModel ClientModel)
        {
            return new ClientModel().Insert(ClientModel);
        }

        public int UpdateByClientId(ClientModel ClientModel)
        {
            return new ClientModel().UpdateByClientId(ClientModel);
        }

        public int DeleteByClientId(int ClientId)
        {
            return new ClientModel().DeleteByClientId(ClientId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                ClientModel ClientModel = new ClientModel();
                ClientModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ClientModel ClientModel = new ClientModel().Select1ByClientIdToModel(Convert.ToInt32(RowsChecked[i]));
                    ClientModel.DeleteByClientId(ClientModel.ClientId);
                }
            }
        }

        public int CopyByClientId(int ClientId)
        {
            ClientModel ClientModel = new ClientModel().Select1ByClientIdToModel(ClientId);
            int NewEnteredId = new ClientModel().Insert(ClientModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<ClientModel> lstClientModel = new List<ClientModel> { };
                lstClientModel = new ClientModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstClientModel.Count];

                for (int i = 0; i < lstClientModel.Count; i++)
                {
                    NewEnteredIds[i] = lstClientModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ClientModel ClientModel = new ClientModel().Select1ByClientIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = ClientModel.Insert();
                }

                return NewEnteredIds;
            }
        }
        #endregion

        #region Other services
        public DateTime ExportAsPDF(Ajax Ajax, string ExportationType)
        {
            var Renderer = new HtmlToPdf();
            DateTime Now = DateTime.Now;
            string RowsAsHTML = "";
            List<ClientModel> lstClientModel = new List<ClientModel> { };

            if (ExportationType == "All")
            {
                lstClientModel = new ClientModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ClientModel ClientModel = new ClientModel().Select1ByClientIdToModel(Convert.ToInt32(RowChecked));
                    lstClientModel.Add(ClientModel);
                }
            }

            foreach (ClientModel row in lstClientModel)
            {
                RowsAsHTML += $@"{row.ToStringOnlyValuesForHTML()}";
            }

            Renderer.RenderHtmlAsPdf($@"<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""88%"" style=""width: 88% !important; min-width: 88%; max-width: 88%;"">
    <tr>
    <td align=""left"" valign=""top"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#1a1a1a"" style=""font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #1a1a1a; font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">Mikromatica</span>
        </font>
        <div style=""height: 25px; line-height: 25px; font-size: 23px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#4c4c4c"" style=""font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Client</span>
        </font>
        <div style=""height: 35px; line-height: 35px; font-size: 33px;"">&nbsp;</div>
    </td>
    </tr>
</table>
<br>
<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""width: 100% !important; min-width: 100%; max-width: 100%;"">
    <tr>
        <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ClientId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Active&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeCreation&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeLastModification&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserCreationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserLastModificationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">FirstName&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">LastName&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">BusinessName&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">PhoneNumber&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Email&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th>
    </tr>
    {RowsAsHTML}
</table>
<br>
<font face=""'Source Sans Pro', sans-serif"" color=""#868686"" style=""font-size: 17px; line-height: 20px;"">
    <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #868686; font-size: 17px; line-height: 20px;"">Printed on: {Now}</span>
</font>
").SaveAs($@"wwwroot/PDFFiles/Requirement/Client/Client_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtClient = new DataTable();
                dtClient.TableName = "Client";

                //We define another DataTable dtClientCopy to avoid issue related to DateTime conversion
                DataTable dtClientCopy = new DataTable();
                dtClientCopy.TableName = "Client";

                #region Define columns for dtClientCopy
                DataColumn dtColumnClientIdFordtClientCopy = new DataColumn();
                    dtColumnClientIdFordtClientCopy.DataType = typeof(string);
                    dtColumnClientIdFordtClientCopy.ColumnName = "ClientId";
                    dtClientCopy.Columns.Add(dtColumnClientIdFordtClientCopy);

                    DataColumn dtColumnActiveFordtClientCopy = new DataColumn();
                    dtColumnActiveFordtClientCopy.DataType = typeof(string);
                    dtColumnActiveFordtClientCopy.ColumnName = "Active";
                    dtClientCopy.Columns.Add(dtColumnActiveFordtClientCopy);

                    DataColumn dtColumnDateTimeCreationFordtClientCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtClientCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtClientCopy.ColumnName = "DateTimeCreation";
                    dtClientCopy.Columns.Add(dtColumnDateTimeCreationFordtClientCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtClientCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtClientCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtClientCopy.ColumnName = "DateTimeLastModification";
                    dtClientCopy.Columns.Add(dtColumnDateTimeLastModificationFordtClientCopy);

                    DataColumn dtColumnUserCreationIdFordtClientCopy = new DataColumn();
                    dtColumnUserCreationIdFordtClientCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtClientCopy.ColumnName = "UserCreationId";
                    dtClientCopy.Columns.Add(dtColumnUserCreationIdFordtClientCopy);

                    DataColumn dtColumnUserLastModificationIdFordtClientCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtClientCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtClientCopy.ColumnName = "UserLastModificationId";
                    dtClientCopy.Columns.Add(dtColumnUserLastModificationIdFordtClientCopy);

                    DataColumn dtColumnFirstNameFordtClientCopy = new DataColumn();
                    dtColumnFirstNameFordtClientCopy.DataType = typeof(string);
                    dtColumnFirstNameFordtClientCopy.ColumnName = "FirstName";
                    dtClientCopy.Columns.Add(dtColumnFirstNameFordtClientCopy);

                    DataColumn dtColumnLastNameFordtClientCopy = new DataColumn();
                    dtColumnLastNameFordtClientCopy.DataType = typeof(string);
                    dtColumnLastNameFordtClientCopy.ColumnName = "LastName";
                    dtClientCopy.Columns.Add(dtColumnLastNameFordtClientCopy);

                    DataColumn dtColumnBusinessNameFordtClientCopy = new DataColumn();
                    dtColumnBusinessNameFordtClientCopy.DataType = typeof(string);
                    dtColumnBusinessNameFordtClientCopy.ColumnName = "BusinessName";
                    dtClientCopy.Columns.Add(dtColumnBusinessNameFordtClientCopy);

                    DataColumn dtColumnPhoneNumberFordtClientCopy = new DataColumn();
                    dtColumnPhoneNumberFordtClientCopy.DataType = typeof(string);
                    dtColumnPhoneNumberFordtClientCopy.ColumnName = "PhoneNumber";
                    dtClientCopy.Columns.Add(dtColumnPhoneNumberFordtClientCopy);

                    DataColumn dtColumnEmailFordtClientCopy = new DataColumn();
                    dtColumnEmailFordtClientCopy.DataType = typeof(string);
                    dtColumnEmailFordtClientCopy.ColumnName = "Email";
                    dtClientCopy.Columns.Add(dtColumnEmailFordtClientCopy);

                    
                #endregion

                dtClient = new ClientModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtClient.Rows)
                {
                    dtClientCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtClientCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Clienting/Client/Client_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsClient = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtClientCopy to avoid issue related to DateTime conversion
                    DataTable dtClientCopy = new DataTable();
                    dtClientCopy.TableName = "Client";

                    #region Define columns for dtClientCopy
                    DataColumn dtColumnClientIdFordtClientCopy = new DataColumn();
                    dtColumnClientIdFordtClientCopy.DataType = typeof(string);
                    dtColumnClientIdFordtClientCopy.ColumnName = "ClientId";
                    dtClientCopy.Columns.Add(dtColumnClientIdFordtClientCopy);

                    DataColumn dtColumnActiveFordtClientCopy = new DataColumn();
                    dtColumnActiveFordtClientCopy.DataType = typeof(string);
                    dtColumnActiveFordtClientCopy.ColumnName = "Active";
                    dtClientCopy.Columns.Add(dtColumnActiveFordtClientCopy);

                    DataColumn dtColumnDateTimeCreationFordtClientCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtClientCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtClientCopy.ColumnName = "DateTimeCreation";
                    dtClientCopy.Columns.Add(dtColumnDateTimeCreationFordtClientCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtClientCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtClientCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtClientCopy.ColumnName = "DateTimeLastModification";
                    dtClientCopy.Columns.Add(dtColumnDateTimeLastModificationFordtClientCopy);

                    DataColumn dtColumnUserCreationIdFordtClientCopy = new DataColumn();
                    dtColumnUserCreationIdFordtClientCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtClientCopy.ColumnName = "UserCreationId";
                    dtClientCopy.Columns.Add(dtColumnUserCreationIdFordtClientCopy);

                    DataColumn dtColumnUserLastModificationIdFordtClientCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtClientCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtClientCopy.ColumnName = "UserLastModificationId";
                    dtClientCopy.Columns.Add(dtColumnUserLastModificationIdFordtClientCopy);

                    DataColumn dtColumnFirstNameFordtClientCopy = new DataColumn();
                    dtColumnFirstNameFordtClientCopy.DataType = typeof(string);
                    dtColumnFirstNameFordtClientCopy.ColumnName = "FirstName";
                    dtClientCopy.Columns.Add(dtColumnFirstNameFordtClientCopy);

                    DataColumn dtColumnLastNameFordtClientCopy = new DataColumn();
                    dtColumnLastNameFordtClientCopy.DataType = typeof(string);
                    dtColumnLastNameFordtClientCopy.ColumnName = "LastName";
                    dtClientCopy.Columns.Add(dtColumnLastNameFordtClientCopy);

                    DataColumn dtColumnBusinessNameFordtClientCopy = new DataColumn();
                    dtColumnBusinessNameFordtClientCopy.DataType = typeof(string);
                    dtColumnBusinessNameFordtClientCopy.ColumnName = "BusinessName";
                    dtClientCopy.Columns.Add(dtColumnBusinessNameFordtClientCopy);

                    DataColumn dtColumnPhoneNumberFordtClientCopy = new DataColumn();
                    dtColumnPhoneNumberFordtClientCopy.DataType = typeof(string);
                    dtColumnPhoneNumberFordtClientCopy.ColumnName = "PhoneNumber";
                    dtClientCopy.Columns.Add(dtColumnPhoneNumberFordtClientCopy);

                    DataColumn dtColumnEmailFordtClientCopy = new DataColumn();
                    dtColumnEmailFordtClientCopy.DataType = typeof(string);
                    dtColumnEmailFordtClientCopy.ColumnName = "Email";
                    dtClientCopy.Columns.Add(dtColumnEmailFordtClientCopy);

                    
                    #endregion

                    dsClient.Tables.Add(dtClientCopy);

                    for (int i = 0; i < dsClient.Tables.Count; i++)
                    {
                        dtClientCopy = new ClientModel().Select1ByClientIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtClientCopy.Rows)
                        {
                            dsClient.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsClient.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsClient.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Clienting/Client/Client_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<ClientModel> lstClientModel = new List<ClientModel> { };

            if (ExportationType == "All")
            {
                lstClientModel = new ClientModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ClientModel ClientModel = new ClientModel().Select1ByClientIdToModel(Convert.ToInt32(RowChecked));
                    lstClientModel.Add(ClientModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Clienting/Client/Client_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstClientModel);
            }

            return Now;
        }
        #endregion
    }
}