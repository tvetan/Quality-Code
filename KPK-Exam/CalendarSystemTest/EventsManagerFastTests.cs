using System;
using System.Globalization;
using System.Linq;
using CalendarSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalendarSystemTest
{
    [TestClass]
    public class EventsManagerFastTests
    {
        [TestMethod]
        public void AddSingleEventTest()
        {
            CalendarEvent calendarEvent = new CalendarEvent();
            calendarEvent.Title = "Party";

            EventsManagerFast eventsManager = new EventsManagerFast();

            eventsManager.AddEvent(calendarEvent);

            Assert.AreEqual(eventsManager.CurrentEventsCount, 1);
        }

        [TestMethod]
        public void AddTwoEventsTest()
        {
            CalendarEvent calendarEvent = new CalendarEvent();
            calendarEvent.Title = "Party";
            CalendarEvent calendarEvent1 = new CalendarEvent();
            calendarEvent1.Title = "ivan";
            EventsManagerFast eventsManager = new EventsManagerFast();

            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent1);

            Assert.AreEqual(2, eventsManager.CurrentEventsCount);
        }

        [TestMethod]
        public void AddThreeEventsTest()
        {
            CalendarEvent calendarEvent = new CalendarEvent();
            calendarEvent.Title = "Party";
            CalendarEvent calendarEvent1 = new CalendarEvent();
            calendarEvent1.Title = "ivan";
            CalendarEvent calendarEvent2 = new CalendarEvent();
            calendarEvent2.Title = "ivan1";
            EventsManagerFast eventsManager = new EventsManagerFast();

            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent1);
            eventsManager.AddEvent(calendarEvent2);

            Assert.AreEqual(3, eventsManager.CurrentEventsCount);
        }

        [TestMethod]
        public void AddSingleEventWithDateTimeAndNullLocationCountTest()
        {
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var calendarEvent = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent);

            Assert.AreEqual(eventsManager.CurrentEventsCount, 1);
        }

        [TestMethod]
        public void AddDuplicatedEventsTest()
        {
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var calendarEvent = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent1 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };
            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent1);

            Assert.AreEqual(eventsManager.CurrentEventsCount, 2);
        }

        [TestMethod]
        public void AddDuplicatedAndNoDuplicateEventsTest()
        {
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var date1 = DateTime.ParseExact("2011-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var calendarEvent = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent1 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent2 = new CalendarEvent
            {
                Date = date1,
                Title = "Big party",
                Location = "kjfals",
            };

            var calendarEvent3 = new CalendarEvent
            {
                Date = date1,
                Title = "Not so big party",
                Location = "Sofia",
            };

            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent1);
            eventsManager.AddEvent(calendarEvent2);
            eventsManager.AddEvent(calendarEvent3);

            Assert.AreEqual(eventsManager.CurrentEventsCount, 4);
        }

        [TestMethod]
        public void AddSameCalendarEventDuplicatedEvents()
        {
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var calendarEvent = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent);

            Assert.AreEqual(eventsManager.CurrentEventsCount, 2);
        }

        [TestMethod]
        public void DeleteEventFromEmptyEventManager()
        {
            EventsManagerFast eventsManager = new EventsManagerFast();

            eventsManager.DeleteEventsByTitle("");

            Assert.AreEqual(eventsManager.CurrentEventsCount, 0);
        }

        [TestMethod]
        public void DeleteEventSingleCommandInEventManager()
        {
            EventsManagerFast eventsManager = new EventsManagerFast();
            CalendarEvent calendarEvent = new CalendarEvent();

            calendarEvent.Title = "Party";
            eventsManager.AddEvent(calendarEvent);
        
            Assert.AreEqual(eventsManager.CurrentEventsCount, 1);

            eventsManager.DeleteEventsByTitle("Party");
            Assert.AreEqual(eventsManager.CurrentEventsCount, 0);
        }

        [TestMethod]
        public void DeleteEventNoMatchingEvents()
        {
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var date1 = DateTime.ParseExact("2011-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var date2 = DateTime.ParseExact("2012-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

            var calendarEvent1 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent2 = new CalendarEvent
            {
                Date = date1,
                Title = "1party Lora1",
                Location = null,
            };


            var calendarEvent3 = new CalendarEvent
            {
                Date = date2,
                Title = "2party Lora2",
                Location = "Sofia",
            };

            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent3);
            eventsManager.AddEvent(calendarEvent1);
            eventsManager.AddEvent(calendarEvent2);

            eventsManager.DeleteEventsByTitle("SomeTitle");

            Assert.AreEqual(eventsManager.CurrentEventsCount, 3);
        }

        [TestMethod]
        public void DeleteEventDuplicateCommandInEventManager()
        { 
            CalendarEvent calendarEvent = new CalendarEvent();
            calendarEvent.Title = "Party";
            
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var calendarEvent1 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent2 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent1);
            eventsManager.AddEvent(calendarEvent2);

            Assert.AreEqual(eventsManager.CurrentEventsCount, 4);

            eventsManager.DeleteEventsByTitle("party Lora");
            Assert.AreEqual(eventsManager.CurrentEventsCount, 2);
        }

        [TestMethod]
        public void ListEventsWithNoElements()
        {
            EventsManagerFast eventsManager = new EventsManagerFast();
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var matchedEvents = eventsManager.ListEvents(date, 100).ToList();

            Assert.AreEqual(0, matchedEvents.Count);
        }

        [TestMethod]
        public void ListEventsWithOneMatchingAndOtherUnmatching()
        {            
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var date1 = DateTime.ParseExact("2011-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            CalendarEvent calendarEvent = new CalendarEvent();
            calendarEvent.Title = "Party";
            calendarEvent.Date = date;

            var calendarEvent1 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent2 = new CalendarEvent
            {
                Date = date1,
                Title = "party Lora",
                Location = null,
            };

            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent1);
            eventsManager.AddEvent(calendarEvent2);

            var matchedEvents = eventsManager.ListEvents(date1, 100).ToList();
            Assert.AreEqual(1, matchedEvents.Count);
        }

        [TestMethod]
        public void ListEventsWithMatchingLessThenTheRequestedCountAndOtherUnmatching()
        {
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var date1 = DateTime.ParseExact("2011-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            CalendarEvent calendarEvent = new CalendarEvent();
            calendarEvent.Title = "Party";
            calendarEvent.Date = date;

            var calendarEvent1 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent2 = new CalendarEvent
            {
                Date = date1,
                Title = "party Lora",
                Location = null,
            };

            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent);
            eventsManager.AddEvent(calendarEvent1);
            eventsManager.AddEvent(calendarEvent2);

            var matchedEvents = eventsManager.ListEvents(date1, 3).ToList();
            Assert.AreEqual(1, matchedEvents.Count);
        }

        [TestMethod]
        public void ListEventsWithMoreMatchingThenTheRequstedCount()
        {
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var date1 = DateTime.ParseExact("2011-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);           

            var calendarEvent1 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent2 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };


            var calendarEvent3 = new CalendarEvent
            {
                Date = date1,
                Title = "party Lora",
                Location = "Sofia",
            };
            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent3);
            eventsManager.AddEvent(calendarEvent1);
            eventsManager.AddEvent(calendarEvent2);

            var matchedEvents = eventsManager.ListEvents(date, 1).ToList();
            Assert.AreEqual(1, matchedEvents.Count);
        }

        [TestMethod]
        public void ListEventsWithNoMatchingEvents()
        {
            var date = DateTime.ParseExact("2001-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var date1 = DateTime.ParseExact("2011-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            var date2 = DateTime.ParseExact("2012-01-01T12:00:00", "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

            var calendarEvent1 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };

            var calendarEvent2 = new CalendarEvent
            {
                Date = date,
                Title = "party Lora",
                Location = null,
            };


            var calendarEvent3 = new CalendarEvent
            {
                Date = date1,
                Title = "party Lora",
                Location = "Sofia",
            };
            EventsManagerFast eventsManager = new EventsManagerFast();
            eventsManager.AddEvent(calendarEvent3);
            eventsManager.AddEvent(calendarEvent1);
            eventsManager.AddEvent(calendarEvent2);

            var matchedEvents = eventsManager.ListEvents(date2, 5).ToList();
            Assert.AreEqual(0, matchedEvents.Count);
        }

    }
}