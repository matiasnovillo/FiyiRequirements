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

//Last modification on: 29/12/2022 10:16:50

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 29/12/2022 10:16:50
    /// </summary>
    public partial class RequirementFileService : RequirementFileProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RequirementFileService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RequirementFileModel Select1ByRequirementFileIdToModel(int RequirementFileId)
        {
            return new RequirementFileModel().Select1ByRequirementFileIdToModel(RequirementFileId);
        }

        public List<RequirementFileModel> SelectAllToList()
        {
            return new RequirementFileModel().SelectAllToList();
        }

        public requirementfileModelQuery SelectAllPagedToModel(requirementfileModelQuery requirementfileModelQuery, int RequirementId)
        {
            return new RequirementFileModel().SelectAllPagedToModel(requirementfileModelQuery, RequirementId);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RequirementFileModel RequirementFileModel)
        {
            return new RequirementFileModel().Insert(RequirementFileModel);
        }

        public int UpdateByRequirementFileId(RequirementFileModel RequirementFileModel)
        {
            return new RequirementFileModel().UpdateByRequirementFileId(RequirementFileModel);
        }

        public int DeleteByRequirementFileId(int RequirementFileId)
        {
            return new RequirementFileModel().DeleteByRequirementFileId(RequirementFileId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RequirementFileModel RequirementFileModel = new RequirementFileModel();
                RequirementFileModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementFileModel RequirementFileModel = new RequirementFileModel().Select1ByRequirementFileIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RequirementFileModel.DeleteByRequirementFileId(RequirementFileModel.RequirementFileId);
                }
            }
        }

        public int CopyByRequirementFileId(int RequirementFileId)
        {
            RequirementFileModel RequirementFileModel = new RequirementFileModel().Select1ByRequirementFileIdToModel(RequirementFileId);
            int NewEnteredId = new RequirementFileModel().Insert(RequirementFileModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RequirementFileModel> lstRequirementFileModel = new List<RequirementFileModel> { };
                lstRequirementFileModel = new RequirementFileModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRequirementFileModel.Count];

                for (int i = 0; i < lstRequirementFileModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRequirementFileModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementFileModel RequirementFileModel = new RequirementFileModel().Select1ByRequirementFileIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RequirementFileModel.Insert();
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
            List<RequirementFileModel> lstRequirementFileModel = new List<RequirementFileModel> { };

            if (ExportationType == "All")
            {
                lstRequirementFileModel = new RequirementFileModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementFileModel RequirementFileModel = new RequirementFileModel().Select1ByRequirementFileIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementFileModel.Add(RequirementFileModel);
                }
            }

            foreach (RequirementFileModel row in lstRequirementFileModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of RequirementFile</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementFileId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">FilePath&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/RequirementFile/RequirementFile_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRequirementFile = new DataTable();
                dtRequirementFile.TableName = "RequirementFile";

                //We define another DataTable dtRequirementFileCopy to avoid issue related to DateTime conversion
                DataTable dtRequirementFileCopy = new DataTable();
                dtRequirementFileCopy.TableName = "RequirementFile";

                #region Define columns for dtRequirementFileCopy
                DataColumn dtColumnRequirementFileIdFordtRequirementFileCopy = new DataColumn();
                    dtColumnRequirementFileIdFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnRequirementFileIdFordtRequirementFileCopy.ColumnName = "RequirementFileId";
                    dtRequirementFileCopy.Columns.Add(dtColumnRequirementFileIdFordtRequirementFileCopy);

                    DataColumn dtColumnActiveFordtRequirementFileCopy = new DataColumn();
                    dtColumnActiveFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementFileCopy.ColumnName = "Active";
                    dtRequirementFileCopy.Columns.Add(dtColumnActiveFordtRequirementFileCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementFileCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementFileCopy.ColumnName = "DateTimeCreation";
                    dtRequirementFileCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementFileCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementFileCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementFileCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementFileCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementFileCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementFileCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementFileCopy.ColumnName = "UserCreationId";
                    dtRequirementFileCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementFileCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementFileCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementFileCopy.ColumnName = "UserLastModificationId";
                    dtRequirementFileCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementFileCopy);

                    DataColumn dtColumnRequirementIdFordtRequirementFileCopy = new DataColumn();
                    dtColumnRequirementIdFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnRequirementIdFordtRequirementFileCopy.ColumnName = "RequirementId";
                    dtRequirementFileCopy.Columns.Add(dtColumnRequirementIdFordtRequirementFileCopy);

                    DataColumn dtColumnFilePathFordtRequirementFileCopy = new DataColumn();
                    dtColumnFilePathFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnFilePathFordtRequirementFileCopy.ColumnName = "FilePath";
                    dtRequirementFileCopy.Columns.Add(dtColumnFilePathFordtRequirementFileCopy);

                    
                #endregion

                dtRequirementFile = new RequirementFileModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRequirementFile.Rows)
                {
                    dtRequirementFileCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRequirementFileCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementFileing/RequirementFile/RequirementFile_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRequirementFile = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRequirementFileCopy to avoid issue related to DateTime conversion
                    DataTable dtRequirementFileCopy = new DataTable();
                    dtRequirementFileCopy.TableName = "RequirementFile";

                    #region Define columns for dtRequirementFileCopy
                    DataColumn dtColumnRequirementFileIdFordtRequirementFileCopy = new DataColumn();
                    dtColumnRequirementFileIdFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnRequirementFileIdFordtRequirementFileCopy.ColumnName = "RequirementFileId";
                    dtRequirementFileCopy.Columns.Add(dtColumnRequirementFileIdFordtRequirementFileCopy);

                    DataColumn dtColumnActiveFordtRequirementFileCopy = new DataColumn();
                    dtColumnActiveFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementFileCopy.ColumnName = "Active";
                    dtRequirementFileCopy.Columns.Add(dtColumnActiveFordtRequirementFileCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementFileCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementFileCopy.ColumnName = "DateTimeCreation";
                    dtRequirementFileCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementFileCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementFileCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementFileCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementFileCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementFileCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementFileCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementFileCopy.ColumnName = "UserCreationId";
                    dtRequirementFileCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementFileCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementFileCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementFileCopy.ColumnName = "UserLastModificationId";
                    dtRequirementFileCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementFileCopy);

                    DataColumn dtColumnRequirementIdFordtRequirementFileCopy = new DataColumn();
                    dtColumnRequirementIdFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnRequirementIdFordtRequirementFileCopy.ColumnName = "RequirementId";
                    dtRequirementFileCopy.Columns.Add(dtColumnRequirementIdFordtRequirementFileCopy);

                    DataColumn dtColumnFilePathFordtRequirementFileCopy = new DataColumn();
                    dtColumnFilePathFordtRequirementFileCopy.DataType = typeof(string);
                    dtColumnFilePathFordtRequirementFileCopy.ColumnName = "FilePath";
                    dtRequirementFileCopy.Columns.Add(dtColumnFilePathFordtRequirementFileCopy);

                    
                    #endregion

                    dsRequirementFile.Tables.Add(dtRequirementFileCopy);

                    for (int i = 0; i < dsRequirementFile.Tables.Count; i++)
                    {
                        dtRequirementFileCopy = new RequirementFileModel().Select1ByRequirementFileIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRequirementFileCopy.Rows)
                        {
                            dsRequirementFile.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRequirementFile.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRequirementFile.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementFileing/RequirementFile/RequirementFile_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RequirementFileModel> lstRequirementFileModel = new List<RequirementFileModel> { };

            if (ExportationType == "All")
            {
                lstRequirementFileModel = new RequirementFileModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementFileModel RequirementFileModel = new RequirementFileModel().Select1ByRequirementFileIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementFileModel.Add(RequirementFileModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/RequirementFileing/RequirementFile/RequirementFile_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRequirementFileModel);
            }

            return Now;
        }
        #endregion
    }
}