namespace HabitTrackerApp
{
    public class Habit
    {
        public string Name { get; set; }
        public int Streak { get; set; }
        public bool IsCompletedToday { get; set; }

        public string ToCsvString()
        {
            return $"{Name};{Streak};{IsCompletedToday}";
        }

        public static Habit FromCsvString(string csvLine)
        {
            var parts = csvLine.Split(';');
            if (parts.Length != 3) return null;

            if (int.TryParse(parts[1], out int streak) &&
                bool.TryParse(parts[2], out bool isCompleted))
            {
                return new Habit
                {
                    Name = parts[0],
                    Streak = streak,
                    IsCompletedToday = isCompleted
                };
            }

            return null;
        }
    }
}