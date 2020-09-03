using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CumulativeAgeCalculator.Services
{
    public class AgeCalculatorService
    {
        private DateTime _firstPersonDob;
        private List<DateTime> _otherDobs;

        public AgeCalculatorService()
        {
        }

        public decimal CalculateAge(DateTime firstPersonDob, List<DateTime> otherDobs)
        {
            _firstPersonDob = firstPersonDob;

            if (otherDobs == null)
            {
                throw new ArgumentNullException(nameof(otherDobs));
            }

            if (otherDobs.Count == 0)
            {
                throw new ArgumentException(nameof(otherDobs));
            }

            otherDobs.Sort();
            _otherDobs = otherDobs;

            int cumulativeAgeOfOthersAtStart = _CalculateCumulateAgeOfOthersInDaysAtStart();
            int ageOfFirstAtStart = _CalculateAgeOfFirstInDaysAtStart();

            decimal age = (decimal)ageOfFirstAtStart + ((decimal)(ageOfFirstAtStart - cumulativeAgeOfOthersAtStart) / (decimal)(_otherDobs.Count - 1));
            return age;
        }

        private int _CalculateCumulateAgeOfOthersInDaysAtStart()
        {
            DateTime youngest = _otherDobs.Last();
            var diffs = 0;

            if (_otherDobs.Count > 1)
            {
                IOrderedEnumerable<DateTime> others = _otherDobs.Take(_otherDobs.Count - 1).OrderByDescending(d => d);
                foreach(DateTime other in others)
                {
                    diffs += (int)(youngest - other).TotalDays;
                }
            }

            return diffs;
        }

        private int _CalculateAgeOfFirstInDaysAtStart()
        {
            DateTime youngest = _otherDobs.Last();
            return (int)(youngest - _firstPersonDob).TotalDays;
        }
    }
}
