using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class ElectricityRecordEqualityComparer : IEqualityComparer<ElectricityRecord>
    {
        public bool Equals(ElectricityRecord? x, ElectricityRecord? y)
        {
            return x.FlatNumber.Equals(y.FlatNumber) && x.OwnerName.Equals(y.OwnerName);
        }

        public int GetHashCode([DisallowNull] ElectricityRecord obj)
        {
            return base.GetHashCode();
        }
    }
}
