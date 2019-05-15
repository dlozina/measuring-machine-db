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

namespace MeasuringMachineApp.PagesM2
{
    /// <summary>
    /// Interaction logic for ManualInput2.xaml
    /// </summary>
    public partial class ManualInput2 : Page
    {
        public ManualInput2()
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

            Thread TestConnectionM2 = new Thread(() => App.CncInterface.TestConnection(_ipAddress, _portNumber));
            TestConnectionM2.Name = "TestConnectionM2";
            TestConnectionM2.Start();
        }

        private void MtoolInfo_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _ipAddress = IpAddress.Text;

            Thread ToolInfoM2 = new Thread(() => App.CncInterface.ReadMachineToolInfo(_ipAddress, _portNumber));
            ToolInfoM2.Name = "ToolInfoM2";
            ToolInfoM2.Start();
        }

        private void MtoolOffset_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _ipAddress = IpAddress.Text;

            Thread ToolOffsetM2 = new Thread(() => App.CncInterface.ReadToolOffset(_ipAddress, _portNumber));
            ToolOffsetM2.Name = "ToolOffsetM2";
            ToolOffsetM2.Start();
        }

        private void TestOffsetX_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _value = int.Parse(ToolOffsetX.Text);
            _ipAddress = IpAddress.Text;

            Thread WriteToolOffsetXM2 = new Thread(() => App.CncInterface.WriteToolOffsetX(_ipAddress, _portNumber, _value));
            WriteToolOffsetXM2.Name = "WriteToolOffsetXM2";
            WriteToolOffsetXM2.Start();
        }

        private void TestOffsetY_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _value = int.Parse(ToolOffsetY.Text);
            _ipAddress = IpAddress.Text;

            Thread WriteToolOffsetYM2 = new Thread(() => App.CncInterface.WriteToolOffsetY(_ipAddress, _portNumber, _value));
            WriteToolOffsetYM2.Name = "WriteToolOffsetYM2";
            WriteToolOffsetYM2.Start();
        }

        private void TestOffsetR_Click(object sender, RoutedEventArgs e)
        {
            _portNumber = ushort.Parse(PortNumber.Text);
            _value = int.Parse(ToolOffsetZ.Text);
            _ipAddress = IpAddress.Text;

            Thread WriteToolOffsetRM2 = new Thread(() => App.CncInterface.WriteToolOffsetR(_ipAddress, _portNumber, _value));
            WriteToolOffsetRM2.Name = "WriteToolOffsetRM2";
            WriteToolOffsetRM2.Start();
        }
    }
}
