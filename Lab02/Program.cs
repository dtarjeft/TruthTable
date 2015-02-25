using System;
using System.Collections.Generic;

namespace Lab02
{
    class Program
    {
        private static void TestTruthTable(TruthTable truthTable)
        {
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

        static void Main()
        {
            //TestTruthTable(new TruthTable(3));
            SetParser.Sets = new List<Set>
            {
                new Set(new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}),
                new Set(new List<int> {2, 4, 6, 8, 10}),
                new Set(new List<int> {1, 3, 5, 7, 9}),
                new Set(new List<int> {1, 2, 3, 5, 7})
            };

            var problems = new List<string>
            {
                "A n D",
                ("( B U C ) n  A"),
                ("(!C U  C) n  A"),
                ("A – D"),
                ("N(!A U ( C U  D))"),
                ("B n C"),
                ("N(B n  C)"),
                ("A U  B U  C U  D")
            };
            Console.WriteLine("Sets:");
            for (var i = 0; i < SetParser.Sets.Count; i++)
            {
                var set = SetParser.Sets[i];
                Console.WriteLine((char)(i+'A')+ ": " +set.ToString());
            }
            foreach (var problem in problems)
            {
                Console.WriteLine(problem + ": " + SetParser.ParseProblem(problem));
            }
        }
    }
}
