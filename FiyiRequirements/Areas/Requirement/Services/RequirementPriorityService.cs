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

//Last modification on: 21/02/2023 21:10:11

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 21/02/2023 21:10:11
    /// </summary>
    public partial class RequirementPriorityService : IRequirementPriority
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RequirementPriorityService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RequirementPriorityModel Select1ByRequirementPriorityIdToModel(int RequirementPriorityId)
        {
            return new RequirementPriorityModel().Select1ByRequirementPriorityIdToModel(RequirementPriorityId);
        }

        public List<RequirementPriorityModel> SelectAllToList()
        {
            return new RequirementPriorityModel().SelectAllToList();
        }

        public requirementprioritySelectAllPaged SelectAllPagedToModel(requirementprioritySelectAllPaged requirementprioritySelectAllPaged)
        {
            return new RequirementPriorityModel().SelectAllPagedToModel(requirementprioritySelectAllPaged);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RequirementPriorityModel RequirementPriorityModel)
        {
            return new RequirementPriorityModel().Insert(RequirementPriorityModel);
        }

        public int UpdateByRequirementPriorityId(RequirementPriorityModel RequirementPriorityModel)
        {
            return new RequirementPriorityModel().UpdateByRequirementPriorityId(RequirementPriorityModel);
        }

        public int DeleteByRequirementPriorityId(int RequirementPriorityId)
        {
            return new RequirementPriorityModel().DeleteByRequirementPriorityId(RequirementPriorityId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RequirementPriorityModel RequirementPriorityModel = new RequirementPriorityModel();
                RequirementPriorityModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementPriorityModel RequirementPriorityModel = new RequirementPriorityModel().Select1ByRequirementPriorityIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RequirementPriorityModel.DeleteByRequirementPriorityId(RequirementPriorityModel.RequirementPriorityId);
                }
            }
        }

        public int CopyByRequirementPriorityId(int RequirementPriorityId)
        {
            RequirementPriorityModel RequirementPriorityModel = new RequirementPriorityModel().Select1ByRequirementPriorityIdToModel(RequirementPriorityId);
            int NewEnteredId = new RequirementPriorityModel().Insert(RequirementPriorityModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RequirementPriorityModel> lstRequirementPriorityModel = new List<RequirementPriorityModel> { };
                lstRequirementPriorityModel = new RequirementPriorityModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRequirementPriorityModel.Count];

                for (int i = 0; i < lstRequirementPriorityModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRequirementPriorityModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementPriorityModel RequirementPriorityModel = new RequirementPriorityModel().Select1ByRequirementPriorityIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RequirementPriorityModel.Insert();
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
            List<RequirementPriorityModel> lstRequirementPriorityModel = new List<RequirementPriorityModel> { };

            if (ExportationType == "All")
            {
                lstRequirementPriorityModel = new RequirementPriorityModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementPriorityModel RequirementPriorityModel = new RequirementPriorityModel().Select1ByRequirementPriorityIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementPriorityModel.Add(RequirementPriorityModel);
                }
            }

            foreach (RequirementPriorityModel row in lstRequirementPriorityModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of RequirementPriority</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementPriorityId&nbsp;&nbsp;&nbsp;</span>
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
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Description&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/RequirementPriority/RequirementPriority_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRequirementPriority = new DataTable();
                dtRequirementPriority.TableName = "RequirementPriority";

                //We define another DataTable dtRequirementPriorityCopy to avoid issue related to DateTime conversion
                DataTable dtRequirementPriorityCopy = new DataTable();
                dtRequirementPriorityCopy.TableName = "RequirementPriority";

                #region Define columns for dtRequirementPriorityCopy
                DataColumn dtColumnRequirementPriorityIdFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnRequirementPriorityIdFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnRequirementPriorityIdFordtRequirementPriorityCopy.ColumnName = "RequirementPriorityId";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnRequirementPriorityIdFordtRequirementPriorityCopy);

                    DataColumn dtColumnActiveFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnActiveFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementPriorityCopy.ColumnName = "Active";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnActiveFordtRequirementPriorityCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementPriorityCopy.ColumnName = "DateTimeCreation";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementPriorityCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementPriorityCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementPriorityCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementPriorityCopy.ColumnName = "UserCreationId";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementPriorityCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementPriorityCopy.ColumnName = "UserLastModificationId";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementPriorityCopy);

                    DataColumn dtColumnNameFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnNameFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnNameFordtRequirementPriorityCopy.ColumnName = "Name";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnNameFordtRequirementPriorityCopy);

                    DataColumn dtColumnDescriptionFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnDescriptionFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnDescriptionFordtRequirementPriorityCopy.ColumnName = "Description";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnDescriptionFordtRequirementPriorityCopy);

                    
                #endregion

                dtRequirementPriority = new RequirementPriorityModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRequirementPriority.Rows)
                {
                    dtRequirementPriorityCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRequirementPriorityCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementPrioritying/RequirementPriority/RequirementPriority_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRequirementPriority = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRequirementPriorityCopy to avoid issue related to DateTime conversion
                    DataTable dtRequirementPriorityCopy = new DataTable();
                    dtRequirementPriorityCopy.TableName = "RequirementPriority";

                    #region Define columns for dtRequirementPriorityCopy
                    DataColumn dtColumnRequirementPriorityIdFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnRequirementPriorityIdFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnRequirementPriorityIdFordtRequirementPriorityCopy.ColumnName = "RequirementPriorityId";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnRequirementPriorityIdFordtRequirementPriorityCopy);

                    DataColumn dtColumnActiveFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnActiveFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementPriorityCopy.ColumnName = "Active";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnActiveFordtRequirementPriorityCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementPriorityCopy.ColumnName = "DateTimeCreation";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementPriorityCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementPriorityCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementPriorityCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementPriorityCopy.ColumnName = "UserCreationId";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementPriorityCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementPriorityCopy.ColumnName = "UserLastModificationId";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementPriorityCopy);

                    DataColumn dtColumnNameFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnNameFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnNameFordtRequirementPriorityCopy.ColumnName = "Name";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnNameFordtRequirementPriorityCopy);

                    DataColumn dtColumnDescriptionFordtRequirementPriorityCopy = new DataColumn();
                    dtColumnDescriptionFordtRequirementPriorityCopy.DataType = typeof(string);
                    dtColumnDescriptionFordtRequirementPriorityCopy.ColumnName = "Description";
                    dtRequirementPriorityCopy.Columns.Add(dtColumnDescriptionFordtRequirementPriorityCopy);

                    
                    #endregion

                    dsRequirementPriority.Tables.Add(dtRequirementPriorityCopy);

                    for (int i = 0; i < dsRequirementPriority.Tables.Count; i++)
                    {
                        dtRequirementPriorityCopy = new RequirementPriorityModel().Select1ByRequirementPriorityIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRequirementPriorityCopy.Rows)
                        {
                            dsRequirementPriority.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRequirementPriority.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRequirementPriority.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementPrioritying/RequirementPriority/RequirementPriority_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RequirementPriorityModel> lstRequirementPriorityModel = new List<RequirementPriorityModel> { };

            if (ExportationType == "All")
            {
                lstRequirementPriorityModel = new RequirementPriorityModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementPriorityModel RequirementPriorityModel = new RequirementPriorityModel().Select1ByRequirementPriorityIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementPriorityModel.Add(RequirementPriorityModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/RequirementPrioritying/RequirementPriority/RequirementPriority_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRequirementPriorityModel);
            }

            return Now;
        }
        #endregion
    }
}