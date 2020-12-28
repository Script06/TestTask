using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    class UI
    {
       public void menu()
        {
            Console.WriteLine(new string('-', 30));
            Console.WriteLine("1.добавить задачу");
            Console.WriteLine("2.Отобразить текущие задачи");
            Console.WriteLine("3.удалить задачу");
            Console.WriteLine("4.редактировать задачу");
            Console.WriteLine("5.Очистить окно консоли");
            Console.WriteLine("6.Показать содержимое БД");
            Console.WriteLine("7.сохранить в БД");
            Console.WriteLine("8.Загрузить из БД");
            Console.WriteLine("для выхода напишите exit");
        }
    }
}
