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
    /// Fields:            8 <br/> 
    /// Sub-models:      2 models <br/>
    /// Last modification: 25/12/2022 18:13:11
    /// </summary>
    [Serializable]
    public partial class RequirementPriorityModel
    {
        [NotMapped]
        private string _ConnectionString = "data source =.; initial catalog = fiyistack_FiyiRequirements; Integrated Security = SSPI; MultipleActiveResultSets=True;Pooling=false;Persist Security Info=True;App=EntityFramework;TrustServerCertificate=True";

        #region Fields
        [Library.ModelAttributeValidator.Key("RequirementPriorityId")]
        public int RequirementPriorityId { get; set; }

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

        [Library.ModelAttributeValidator.String("Name", false, 1, 100, "")]
        public string Name { get; set; }

        [Library.ModelAttributeValidator.String("Description", false, 1, 2000, "")]
        public string Description { get; set; }

        public string UserCreationIdFantasyName { get; set; }

        public string UserLastModificationIdFantasyName { get; set; }
    #endregion

    #region Sub-lists
    public virtual List<RequirementModel> lstRequirementModel { get; set; } //Foreign Key name: RequirementPriorityId 
		public virtual List<RequirementChangehistoryModel> lstRequirementChangehistoryModel { get; set; } //Foreign Key name: RequirementPriorityId 
        #endregion

        #region Constructors
        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create fastly this model with field RequirementPriorityId = 0 <br/>
        /// Note 1:       Generally used to have fast access to functions of object. <br/>
        /// Note 2:       Need construction with [new] reserved word, as all constructors. <br/>
        /// Fields:       8 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RequirementPriorityModel()
        {
            try 
            {
                RequirementPriorityId = 0;

                //Initialize sub-lists
                lstRequirementModel = new List<RequirementModel>();
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with stored information in database using RequirementPriorityId <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       8 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RequirementPriorityModel(int RequirementPriorityId)
        {
            try
            {
                List<RequirementPriorityModel> lstRequirementPriorityModel = new List<RequirementPriorityModel>();

                //Initialize sub-lists
                lstRequirementModel = new List<RequirementModel>();
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                
                
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    //In case of not finding anything, Dapper return a List<RequirementPriorityModel>
                    lstRequirementPriorityModel = (List<RequirementPriorityModel>)sqlConnection.Query<RequirementPriorityModel>("[dbo].[Requirement.RequirementPriority.Select1ByRequirementPriorityId]", dp, commandType: CommandType.StoredProcedure);
                }

                if (lstRequirementPriorityModel.Count > 1)
                {
                    throw new Exception("The stored procedure [dbo].[Requirement.RequirementPriority.Select1ByRequirementPriorityId] returned more than one register/row");
                }
        
                foreach (RequirementPriorityModel requirementpriority in lstRequirementPriorityModel)
                {
                    this.RequirementPriorityId = requirementpriority.RequirementPriorityId;
					this.Active = requirementpriority.Active;
					this.DateTimeCreation = requirementpriority.DateTimeCreation;
					this.DateTimeLastModification = requirementpriority.DateTimeLastModification;
					this.UserCreationId = requirementpriority.UserCreationId;
					this.UserLastModificationId = requirementpriority.UserLastModificationId;
					this.Name = requirementpriority.Name;
					this.Description = requirementpriority.Description;
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model with filled parameters <br/>
        /// Note:         Raise exception on duplicated IDs <br/>
        /// Fields:       8 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RequirementPriorityModel(int RequirementPriorityId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Name, string Description)
        {
            try
            {
                //Initialize sub-lists
                lstRequirementModel = new List<RequirementModel>();
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                

                this.RequirementPriorityId = RequirementPriorityId;
				this.Active = Active;
				this.DateTimeCreation = DateTimeCreation;
				this.DateTimeLastModification = DateTimeLastModification;
				this.UserCreationId = UserCreationId;
				this.UserLastModificationId = UserLastModificationId;
				this.Name = Name;
				this.Description = Description;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Stack:        3 <br/>
        /// Function:     Create this model (copy) using the given model (original), requirementpriority, passed by parameter. <br/>
        /// Note:         This constructor is generally used to execute functions using the copied fields <br/>
        /// Fields:       8 <br/> 
        /// Dependencies: 2 models depend on this model <br/>
        /// </summary>
        public RequirementPriorityModel(RequirementPriorityModel requirementpriority)
        {
            try
            {
                //Initialize sub-lists
                lstRequirementModel = new List<RequirementModel>();
                lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                

                RequirementPriorityId = requirementpriority.RequirementPriorityId;
				Active = requirementpriority.Active;
				DateTimeCreation = requirementpriority.DateTimeCreation;
				DateTimeLastModification = requirementpriority.DateTimeLastModification;
				UserCreationId = requirementpriority.UserCreationId;
				UserLastModificationId = requirementpriority.UserLastModificationId;
				Name = requirementpriority.Name;
				Description = requirementpriority.Description;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The number of rows inside RequirementPriority</returns>
        public int Count()
        {
            try
            {
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.Count]", commandType: CommandType.StoredProcedure, param: dp);
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
        public DataTable Select1ByRequirementPriorityIdToDataTable(int RequirementPriorityId)
        {
            try
            {
                DataTable DataTable = new DataTable();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.Select1ByRequirementPriorityId]", commandType: CommandType.StoredProcedure, param: dp);

                    DataTable.Load(dataReader);
                }

                if (DataTable.Rows.Count > 1)
                { throw new Exception("The stored procedure [dbo].[Requirement.RequirementPriority.Select1ByRequirementPriorityId] returned more than one register/row"); }

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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.SelectAll]", commandType: CommandType.StoredProcedure, param: dp);

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
        public RequirementPriorityModel Select1ByRequirementPriorityIdToModel(int RequirementPriorityId)
        {
            try
            {
                RequirementPriorityModel RequirementPriorityModel = new RequirementPriorityModel();
                List<RequirementPriorityModel> lstRequirementPriorityModel = new List<RequirementPriorityModel>();
                DynamicParameters dp = new DynamicParameters();

                dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstRequirementPriorityModel = (List<RequirementPriorityModel>)sqlConnection.Query<RequirementPriorityModel>("[dbo].[Requirement.RequirementPriority.Select1ByRequirementPriorityId]", dp, commandType: CommandType.StoredProcedure);
                }
        
                if (lstRequirementPriorityModel.Count > 1)
                { throw new Exception("The stored procedure [dbo].[Requirement.RequirementPriority.Select1ByRequirementPriorityId] returned more than one register/row"); }

                foreach (RequirementPriorityModel requirementpriority in lstRequirementPriorityModel)
                {
                    RequirementPriorityModel.RequirementPriorityId = requirementpriority.RequirementPriorityId;
					RequirementPriorityModel.Active = requirementpriority.Active;
					RequirementPriorityModel.DateTimeCreation = requirementpriority.DateTimeCreation;
					RequirementPriorityModel.DateTimeLastModification = requirementpriority.DateTimeLastModification;
					RequirementPriorityModel.UserCreationId = requirementpriority.UserCreationId;
					RequirementPriorityModel.UserLastModificationId = requirementpriority.UserLastModificationId;
					RequirementPriorityModel.Name = requirementpriority.Name;
					RequirementPriorityModel.Description = requirementpriority.Description;
                }

                return RequirementPriorityModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public List<RequirementPriorityModel> SelectAllToList()
        {
            try
            {
                List<RequirementPriorityModel> lstRequirementPriorityModel = new List<RequirementPriorityModel>();
                DynamicParameters dp = new DynamicParameters();

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    lstRequirementPriorityModel = (List<RequirementPriorityModel>)sqlConnection.Query<RequirementPriorityModel>("[dbo].[Requirement.RequirementPriority.SelectAll]", dp, commandType: CommandType.StoredProcedure);
                }

                return lstRequirementPriorityModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public requirementpriorityModelQuery SelectAllPagedToModel(requirementpriorityModelQuery requirementpriorityModelQuery)
        {
            try
            {
                requirementpriorityModelQuery.lstRequirementPriorityModel = new List<RequirementPriorityModel>();
                DynamicParameters dp = new DynamicParameters();
                dp.Add("QueryString", requirementpriorityModelQuery.QueryString, DbType.String, ParameterDirection.Input);
                dp.Add("ActualPageNumber", requirementpriorityModelQuery.ActualPageNumber, DbType.Int32, ParameterDirection.Input);
                dp.Add("RowsPerPage", requirementpriorityModelQuery.RowsPerPage, DbType.Int32, ParameterDirection.Input);
                dp.Add("SorterColumn", requirementpriorityModelQuery.SorterColumn, DbType.String, ParameterDirection.Input);
                dp.Add("SortToggler", requirementpriorityModelQuery.SortToggler, DbType.Boolean, ParameterDirection.Input);
                dp.Add("TotalRows", requirementpriorityModelQuery.TotalRows, DbType.Int32, ParameterDirection.Output);

                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    requirementpriorityModelQuery.lstRequirementPriorityModel = (List<RequirementPriorityModel>)sqlConnection.Query<RequirementPriorityModel>("[dbo].[Requirement.RequirementPriority.SelectAllPagedCustom]", dp, commandType: CommandType.StoredProcedure);
                    requirementpriorityModelQuery.TotalRows = dp.Get<int>("TotalRows");
                }

                requirementpriorityModelQuery.TotalPages = Library.Math.Divide(requirementpriorityModelQuery.TotalRows, requirementpriorityModelQuery.RowsPerPage, Library.Math.RoundType.RoundUp);

                //Loop through lists and sublists
                for (int i = 0; i < requirementpriorityModelQuery.lstRequirementPriorityModel.Count; i++)
                {
                    DynamicParameters dpForRequirementModel = new DynamicParameters();
                    dpForRequirementModel.Add("RequirementPriorityId", requirementpriorityModelQuery.lstRequirementPriorityModel[i].RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<RequirementModel> lstRequirementModel = new List<RequirementModel>();
                        lstRequirementModel = (List<RequirementModel>)sqlConnection.Query<RequirementModel>("[dbo].[Requirement.Requirement.SelectAllByRequirementPriorityIdCustom]", dpForRequirementModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var RequirementModel in lstRequirementModel)
                        {
                            requirementpriorityModelQuery.lstRequirementPriorityModel[i].lstRequirementModel.Add(RequirementModel);
                        }
                    }
                }
                
                //Loop through lists and sublists
                for (int i = 0; i < requirementpriorityModelQuery.lstRequirementPriorityModel.Count; i++)
                {
                    DynamicParameters dpForRequirementChangehistoryModel = new DynamicParameters();
                    dpForRequirementChangehistoryModel.Add("RequirementPriorityId", requirementpriorityModelQuery.lstRequirementPriorityModel[i].RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
                    using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                    {
                        List<RequirementChangehistoryModel> lstRequirementChangehistoryModel = new List<RequirementChangehistoryModel>();
                        lstRequirementChangehistoryModel = (List<RequirementChangehistoryModel>)sqlConnection.Query<RequirementChangehistoryModel>("[dbo].[Requirement.RequirementChangehistory.SelectAllByRequirementPriorityIdCustom]", dpForRequirementChangehistoryModel, commandType: CommandType.StoredProcedure);
                        
                        //Add list item inside another list
                        foreach (var RequirementChangehistoryModel in lstRequirementChangehistoryModel)
                        {
                            requirementpriorityModelQuery.lstRequirementPriorityModel[i].lstRequirementChangehistoryModel.Add(RequirementChangehistoryModel);
                        }
                    }
                }
                
                

                return requirementpriorityModelQuery;
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <returns>NewEnteredId: The ID of the last registry inserted in RequirementPriority table</returns>
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
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Description", Description, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in RequirementPriority table</returns>
        public int Insert(RequirementPriorityModel requirementpriority)
        {
            try
            {
                int NewEnteredId = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("Active", requirementpriority.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", requirementpriority.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", requirementpriority.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", requirementpriority.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", requirementpriority.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", requirementpriority.Name, DbType.String, ParameterDirection.Input);
				dp.Add("Description", requirementpriority.Description, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
                
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The ID of the last registry inserted in RequirementPriority table</returns>
        public int Insert(bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Name, string Description)
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
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Description", Description, DbType.String, ParameterDirection.Input);
                dp.Add("NewEnteredId", NewEnteredId, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.Insert]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in RequirementPriority table</returns>
        public int UpdateByRequirementPriorityId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Description", Description, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.UpdateByRequirementPriorityId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in RequirementPriority table</returns>
        public int UpdateByRequirementPriorityId(RequirementPriorityModel requirementpriority)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementPriorityId", requirementpriority.RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", requirementpriority.Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", requirementpriority.DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", requirementpriority.DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", requirementpriority.UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", requirementpriority.UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", requirementpriority.Name, DbType.String, ParameterDirection.Input);
				dp.Add("Description", requirementpriority.Description, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.UpdateByRequirementPriorityId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows updated in RequirementPriority table</returns>
        public int UpdateByRequirementPriorityId(int RequirementPriorityId, bool Active, DateTime DateTimeCreation, DateTime DateTimeLastModification, int UserCreationId, int UserLastModificationId, string Name, string Description)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();

                dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Active", Active, DbType.Boolean, ParameterDirection.Input);
				dp.Add("DateTimeCreation", DateTimeCreation, DbType.DateTime, ParameterDirection.Input);
				dp.Add("DateTimeLastModification", DateTimeLastModification, DbType.DateTime, ParameterDirection.Input);
				dp.Add("UserCreationId", UserCreationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("UserLastModificationId", UserLastModificationId, DbType.Int32, ParameterDirection.Input);
				dp.Add("Name", Name, DbType.String, ParameterDirection.Input);
				dp.Add("Description", Description, DbType.String, ParameterDirection.Input);
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.UpdateByRequirementPriorityId]", commandType: CommandType.StoredProcedure, param: dp);
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
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.DeleteAll]", commandType: CommandType.StoredProcedure, param: dp);
                    DataTable.Load(dataReader);
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <returns>The number of rows deleted in RequirementPriority table</returns>
        public int DeleteByRequirementPriorityId()
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.DeleteByRequirementPriorityId]", commandType: CommandType.StoredProcedure, param: dp);
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
        /// <returns>The number of rows affected in RequirementPriority table</returns>
        public int DeleteByRequirementPriorityId(int RequirementPriorityId)
        {
            try
            {
                int RowsAffected = 0;
                DynamicParameters dp = new DynamicParameters();
                DataTable DataTable = new DataTable();
        
                dp.Add("RequirementPriorityId", RequirementPriorityId, DbType.Int32, ParameterDirection.Input);        
                dp.Add("RowsAffected", RowsAffected, DbType.Int32, ParameterDirection.Output);
        
                using (SqlConnection sqlConnection = new SqlConnection(_ConnectionString))
                {
                    var dataReader = sqlConnection.ExecuteReader("[dbo].[Requirement.RequirementPriority.DeleteByRequirementPriorityId]", commandType: CommandType.StoredProcedure, param: dp);
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
        public RequirementPriorityModel ByteArrayToRequirementPriorityModel<T>(byte[] arrRequirementPriorityModel)
        {
            try
            {
                if (arrRequirementPriorityModel == null)
                { return new RequirementPriorityModel(); }
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                using MemoryStream MemoryStream = new MemoryStream(arrRequirementPriorityModel);
                object Object = BinaryFormatter.Deserialize(MemoryStream);
                return (RequirementPriorityModel)Object;
            }
            catch (Exception ex)
            { throw ex; }
        }
        
        /// <summary>
        /// Function: Show all information (fields) inside the model during depuration mode.
        /// </summary>
        public override string ToString()
        {
            return $"RequirementPriorityId: {RequirementPriorityId}, " +
				$"Active: {Active}, " +
				$"DateTimeCreation: {DateTimeCreation}, " +
				$"DateTimeLastModification: {DateTimeLastModification}, " +
				$"UserCreationId: {UserCreationId}, " +
				$"UserLastModificationId: {UserLastModificationId}, " +
				$"Name: {Name}, " +
				$"Description: {Description}";
        }

        public string ToStringOnlyValuesForHTML()
        {
            return $@"<tr>
                <td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{RequirementPriorityId}</span>
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
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Name}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td><td align=""left"" valign=""top"">
        <div style=""height: 12px; line-height: 12px; font-size: 10px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px;"">{Description}</span>
        </font>
        <div style=""height: 40px; line-height: 40px; font-size: 38px;"">&nbsp;</div>
    </td>
                </tr>";
        }
    }

    /// <summary>
    /// Virtual model used for [dbo].[Requirement.RequirementPriority.SelectAllPaged] stored procedure
    /// </summary>
    public partial class requirementpriorityModelQuery 
    {
        public string QueryString { get; set; }
        public int ActualPageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SorterColumn { get; set; }
        public bool SortToggler { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<RequirementPriorityModel> lstRequirementPriorityModel { get; set; }
    }
}