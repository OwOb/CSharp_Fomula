using System;

namespace FormulaLib
{
	//---------//
	//  class  //
	//---------//

	// base type for Formula
	public class FormulaBase
	{
		// constructor
		public FormulaBase() {}

		// calculate
		public virtual double Calculate(double x)
        {
			return 0.0;
		}
	}

	// pure value
	public class FormulaPureValue : FormulaBase
	{
		// class member
		private double _pv;
		
		// construct
		public FormulaPureValue(double x)
        {
			_pv = x;
		}
		
		// calculate subtitute x into formula
		public override double Calculate(double x)
        {
			return _pv;
		}
	};

	// pure variable
	public class FormulaPureVariable : FormulaBase {

		// construct
		public FormulaPureVariable() {}
		
		// calculate subtitute x into formula
		public override double Calculate(double x)
        {
			return x;
		}
	};

	// unary operation Formula
	public class FormulaUnary : FormulaBase {
		
		// class member
		private FormulaBase _formula;
		private Func<double, double> _function;
		
		// construct
		public FormulaUnary(FormulaBase _fml, Func<double, double> _func)
        {
			_formula = _fml;
			_function = _func;
		}
		
		// calculate subtitute x into formula
		public override double Calculate(double x)
        {
			return _function(_formula.Calculate(x));
		}
	};

	// binary operation Formula
	public class FormulaBinary : FormulaBase {
		
		// class member
		public FormulaBase _formula_left;
        public FormulaBase _formula_right;
		public Func<double, double, double> _function;
		
		// construct
		public FormulaBinary(FormulaBase _fmll, FormulaBase _fmlr, Func<double, double, double> _func)
        {
			_formula_left = _fmll;
			_formula_right = _fmlr;
			_function = _func;
		}
		
		// calculate subtitute x into formula
		public override double Calculate(double x)
        {
			return _function(_formula_left.Calculate(x), _formula_right.Calculate(x));
		}
	};


	//------------//
	//  function  //
	//------------//

	public class FormulaFunction 
    {
        // positive function
        static public double Positive(double x)
        {
            return x;
        }

        // negative function
        static public double Negative(double x)
        {
            return -x;
        }

        // absolute function
        static public double Abs(double x)
        {
            return x < 0.0 ? -x : x;
        }

        // sqrt function
        static public Func<double, double> Sqrt = Math.Sqrt;

        // log function
        static public Func<double, double> Log = Math.Log;

        // sin function
        static public Func<double, double> Sin = Math.Sin;

        // cos function
        static public Func<double, double> Cos = Math.Cos;

        // tan function
        static public Func<double, double> Tan = Math.Tan;

        // add(+)
		static public double Add(double lhs, double rhs)
        {
			return lhs + rhs;
		}

        // subtract(-)
		static public double Subtract(double lhs, double rhs)
        {
			return lhs - rhs;
		}

        // multiply(*)
		static public double Multiply(double lhs, double rhs)
        {
			return lhs * rhs;
		}

        // divide(/)
		static public double Divide(double lhs, double rhs)
        {
			return lhs / rhs;
		}

        // power(^)
		static public Func<double, double, double> Power = Math.Pow;
	}
}