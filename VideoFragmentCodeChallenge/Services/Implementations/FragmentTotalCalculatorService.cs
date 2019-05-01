using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoFragmentCodeChallenge.DataModel;
using VideoFragmentCodeChallenge.Services.Interfaces;

namespace VideoFragmentCodeChallenge.Services.Implementations
{
    public class FragmentTotalCalculatorService : IFragmentTotalCalculatorService
    {
        /* Coding Challenge: Things I thought of
         * 
         *      To sort, or not?
         *      I decided it's the responsibility of this class to not only 
         *  total the portion of the video watched, but also to sort the 
         *  fragments coming in. One could argue that the fragments getting
         *  passed to the CaculateTotal function should be prepared properly,
         *  however ANY service could use this class due to abstraction.
         *      More, it contributes to the robustness of calculating the total.
         *  At the performance cost of calling OrderBy, we're guaranteed to
         *  calculate the total correctly. Also, we've removed the requirement
         *  that fragments must be sorted before calculating the total (an 
         *  implementation detail not relevant to the caller).
         *  
         *      The Algorithm
         *      The algorithm works by sorting the fragments by StartTime from
         *  smallest to greatest. Next, we keep track of two goalposts: minStartTime
         *  and maxEndTime. 
         *      Iterating over the remaning fragments, when a fragment's
         *  start time is greater than the current maxEndTime, we know that the 
         *  aggregate fragment (minStartTime / maxEndTime) has combined all
         *  previous fragments, and can be added to the total.
         *      If a fragment's start time is not greater than the aggregate fragments's
         *  maxEndTime, then we check if the fragment's EndTime is greater than maxEndTime,
         *  which will mean one of two things:
         *      1) The current fragment is completely contained within the aggregate, and
         *          therefore adds no new information, or
         *      2) The aggregate fragment can extend its maxEndTime to the current fragment,
         *          thereby joining the current fragment with the aggregate.
         *      At the end, the total is updated with the final minStartTime/maxEndTime 
         *  values. This is the total time in ms the user has watched the video.
         *  
         */

        public int CalculateTotal(IReadOnlyCollection<Fragment> fragments)
        {
            if (fragments == null)
            {
                throw new ArgumentNullException(nameof(fragments));
            }

            int total = 0;

            if (fragments.Count == 0)
            {
                return total;
            }

            var sortedFragments = fragments.OrderBy(f => f.StartTime);

            int minStartTime = sortedFragments.First().StartTime;
            int maxEndTime = sortedFragments.First().EndTime;

            foreach (var fragment in sortedFragments)
            {
                if (fragment.StartTime > maxEndTime)
                {
                    total += (maxEndTime - minStartTime);
                    minStartTime = fragment.StartTime;
                    maxEndTime = fragment.EndTime;
                }
                else if (fragment.EndTime > maxEndTime)
                {
                    maxEndTime = fragment.EndTime;
                }
            }

            total += (maxEndTime - minStartTime);

            return total;
        }
    }
}
