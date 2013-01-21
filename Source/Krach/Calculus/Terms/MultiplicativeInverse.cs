using System;

namespace Krach.Calculus.Terms
{
	public class MultiplicativeInverse : Term
	{
		readonly Term term;

		public MultiplicativeInverse(Term term)
		{
			if (term == null) throw new ArgumentNullException("term");

			this.term = term;
		}
		
		public override string ToString()
		{
			return string.Format("({0}) ^ -1", term);
		}
		public override double Evaluate()
		{
			return 1 / term.Evaluate();
		}
		public override Term Substitute(Variable variable, Term substitute)
		{
			return new MultiplicativeInverse(term.Substitute(variable, substitute));
		}
		public override Term GetDerivative(Variable variable)
		{
			return new AdditiveInverse(new Product(term.GetDerivative(variable), new MultiplicativeInverse(new Product(term, term))));
		}
	}
}

