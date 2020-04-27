namespace SharedTrip.Services
{
    using SharedTrip.Models;
    using SharedTrip.ViewModels.Trips;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(TripInputModel tripInput)
        {
            var trip = new Trip()
            {
                StartPoint = tripInput.StartPoint,
                EndPoint = tripInput.EndPoint,
                Description = tripInput.Description,
                DepartureTime = DateTime.Parse(tripInput.DepartureTime).ToUniversalTime(),
                ImagePath = tripInput.ImagePath,
                Seats = int.Parse(tripInput.Seats),
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public bool AddUserToTrip(string userId, string tripId)
        {
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);
            var trip = this.db.Trips.FirstOrDefault(x => x.Id == tripId);

            if (trip.Seats == 0)
            {
                return false;
            }

            var userTrip = new UserTrip
            {
                UserId = user.Id,
                TripId = trip.Id
            };

            trip.Seats--;
            this.db.UsersTrips.Add(userTrip);
            this.db.SaveChanges();

            return true;
        }

        public IEnumerable<TripListViewModel> GetAll()
        => this.db.Trips.Select(x => new TripListViewModel()
        {
            Id = x.Id,
            StartPoint = x.StartPoint,
            EndPoint = x.EndPoint,
            DepartureTime = x.DepartureTime.ToLocalTime().ToString("dd.MM.yyyy HH: mm"),
            Seats = x.Seats,
        }).ToArray();

        public TripListViewModel GetById(string id)
          => this.db.Trips
            .Where(x => x.Id == id)
            .Select(x => new TripListViewModel()
          {
              Id = x.Id,
              StartPoint = x.StartPoint,
              EndPoint = x.EndPoint,
              DepartureTime = x.DepartureTime.ToLocalTime().ToString("dd.MM.yyyy HH: mm"),
              Seats = x.Seats,
              ImagePath = x.ImagePath,
              Description = x.Description
          }).FirstOrDefault();

        public bool AlreadyJoinedToTrip(string userId, string tripId)
        => this.db.UsersTrips.Any(x => x.UserId == userId && x.TripId == tripId);
    }
}
