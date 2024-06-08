using System.Windows;
using TodoBot.UI.ViewModels;

namespace TodoBot.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent ();
            this.DataContext = new MainViewModel ();
        }
    }
}