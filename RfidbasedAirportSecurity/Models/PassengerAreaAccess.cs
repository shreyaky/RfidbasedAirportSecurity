using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RfidBasedAirportSecurity.Models
{
    public class PassengerAreaAccess
    {
        
        public string ZoneId { get; set; }  // Primary Key

        [Key]
        public string RFID_ID { get; set; }
        

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]  // similar to SYSDATE
        public DateTime? Access_Time { get; set; }

    }
}