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

//Last modification on: 24/12/2022 6:47:42

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 24/12/2022 6:47:42
    /// </summary>
    public partial class ClientApplicationService : ClientApplicationProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ClientApplicationService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public ClientApplicationModel Select1ByClientApplicationIdToModel(int ClientApplicationId)
        {
            return new ClientApplicationModel().Select1ByClientApplicationIdToModel(ClientApplicationId);
        }

        public List<ClientApplicationModel> SelectAllToList()
        {
            return new ClientApplicationModel().SelectAllToList();
        }

        public clientapplicationModelQuery SelectAllPagedToModel(clientapplicationModelQuery clientapplicationModelQuery)
        {
            return new ClientApplicationModel().SelectAllPagedToModel(clientapplicationModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(ClientApplicationModel ClientApplicationModel)
        {
            return new ClientApplicationModel().Insert(ClientApplicationModel);
        }

        public int UpdateByClientApplicationId(ClientApplicationModel ClientApplicationModel)
        {
            return new ClientApplicationModel().UpdateByClientApplicationId(ClientApplicationModel);
        }

        public int DeleteByClientApplicationId(int ClientApplicationId)
        {
            return new ClientApplicationModel().DeleteByClientApplicationId(ClientApplicationId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                ClientApplicationModel ClientApplicationModel = new ClientApplicationModel();
                ClientApplicationModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ClientApplicationModel ClientApplicationModel = new ClientApplicationModel().Select1ByClientApplicationIdToModel(Convert.ToInt32(RowsChecked[i]));
                    ClientApplicationModel.DeleteByClientApplicationId(ClientApplicationModel.ClientApplicationId);
                }
            }
        }

        public int CopyByClientApplicationId(int ClientApplicationId)
        {
            ClientApplicationModel ClientApplicationModel = new ClientApplicationModel().Select1ByClientApplicationIdToModel(ClientApplicationId);
            int NewEnteredId = new ClientApplicationModel().Insert(ClientApplicationModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<ClientApplicationModel> lstClientApplicationModel = new List<ClientApplicationModel> { };
                lstClientApplicationModel = new ClientApplicationModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstClientApplicationModel.Count];

                for (int i = 0; i < lstClientApplicationModel.Count; i++)
                {
                    NewEnteredIds[i] = lstClientApplicationModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ClientApplicationModel ClientApplicationModel = new ClientApplicationModel().Select1ByClientApplicationIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = ClientApplicationModel.Insert();
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
            List<ClientApplicationModel> lstClientApplicationModel = new List<ClientApplicationModel> { };

            if (ExportationType == "All")
            {
                lstClientApplicationModel = new ClientApplicationModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ClientApplicationModel ClientApplicationModel = new ClientApplicationModel().Select1ByClientApplicationIdToModel(Convert.ToInt32(RowChecked));
                    lstClientApplicationModel.Add(ClientApplicationModel);
                }
            }

            foreach (ClientApplicationModel row in lstClientApplicationModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of ClientApplication</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ClientApplicationId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ClientId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ApplicationId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/ClientApplication/ClientApplication_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtClientApplication = new DataTable();
                dtClientApplication.TableName = "ClientApplication";

                //We define another DataTable dtClientApplicationCopy to avoid issue related to DateTime conversion
                DataTable dtClientApplicationCopy = new DataTable();
                dtClientApplicationCopy.TableName = "ClientApplication";

                #region Define columns for dtClientApplicationCopy
                DataColumn dtColumnClientApplicationIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnClientApplicationIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnClientApplicationIdFordtClientApplicationCopy.ColumnName = "ClientApplicationId";
                    dtClientApplicationCopy.Columns.Add(dtColumnClientApplicationIdFordtClientApplicationCopy);

                    DataColumn dtColumnActiveFordtClientApplicationCopy = new DataColumn();
                    dtColumnActiveFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnActiveFordtClientApplicationCopy.ColumnName = "Active";
                    dtClientApplicationCopy.Columns.Add(dtColumnActiveFordtClientApplicationCopy);

                    DataColumn dtColumnDateTimeCreationFordtClientApplicationCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtClientApplicationCopy.ColumnName = "DateTimeCreation";
                    dtClientApplicationCopy.Columns.Add(dtColumnDateTimeCreationFordtClientApplicationCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtClientApplicationCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtClientApplicationCopy.ColumnName = "DateTimeLastModification";
                    dtClientApplicationCopy.Columns.Add(dtColumnDateTimeLastModificationFordtClientApplicationCopy);

                    DataColumn dtColumnUserCreationIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnUserCreationIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtClientApplicationCopy.ColumnName = "UserCreationId";
                    dtClientApplicationCopy.Columns.Add(dtColumnUserCreationIdFordtClientApplicationCopy);

                    DataColumn dtColumnUserLastModificationIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtClientApplicationCopy.ColumnName = "UserLastModificationId";
                    dtClientApplicationCopy.Columns.Add(dtColumnUserLastModificationIdFordtClientApplicationCopy);

                    DataColumn dtColumnClientIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnClientIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnClientIdFordtClientApplicationCopy.ColumnName = "ClientId";
                    dtClientApplicationCopy.Columns.Add(dtColumnClientIdFordtClientApplicationCopy);

                    DataColumn dtColumnApplicationIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnApplicationIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnApplicationIdFordtClientApplicationCopy.ColumnName = "ApplicationId";
                    dtClientApplicationCopy.Columns.Add(dtColumnApplicationIdFordtClientApplicationCopy);

                    
                #endregion

                dtClientApplication = new ClientApplicationModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtClientApplication.Rows)
                {
                    dtClientApplicationCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtClientApplicationCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/ClientApplicationing/ClientApplication/ClientApplication_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsClientApplication = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtClientApplicationCopy to avoid issue related to DateTime conversion
                    DataTable dtClientApplicationCopy = new DataTable();
                    dtClientApplicationCopy.TableName = "ClientApplication";

                    #region Define columns for dtClientApplicationCopy
                    DataColumn dtColumnClientApplicationIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnClientApplicationIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnClientApplicationIdFordtClientApplicationCopy.ColumnName = "ClientApplicationId";
                    dtClientApplicationCopy.Columns.Add(dtColumnClientApplicationIdFordtClientApplicationCopy);

                    DataColumn dtColumnActiveFordtClientApplicationCopy = new DataColumn();
                    dtColumnActiveFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnActiveFordtClientApplicationCopy.ColumnName = "Active";
                    dtClientApplicationCopy.Columns.Add(dtColumnActiveFordtClientApplicationCopy);

                    DataColumn dtColumnDateTimeCreationFordtClientApplicationCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtClientApplicationCopy.ColumnName = "DateTimeCreation";
                    dtClientApplicationCopy.Columns.Add(dtColumnDateTimeCreationFordtClientApplicationCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtClientApplicationCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtClientApplicationCopy.ColumnName = "DateTimeLastModification";
                    dtClientApplicationCopy.Columns.Add(dtColumnDateTimeLastModificationFordtClientApplicationCopy);

                    DataColumn dtColumnUserCreationIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnUserCreationIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtClientApplicationCopy.ColumnName = "UserCreationId";
                    dtClientApplicationCopy.Columns.Add(dtColumnUserCreationIdFordtClientApplicationCopy);

                    DataColumn dtColumnUserLastModificationIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtClientApplicationCopy.ColumnName = "UserLastModificationId";
                    dtClientApplicationCopy.Columns.Add(dtColumnUserLastModificationIdFordtClientApplicationCopy);

                    DataColumn dtColumnClientIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnClientIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnClientIdFordtClientApplicationCopy.ColumnName = "ClientId";
                    dtClientApplicationCopy.Columns.Add(dtColumnClientIdFordtClientApplicationCopy);

                    DataColumn dtColumnApplicationIdFordtClientApplicationCopy = new DataColumn();
                    dtColumnApplicationIdFordtClientApplicationCopy.DataType = typeof(string);
                    dtColumnApplicationIdFordtClientApplicationCopy.ColumnName = "ApplicationId";
                    dtClientApplicationCopy.Columns.Add(dtColumnApplicationIdFordtClientApplicationCopy);

                    
                    #endregion

                    dsClientApplication.Tables.Add(dtClientApplicationCopy);

                    for (int i = 0; i < dsClientApplication.Tables.Count; i++)
                    {
                        dtClientApplicationCopy = new ClientApplicationModel().Select1ByClientApplicationIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtClientApplicationCopy.Rows)
                        {
                            dsClientApplication.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsClientApplication.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsClientApplication.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/ClientApplicationing/ClientApplication/ClientApplication_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<ClientApplicationModel> lstClientApplicationModel = new List<ClientApplicationModel> { };

            if (ExportationType == "All")
            {
                lstClientApplicationModel = new ClientApplicationModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ClientApplicationModel ClientApplicationModel = new ClientApplicationModel().Select1ByClientApplicationIdToModel(Convert.ToInt32(RowChecked));
                    lstClientApplicationModel.Add(ClientApplicationModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/ClientApplicationing/ClientApplication/ClientApplication_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstClientApplicationModel);
            }

            return Now;
        }
        #endregion
    }
}