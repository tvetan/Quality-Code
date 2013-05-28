using System;
using System.IO;
using CalendarSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalendarSystemTest
{
    [TestClass]
    public class CommandParseTest
    {
        [TestMethod]
        public void CommandParseNameChekingTest()
        {
            Command command = Command.Parse("AddEvent 2012-03-07T22:30:00 | party | Vitosha");
            string expected = command.Name;

            Assert.AreEqual(expected, "AddEvent");
        }

        [TestMethod]
        public void CommandParseParametersChekingTest()
        {
            Command command = Command.Parse("AddEvent 2012-03-07T22:30:00 | party | Vitosha");
            string[] expected = command.Parameters;
            string[] actual = { "2012-03-07T22:30:00", "party", "Vitosha" };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CommandParseAddEventWithoutLocationTest()
        {
            Command command = Command.Parse("AddEvent 2001-01-01T10:30:01 | EXAM - 197258");
            string[] expected = command.Parameters;
            string[] actual = { "2001-01-01T10:30:01", "EXAM - 197258", };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CommandParseListEventCommandNameTest()
        {
            Command command = Command.Parse("ListEvents 2012-03-31T23:59:59 | 8");

            string expected = "ListEvents";

            Assert.AreEqual(expected, command.Name);
        }

        [TestMethod]
        public void CommandParseListEventParametersTest()
        {
            Command command = Command.Parse("ListEvents 2012-03-31T23:59:59 | 8");

            string[] expected = command.Parameters;
            string[] actual = { "2012-03-31T23:59:59", "8", };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CommandParseEndCommandNameTest()
        {
            Command command = Command.Parse("End ");
            string expected = "End";
            
            Assert.AreEqual(command.Name, "End");
        }

        [TestMethod]
        public void CommandParseDeleteEventsParametersTest()
        {
            Command command = Command.Parse("DeleteEvents exam - 683989");

            string[] expected = command.Parameters;
            string[] actual = { "exam - 683989", };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CommandParseDeleteEventsNameTest()
        {
            Command command = Command.Parse("DeleteEvents exam - 683989");
            string expected = "DeleteEvents";

            Assert.AreEqual(expected, command.Name);
        }

        [TestMethod]
        public void FullOutputTest()
        {

            var output = new StringWriter();
            var input = new StringReader(@"AddEvent 2012-01-21T20:00:00 | party Viki | home
AddEvent 2012-03-26T09:00:00 | C# exam
AddEvent 2012-03-26T09:00:00 | C# exam
AddEvent 2012-03-26T08:00:00 | C# exam
AddEvent 2012-03-07T22:30:00 | party | Vitosha
ListEvents 2012-03-07T08:00:00 | 3
DeleteEvents c# exam
DeleteEvents My granny's bushes
ListEvents 2013-11-27T08:30:25 | 25
AddEvent 2012-03-07T22:30:00 | party | Club XXX
ListEvents 2012-01-07T20:00:00 | 10
AddEvent 2012-03-07T22:30:00 | Party | Club XXX
ListEvents 2012-03-07T22:30:00 | 3
End"
                );
            string expected = @"Event added
Event added
Event added
Event added
Event added
2012-03-07T22:30:00 | party | Vitosha
2012-03-26T08:00:00 | C# exam
2012-03-26T09:00:00 | C# exam
3 events deleted
No events found
No events found
Event added
2012-01-21T20:00:00 | party Viki | home
2012-03-07T22:30:00 | party | Club XXX
2012-03-07T22:30:00 | party | Vitosha
Event added
2012-03-07T22:30:00 | party | Club XXX
2012-03-07T22:30:00 | party | Vitosha
2012-03-07T22:30:00 | Party | Club XXX
";

            using (output)
            {
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    CalendarSystem.CallendarSystemDemo.Main();

                    string actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod]
        public void FullOutputTest1()
        {
            var output = new StringWriter();
            var input = new StringReader(@"AddEvent 2011-11-11T11:11:22 | party Viki | home
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:55 | party Viki | home
AddEvent 2011-11-11T11:11:33 | party Viki
AddEvent 2011-11-11T11:11:11 | party Viki | home
AddEvent 2011-11-11T11:11:44 | party Viki
AddEvent 2011-11-11T11:11:55 | party Viki
AddEvent 2011-11-11T11:11:33 | party Viki | home
AddEvent 2011-11-11T11:11:11 | party Viki
AddEvent 2011-11-11T11:11:44 | party Viki | home
ListEvents 2011-11-11T11:11:22 | 5
End"
                );
            string expected = @"Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki | home
2011-11-11T11:11:33 | party Viki
2011-11-11T11:11:33 | party Viki | home
2011-11-11T11:11:44 | party Viki
";

            using (output)
            {
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    CalendarSystem.CallendarSystemDemo.Main();

                    string actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod]
        public void FullOutputTest2()
        {
            var output = new StringWriter();
            var input = new StringReader(@"AddEvent 2010-01-01T00:00:00 | party Viki 2010
AddEvent 2010-01-01T00:00:00 | Party VIKI
AddEvent 2011-11-11T11:11:22 | party Viki | home
AddEvent 2011-11-11T11:11:22 | Party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:55 | party Viki | home
AddEvent 2011-11-11T11:11:33 | party Viki
AddEvent 2012-03-27T07:07:07 | party Kiro - 7 km
DeleteEvents party Viki
DeleteEvents party Viki
AddEvent 2011-11-11T11:11:11 | party Viki | home
AddEvent 2011-11-11T11:11:44 | party viki
AddEvent 2011-11-11T11:11:55 | Party Viki
AddEvent 2010-01-01T00:00:00 | party Viki 2010
AddEvent 2011-11-11T11:11:44 | PaRtY ViKi
AddEvent 2010-01-01T00:00:00 | Party VIKI
AddEvent 2011-11-11T11:11:55 | party VIKI
AddEvent 2011-11-11T11:11:55 | party Viki
AddEvent 2011-11-11T11:11:33 | party Viki | home
AddEvent 2011-11-11T11:11:11 | pArtY VikI
AddEvent 2011-11-11T11:11:44 | party Viki | home
AddEvent 2011-11-11T11:11:44 | party Viki | alpha
AddEvent 2011-11-11T11:11:22 | PARTY VIKI
AddEvent 2011-11-11T11:11:22 | Party Viki
AddEvent 2011-11-11T11:11:22 | party 11:22 @ Mimi
ListEvents 2011-11-11T11:11:22 | 10
ListEvents 2011-11-11T11:11:22 | 5
AddEvent 2010-01-01T00:00:00 | party Viki 2010
AddEvent 2010-01-01T00:00:00 | Party VIKI
AddEvent 2011-11-11T11:11:22 | party Viki | home
AddEvent 2011-11-11T11:11:22 | Party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:55 | party Viki | home
AddEvent 2011-11-11T11:11:33 | party Viki
AddEvent 2012-03-27T07:07:07 | party Kiro - 7 km
AddEvent 2010-01-01T00:00:00 | party Viki 2010
AddEvent 2010-01-01T00:00:00 | Party VIKI
AddEvent 2011-11-11T11:11:22 | party Viki | home
AddEvent 2011-11-11T11:11:22 | Party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:55 | party Viki | home
AddEvent 2011-11-11T11:11:33 | party Viki
AddEvent 2012-03-27T07:07:07 | party Kiro - 7 km
AddEvent 2010-01-01T00:00:00 | party Viki 2010
AddEvent 2010-01-01T00:00:00 | Party VIKI
AddEvent 2011-11-11T11:11:22 | party Viki | home
AddEvent 2011-11-11T11:11:22 | Party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:55 | party Viki | home
AddEvent 2011-11-11T11:11:33 | party Viki
AddEvent 2012-03-27T07:07:07 | party Kiro - 7 km
AddEvent 2010-01-01T00:00:00 | party Viki 2010
AddEvent 2010-01-01T00:00:00 | Party VIKI
AddEvent 2011-11-11T11:11:22 | party Viki | home
AddEvent 2011-11-11T11:11:22 | Party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:22 | party Viki
AddEvent 2011-11-11T11:11:55 | party Viki | home
AddEvent 2011-11-11T11:11:33 | party Viki
AddEvent 2012-03-27T07:07:07 | party Kiro - 7 km
AddEvent 2011-11-11T11:11:11 | party Viki | home
AddEvent 2011-11-11T11:11:44 | party viki
AddEvent 2011-11-11T11:11:55 | Party Viki
AddEvent 2010-01-01T00:00:00 | party Viki 2010
AddEvent 2011-11-11T11:11:44 | PaRtY ViKi
AddEvent 2010-01-01T00:00:00 | Party VIKI
AddEvent 2011-11-11T11:11:55 | party VIKI
AddEvent 2011-11-11T11:11:55 | party Viki
AddEvent 2011-11-11T11:11:33 | party Viki | home
AddEvent 2011-11-11T11:11:11 | pArtY VikI
AddEvent 2011-11-11T11:11:44 | party Viki | home
AddEvent 2011-11-11T11:11:44 | party Viki | alpha
AddEvent 2011-11-11T11:11:22 | PARTY VIKI
AddEvent 2011-11-11T11:11:22 | Party Viki
AddEvent 2011-11-11T11:11:22 | party 11:22 @ Mimi
AddEvent 2011-11-11T11:11:11 | party Viki | home
AddEvent 2011-11-11T11:11:44 | party viki
AddEvent 2011-11-11T11:11:55 | Party Viki
AddEvent 2010-01-01T00:00:00 | party Viki 2010
AddEvent 2011-11-11T11:11:44 | PaRtY ViKi
AddEvent 2010-01-01T00:00:00 | Party VIKI
AddEvent 2011-11-11T11:11:55 | party VIKI
AddEvent 2011-11-11T11:11:55 | party Viki
AddEvent 2011-11-11T11:11:33 | party Viki | home
AddEvent 2011-11-11T11:11:11 | pArtY VikI
AddEvent 2011-11-11T11:11:44 | party Viki | home
AddEvent 2011-11-11T11:11:44 | party Viki | alpha
AddEvent 2011-11-11T11:11:22 | PARTY VIKI
AddEvent 2011-11-11T11:11:22 | Party Viki
AddEvent 2011-11-11T11:11:22 | party 11:22 @ Mimi
ListEvents 2011-11-11T11:11:22 | 100
ListEvents 2000-01-01T00:00:00 | 5
ListEvents 2020-01-01T00:00:00 | 5
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
DeleteEvents party Viki
ListEvents 2011-11-11T11:11:22 | 5
End
"
                );
            string expected = @"Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
7 events deleted
No events found
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
2011-11-11T11:11:22 | party 11:22 @ Mimi
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | PARTY VIKI
2011-11-11T11:11:33 | party Viki | home
2011-11-11T11:11:44 | party viki
2011-11-11T11:11:44 | party Viki | alpha
2011-11-11T11:11:44 | party Viki | home
2011-11-11T11:11:44 | PaRtY ViKi
2011-11-11T11:11:55 | party Viki
2011-11-11T11:11:55 | party VIKI
2011-11-11T11:11:22 | party 11:22 @ Mimi
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | PARTY VIKI
2011-11-11T11:11:33 | party Viki | home
2011-11-11T11:11:44 | party viki
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
Event added
2011-11-11T11:11:22 | party 11:22 @ Mimi
2011-11-11T11:11:22 | party 11:22 @ Mimi
2011-11-11T11:11:22 | party 11:22 @ Mimi
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki
2011-11-11T11:11:22 | party Viki | home
2011-11-11T11:11:22 | party Viki | home
2011-11-11T11:11:22 | party Viki | home
2011-11-11T11:11:22 | party Viki | home
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | Party Viki
2011-11-11T11:11:22 | PARTY VIKI
2011-11-11T11:11:22 | PARTY VIKI
2011-11-11T11:11:22 | PARTY VIKI
2011-11-11T11:11:33 | party Viki
2011-11-11T11:11:33 | party Viki
2011-11-11T11:11:33 | party Viki
2011-11-11T11:11:33 | party Viki
2011-11-11T11:11:33 | party Viki | home
2011-11-11T11:11:33 | party Viki | home
2011-11-11T11:11:33 | party Viki | home
2011-11-11T11:11:44 | party viki
2011-11-11T11:11:44 | party viki
2011-11-11T11:11:44 | party viki
2011-11-11T11:11:44 | party Viki | alpha
2011-11-11T11:11:44 | party Viki | alpha
2011-11-11T11:11:44 | party Viki | alpha
2011-11-11T11:11:44 | party Viki | home
2011-11-11T11:11:44 | party Viki | home
2011-11-11T11:11:44 | party Viki | home
2011-11-11T11:11:44 | PaRtY ViKi
2011-11-11T11:11:44 | PaRtY ViKi
2011-11-11T11:11:44 | PaRtY ViKi
2011-11-11T11:11:55 | party Viki
2011-11-11T11:11:55 | party Viki
2011-11-11T11:11:55 | party Viki
2011-11-11T11:11:55 | party Viki | home
2011-11-11T11:11:55 | party Viki | home
2011-11-11T11:11:55 | party Viki | home
2011-11-11T11:11:55 | party Viki | home
2011-11-11T11:11:55 | party VIKI
2011-11-11T11:11:55 | party VIKI
2011-11-11T11:11:55 | party VIKI
2011-11-11T11:11:55 | Party Viki
2011-11-11T11:11:55 | Party Viki
2011-11-11T11:11:55 | Party Viki
2012-03-27T07:07:07 | party Kiro - 7 km
2012-03-27T07:07:07 | party Kiro - 7 km
2012-03-27T07:07:07 | party Kiro - 7 km
2012-03-27T07:07:07 | party Kiro - 7 km
2012-03-27T07:07:07 | party Kiro - 7 km
2010-01-01T00:00:00 | Party VIKI
2010-01-01T00:00:00 | Party VIKI
2010-01-01T00:00:00 | Party VIKI
2010-01-01T00:00:00 | Party VIKI
2010-01-01T00:00:00 | Party VIKI
No events found
67 events deleted
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
No events found
2011-11-11T11:11:22 | party 11:22 @ Mimi
2011-11-11T11:11:22 | party 11:22 @ Mimi
2011-11-11T11:11:22 | party 11:22 @ Mimi
2012-03-27T07:07:07 | party Kiro - 7 km
2012-03-27T07:07:07 | party Kiro - 7 km
";

            using (output)
            {
                using (input)
                {
                    Console.SetIn(input);
                    Console.SetOut(output);

                    CalendarSystem.CallendarSystemDemo.Main();

                    string actual = output.ToString();
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}
