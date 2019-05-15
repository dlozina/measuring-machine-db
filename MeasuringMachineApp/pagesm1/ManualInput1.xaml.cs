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

namespace MeasuringMachineApp.PagesM1
{
    /// <summary>
    /// Interaction logic for ManualInput1.xaml
    /// </summary>
    public partial class ManualInput1 : Page
    {
        public ManualInput1()
        {
            InitializeComponent();
            DataContext = App.CncInterface;
        }

        private string _ipAddress;
        private ushort _portNumber;
        private int _value;

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _ipAddress = IpAddress.Text;

            Thread TestConnectionM1 = new Thread(() => App.CncInterface.TestConnection(_ipAddress, _portNumber));
            TestConnectionM1.Name = "TestConnectionM1";
            TestConnectionM1.Start();
        }

        private void MtoolInfo_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _ipAddress = IpAddress.Text;

            Thread ToolInfoM1 = new Thread(() => App.CncInterface.ReadMachineToolInfo(_ipAddress, _portNumber));
            ToolInfoM1.Name = "ToolInfoM1";
            ToolInfoM1.Start();
        }

        private void MtoolOffset_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _ipAddress = IpAddress.Text;

            Thread ToolOffsetM1 = new Thread(() => App.CncInterface.ReadToolOffset(_ipAddress, _portNumber));
            ToolOffsetM1.Name = "ToolOffsetM1";
            ToolOffsetM1.Start();
        }

        private void TestOffsetX_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _value = int.Parse(ToolOffsetX.Text);
            _ipAddress = IpAddress.Text;

            Thread WriteToolOffsetXM1 = new Thread(() => App.CncInterface.WriteToolOffsetX(_ipAddress, _portNumber, _value));
            WriteToolOffsetXM1.Name = "WriteToolOffsetXM1";
            WriteToolOffsetXM1.Start();   
        }

        private void TestOffsetY_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _value = int.Parse(ToolOffsetY.Text);
            _ipAddress = IpAddress.Text;

            Thread WriteToolOffsetYM1 = new Thread(() => App.CncInterface.WriteToolOffsetY(_ipAddress, _portNumber, _value));
            WriteToolOffsetYM1.Name = "WriteToolOffsetYM1";
            WriteToolOffsetYM1.Start(); 
        }

        private void TestOffsetR_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _value = int.Parse(ToolOffsetZ.Text);
            _ipAddress = IpAddress.Text;

            Thread WriteToolOffsetRM1 = new Thread(() => App.CncInterface.WriteToolOffsetR(_ipAddress, _portNumber, _value));
            WriteToolOffsetRM1.Name = "WriteToolOffsetRM1";
            WriteToolOffsetRM1.Start();
        }
    }
}
