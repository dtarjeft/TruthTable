using System;
using System.Collections.Generic;
using Sprache;

namespace Lab02
{
    static class SetParser
    {
        public static List<Set> Sets { get; set; }

        public static string ParseProblem(string problem)
        {
            var inputStr = problem.Replace(" ", "");
            var result = ExpressionParser.Parse(inputStr);
            return result.Members.Count == 0 ? "{null}" : result.ToString();
        }

        private static readonly Parser<Func<Set, Set, Set>> Union =
            Parse.String("U").Return<IEnumerable<char>, Func<Set, Set, Set>>((a, b) => b * a);

        private static readonly Parser<Func<Set, Set, Set>> Intersection =
            Parse.String("n").Return<IEnumerable<char>, Func<Set, Set, Set>>((a, b) => a + b);

        private static readonly Parser<Func<Set, Set, Set>> Subtract =
            Parse.String("–").Return<IEnumerable<char>, Func<Set, Set, Set>>((a, b) => a - b);

        private static readonly Parser<Func<Set, Set, Set>> Operations =
            Union.Or(Intersection.Or(Subtract));

        private static readonly Parser<Set> Parenthesis =
            from count in Parse.Char('N').Optional()
            from inverse in Parse.Char('!').Optional()
            from expression in Parse.Ref(() => ExpressionParser.Contained(Parse.Char('('), Parse.Char(')')))
            select count.IsDefined ? new Set(new List<int>{expression.Members.Count}) : inverse.IsDefined ? !expression : expression;

        private static readonly Parser<Set> Set =
            from inverse in Parse.Char('!').Optional()
            from letter in Parse.CharExcept(new[] { 'N', '('})
            select inverse.IsDefined ? !Sets[letter - 'A'] : Sets[letter - 'A'];

        private static readonly Parser<Set> ExpressionParser = Parse.ChainOperator(Operations, Parenthesis.Or(Set),
            (op, arg1, arg2) => op(arg1, arg2));



    }
}