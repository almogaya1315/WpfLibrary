using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Solitare.UI.Game.ViewModels
{
    public class ResourceDefinition
    {
        public Type TargetType { get; set; }

        public RoutedEvent Event { get; set; }
    }
}
