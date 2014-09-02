﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridsum.DataflowEx.ETL
{
    using C5;

    /// <summary>
    /// Represents a partial dim row from merge output result
    /// </summary>
    public class PartialDimRow<TLookupKey> : IDimRow<TLookupKey>, IComparable<PartialDimRow<TLookupKey>>
    {
        public PartialDimRow()
        {
            this.LastHitTime = DateTime.UtcNow;
        }

        public long AutoIncrementKey { get; set; }
        public TLookupKey JoinOn { get; set; }

        public bool IsFullRow
        {
            get
            {
                return false;
            }
        }

        public IPriorityQueueHandle<IDimRow<TLookupKey>> Handle { get; set; }

        public DateTime LastHitTime { get; set; }

        public int CompareTo(PartialDimRow<TLookupKey> other)
        {
            TimeSpan span = this.LastHitTime - other.LastHitTime;

            if (span.Ticks < 0) return -1;
            else if (span.Ticks > 0) return 1;
            else
            {
                return 0;
            }
        }
    }
}
