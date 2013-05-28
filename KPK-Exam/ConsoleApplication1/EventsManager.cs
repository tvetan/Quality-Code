using System;
using System.Collections.Generic;
using System.Linq;

namespace CalendarSystem
{
    public class EventsManager : IEventsManager
    {
        private readonly List<CalendarEvent> currentEvents = new List<CalendarEvent>();

        public void AddEvent(CalendarEvent calendarEvent)
        {
            this.currentEvents.Add(calendarEvent);
        }

        public int DeleteEventsByTitle(string title)
        {
            string lowerCaseTitle = title.ToLowerInvariant();
            var deletedCalendarEvents = this.currentEvents.RemoveAll(
                calendarEvent => calendarEvent.Title.ToLowerInvariant() == lowerCaseTitle);

            return deletedCalendarEvents;
        }

        public IEnumerable<CalendarEvent> ListEvents(DateTime searchedDate, int numberOfReturnedEvents)
        {
            var matchedCalendarEvents = (from calendarEvent in this.currentEvents
                                         where calendarEvent.Date >= searchedDate
                                         orderby calendarEvent.Date, calendarEvent.Title, calendarEvent.Location
                                         select calendarEvent).Take(numberOfReturnedEvents);

            return matchedCalendarEvents;
        }
    }
}