using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaAPI.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Abb { get; set; }
        public string Arena { get; set; }
        public DateTime? Founded { get; set; }
        public string CoachName { get; set; }
    }
}
