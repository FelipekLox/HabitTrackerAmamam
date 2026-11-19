using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HabitTrackerApp
{
    public class HabitManager
    {
        private readonly string _filePath;
        public List<Habit> Habits { get; private set; }

        public HabitManager(string filePath = "habits.txt")
        {
            _filePath = filePath;
            Habits = new List<Habit>();
        }

        public void LoadHabits()
        {
            Habits.Clear();

            if (!File.Exists(_filePath))
            {
                return;
            }

            using (var reader = new StreamReader(_filePath, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var habit = Habit.FromCsvString(line);
                    if (habit != null)
                    {
                        Habits.Add(habit);
                    }
                }
            }
        }

        public void SaveHabits()
        {
            using (var writer = new StreamWriter(_filePath, false, Encoding.UTF8))
            {
                foreach (var habit in Habits)
                {
                    writer.WriteLine(habit.ToCsvString());
                }
            }
        }

        public void AddHabit(string name)
        {
            Habits.Add(new Habit
            {
                Name = name,
                Streak = 0,
                IsCompletedToday = false
            });
        }

        public bool CompleteHabit(int index)
        {
            if (index < 0 || index >= Habits.Count) return false;

            var habit = Habits[index];
            if (!habit.IsCompletedToday)
            {
                habit.IsCompletedToday = true;
                habit.Streak++;
            }
            return true;
        }
    }
}