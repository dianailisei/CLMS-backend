using System;
using Schedule.Domain.Entities;

namespace Schedule.Business.Laboratory
{
    public class LaboratoryDetailsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Weekday { get; set; }
        public short StartHour { get; set; }
        public short EndHour { get; set; }
        public string Group { get; set; }
        public Teacher Teacher { get; set; }
    }
}
