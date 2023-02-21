using FiyiRequirements.Areas.BasicCore.Models;
using System.Collections.Generic;

namespace FiyiRequirements.Areas.BasicCore.DTOs
{
    /// <summary>
    /// Virtual model used for [dbo].[BasicCore.Failure.SelectAllPaged] stored procedure
    /// </summary>
    public partial class failureSelectAllPaged
    {
        public string QueryString { get; set; }
        public int ActualPageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public string SorterColumn { get; set; }
        public bool SortToggler { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
        public List<FailureModel> lstFailureModel { get; set; }
    }
}
