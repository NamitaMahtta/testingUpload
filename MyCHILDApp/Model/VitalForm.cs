using System;
namespace MyCHILDApp.Model
{
    public class VitalForm
    {
        public int id { get; set; }
        public string temp { get; set; }
        public string heartRate { get; set; }
        public string respiratoryRate { get; set; }
        public int oxygenSat { get; set; }
        public int painScore { get; set; }
        public int feedingStatus { get; set; }
        public string mentalStatus { get; set; }
        public int seizure { get; set; }
        public string caregiverWorry { get; set; }

        public VitalForm()
        {
        }
    }
}
