using System;

namespace ShacoBox
{
    public class DeadLine
    {
        TimeSpan GetRestTime(DateTime deadLine)
        {
            return deadLine.Subtract(DateTime.Now);
        }
    }
}
