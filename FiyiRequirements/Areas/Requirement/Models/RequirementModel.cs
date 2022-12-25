using Dapper;
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
 * Copyright © 2022
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
    /// Fields:            13 <br/> 
    /// Sub-models:      2 models <br/>
    /// Last modification: 25/12/2022 13:30:52
    /// </summary>
    [Serializable]
    public partial class RequirementModel
    {
        [NotMapped]
        private string _ConnectionString = "data source =.; initial catalog = fiyistack_FiyiRequirements; Integrated Security = SSPI; MultipleActiveResultSets=True;Pooling=false;Persist Security Info=True;App=EntityFramework;TrustServerCertificate=True";

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

        [Library.ModelAttributeValidator.Key("ClientId")]
        public int ClientId { get; set; }

        [Library.ModelAttributeValidator.String("Title", false, 1, 100, "")]
        public string Title { get; set; }

        [Library.ModelAttributeValidator.String("Body", false, 1, 8000, "")]
        public string Body { get; set; }

        [Library.ModelAttributeValidator.Key("RequirementStateId")]
        public int RequirementStateId { get; set; }

        [Library.ModelAttributeValidator.Key("RequirementTypeId")]
        public int RequirementTypeId { get; set; }

        [Library.ModelAttributeValidator.Key("RequirementPriorityId")]
        public int RequirementPriorityId { get; set; }

        [Library.ModelAttributeValidator.Key("UserProgrammerId")]
        public int UserProgrammerId { get; set; }
        #endregion

        #region Sub-lists
        public virtual List<RequirementChangehistoryModel> lstRequirementChangehistoryModel { get; set; } //Foreign Key name: RequirementId 
		public virtual List<RequirementFileModel> lstRequirementFileModel { get; set; } //Foreign Key name: RequirementId 
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field RequirementId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       13 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RequirementModel()
        {
            try 
            {
                RequirementId = 0;

                //Initialize sub-lists
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                lstRequirementFileModel = new List<RequirementFileModel>();
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using RequirementId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       13 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RequirementModel(int RequirementId)
        {
            try
            {
                List<RequirementModel> lstRequirementModel = new List<RequirementModel>();

                //Initialize sub-lists
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                lstRequirementFileModel = new List<RequirementFileModel>();
                
                
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
					this.ClientId = requirement.ClientId;
					this.Title = requirement.Title;
					this.Body = requirement.Body;
					this.RequirementStateId = requirement.RequirementStateId;
					this.RequirementTypeId = requirement.RequirementTypeId;
					this.RequirementPriorityId = requirement.RequirementPriorityId;
					this.UserProgrammerId = requirement.UserProgrammerId;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       13 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RequirementModel(int RequirementId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ClientId, string Title, string Body, int RequirementStateId, int RequirementTypeId, int RequirementPriorityId, int UserProgrammerId)
        {
            try
            {
                //Initialize sub-lists
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                lstRequirementFileModel = new List<RequirementFileModel>();
                

                this.RequirementId = RequirementId;
				this.Active = Active;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.ClientId = ClientId;
				this.Title = Title;
				this.Body = Body;
				this.RequirementStateId = RequirementStateId;
				this.RequirementTypeId = RequirementTypeId;
				this.RequirementPriorityId = RequirementPriorityId;
				this.UserProgrammerId = UserProgrammerId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), requirement, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       13 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RequirementModel(RequirementModel requirement)
        {
            try
            {
                //Initialize sub-lists
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                lstRequirementFileModel = new List<RequirementFileModel>();
                

                RequirementId = requirement.RequirementId;
				Active = requirement.Active;
				DateTimeCreation = requirement.DateTimeCreation;
				DateTimeLastModification = requirement.DateTimeLastModification;
				UserCreationId = requirement.UserCreationId;
				UserLastModificationId = requirement.UserLastModificationId;
				ClientId = requirement.ClientId;
				Title = requirement.Title;
				Body = requirement.Body;
				RequirementStateId = requirement.RequirementStateId;
				RequirementTypeId = requirement.RequirementTypeId;
				RequirementPriorityId = requirement.RequirementPriorityId;
				UserProgrammerId = requirement.UserProgrammerId;
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
					RequirementModel.ClientId = requirement.ClientId;
					RequirementModel.Title = requirement.Title;
					RequirementModel.Body = requirement.Body;
					RequirementModel.RequirementStateId = requirement.RequirementStateId;
					RequirementModel.RequirementTypeId = requirement.RequirementTypeId;
					RequirementModel.RequirementPriorityId = requirement.RequirementPriorityId;
					RequirementModel.UserProgrammerId = requirement.UserProgrammerId;
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

        public requirementModelQuery SelectAllPagedToModel(requirementModelQuery requirementModelQuery)
        {
            try
            {
                requirementModelQuery.lstRequirementModel = new List<RequirementModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", requirementModelQuery.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", requirementModelQuery.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", requirementModelQuery.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", requirementModelQuery.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", requirementModelQuery.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", requirementModelQuery.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    requirementModelQuery.lstRequirementModel = (List<RequirementModel>)sqlConnection.Query<RequirementModel>("[dbo].[Requirement.Requirement.SelectAllPaged]", dp, commandType: CommandType.StoredProcedure);
                    requirementModelQuery.TotalRows = dp.Get<int>("TotalRows");
                }

                requirementModelQuery.TotalPages = Library.Math.Divide(requirementModelQuery.TotalRows, requirementModelQuery.RowsPerPage, Library.Math.RoundType.RoundUp);

                //Loop through lists and sublists
                for (int i = 0; i < requirementModelQuery.lstRequirementModel.Count; i++)
                {
                    DynamicParameters dpForRequirementChangehistoryModel = new DynamicParameters();
                    dpForRequirementChangehistoryModel.Add("RequirementId", requirementModelQuery.lstRequirementModel[i].RequirementId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                        lstRequirementChangehistoryModel = (List<RequirementChangehistoryModel>)sqlConnection.Query<RequirementChangehistoryModel>("[dbo].[Requirement.RequirementChangehistory.SelectAllByRequirementIdCustom]", dpForRequirementChangehistoryModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var RequirementChangehistoryModel in lstRequirementChangehistoryModel)
                        {
                            requirementModelQuery.lstRequirementModel[i].lstRequirementChangehistoryModel.Add(RequirementChangehistoryModel);
                        }
                    }
                }
                
                //Loop through lists and sublists
                for (int i = 0; i < requirementModelQuery.lstRequirementModel.Count; i++)
                {
                    DynamicParameters dpForRequirementFileModel = new DynamicParameters();
                    dpForRequirementFileModel.Add("RequirementId", requirementModelQuery.lstRequirementModel[i].RequirementId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<RequirementFileModel> lstRequirementFileModel = new List<RequirementFileModel>();
                        lstRequirementFileModel = (List<RequirementFileModel>)sqlConnection.Query<RequirementFileModel>("[dbo].[Requirement.RequirementFile.SelectAllByRequirementIdCustom]", dpForRequirementFileModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var RequirementFileModel in lstRequirementFileModel)
                        {
                            requirementModelQuery.lstRequirementModel[i].lstRequirementFileModel.Add(RequirementFileModel);
                        }
                    }
                }
                
                

                return requirementModelQuery;
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
				dp.Add("ClientId", ClientId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementTypeId", RequirementTypeId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserProgrammerId", UserProgrammerId, DbType.Int32, ParameterDirection.Input);
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
				dp.Add("ClientId", requirement.ClientId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", requirement.Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", requirement.Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", requirement.RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementTypeId", requirement.RequirementTypeId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", requirement.RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserProgrammerId", requirement.UserProgrammerId, DbType.Int32, ParameterDirection.Input);
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
        public int Insert(bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ClientId, string Title, string Body, int RequirementStateId, int RequirementTypeId, int RequirementPriorityId, int UserProgrammerId)
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
				dp.Add("ClientId", ClientId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementTypeId", RequirementTypeId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserProgrammerId", UserProgrammerId, DbType.Int32, ParameterDirection.Input);
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
				dp.Add("ClientId", ClientId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementTypeId", RequirementTypeId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserProgrammerId", UserProgrammerId, DbType.Int32, ParameterDirection.Input);
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
				dp.Add("ClientId", requirement.ClientId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", requirement.Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", requirement.Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", requirement.RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementTypeId", requirement.RequirementTypeId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", requirement.RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserProgrammerId", requirement.UserProgrammerId, DbType.Int32, ParameterDirection.Input);
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
        public int UpdateByRequirementId(int RequirementId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int ClientId, string Title, string Body, int RequirementStateId, int RequirementTypeId, int RequirementPriorityId, int UserProgrammerId)
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
				dp.Add("ClientId", ClientId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Title", Title, DbType.String, ParameterDirection.Input);
				dp.Add("Body", Body, DbType.String, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementTypeId", RequirementTypeId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserProgrammerId", UserProgrammerId, DbType.Int32, ParameterDirection.Input);
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
				$"ClientId: {ClientId}, " +
				$"Title: {Title}, " +
				$"Body: {Body}, " +
				$"RequirementStateId: {RequirementStateId}, " +
				$"RequirementTypeId: {RequirementTypeId}, " +
				$"RequirementPriorityId: {RequirementPriorityId}, " +
				$"UserProgrammerId: {UserProgrammerId}";
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{ClientId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RequirementTypeId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{UserProgrammerId}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
                </tr>";
        }
    }

    /// <summary>
    /// Virtual model used for [dbo].[Requirement.Requirement.SelectAllPaged] stored procedure
    /// </summary>
    public partial class requirementModelQuery 
    {
        public string QueryString { get; set; }
        public int ActualPageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SorterColumn { get; set; }
        public bool SortToggler { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<RequirementModel> lstRequirementModel { get; set; }
    }
}