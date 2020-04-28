namespace BattleCards.Controllers
{
    using BattleCards.Services.Cards;
    using BattleCards.ViewModels.Cards;
    using SIS.HTTP;
    using SIS.MvcFramework;
    using System;
    using System.Collections.Generic;
    using System.Text;

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

            // TODO VALIDATION

            this.cardsService.Add(input);

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
                // Just in case, i will add commented code below so you can see that i understood the task,
                // but it doesn't seem logocal to me to redirect to the same page if succeed or not;
                return this.Redirect("/Cards/Collection");
                // return this.Redirect("/Cards/All");
            }

            return this.Redirect("/Cards/All");
        }
    }
}
