namespace BattleCards.Services.Cards
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        IEnumerable<CardListViewModel> GetAll(string userId = null);

        void Add(CardInputModel input, string userId);

        bool AddToCollection(int cardId, string userId);

        bool RemoveFromCollection(int cardId, string userId);
    }
}
