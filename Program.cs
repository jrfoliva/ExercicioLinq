using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ExercicioLinq.Entities;

namespace ExercicioLinq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = @"c:\temp\employee.txt";
            Console.WriteLine(path);

            List<Employee> employees = new List<Employee>();
            try
            {
                using(StreamReader sr = File.OpenText(path)) 
                {
                    while (!(sr.EndOfStream))
                    {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        string email = fields[1];
                        double salary = Double.Parse(fields[2], CultureInfo.InvariantCulture);
                        employees.Add(new Employee(name, email, salary));   
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("An error occurred...");
                Console.WriteLine(ex);
            }

            /**********************************************************************************************/
            /* Fazer um programa para ler os dados (nomememail e salário) de funcionários a partir 
             * de um arquivo .csv.
             * 
             * Em seguida mostrar, em ordem alfabética, o email dos funcionários cujo salário seja superior a um
             * dado valor fornecido pelo usuário.
             * 
             * Mostrar também a soma dos salários dos funcionários cujo nome começa com a letra 'M'.
            */

            Console.Write("Enter a salary to search: $ ");
            double aSalary = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);


            //var r1 = employees.Where(p => p.Salary > aSalary).OrderBy(p => p.Email).Select(p => p.Email); ou
            var r1 =
                (from e in employees
                 where e.Salary > aSalary
                 orderby e.Email
                 select e.Email);
            if (r1.Any())
            {
                Console.WriteLine($"Salary > {aSalary} ordered by email...");
                foreach (string email in r1)
                {
                    Console.WriteLine(email);
                }
            }
            Console.WriteLine();

            //var r2 = employees.Where(p => p.Name[0] == 'M').Select(p => p.Salary).Sum(); ou
            var r2 =
                (from e in employees
                 where e.Name[0] == 'M'
                 select e.Salary).Sum();
            Console.WriteLine("Sum of Salaries by Names starts with 'M'...");
            Console.WriteLine(r2.ToString("F2",CultureInfo.InvariantCulture));

        }
    }
}