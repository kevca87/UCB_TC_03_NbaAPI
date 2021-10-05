using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NbaAPI.Models
{
    public class TeamModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abb { get; set; }
        public string Arena { get; set; }
        public DateTime? Founded { get; set; }
        public string CoachName { get; set; }
        public int Season_wins { get; set; }
        public int Season_loses { get; set; }

    }
}
