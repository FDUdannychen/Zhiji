using System;
using System.Collections.Generic;
using System.Text;

namespace Zhiji.Common.Models
{
    public struct Range<T> : IEquatable<Range<T>>
        where T : IComparable
    {
        public static Range<T> WithLowerBound(T lowerBound, bool includeLowerBound = true)
            => new Range<T>(new Optional<T>(lowerBound), Optional<T>.None, includeLowerBound, false);

        public static Range<T> WithUpperBound(T upperBound, bool includeUpperBound = false)
            => new Range<T>(Optional<T>.None, new Optional<T>(upperBound), false, includeUpperBound);

        public static Range<T> WithBounds(T lowerBound, T upperBound, bool includeLowerBound = true, bool includeUpperBound = false)
            => new Range<T>(new Optional<T>(lowerBound), new Optional<T>(upperBound), includeLowerBound, includeUpperBound);

        public Optional<T> LowerBound { get; }
        public Optional<T> UpperBound { get; }
        public bool IncludeLowerBound { get; }
        public bool IncludeUpperBound { get; }
        
        private Range(Optional<T> lowerBound, Optional<T> upperBound, bool includeLowerBound, bool includeUpperBound)
        {
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.IncludeLowerBound = includeLowerBound;
            this.IncludeUpperBound = includeUpperBound;
        }

        public bool Contains(T value)
        {
            if (this.LowerBound.HasValue)
            {
                var r = value.CompareTo(this.LowerBound.GetValueOrDefault());
                if (r < 0 || !this.IncludeLowerBound && r == 0) return false;
            }

            if (this.UpperBound.HasValue)
            {
                var r = value.CompareTo(this.UpperBound.GetValueOrDefault());
                if (r > 0 || !this.IncludeUpperBound && r == 0) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.IncludeLowerBound.GetHashCode() * 3
                + this.IncludeUpperBound.GetHashCode() * 5
                + this.LowerBound.GetHashCode() * 7
                + this.UpperBound.GetHashCode() * 11;
        }

        public override bool Equals(object obj)
        {
            return obj is Range<T> other && this.Equals(other);
        }

        public bool Equals(Range<T> other)
        {
            return this.IncludeLowerBound == other.IncludeLowerBound
                && this.IncludeUpperBound == other.IncludeUpperBound
                && this.LowerBound.Equals(other.LowerBound)
                && this.UpperBound.Equals(other.UpperBound);
        }
    }
}
