using System;

namespace ShacoBox
{
    class RealTime
    {
        TimeSpan GetRealTime()
        {
            return DateTime.Now.Subtract(new DateTime(0, 0, 0, 0, 0, 0, 0));
        }
    }
}
