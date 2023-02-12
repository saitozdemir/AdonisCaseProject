using System;

namespace AdonisCaseProject.WebAPI.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string GuestName { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set;}
        public bool Status { get; set; }
    }
}
