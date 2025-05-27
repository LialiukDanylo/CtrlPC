using System.Diagnostics;
using CtrlPC.Mobile.Commands;
using CtrlPC.Mobile.Networking;
using CtrlPC.Networking;

namespace CtrlPC.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly IBroadcastProvider _broadcastProvider;
        private readonly INetworkScanner _networkScanner;
        private readonly ICommandSender _commandSender;
        private readonly int _port = 5151;

        private List<Models.Device> _currentDevices = new();

        public MainPage()
        {
            InitializeComponent();

            _broadcastProvider = new BroadcastProvider();
            _networkScanner = new NetworkScanner();
            _commandSender = new CommandSender();

            _ = Task.Run(() => PeriodicScan());
        }

        private async Task PeriodicScan()
        {
            while (true)
            {
                await ScanAndUpdateUIAsync();
                await Task.Delay(30000);
            }
        }

        private async void OnScanButtonClicked(object sender, EventArgs e)
        {
            await ScanAndUpdateUIAsync();
        }

        private async Task ScanAndUpdateUIAsync()
        {
            string broadcastIp = _broadcastProvider.GetBroadcastAddress();
            var devices = await _networkScanner.ScanNetworkAsync(broadcastIp, _port);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                DebugLabel.Text = broadcastIp;

                var oldIps = _currentDevices.Select(d => d.IpAddress);
                var newIps = devices.Select(d => d.IpAddress);

                bool areEqual = oldIps.OrderBy(ip => ip).SequenceEqual(newIps.OrderBy(ip => ip));

                if (!areEqual)
                {
                    _currentDevices = devices;
                    IpListView.ItemsSource = devices;
                }
            });
        }

        private void OnSleepClicked(object sender, EventArgs e) => SendCommandToSelected("SLEEP");
        private void OnShutdownClicked(object sender, EventArgs e) => SendCommandToSelected("SHUTDOWN");
        private void OnRestartClicked(object sender, EventArgs e) => SendCommandToSelected("RESTART");

        private void SendCommandToSelected(string command)
        {
            if (IpListView.SelectedItem is Models.Device selectedDevice)
            {
                try
                {
                    _commandSender.SendCommand(selectedDevice.IpAddress, _port, command);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}
