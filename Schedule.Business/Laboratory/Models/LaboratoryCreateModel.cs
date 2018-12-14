
namespace Schedule.Business.Laboratory
{
    public class LaboratoryCreateModel
    {
        public string Name { get; set; }

        public string Group { get; set; }

        public string Weekday { get; set; }

        public short StartHour { get; set; }

        public short EndHour { get; set; }
    }
}
