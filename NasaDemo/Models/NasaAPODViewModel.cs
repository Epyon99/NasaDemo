using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasaDemo.Models
{
    public class NasaAPODViewModel
    {
        public string Copyright { get; set; }
        public string Date { get; set; }
        public string Explanation { get; set; }
        public string hdurl { get; set; }
        public string Media_Type { get; set; }
        public string Service_Version { get; set; }
        public string Url { get; set; }
    }
}