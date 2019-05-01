using System;
using System.Collections.Generic;
using System.Text;

namespace VideoFragmentCodeChallenge.DataModel
{
    /* Coding Challenge: Things I thought of
     * 
     *      The ms values are ints instead of longs because an Int32 can hold
     * a maximum value of 2.147 billion, which converted from ms to hours is 596,
     * or watching the entire lord of the rings trilogy 54 times. A reasonable
     * assumption, but of course, consult your friendly Business Analyst.
     */

    public class Fragment
    {
        public int StartTime;
        public int EndTime;
    }
}
