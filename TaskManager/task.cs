using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TaskManager
{
    public class task
    {
       private int id;
       private string taskText;
       private string date; //про тип DateTime знаю - лень было заморачиваться :)

        static public void AddTask(List<task> tasks)
        {
            int i = tasks.Count;
            char cont = 's' ;
            do
            {
                var buf = "empty";
                Console.WriteLine("Введите текст задачи");
                tasks.Add(new task() { taskText = Console.ReadLine() });
                tasks[i].id = i + 1;//id для пользователя.В List'e другой id!
                Console.WriteLine("Введите дату выполнения задачи");
                tasks[i].date = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Добавить еще одну задачу? n - для завершения добавления; y - для продолжения");

                //дальше кусок велосипеда, чтоб пользователь точно ввёл y или n
                do
                {
                    buf = Console.ReadLine();
                    if (buf.Length == 1 & buf is string)
                    {
                        cont = Convert.ToChar(buf);
                    }

                    if (cont != 'y' && cont != 'n')
                    {
                        Console.WriteLine("Не понял? давай ещё раз - y или n :)");
                    }
                } while (cont != 'y' && cont != 'n');
                //конец велосипеда
                i++;

            }
            while (cont != 'n');
            Console.Clear();
        }
        
        public static void DisplayTasks (List<task> tasks)
        {
            foreach (task aTask in tasks)
            {
                Console.WriteLine($"ID: {aTask.id}.  задание: {aTask.taskText} Дата: {aTask.date}");
                Console.WriteLine();
            }

        }
        
        static public void DeleteTask(List<task> tasks)
        {
            Console.WriteLine("Введите ID удаляемой задачи");
            int i = Convert.ToInt32(Console.ReadLine());
            i--;
            tasks.RemoveAt(i);
            for (; i < tasks.Count; i++)//для перенумерации id задачи отображаемые пользователю;
            {
                tasks[i].id = i + 1;
            }
        }

        static public void EditTasks(List<task> tasks)
        {
            Console.WriteLine("Введите номер редактируемой задачи");
            int i = Convert.ToInt32(Console.ReadLine());
            i--;//т.к. ID отображаемые пользователю начинаются с 1, а list с 0.
            Console.WriteLine("Введите текст задачи");
            tasks[i].taskText = Console.ReadLine();
            tasks[i].id = i + 1;
            Console.WriteLine("Введите дату выполнения задачи");
            tasks[i].date = Convert.ToString(Console.ReadLine());
        }

        /*МЕТОДЫ ДЛЯ РАБОТЫ С БД*/
        public static void ShowDB(SqlConnection sqlConnection)
        {
            SqlDataReader sqlDataReader = null;//объект, с помощью которого будем делать выборки из базы данных

            SqlCommand sqlCommand = new SqlCommand("select * from [Tasks]", sqlConnection);//создаем запрос select * from [Tasks] и через sql пихает в sqlcommand

            sqlDataReader = sqlCommand.ExecuteReader();

            

            while (sqlDataReader.Read())
            {
                Console.WriteLine($"ID:{sqlDataReader["Id"]} задание: {sqlDataReader["TaskText"]} Дата: {sqlDataReader["Date"]}");
                Console.WriteLine(new string('-', 30)); 
            }

            if (sqlDataReader != null)
            {
                sqlDataReader.Close();
            }

        }

        public static void PushToDB(List<task> tasks, SqlConnection sqlConnection)
        {
           
            SqlCommand ClearTable = new SqlCommand($"TRUNCATE TABLE tasks", sqlConnection);
            ClearTable.ExecuteNonQuery();
            foreach (task aTask in tasks)
            {
                SqlCommand sqlCommand = new SqlCommand($"insert into tasks (TaskText, Date) values (N'{aTask.taskText}', N'{aTask.date}')", sqlConnection);
                
                sqlCommand.ExecuteNonQuery();
            }
            Console.WriteLine("сохранение произошло успешно!");

        }

        public static void LoadDB(List<task> tasks, SqlConnection sqlConnection)
        {
            int i;
            tasks.Clear();
            SqlDataReader sqlDataReader = null;//объект, с помощью которого будем делать выборки из базы данных

            SqlCommand sqlCommand = new SqlCommand("select * from [Tasks]", sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader();
            
            while (sqlDataReader.Read())
            {
                i = Convert.ToInt32(sqlDataReader["Id"]);
                i--;
                tasks.Add(new task() { });
                tasks[i].id = i + 1;
                tasks[i].taskText = Convert.ToString(sqlDataReader["TaskText"]);
                tasks[i].date = Convert.ToString(sqlDataReader["Date"]);
                Console.WriteLine($"{sqlDataReader["Id"]} {sqlDataReader["TaskText"]} {sqlDataReader["Date"]}");
                Console.WriteLine(new string('-', 30)); 
            }

            if (sqlDataReader != null)
            {
                sqlDataReader.Close();
            }

        }



    }


}
