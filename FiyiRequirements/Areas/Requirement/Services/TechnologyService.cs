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

//Last modification on: 25/12/2022 18:26:04

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 25/12/2022 18:26:04
    /// </summary>
    public partial class TechnologyService : TechnologyProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public TechnologyService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public TechnologyModel Select1ByTechnologyIdToModel(int TechnologyId)
        {
            return new TechnologyModel().Select1ByTechnologyIdToModel(TechnologyId);
        }

        public List<TechnologyModel> SelectAllToList()
        {
            return new TechnologyModel().SelectAllToList();
        }

        public technologyModelQuery SelectAllPagedToModel(technologyModelQuery technologyModelQuery)
        {
            return new TechnologyModel().SelectAllPagedToModel(technologyModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(TechnologyModel TechnologyModel)
        {
            return new TechnologyModel().Insert(TechnologyModel);
        }

        public int UpdateByTechnologyId(TechnologyModel TechnologyModel)
        {
            return new TechnologyModel().UpdateByTechnologyId(TechnologyModel);
        }

        public int DeleteByTechnologyId(int TechnologyId)
        {
            return new TechnologyModel().DeleteByTechnologyId(TechnologyId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                TechnologyModel TechnologyModel = new TechnologyModel();
                TechnologyModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    TechnologyModel TechnologyModel = new TechnologyModel().Select1ByTechnologyIdToModel(Convert.ToInt32(RowsChecked[i]));
                    TechnologyModel.DeleteByTechnologyId(TechnologyModel.TechnologyId);
                }
            }
        }

        public int CopyByTechnologyId(int TechnologyId)
        {
            TechnologyModel TechnologyModel = new TechnologyModel().Select1ByTechnologyIdToModel(TechnologyId);
            int NewEnteredId = new TechnologyModel().Insert(TechnologyModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<TechnologyModel> lstTechnologyModel = new List<TechnologyModel> { };
                lstTechnologyModel = new TechnologyModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstTechnologyModel.Count];

                for (int i = 0; i < lstTechnologyModel.Count; i++)
                {
                    NewEnteredIds[i] = lstTechnologyModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    TechnologyModel TechnologyModel = new TechnologyModel().Select1ByTechnologyIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = TechnologyModel.Insert();
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
            List<TechnologyModel> lstTechnologyModel = new List<TechnologyModel> { };

            if (ExportationType == "All")
            {
                lstTechnologyModel = new TechnologyModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    TechnologyModel TechnologyModel = new TechnologyModel().Select1ByTechnologyIdToModel(Convert.ToInt32(RowChecked));
                    lstTechnologyModel.Add(TechnologyModel);
                }
            }

            foreach (TechnologyModel row in lstTechnologyModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Technology</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TechnologyId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/Technology/Technology_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtTechnology = new DataTable();
                dtTechnology.TableName = "Technology";

                //We define another DataTable dtTechnologyCopy to avoid issue related to DateTime conversion
                DataTable dtTechnologyCopy = new DataTable();
                dtTechnologyCopy.TableName = "Technology";

                #region Define columns for dtTechnologyCopy
                DataColumn dtColumnTechnologyIdFordtTechnologyCopy = new DataColumn();
                    dtColumnTechnologyIdFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnTechnologyIdFordtTechnologyCopy.ColumnName = "TechnologyId";
                    dtTechnologyCopy.Columns.Add(dtColumnTechnologyIdFordtTechnologyCopy);

                    DataColumn dtColumnActiveFordtTechnologyCopy = new DataColumn();
                    dtColumnActiveFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnActiveFordtTechnologyCopy.ColumnName = "Active";
                    dtTechnologyCopy.Columns.Add(dtColumnActiveFordtTechnologyCopy);

                    DataColumn dtColumnDateTimeCreationFordtTechnologyCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtTechnologyCopy.ColumnName = "DateTimeCreation";
                    dtTechnologyCopy.Columns.Add(dtColumnDateTimeCreationFordtTechnologyCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtTechnologyCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtTechnologyCopy.ColumnName = "DateTimeLastModification";
                    dtTechnologyCopy.Columns.Add(dtColumnDateTimeLastModificationFordtTechnologyCopy);

                    DataColumn dtColumnUserCreationIdFordtTechnologyCopy = new DataColumn();
                    dtColumnUserCreationIdFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtTechnologyCopy.ColumnName = "UserCreationId";
                    dtTechnologyCopy.Columns.Add(dtColumnUserCreationIdFordtTechnologyCopy);

                    DataColumn dtColumnUserLastModificationIdFordtTechnologyCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtTechnologyCopy.ColumnName = "UserLastModificationId";
                    dtTechnologyCopy.Columns.Add(dtColumnUserLastModificationIdFordtTechnologyCopy);

                    DataColumn dtColumnNameFordtTechnologyCopy = new DataColumn();
                    dtColumnNameFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnNameFordtTechnologyCopy.ColumnName = "Name";
                    dtTechnologyCopy.Columns.Add(dtColumnNameFordtTechnologyCopy);

                    DataColumn dtColumnDescriptionFordtTechnologyCopy = new DataColumn();
                    dtColumnDescriptionFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnDescriptionFordtTechnologyCopy.ColumnName = "Description";
                    dtTechnologyCopy.Columns.Add(dtColumnDescriptionFordtTechnologyCopy);

                    
                #endregion

                dtTechnology = new TechnologyModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtTechnology.Rows)
                {
                    dtTechnologyCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtTechnologyCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Technologying/Technology/Technology_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsTechnology = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtTechnologyCopy to avoid issue related to DateTime conversion
                    DataTable dtTechnologyCopy = new DataTable();
                    dtTechnologyCopy.TableName = "Technology";

                    #region Define columns for dtTechnologyCopy
                    DataColumn dtColumnTechnologyIdFordtTechnologyCopy = new DataColumn();
                    dtColumnTechnologyIdFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnTechnologyIdFordtTechnologyCopy.ColumnName = "TechnologyId";
                    dtTechnologyCopy.Columns.Add(dtColumnTechnologyIdFordtTechnologyCopy);

                    DataColumn dtColumnActiveFordtTechnologyCopy = new DataColumn();
                    dtColumnActiveFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnActiveFordtTechnologyCopy.ColumnName = "Active";
                    dtTechnologyCopy.Columns.Add(dtColumnActiveFordtTechnologyCopy);

                    DataColumn dtColumnDateTimeCreationFordtTechnologyCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtTechnologyCopy.ColumnName = "DateTimeCreation";
                    dtTechnologyCopy.Columns.Add(dtColumnDateTimeCreationFordtTechnologyCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtTechnologyCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtTechnologyCopy.ColumnName = "DateTimeLastModification";
                    dtTechnologyCopy.Columns.Add(dtColumnDateTimeLastModificationFordtTechnologyCopy);

                    DataColumn dtColumnUserCreationIdFordtTechnologyCopy = new DataColumn();
                    dtColumnUserCreationIdFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtTechnologyCopy.ColumnName = "UserCreationId";
                    dtTechnologyCopy.Columns.Add(dtColumnUserCreationIdFordtTechnologyCopy);

                    DataColumn dtColumnUserLastModificationIdFordtTechnologyCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtTechnologyCopy.ColumnName = "UserLastModificationId";
                    dtTechnologyCopy.Columns.Add(dtColumnUserLastModificationIdFordtTechnologyCopy);

                    DataColumn dtColumnNameFordtTechnologyCopy = new DataColumn();
                    dtColumnNameFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnNameFordtTechnologyCopy.ColumnName = "Name";
                    dtTechnologyCopy.Columns.Add(dtColumnNameFordtTechnologyCopy);

                    DataColumn dtColumnDescriptionFordtTechnologyCopy = new DataColumn();
                    dtColumnDescriptionFordtTechnologyCopy.DataType = typeof(string);
                    dtColumnDescriptionFordtTechnologyCopy.ColumnName = "Description";
                    dtTechnologyCopy.Columns.Add(dtColumnDescriptionFordtTechnologyCopy);

                    
                    #endregion

                    dsTechnology.Tables.Add(dtTechnologyCopy);

                    for (int i = 0; i < dsTechnology.Tables.Count; i++)
                    {
                        dtTechnologyCopy = new TechnologyModel().Select1ByTechnologyIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtTechnologyCopy.Rows)
                        {
                            dsTechnology.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsTechnology.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsTechnology.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Technologying/Technology/Technology_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<TechnologyModel> lstTechnologyModel = new List<TechnologyModel> { };

            if (ExportationType == "All")
            {
                lstTechnologyModel = new TechnologyModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    TechnologyModel TechnologyModel = new TechnologyModel().Select1ByTechnologyIdToModel(Convert.ToInt32(RowChecked));
                    lstTechnologyModel.Add(TechnologyModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Technologying/Technology/Technology_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstTechnologyModel);
            }

            return Now;
        }
        #endregion
    }
}