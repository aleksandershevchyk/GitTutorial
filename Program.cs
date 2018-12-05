using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {

        protected static List<Employee> EmployeeList = new List<Employee>();

        static void Main(string[] args)
        {
           
            Console.WriteLine($"{args[0]}");
            
            InitEmployeeList();
            
            Console.WriteLine("Введите одну из доступных команд: \nList - показать список дел \nAdd - добавить дело \nDelete - удалить дело \nEdit - редактирование дела \nExit - выход из программы ");
            EnterCommand(Console.ReadLine());
        }

        static int EnterCommand(string Enter)
        {
            switch (Enter) //в зависимости что введено, выполняется 1 из 4х функций ниже
            {
                case "List":
                    processCommandList();
                    break;
                case "Add":
                    processCommandAdd();
                    break;
                case "Delete":
                    processCommandDelete();
                    break;
                case "Edit":
                    processCommandEdit();
                    break;
                case "Exit":
                    break;
                default:
                    Console.WriteLine($"Введённая команда {Enter} не существует");
                    break;
            }

            if (Enter == "Exit")
            {
                return 0;
            }
            Console.WriteLine("Введите одну из доступных команд: \nList - показать список сотрудников \nAdd - добавить сотрудника \nDelete - удалить сотрудника \nEdit - редактирование сотрудника \nExit - выход из программы ");
            return EnterCommand(Console.ReadLine());
        }



        protected static void outputEmployeeList(List<Employee> outputEmployeeList)
        {
            foreach (Employee item in outputEmployeeList)
            {
                Console.WriteLine($"ID:{item.Id}, Имя: {item.Name}, Отдел №:{item.GetDepartment()}, Должность: {item.Position}, ЗП: {item.Salary}, Стаж: {item.WorkExp}, Дата Рождения: {item.BirthDate.ToString("dd.MM.yyyy")}");
            }
        }

        protected static void processCommandList()
        {
            Console.WriteLine(
                "1 - вывести весь список сотрудников \n" +
                "2 - вывести список сотрудников по отделам в порядке убывания стажа работы \n" +
                "3 - вывести информацию о сотрудниках, имеющих зарплату ниже уровня , отсортировав их по рабочему стажу \n" +
                "4 - вывести  сотрудников по отделам \n" +
                "5 - вывести список сотрудников, включив в него, помимо начальных данных, столбец премий(50%) " +
                    "от оклада и итоговые суммы для каждого сотрудника \n" +
                "6 - вывести список сотрудников, день рождения которых в текущем месяце \n" +
                "7 - вывести два новых списка, поместив в первый тех сотрудников, которые работают меньше 5 лет," +
                    " а во второй - всех прочих, подсчитать количество сотрудников каждой группы \n");
            var ListNumber = Console.ReadLine();
            switch (ListNumber) //в зависимости что введено, выполняется 1 из 7х функций ниже
            {
                case "1":
                    outputEmployeeList(EmployeeList);
                    break;
                case "2":
                    processListDepartmentByWorkExp();
                    break;
                case "3":
                    processListSalaryByWorkExp();
                    break;
                case "4":
                    processListDepartment();
                    break;
                case "5":
                    processListPrize();
                    break;
                case "6":
                    processListBirthdayInMonth();
                    break;
                case "7":
                    processTwoListWorkExp();
                    break;
                default:
                    Console.WriteLine($"Введённая команда {ListNumber} не существует");
                    break;
            }


        }

        protected static int? FindEmplooyeListIndexByID(string Id)
        {
            int index = 0;
            foreach (Employee emp in EmployeeList)
            {
                if (emp.Id == Id)
                {
                    return index;
                }
                index++;
            } 

            return null;
        }

        protected static void processListDepartmentByWorkExp()
        {

            foreach (string Dep in Employee.Deps)
            {
                  List<Employee> DepEmployees = new List<Employee>();
                foreach (Employee employee in EmployeeList)
                {
                    if(employee.GetDepartment() == Dep)
                    {
                        DepEmployees.Add(employee);
                    }
                }
                Console.WriteLine($"Работники Отдела {Dep}");
                //var orderedEmployes = DepEmployees.OrderBy(e => e.WorkExp);
                //var OrderedEmployesWithRightType = orderedEmployes.ToList();
                //outputEmployeeList(OrderedEmployesWithRightType);
                outputEmployeeList(DepEmployees.OrderByDescending(e => e.WorkExp).ToList());
                Console.WriteLine("\n");
            }
        }

        protected static void processListSalaryByWorkExp()
        {
            Console.WriteLine("Введите ограничение зп для фильтрации");
            double MaxSalary = double.Parse(Console.ReadLine());
            List<Employee> EmployeesWithSalaryLessThanMax = new List<Employee>();
            foreach (Employee employee in EmployeeList)
            {
                if (employee.Salary < MaxSalary)
                {
                    EmployeesWithSalaryLessThanMax.Add(employee);
                }
            }
            Console.WriteLine($"Работники с ЗП ниже {MaxSalary}");
            outputEmployeeList(EmployeesWithSalaryLessThanMax.OrderBy(e => e.WorkExp).ToList());
            Console.WriteLine("\n");
        }

        protected static void processListDepartment()
        {

            //foreach (string Dep in Employee.Deps)
            //{
            //    List<Employee> DepEmployees = new List<Employee>();
            //    foreach (Employee employee in EmployeeList)
            //    {
            //        if (employee.Department == Dep)
            //        {
            //            DepEmployees.Add(employee);
            //        }
            //    }
            //    Console.WriteLine($"Работники Отдела {Dep}");
            //    outputEmployeeList(DepEmployees);
            //    Console.WriteLine("\n");
            //}
            outputEmployeeList(EmployeeList.OrderBy(e => e.GetDepartment()).ToList());
        }

        protected static void processListPrize()
        {
            foreach (Employee item in EmployeeList)
            {
                Console.WriteLine($"ID:{item.Id}, Имя: {item.Name}, Отдел №:{item.GetDepartment()}, Должность: {item.Position}, ЗП: {item.Salary}, Премия 50%: {(item.Salary)/2}, ЗП + Премия: {(item.Salary)*1.5}, Стаж: {item.WorkExp}, Дата Рождения: {item.BirthDate.ToString("dd.MM.yyyy")}");
            }
        }

        protected static void processTwoListWorkExp()
        {
            List<Employee> processListWorkExpLessThenFive = new List<Employee>();
            foreach (Employee employee in EmployeeList)
            {
                if (employee.WorkExp < 5)
                {
                    processListWorkExpLessThenFive.Add(employee);
                }
            }
            Console.WriteLine($"Работники с опытом работы меньше 5 лет:");
            outputEmployeeList(processListWorkExpLessThenFive);
            Console.WriteLine("\n");

            List<Employee> processListWorkExpMoreThenFive = new List<Employee>();
            foreach (Employee employee in EmployeeList)
            {
                if (employee.WorkExp >= 5)
                {
                    processListWorkExpMoreThenFive.Add(employee);
                }
            }
            Console.WriteLine($"Остальные работники:");
            outputEmployeeList(processListWorkExpMoreThenFive);
            Console.WriteLine("\n");
        }

        protected static void processListBirthdayInMonth()
        {
            List<Employee> BirthDateEmployees = new List<Employee>();
            foreach (Employee employee in EmployeeList)
            {
                if (employee.BirthDate.ToString("MMM") == DateTime.Now.ToString("MMM"))
                {
                    BirthDateEmployees.Add(employee);
                }
            }
            Console.WriteLine($"Работники с ДР в текущем месяце:");
            outputEmployeeList(BirthDateEmployees);
            Console.WriteLine("\n");

        }

        protected static void InitEmployeeList()
        {
            string filePath = @"C:\Users\Max\source\repos\ConsoleApp6\ConsoleApp6\Emplyooe.csv";
            using (TextFieldParser tfp = new TextFieldParser(filePath))
            {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(";");

                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    Employee employee = new Employee()
                    {
                        Id = fields[0],
                        Name = fields[1],
                        Position = fields[3],
                        Salary = Convert.ToDouble(fields[4]),
                        WorkExp =Int32.Parse(fields[5]),
                        BirthDate = DateTime.ParseExact(fields[6], "dd.MM.yyyy", CultureInfo.InvariantCulture)
                    };
                    try
                    {
                        employee.SetDepartment(fields[2]);
                        EmployeeList.Add(employee);
                    }
                    catch(System.ArgumentException)
                    {
                        Console.WriteLine("ОШИБКА,введённого отдела не существует");
                    }
                    
                }
            }
            
        }

        protected static void InitEmployeeList1()
        {
            string[] Ids = { "1", "2", "3", "4", "5" };
            string[] Name = { "Alexandr", "Alex", "Bond", "Sam", "Mat" };
            string[] Department = { "22", "19", "23", "22", "19" };
            string[] Position = { "Injener", "Finansist", "Specialist", "Nachalnik", "Director" };
            double[] Salary = { 250.63, 200.3, 300.8, 400.8, 600.9 };
            int[] WorkExp = {7,6,3,2,10};
            string[] BirthDate = {"24.06.1990", "19.07.1985", "12.01.1984", "01.01.1984", "14.09.1989"};

            for (int i = 0; i < Ids.Length; i++)
            {
                Employee employee = new Employee()
                {
                    Id = Ids[i],
                    Name = Name[i],
                    Position = Position[i],
                   
                };
                employee.SetDepartment(Department[i]);
                EmployeeList.Add(employee);
            }
        }

        protected static void processCommandDelete()
        {
            Console.WriteLine("Введите ID работника, которого хотите удалить");
            int? index = FindEmplooyeListIndexByID(Console.ReadLine());
            if (index == null)
            {
                Console.WriteLine("Работник с таким табельным ID не найден");
                return;
            }
            else
            {
                EmployeeList.RemoveAt(index ?? default(int));
                Console.WriteLine("Работник удалён");
            }
        }

        protected static void processCommandEdit()
        {
            Console.WriteLine("Введите ID");
            int? index = FindEmplooyeListIndexByID(Console.ReadLine());
            if(index == null)
            {
                Console.WriteLine("Работник с таким табельным ID не найден");
                return;
            }

            Employee employee = EmployeeList[index??default(int)];//получение элемента из списка по индексу
            Console.WriteLine("Введите Имя");
            // Ридлайном считываю следующие данные и сохраняю его в todo
            employee.Name = Console.ReadLine();
            Console.WriteLine("Введите номер отдела");
            try
            {
                employee.SetDepartment(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine($"Ошибка, такого отдела не существует");
            }
            Console.WriteLine("Введите должность");
            employee.Position = Console.ReadLine();
            Console.WriteLine("Введите ЗП");
            employee.Salary = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите рабочий стаж");
            employee.WorkExp = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату Рождения в формате дд.мм.гггг");
            employee.BirthDate = DateTime.Parse(Console.ReadLine());
        }

        protected static void processCommandAdd()
        { 
            Console.WriteLine("Введите ID");
            int? index = FindEmplooyeListIndexByID(Console.ReadLine());
            //if (index == null)
            //{
            //    Console.WriteLine("Работник с таким табельным ID не найден");
            //    return;
            //}

            Employee employee = EmployeeList[index ?? default(int)];
            Console.WriteLine("Введите Имя");
            employee.Name = Console.ReadLine();
            Console.WriteLine("Введите номер отдела");
            try
            {
                employee.SetDepartment(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine($"Ошибка, такого отдела не существует");
            }
            Console.WriteLine("Введите должность");
            employee.Position = Console.ReadLine();
            Console.WriteLine("Введите ЗП");
            employee.Salary = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите рабочий стаж");
            employee.WorkExp = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату Рождения");
            employee.BirthDate = DateTime.Parse(Console.ReadLine());
            EmployeeList.Add(employee);
            Console.WriteLine("Запись добавлена");
        }

    }
    public class Employee
    {
        ///<summary>
        ///табельный номер сотрудника
        ///</summary>
        public string Id { get; set; }

        ///<summary>
        ///Имя сотрудника
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///Номер Отдела
        ///</summary>
        protected string Department { get; set; }

        ///<summary>
        ///Должность
        ///</summary>
        public string Position { get; set; }//to do перевести в Enum

        ///<summary>
        ///Заработная плата в рублях
        ///</summary>
        public double Salary { get; set; }

        ///<summary>
        ///Рабочий стаж в годах
        ///</summary>
        public int WorkExp { get; set; }

        ///<summary>
        ///Дата Рождения
        ///</summary>
        public DateTime BirthDate { get; set; }

        public static string[] Deps = { "19", "22", "23" };

        public Employee()
        {
            
        }

        public void SetDepartment(string dep)
        {
            if (!Deps.Contains(dep))//!-- инверсия условий, т.е. фолс если в депс содержится деп
            {
                throw new System.ArgumentException("Отдел неизвестен", "dep");
            }
             Department = dep;
        }
        
        public string GetDepartment()
        {
            return Department;
        }
    }
}
