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
    class Program
    {
        
        static void Main(string[] args)
        {
            UI ui = new UI();
            List<task> tasks = new List<task>();
            SqlConnection sqlConnection = null;
            string command = string.Empty;
            sqlConnection = Database.ConnectDB();
            while (true)
            {
                try
                {
                    ui.menu();
                    Console.Write(">");
                    command = Console.ReadLine();

                    #region Exit
                    if (command.ToLower().Equals("exit"))
                    {
                        /*if (sqlConnection.State == ConnectionState.Open)
                        {
                            sqlConnection.Close();
                        }
                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();
                        }*/
                        break;
                    }
                    #endregion

                    

                    switch (command)
                    {
                        case "1":
                            task.AddTask(tasks);
                            break;
                        case "2":
                            Console.Clear();
                            task.DisplayTasks(tasks);
                            break;
                        case "3":
                            Console.Clear();
                            task.DisplayTasks(tasks);
                            task.DeleteTask(tasks);
                            break;
                        case "4":
                            task.DisplayTasks(tasks);
                            task.EditTasks(tasks);
                            break;
                        case "5":
                            Console.Clear();
                            break;
                        case "6"://прочитать БД
                            Console.Clear();
                            task.ShowDB(sqlConnection);
                            break;
                        case "7"://сохранить в БД
                            Console.Clear();
                            if (tasks.Count != 0)
                            {
                                task.PushToDB(tasks, sqlConnection);
                            }
                            else
                            {
                                Console.WriteLine("Нечего сохранять! Сначала создайте список задач");
                            }
                            
                            break;
                        case "8"://загрузить из БД
                            Console.Clear();
                            task.LoadDB(tasks, sqlConnection);
                            if (tasks.Count != 0)
                            {
                                Console.WriteLine("Загрузка данных произошла успешно");
                            }
                            break;
                        default:
                            Console.WriteLine("выбранного пункта нет в списке");
                            break;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка:{ex.Message}");
                }
            }

            Console.ReadKey();
        }
    }
}
