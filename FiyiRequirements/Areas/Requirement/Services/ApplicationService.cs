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

//Last modification on: 25/12/2022 12:07:25

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 25/12/2022 12:07:25
    /// </summary>
    public partial class ApplicationService : ApplicationProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ApplicationService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public ApplicationModel Select1ByApplicationIdToModel(int ApplicationId)
        {
            return new ApplicationModel().Select1ByApplicationIdToModel(ApplicationId);
        }

        public List<ApplicationModel> SelectAllToList()
        {
            return new ApplicationModel().SelectAllToList();
        }

        public applicationModelQuery SelectAllPagedToModel(applicationModelQuery applicationModelQuery)
        {
            return new ApplicationModel().SelectAllPagedToModel(applicationModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(ApplicationModel ApplicationModel)
        {
            return new ApplicationModel().Insert(ApplicationModel);
        }

        public int UpdateByApplicationId(ApplicationModel ApplicationModel)
        {
            return new ApplicationModel().UpdateByApplicationId(ApplicationModel);
        }

        public int DeleteByApplicationId(int ApplicationId)
        {
            return new ApplicationModel().DeleteByApplicationId(ApplicationId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                ApplicationModel ApplicationModel = new ApplicationModel();
                ApplicationModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ApplicationModel ApplicationModel = new ApplicationModel().Select1ByApplicationIdToModel(Convert.ToInt32(RowsChecked[i]));
                    ApplicationModel.DeleteByApplicationId(ApplicationModel.ApplicationId);
                }
            }
        }

        public int CopyByApplicationId(int ApplicationId)
        {
            ApplicationModel ApplicationModel = new ApplicationModel().Select1ByApplicationIdToModel(ApplicationId);
            int NewEnteredId = new ApplicationModel().Insert(ApplicationModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<ApplicationModel> lstApplicationModel = new List<ApplicationModel> { };
                lstApplicationModel = new ApplicationModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstApplicationModel.Count];

                for (int i = 0; i < lstApplicationModel.Count; i++)
                {
                    NewEnteredIds[i] = lstApplicationModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    ApplicationModel ApplicationModel = new ApplicationModel().Select1ByApplicationIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = ApplicationModel.Insert();
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
            List<ApplicationModel> lstApplicationModel = new List<ApplicationModel> { };

            if (ExportationType == "All")
            {
                lstApplicationModel = new ApplicationModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ApplicationModel ApplicationModel = new ApplicationModel().Select1ByApplicationIdToModel(Convert.ToInt32(RowChecked));
                    lstApplicationModel.Add(ApplicationModel);
                }
            }

            foreach (ApplicationModel row in lstApplicationModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Application</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ApplicationId&nbsp;&nbsp;&nbsp;</span>
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
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TechnologyId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/Application/Application_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtApplication = new DataTable();
                dtApplication.TableName = "Application";

                //We define another DataTable dtApplicationCopy to avoid issue related to DateTime conversion
                DataTable dtApplicationCopy = new DataTable();
                dtApplicationCopy.TableName = "Application";

                #region Define columns for dtApplicationCopy
                DataColumn dtColumnApplicationIdFordtApplicationCopy = new DataColumn();
                    dtColumnApplicationIdFordtApplicationCopy.DataType = typeof(string);
                    dtColumnApplicationIdFordtApplicationCopy.ColumnName = "ApplicationId";
                    dtApplicationCopy.Columns.Add(dtColumnApplicationIdFordtApplicationCopy);

                    DataColumn dtColumnActiveFordtApplicationCopy = new DataColumn();
                    dtColumnActiveFordtApplicationCopy.DataType = typeof(string);
                    dtColumnActiveFordtApplicationCopy.ColumnName = "Active";
                    dtApplicationCopy.Columns.Add(dtColumnActiveFordtApplicationCopy);

                    DataColumn dtColumnDateTimeCreationFordtApplicationCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtApplicationCopy.ColumnName = "DateTimeCreation";
                    dtApplicationCopy.Columns.Add(dtColumnDateTimeCreationFordtApplicationCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtApplicationCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtApplicationCopy.ColumnName = "DateTimeLastModification";
                    dtApplicationCopy.Columns.Add(dtColumnDateTimeLastModificationFordtApplicationCopy);

                    DataColumn dtColumnUserCreationIdFordtApplicationCopy = new DataColumn();
                    dtColumnUserCreationIdFordtApplicationCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtApplicationCopy.ColumnName = "UserCreationId";
                    dtApplicationCopy.Columns.Add(dtColumnUserCreationIdFordtApplicationCopy);

                    DataColumn dtColumnUserLastModificationIdFordtApplicationCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtApplicationCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtApplicationCopy.ColumnName = "UserLastModificationId";
                    dtApplicationCopy.Columns.Add(dtColumnUserLastModificationIdFordtApplicationCopy);

                    DataColumn dtColumnNameFordtApplicationCopy = new DataColumn();
                    dtColumnNameFordtApplicationCopy.DataType = typeof(string);
                    dtColumnNameFordtApplicationCopy.ColumnName = "Name";
                    dtApplicationCopy.Columns.Add(dtColumnNameFordtApplicationCopy);

                    DataColumn dtColumnDescriptionFordtApplicationCopy = new DataColumn();
                    dtColumnDescriptionFordtApplicationCopy.DataType = typeof(string);
                    dtColumnDescriptionFordtApplicationCopy.ColumnName = "Description";
                    dtApplicationCopy.Columns.Add(dtColumnDescriptionFordtApplicationCopy);

                    DataColumn dtColumnTechnologyIdFordtApplicationCopy = new DataColumn();
                    dtColumnTechnologyIdFordtApplicationCopy.DataType = typeof(string);
                    dtColumnTechnologyIdFordtApplicationCopy.ColumnName = "TechnologyId";
                    dtApplicationCopy.Columns.Add(dtColumnTechnologyIdFordtApplicationCopy);

                    
                #endregion

                dtApplication = new ApplicationModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtApplication.Rows)
                {
                    dtApplicationCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtApplicationCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Applicationing/Application/Application_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsApplication = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtApplicationCopy to avoid issue related to DateTime conversion
                    DataTable dtApplicationCopy = new DataTable();
                    dtApplicationCopy.TableName = "Application";

                    #region Define columns for dtApplicationCopy
                    DataColumn dtColumnApplicationIdFordtApplicationCopy = new DataColumn();
                    dtColumnApplicationIdFordtApplicationCopy.DataType = typeof(string);
                    dtColumnApplicationIdFordtApplicationCopy.ColumnName = "ApplicationId";
                    dtApplicationCopy.Columns.Add(dtColumnApplicationIdFordtApplicationCopy);

                    DataColumn dtColumnActiveFordtApplicationCopy = new DataColumn();
                    dtColumnActiveFordtApplicationCopy.DataType = typeof(string);
                    dtColumnActiveFordtApplicationCopy.ColumnName = "Active";
                    dtApplicationCopy.Columns.Add(dtColumnActiveFordtApplicationCopy);

                    DataColumn dtColumnDateTimeCreationFordtApplicationCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtApplicationCopy.ColumnName = "DateTimeCreation";
                    dtApplicationCopy.Columns.Add(dtColumnDateTimeCreationFordtApplicationCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtApplicationCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtApplicationCopy.ColumnName = "DateTimeLastModification";
                    dtApplicationCopy.Columns.Add(dtColumnDateTimeLastModificationFordtApplicationCopy);

                    DataColumn dtColumnUserCreationIdFordtApplicationCopy = new DataColumn();
                    dtColumnUserCreationIdFordtApplicationCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtApplicationCopy.ColumnName = "UserCreationId";
                    dtApplicationCopy.Columns.Add(dtColumnUserCreationIdFordtApplicationCopy);

                    DataColumn dtColumnUserLastModificationIdFordtApplicationCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtApplicationCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtApplicationCopy.ColumnName = "UserLastModificationId";
                    dtApplicationCopy.Columns.Add(dtColumnUserLastModificationIdFordtApplicationCopy);

                    DataColumn dtColumnNameFordtApplicationCopy = new DataColumn();
                    dtColumnNameFordtApplicationCopy.DataType = typeof(string);
                    dtColumnNameFordtApplicationCopy.ColumnName = "Name";
                    dtApplicationCopy.Columns.Add(dtColumnNameFordtApplicationCopy);

                    DataColumn dtColumnDescriptionFordtApplicationCopy = new DataColumn();
                    dtColumnDescriptionFordtApplicationCopy.DataType = typeof(string);
                    dtColumnDescriptionFordtApplicationCopy.ColumnName = "Description";
                    dtApplicationCopy.Columns.Add(dtColumnDescriptionFordtApplicationCopy);

                    DataColumn dtColumnTechnologyIdFordtApplicationCopy = new DataColumn();
                    dtColumnTechnologyIdFordtApplicationCopy.DataType = typeof(string);
                    dtColumnTechnologyIdFordtApplicationCopy.ColumnName = "TechnologyId";
                    dtApplicationCopy.Columns.Add(dtColumnTechnologyIdFordtApplicationCopy);

                    
                    #endregion

                    dsApplication.Tables.Add(dtApplicationCopy);

                    for (int i = 0; i < dsApplication.Tables.Count; i++)
                    {
                        dtApplicationCopy = new ApplicationModel().Select1ByApplicationIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtApplicationCopy.Rows)
                        {
                            dsApplication.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsApplication.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsApplication.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Applicationing/Application/Application_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<ApplicationModel> lstApplicationModel = new List<ApplicationModel> { };

            if (ExportationType == "All")
            {
                lstApplicationModel = new ApplicationModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    ApplicationModel ApplicationModel = new ApplicationModel().Select1ByApplicationIdToModel(Convert.ToInt32(RowChecked));
                    lstApplicationModel.Add(ApplicationModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Applicationing/Application/Application_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstApplicationModel);
            }

            return Now;
        }
        #endregion
    }
}