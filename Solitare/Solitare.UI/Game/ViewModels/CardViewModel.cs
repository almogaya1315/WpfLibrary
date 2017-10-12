using GalaSoft.MvvmLight;
using Solitare.UI.Enums;

namespace Solitare.UI.Game.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public CardViewModel()
        {

        }

        public CardViewModel(CardViewModel card)
        {
            Value = card.Value;
            Shape = card.Shape;
            Name = card.Name;
            CurrentDeck = card.CurrentDeck;
            Path = card.Path;
        }

        public int Value { get; set; }

        public CardShape? Shape { get; set; }

        public CardName? Name { get; set; }

        public DeckName CurrentDeck { get; set; }

        public string Path { get; set; }
    }
}
