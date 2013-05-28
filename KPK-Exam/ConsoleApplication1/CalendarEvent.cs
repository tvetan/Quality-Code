using System;
using System.Text;

namespace CalendarSystem
{
    public class CalendarEvent : IComparable<CalendarEvent>
    {
        public DateTime Date { get; set; }

        public string Title;

        public string Location;

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0:yyyy-MM-ddTHH:mm:ss} | {1}", this.Date, this.Title);

            if (this.Location != null)
            {
                result.AppendFormat(" | {0}", this.Location);
            }

            return result.ToString();
        }

        public int CompareTo(CalendarEvent other)
        {
            int result = DateTime.Compare(this.Date, other.Date);
            
            if (result == 0)
            {
                result = string.Compare(this.Title, other.Title);
            }

            if (result == 0)
            {
                result = string.Compare(this.Location, other.Location);
            }

            return result;
        }
    }
}