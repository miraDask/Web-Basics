namespace BattleCards.Controllers
{
    using BattleCards.Services.Cards;
    using BattleCards.ViewModels.Cards;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cardsAll = this.cardsService.GetAll();
            return this.View(cardsAll);
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
        public HttpResponse Add(CardInputModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (input.Name.Length < 5 || input.Name.Length > 15 || string.IsNullOrEmpty(input.Name))
            {
                return this.View();
            }

            if ( string.IsNullOrEmpty(input.ImageUrl) || string.IsNullOrEmpty(input.Keyword))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(input.Attack) || int.Parse(input.Attack) < 0)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(input.Health) || int.Parse(input.Health) < 0)
            {
                return this.View();
            }

            if (input.Description.Length > 200 || string.IsNullOrEmpty(input.Description))
            {
                return this.View();
            }

            this.cardsService.Add(input, this.User);

            return this.Redirect("/");
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var success = this.cardsService.AddToCollection(cardId, this.User);
            if (success)
            {
                // I think that is more logical to redirect to the card collection so user can see that the card is added;
                // Just in case, i will add commented code below :
                // return this.Redirect("/Cards/Collection");
                return this.Redirect("/Cards/All");
            }

            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var success = this.cardsService.RemoveFromCollection(cardId, this.User);
            if (success)
            {
                return this.Redirect("/Cards/Collection");
            }

            return this.Redirect("/Cards/Collection");
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userCollection = this.cardsService.GetAll(this.User);
            return this.View(userCollection);
        }
    }
}
