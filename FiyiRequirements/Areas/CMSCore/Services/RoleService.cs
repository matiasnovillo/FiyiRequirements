using ClosedXML.Excel;
using CsvHelper;
using IronPdf;
using Microsoft.AspNetCore.Http;
using FiyiRequirements.Areas.CMSCore.Models;
using FiyiRequirements.Areas.CMSCore.Protocols;
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

//Last modification on: 20/12/2022 20:47:32

namespace FiyiRequirements.Areas.CMSCore.Services
{
    /// <summary>
    /// Stack:             4<br/>
    /// Name:              C# Service. <br/>
    /// Function:          Allow you to separate data contract stored in C# model from business with your clients. <br/>
    /// Also, allow dependency injection inside controllers/web apis<br/>
    /// Last modification: 20/12/2022 20:47:32
    /// </summary>
    public partial class RoleService : RoleProtocol
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public RoleService(IHttpContextAccessor IHttpContextAccessor)
        {
            _IHttpContextAccessor = IHttpContextAccessor;
        }

        #region Queries
        public RoleModel Select1ByRoleIdToModel(int RoleId)
        {
            return new RoleModel().Select1ByRoleIdToModel(RoleId);
        }

        public List<RoleModel> SelectAllToList()
        {
            return new RoleModel().SelectAllToList();
        }

        public roleModelQuery SelectAllPagedToModel(roleModelQuery roleModelQuery)
        {
            return new RoleModel().SelectAllPagedToModel(roleModelQuery);
        } 
        #endregion

        #region Non-Queries
        public int Insert(RoleModel RoleModel)
        {
            return new RoleModel().Insert(RoleModel);
        }

        public int UpdateByRoleId(RoleModel RoleModel)
        {
            return new RoleModel().UpdateByRoleId(RoleModel);
        }

