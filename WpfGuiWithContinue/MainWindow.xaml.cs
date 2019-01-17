using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGuiWithContinue
{
    public class CalculationContinue
    {
        public Task<int> Calculate(int value)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000);
                return ++value;
            });
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CalculationContinue _cc = new CalculationContinue();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            var calculate = _cc.Calculate(100);

            //TextBox.Text = calculate.Result.ToString(); <== Calling the Result property is also blocking!

            // This would throw an exception (changing a GUI element from inside another thread!)
            // calculate.ContinueWith(task => { TextBox.Text = task.Result.ToString(); });

            calculate.ContinueWith(task => { TextBox.Text = task.Result.ToString(); },
                TaskScheduler.FromCurrentSynchronizationContext()); // This additional parameter ensures that the assignement is performed in the GUI thread
        }
    }
}
