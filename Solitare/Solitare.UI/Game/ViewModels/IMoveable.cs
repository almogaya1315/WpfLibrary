using Solitare.UI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitare.UI.Game.ViewModels
{
    public interface IMoveable
    {
        string CardPath { get; set; }

        string FrontCardPath { get; set; }

        CardName? CardName { get; set; }

        CardShape? CardShape { get; set; }

        int CardValue { get; set; }
    }
}
