using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MetricUnitTest
{
    [DisplayName("level1")]
    enum Level1Enum
    {
        [DisplayName("one1")]
        One1,
        two1, three1
    }

    [DisplayName("level2")]
    enum Level2Enum
    {
        one2, two2, three2, four2
    }

    [DisplayName("level3")]
    enum Level3Enum
    {
        one3, two3, three3, four3, five3
    }

}
