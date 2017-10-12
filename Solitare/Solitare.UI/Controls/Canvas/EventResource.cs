using Solitare.UI.Controls.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Solitare.UI.Controls.Canvas
{
    public class EventResource
    {
        public EventResource(EventName eventName)
        {
            switch (eventName)
            {
                case EventName.MouseLeftButtonDownEvent:
                    TargetType = typeof(Card);
                    Event = UIElement.MouseLeftButtonDownEvent;
                    break;
                default:
                    break;
            }
        }

        public Type TargetType { get; set; }

        public RoutedEvent Event { get; set; }
    }

    public enum EventName
    {
        MouseLeftButtonDownEvent,
    }
}
