using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Main.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand<string> OpenTab { get; set; }

        Menu _customMenu;
        public Menu CustomMenu
        {
            get
            {
                if (_customMenu == null)
                    _customMenu = new Menu();
                return _customMenu;
            }
            set
            {
                _customMenu = value;
                RaisePropertyChanged();
            }
        }

        TabControl _customTabControl;
        public TabControl CustomTabControl
        {
            get
            {
                if (_customTabControl == null)
                    _customTabControl = new TabControl();
                return _customTabControl;
            }
            set
            {
                _customTabControl = value;
                RaisePropertyChanged();
            }
        }

        ObservableCollection<TabItem> _tabs;
        public ObservableCollection<TabItem> Tabs
        {
            get
            {
                if (_tabs == null)
                    _tabs = new ObservableCollection<TabItem>();
                return _tabs;
            }
            set
            {
                _tabs = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            OpenTab = new RelayCommand<string>(OpenNewTab);

            MenuItem item1 = new MenuItem()
            {
                Header = "Item 1",
                Command = OpenTab,
                CommandParameter = "1"
            };
            MenuItem item2 = new MenuItem()
            {
                Header = "Item 2",
                Command = OpenTab,
                CommandParameter = "2"
            };
            MenuItem item3 = new MenuItem()
            {
                Header = "Item 3",
                Command = OpenTab,
                CommandParameter = "3"
            };
            MenuItem item2A = new MenuItem()
            {
                Header = "Item 2A",
                Command = OpenTab,
                CommandParameter = "2A"
            };
            MenuItem item2B = new MenuItem()
            {
                Header = "Item 2B",
                Command = OpenTab,
                CommandParameter = "2B"
            };
            item2.Items.Add(item2A);
            item2.Items.Add(item2B);

            MenuItem main = new MenuItem() { Header = "Menu" };
            main.Items.Add(item1);
            main.Items.Add(item2);
            main.Items.Add(item3);

            CustomMenu.Items.Add(main);

            CustomTabControl.ItemsSource = Tabs;
        }

        void OpenNewTab(string item)
        {
            Tabs.Add(new TabItem()
            {
                Header = "Item " + item,
                Content = "Item " + item
            });
        }
    }
}