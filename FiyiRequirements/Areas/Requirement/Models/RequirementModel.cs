using Dapper;
using FiyiRequirements.Areas.Requirement.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

namespace FiyiRequirements.Areas.Requirement.Models
{
    /// <summary>
    /// Stack:             3 <br/>
    /// Name:              C# Model with stored procedure calls saved on database. <br/>
    /// Function:          Allow you to manipulate information from database using stored procedures.
    ///                    Also, let you make other related actions with the model in question or
    ///                    make temporal copies with random data. <br/>
    /// Fields:            11 <br/> 
    /// Sub-models:      3 models <br/>
    /// Last modification: 21/02/2023 18:24:38
    /// </summary>
    [Serializable]
    public partial class RequirementModel
    {
        [NotMapped]
        private string _ConnectionString = ConnectionStrings.ConnectionStrings.Development();

        #region Fields
        [Library.ModelAttributeValidator.Key("RequirementId")]
        public int RequirementId { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        public bool Active { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.DateTime("DateTimeCreation", false, "1753-01-01T00:00", "9998-12-30T23:59")]
        public DateTime DateTimeCreation { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.DateTime("DateTimeLastModification", false, "1753-01-01T00:00", "9998-12-30T23:59")]
        public DateTime DateTimeLastModification { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.Key("UserCreationId")]
        public int UserCreationId { get; set; }

        ///<summary>
        /// For auditing purposes
        ///</summary>
        [Library.ModelAttributeValidator.Key("UserLastModificationId")]
        public int UserLastModificationId { get; set; }

        [Library.ModelAttributeValidator.String("Title", false, 1, 100, "")]
        public string Title { get; set; }

        public string Body { get; set; }

        [Library.ModelAttributeValidator.Key("RequirementStateId")]
        public int RequirementStateId { get; set; }

        [Library.ModelAttributeValidator.Key("RequirementPriorityId")]
        public int RequirementPriorityId { get; set; }

        [Library.ModelAttributeValidator.Key("UserEmployeeId")]
        public int UserEmployeeId { get; set; }

        public string UserCreationIdFantasyName { get; set; }

        public string UserLastModificationIdFantasyName { get; set; }

        public string RequirementStateIdName { get; set; }

        public string RequirementPriorityIdName { get; set; }

        public string UserEmployeeIdFantasyName { get; set; }
        #endregion

        #region Sub-lists
        public virtual List<RequirementFileModel> lstRequirementFileModel { get; set; } //Foreign Key name: RequirementId 
		public virtual List<RequirementNoteModel> lstRequirementNoteModel { get; set; } //Foreign Key name: RequirementId 
		public virtual List<RequirementChangehistoryModel> lstRequirementChangehistoryModel { get; set; } //Foreign Key name: RequirementId 
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field RequirementId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       11 <br/> 
        /// Dependencies: 3 models depend on this model <br/>
        /// </summary>
        public RequirementModel()
        {
            try 
            {
                RequirementId = 0;

                //Initialize sub-lists
                lstRequirementFileModel = new List<RequirementFileModel>();
                lstRequirementNoteModel = new List<RequirementNoteModel>();
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using RequirementId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       11 <br/> 
        /// Dependencies: 3 models depend on this model <br/>
        /// </summary>
        public RequirementModel(int RequirementId)
        {
            try
            {
                List<RequirementModel> lstRequirementModel = new List<RequirementModel>();

                //Initialize sub-lists
                lstRequirementFileModel = new List<RequirementFileModel>();
                lstRequirementNoteModel = new List<RequirementNoteModel>();
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                
                
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<RequirementModel>
                    lstRequirementModel = (List<RequirementModel>)sqlConnection.Query<RequirementModel>("[dbo].[Requirement.Requirement.Select1ByRequirementId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstRequirementModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[Requirement.Requirement.Select1ByRequirementId] returned more than one register/row");
                }
        
                foreach (RequirementModel requirement in lstRequirementModel)
                {
                    this.RequirementId = requirement.RequirementId;
					this.Active = requirement.Active;
					this.DateTimeCreation = requirement.DateTimeCreation;
					this.DateTimeLastModification = requirement.DateTimeLastModification;
					this.UserCreationId = requirement.UserCreationId;
					this.UserLastModificationId = requirement.UserLastModificationId;
					this.Title = requirement.Title;
					this.Body = requirement.Body;
					this.RequirementStateId = requirement.RequirementStateId;
					this.RequirementPriorityId = requirement.RequirementPriorityId;
					this.UserEmployeeId = requirement.UserEmployeeId;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       11 <br/> 
        /// Dependencies: 3 models depend on this model <br/>
        /// </summary>
        public RequirementModel(int RequirementId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Title, string Body, int RequirementStateId, int RequirementPriorityId, int UserEmployeeId)
        {
            try
            {
                //Initialize sub-lists
                lstRequirementFileModel = new List<RequirementFileModel>();
                lstRequirementNoteModel = new List<RequirementNoteModel>();
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                

                this.RequirementId = RequirementId;
				this.Active = Active;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.Title = Title;
				this.Body = Body;
				this.RequirementStateId = RequirementStateId;
				this.RequirementPriorityId = RequirementPriorityId;
				this.UserEmployeeId = UserEmployeeId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), requirement, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       11 <br/> 
        /// Dependencies: 3 models depend on this model <br/>
        /// </summary>
        public RequirementModel(RequirementModel requirement)
        {
            try
            {
                //Initialize sub-lists
                lstRequirementFileModel = new List<RequirementFileModel>();
                lstRequirementNoteModel = new List<RequirementNoteModel>();
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                

                RequirementId = requirement.RequirementId;
				Active = requirement.Active;
				DateTimeCreation = requirement.DateTimeCreation;
				DateTimeLastModification = requirement.DateTimeLastModification;
				UserCreationId = requirement.UserCreationId;
				UserLastModificationId = requirement.UserLastModificationId;
				Title = requirement.Title;
				Body = requirement.Body;
				RequirementStateId = requirement.RequirementStateId;
				RequirementPriorityId = requirement.RequirementPriorityId;
				UserEmployeeId = requirement.UserEmployeeId;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside Requirement</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.Count]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }

                int RowsCounter = Convert.ToInt32(DataTable.Rows[0][0].ToString());

                return RowsCounter;
            }
            catch (Exception ex) { throw ex; }
        }

        #region Queries to DataTable
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        public DataTable Select1ByRequirementIdToDataTable(int RequirementId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.Select1ByRequirementId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[Requirement.Requirement.Select1ByRequirementId] returned more than one register/row"); }

                return DataTable;
            }
            catch (Exception ex) { throw ex; }
        }

        public DataTable SelectAllToDataTable()
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                return DataTable;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Queries to Models
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        public RequirementModel Select1ByRequirementIdToModel(int RequirementId)
        {
            try
            {
                RequirementModel RequirementModel = new RequirementModel();
                List<RequirementModel> lstRequirementModel = new List<RequirementModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstRequirementModel = (List<RequirementModel>)sqlConnection.Query<RequirementModel>("[dbo].[Requirement.Requirement.Select1ByRequirementId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstRequirementModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[Requirement.Requirement.Select1ByRequirementId] returned more than one register/row"); }

                foreach (RequirementModel requirement in lstRequirementModel)
                {
                    RequirementModel.RequirementId = requirement.RequirementId;
					RequirementModel.Active = requirement.Active;
					RequirementModel.DateTimeCreation = requirement.DateTimeCreation;
					RequirementModel.DateTimeLastModification = requirement.DateTimeLastModification;
					RequirementModel.UserCreationId = requirement.UserCreationId;
					RequirementModel.UserLastModificationId = requirement.UserLastModificationId;
					RequirementModel.Title = requirement.Title;
					RequirementModel.Body = requirement.Body;
					RequirementModel.RequirementStateId = requirement.RequirementStateId;
					RequirementModel.RequirementPriorityId = requirement.RequirementPriorityId;
					RequirementModel.UserEmployeeId = requirement.UserEmployeeId;
                }

                return RequirementModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<RequirementModel> SelectAllToList()
        {
            try
            {
                List<RequirementModel> lstRequirementModel = new List<RequirementModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstRequirementModel = (List<RequirementModel>)sqlConnection.Query<RequirementModel>("[dbo].[Requirement.Requirement.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstRequirementModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public requirementSelectAllPaged SelectAllPagedToModel(requirementSelectAllPaged requirementSelectAllPaged, int UserId, int RoleId)
        {
            try
            {
                requirementSelectAllPaged.lstRequirementModel = new List<RequirementModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", requirementSelectAllPaged.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", requirementSelectAllPaged.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", requirementSelectAllPaged.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", requirementSelectAllPaged.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", requirementSelectAllPaged.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", requirementSelectAllPaged.TotalRows, DbType.Int32, ParameterDirection.Output);

                dp.Add("UserId", UserId, DbType.Int32, ParameterDirection.Input);
                dp.Add("RoleId", RoleId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    requirementSelectAllPaged.lstRequirementModel = (List<RequirementModel>)sqlConnection.Query<RequirementModel>("[dbo].[Requirement.Requirement.SelectAllPagedCustom]", dp, commandType: CommandType.StoredProcedure);
                    requirementSelectAllPaged.TotalRows = dp.Get<int>("TotalRows");
                }

                requirementSelectAllPaged.TotalPages = Library.Math.Divide(requirementSelectAllPaged.TotalRows, requirementSelectAllPaged.RowsPerPage, Library.Math.RoundType.RoundUp);

                //Loop through lists and sublists
                for (int i = 0; i < requirementSelectAllPaged.lstRequirementModel.Count; i++)
                {
                    DynamicParameters dpForRequirementFileModel = new DynamicParameters();
                    dpForRequirementFileModel.Add("RequirementId", requirementSelectAllPaged.lstRequirementModel[i].RequirementId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<RequirementFileModel> lstRequirementFileModel = new List<RequirementFileModel>();
                        lstRequirementFileModel = (List<RequirementFileModel>)sqlConnection.Query<RequirementFileModel>("[dbo].[Requirement.RequirementFile.SelectAllByRequirementIdCustom]", dpForRequirementFileModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var RequirementFileModel in lstRequirementFileModel)
                        {
                            requirementSelectAllPaged.lstRequirementModel[i].lstRequirementFileModel.Add(RequirementFileModel);
                        }
                    }
                }
                
                //Loop through lists and sublists
                for (int i = 0; i < requirementSelectAllPaged.lstRequirementModel.Count; i++)
                {
                    DynamicParameters dpForRequirementNoteModel = new DynamicParameters();
                    dpForRequirementNoteModel.Add("RequirementId", requirementSelectAllPaged.lstRequirementModel[i].RequirementId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<RequirementNoteModel> lstRequirementNoteModel = new List<RequirementNoteModel>();
                        lstRequirementNoteModel = (List<RequirementNoteModel>)sqlConnection.Query<RequirementNoteModel>("[dbo].[Requirement.RequirementNote.SelectAllByRequirementIdCustom]", dpForRequirementNoteModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var RequirementNoteModel in lstRequirementNoteModel)
                        {
                            requirementSelectAllPaged.lstRequirementModel[i].lstRequirementNoteModel.Add(RequirementNoteModel);
                        }
                    }
                }
                
                //Loop through lists and sublists
                for (int i = 0; i < requirementSelectAllPaged.lstRequirementModel.Count; i++)
                {
                    DynamicParameters dpForRequirementChangehistoryModel = new DynamicParameters();
                    dpForRequirementChangehistoryModel.Add("RequirementId", requirementSelectAllPaged.lstRequirementModel[i].RequirementId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                        lstRequirementChangehistoryModel = (List<RequirementChangehistoryModel>)sqlConnection.Query<RequirementChangehistoryModel>("[dbo].[Requirement.RequirementChangehistory.SelectAllByRequirementIdCustom]", dpForRequirementChangehistoryModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var RequirementChangehistoryModel in lstRequirementChangehistoryModel)
                        {
                            requirementSelectAllPaged.lstRequirementModel[i].lstRequirementChangehistoryModel.Add(RequirementChangehistoryModel);
                        }
                    }
                }
                
                

                return requirementSelectAllPaged;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Requirement table</returns>
        public int Insert()
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
                
                dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserEmployeeId", UserEmployeeId, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>The ID of the last registry inserted in Requirement table</returns>
        public int Insert(RequirementModel requirement)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", requirement.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", requirement.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", requirement.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", requirement.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", requirement.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", requirement.Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", requirement.Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", requirement.RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", requirement.RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserEmployeeId", requirement.UserEmployeeId, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>The ID of the last registry inserted in Requirement table</returns>
        public int Insert(bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Title, string Body, int RequirementStateId, int RequirementPriorityId, int UserEmployeeId)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserEmployeeId", UserEmployeeId, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.Insert]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    NewEnteredId = dp.Get<int>("NewEnteredId");
                }
                                
                if (NewEnteredId == 0) { throw new Exception("NewEnteredId with no value"); }

                return NewEnteredId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Requirement table</returns>
        public int UpdateByRequirementId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserEmployeeId", UserEmployeeId, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.UpdateByRequirementId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Requirement table</returns>
        public int UpdateByRequirementId(RequirementModel requirement)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementId", requirement.RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", requirement.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", requirement.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", requirement.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", requirement.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", requirement.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", requirement.Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", requirement.Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", requirement.RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", requirement.RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserEmployeeId", requirement.UserEmployeeId, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.UpdateByRequirementId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <returns>The number of rows updated in Requirement table</returns>
        public int UpdateByRequirementId(int RequirementId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Title, string Body, int RequirementStateId, int RequirementPriorityId, int UserEmployeeId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserEmployeeId", UserEmployeeId, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.UpdateByRequirementId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        ///
        public void DeleteAll()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in Requirement table</returns>
        public int DeleteByRequirementId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.DeleteByRequirementId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows affected in Requirement table</returns>
        public int DeleteByRequirementId(int RequirementId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.Requirement.DeleteByRequirementId]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                    RowsAffected = dp.Get<int>("RowsAffected");
                }
                                
                if (RowsAffected == 0) { throw new Exception("RowsAffected with no value"); }

                return RowsAffected;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        /// <summary>
        /// Function: Take the model stored in the given byte array to return the model. <br/>
        /// Note 1:   Similar to a decryptor function. <br/>
        /// Note 2:   The model need the [Serializable] decorator in model definition. <br/>
        /// </summary>
        public RequirementModel ByteArrayToRequirementModel<T>(byte[] arrRequirementModel)
        {
            try
            {
                if (arrRequirementModel == null)
                { return new RequirementModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrRequirementModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (RequirementModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"RequirementId: {RequirementId}, " +
				$"Active: {Active}, " +
				$"DateTimeCreation: {DateTimeCreation}, " +
				$"DateTimeLastModification: {DateTimeLastModification}, " +
				$"UserCreationId: {UserCreationId}, " +
				$"UserLastModificationId: {UserLastModificationId}, " +
				$"Title: {Title}, " +
				$"Body: {Body}, " +
				$"RequirementStateId: {RequirementStateId}, " +
				$"RequirementPriorityId: {RequirementPriorityId}, " +
				$"UserEmployeeId: {UserEmployeeId}";
        }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"<tr>
                <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RequirementId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Active}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{DateTimeCreation}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{DateTimeLastModification}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserCreationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserLastModificationId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Title}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Body}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RequirementStateId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RequirementPriorityId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserEmployeeId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
                </tr>";
        }
    }
}