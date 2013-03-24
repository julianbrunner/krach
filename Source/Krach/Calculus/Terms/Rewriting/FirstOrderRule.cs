using System;
using System.Collections.Generic;
using System.Linq;
using Krach.Extensions;
using Krach.Calculus.Terms;
using Krach.Calculus.Terms.Combination;
using Krach.Calculus.Terms.Basic;

namespace Krach.Terms.Rewriting
{
	public class FirstOrderRule : Rule
	{
		readonly ValueTerm originalTerm;
		readonly ValueTerm rewrittenTerm;
		
		public FirstOrderRule(ValueTerm originalTerm, ValueTerm rewrittenTerm)
		{
			if (originalTerm == null) throw new ArgumentNullException("originalTerm");
			if (rewrittenTerm == null) throw new ArgumentNullException("rewrittenTerm");
			
			this.originalTerm = originalTerm;
			this.rewrittenTerm = rewrittenTerm;
		}
		
		public override bool Matches<T>(T term)
		{
			try { Match(originalTerm, term); }
			catch (InvalidOperationException) { return false; }
			
			return true;
		}
		public override T Rewrite<T>(T term)
		{
			IEnumerable<Assignment> equations = Match(originalTerm, term);
			Substitution substitution = Substitution.FromEquations(equations);
			BaseTerm result = substitution.Apply(rewrittenTerm);
			
			return (T)result;
		}
	
		static IEnumerable<Assignment> Match(ValueTerm pattern, BaseTerm term)
		{
			if (!(term is ValueTerm)) throw new InvalidOperationException();
			
			ValueTerm valueTerm = (ValueTerm)term;
			
			if (pattern.Dimension != valueTerm.Dimension) throw new InvalidOperationException();
			
			return AggregateEquations(pattern, valueTerm);
		}
		static IEnumerable<Assignment> AggregateEquations(ValueTerm pattern, ValueTerm term)
		{
			if (pattern is BasicValue && term is BasicValue)
			{
				BasicValue patternBasicValue = (BasicValue)pattern;
				BasicValue termBasicValue = (BasicValue)term;
				
				if (patternBasicValue != termBasicValue) throw new InvalidOperationException();
				
				return Enumerables.Create<Assignment>();
			}
			if (pattern is Variable)
			{
				Variable patternVariable = (Variable)pattern;
				
				return Enumerables.Create(new Assignment(patternVariable, term));
			}
			if (pattern is Application && term is Application) 
			{
				Application patternApplication = (Application)pattern;
				Application termApplication = (Application)term;
				
				if (patternApplication.Function != termApplication.Function) throw new InvalidOperationException();
				
				return AggregateEquations(patternApplication.Parameter, termApplication.Parameter);
			}
			if (pattern is Vector && term is Vector)
			{
				Vector patternVector = (Vector)pattern;
				Vector termVector = (Vector)term;
				
				if (patternVector.Terms.Count() != termVector.Terms.Count()) throw new InvalidOperationException();
				
				return
				(
					from equations in Enumerable.Zip(patternVector.Terms, termVector.Terms, AggregateEquations)
					from equation in equations
					select equation
				)
				.ToArray();
			}
			if (pattern is Selection && term is Selection)
			{
				Selection patternSelection = (Selection)pattern;
				Selection termSelection = (Selection)term;
				
				if (patternSelection.Index != termSelection.Index) throw new InvalidOperationException();
				
				return AggregateEquations(patternSelection.Term, termSelection.Term);
			}
			
			throw new InvalidOperationException();
		}
	}
}

