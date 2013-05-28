using System;

namespace CalendarSystem
{
    public class CallendarSystemDemo
    {
        public static void Main()
        {
            var eventManager = new EventsManager();
            var eventManagerFast = new EventsManagerFast();
            var calendarEventProcessor = new CalendarEventProcessor(eventManagerFast);

            while (true)
            {
                string inputLine = Console.ReadLine();
                if (inputLine == "End" || inputLine == null)
                {
                    break;
                }

                var processedCommand = calendarEventProcessor.ProcessCommand(Command.Parse(inputLine));
                Console.WriteLine(processedCommand);
            }
        }
    }
}