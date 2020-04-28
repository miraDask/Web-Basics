namespace BattleCards.Services.Cards
{
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;
    using System.Linq;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(CardInputModel input)
        {
            var card = new Card()
            {
                Name = input.Name,
                ImageUrl = input.ImageUrl,
                Keyword = input.Keyword,
                Attack = int.Parse(input.Attack),
                Health = int.Parse(input.Health),
                Description = input.Description,
            };

            this.db.Cards.Add(card);
            this.db.SaveChanges();
        }

        public bool AddToCollection(int cardId, string userId)
        {
            var card = this.db.Cards.FirstOrDefault(x => x.Id == cardId);
            var user = this.db.Users.FirstOrDefault(x => x.Id == userId);

            if (this.db.UsersCards.Any(x => x.CardId == cardId && x.UserId == userId))
            {
                return false;
            }

            var userCard = new UserCard()
            {
                UserId = userId,
                CardId = cardId,
            };

            this.db.UsersCards.Add(userCard);
            this.db.SaveChanges();
            return true;
        }

        public IEnumerable<CardListViewModel> GetAll()
        => this.db.Cards.Select(x => new CardListViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            ImageUrl = x.ImageUrl,
            Keyword = x.Keyword,
            Attack = x.Attack,
            Health = x.Health,
            Description = x.Description,
        }).ToArray();
    }
}
