using static System.Console;

internal static class IntegerFacts
{
    private static void Main(string[] args)
    {
        int[] intArray          = new int[10];
        int   actualArrayLength = 0;

        WriteLine("Welcome to the Integer Fact Machine\nWe provide statiics regarding the integers you provide.\nYou may input up to 10 integers.");

        FillArray(ref actualArrayLength, ref intArray);
        GetStats(actualArrayLength, intArray, out int highest, out int lowest, out int sum, out double average);

        if (actualArrayLength > 0)
            WriteLine("\n-Statistics-\nhighest value : {0}\nlowest value : {1}\nsum : {2}\narithmetic average : {3}\n",
                      highest, lowest, sum, average);
        else WriteLine("You did not enter any integers!");

        WriteLine("Thank you for using the Integer Fact Machine");
    }

    // do the statistics and send to back using out values
    private static void GetStats(int actualArrayLength, int[] array, out int highest, out int lowest, out int sum,
        out double                   average)
    {
        average = sum    = 0;        //set both to 0
        highest = lowest = array[0]; // set both to element 1
        foreach (int element in array)
        {
            highest =  element > highest ? element : highest;
            lowest  =  element < lowest ? element : lowest;
            sum     += element;
        }

        if (actualArrayLength > 0) average = (double)sum / actualArrayLength; // so we dont divide by 0
    }

    //ask the user for up to 10(size of the array) values and put them into the array
    private static void FillArray(ref int length, ref int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            WriteLine("Would you like to input an integer? y or n...");

            if (IsValidChar(out char response) && response == 'y')
            {
                length   = i + 1; // length of array with x elements has a length of x + 1
                array[i] = PromptInt();
            }
            else break;
        }
    }

    //prompt user for an int value: if they provide a actual value return it, else ask again
    private static int PromptInt()
    {
        WriteLine("Please input an integer");

        return IsValidInt(out int integer) ? integer : PromptInt();
    }

    //read user input and run a tryparse method to check if the value is a valid int and char type
    private static bool IsValidChar(out char response) => char.TryParse(ReadLine(), out response);
    private static bool IsValidInt(out  int  response) => int.TryParse(ReadLine(), out response);
