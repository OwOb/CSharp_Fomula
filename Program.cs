using System;
using FormulaLib;

class Program
{
    static Formula fml = new Formula("sin(-cos(t)^3-1.5)/t+2");
    static void Main(string[] args)
    {
        Console.WriteLine(fml.Calculate(2.0));
        Console.WriteLine(fml.OperatorCount());
    }
}
