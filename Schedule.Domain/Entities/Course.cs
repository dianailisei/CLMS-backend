namespace Schedule.Domain.Entities
{
    public abstract class Course : Entity
    {
        public string Name { get; set; }
        public string Weekday { get; set; }
        public short StartHour { get; set; }
        public short EndHour { get; set; }
    }
}
