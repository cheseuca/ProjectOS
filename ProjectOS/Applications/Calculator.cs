using Cosmos.System;
using ProjectOS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Applications
{
    internal class Calc : Command
    {
        public Calc(string name) : base(name) { }
        public override string execute(string[] args)
        {
            System.Console.Clear();
            Calculator calculator = new Calculator();
            return " ";
        }
    }
    
    internal class Calculator
    {
        double num1 = 0;
        double num2 = 0;
        double ans = 0;
        string op;
        public Calculator()
        {
            string result = "=\t ";
            CalculatorCLI();

            switch (op)
            {
                case "+":
                    ans = num1 + num2;
                    System.Console.WriteLine(result + ans);
                    break;
                case "-":
                    ans = num1 - num2;
                    System.Console.WriteLine(result + ans);
                    break;
                case "*":
                    ans = num1 * num2;
                    System.Console.WriteLine(result + ans);
                    break;
                case "/":
                    ans = num1 / num2;
                    System.Console.WriteLine(result + ans);
                    break;
                default:
                    System.Console.WriteLine("Error! Try Again!");
                    break;
            }
            System.Console.WriteLine("Type \"1\" to Retry/Continue or type \"2\" to Exit Calculator");
            string user_input = System.Console.ReadLine();
            if (user_input != null)
            {
                if (user_input == "1")
                {
                    System.Console.Clear();
                    CalculatorCLI();
                }
                else if (user_input == "2")
                {
                    System.Console.Clear();
                    System.Console.WriteLine("Welcome to ProjectOS\nType \"help\" for a list of commands");
                }
                }
            else
            {
                    System.Console.WriteLine("Error!");
            }
            


        }
        void CalculatorCLI()
        {
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("||  \tCalculator  \t||");
            System.Console.WriteLine("==========================");
            System.Console.WriteLine("||  \t Operators  \t||");
            System.Console.WriteLine("|| \t+  -  *  -   \t||");
            System.Console.WriteLine("==========================");
            System.Console.Write("Enter 1st Number: ");
            num1 = double.Parse(System.Console.ReadLine());
            System.Console.Write("Enter Operator: ");
            op = System.Console.ReadLine();
            System.Console.Write("Enter 2nd Number: ");
            num2 = double.Parse(System.Console.ReadLine());

        }

    }
}
