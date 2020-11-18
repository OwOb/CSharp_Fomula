using System;

namespace FormulaLib
{
	public class Formula
	{
		// class member
		private FormulaBase _formula;
		private int _operatorCount;

		// analysis String to Formula
		private FormulaBase StringToFormula(string fml)
		{
			// remove all leading and trailing whitespace characters
			fml = fml.Trim();
			//Console.WriteLine(fml);

			int parenthesesCount = 0;
			// add(+) & subtract(-)
			for (int i = fml.Length-1; i >= 0; --i)
			{
				if (fml[i] == ')')
				{
					++parenthesesCount;
				}
				else if (fml[i] == '(')
				{
					--parenthesesCount;
				}
				else if (fml[i] == '+' && parenthesesCount == 0)
				{
					string leftString = fml.Substring(0, i).Trim();
					if (leftString != "" && "+-*/^".IndexOf(leftString[leftString.Length-1]) < 0)
					{
						++_operatorCount;
						return new FormulaBinary(StringToFormula(leftString), StringToFormula(fml.Substring(i+1)), FormulaFunction.Add);
					}
				}
				else if (fml[i] == '-' && parenthesesCount == 0)
				{
					string leftString = fml.Substring(0, i).Trim();
					if (leftString != "" && "+-*/^".IndexOf(leftString[leftString.Length-1]) < 0)
					{
						++_operatorCount;
						return new FormulaBinary(StringToFormula(leftString), StringToFormula(fml.Substring(i+1)), FormulaFunction.Subtract);
					}
				}
			}
			// multiply(*) & divide(/)
			for (int i = fml.Length-1; i >= 0; --i)
			{
				if (fml[i] == ')')
				{
					++parenthesesCount;
				}
				else if (fml[i] == '(')
				{
					--parenthesesCount;
				}
				else if (fml[i] == '*' && parenthesesCount == 0)
				{
					++_operatorCount;
					return new FormulaBinary(StringToFormula(fml.Substring(0, i)), StringToFormula(fml.Substring(i+1)), FormulaFunction.Multiply);
				}
				else if (fml[i] == '/' && parenthesesCount == 0)
				{
					++_operatorCount;
					return new FormulaBinary(StringToFormula(fml.Substring(0, i)), StringToFormula(fml.Substring(i+1)), FormulaFunction.Divide);
				}
			}
			// power(^)
			for (int i = 0; i < fml.Length; ++i)
			{
				if (fml[i] == ')')
				{
					++parenthesesCount;
				}
				else if (fml[i] == '(')
				{
					--parenthesesCount;
				}
				else if (fml[i] == '^' && parenthesesCount == 0)
				{
					++_operatorCount;
					return new FormulaBinary(StringToFormula(fml.Substring(0, i)), StringToFormula(fml.Substring(i+1)), FormulaFunction.Power);
				}
			}

			// empty formula
			if (fml == "")
			{
					return new FormulaBase();
			}

			// negative(-)
			if (fml[0] == '-')
			{
					++_operatorCount;
			return new FormulaUnary(StringToFormula(fml.Substring(1)), FormulaFunction.Negative);
			}
				
			// variable "t"
			if (fml == "t")
			{
				return new FormulaPureVariable();
			}

			// function
			if (fml[fml.Length-1] == ')')
			{
				int leftParenthesesIndex = fml.IndexOf('(');
				string functionName = fml.Substring(0, leftParenthesesIndex);
				Func<double, double> func = FormulaFunction.Positive;
				switch (functionName)
				{
					case "" :
					case "+" :
						break;
					case "-" :
						++_operatorCount;
						func = FormulaFunction.Negative;
						break;
					case "abs" :
						++_operatorCount;
						func = FormulaFunction.Abs;
						break;
					case "sqrt" :
						++_operatorCount;
						func = FormulaFunction.Sqrt;
						break;
					case "log" :
						++_operatorCount;
						func = FormulaFunction.Log;
						break;
					case "sin" :
						++_operatorCount;
						func = FormulaFunction.Sin;
						break;
					case "cos" :
						++_operatorCount;
						func = FormulaFunction.Cos;
						break;
					case "tan" :
						++_operatorCount;
						func = FormulaFunction.Tan;
						break;
				}
				return new FormulaUnary(StringToFormula(fml.Substring(leftParenthesesIndex+1, fml.Length-leftParenthesesIndex-2)), func);
			}
			// pure value
			return new FormulaPureValue(Convert.ToDouble(fml));
		}

		// constructor
		public Formula(string fml)
		{
			_operatorCount = 0;
			_formula = StringToFormula(fml);
		}

		// calculate subtitute x into formula
		public double Calculate(double x)
		{
			return _formula.Calculate(x);
		}

		// number of operators
		public int OperatorCount()
		{
			return _operatorCount;
		}
	}
}