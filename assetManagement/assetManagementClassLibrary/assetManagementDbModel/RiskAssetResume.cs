using System;
using System.Collections.Generic;

namespace assetManagementClassLibrary.assetManagementDbModel
{
    public partial class RiskAssetResume
    {
        public int AssetId { get; set; }
        public string? AssetDescription { get; set; }
        public int? TotalValidations { get; set; }
        public int? CompleteValidations { get; set; }
        public int? CompletnessPercentaje { get; set; }
        public string RiskAssessment { get; set; } = null!;
    }
}
