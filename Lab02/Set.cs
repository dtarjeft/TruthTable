using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace Lab02
{
    public class Set
    {
        public List<int> Members { get; set; }
        private static List<int> FullSet { get; set; }

        public Set(List<int> members = null)
        {
            Members = members;
            FullSet = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10
            }
            ;
        }

        /// <summary>
        /// Intersection
        /// </summary>
        public static Set operator +(Set s1, Set s2)
        {
            var resultSet = new Set(new List<int>());
            foreach (var member in s1.Members.Where(member => s2.Members.Contains(member)))
            {
                resultSet.Members.Add(member);
            }

            resultSet.Members.Sort((a, b) => a > b ? 1 : -1);
            return resultSet;
        }

        /// <summary>
        /// Union
        /// </summary>
        public static Set operator *(Set s1, Set s2)
        {
            var resultSet = new Set(new List<int>(s1.Members));
            foreach (var member in s2.Members.Where(member => !s1.Members.Contains(member)))
            {
                resultSet.Members.Add(member);
            }
            resultSet.Members.Sort((a, b) => a > b ? 1 : -1);
            return resultSet;
        }

        public static Set operator !(Set s1)
        {
            var resultSet = new Set(FullSet);
            return resultSet - s1;
        }

        public static Set operator -(Set s1, Set s2)
        {
            var resultSet = new Set(new List<int>(s1.Members));
            foreach (var number in s2.Members.Where(number => resultSet.Members.Contains(number)))
            {
                resultSet.Members.Remove(number);
            }

            resultSet.Members.Sort((a, b) => a > b ? 1 : -1);
            return resultSet;
        }

        public override string ToString()
        {
            return Members.Aggregate("", (current, member) => current + (member + " "));
        }
    }
}