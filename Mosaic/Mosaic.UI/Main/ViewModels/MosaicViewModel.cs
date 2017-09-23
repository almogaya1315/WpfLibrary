using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Mosaic.UI.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;

namespace Mosaic.UI.Main.ViewModels
{
    public class MosaicViewModel : ViewModelBase
    {
        private int _cardsCount;
        private Dictionary<CardType, CardViewModel> _moveableCards;

        public string RangeValue { get; set; }

        private int _rows;
        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                RaisePropertyChanged();
            }
        }

        public string Message
        {
            get
            {
                string min = string.Empty; ;
                int startIndex = 0;

                foreach (var c in RangeValue)
                {
                    if (!Equals(c.ToString(), " ")) min += c;
                    else
                    {
                        startIndex = RangeValue.IndexOf(c) + 1;
                        break;
                    }
                }

                var length = RangeValue.Count() - startIndex;
                var max = RangeValue.Substring(startIndex, length);
                return $"Enter number of rows ({min} - {max})";
            }
        }

        private List<CardViewModel> _cards;
        public List<CardViewModel> Cards
        {
            get { return _cards; }
            set
            {
                _cards = value;
                RaisePropertyChanged();
            }
        }

        #region
        /*
        private CardViewModel _emptyCard;
        public CardViewModel EmptyCard
        {
            get { return _emptyCard; }
            set
            {
                _emptyCard = value;
                RaisePropertyChanged();
            }
        }
        private CardViewModel _leftCard;
        public CardViewModel LeftCard
        {
            get { return _leftCard; }
            set
            {
                _leftCard = value;
                RaisePropertyChanged();
            }
        }
        private CardViewModel _rightCard;
        public CardViewModel RightCard
        {
            get { return _rightCard; }
            set
            {
                _rightCard = value;
                RaisePropertyChanged();
            }
        }
        private CardViewModel _upCard;
        public CardViewModel UpCard
        {
            get { return _upCard; }
            set
            {
                _upCard = value;
                RaisePropertyChanged();
            }
        }
        private CardViewModel _downCard;
        public CardViewModel DownCard
        {
            get { return _downCard; }
            set
            {
                _downCard = value;
                RaisePropertyChanged();
            }
        }
        */
        #endregion

        private int _attempts;
        public int Attempts
        {
            get { return _attempts; }
            set
            {
                _attempts = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Start { get; set; }
        public ICommand MoveCard { get; set; }

        public MosaicViewModel()
        {
            RangeValue = "2 10";

            Cards = new List<CardViewModel>();
            _moveableCards = new Dictionary<CardType, CardViewModel>();

            Start = new RelayCommand(StartNewGame, () => Rows > 0);
            MoveCard = new RelayCommand<int>(MoveCardToEmpty);
        }

        private void StartNewGame()
        {
            Cards.Clear();
            Attempts = 0;

            _cardsCount = Convert.ToInt32(Math.Pow(Rows, 2));
            for (int i = 1; i < _cardsCount; i++)
            {
                Cards.Add(new CardViewModel()
                {
                    Value = i,
                    TemplateName = "VisibleCardTemplate",
                    DataContextPath = "Value",
                    Type = CardType.None,
                });
            }
            Cards.Add(new CardViewModel() { Value = 0 });

            Cards.Shuffle();

            FindMoveableCards();
        }

        private void FindMoveableCards()
        {
            var emptyCard = _moveableCards[CardType.EmptyCard] = Cards.First(c => c.Value == 0);
            emptyCard.TemplateName = "HiddenCardTemplate";
            emptyCard.Type = CardType.EmptyCard;
            emptyCard.Index = Cards.IndexOf(emptyCard);

            var leftCard = SetMoveableCard(CardType.LeftCard, emptyCard.Index - 1, emptyCard.Index, 0, _cardsCount, Rows);

            var rightCard = SetMoveableCard(CardType.RightCard, emptyCard.Index + 1, emptyCard.Index, Rows - 1, _cardsCount, Rows);

            var upCard = SetMoveableCard(CardType.UpCard, emptyCard.Index - Rows, emptyCard.Index, 0, Rows - 1, 1);

            var downCard = SetMoveableCard(CardType.DownCard, emptyCard.Index + Rows, emptyCard.Index, _cardsCount - Rows, _cardsCount, 1);

            foreach (var card in Cards)
            {
                if (card.Value == emptyCard.Value) continue;
                if (upCard != null) if (card.Value == upCard.Value) continue;
                if (downCard != null) if (card.Value == downCard.Value) continue;
                if (leftCard != null) if (card.Value == leftCard.Value) continue;
                if (rightCard != null) if (card.Value == rightCard.Value) continue;

                card.TemplateName = "VisibleCardTemplate";
                card.Type = CardType.None;
                card.Index = Cards.IndexOf(card);
            }

            var cards = Cards;
            Cards = new List<CardViewModel>(cards);
        }

        private CardViewModel SetMoveableCard(CardType type, int cardIndex, int emptyIndex, int indexStart, int indexCondition, int indexAddition)
        {
            bool hasValue = true;
            for (int i = indexStart; i <= indexCondition; i += indexAddition)
            {
                if (emptyIndex != i) continue;
                else
                {
                    hasValue = false;
                    break;
                }
            }
            var card = _moveableCards[type] = hasValue ? Cards.ElementAt(cardIndex) : null;
            if (card != null)
            {
                card.TemplateName = "MoveableCardTemplate";
                card.Type = type;
                card.Index = Cards.IndexOf(card);
            }

            return card;
        }

        private void MoveCardToEmpty(int value)
        {
            var card = Cards.First(c => c.Value == value);
            var emptyCard = Cards.First(c => c.Type == CardType.EmptyCard);

            Cards.RemoveAt(card.Index);
            Cards.Insert(card.Index, emptyCard);

            Cards.RemoveAt(emptyCard.Index);
            Cards.Insert(emptyCard.Index, card);

            FindMoveableCards();

            Attempts++;

            CheckValuesOdrer();
        }

        private void CheckValuesOdrer()
        {
            if (Cards.Last().Type == CardType.EmptyCard)
            {
                for (int i = 0; i < _cardsCount; i++)
                {
                    if (Cards[i].Value == 0) continue;

                    if (Cards[i].Value == Cards[_cardsCount - 2].Value) continue;

                    if (Cards[i].Value == Cards[i + 1].Value - 1) continue;

                    return;
                }

                MessageBox.Show($"Total moves: {Attempts}", "Game completed!", MessageBoxButton.OK);

                Cards = new List<CardViewModel>();
                Rows = 0;
                Attempts = 0;
            }
        }
    }
}
