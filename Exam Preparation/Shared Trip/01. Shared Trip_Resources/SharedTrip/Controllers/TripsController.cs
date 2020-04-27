namespace SharedTrip.Controllers
{
    using SharedTrip.Services;
    using SharedTrip.ViewModels.Trips;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System.Text.RegularExpressions;

    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var tripsAll = this.tripsService.GetAll();

            return this.View(tripsAll);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripInputModel tripInput)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(tripInput.StartPoint) ||
              string.IsNullOrWhiteSpace(tripInput.EndPoint) ||
              string.IsNullOrWhiteSpace(tripInput.DepartureTime) ||
              string.IsNullOrWhiteSpace(tripInput.Description) ||
              string.IsNullOrWhiteSpace(tripInput.Seats))
            {
                return this.View();
            }

            var pattern = @"^([1-9]|([012][0-9])|(3[01])).([0]{0,1}[1-9]|1[012]).\d\d\d\d [012]{0,1}[0-9]:[0-6][0-9]$";
            var match = Regex.Match(tripInput.DepartureTime, pattern);

            if (!match.Success)
            {
                return this.View();
            }

            if (int.Parse(tripInput.Seats) < 2 || int.Parse(tripInput.Seats) > 6)
            {
                return this.View();
            }

            if (tripInput.Description.Length> 80)
            {
                return this.View();
            }

            this.tripsService.Add(tripInput);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var model = this.tripsService.GetById(tripId);

            return this.View(model);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (this.tripsService.AlreadyJoinedToTrip(this.User, tripId))
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            var success = this.tripsService.AddUserToTrip(this.User, tripId);
            if (success)
            {
                return this.Redirect("/");
            }

            return this.Redirect($"/Trips/Details?tripId={tripId}");
        }
    }
}
