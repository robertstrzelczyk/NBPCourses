using System.Collections.Generic;

namespace NBPCourses.Core.Integrations.Models
{
    public class ExchangeRatesTable
    {
        public string Table { get; set; }
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public List<Rate> Rates { get; set; }
    }
}
