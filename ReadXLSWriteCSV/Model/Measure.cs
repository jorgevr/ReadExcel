using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadXLSWriteCSV.Model
{
    public class Measure
    {
     
        public string Plant { get; set; }
        public string Source { get; set; }
        public string Datavariable { get; set; }
        public string Resolution { get; set; }
        public string Utcdate { get; set; }
        public double Value { get; set; }
        public double Measurepercentage { get; set; }
        public int Reliabilitytype { get; set; }

        private Measure (string plant, string source, string datavariable, string utcdate
            , double value, double percentage, int reliability, string resolution)
        {
            Plant = plant;
            Source = source;
            Datavariable = datavariable;
            Utcdate = utcdate;
            Value = value;
            Measurepercentage = percentage;
            Reliabilitytype = reliability;
            Resolution = resolution;
        }

        public static Measure Create(string plant, string source, string datavariable, string utcdate
            , double value, double percentage, int reliability, string resolution)
        {
            return new Measure(plant, source, datavariable, utcdate, value, percentage, reliability, resolution);
        }

    }
}
