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
using System.Windows.Threading;

namespace WpfGuiDispatcher
{
    public class CalculationBlocking
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
        CalculationBlocking _cb = new CalculationBlocking();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => { Application.Current.Dispatcher.InvokeAsync(() => { Increment(); }); });

            //Task.Factory.StartNew(() => { Dispatcher.InvokeAsync(Increment); });
        }

        private void Increment()
        {
           TextBox.Text = _cb.Calculate(100).Result.ToString();
        }
    }
}
