using FiyiRequirements.Areas.CMSCore.Models;
using FiyiRequirements.Library;
using System;
using System.Collections.Generic;

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

namespace FiyiRequirements.Areas.CMSCore.Protocols
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Protocol/Interface. <br/>
    /// Function:          This protocol/interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 20/12/2022 20:47:32
    /// </summary>
    public partial interface RoleProtocol
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        RoleModel Select1ByRoleIdToModel(int RoleId);

        List<RoleModel> SelectAllToList();

        roleModelQuery SelectAllPagedToModel(roleModelQuery roleModelQuery);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="Role"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Role table</returns>
        int Insert(RoleModel Role);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="Role"></param>
        /// <returns>The number of rows updated in Role table</returns>
        int UpdateByRoleId(RoleModel Role);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns>The number of rows deleted in Role table</returns>
        int DeleteByRoleId(int RoleId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByRoleId(int RoleId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}