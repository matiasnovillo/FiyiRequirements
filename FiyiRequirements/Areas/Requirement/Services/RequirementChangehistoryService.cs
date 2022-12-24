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

//Last modification on: 24/12/2022 6:48:12

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 24/12/2022 6:48:12
    /// </summary>
    public partial class RequirementChangehistoryService : RequirementChangehistoryProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RequirementChangehistoryService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RequirementChangehistoryModel Select1ByRequirementChangehistoryIdToModel(int RequirementChangehistoryId)
        {
            return new RequirementChangehistoryModel().Select1ByRequirementChangehistoryIdToModel(RequirementChangehistoryId);
        }

        public List<RequirementChangehistoryModel> SelectAllToList()
        {
            return new RequirementChangehistoryModel().SelectAllToList();
        }

        public requirementchangehistoryModelQuery SelectAllPagedToModel(requirementchangehistoryModelQuery requirementchangehistoryModelQuery)
        {
            return new RequirementChangehistoryModel().SelectAllPagedToModel(requirementchangehistoryModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RequirementChangehistoryModel RequirementChangehistoryModel)
        {
            return new RequirementChangehistoryModel().Insert(RequirementChangehistoryModel);
        }

        public int UpdateByRequirementChangehistoryId(RequirementChangehistoryModel RequirementChangehistoryModel)
        {
            return new RequirementChangehistoryModel().UpdateByRequirementChangehistoryId(RequirementChangehistoryModel);
        }

        public int DeleteByRequirementChangehistoryId(int RequirementChangehistoryId)
        {
            return new RequirementChangehistoryModel().DeleteByRequirementChangehistoryId(RequirementChangehistoryId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RequirementChangehistoryModel RequirementChangehistoryModel = new RequirementChangehistoryModel();
                RequirementChangehistoryModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementChangehistoryModel RequirementChangehistoryModel = new RequirementChangehistoryModel().Select1ByRequirementChangehistoryIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RequirementChangehistoryModel.DeleteByRequirementChangehistoryId(RequirementChangehistoryModel.RequirementChangehistoryId);
                }
            }
        }

        public int CopyByRequirementChangehistoryId(int RequirementChangehistoryId)
        {
            RequirementChangehistoryModel RequirementChangehistoryModel = new RequirementChangehistoryModel().Select1ByRequirementChangehistoryIdToModel(RequirementChangehistoryId);
            int NewEnteredId = new RequirementChangehistoryModel().Insert(RequirementChangehistoryModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel> { };
                lstRequirementChangehistoryModel = new RequirementChangehistoryModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRequirementChangehistoryModel.Count];

                for (int i = 0; i < lstRequirementChangehistoryModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRequirementChangehistoryModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementChangehistoryModel RequirementChangehistoryModel = new RequirementChangehistoryModel().Select1ByRequirementChangehistoryIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RequirementChangehistoryModel.Insert();
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
            List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel> { };

            if (ExportationType == "All")
            {
                lstRequirementChangehistoryModel = new RequirementChangehistoryModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementChangehistoryModel RequirementChangehistoryModel = new RequirementChangehistoryModel().Select1ByRequirementChangehistoryIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementChangehistoryModel.Add(RequirementChangehistoryModel);
                }
            }

            foreach (RequirementChangehistoryModel row in lstRequirementChangehistoryModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of RequirementChangehistory</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementChangehistoryId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementStateId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementPriorityId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/RequirementChangehistory/RequirementChangehistory_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRequirementChangehistory = new DataTable();
                dtRequirementChangehistory.TableName = "RequirementChangehistory";

                //We define another DataTable dtRequirementChangehistoryCopy to avoid issue related to DateTime conversion
                DataTable dtRequirementChangehistoryCopy = new DataTable();
                dtRequirementChangehistoryCopy.TableName = "RequirementChangehistory";

                #region Define columns for dtRequirementChangehistoryCopy
                DataColumn dtColumnRequirementChangehistoryIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnRequirementChangehistoryIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnRequirementChangehistoryIdFordtRequirementChangehistoryCopy.ColumnName = "RequirementChangehistoryId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnRequirementChangehistoryIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnActiveFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnActiveFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementChangehistoryCopy.ColumnName = "Active";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnActiveFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementChangehistoryCopy.ColumnName = "DateTimeCreation";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementChangehistoryCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementChangehistoryCopy.ColumnName = "UserCreationId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementChangehistoryCopy.ColumnName = "UserLastModificationId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnRequirementIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnRequirementIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnRequirementIdFordtRequirementChangehistoryCopy.ColumnName = "RequirementId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnRequirementIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnRequirementStateIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnRequirementStateIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnRequirementStateIdFordtRequirementChangehistoryCopy.ColumnName = "RequirementStateId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnRequirementStateIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnRequirementPriorityIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnRequirementPriorityIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnRequirementPriorityIdFordtRequirementChangehistoryCopy.ColumnName = "RequirementPriorityId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnRequirementPriorityIdFordtRequirementChangehistoryCopy);

                    
                #endregion

                dtRequirementChangehistory = new RequirementChangehistoryModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRequirementChangehistory.Rows)
                {
                    dtRequirementChangehistoryCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRequirementChangehistoryCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementChangehistorying/RequirementChangehistory/RequirementChangehistory_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRequirementChangehistory = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRequirementChangehistoryCopy to avoid issue related to DateTime conversion
                    DataTable dtRequirementChangehistoryCopy = new DataTable();
                    dtRequirementChangehistoryCopy.TableName = "RequirementChangehistory";

                    #region Define columns for dtRequirementChangehistoryCopy
                    DataColumn dtColumnRequirementChangehistoryIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnRequirementChangehistoryIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnRequirementChangehistoryIdFordtRequirementChangehistoryCopy.ColumnName = "RequirementChangehistoryId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnRequirementChangehistoryIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnActiveFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnActiveFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementChangehistoryCopy.ColumnName = "Active";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnActiveFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementChangehistoryCopy.ColumnName = "DateTimeCreation";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementChangehistoryCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementChangehistoryCopy.ColumnName = "UserCreationId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementChangehistoryCopy.ColumnName = "UserLastModificationId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnRequirementIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnRequirementIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnRequirementIdFordtRequirementChangehistoryCopy.ColumnName = "RequirementId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnRequirementIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnRequirementStateIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnRequirementStateIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnRequirementStateIdFordtRequirementChangehistoryCopy.ColumnName = "RequirementStateId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnRequirementStateIdFordtRequirementChangehistoryCopy);

                    DataColumn dtColumnRequirementPriorityIdFordtRequirementChangehistoryCopy = new DataColumn();
                    dtColumnRequirementPriorityIdFordtRequirementChangehistoryCopy.DataType = typeof(string);
                    dtColumnRequirementPriorityIdFordtRequirementChangehistoryCopy.ColumnName = "RequirementPriorityId";
                    dtRequirementChangehistoryCopy.Columns.Add(dtColumnRequirementPriorityIdFordtRequirementChangehistoryCopy);

                    
                    #endregion

                    dsRequirementChangehistory.Tables.Add(dtRequirementChangehistoryCopy);

                    for (int i = 0; i < dsRequirementChangehistory.Tables.Count; i++)
                    {
                        dtRequirementChangehistoryCopy = new RequirementChangehistoryModel().Select1ByRequirementChangehistoryIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRequirementChangehistoryCopy.Rows)
                        {
                            dsRequirementChangehistory.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRequirementChangehistory.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRequirementChangehistory.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementChangehistorying/RequirementChangehistory/RequirementChangehistory_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel> { };

            if (ExportationType == "All")
            {
                lstRequirementChangehistoryModel = new RequirementChangehistoryModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementChangehistoryModel RequirementChangehistoryModel = new RequirementChangehistoryModel().Select1ByRequirementChangehistoryIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementChangehistoryModel.Add(RequirementChangehistoryModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/RequirementChangehistorying/RequirementChangehistory/RequirementChangehistory_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRequirementChangehistoryModel);
            }

            return Now;
        }
        #endregion
    }
}