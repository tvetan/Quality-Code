using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace CalendarSystem
{
    public class EventsManagerFast : IEventsManager
    {
        private readonly MultiDictionary<string, CalendarEvent> currentEventsUnordered =
            new MultiDictionary<string, CalendarEvent>(true);

        private readonly OrderedMultiDictionary<DateTime, CalendarEvent> currentEventsOrdered =
            new OrderedMultiDictionary<DateTime, CalendarEvent>(true);

        private int currentEventsCount = 0;

        public int CurrentEventsCount
        {
            get
            {
                return this.currentEventsCount;
            }
        }

        public void AddEvent(CalendarEvent addedEvent)
        {
            string eventTitleLowerCase = addedEvent.Title.ToLowerInvariant();

            this.currentEventsUnordered.Add(eventTitleLowerCase, addedEvent);
            this.currentEventsOrdered.Add(addedEvent.Date, addedEvent);
            this.currentEventsCount++;
        }

        public int DeleteEventsByTitle(string title)
        {
            string titleInLowerCase = title.ToLowerInvariant();
            var deletedCalendarEvents = this.currentEventsUnordered[titleInLowerCase];
            int deletedEventsCount = deletedCalendarEvents.Count;

            foreach (var calenderEvemt in deletedCalendarEvents)
            {
                this.currentEventsOrdered.Remove(calenderEvemt.Date, calenderEvemt);
            }

            this.currentEventsUnordered.Remove(titleInLowerCase);
            this.currentEventsCount -= deletedEventsCount;

            return deletedEventsCount;
        }

        public IEnumerable<CalendarEvent> ListEvents(DateTime searchedDate, int numberOfReturnedEvents)
        {
            var matchedCalendarEvents =
                                       from matchedEvent in this.currentEventsOrdered.RangeFrom(searchedDate, true).Values
                                       select matchedEvent;

            var filteredEvents = matchedCalendarEvents.Take(numberOfReturnedEvents);
            
            return filteredEvents;
        }
    }
}