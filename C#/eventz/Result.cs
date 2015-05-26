using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz
{
    public class Result
    {
        public Result(string header, string desc, int level, int confidence, int id)
        {
            Header = header;
            Description = desc;
            Level = level;
            Confidence = confidence;
            Id = id;
        }

        public string Header;
        public string Description;

        public int Level;
        public int Confidence;

        public int Id;
    }
}
