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

//Last modification on: 24/12/2022 6:47:16

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 24/12/2022 6:47:16
    /// </summary>
    public partial class RequirementTypeService : RequirementTypeProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RequirementTypeService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RequirementTypeModel Select1ByRequirementTypeIdToModel(int RequirementTypeId)
        {
            return new RequirementTypeModel().Select1ByRequirementTypeIdToModel(RequirementTypeId);
        }

        public List<RequirementTypeModel> SelectAllToList()
        {
            return new RequirementTypeModel().SelectAllToList();
        }

        public requirementtypeModelQuery SelectAllPagedToModel(requirementtypeModelQuery requirementtypeModelQuery)
        {
            return new RequirementTypeModel().SelectAllPagedToModel(requirementtypeModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RequirementTypeModel RequirementTypeModel)
        {
            return new RequirementTypeModel().Insert(RequirementTypeModel);
        }

        public int UpdateByRequirementTypeId(RequirementTypeModel RequirementTypeModel)
        {
            return new RequirementTypeModel().UpdateByRequirementTypeId(RequirementTypeModel);
        }

        public int DeleteByRequirementTypeId(int RequirementTypeId)
        {
            return new RequirementTypeModel().DeleteByRequirementTypeId(RequirementTypeId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RequirementTypeModel RequirementTypeModel = new RequirementTypeModel();
                RequirementTypeModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementTypeModel RequirementTypeModel = new RequirementTypeModel().Select1ByRequirementTypeIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RequirementTypeModel.DeleteByRequirementTypeId(RequirementTypeModel.RequirementTypeId);
                }
            }
        }

        public int CopyByRequirementTypeId(int RequirementTypeId)
        {
            RequirementTypeModel RequirementTypeModel = new RequirementTypeModel().Select1ByRequirementTypeIdToModel(RequirementTypeId);
            int NewEnteredId = new RequirementTypeModel().Insert(RequirementTypeModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RequirementTypeModel> lstRequirementTypeModel = new List<RequirementTypeModel> { };
                lstRequirementTypeModel = new RequirementTypeModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRequirementTypeModel.Count];

                for (int i = 0; i < lstRequirementTypeModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRequirementTypeModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RequirementTypeModel RequirementTypeModel = new RequirementTypeModel().Select1ByRequirementTypeIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RequirementTypeModel.Insert();
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
            List<RequirementTypeModel> lstRequirementTypeModel = new List<RequirementTypeModel> { };

            if (ExportationType == "All")
            {
                lstRequirementTypeModel = new RequirementTypeModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementTypeModel RequirementTypeModel = new RequirementTypeModel().Select1ByRequirementTypeIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementTypeModel.Add(RequirementTypeModel);
                }
            }

            foreach (RequirementTypeModel row in lstRequirementTypeModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of RequirementType</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RequirementTypeId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/RequirementType/RequirementType_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRequirementType = new DataTable();
                dtRequirementType.TableName = "RequirementType";

                //We define another DataTable dtRequirementTypeCopy to avoid issue related to DateTime conversion
                DataTable dtRequirementTypeCopy = new DataTable();
                dtRequirementTypeCopy.TableName = "RequirementType";

                #region Define columns for dtRequirementTypeCopy
                DataColumn dtColumnRequirementTypeIdFordtRequirementTypeCopy = new DataColumn();
                    dtColumnRequirementTypeIdFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnRequirementTypeIdFordtRequirementTypeCopy.ColumnName = "RequirementTypeId";
                    dtRequirementTypeCopy.Columns.Add(dtColumnRequirementTypeIdFordtRequirementTypeCopy);

                    DataColumn dtColumnActiveFordtRequirementTypeCopy = new DataColumn();
                    dtColumnActiveFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementTypeCopy.ColumnName = "Active";
                    dtRequirementTypeCopy.Columns.Add(dtColumnActiveFordtRequirementTypeCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementTypeCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementTypeCopy.ColumnName = "DateTimeCreation";
                    dtRequirementTypeCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementTypeCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementTypeCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementTypeCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementTypeCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementTypeCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementTypeCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementTypeCopy.ColumnName = "UserCreationId";
                    dtRequirementTypeCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementTypeCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementTypeCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementTypeCopy.ColumnName = "UserLastModificationId";
                    dtRequirementTypeCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementTypeCopy);

                    DataColumn dtColumnNameFordtRequirementTypeCopy = new DataColumn();
                    dtColumnNameFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnNameFordtRequirementTypeCopy.ColumnName = "Name";
                    dtRequirementTypeCopy.Columns.Add(dtColumnNameFordtRequirementTypeCopy);

                    DataColumn dtColumnDescriptionFordtRequirementTypeCopy = new DataColumn();
                    dtColumnDescriptionFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnDescriptionFordtRequirementTypeCopy.ColumnName = "Description";
                    dtRequirementTypeCopy.Columns.Add(dtColumnDescriptionFordtRequirementTypeCopy);

                    
                #endregion

                dtRequirementType = new RequirementTypeModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRequirementType.Rows)
                {
                    dtRequirementTypeCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRequirementTypeCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementTypeing/RequirementType/RequirementType_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRequirementType = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRequirementTypeCopy to avoid issue related to DateTime conversion
                    DataTable dtRequirementTypeCopy = new DataTable();
                    dtRequirementTypeCopy.TableName = "RequirementType";

                    #region Define columns for dtRequirementTypeCopy
                    DataColumn dtColumnRequirementTypeIdFordtRequirementTypeCopy = new DataColumn();
                    dtColumnRequirementTypeIdFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnRequirementTypeIdFordtRequirementTypeCopy.ColumnName = "RequirementTypeId";
                    dtRequirementTypeCopy.Columns.Add(dtColumnRequirementTypeIdFordtRequirementTypeCopy);

                    DataColumn dtColumnActiveFordtRequirementTypeCopy = new DataColumn();
                    dtColumnActiveFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnActiveFordtRequirementTypeCopy.ColumnName = "Active";
                    dtRequirementTypeCopy.Columns.Add(dtColumnActiveFordtRequirementTypeCopy);

                    DataColumn dtColumnDateTimeCreationFordtRequirementTypeCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRequirementTypeCopy.ColumnName = "DateTimeCreation";
                    dtRequirementTypeCopy.Columns.Add(dtColumnDateTimeCreationFordtRequirementTypeCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRequirementTypeCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRequirementTypeCopy.ColumnName = "DateTimeLastModification";
                    dtRequirementTypeCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRequirementTypeCopy);

                    DataColumn dtColumnUserCreationIdFordtRequirementTypeCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRequirementTypeCopy.ColumnName = "UserCreationId";
                    dtRequirementTypeCopy.Columns.Add(dtColumnUserCreationIdFordtRequirementTypeCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRequirementTypeCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRequirementTypeCopy.ColumnName = "UserLastModificationId";
                    dtRequirementTypeCopy.Columns.Add(dtColumnUserLastModificationIdFordtRequirementTypeCopy);

                    DataColumn dtColumnNameFordtRequirementTypeCopy = new DataColumn();
                    dtColumnNameFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnNameFordtRequirementTypeCopy.ColumnName = "Name";
                    dtRequirementTypeCopy.Columns.Add(dtColumnNameFordtRequirementTypeCopy);

                    DataColumn dtColumnDescriptionFordtRequirementTypeCopy = new DataColumn();
                    dtColumnDescriptionFordtRequirementTypeCopy.DataType = typeof(string);
                    dtColumnDescriptionFordtRequirementTypeCopy.ColumnName = "Description";
                    dtRequirementTypeCopy.Columns.Add(dtColumnDescriptionFordtRequirementTypeCopy);

                    
                    #endregion

                    dsRequirementType.Tables.Add(dtRequirementTypeCopy);

                    for (int i = 0; i < dsRequirementType.Tables.Count; i++)
                    {
                        dtRequirementTypeCopy = new RequirementTypeModel().Select1ByRequirementTypeIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRequirementTypeCopy.Rows)
                        {
                            dsRequirementType.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRequirementType.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRequirementType.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/RequirementTypeing/RequirementType/RequirementType_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RequirementTypeModel> lstRequirementTypeModel = new List<RequirementTypeModel> { };

            if (ExportationType == "All")
            {
                lstRequirementTypeModel = new RequirementTypeModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RequirementTypeModel RequirementTypeModel = new RequirementTypeModel().Select1ByRequirementTypeIdToModel(Convert.ToInt32(RowChecked));
                    lstRequirementTypeModel.Add(RequirementTypeModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/RequirementTypeing/RequirementType/RequirementType_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRequirementTypeModel);
            }

            return Now;
        }
        #endregion
    }
}