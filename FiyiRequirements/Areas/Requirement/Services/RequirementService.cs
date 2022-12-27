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

//Last modification on: 27/12/2022 17:32:21

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 27/12/2022 17:32:21
    /// </summary>
    public partial class RequirementService : RequirementProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RequirementService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RequirementModel Select1ByRequirementIdToModel(int RequirementId)
        {
            return new RequirementModel().Select1ByRequirementIdToModel(RequirementId);
        }

        public List<RequirementModel> SelectAllToList()
        {
            return new RequirementModel().SelectAllToList();
        }

        public requirementModelQuery SelectAllPagedToModel(requirementModelQuery requirementModelQuery)
        {
            return new RequirementModel().SelectAllPagedToModel(requirementModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RequirementModel RequirementModel)
        {
            return new RequirementModel().Insert(RequirementModel);
        }

        public int UpdateByRequirementId(RequirementModel RequirementModel)
        {
            return new RequirementModel().UpdateByRequirementId(RequirementModel);
        }

        public int DeleteByRequirementId(int RequirementId)
        {
            return new RequirementModel().DeleteByRequirementId(RequirementId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RequirementModel RequirementModel = new RequirementModel();
                RequirementModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementModel RequirementModel = new RequirementModel().Select1ByRequirementIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RequirementModel.DeleteByRequirementId(RequirementModel.RequirementId);
                }
            }
        }

        public int CopyByRequirementId(int RequirementId)
        {
            RequirementModel RequirementModel = new RequirementModel().Select1ByRequirementIdToModel(RequirementId);
            int NewEnteredId = new RequirementModel().Insert(RequirementModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RequirementModel> lstRequirementModel = new List<RequirementModel> { };
                lstRequirementModel = new RequirementModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRequirementModel.Count];

                for (int i = 0; i < lstRequirementModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRequirementModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementModel RequirementModel = new RequirementModel().Select1ByRequirementIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RequirementModel.Insert();
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
            List<RequirementModel> lstRequirementModel = new List<RequirementModel> { };

            if (ExportationType == "All")
            {
                lstRequirementModel = new RequirementModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementModel RequirementModel = new RequirementModel().Select1ByRequirementIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementModel.Add(RequirementModel);
                }
            }

            foreach (RequirementModel row in lstRequirementModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Requirement</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementStateId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementPriorityId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserProgrammerId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/Requirement/Requirement_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRequirement = new DataTable();
                dtRequirement.TableName = "Requirement";

                //We define another DataTable dtRequirementCopy to avoid issue related to DateTime conversion
                DataTable dtRequirementCopy = new DataTable();
                dtRequirementCopy.TableName = "Requirement";

                #region Define columns for dtRequirementCopy
                DataColumn dtColumnRequirementIdFordtRequirementCopy = new DataColumn();
                    dtColumnRequirementIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnRequirementIdFordtRequirementCopy.ColumnName = "RequirementId";
                    dtRequirementCopy.Columns.Add(dtColumnRequirementIdFordtRequirementCopy);

                    DataColumn dtColumnActiveFordtRequirementCopy = new DataColumn();
                    dtColumnActiveFordtRequirementCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementCopy.ColumnName = "Active";
                    dtRequirementCopy.Columns.Add(dtColumnActiveFordtRequirementCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementCopy.ColumnName = "DateTimeCreation";
                    dtRequirementCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementCopy.ColumnName = "UserCreationId";
                    dtRequirementCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementCopy.ColumnName = "UserLastModificationId";
                    dtRequirementCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementCopy);

                    DataColumn dtColumnTitleFordtRequirementCopy = new DataColumn();
                    dtColumnTitleFordtRequirementCopy.DataType = typeof(string);
                    dtColumnTitleFordtRequirementCopy.ColumnName = "Title";
                    dtRequirementCopy.Columns.Add(dtColumnTitleFordtRequirementCopy);

                    DataColumn dtColumnBodyFordtRequirementCopy = new DataColumn();
                    dtColumnBodyFordtRequirementCopy.DataType = typeof(string);
                    dtColumnBodyFordtRequirementCopy.ColumnName = "Body";
                    dtRequirementCopy.Columns.Add(dtColumnBodyFordtRequirementCopy);

                    DataColumn dtColumnRequirementStateIdFordtRequirementCopy = new DataColumn();
                    dtColumnRequirementStateIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnRequirementStateIdFordtRequirementCopy.ColumnName = "RequirementStateId";
                    dtRequirementCopy.Columns.Add(dtColumnRequirementStateIdFordtRequirementCopy);

                    DataColumn dtColumnRequirementPriorityIdFordtRequirementCopy = new DataColumn();
                    dtColumnRequirementPriorityIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnRequirementPriorityIdFordtRequirementCopy.ColumnName = "RequirementPriorityId";
                    dtRequirementCopy.Columns.Add(dtColumnRequirementPriorityIdFordtRequirementCopy);

                    DataColumn dtColumnUserProgrammerIdFordtRequirementCopy = new DataColumn();
                    dtColumnUserProgrammerIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnUserProgrammerIdFordtRequirementCopy.ColumnName = "UserProgrammerId";
                    dtRequirementCopy.Columns.Add(dtColumnUserProgrammerIdFordtRequirementCopy);

                    
                #endregion

                dtRequirement = new RequirementModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRequirement.Rows)
                {
                    dtRequirementCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRequirementCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Requirementing/Requirement/Requirement_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRequirement = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRequirementCopy to avoid issue related to DateTime conversion
                    DataTable dtRequirementCopy = new DataTable();
                    dtRequirementCopy.TableName = "Requirement";

                    #region Define columns for dtRequirementCopy
                    DataColumn dtColumnRequirementIdFordtRequirementCopy = new DataColumn();
                    dtColumnRequirementIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnRequirementIdFordtRequirementCopy.ColumnName = "RequirementId";
                    dtRequirementCopy.Columns.Add(dtColumnRequirementIdFordtRequirementCopy);

                    DataColumn dtColumnActiveFordtRequirementCopy = new DataColumn();
                    dtColumnActiveFordtRequirementCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementCopy.ColumnName = "Active";
                    dtRequirementCopy.Columns.Add(dtColumnActiveFordtRequirementCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementCopy.ColumnName = "DateTimeCreation";
                    dtRequirementCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementCopy.ColumnName = "UserCreationId";
                    dtRequirementCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementCopy.ColumnName = "UserLastModificationId";
                    dtRequirementCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementCopy);

                    DataColumn dtColumnTitleFordtRequirementCopy = new DataColumn();
                    dtColumnTitleFordtRequirementCopy.DataType = typeof(string);
                    dtColumnTitleFordtRequirementCopy.ColumnName = "Title";
                    dtRequirementCopy.Columns.Add(dtColumnTitleFordtRequirementCopy);

                    DataColumn dtColumnBodyFordtRequirementCopy = new DataColumn();
                    dtColumnBodyFordtRequirementCopy.DataType = typeof(string);
                    dtColumnBodyFordtRequirementCopy.ColumnName = "Body";
                    dtRequirementCopy.Columns.Add(dtColumnBodyFordtRequirementCopy);

                    DataColumn dtColumnRequirementStateIdFordtRequirementCopy = new DataColumn();
                    dtColumnRequirementStateIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnRequirementStateIdFordtRequirementCopy.ColumnName = "RequirementStateId";
                    dtRequirementCopy.Columns.Add(dtColumnRequirementStateIdFordtRequirementCopy);

                    DataColumn dtColumnRequirementPriorityIdFordtRequirementCopy = new DataColumn();
                    dtColumnRequirementPriorityIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnRequirementPriorityIdFordtRequirementCopy.ColumnName = "RequirementPriorityId";
                    dtRequirementCopy.Columns.Add(dtColumnRequirementPriorityIdFordtRequirementCopy);

                    DataColumn dtColumnUserProgrammerIdFordtRequirementCopy = new DataColumn();
                    dtColumnUserProgrammerIdFordtRequirementCopy.DataType = typeof(string);
                    dtColumnUserProgrammerIdFordtRequirementCopy.ColumnName = "UserProgrammerId";
                    dtRequirementCopy.Columns.Add(dtColumnUserProgrammerIdFordtRequirementCopy);

                    
                    #endregion

                    dsRequirement.Tables.Add(dtRequirementCopy);

                    for (int i = 0; i < dsRequirement.Tables.Count; i++)
                    {
                        dtRequirementCopy = new RequirementModel().Select1ByRequirementIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRequirementCopy.Rows)
                        {
                            dsRequirement.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRequirement.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRequirement.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Requirementing/Requirement/Requirement_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RequirementModel> lstRequirementModel = new List<RequirementModel> { };

            if (ExportationType == "All")
            {
                lstRequirementModel = new RequirementModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementModel RequirementModel = new RequirementModel().Select1ByRequirementIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementModel.Add(RequirementModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Requirementing/Requirement/Requirement_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRequirementModel);
            }

            return Now;
        }
        #endregion
    }
}