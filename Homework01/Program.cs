//MakeChange.cs converts whole dollar amounts into change equivalents using the command line
//Jonathan Groberg 5/26/22
using static System.Console;
using System;

class MakeChange
{
    //text display when the program starts
    private const string WELCOME_TEXT = "Welcome to the Change Maker!\n" +
        "We convert whole dollar amounts to their corresponding change equivalents!\n" +
        "For example, $113 is 5 twenties, 1 ten, 0 fives, and 3 ones.\n" +
        "\n--- please do not include the dollar sign symbol ($) in your responses ---";

     static void Main()
    {
        WriteLine(WELCOME_TEXT);

        StartDialogue();
    }

    // controls the dialogue logic
    private static void StartDialogue()
    {
        //reads and converts user input into a new integer variable
        WriteLine("\nPlease enter the dollar amount you wish to covert below ↓\n");
        int dollarAmount = Convert.ToInt32(ReadLine());

        //we dont want to work with negitive numbers
        if (dollarAmount < 0)
        {
            WriteLine("Sorry, we dont work with loans or debt. Let's try again!");
            StartDialogue();
        }
        else
        {
            //reads and converts user response to a boolean, then takes the appropiate action based on user response
            bool isNotCorrect = !PromptUser($"\nIs ${dollarAmount} the correct amount that you wish to convert?");
            if (isNotCorrect)
            {
                WriteLine("OK, lets try again...\n");
                StartDialogue();
            }
            else
            {
                WriteLine("\nAwesome, lets continue! \nConverting {0} to change...\n", dollarAmount);
                Change change = ConvertToChange(dollarAmount);
                WriteLine("OK, so it looks like ${0} has been converted into {1} twenties, {2} tens, {3} fives, and {4} ones", dollarAmount, change.twenties, change.tens, change.fives, change.ones);
            }

            //after conversion we ask the user if they are done, either running again or exiting the program.
            bool doMore = PromptUser("\nDo you want to do more conversions?");
            if (doMore)
            {
                StartDialogue();
            }
            else
            {
                WriteLine("\nNo problem! Thanks for testing our application");
                System.Environment.Exit(1);
            }
        }
    }

    //if the user types y then return true, if not y then return false
    private static Boolean PromptUser(string prompt)
    {
        WriteLine(prompt);
        WriteLine("(y/n)");
        bool yOrN = ReadLine() == "y";
        return yOrN;
    }

    //controls the math logic
    private static Change ConvertToChange(int dollarsLeft)
    {
        Change change = new Change();

        //because c# appears get rid of fractions while doing division on integers we can figure out how many $x bills go into $y
        //for example five $20 bills go into $110 (110/20 = 5)
        //does modular math do figure out how many dollars are left over
        change.twenties = dollarsLeft / 20;
        dollarsLeft %= 20;
        change.tens = dollarsLeft / 10;
        dollarsLeft %= 10;
        change.fives = dollarsLeft / 5;
        dollarsLeft %= 5;
        change.ones = dollarsLeft / 1;
        //x mod 1 is always 0 so we do not calculate it

        return change;
    }


    //using a struct we can pass around named values like twenties,tens... whereas an array values are named 0,1,2....
    private struct Change
    {
        public int twenties, tens, fives, ones;
    }
}
