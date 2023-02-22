using ClosedXML.Excel;
using CsvHelper;
using IronPdf;
using Microsoft.AspNetCore.Http;
using FiyiRequirements.Areas.Requirement.Models;
using FiyiRequirements.Areas.Requirement.DTOs;
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
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

//Last modification on: 21/02/2023 21:13:53

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 21/02/2023 21:13:53
    /// </summary>
    public partial class RequirementStateService : RequirementStateProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RequirementStateService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RequirementStateModel Select1ByRequirementStateIdToModel(int RequirementStateId)
        {
            return new RequirementStateModel().Select1ByRequirementStateIdToModel(RequirementStateId);
        }

        public List<RequirementStateModel> SelectAllToList()
        {
            return new RequirementStateModel().SelectAllToList();
        }

        public requirementstateSelectAllPaged SelectAllPagedToModel(requirementstateSelectAllPaged requirementstateSelectAllPaged)
        {
            return new RequirementStateModel().SelectAllPagedToModel(requirementstateSelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RequirementStateModel RequirementStateModel)
        {
            return new RequirementStateModel().Insert(RequirementStateModel);
        }

        public int UpdateByRequirementStateId(RequirementStateModel RequirementStateModel)
        {
            return new RequirementStateModel().UpdateByRequirementStateId(RequirementStateModel);
        }

        public int DeleteByRequirementStateId(int RequirementStateId)
        {
            return new RequirementStateModel().DeleteByRequirementStateId(RequirementStateId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RequirementStateModel RequirementStateModel = new RequirementStateModel();
                RequirementStateModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementStateModel RequirementStateModel = new RequirementStateModel().Select1ByRequirementStateIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RequirementStateModel.DeleteByRequirementStateId(RequirementStateModel.RequirementStateId);
                }
            }
        }

        public int CopyByRequirementStateId(int RequirementStateId)
        {
            RequirementStateModel RequirementStateModel = new RequirementStateModel().Select1ByRequirementStateIdToModel(RequirementStateId);
            int NewEnteredId = new RequirementStateModel().Insert(RequirementStateModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RequirementStateModel> lstRequirementStateModel = new List<RequirementStateModel> { };
                lstRequirementStateModel = new RequirementStateModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRequirementStateModel.Count];

                for (int i = 0; i < lstRequirementStateModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRequirementStateModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementStateModel RequirementStateModel = new RequirementStateModel().Select1ByRequirementStateIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RequirementStateModel.Insert();
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
            List<RequirementStateModel> lstRequirementStateModel = new List<RequirementStateModel> { };

            if (ExportationType == "All")
            {
                lstRequirementStateModel = new RequirementStateModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementStateModel RequirementStateModel = new RequirementStateModel().Select1ByRequirementStateIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementStateModel.Add(RequirementStateModel);
                }
            }

            foreach (RequirementStateModel row in lstRequirementStateModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of RequirementState</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementStateId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Name&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/RequirementState/RequirementState_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRequirementState = new DataTable();
                dtRequirementState.TableName = "RequirementState";

                //We define another DataTable dtRequirementStateCopy to avoid issue related to DateTime conversion
                DataTable dtRequirementStateCopy = new DataTable();
                dtRequirementStateCopy.TableName = "RequirementState";

                #region Define columns for dtRequirementStateCopy
                DataColumn dtColumnRequirementStateIdFordtRequirementStateCopy = new DataColumn();
                    dtColumnRequirementStateIdFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnRequirementStateIdFordtRequirementStateCopy.ColumnName = "RequirementStateId";
                    dtRequirementStateCopy.Columns.Add(dtColumnRequirementStateIdFordtRequirementStateCopy);

                    DataColumn dtColumnActiveFordtRequirementStateCopy = new DataColumn();
                    dtColumnActiveFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementStateCopy.ColumnName = "Active";
                    dtRequirementStateCopy.Columns.Add(dtColumnActiveFordtRequirementStateCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementStateCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementStateCopy.ColumnName = "DateTimeCreation";
                    dtRequirementStateCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementStateCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementStateCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementStateCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementStateCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementStateCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementStateCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementStateCopy.ColumnName = "UserCreationId";
                    dtRequirementStateCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementStateCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementStateCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementStateCopy.ColumnName = "UserLastModificationId";
                    dtRequirementStateCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementStateCopy);

                    DataColumn dtColumnNameFordtRequirementStateCopy = new DataColumn();
                    dtColumnNameFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnNameFordtRequirementStateCopy.ColumnName = "Name";
                    dtRequirementStateCopy.Columns.Add(dtColumnNameFordtRequirementStateCopy);

                    
                #endregion

                dtRequirementState = new RequirementStateModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRequirementState.Rows)
                {
                    dtRequirementStateCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRequirementStateCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementStateing/RequirementState/RequirementState_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRequirementState = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRequirementStateCopy to avoid issue related to DateTime conversion
                    DataTable dtRequirementStateCopy = new DataTable();
                    dtRequirementStateCopy.TableName = "RequirementState";

                    #region Define columns for dtRequirementStateCopy
                    DataColumn dtColumnRequirementStateIdFordtRequirementStateCopy = new DataColumn();
                    dtColumnRequirementStateIdFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnRequirementStateIdFordtRequirementStateCopy.ColumnName = "RequirementStateId";
                    dtRequirementStateCopy.Columns.Add(dtColumnRequirementStateIdFordtRequirementStateCopy);

                    DataColumn dtColumnActiveFordtRequirementStateCopy = new DataColumn();
                    dtColumnActiveFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementStateCopy.ColumnName = "Active";
                    dtRequirementStateCopy.Columns.Add(dtColumnActiveFordtRequirementStateCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementStateCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementStateCopy.ColumnName = "DateTimeCreation";
                    dtRequirementStateCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementStateCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementStateCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementStateCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementStateCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementStateCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementStateCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementStateCopy.ColumnName = "UserCreationId";
                    dtRequirementStateCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementStateCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementStateCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementStateCopy.ColumnName = "UserLastModificationId";
                    dtRequirementStateCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementStateCopy);

                    DataColumn dtColumnNameFordtRequirementStateCopy = new DataColumn();
                    dtColumnNameFordtRequirementStateCopy.DataType = typeof(string);
                    dtColumnNameFordtRequirementStateCopy.ColumnName = "Name";
                    dtRequirementStateCopy.Columns.Add(dtColumnNameFordtRequirementStateCopy);

                    
                    #endregion

                    dsRequirementState.Tables.Add(dtRequirementStateCopy);

                    for (int i = 0; i < dsRequirementState.Tables.Count; i++)
                    {
                        dtRequirementStateCopy = new RequirementStateModel().Select1ByRequirementStateIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRequirementStateCopy.Rows)
                        {
                            dsRequirementState.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRequirementState.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRequirementState.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementStateing/RequirementState/RequirementState_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RequirementStateModel> lstRequirementStateModel = new List<RequirementStateModel> { };

            if (ExportationType == "All")
            {
                lstRequirementStateModel = new RequirementStateModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementStateModel RequirementStateModel = new RequirementStateModel().Select1ByRequirementStateIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementStateModel.Add(RequirementStateModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/RequirementStateing/RequirementState/RequirementState_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRequirementStateModel);
            }

            return Now;
        }
        #endregion
    }
}