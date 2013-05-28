using System;
using System.Collections.Generic;

namespace CalendarSystem
{
    public interface IEventsManager
    {
        /// <summary>
        /// Adds a calendar event to a list containing all of the current events.
        /// There can be duplicate events.
        /// </summary>
        /// <param name="addedEvent">The calendar event that is
        /// going to be added to the current list of events.</param>
        void AddEvent(CalendarEvent addedEvent);

        /// <summary>
        /// Deletes all event in the current events list that match the provided title. 
        /// The matching is done in case insensitive manner.
        /// </summary>
        /// <param name="title">The title that we are going to match and remove from the current events list.</param>
        /// <returns>The number of events that were removed from the current list of events</returns>
        int DeleteEventsByTitle(string title);

        /// <summary>
        /// Finds all of the calendar events in the current events list, that are starting from the given date and time. We are 
        /// returning only the given count or less (if not enough events are available).
        /// </summary>
        /// <param name="searchedDate">The date and time that is going to be our starting point for
        /// matching the searched calendar events.</param>
        /// <param name="numberOfReturnedEvents">The number of matched events that are going to be returned.
        /// If the number of matched events is less we are returning all matched events.</param>
        /// <returns>IEnumerable containing all of the matched calendar events.</returns>
        IEnumerable<CalendarEvent> ListEvents(DateTime searchedDate, int numberOfReturnedEvents);
    }
}