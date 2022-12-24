using FiyiRequirements.Areas.BasicCulture.Models;
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

//Last modification on: 20/12/2022 20:14:59

namespace FiyiRequirements.Areas.BasicCulture.Protocols
{
    /// <summary>
    /// Stack:             5<br/>
    /// Name:              C# Protocol/Interface. <br/>
    /// Function:          This protocol/interface allow you to standardize the C# service associated. 
    ///                    In other words, define the functions that has to implement the C# service. <br/>
    /// Note:              Raise exception in case of missing any function declared here but not in the service. <br/>
    /// Last modification: 20/12/2022 20:14:59
    /// </summary>
    public partial interface ProvinceProtocol
    {
        #region Queries
        /// <summary>
        /// Note: Raise exception when the query find duplicated IDs
        /// </summary>
        /// <param name="ProvinceId"></param>
        /// <returns></returns>
        ProvinceModel Select1ByProvinceIdToModel(int ProvinceId);

        List<ProvinceModel> SelectAllToList();

        provinceModelQuery SelectAllPagedToModel(provinceModelQuery provinceModelQuery);
        #endregion

        #region Non-Queries
        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull insertion in database
        /// </summary>
        /// <param name="Province"></param>
        /// <returns>NewEnteredId: The ID of the last registry inserted in Province table</returns>
        int Insert(ProvinceModel Province);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull update in database
        /// </summary>
        /// <param name="Province"></param>
        /// <returns>The number of rows updated in Province table</returns>
        int UpdateByProvinceId(ProvinceModel Province);

        /// <summary>
        /// Note: Raise exception when the function did not made a succesfull deletion in database
        /// </summary>
        /// <param name="ProvinceId"></param>
        /// <returns>The number of rows deleted in Province table</returns>
        int DeleteByProvinceId(int ProvinceId);

        void DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByProvinceId(int ProvinceId);

        int[] CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion

        #region Other actions
        DateTime ExportAsPDF(Ajax Ajax, string ExportationType);

        DateTime ExportAsExcel(Ajax Ajax, string ExportationType);

        DateTime ExportAsCSV(Ajax Ajax, string ExportationType);
        #endregion
    }
}