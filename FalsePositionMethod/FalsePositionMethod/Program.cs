using System;
using System.Collections.Generic;
using System.Globalization;

namespace FalsePositionMethod
{
    public class Program
    {
        static void Main(string[] args)
        {
            var application = new FalsePositionMethod();
            application.ShowHead();
            var (selection, expression) = application.ShowOptionsExpresions();
            while(selection<1 || selection>3)
            {
                Console.Clear();
                Console.WriteLine("Try again please!!!");
                application.ShowHead();
                (selection, expression) = application.ShowOptionsExpresions();
            }
            Console.WriteLine();
            Console.WriteLine($"Please Plot Graphic of the Expression {expression} to select Close Interval [a, b]");
            
            Console.WriteLine("Please enter a:");
            var aStr = Console.ReadLine();
            while (string.IsNullOrEmpty(aStr))
            {                
                Console.WriteLine("Please enter a:");
                aStr = Console.ReadLine();
            }
            var a = double.Parse(aStr);
            Console.WriteLine();

            Console.WriteLine("Please enter b:");
            var bStr = Console.ReadLine();
            while (string.IsNullOrEmpty(bStr))
            {
                Console.WriteLine("Please enter b:");
                bStr = Console.ReadLine();
            }
            var b = double.Parse(bStr);
            Console.WriteLine();

            Console.WriteLine("Please enter Maximun Iterations (iteration != 0):");
            var iterMaxStr= Console.ReadLine();
            while (string.IsNullOrEmpty(iterMaxStr)||iterMaxStr=="0")
            {
                Console.WriteLine("Please enter Maximun Iterations (iteration != 0):");
                iterMaxStr = Console.ReadLine();
            }
            var iterMax = new int();
            var ready = true;
            while (ready)
            {
                ready = false;
                try
                {
                    iterMax = int.Parse(iterMaxStr);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    Console.WriteLine("Please NOT include decimal numbers");
                    Console.WriteLine("Please NOT include Negative numbers");                    
                    
                    Console.WriteLine("Please enter Maximun Iterations (iteration != 0):");
                    iterMaxStr = Console.ReadLine();
                    while (string.IsNullOrEmpty(iterMaxStr) || iterMaxStr == "0")
                    {
                        Console.WriteLine("Please enter Maximun Iterations (iteration != 0):");
                        iterMaxStr = Console.ReadLine();
                    }

                    ready = true;
                }
            }                
            if (iterMax>0 && iterMax < 1) iterMax = 1;
            if (iterMax < 0) iterMax = iterMax * (-1);
            //Console.WriteLine(iterMax);
            Console.WriteLine();

            Console.WriteLine("Please enter Tolerance [0.00% - 100.00%]");
            var tolStr= Console.ReadLine();            
            while (string.IsNullOrEmpty(tolStr))
            {
                Console.WriteLine("Please enter Tolerance [0.00% - 100.00%]");
                tolStr = Console.ReadLine();
            }
            var tol = new double();
            var ready2 = true;
            while (ready2)
            {
                ready2 = false;
                try
                {
                    tol = double.Parse(tolStr);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{e.Message}");                    
                    Console.WriteLine("Please NOT include Negative numbers");

                    Console.WriteLine("Please enter Tolerance [0.00% - 100.00%]");
                    tolStr = Console.ReadLine();
                    while (string.IsNullOrEmpty(tolStr))
                    {
                        Console.WriteLine("Please enter Tolerance [0.00% - 100.00%]");
                        tolStr = Console.ReadLine();
                    }
                    ready2 = true;
                }
                tol = tol / 100;
                //Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "{0:#0.##%}", tol));
            }
            if (tol < 0) tol = tol * (-1);
            Console.WriteLine();            
            application.FalsePosition(a, b, selection, iterMax, tol);

        }

