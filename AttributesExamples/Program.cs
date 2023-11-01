#define LOG_INFO
using System;
using System.Reflection;
using System.Text.Json;
using AttributesExamples.Models;
using LoggingComponent;
using ValidateComponent;

[assembly: AssemblyDescription("My Assembly Description")]

namespace AttributesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly=typeof(Program).Assembly;
            AssemblyName assemblyName = assembly.GetName();

            Version assemblyVersion = assemblyName.Version;
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);  
            var thisAssemDescriptionAttribute = attributes[0] as AssemblyDescriptionAttribute;

            //Logging.LogToScreen("This code is testing logging functionally");

            Employee emp = new Employee();
            string empId = null;
            string firstName = null;
            string lastName = null;
            string postCode = null;

            Type employeeType = typeof(Employee);

            Department dept = new Department();
            string deptShortName = null;
            Type departmentType = typeof(Department);

            if (GetInput(employeeType, "Please enter your id", "Id", out empId))
            {
                emp.Id= Int32.Parse(empId);
            }

            if (GetInput(employeeType, "Please enter your firstname", "FirstName", out firstName))
            {
                emp.FirstName= firstName;
            }

            if (GetInput(employeeType, "Please enter your lastname", "LastName", out lastName))
            {
                emp.LastName = lastName;
            }

            if (GetInput(employeeType, "Please enter your postcode", "PostCode", out postCode))
            {
                emp.PostCode = postCode;
            }

            if (GetInput(departmentType, "Please enter the employee's department code", "DeptShortName", out deptShortName))
            {
                dept.DeptShortName = deptShortName;
            }

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Thank you! Employee with first name, {emp.FirstName},last name, {emp.LastName} and Id, {emp.Id}, and post code, {emp.PostCode}, has been entered successfully!!");

            var employeeJSON = JsonSerializer.Serialize(emp);
            Console.WriteLine(employeeJSON);


            Console.ReadKey();
        }

        private static bool GetInput(Type t, string promptText, string fieldName, out string fieldValue)
        {
            fieldValue = "";
            string enteredValue = "";
            string errorMessage = null;
            do
            {
                Console.WriteLine(promptText);

                enteredValue = Console.ReadLine();

                if (!Validation.PropertyValueIsValid(t, enteredValue, fieldName, out errorMessage))
                {
                    fieldValue = null;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(errorMessage);
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else
                {
                    fieldValue = enteredValue;
                    break;
                }


            }
            while (true);

            return true;
        }
    }
}