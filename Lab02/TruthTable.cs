using System;
using System.Collections.Generic;
using Sprache;

namespace Lab02
{
    //TODO: Make this not a Console project.
    class TruthTable
    {
        public static bool[,] Table { get; private set; }
        private readonly int _variableCount;
        public TruthTable(int variableCount)
        {
            _variableCount = variableCount;
            _buildTable();
        }

        private void _buildTable()
        {
            var rows = (int)Math.Pow(2, _variableCount);
            Table = new bool[rows, _variableCount];
            var countCopy = _variableCount - 1;
            for (var j = 0; j < _variableCount; j++)
            {
                var tablePopulator = false;
                for (var i = 0; i < rows; i += (int)Math.Pow(2, countCopy))
                {
                    tablePopulator = !tablePopulator;
                    if (!tablePopulator) continue;
                    for (var k = 0; k < (int)Math.Pow(2, countCopy); k++)
                    {
                        Table[i + k, j] = true;
                    }
                }
                countCopy--;
            }
        }

        private static int RowToLookUp { get; set; }

        public void EvaluateExpression(string expression)
        {
            var resultInts = new List<int>();
            Console.WriteLine("Parsing {0}", expression);
            expression = expression.Replace(" ", "");
            for (var i = 0; i < Math.Pow(2,_variableCount); i++)
            {
                RowToLookUp = i;
                if (ExpressionParser.Parse(expression))
                {
                    resultInts.Add(i);
                }
            }

            if (resultInts.Count <= 0)
            {
                Console.WriteLine("Expression is never true.\n");
                return;
            }
            Console.WriteLine("Expression is true in {0} cases: ", resultInts.Count);
            PrintLabels();
            foreach (var resultInt in resultInts)
            {
                _singleRowToConsole(resultInt);
            }
            Console.WriteLine();
        }

        private static readonly Parser<bool> Parenthetical =
            from negative in Parse.Char('!').Optional()
            from expression in Parse.Ref(() => ExpressionParser.Contained(Parse.Char('('), Parse.Char(')')))
            select negative.IsDefined ? !expression : expression;
        private static readonly Parser<Func<bool, bool, bool>> And =
            Parse.String("and").Return<IEnumerable<char>, Func<bool, bool, bool>>((a, b) => a && b);
        private static readonly Parser<Func<bool, bool, bool>> Or = 
            Parse.String("or").Return<IEnumerable<char>, Func<bool, bool, bool>>((a, b) => a || b);

        private static readonly Parser<bool> BoolParser =
            from negative in Parse.Char('!').Optional()
            from letter in Parse.Letter
            select (negative.IsDefined ? !Table[RowToLookUp, letter - 'A'] : Table[RowToLookUp, letter - 'A']);

        private static readonly Parser<Func<bool, bool, bool>> AndAndOrParser = 
            And.Or(Or);

        private static readonly Parser<bool> ExpressionParser = Parse.ChainOperator(AndAndOrParser,
            BoolParser.Or(Parenthetical), (op, arg1, arg2) => op(arg1, arg2));
         



        private void PrintLabels()
        {
            for (var k = 0; k < _variableCount; k++)
            {
                Console.Write("{0,7}", (char)(k + 'A'));
            }
            Console.WriteLine();
        }

        public void ToConsole()
        {
            PrintLabels();
            for (var i = 0; i < (int) Math.Pow(2, _variableCount); i++)
            {
                _singleRowToConsole(i);
            }
        }

        private void _singleRowToConsole(int row)
        {
            for (var j = 0; j < _variableCount; j++)
            {
                Console.Write("{0,7}", Table[row, j]);
            }
            Console.WriteLine();
        }

        public void SingleRowToConsole(int row)
        {
            PrintLabels();
            _singleRowToConsole(row);
        }
    }
}