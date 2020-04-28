namespace BattleCards.Services.Cards
{
    using BattleCards.ViewModels.Cards;
    using System.Collections.Generic;

    public interface ICardsService
    {
        IEnumerable<CardListViewModel> GetAll();

        void Add(CardInputModel input);

        bool AddToCollection(int cardId, string userId);
    }
}
