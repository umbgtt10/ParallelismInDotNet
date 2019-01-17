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

namespace WpfGuiWithAsync
{
    public class CalculationAsync
    {
        public async Task<int> CalculateAsync(int value)
        {
            await Task.Delay(5000); // ==> For await
            // Thread.Sleep(5000); // ==> For Result
            return ++value;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CalculationAsync _ca = new CalculationAsync();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_OnClick(object sender, RoutedEventArgs e)
        {
            //var calculate = _ca.CalculateAsync(100).Result;
            var calculate = await _ca.CalculateAsync(100);

            TextBox.Text = calculate.ToString();

            var t = Task.Factory.StartNew(() => { });

            var t2 = await Task.WhenAny(t);
        }
    }
}
