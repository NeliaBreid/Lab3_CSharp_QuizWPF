using QuizLab3.Model;
using QuizLab3.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizLab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var pack =  new QuestionPackViewModel(new QuestionPack("My Question Pack")); //Test för F11
            pack.TimeLimitInSeconds = 100; //test F11
            
        }
    }
}