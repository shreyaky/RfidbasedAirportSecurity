using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RfidBasedAirportSecurity.Models
{
    public class ZoneAccessMetadata
    {
        // Role	Type/Class	Zone1	Zone2	Zone3
        [Key, Column(Order = 1)]
        public string Role { get; set; }
        [Key, Column(Order = 2)]
        public string Type { get; set; }

        public bool B1 { get; set; }
        public bool C1 { get; set; }
        public bool D3 { get; set; }
        public bool E1 { get; set; }
        public bool F1 { get; set; }
        public bool G1 { get; set; }
    }
}