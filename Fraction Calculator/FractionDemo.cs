//FractionDemo.cs is a Console Application that instantiates several Fraction objects and demonstrates that their methods work correctly.
//The Fraction class contains fields that hold a whole number, a numerator, and a denominator and has constructors that can be instantiated with any none, some, or all corresponding parameters.
//Jonathan Groberg  6/20/22

using static System.Console;

class FractionDemo
{
    static void Main()
    {
        Fraction[] exampleFractions = { new Fraction(4, 15, 5), new Fraction(6, 2), new Fraction(0, 5, 15), new Fraction() };

        WriteLine("Here are 4 mixed and non-mixed fractions:\n");
        foreach (Fraction fraction in exampleFractions)
        {
            WriteLine(fraction);
        }

        WriteLine("\nHere is what happens when we simplify each fraction: ");
        foreach (Fraction fraction in exampleFractions)
        {
            WriteLine($"{fraction} = {fraction.Reduce()}");
        }

        WriteLine("\nWhat happens when we add them together?");
        WriteLine($"{exampleFractions[0]} + {exampleFractions[1]} = {exampleFractions[0] + exampleFractions[1]}");
        WriteLine($"{exampleFractions[1]} + {exampleFractions[2]} = {exampleFractions[1] + exampleFractions[2]}");
        WriteLine($"{exampleFractions[2]} + {exampleFractions[3]} = {exampleFractions[2] + exampleFractions[3]}\n");
    }
}

//Fractions can be reduced, added, and also contain a ToString implementation the returns a Fraction in the usual display format—the whole number, a space, the numerator, a slash (/), and a denominator
//Fractions can be created by inputs the whole number and fraction, only the fraction, or empty eg : Fraction(0,2,3) == Fraction (2,3)
class Fraction
{
    private int denominator;
    public int WholeNumber { get; set; }
    public int Numerator { get; set; }
    public int Denominator { get { return denominator; } set { denominator = value == 0 ? 1 : value; } } // Denominator does not allow for a value of 0, defaults to 1

    public Fraction(int whole, int num, int dem)
    {
        WholeNumber = whole;
        Numerator = num;
        Denominator = dem;
    }
    public Fraction(int num, int dem) : this(0, num, dem)
    {
    }
    public Fraction() : this(0, 0, 0)
    {
    }

    // instantiates 2 new fractions so we can avoid changing the orignals, and a third to give us the final result.
    public static Fraction operator +(Fraction first, Fraction second)
    {
        Fraction fraction1 = new Fraction(first.WholeNumber, first.Numerator, first.Denominator).Reduce();
        Fraction fraction2 = new Fraction(second.WholeNumber, second.Numerator, second.Denominator).Reduce();

        int commonDenom = fraction1.Denominator * fraction2.Denominator;
        fraction1.Numerator *= fraction2.Denominator;
        fraction2.Numerator *= fraction1.Denominator;

        Fraction result = new Fraction(fraction1.WholeNumber + fraction2.WholeNumber, fraction1.Numerator + fraction2.Numerator, commonDenom);

        return result.Reduce();
    }

    //returns a copy of the object that has been reduced
    public Fraction Reduce()
    {
        Fraction fraction = new Fraction(WholeNumber, Numerator, Denominator).ToNonMixedFraction();
        int theGCD = FindGCD(Numerator, Denominator);
        fraction.Numerator /= theGCD;
        fraction.Denominator /= theGCD;

        return fraction.ToMixedFraction();
    }

    //uses recursion to find the greatest common denominator
    //when a mod b is 0, it signifies tha we found the GCD
    public int FindGCD(int a, int b)
    {
        return a % b == 0 ? b : FindGCD(b, a % b);
    }


    public Fraction ToNonMixedFraction()
    {
        if (WholeNumber != 0)
        {
            Numerator += WholeNumber * Denominator;
            WholeNumber = 0;
        }
        return this; // allows us to do: newFraction = otherfraction.ToNonMixedFraction()
    }

    public Fraction ToMixedFraction()
    {
        if (WholeNumber == 0)
        {
            WholeNumber = Numerator / Denominator;
            Numerator -= WholeNumber * Denominator;
        }
        return this;
    }

    //If both numerator and whole number are 0, we display 0 since there is no value
    //If the whole number is 0, just the Fraction part of the value should be displayed (for example, 1/2 instead of 0 1/2).
    //If the numerator is 0, just the whole number should be displayed (for example, 2 instead of 2 0/3).
    public override string ToString()
    {
        if (WholeNumber == 0) return $"{Numerator}/{Denominator}";
        else if (Numerator == 0) return $"{WholeNumber}";
        else return $"{WholeNumber} {Numerator}/{Denominator}";
    }

}
