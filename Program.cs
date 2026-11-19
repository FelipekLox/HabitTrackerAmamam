using System;
using System.Collections.Generic;
using System.Text;

namespace HabitTrackerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var manager = new HabitManager("habits.txt");
            manager.LoadHabits();

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== HABIT TRACKER ===");
                PrintHabits(manager.Habits);

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Добавить новую привычку");
                Console.WriteLine("2. Отметить привычку выполненной сегодня");
                Console.WriteLine("0. Сохранить и выйти");
                Console.Write("\nВаш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите название привычки: ");
                        string name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            manager.AddHabit(name.Trim().Replace(";", ","));
                        }
                        break;

                    case "2":
                        if (manager.Habits.Count == 0)
                        {
                            Console.WriteLine("Список привычек пуст. Нажмите Enter...");
                            Console.ReadLine();
                            break;
                        }
                        Console.Write("Введите номер выполненной привычки: ");
                        if (int.TryParse(Console.ReadLine(), out int num) && manager.CompleteHabit(num - 1))
                        {
                            Console.WriteLine("Отлично! Прогресс обновлён.");
                        }
                        else
                        {
                            Console.WriteLine("Некорректный номер.");
                            Console.ReadLine();
                        }
                        break;

                    case "0":
                        manager.SaveHabits();
                        Console.WriteLine("Данные сохранены в habits.txt. До встречи!");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда. Нажмите Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void PrintHabits(List<Habit> habits)
        {
            if (habits.Count == 0)
            {
                Console.WriteLine("Пока нет ни одной привычки.");
                return;
            }

            for (int i = 0; i < habits.Count; i++)
            {
                var h = habits[i];
                string status = h.IsCompletedToday ? "[Выполнено]" : "[Не выполнено]";
                Console.WriteLine($"{i + 1}. {h.Name} | Серия: {h.Streak} дн. | {status}");
            }
        }
    }
}