using GalaSoft.MvvmLight;
using System.Collections;
using System.Linq;

namespace Editor.Controls.DataGrid
{
    public class CellDefinition : ViewModelBase
    {
        public virtual object Value { get; set; }

        public CellDefinition(object value)
        {
            Value = value;
        }
    }

    public class EditingCellDefinition : CellDefinition
    {
        private string _columnName { get; set; }

        public EditingCellDefinition(object value, string columnName) : base(value)
        {
            _columnName = columnName;
        }

        public override object Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                RaisePropertyChanged();
            }
        }
    }

    public class ComboBoxCellDefinition : EditingCellDefinition
    {
        public IEnumerable Items { get; set; }

        public ComboBoxCellDefinition(object value, string columnName, IEnumerable items) : base(value, columnName)
        {
            Items = items;
        }
    }
}
