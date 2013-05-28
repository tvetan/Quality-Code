using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CalendarSystem
{
    public class CalendarEventProcessor
    {
        private readonly IEventsManager eventManager;

        public CalendarEventProcessor(IEventsManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public IEventsManager EventsProcessor
        {
            get
            {
                return this.eventManager;
            }
        }

        public string ProcessCommand(Command command)
        {
            string commandName = command.Name;

            switch (commandName)
            {
                case "AddEvent":
                    return this.ProcessAddEventCommand(command);       
                case "DeleteEvents":
                    return this.ProcessDeleteEvent(command);
                case "ListEvents":
                    return this.ProcessListEventsCommand(command);
                default:
                    throw new ArgumentException("The command was not in the expected format");
            }
        }
  
        private string ProcessListEventsCommand(Command command)
        {
            var commandDate = DateTime.ParseExact(command.Parameters[0], "yyyy-MM-ddTHH:mm:ss",
                CultureInfo.InvariantCulture);
            var commandAsString = int.Parse(command.Parameters[1]);
            var matchedEvents = this.eventManager.ListEvents(commandDate, commandAsString).ToList();
            var result = new StringBuilder();
                        
            if (!matchedEvents.Any())
            {
                result.Append("No events found");
                return result.ToString();
            }

            foreach (var matchedEvent in matchedEvents)
            {
                result.AppendLine(matchedEvent.ToString());
            }

            return result.ToString().Trim();
        }
  
        private string ProcessDeleteEvent(Command command)
        {
            int deletedEventsCount = this.eventManager.DeleteEventsByTitle(command.Parameters[0]);
            StringBuilder result = new StringBuilder();

            if (deletedEventsCount == 0)
            {
                result.Append("No events found");
                return result.ToString();
            }

            result.Append(deletedEventsCount).Append(" events deleted");

            return result.ToString();
        }
  
        private string ProcessAddEventCommand(Command command)
        {
            StringBuilder result = new StringBuilder();
            var date = DateTime.ParseExact(command.Parameters[0], "yyyy-MM-ddTHH:mm:ss",
                    CultureInfo.InvariantCulture);
            var calenderEvent = new CalendarEvent();

            calenderEvent.Date = date;
            calenderEvent.Title = command.Parameters[1];

            if (command.Parameters.Length == 2)
            {
                calenderEvent.Location = null;
            }
            else if (command.Parameters.Length == 3)
            {
                calenderEvent.Location = command.Parameters[2];
            }

            this.eventManager.AddEvent(calenderEvent);
            result.Append("Event added");

            return result.ToString();
        }
    }
}