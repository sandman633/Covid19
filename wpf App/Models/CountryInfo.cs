using System.Collections.Generic;

namespace Covid19.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<ConfirmedCount> ProvinceCounts { get; set; }

    }
}