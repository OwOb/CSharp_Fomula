# CSharp_Fomula

A simple tool used to calculate formula string by given variable in C#.

---

## Formula constructor
varible use `t`
```cs
Formula(string)
```
example:
```cs
Formula fml = new Formula("sin(-cos(t)^3-1.5)/t+2");
```

---

## Formula calculate
calculate formula by given value substitute varible `t`
```cs
double Formula.Calculate(double)
```
example:
```cs
Formula fml = new Formula("sin(-cos(t)^3-1.5)/t+2");
fml.Calculate(2.0);   // 1.50509384955828
```

---

## Formula operator count
number of operator use in formula
```cs
int Formula.OperatorCount()
```
example:
```cs
Formula fml = new Formula("sin(-cos(t)^3-1.5)/t+2");
fml.OperatorCount();   // 7
```
