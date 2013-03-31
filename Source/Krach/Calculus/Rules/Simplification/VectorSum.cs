using System;
using Krach.Calculus.Terms.Composite;
using Krach.Calculus.Terms;
using System.Linq;
using System.Collections.Generic;
using Krach.Extensions;
using Krach.Calculus.Terms.Basic;
using Krach.Calculus;
using Krach.Calculus.Terms.Basic.Definitions;
using System.Text.RegularExpressions;

namespace Krach.Calculus.Rules.Simplification
{
    public class VectorSum
    {
        public class ZeroDimensional : Rule
        {
            public override string ToString()
            {
                return "vector_sum_0";
            }
            public override T Rewrite<T>(T term)
            {
                if (!(term is FunctionDefinition)) return null;

                FunctionDefinition functionDefinition = (FunctionDefinition)(BaseTerm)term;

                Match match = Regex.Match(functionDefinition.Name, @"^vector_sum_0$");

                if (!match.Success) return null;

                return (T)(BaseTerm)Term.Constant().Abstract();
            }
        }
        public class OneDimensional : Rule
        {
            public override string ToString()
            {
                return "vector_sum_1";
            }
            public override T Rewrite<T>(T term)
            {
                if (!(term is FunctionDefinition)) return null;

                FunctionDefinition functionDefinition = (FunctionDefinition)(BaseTerm)term;

                Match match = Regex.Match(functionDefinition.Name, @"^vector_sum_1$");

                if (!match.Success) return null;

                return (T)(BaseTerm)Term.Sum();
            }
        }
    }
}

