using System;
using System.Collections;
using System.Collections.Generic;

namespace Practical_Work_2
{
    public class Converter
    {
        private List<Conversion> operations;
        public List<Conversion> Operations => operations;

        public Converter()
        {
            operations = new List<Conversion>
            {
                new DecimalToBinary("Binary", "Decimal to Binary"),
                new DecimalToTwoComplement("TwoComplement", "Decimal to Two's Complement"),
                new DecimalToOctal("Octal", "Decimal to Octal"),
                new DecimalToHexadecimal("Hexadecimal", "Decimal to Hexadecimal"),
                new BinaryToDecimal("Decimal", "Binary to Decimal"),
                new TwoComplementToDecimal("Decimal", "Two's Complement to Decimal"),
                new OctalToDecimal("Decimal", "Octal to Decimal"),
                new HexadecimalToDecimal("Decimal", "Hexadecimal to Decimal")
            };
        }
        
        public int Exit()
        {
            return this.operations.Count + 1;
        }

        public int GetNumberOperations()
        {
            return this.operations.Count;
        }
        
        public int PrintOperations()
        {
            Console.Clear();

            Console.WriteLine("--------------------------------------");

            for (int i = 1; i <= this.operations.Count; i++)
            {
                Console.WriteLine($" {i}. {this.operations[i - 1].GetDefinition()}");
            }

            Console.WriteLine($" {this.Exit()}. Exit");
            Console.WriteLine("--------------------------------------");

            string? tmp = Console.ReadLine();
            if (tmp == "") return this.Exit();

            int option = int.Parse(tmp);

            return (option < 1 || option > this.GetNumberOperations()) ? this.Exit(): option;
        }

        
        public string PerformConversion(int op, string input)
        {
            this.operations[op-1].validate(input);
            if (this.operations[op-1].NeedBitSize())
            {
                Console.WriteLine("How many bits should i use: ");
                int bits = Int32.Parse(Console.ReadLine());

                return this.operations[op-1].Change(input,bits);
            }
            return this.operations[op-1].Change(input);
        }

        public void PrintConversion(int op, string input, string output)
        {
            this.operations[op-1].PrintConversion(input, output);
        }
    }
}