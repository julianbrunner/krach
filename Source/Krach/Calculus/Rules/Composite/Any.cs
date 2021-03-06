using System;
using Krach.Calculus.Terms;
using System.Linq;
using System.Collections.Generic;
using Krach.Extensions;
using Krach.Calculus.Terms.Basic;
using Krach.Calculus;

namespace Krach.Calculus.Rules.Composite
{
	public class Any : Rule
	{
		readonly IEnumerable<Rule> rules;

		public IEnumerable<Rule> Rules { get { return rules; } }

		public Any(IEnumerable<Rule> rules)
		{
			if (rules == null) throw new ArgumentException("rules");

			this.rules = rules.ToArray();
		}
		public Any(params Rule[] rules) : this((IEnumerable<Rule>)rules) { }

		public override string ToString()
		{
			return string.Format("({0})", rules.ToStrings().Separate("|").AggregateString());
		}
		public override T Rewrite<T>(T term)
		{
			foreach (Rule rule in rules)
			{
				T rewrittenTerm = rule.Rewrite(term);

                if (rewrittenTerm != null) return rewrittenTerm;
			}

			return null;
		}
	}
}

