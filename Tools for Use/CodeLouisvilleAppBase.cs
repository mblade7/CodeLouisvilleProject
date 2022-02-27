using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeLouisvilleLibrary
{

    public abstract class CodeLouisvilleAppBase
    {
        protected string AppName { get; private set; }
        protected DateTime StartDate { get; private set; }
        protected DateTime EndDate { get; private set; }

        public CodeLouisvilleAppBase(string appName)
        {
            AppName = appName;
        }

        protected abstract bool Main();

        public void Run()
        {
            StartDate = DateTime.Now;

            Welcome();

            Console.Clear();

            bool Continue = true;
            while(Continue)
            {
                Continue = Main();
            }

            EndDate = DateTime.Now;
            Goodbye();
        }

        protected virtual void Welcome()
        {
            Console.Write($"Welcome to {AppName}!  Press any key to continue...");
            Console.ReadKey();
        }

        protected virtual void Goodbye()
        {
            Console.WriteLine($"\nYou spent {CreateTimeSpentString(StartDate, EndDate)} using {AppName}.");
            Console.WriteLine("I hope you enjoyed it and will come back again.");
        }

        protected string CreateTimeSpentString(DateTime startTime, DateTime endTime)
        {
            TimeSpan timeSpent = endTime - startTime;

            List<string> timeSpentStringParts = new List<string>();
            if (timeSpent.Days > 0)
                timeSpentStringParts.Add($"{timeSpent.Days} Day{(timeSpent.Days > 1 ? "s" : "")}");
            if (timeSpent.Hours > 0)
                timeSpentStringParts.Add($"{timeSpent.Hours} Hour{(timeSpent.Hours > 1 ? "s" : "")}");
            if (timeSpent.Minutes > 0)
                timeSpentStringParts.Add($"{timeSpent.Minutes} Minute{(timeSpent.Minutes > 1 ? "s" : "")}");
            if (timeSpent.Seconds > 0)
                timeSpentStringParts.Add($"{timeSpent.Seconds} Second{(timeSpent.Seconds > 1 ? "s" : "")}");
            if (timeSpent.Milliseconds > 0)
                timeSpentStringParts.Add($"{timeSpent.Milliseconds} Millisecond{(timeSpent.Milliseconds > 1 ? "s" : "")}");

            StringBuilder timeSpentString = new StringBuilder("");

            if (timeSpentStringParts.Count > 0)
            {
                timeSpentString.Append(timeSpentStringParts[0]);

                for (int i = 1; i < timeSpentStringParts.Count; i++)
                {
                    if (i == timeSpentStringParts.Count - 1) // if the last entry in the list
                        timeSpentString.Append($" and {timeSpentStringParts[i]}");
                    else
                        timeSpentString.Append($", {timeSpentStringParts[i]}");
                }
            }
            return timeSpentString.ToString();
        }

        #region "Utility" methods. 

        public static int Prompt4Integer(string prompt)
        {
            int value;

            do
            {
                Console.Write(prompt);
            }
            while (!int.TryParse(Console.ReadLine(), out value));

            return value;
        }
        public static double Prompt4Double(string prompt)
        {
            double value;

            do
            {
                Console.Write(prompt);
            }
            while (!double.TryParse(Console.ReadLine(), out value));

            return value;
        }

        public static bool Prompt4YesNo(string prompt)
        {
            string userInput = "";

            do
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();
            } while (userInput.ToUpper() != "Y" && userInput.ToUpper() != "N");

            return userInput.ToUpper() == "Y";
        }

        public static T Prompt4MenuItem<T>(string prompt, List<KeyValuePair<T, string>> menu)
        {
            Console.WriteLine(prompt);
            // this is the menu
            foreach (KeyValuePair<T, string> menuItem in menu)
            {
                Console.WriteLine($"\t{menuItem.Key.ToString()}: {menuItem.Value}");
            }
            Console.Write("Selection: ");
            string userSelection = Console.ReadLine();

            foreach (KeyValuePair<T, string> menuitem in menu)
            {
                if (menuitem.Key.ToString().ToUpper() == userSelection.ToUpper())
                    return menuitem.Key;
            }

            return default(T);
        }

        public static T Prompt4MenuItem<T>(string prompt, Menu<T> menu)
        {
            return Prompt4MenuItem(prompt, menu.MenuItems);
        }

        public static string Prompt4MenuItem(string prompt, Menu menu)
        {
            return Prompt4MenuItem(prompt, menu.MenuItems);
        }

        public static bool TryPrompt4Integer(out int value, string prompt = "", uint maxAttempts = 0, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentException("minValue must be <= maxValue");

            if (string.IsNullOrWhiteSpace(prompt))
            {
                StringBuilder newPrompt = new StringBuilder("Enter a number");
                if (minValue != int.MinValue)
                    newPrompt.Append($" >= {minValue}");
                if (minValue != int.MinValue && maxValue != int.MaxValue)
                    newPrompt.Append(" and");
                if (maxValue != int.MaxValue)
                    newPrompt.Append($" <= {maxValue}");
                if (maxAttempts > 0)
                    newPrompt.Append($" (You will get {maxAttempts} tries)");
                newPrompt.Append(": ");
                prompt = newPrompt.ToString();
            }

            bool success = false;
            uint attempt = 0;
            bool quit = false;

            do
            {
                if (maxAttempts > 0) // if user has a limited number of attempts
                {
                    attempt++;
                    WriteRetryPrompt(attempt, maxAttempts);
                }

                Console.Write(prompt);

                success = int.TryParse(Console.ReadLine(), out value);
                if (!success)
                    Console.WriteLine("Entry is not a number.");
                else if (value < minValue || value > maxValue)
                {
                    // they entered a number but it's outside of the range
                    // we wanted
                    success = false;
                    Console.WriteLine($"Input must be between {minValue} and {maxValue}");
                }

                // quit when they've entered a number like we've asked
                // OR they've exceeded the maximum number of allowed attempts
                quit = success || attempt >= maxAttempts;

                if (!quit) Console.WriteLine();

            } while (!quit);

            return success;
        }

        public static bool TryPrompt4YesNo(string prompt, out bool response, uint maxAttempts = 0, string yesResponse = "Y", string noResponse = "N")
        {
            if (string.IsNullOrWhiteSpace(prompt))
                prompt = $"Please answer Yes ({yesResponse}) or No ({noResponse}): ";

            bool success = false;
            uint attempt = 0;
            bool quit = false;
            response = false;

            do
            {
                if (maxAttempts > 0) // if user has a limited number of attempts
                {
                    attempt++;
                    WriteRetryPrompt(attempt, maxAttempts);
                }
                Console.Write(prompt);
                string userInput = Console.ReadLine();
                if (userInput.ToUpper() == yesResponse.ToUpper() || userInput.ToUpper() == noResponse.ToUpper())
                {
                    success = true;
                    response = (userInput.ToUpper() == yesResponse.ToUpper());
                }
                else
                {
                    Console.WriteLine($"Invalid input.  Please enter {yesResponse} (Yes) or {noResponse} (No).");
                }

                // quit when they've selected a menu option like we've asked
                // OR they've exceeded the maximum number of allowed attempts
                quit = success || attempt >= maxAttempts;

                if (!quit) Console.WriteLine();

            } while (!quit);

            return success;
        }

        public static bool TryPrompt4MenuItem<T>(string prompt, List<KeyValuePair<T, string>> menu, out T menuSelection, uint maxAttempts = 0)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                prompt = "Please select one of the following options:";

            bool success = false;
            uint attempt = 0;
            bool quit = false;
            menuSelection = default(T);

            do
            {
                if (maxAttempts > 0) // if user has a limited number of attempts
                {
                    attempt++;
                    WriteRetryPrompt(attempt, maxAttempts);
                }

                Console.WriteLine(prompt);
                // this is the menu
                foreach (KeyValuePair<T, string> menuItem in menu)
                {
                    Console.WriteLine($"\t{menuItem.Key.ToString()}: {menuItem.Value}");
                }
                Console.Write("Selection: ");
                string userSelection = Console.ReadLine();

                if (menu.Any(mi => mi.Key.ToString().ToUpper() == userSelection.ToUpper()))
                {
                    success = true;
                    menuSelection = menu.FirstOrDefault(mi => mi.Key.ToString() == userSelection.ToUpper()).Key;
                }
                else
                    Console.WriteLine($"{userSelection} is not an available option");

                // quit when they've selected a menu option like we've asked
                // OR they've exceeded the maximum number of allowed attempts
                quit = success || attempt >= maxAttempts;

                if (!quit) Console.WriteLine();

            } while (!quit);

            return success;
        }

        public static bool TryPrompt4MenuItem<T>(string prompt, Menu<T> menu, out T menuSelection, uint maxAttempts = 0)
        {
            return TryPrompt4MenuItem(prompt, menu.MenuItems, out menuSelection, maxAttempts);
        }

        public static void Animate(string[] parts, int pause = 500)
        {
            string prev = "";
            for(int i = 0; i < parts.Length; i++)
            {
                Console.SetCursorPosition(Console.CursorLeft - prev.Length, Console.CursorTop);
                Console.Write(parts[i].PadRight(prev.Length));
                prev = parts[i].PadRight(prev.Length);

                Thread.Sleep(pause);
            }
        }

        private static void WriteRetryPrompt(uint attempt, uint maxAttempts)
        {
            if (maxAttempts > 0 && attempt <= maxAttempts)
            {
                if (maxAttempts == 1)
                    Console.WriteLine("You only get one try.");
                else if (attempt > 1)
                {
                    if (maxAttempts - attempt == 0)
                        Console.WriteLine("This is your last try.");
                    else
                        Console.WriteLine($"You have {maxAttempts - attempt + 1} attempts remaining.");
                }
            }
        }
        #endregion
    }
}
