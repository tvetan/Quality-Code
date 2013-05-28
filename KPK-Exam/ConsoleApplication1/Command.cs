using System;

namespace CalendarSystem
{
    public struct Command
    {
        public string Name;

        public string[] Parameters { get; set; }

        public static Command Parse(string command)
        {
            int whiteSpaceIndex = command.IndexOf(' ');
            if (whiteSpaceIndex == -1)
            {
                throw new ArgumentException("The command cannot be parsed because there was no white space.");
            }

            string commandName = command.Substring(0, whiteSpaceIndex);
            string extractedArguments = command.Substring(whiteSpaceIndex + 1);

            var commandArguments = extractedArguments.Split('|');
            TrimParameters(commandArguments);

            var createdCommand = new Command
            {
                Name = commandName,
                Parameters = commandArguments
            };

            return createdCommand;
        }
  
        private static void TrimParameters(string[] commandArguments)
        {
            for (int index = 0; index < commandArguments.Length; index++)
            {
                string swapValue = commandArguments[index].Trim();
                commandArguments[index] = swapValue;
            }
        }
    }
}