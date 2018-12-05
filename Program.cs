using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        //здесь объявляется свойство класса Program под названием TodoList который имеет тип
        //List объектов Todo и тут же этот список(List<todo>)инициализируется
        protected static List<Todo> TodoList = new List<Todo>();

        static void Main(string[] args)
        {
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

            Console.WriteLine("Введите одну из доступных команд: \nList - показать список дел \nAdd - добавить дело \nDelete - удалить дело \nEdit - редактирование дела \nExit - выход из программы ");
            return EnterCommand(Console.ReadLine());
        }

        //static void Main2(string[] args)
        //{
        //    string EnterCommand;
        //    do
        //    {
        //        Console.WriteLine("Введите одну из доступных команд: \nList - показать список дел \nAdd - добавить дело \nDelete - удалить дело \nEdit - редактирование дела \nExit - выход из программы ");
        //        EnterCommand = Console.ReadLine();// запись в переменную enter, то что написал в консоли
        //        switch (EnterCommand) //в зависимости что введено, выполняется 1 из 4х функций ниже
        //        {
        //            case "List":
        //                processCommandList();
        //                break;
        //            case "Add":
        //                processCommandAdd();
        //                break;
        //            case "Delete":
        //                processCommandDelete();
        //                break;
        //            case "Edit":
        //                processCommandEdit();
        //                break;
        //            case "Exit":
        //                break;
        //            default:
        //                Console.WriteLine("Введённая команда '{0}' не существует", EnterCommand);
        //                break;
        //        }
        //    }
        //    while (EnterCommand != "Exit");
        //}

        protected static void processCommandDelete()
        {
            Console.WriteLine("Введите ID дела, который хотите удалить");
            int index = 0; //Чтобы не было ошибки отсутсвия переменной
            try
            {
                //Считываем индекс редактирования 
                index = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                //Если индекс не существует выводим сообщение об ошибке
                Console.WriteLine($"Ошибка, введено невалидное значение {index}");
                return;
            }

            //Если индекс существует удалить элемент с индексом удаления
            if (TodoList.Count > index && index >= 0)
            {
                Todo todo = TodoList[index];
                Console.WriteLine("Дело удалено");
                TodoList.RemoveAt(index);
            }
            else
            { Console.WriteLine("Данного индекса не существует"); }
        }

        protected static void processCommandEdit()
        {
            Console.WriteLine("Введите ID");
            int index = 0; //Чтобы не было ошибки отсутсвия переменной
            try
            {
                //Считываем индекс редактирования 
                index = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                //Если индекс не существует вывести сообщение об ошибке
                Console.WriteLine($"Ошибка, введено невалидное значение {index}");
                return;
            }
            //TodoList.Count -- количество элементов в списке,Если TodoList.Count больше вводимого значения с клавы и это значение больше-равно нулю то выполняется следующее
            if (TodoList.Count > index && index >= 0)
            {
                Todo todo = TodoList[index];//получение элемента из списка по индексу
                Console.WriteLine("Введите Description");
                // Ридлайном считываю Description и сохраняю его в todo
                todo.Description = Console.ReadLine();
                Console.WriteLine("Введите статус выполнения дела \n(1 для выполнено и 0 для невыполнено):");
                string EnterIsFinished = Console.ReadLine();
                if (EnterIsFinished == "0" || EnterIsFinished == "1")
                {
                    //Ридлайном считываю IsFinished и сохраняю его в todo
                    todo.IsFinished = EnterIsFinished == "1";// тру если 1, если другое то фолс
                    Console.WriteLine("Дело отредактировано.");
                }
                else
                {
                    Console.WriteLine("Введенный статус выполнения невалиден.");
                }
            }
            else
            { Console.WriteLine("Данного индекса не существует"); }
        }

        protected static void processCommandAdd()
        {
            //создаю новый объект Todo
            Todo todo = new Todo();
            Console.WriteLine("Введите Description");
            // Ридлайном считываю Description и сохраняю его в todo
            todo.Description = Console.ReadLine();
            //Добавляю новый объект Todo в List
            TodoList.Add(todo);
            Console.WriteLine("Запись добавлена");
        }

        protected static void processCommandList()
        {

            int i = 0;
            foreach (Todo item in TodoList)
            {
                string IsFinishedLine = item.IsFinished ? "Выполнено" : "Не выполнено";
                Console.WriteLine($"ID:{i++}, Description: {item.Description}, Дата создания:{item.CreatedDate}, Статус выполнения:{IsFinishedLine}");
            }
            Console.WriteLine("\n");
        }
    }
}