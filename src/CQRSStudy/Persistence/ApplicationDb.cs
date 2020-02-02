using System;
using System.Collections.Generic;

namespace CQRSStudy.Persistence
{
    public class ApplicationDb
    {
        public List<CalculationResult> Results { get; set; } = new List<CalculationResult>();
    }
    public class CalculationResult
    {
        public string CalculationResultType;
        public int Result;
        public DateTime At;
    }
}