        public int DeleteByRoleId(int RoleId)
        {
            return new RoleModel().DeleteByRoleId(RoleId);
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            if (DeleteType == "All")
            {
                RoleModel RoleModel = new RoleModel();
                RoleModel.DeleteAll();
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RoleModel RoleModel = new RoleModel().Select1ByRoleIdToModel(Convert.ToInt32(RowsChecked[i]));
                    RoleModel.DeleteByRoleId(RoleModel.RoleId);
                }
            }
        }

        public int CopyByRoleId(int RoleId)
        {
            RoleModel RoleModel = new RoleModel().Select1ByRoleIdToModel(RoleId);
            int NewEnteredId = new RoleModel().Insert(RoleModel);

            return NewEnteredId;
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            if (CopyType == "All")
            {
                List<RoleModel> lstRoleModel = new List<RoleModel> { };
                lstRoleModel = new RoleModel().SelectAllToList();

                int[] NewEnteredIds = new int[lstRoleModel.Count];

                for (int i = 0; i < lstRoleModel.Count; i++)
                {
                    NewEnteredIds[i] = lstRoleModel[i].Insert();
                }

                return NewEnteredIds;
            }
            else
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');
                int[] NewEnteredIds = new int[RowsChecked.Length];

                for (int i = 0; i < RowsChecked.Length; i++)
                {
                    RoleModel RoleModel = new RoleModel().Select1ByRoleIdToModel(Convert.ToInt32(RowsChecked[i]));
                    NewEnteredIds[i] = RoleModel.Insert();
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
            List<RoleModel> lstRoleModel = new List<RoleModel> { };

            if (ExportationType == "All")
            {
                lstRoleModel = new RoleModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RoleModel RoleModel = new RoleModel().Select1ByRoleIdToModel(Convert.ToInt32(RowChecked));
                    lstRoleModel.Add(RoleModel);
                }
            }

            foreach (RoleModel row in lstRoleModel)
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of Role</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RoleId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Name&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Active&nbsp;&nbsp;&nbsp;</span>
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
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeCreation&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeLastModification&nbsp;&nbsp;&nbsp;</span>
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
").SaveAs($@"wwwroot/PDFFiles/CMSCore/Role/Role_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.pdf");

            return Now;
        }

        public DateTime ExportAsExcel(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;

            using var Book = new XLWorkbook();

            if (ExportationType == "All")
            {
                DataTable dtRole = new DataTable();
                dtRole.TableName = "Role";

                //We define another DataTable dtRoleCopy to avoid issue related to DateTime conversion
                DataTable dtRoleCopy = new DataTable();
                dtRoleCopy.TableName = "Role";

                #region Define columns for dtRoleCopy
                DataColumn dtColumnRoleIdFordtRoleCopy = new DataColumn();
                    dtColumnRoleIdFordtRoleCopy.DataType = typeof(string);
                    dtColumnRoleIdFordtRoleCopy.ColumnName = "RoleId";
                    dtRoleCopy.Columns.Add(dtColumnRoleIdFordtRoleCopy);

                    DataColumn dtColumnNameFordtRoleCopy = new DataColumn();
                    dtColumnNameFordtRoleCopy.DataType = typeof(string);
                    dtColumnNameFordtRoleCopy.ColumnName = "Name";
                    dtRoleCopy.Columns.Add(dtColumnNameFordtRoleCopy);

                    DataColumn dtColumnActiveFordtRoleCopy = new DataColumn();
                    dtColumnActiveFordtRoleCopy.DataType = typeof(string);
                    dtColumnActiveFordtRoleCopy.ColumnName = "Active";
                    dtRoleCopy.Columns.Add(dtColumnActiveFordtRoleCopy);

                    DataColumn dtColumnUserCreationIdFordtRoleCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRoleCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRoleCopy.ColumnName = "UserCreationId";
                    dtRoleCopy.Columns.Add(dtColumnUserCreationIdFordtRoleCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRoleCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRoleCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRoleCopy.ColumnName = "UserLastModificationId";
                    dtRoleCopy.Columns.Add(dtColumnUserLastModificationIdFordtRoleCopy);

                    DataColumn dtColumnDateTimeCreationFordtRoleCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRoleCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRoleCopy.ColumnName = "DateTimeCreation";
                    dtRoleCopy.Columns.Add(dtColumnDateTimeCreationFordtRoleCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRoleCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRoleCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRoleCopy.ColumnName = "DateTimeLastModification";
                    dtRoleCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRoleCopy);

                    
                #endregion

                dtRole = new RoleModel().SelectAllToDataTable();

                foreach (DataRow DataRow in dtRole.Rows)
                {
                    dtRoleCopy.Rows.Add(DataRow.ItemArray);
                }

                var Sheet = Book.Worksheets.Add(dtRoleCopy);

                Sheet.ColumnsUsed().AdjustToContents();

                Book.SaveAs($@"wwwroot/ExcelFiles/Roleing/Role/Role_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                DataSet dsRole = new DataSet();

                foreach (string RowChecked in RowsChecked)
                {
                    //We define another DataTable dtRoleCopy to avoid issue related to DateTime conversion
                    DataTable dtRoleCopy = new DataTable();
                    dtRoleCopy.TableName = "Role";

                    #region Define columns for dtRoleCopy
                    DataColumn dtColumnRoleIdFordtRoleCopy = new DataColumn();
                    dtColumnRoleIdFordtRoleCopy.DataType = typeof(string);
                    dtColumnRoleIdFordtRoleCopy.ColumnName = "RoleId";
                    dtRoleCopy.Columns.Add(dtColumnRoleIdFordtRoleCopy);

                    DataColumn dtColumnNameFordtRoleCopy = new DataColumn();
                    dtColumnNameFordtRoleCopy.DataType = typeof(string);
                    dtColumnNameFordtRoleCopy.ColumnName = "Name";
                    dtRoleCopy.Columns.Add(dtColumnNameFordtRoleCopy);

                    DataColumn dtColumnActiveFordtRoleCopy = new DataColumn();
                    dtColumnActiveFordtRoleCopy.DataType = typeof(string);
                    dtColumnActiveFordtRoleCopy.ColumnName = "Active";
                    dtRoleCopy.Columns.Add(dtColumnActiveFordtRoleCopy);

                    DataColumn dtColumnUserCreationIdFordtRoleCopy = new DataColumn();
                    dtColumnUserCreationIdFordtRoleCopy.DataType = typeof(string);
                    dtColumnUserCreationIdFordtRoleCopy.ColumnName = "UserCreationId";
                    dtRoleCopy.Columns.Add(dtColumnUserCreationIdFordtRoleCopy);

                    DataColumn dtColumnUserLastModificationIdFordtRoleCopy = new DataColumn();
                    dtColumnUserLastModificationIdFordtRoleCopy.DataType = typeof(string);
                    dtColumnUserLastModificationIdFordtRoleCopy.ColumnName = "UserLastModificationId";
                    dtRoleCopy.Columns.Add(dtColumnUserLastModificationIdFordtRoleCopy);

                    DataColumn dtColumnDateTimeCreationFordtRoleCopy = new DataColumn();
                    dtColumnDateTimeCreationFordtRoleCopy.DataType = typeof(string);
                    dtColumnDateTimeCreationFordtRoleCopy.ColumnName = "DateTimeCreation";
                    dtRoleCopy.Columns.Add(dtColumnDateTimeCreationFordtRoleCopy);

                    DataColumn dtColumnDateTimeLastModificationFordtRoleCopy = new DataColumn();
                    dtColumnDateTimeLastModificationFordtRoleCopy.DataType = typeof(string);
                    dtColumnDateTimeLastModificationFordtRoleCopy.ColumnName = "DateTimeLastModification";
                    dtRoleCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRoleCopy);

                    
                    #endregion

                    dsRole.Tables.Add(dtRoleCopy);

                    for (int i = 0; i < dsRole.Tables.Count; i++)
                    {
                        dtRoleCopy = new RoleModel().Select1ByRoleIdToDataTable(Convert.ToInt32(RowChecked));

                        foreach (DataRow DataRow in dtRoleCopy.Rows)
                        {
                            dsRole.Tables[0].Rows.Add(DataRow.ItemArray);
                        }
                    }
                    
                }

                for (int i = 0; i < dsRole.Tables.Count; i++)
                {
                    var Sheet = Book.Worksheets.Add(dsRole.Tables[i]);
                    Sheet.ColumnsUsed().AdjustToContents();
                }

                Book.SaveAs($@"wwwroot/ExcelFiles/Roleing/Role/Role_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.xlsx");
            }

            return Now;
        }

        public DateTime ExportAsCSV(Ajax Ajax, string ExportationType)
        {
            DateTime Now = DateTime.Now;
            List<RoleModel> lstRoleModel = new List<RoleModel> { };

            if (ExportationType == "All")
            {
                lstRoleModel = new RoleModel().SelectAllToList();

            }
            else if (ExportationType == "JustChecked")
            {
                string[] RowsChecked = Ajax.AjaxForString.Split(',');

                foreach (string RowChecked in RowsChecked)
                {
                    RoleModel RoleModel = new RoleModel().Select1ByRoleIdToModel(Convert.ToInt32(RowChecked));
                    lstRoleModel.Add(RoleModel);
                }
            }

            using (var Writer = new StreamWriter($@"wwwroot/CSVFiles/Roleing/Role/Role_{Now.ToString("yyyy_MM_dd_HH_mm_ss_fff")}.csv"))
            using (var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture))
            {
                CsvWriter.WriteRecords(lstRoleModel);
            }

            return Now;
        }
        #endregion
    }
}