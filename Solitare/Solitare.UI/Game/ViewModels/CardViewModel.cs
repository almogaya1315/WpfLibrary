using GalaSoft.MvvmLight;
using Solitare.UI.Enums;

namespace Solitare.UI.Game.ViewModels
{
    public class CardViewModel : ViewModelBase, IMoveable
    {
        public CardViewModel()
        {

        }

        public CardViewModel(CardViewModel card)
        {
            CardValue = card.CardValue;
            CardShape = card.CardShape;
            CardName = card.CardName;
            CurrentDeck = card.CurrentDeck;
            CardPath = card.CardPath;
        }

        public DeckName? CurrentDeck { get; set; }

        private string _cardPath;
        public string CardPath
        {
            get { return _cardPath; }
            set
            {
                _cardPath = value;
                RaisePropertyChanged();
            }
        }

        public string FrontCardPath { get; set; }

        public CardName? CardName { get; set; }

        public CardShape? CardShape { get; set; }

        public int CardValue { get; set; }
    }
}