        public class FalsePositionMethod
        {
            public void ShowHead()
            {
                var lineThink = byte.Parse("2");
                var head = "FALSE POSITION METHOD, Vers 1.0";
                var numHeadChar = head.Length;
                var withHead = 20 + numHeadChar;
                var heigthHead = 5;
                var fI = 2;
                var fF = fI + heigthHead;
                var cI = 2;
                var cF = cI + withHead;
                int t, f, c;
                for (t = 1; t <= lineThink; t++)
                {
                    for (f = cI; f <= cF; f++)
                    {
                        Console.SetCursorPosition(f, fI);
                        Console.Write("_");
                        Console.SetCursorPosition(f, fF);
                        Console.Write("_");
                        if (t == 1)
                        {
                            Console.SetCursorPosition(cI + (10), fI + 1+ (heigthHead / 2));
                            Console.Write(head);
                        }                        
                    }

                    for (c = fI+1; c <= fF; c++)
                    {
                        Console.SetCursorPosition(cI, c);
                        Console.Write("|");
                        Console.SetCursorPosition(cF, c);
                        Console.Write("|");
                    }

                    fI = fI + 1;
                    cI = cI + 2;
                    fF = fF - 1;
                    cF = cF - 2;
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }

            public (int, string) ShowOptionsExpresions()
            {
                var expressions = new List<string>();
                var expression_1 = "f(x) = x^2-5";
                expressions.Add(expression_1);
                var expression_2 = "f(x) = 4x^3+2x-6";
                expressions.Add(expression_2);
                var expression_3 = "f(x) = x+cos(x)";
                expressions.Add(expression_3);
                Console.WriteLine("This are the Functions for test to False Position Method, please choose one option:");
                Console.WriteLine();
                foreach (string expresion in expressions)
                {
                    var index=expressions.IndexOf(expresion)+1;
                    Console.Write(" "+index+")"+"  ");
                    Console.WriteLine(expresion);
                }
                Console.WriteLine();
                Console.WriteLine("Your Choose please:");
                var expressionSelected = new int();
                try
                {
                    expressionSelected = (int.Parse(Console.ReadLine())) - 1;
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Error Description{e.Message}");
                    Console.WriteLine("You Must select an option from 1 to 3");
                    var (selection, expression)=this.ShowOptionsExpresions();
                    return (selection, expression);
                }
                //if (expressionSelected > 2)
                //{
                //    throw new Exception("Your Selecction isn´t in options list");
                //}
                Console.WriteLine();
                Console.WriteLine("The Function you are selected is:");
                var function=new string("");
                try
                {
                    function = expressions[expressionSelected];
                    Console.WriteLine(function);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("You Must select an option from 1 to 3");
                    Console.WriteLine("Please Try Again");
                }
                expressionSelected = expressionSelected + 1;
                return (expressionSelected,function);
                
            }

            public double FxSelected(int expressionSelected, double x)
            {
                double expression=new double();

                switch (expressionSelected)
                {
                    case 1:
                        expression = (((Math.Pow(x, 2))) - 5);                        
                        break;
                    case 2:
                        expression = ((4 * (Math.Pow(x, 3))) + (2 * x) - 6);
                        break;
                    case 3:
                        expression = ((x) - (Math.Cos(x)));
                        break;
                }
                return expression;
            }

            public void FalsePosition(double a, double b, int selection, int iterMax, double tol)
            {
                var c = (a + b) / 2;
                var fa = FxSelected(selection, a);
                var fb = FxSelected(selection, b);
                var fc = FxSelected(selection, c);
                var valid = fa * fc;
                var error = c - a;
                var iter = 0;
                if (valid >= 0) Console.WriteLine("For this Close Interval there ist´n ROOTS");
                //Console.WriteLine(valid);
                var interations = new List<Interation>();
                var iterX = new Interation() { Iter = iter, A = a, B = b, C = c, FA = fa, FB = fb, FC = fc, ValidX = valid, ErrorX = error };
                interations.Add(iterX);
                var bX = new double();
                var fbX = new double();
                var validX = new double();
                var errorX = new double();
                for (iter = 1; iter <= iterMax; iter++)
                {
                    if(iter<iterMax || (b - a) > tol)
                    {
                        bX = a - (((c - a) / (fc - fa)) * fa);
                        fbX = FxSelected(selection, bX);
                        validX = fa * fc;
                        if (validX < 0)
                        {
                            c = bX;
                            fc = fbX;
                            errorX = c - a;
                        }
                        else
                        {
                            a = bX;
                            fa = fbX;
                            errorX = a - c;
                        }
                    }
                    var iterXn = new Interation() { Iter=iter, A=a, B=b, C=c, FA=fa, FB=fb, FC=fc, ValidX=validX, ErrorX=errorX };

                    //iterX.Iter = iter;
                    //iterX.A = a;
                    //iterX.B = b;
                    //iterX.C = c;
                    //iterX.FA = fa;
                    //iterX.FB = fb;
                    //iterX.FC = fc;
                    //iterX.ValidX = validX;
                    //iterX.ErrorX = errorX;

                    interations.Add(iterXn);
                }
                ShowResultHead(interations);
            }
            
            public void ShowResultHead(List<Interation> interations)
            {
                Console.Clear();
                // iter table
                var fI = 2;
                var cI = 3;
                var width = 114;
                var height = 3;
                var fF = fI + height;
                var cF = cI + width;
                var f = new int();
                var c = new int();

                for (f = cI; f <= cF; f++)
                {
                    Console.SetCursorPosition(f, fI);
                    Console.Write("-");
                }
                for (f = cI; f <= cF; f++)
                {
                    Console.SetCursorPosition(f, fF);
                    Console.Write("-");
                }
                for(c=fI; c <= 3; c++)
                {
                    Console.SetCursorPosition(cI, c+1);
                    Console.Write("|");                    
                    Console.SetCursorPosition(cI+8, c + 1);
                    Console.Write("|");
                    Console.SetCursorPosition(cI + 22, c + 1);
                    Console.Write("|");
                    Console.SetCursorPosition(cI + 36, c + 1);
                    Console.Write("|");
                    Console.SetCursorPosition(cI + 50, c + 1);
                    Console.Write("|");
                    Console.SetCursorPosition(cI + 66, c + 1);
                    Console.Write("|");
                    Console.SetCursorPosition(cI + 82, c + 1);
                    Console.Write("|");
                    Console.SetCursorPosition(cI + 98, c + 1);
                    Console.Write("|");
                    Console.SetCursorPosition(cI + 114, c + 1);
                    Console.Write("|");
                }
                Console.SetCursorPosition(cI + 2, 4);
                Console.Write("Iter");
                Console.SetCursorPosition(cI + 15, 4);
                Console.Write("a");
                Console.SetCursorPosition(cI + 29, 4);
                Console.Write("c");
                Console.SetCursorPosition(cI + 43, 4);
                Console.Write("b");
                Console.SetCursorPosition(cI + 58, 4);
                Console.Write("fa");
                Console.SetCursorPosition(cI + 74, 4);
                Console.Write("fc");
                Console.SetCursorPosition(cI + 90, 4);
                Console.Write("fb");
                Console.SetCursorPosition(cI + 106, 4);
                Console.Write("Error");
                fI = 5;
                cI = 3;
                var count = interations.Count;
                
                for (int i=0;i<count;i++)
                {
                    var iter = interations[i].Iter;
                    //Console.WriteLine(iter);
                    for (c = fI; c <= fI+3; c++)
                    {
                        Console.SetCursorPosition(cI, c + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(cI + 8, c + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(cI + 22, c + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(cI + 36, c + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(cI + 50, c + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(cI + 66, c + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(cI + 82, c + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(cI + 98, c + 1);
                        Console.Write("|");
                        Console.SetCursorPosition(cI + 114, c + 1);
                        Console.Write("|");
                    }
                    for (f = cI; f <= cF; f++)
                    {
                        Console.SetCursorPosition(f, fF);
                        Console.Write("-");
                    }
                    Console.SetCursorPosition(cI + 4, fF+2);
                    Console.Write(String.Format(CultureInfo.InvariantCulture,"{0:##}", interations[i].Iter.ToString()));
                    Console.SetCursorPosition(cI + 13, fF+2);
                    Console.Write(String.Format(CultureInfo.InvariantCulture, "{0:0.00}", interations[i].A));
                    Console.SetCursorPosition(cI + 25, fF+2);
                    Console.Write(String.Format(CultureInfo.InvariantCulture, "{0:0.###E-0}", interations[i].C));
                    Console.SetCursorPosition(cI + 42, fF+2);
                    Console.Write(String.Format(CultureInfo.InvariantCulture, "{0:0.00}", interations[i].B));
                    Console.SetCursorPosition(cI + 54, fF+2);
                    Console.Write(String.Format(CultureInfo.InvariantCulture, "{0:0.###E-0}", interations[i].FA));
                    Console.SetCursorPosition(cI + 70, fF+2);
                    Console.Write(String.Format(CultureInfo.InvariantCulture, "{0:0.###E-0}", interations[i].FC));
                    Console.SetCursorPosition(cI + 86, fF+2);
                    Console.Write(String.Format(CultureInfo.InvariantCulture, "{0:0.###E-0}", interations[i].FB));
                    Console.SetCursorPosition(cI + 102, fF+2);
                    Console.Write(String.Format(CultureInfo.InvariantCulture, "{0:#0.###%}", interations[i].ErrorX));
                    
                    fI = fI + 4;
                    fF = fF + 4;
                    
                }
                for (f = cI; f <= cF; f++)
                {
                    Console.SetCursorPosition(f, fF);
                    Console.Write("-");
                }
                Console.SetCursorPosition(cI + 25, fF + 2);
                Console.Write("The Root is: ");
                Console.Write(interations[count - 1].C);
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public class Interation
        {
            public int Iter { get; set; }
            public double A { get; set; }
            public double B { get; set; }
            public double FA { get; set; }
            public double FB { get; set; }
            public double C { get; set; }
            public double FC { get; set; }
            public double ValidX { get; set; }
            public double ErrorX { get; set; }
        }
    }
}
