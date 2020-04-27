namespace SharedTrip.Models
{
    public class UserTrip
    {
        public virtual User User { get; set; }

        public string UserId { get; set; }

        public virtual Trip Trip { get; set; }

        public string TripId { get; set; }
    }
}