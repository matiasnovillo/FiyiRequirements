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
    /// Fields:            9 <br/> 
    /// Sub-models:      0 models <br/>
    /// Last modification: 25/12/2022 18:01:44
    /// </summary>
    [Serializable]
    public partial class RequirementChangehistoryModel
    {
        [NotMapped]
        private string _ConnectionString = "data source =.; initial catalog = fiyistack_FiyiRequirements; Integrated Security = SSPI; MultipleActiveResultSets=True;Pooling=false;Persist Security Info=True;App=EntityFramework;TrustServerCertificate=True";

        #region Fields
        [Library.ModelAttributeValidator.Key("RequirementChangehistoryId")]
        public int RequirementChangehistoryId { get; set; }

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

        [Library.ModelAttributeValidator.Key("RequirementId")]
        public int RequirementId { get; set; }

        [Library.ModelAttributeValidator.Key("RequirementStateId")]
        public int RequirementStateId { get; set; }

        [Library.ModelAttributeValidator.Key("RequirementPriorityId")]
        public int RequirementPriorityId { get; set; }
        #endregion

        #region Sub-lists
        
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field RequirementChangehistoryId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       9 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public RequirementChangehistoryModel()
        {
            try 
            {
                RequirementChangehistoryId = 0;

                //Initialize sub-lists
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using RequirementChangehistoryId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       9 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public RequirementChangehistoryModel(int RequirementChangehistoryId)
        {
            try
            {
                List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();

                //Initialize sub-lists
                
                
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementChangehistoryId", RequirementChangehistoryId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<RequirementChangehistoryModel>
                    lstRequirementChangehistoryModel = (List<RequirementChangehistoryModel>)sqlConnection.Query<RequirementChangehistoryModel>("[dbo].[Requirement.RequirementChangehistory.Select1ByRequirementChangehistoryId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstRequirementChangehistoryModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[Requirement.RequirementChangehistory.Select1ByRequirementChangehistoryId] returned more than one register/row");
                }
        
                foreach (RequirementChangehistoryModel requirementchangehistory in lstRequirementChangehistoryModel)
                {
                    this.RequirementChangehistoryId = requirementchangehistory.RequirementChangehistoryId;
					this.Active = requirementchangehistory.Active;
					this.DateTimeCreation = requirementchangehistory.DateTimeCreation;
					this.DateTimeLastModification = requirementchangehistory.DateTimeLastModification;
					this.UserCreationId = requirementchangehistory.UserCreationId;
					this.UserLastModificationId = requirementchangehistory.UserLastModificationId;
					this.RequirementId = requirementchangehistory.RequirementId;
					this.RequirementStateId = requirementchangehistory.RequirementStateId;
					this.RequirementPriorityId = requirementchangehistory.RequirementPriorityId;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       9 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public RequirementChangehistoryModel(int RequirementChangehistoryId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int RequirementId, int RequirementStateId, int RequirementPriorityId)
        {
            try
            {
                //Initialize sub-lists
                

                this.RequirementChangehistoryId = RequirementChangehistoryId;
				this.Active = Active;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.RequirementId = RequirementId;
				this.RequirementStateId = RequirementStateId;
				this.RequirementPriorityId = RequirementPriorityId;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), requirementchangehistory, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       9 <br/> 
        /// Dependencies: 0 models depend on this model <br/>
        /// </summary>
        public RequirementChangehistoryModel(RequirementChangehistoryModel requirementchangehistory)
        {
            try
            {
                //Initialize sub-lists
                

                RequirementChangehistoryId = requirementchangehistory.RequirementChangehistoryId;
				Active = requirementchangehistory.Active;
				DateTimeCreation = requirementchangehistory.DateTimeCreation;
				DateTimeLastModification = requirementchangehistory.DateTimeLastModification;
				UserCreationId = requirementchangehistory.UserCreationId;
				UserLastModificationId = requirementchangehistory.UserLastModificationId;
				RequirementId = requirementchangehistory.RequirementId;
				RequirementStateId = requirementchangehistory.RequirementStateId;
				RequirementPriorityId = requirementchangehistory.RequirementPriorityId;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside RequirementChangehistory</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.Count]", commandType: CommandType.StoredProcedure, param: dp);
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
        public DataTable Select1ByRequirementChangehistoryIdToDataTable(int RequirementChangehistoryId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementChangehistoryId", RequirementChangehistoryId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.Select1ByRequirementChangehistoryId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[Requirement.RequirementChangehistory.Select1ByRequirementChangehistoryId] returned more than one register/row"); }

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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

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
        public RequirementChangehistoryModel Select1ByRequirementChangehistoryIdToModel(int RequirementChangehistoryId)
        {
            try
            {
                RequirementChangehistoryModel RequirementChangehistoryModel = new RequirementChangehistoryModel();
                List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementChangehistoryId", RequirementChangehistoryId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstRequirementChangehistoryModel = (List<RequirementChangehistoryModel>)sqlConnection.Query<RequirementChangehistoryModel>("[dbo].[Requirement.RequirementChangehistory.Select1ByRequirementChangehistoryId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstRequirementChangehistoryModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[Requirement.RequirementChangehistory.Select1ByRequirementChangehistoryId] returned more than one register/row"); }

                foreach (RequirementChangehistoryModel requirementchangehistory in lstRequirementChangehistoryModel)
                {
                    RequirementChangehistoryModel.RequirementChangehistoryId = requirementchangehistory.RequirementChangehistoryId;
					RequirementChangehistoryModel.Active = requirementchangehistory.Active;
					RequirementChangehistoryModel.DateTimeCreation = requirementchangehistory.DateTimeCreation;
					RequirementChangehistoryModel.DateTimeLastModification = requirementchangehistory.DateTimeLastModification;
					RequirementChangehistoryModel.UserCreationId = requirementchangehistory.UserCreationId;
					RequirementChangehistoryModel.UserLastModificationId = requirementchangehistory.UserLastModificationId;
					RequirementChangehistoryModel.RequirementId = requirementchangehistory.RequirementId;
					RequirementChangehistoryModel.RequirementStateId = requirementchangehistory.RequirementStateId;
					RequirementChangehistoryModel.RequirementPriorityId = requirementchangehistory.RequirementPriorityId;
                }

                return RequirementChangehistoryModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<RequirementChangehistoryModel> SelectAllToList()
        {
            try
            {
                List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstRequirementChangehistoryModel = (List<RequirementChangehistoryModel>)sqlConnection.Query<RequirementChangehistoryModel>("[dbo].[Requirement.RequirementChangehistory.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstRequirementChangehistoryModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public requirementchangehistoryModelQuery SelectAllPagedToModel(requirementchangehistoryModelQuery requirementchangehistoryModelQuery)
        {
            try
            {
                requirementchangehistoryModelQuery.lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", requirementchangehistoryModelQuery.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", requirementchangehistoryModelQuery.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", requirementchangehistoryModelQuery.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", requirementchangehistoryModelQuery.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", requirementchangehistoryModelQuery.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", requirementchangehistoryModelQuery.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    requirementchangehistoryModelQuery.lstRequirementChangehistoryModel = (List<RequirementChangehistoryModel>)sqlConnection.Query<RequirementChangehistoryModel>("[dbo].[Requirement.RequirementChangehistory.SelectAllPaged]", dp, commandType: CommandType.StoredProcedure);
                    requirementchangehistoryModelQuery.TotalRows = dp.Get<int>("TotalRows");
                }

                requirementchangehistoryModelQuery.TotalPages = Library.Math.Divide(requirementchangehistoryModelQuery.TotalRows, requirementchangehistoryModelQuery.RowsPerPage, Library.Math.RoundType.RoundUp);

                

                return requirementchangehistoryModelQuery;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in RequirementChangehistory table</returns>
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
				dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in RequirementChangehistory table</returns>
        public int Insert(RequirementChangehistoryModel requirementchangehistory)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", requirementchangehistory.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", requirementchangehistory.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", requirementchangehistory.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", requirementchangehistory.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", requirementchangehistory.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementId", requirementchangehistory.RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementStateId", requirementchangehistory.RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", requirementchangehistory.RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in RequirementChangehistory table</returns>
        public int Insert(bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int RequirementId, int RequirementStateId, int RequirementPriorityId)
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
				dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in RequirementChangehistory table</returns>
        public int UpdateByRequirementChangehistoryId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementChangehistoryId", RequirementChangehistoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.UpdateByRequirementChangehistoryId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in RequirementChangehistory table</returns>
        public int UpdateByRequirementChangehistoryId(RequirementChangehistoryModel requirementchangehistory)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementChangehistoryId", requirementchangehistory.RequirementChangehistoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", requirementchangehistory.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", requirementchangehistory.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", requirementchangehistory.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", requirementchangehistory.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", requirementchangehistory.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementId", requirementchangehistory.RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementStateId", requirementchangehistory.RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", requirementchangehistory.RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.UpdateByRequirementChangehistoryId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in RequirementChangehistory table</returns>
        public int UpdateByRequirementChangehistoryId(int RequirementChangehistoryId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, int RequirementId, int RequirementStateId, int RequirementPriorityId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementChangehistoryId", RequirementChangehistoryId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementId", RequirementId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementStateId", RequirementStateId, DbType.Int32, ParameterDirection.Input);
				dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.UpdateByRequirementChangehistoryId]", commandType: CommandType.StoredProcedure, param: dp);
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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in RequirementChangehistory table</returns>
        public int DeleteByRequirementChangehistoryId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("RequirementChangehistoryId", RequirementChangehistoryId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.DeleteByRequirementChangehistoryId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows affected in RequirementChangehistory table</returns>
        public int DeleteByRequirementChangehistoryId(int RequirementChangehistoryId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("RequirementChangehistoryId", RequirementChangehistoryId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementChangehistory.DeleteByRequirementChangehistoryId]", commandType: CommandType.StoredProcedure, param: dp);
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
        public RequirementChangehistoryModel ByteArrayToRequirementChangehistoryModel<T>(byte[] arrRequirementChangehistoryModel)
        {
            try
            {
                if (arrRequirementChangehistoryModel == null)
                { return new RequirementChangehistoryModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrRequirementChangehistoryModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (RequirementChangehistoryModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"RequirementChangehistoryId: {RequirementChangehistoryId}, " +
				$"Active: {Active}, " +
				$"DateTimeCreation: {DateTimeCreation}, " +
				$"DateTimeLastModification: {DateTimeLastModification}, " +
				$"UserCreationId: {UserCreationId}, " +
				$"UserLastModificationId: {UserLastModificationId}, " +
				$"RequirementId: {RequirementId}, " +
				$"RequirementStateId: {RequirementStateId}, " +
				$"RequirementPriorityId: {RequirementPriorityId}";
        }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"<tr>
                <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RequirementChangehistoryId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RequirementId}</span>
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
    </td>
                </tr>";
        }
    }

    /// <summary>
    /// Virtual model used for [dbo].[Requirement.RequirementChangehistory.SelectAllPaged] stored procedure
    /// </summary>
    public partial class requirementchangehistoryModelQuery 
    {
        public string QueryString { get; set; }
        public int ActualPageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SorterColumn { get; set; }
        public bool SortToggler { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<RequirementChangehistoryModel> lstRequirementChangehistoryModel { get; set; }
    }
}