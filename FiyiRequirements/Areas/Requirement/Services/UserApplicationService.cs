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

//Last modification on: 27/12/2022 16:32:18

namespace FiyiRequirements.Areas.Requirement.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 27/12/2022 16:32:18
    /// </summary>
    public partial class UserApplicationService : UserApplicationProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public UserApplicationService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public UserApplicationModel Select1ByUserApplicationIdToModel(int UserApplicationId)
        {
            return new UserApplicationModel().Select1ByUserApplicationIdToModel(UserApplicationId);
        }

        public List<UserApplicationModel> SelectAllToList()
        {
            return new UserApplicationModel().SelectAllToList();
        }

        public userapplicationModelQuery SelectAllPagedToModel(userapplicationModelQuery userapplicationModelQuery)
        {
            return new UserApplicationModel().SelectAllPagedToModel(userapplicationModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(UserApplicationModel UserApplicationModel)
        {
            return new UserApplicationModel().Insert(UserApplicationModel);
        }

        public int UpdateByUserApplicationId(UserApplicationModel UserApplicationModel)
        {
            return new UserApplicationModel().UpdateByUserApplicationId(UserApplicationModel);
        }

        public int DeleteByUserApplicationId(int UserApplicationId)
        {
            return new UserApplicationModel().DeleteByUserApplicationId(UserApplicationId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                UserApplicationModel UserApplicationModel = new UserApplicationModel();
                UserApplicationModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    UserApplicationModel UserApplicationModel = new UserApplicationModel().Select1ByUserApplicationIdToModel(Convert.ToInt32(RowsChecked[i]));
                    UserApplicationModel.DeleteByUserApplicationId(UserApplicationModel.UserApplicationId);
                }
            }
        }

        public int CopyByUserApplicationId(int UserApplicationId)
        {
            UserApplicationModel UserApplicationModel = new UserApplicationModel().Select1ByUserApplicationIdToModel(UserApplicationId);
            int NewEnteredId = new UserApplicationModel().Insert(UserApplicationModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<UserApplicationModel> lstUserApplicationModel = new List<UserApplicationModel> { };
                lstUserApplicationModel = new UserApplicationModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstUserApplicationModel.Count];

                for (int i = 0; i < lstUserApplicationModel.Count; i++)
                {
                    NewEnteredIds[i] = lstUserApplicationModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    UserApplicationModel UserApplicationModel = new UserApplicationModel().Select1ByUserApplicationIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = UserApplicationModel.Insert();
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
            List<UserApplicationModel> lstUserApplicationModel = new List<UserApplicationModel> { };

            if (ExportationType == "All")
            {
                lstUserApplicationModel = new UserApplicationModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    UserApplicationModel UserApplicationModel = new UserApplicationModel().Select1ByUserApplicationIdToModel(Convert.ToInt32(RowChecked));
                    lstUserApplicationModel.Add(UserApplicationModel);
                }
            }

            foreach (UserApplicationModel row in lstUserApplicationModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of UserApplication</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserApplicationId&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">ApplicationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserId&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/Requirement/UserApplication/UserApplication_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtUserApplication = new DataTable();
                dtUserApplication.TableName = "UserApplication";

                //We define another DataTable dtUserApplicationCopy to avoid issue related to DateTime conversion
                DataTable dtUserApplicationCopy = new DataTable();
                dtUserApplicationCopy.TableName = "UserApplication";

                #region Define columns for dtUserApplicationCopy
                DataColumn dtColumnUserApplicationIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnUserApplicationIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnUserApplicationIdFordtUserApplicationCopy.ColumnName = "UserApplicationId";
                    dtUserApplicationCopy.Columns.Add(dtColumnUserApplicationIdFordtUserApplicationCopy);

                    DataColumn dtColumnActiveFordtUserApplicationCopy = new DataColumn();
                    dtColumnActiveFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnActiveFordtUserApplicationCopy.ColumnName = "Active";
                    dtUserApplicationCopy.Columns.Add(dtColumnActiveFordtUserApplicationCopy);

                    DataColumn dtColumnDateTimeCreationFordtUserApplicationCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtUserApplicationCopy.ColumnName = "DateTimeCreation";
                    dtUserApplicationCopy.Columns.Add(dtColumnDateTimeCreationFordtUserApplicationCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtUserApplicationCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtUserApplicationCopy.ColumnName = "DateTimeLastModification";
                    dtUserApplicationCopy.Columns.Add(dtColumnDateTimeLastModificationFordtUserApplicationCopy);

                    DataColumn dtColumnUserCreationIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnUserCreationIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtUserApplicationCopy.ColumnName = "UserCreationId";
                    dtUserApplicationCopy.Columns.Add(dtColumnUserCreationIdFordtUserApplicationCopy);

                    DataColumn dtColumnUserLastModificationIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtUserApplicationCopy.ColumnName = "UserLastModificationId";
                    dtUserApplicationCopy.Columns.Add(dtColumnUserLastModificationIdFordtUserApplicationCopy);

                    DataColumn dtColumnApplicationIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnApplicationIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnApplicationIdFordtUserApplicationCopy.ColumnName = "ApplicationId";
                    dtUserApplicationCopy.Columns.Add(dtColumnApplicationIdFordtUserApplicationCopy);

                    DataColumn dtColumnUserIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnUserIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnUserIdFordtUserApplicationCopy.ColumnName = "UserId";
                    dtUserApplicationCopy.Columns.Add(dtColumnUserIdFordtUserApplicationCopy);

                    
                #endregion

                dtUserApplication = new UserApplicationModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtUserApplication.Rows)
                {
                    dtUserApplicationCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtUserApplicationCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/UserApplicationing/UserApplication/UserApplication_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsUserApplication = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtUserApplicationCopy to avoid issue related to DateTime conversion
                    DataTable dtUserApplicationCopy = new DataTable();
                    dtUserApplicationCopy.TableName = "UserApplication";

                    #region Define columns for dtUserApplicationCopy
                    DataColumn dtColumnUserApplicationIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnUserApplicationIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnUserApplicationIdFordtUserApplicationCopy.ColumnName = "UserApplicationId";
                    dtUserApplicationCopy.Columns.Add(dtColumnUserApplicationIdFordtUserApplicationCopy);

                    DataColumn dtColumnActiveFordtUserApplicationCopy = new DataColumn();
                    dtColumnActiveFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnActiveFordtUserApplicationCopy.ColumnName = "Active";
                    dtUserApplicationCopy.Columns.Add(dtColumnActiveFordtUserApplicationCopy);

                    DataColumn dtColumnDateTimeCreationFordtUserApplicationCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtUserApplicationCopy.ColumnName = "DateTimeCreation";
                    dtUserApplicationCopy.Columns.Add(dtColumnDateTimeCreationFordtUserApplicationCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtUserApplicationCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtUserApplicationCopy.ColumnName = "DateTimeLastModification";
                    dtUserApplicationCopy.Columns.Add(dtColumnDateTimeLastModificationFordtUserApplicationCopy);

                    DataColumn dtColumnUserCreationIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnUserCreationIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtUserApplicationCopy.ColumnName = "UserCreationId";
                    dtUserApplicationCopy.Columns.Add(dtColumnUserCreationIdFordtUserApplicationCopy);

                    DataColumn dtColumnUserLastModificationIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtUserApplicationCopy.ColumnName = "UserLastModificationId";
                    dtUserApplicationCopy.Columns.Add(dtColumnUserLastModificationIdFordtUserApplicationCopy);

                    DataColumn dtColumnApplicationIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnApplicationIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnApplicationIdFordtUserApplicationCopy.ColumnName = "ApplicationId";
                    dtUserApplicationCopy.Columns.Add(dtColumnApplicationIdFordtUserApplicationCopy);

                    DataColumn dtColumnUserIdFordtUserApplicationCopy = new DataColumn();
                    dtColumnUserIdFordtUserApplicationCopy.DataType = typeof(string);
                    dtColumnUserIdFordtUserApplicationCopy.ColumnName = "UserId";
                    dtUserApplicationCopy.Columns.Add(dtColumnUserIdFordtUserApplicationCopy);

                    
                    #endregion

                    dsUserApplication.Tables.Add(dtUserApplicationCopy);

                    for (int i = 0; i < dsUserApplication.Tables.Count; i++)
                    {
                        dtUserApplicationCopy = new UserApplicationModel().Select1ByUserApplicationIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtUserApplicationCopy.Rows)
                        {
                            dsUserApplication.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsUserApplication.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsUserApplication.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/UserApplicationing/UserApplication/UserApplication_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<UserApplicationModel> lstUserApplicationModel = new List<UserApplicationModel> { };

            if (ExportationType == "All")
            {
                lstUserApplicationModel = new UserApplicationModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    UserApplicationModel UserApplicationModel = new UserApplicationModel().Select1ByUserApplicationIdToModel(Convert.ToInt32(RowChecked));
                    lstUserApplicationModel.Add(UserApplicationModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/UserApplicationing/UserApplication/UserApplication_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstUserApplicationModel);
            }

            return Now;
        }
        #endregion
    }
}