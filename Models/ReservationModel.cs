using System;
using System.Collections.Generic;
using System.Text;

namespace RUCSpeciale
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Phone {get; set;}

        public int Postal {get; set;}
        public string AmountOfGuests { get; set; }
        public string Placement { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
    }
}
