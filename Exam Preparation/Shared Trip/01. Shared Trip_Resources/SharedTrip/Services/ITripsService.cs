namespace SharedTrip.Services
{
    using SharedTrip.ViewModels.Trips;
    using System.Collections.Generic;

    public interface ITripsService
    {
        IEnumerable<TripListViewModel> GetAll();

        void Add(TripInputModel tripInput);

        TripListViewModel GetById(string id);

        bool AddUserToTrip(string userId, string tripId);

        bool AlreadyJoinedToTrip(string userId, string tripId);
    }
}
