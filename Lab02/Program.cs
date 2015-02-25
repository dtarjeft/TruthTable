using System;

namespace Lab02
{
    class Program
    {
        static void Main()
        {
            var truthTable = new TruthTable(3);
            Console.WriteLine("Truth Table in its entirety:");
            truthTable.ToConsole();
            Console.WriteLine();
            truthTable.EvaluateExpression("(A and B) or (A and C)");
            truthTable.EvaluateExpression("(A and C) and (B and !C)");
            truthTable.EvaluateExpression("(A or B) and !(B or C)");
            truthTable.EvaluateExpression("(A or (B and C)) and (!A and !B)");
            truthTable.EvaluateExpression("(B and C) or (C and A)) and (!(A or B) and C)");
            truthTable.EvaluateExpression("A or !(B and C)");
        }
    }
}
