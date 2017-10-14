using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;

namespace Solitare.UI.Controls.Image
{
    public class Card : System.Windows.Controls.Image //, IReflect
    {
        public bool isOverOpenDeck { get; set; }
        public bool IsOverDiamondsDeck { get; set; }
        public bool IsOverClubsDeck { get; set; }
        public bool IsOverHeartsDeck { get; set; }
        public bool IsOverSpadesDeck { get; set; }
        public bool IsOverFirstDeck { get; set; }
        public bool IsOverSecondDeck { get; set; }
        public bool IsOverThirdDeck { get; set; }
        public bool IsOverFourthDeck { get; set; }
        public bool IsOverFifthDeck { get; set; }
        public bool IsOverSixthDeck { get; set; }
        public bool IsOverSeventhDeck { get; set; }

        public string FrontCardPath { get; set; }

        public Card()
        {

        }

        public Card(Card card)
        {
            Path = card.Path;
            CardName = card.CardName;
            CardShape = card.CardShape;
            CardValue = card.CardValue;
            CurrentDeck = card.CurrentDeck;
            Source = card.Source;
            Height = 149;
        }

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
        public CardName CardName
        {
            get { return (CardName)GetValue(CardNameProperty); }
            set { SetValue(CardNameProperty, value); }
        }
        public CardShape CardShape
        {
            get { return (CardShape)GetValue(CardShapeProperty); }
            set { SetValue(CardShapeProperty, value); }
        }
        public int CardValue
        {
            get { return (int)GetValue(CardValueProperty); }
            set { SetValue(CardValueProperty, value); }
        }
        public DeckName CurrentDeck
        {
            get { return (DeckName)GetValue(CurrentDeckProperty); }
            set { SetValue(CurrentDeckProperty, value); }
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(Card), new PropertyMetadata(""));

        public static readonly DependencyProperty CardNameProperty =
            DependencyProperty.Register("CardName", typeof(CardName), typeof(Card), new PropertyMetadata(null));

        public static readonly DependencyProperty CardShapeProperty =
            DependencyProperty.Register("CardShape", typeof(CardShape), typeof(Card), new PropertyMetadata(null));

        public static readonly DependencyProperty CardValueProperty =
            DependencyProperty.Register("CardValue", typeof(int), typeof(Card), new PropertyMetadata(0));

        public static readonly DependencyProperty CurrentDeckProperty =
            DependencyProperty.Register("CurrentDeck", typeof(DeckName), typeof(Card), new PropertyMetadata(null));

        /*
        #region IReflect
        public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public MethodInfo[] GetMethods(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public FieldInfo GetField(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public FieldInfo[] GetFields(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public PropertyInfo[] GetProperties(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public MemberInfo[] GetMembers(BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
        {
            throw new NotImplementedException();
        }
        #endregion*/
    }
}
