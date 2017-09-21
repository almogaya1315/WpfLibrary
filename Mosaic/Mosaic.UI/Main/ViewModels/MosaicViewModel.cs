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

            Start = new RelayCommand(StartNewGame, CanStart);
        }

        private void StartNewGame()
        {
            Cards.Clear();

            var cardsCount = Math.Pow(Rows, 2);
            for (int i = 1; i < cardsCount; i++)
            {
                Cards.Add(new CardViewModel()
                {
                    Value = i,
                    TemplateName = "VisibleCardTemplate",
                    DataContextPath = "Value",
                });
            }
            Cards.Add(new CardViewModel() { TemplateName = "HiddenCardTemplate" });

            Cards = Cards.Shuffle().ToList();
        }

        private bool CanStart()
        {
            return Rows > 0;
        }
    }
}
