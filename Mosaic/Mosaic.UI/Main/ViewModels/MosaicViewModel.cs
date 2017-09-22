using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Mosaic.UI.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Mosaic.UI.Main.ViewModels
{
    public class MosaicViewModel : ViewModelBase
    {
        private int _cardsCount;

        public string RangeValue { get; set; }
        public int Rows { get; set; }

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

        public MosaicViewModel()
        {
            RangeValue = "4 10";

            Cards = new List<CardViewModel>();

            Start = new RelayCommand(StartNewGame, () => Rows > 0);
        }

        private void StartNewGame()
        {
            Cards.Clear();

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
            Cards.Add(new CardViewModel()
            {
                TemplateName = "HiddenCardTemplate",
                Type = CardType.None,
            });

            Cards = Cards.Shuffle().ToList();

            FindMoveableCards();

            
            
            
            
        }

        private void FindMoveableCards()
        {
            EmptyCard = Cards.First(c => c.Value == 0);
            EmptyCard.TemplateName = "HiddenCardTemplate";
            EmptyCard.Type = CardType.None;
            var emptyIndex = Cards.IndexOf(EmptyCard);

            bool isLeft = true;
            for (int i = 0; i < _cardsCount; i += Rows)
            {
                if (emptyIndex != i) continue;
                else
                {
                    isLeft = false;
                    break;
                }
            }
            LeftCard = isLeft ? Cards.ElementAt(emptyIndex - 1) : null;
            if (LeftCard != null)
            {
                LeftCard.TemplateName = "MoveableCardTemplate"; // Cards.First(c => c.Value == LeftCard.Value)
                LeftCard.Type = CardType.LeftCard;
                var leftIndex = Cards.IndexOf(LeftCard);
            }

            bool isRight = true;
            for (int i = Rows - 1; i < _cardsCount; i += Rows)
            {
                if (emptyIndex != i) continue;
                else
                {
                    isRight = false;
                    break;
                }
            }
            RightCard = isRight ? Cards.ElementAt(emptyIndex + 1) : null;
            if (RightCard != null)
            {
                RightCard.TemplateName = "MoveableCardTemplate"; // Cards.First(c => c.Value == RightCard.Value)
                RightCard.Type = CardType.RightCard;
                var rightIndex = Cards.IndexOf(RightCard);
            }

            bool isUp = true;
            for (int i = 0; i <= Rows - 1; i++)
            {
                if (emptyIndex != i) continue;
                else
                {
                    isUp = false;
                    break;
                }
            }
            UpCard = isUp ? Cards.ElementAt(emptyIndex + 1) : null;
            if (UpCard != null)
            {
                UpCard.TemplateName = "MoveableCardTemplate"; // Cards.First(c => c.Value == UpCard.Value)
                UpCard.Type = CardType.UpCard;
                var upIndex = Cards.IndexOf(UpCard);
            }

            bool isDown = true;
            for (int i = _cardsCount - Rows; i < _cardsCount; i++)
            {
                if (emptyIndex != i) continue;
                else
                {
                    isDown = false;
                    break;
                }
            }
            DownCard = isDown ? Cards.ElementAt(emptyIndex + 1) : null;
            if (DownCard != null)
            {
                DownCard.TemplateName = "MoveableCardTemplate"; // Cards.First(c => c.Value == DownCard.Value)
                DownCard.Type = CardType.DownCard;
                var downIndex = Cards.IndexOf(DownCard);
            }

            foreach (var card in Cards)
            {
                if (card.Value == UpCard.Value || card.Value == DownCard.Value ||
                    card.Value == LeftCard.Value || card.Value == RightCard.Value || card.Value == EmptyCard.Value)
                {
                    card.TemplateName = "VisibleCardTemplate";
                    card.Type = CardType.None;
                }
            }
        }
    }
}
