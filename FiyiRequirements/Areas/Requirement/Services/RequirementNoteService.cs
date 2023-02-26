using ClosedXML.Excel;
using CsvHelper;
using IronPdf;
using Microsoft.AspNetCore.Http;
using FiyiRequirements.Areas.Requirement.Models;
using FiyiRequirements.Areas.Requirement.DTOs;
using FiyiRequirements.Areas.Requirement.Interfaces;
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

//Last modification on: 21/02/2023 20:56:35

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 21/02/2023 20:56:35
    /// </summary>
    public partial class RequirementNoteService : IRequirementNote
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RequirementNoteService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RequirementNoteModel Select1ByRequirementNoteIdToModel(int RequirementNoteId)
        {
            return new RequirementNoteModel().Select1ByRequirementNoteIdToModel(RequirementNoteId);
        }

        public List<RequirementNoteModel> SelectAllToList()
        {
            return new RequirementNoteModel().SelectAllToList();
        }

        public requirementnoteSelectAllPaged SelectAllPagedToModel(requirementnoteSelectAllPaged requirementnoteSelectAllPaged, int RequirementId)
        {
            return new RequirementNoteModel().SelectAllPagedToModel(requirementnoteSelectAllPaged, RequirementId);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RequirementNoteModel RequirementNoteModel)
        {
            return new RequirementNoteModel().Insert(RequirementNoteModel);
        }

        public int UpdateByRequirementNoteId(RequirementNoteModel RequirementNoteModel)
        {
            return new RequirementNoteModel().UpdateByRequirementNoteId(RequirementNoteModel);
        }

        public int DeleteByRequirementNoteId(int RequirementNoteId)
        {
            return new RequirementNoteModel().DeleteByRequirementNoteId(RequirementNoteId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RequirementNoteModel RequirementNoteModel = new RequirementNoteModel();
                RequirementNoteModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementNoteModel RequirementNoteModel = new RequirementNoteModel().Select1ByRequirementNoteIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RequirementNoteModel.DeleteByRequirementNoteId(RequirementNoteModel.RequirementNoteId);
                }
            }
        }

        public int CopyByRequirementNoteId(int RequirementNoteId)
        {
            RequirementNoteModel RequirementNoteModel = new RequirementNoteModel().Select1ByRequirementNoteIdToModel(RequirementNoteId);
            int NewEnteredId = new RequirementNoteModel().Insert(RequirementNoteModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RequirementNoteModel> lstRequirementNoteModel = new List<RequirementNoteModel> { };
                lstRequirementNoteModel = new RequirementNoteModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRequirementNoteModel.Count];

                for (int i = 0; i < lstRequirementNoteModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRequirementNoteModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementNoteModel RequirementNoteModel = new RequirementNoteModel().Select1ByRequirementNoteIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RequirementNoteModel.Insert();
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
            List<RequirementNoteModel> lstRequirementNoteModel = new List<RequirementNoteModel> { };

            if (ExportationType == "All")
            {
                lstRequirementNoteModel = new RequirementNoteModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementNoteModel RequirementNoteModel = new RequirementNoteModel().Select1ByRequirementNoteIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementNoteModel.Add(RequirementNoteModel);
                }
            }

            foreach (RequirementNoteModel row in lstRequirementNoteModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of RequirementNote</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementNoteId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Title&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Body&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/RequirementNote/RequirementNote_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRequirementNote = new DataTable();
                dtRequirementNote.TableName = "RequirementNote";

                //We define another DataTable dtRequirementNoteCopy to avoid issue related to DateTime conversion
                DataTable dtRequirementNoteCopy = new DataTable();
                dtRequirementNoteCopy.TableName = "RequirementNote";

                #region Define columns for dtRequirementNoteCopy
                DataColumn dtColumnRequirementNoteIdFordtRequirementNoteCopy = new DataColumn();
                    dtColumnRequirementNoteIdFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnRequirementNoteIdFordtRequirementNoteCopy.ColumnName = "RequirementNoteId";
                    dtRequirementNoteCopy.Columns.Add(dtColumnRequirementNoteIdFordtRequirementNoteCopy);

                    DataColumn dtColumnActiveFordtRequirementNoteCopy = new DataColumn();
                    dtColumnActiveFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementNoteCopy.ColumnName = "Active";
                    dtRequirementNoteCopy.Columns.Add(dtColumnActiveFordtRequirementNoteCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementNoteCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementNoteCopy.ColumnName = "DateTimeCreation";
                    dtRequirementNoteCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementNoteCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementNoteCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementNoteCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementNoteCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementNoteCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementNoteCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementNoteCopy.ColumnName = "UserCreationId";
                    dtRequirementNoteCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementNoteCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementNoteCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementNoteCopy.ColumnName = "UserLastModificationId";
                    dtRequirementNoteCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementNoteCopy);

                    DataColumn dtColumnTitleFordtRequirementNoteCopy = new DataColumn();
                    dtColumnTitleFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnTitleFordtRequirementNoteCopy.ColumnName = "Title";
                    dtRequirementNoteCopy.Columns.Add(dtColumnTitleFordtRequirementNoteCopy);

                    DataColumn dtColumnBodyFordtRequirementNoteCopy = new DataColumn();
                    dtColumnBodyFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnBodyFordtRequirementNoteCopy.ColumnName = "Body";
                    dtRequirementNoteCopy.Columns.Add(dtColumnBodyFordtRequirementNoteCopy);

                    DataColumn dtColumnRequirementIdFordtRequirementNoteCopy = new DataColumn();
                    dtColumnRequirementIdFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnRequirementIdFordtRequirementNoteCopy.ColumnName = "RequirementId";
                    dtRequirementNoteCopy.Columns.Add(dtColumnRequirementIdFordtRequirementNoteCopy);

                    
                #endregion

                dtRequirementNote = new RequirementNoteModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRequirementNote.Rows)
                {
                    dtRequirementNoteCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRequirementNoteCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementNoteing/RequirementNote/RequirementNote_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRequirementNote = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRequirementNoteCopy to avoid issue related to DateTime conversion
                    DataTable dtRequirementNoteCopy = new DataTable();
                    dtRequirementNoteCopy.TableName = "RequirementNote";

                    #region Define columns for dtRequirementNoteCopy
                    DataColumn dtColumnRequirementNoteIdFordtRequirementNoteCopy = new DataColumn();
                    dtColumnRequirementNoteIdFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnRequirementNoteIdFordtRequirementNoteCopy.ColumnName = "RequirementNoteId";
                    dtRequirementNoteCopy.Columns.Add(dtColumnRequirementNoteIdFordtRequirementNoteCopy);

                    DataColumn dtColumnActiveFordtRequirementNoteCopy = new DataColumn();
                    dtColumnActiveFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementNoteCopy.ColumnName = "Active";
                    dtRequirementNoteCopy.Columns.Add(dtColumnActiveFordtRequirementNoteCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementNoteCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementNoteCopy.ColumnName = "DateTimeCreation";
                    dtRequirementNoteCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementNoteCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementNoteCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementNoteCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementNoteCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementNoteCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementNoteCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementNoteCopy.ColumnName = "UserCreationId";
                    dtRequirementNoteCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementNoteCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementNoteCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementNoteCopy.ColumnName = "UserLastModificationId";
                    dtRequirementNoteCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementNoteCopy);

                    DataColumn dtColumnTitleFordtRequirementNoteCopy = new DataColumn();
                    dtColumnTitleFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnTitleFordtRequirementNoteCopy.ColumnName = "Title";
                    dtRequirementNoteCopy.Columns.Add(dtColumnTitleFordtRequirementNoteCopy);

                    DataColumn dtColumnBodyFordtRequirementNoteCopy = new DataColumn();
                    dtColumnBodyFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnBodyFordtRequirementNoteCopy.ColumnName = "Body";
                    dtRequirementNoteCopy.Columns.Add(dtColumnBodyFordtRequirementNoteCopy);

                    DataColumn dtColumnRequirementIdFordtRequirementNoteCopy = new DataColumn();
                    dtColumnRequirementIdFordtRequirementNoteCopy.DataType = typeof(string);
                    dtColumnRequirementIdFordtRequirementNoteCopy.ColumnName = "RequirementId";
                    dtRequirementNoteCopy.Columns.Add(dtColumnRequirementIdFordtRequirementNoteCopy);

                    
                    #endregion

                    dsRequirementNote.Tables.Add(dtRequirementNoteCopy);

                    for (int i = 0; i < dsRequirementNote.Tables.Count; i++)
                    {
                        dtRequirementNoteCopy = new RequirementNoteModel().Select1ByRequirementNoteIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRequirementNoteCopy.Rows)
                        {
                            dsRequirementNote.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRequirementNote.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRequirementNote.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementNoteing/RequirementNote/RequirementNote_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RequirementNoteModel> lstRequirementNoteModel = new List<RequirementNoteModel> { };

            if (ExportationType == "All")
            {
                lstRequirementNoteModel = new RequirementNoteModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementNoteModel RequirementNoteModel = new RequirementNoteModel().Select1ByRequirementNoteIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementNoteModel.Add(RequirementNoteModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/RequirementNoteing/RequirementNote/RequirementNote_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRequirementNoteModel);
            }

            return Now;
        }
        #endregion
    }
}