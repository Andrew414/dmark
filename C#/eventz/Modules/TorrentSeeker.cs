using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class TorrentSeeker: IAnalyzer
    {
        public TorrentSeeker(AnalyzerForm.ProcessResultDelegate callback)
        {
            resultDelegate = callback;
        }

        private AnalyzerForm.ProcessResultDelegate resultDelegate;
        private bool TorrentRunningReported = false;
        private bool TorrentTransferReported = false;

        public void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = "Torrent seeker";
            description = "";
            version = "0.1";
            noSubscription = false;
        }
        public void ProcessEvent(Event evt)
        {
            if (TorrentRunningReported && TorrentTransferReported)
            {
                return;
            }

            if (evt.ProcessName.ToLower().Contains("torrent") && evt.OpClass == OperationClass.Packet)
            {
                resultDelegate(new Result("Torrent application", "A running torrent application \"" + evt.ProcessName + "\", transmitting some data, is found on the PC. Please disable it immediately", 0, 100, evt.id));
                TorrentRunningReported = TorrentTransferReported = true;
            }

            if (TorrentRunningReported)
            {
                return;
            }

            if (evt.ProcessName.ToLower().Contains("torrent") && evt.OpClass != OperationClass.Packet)
            {
                resultDelegate(new Result("Torrent application", "A running torrent application \"" + evt.ProcessName + "\" is found on the PC. Please disable it immediately", 1, 80, evt.id));
                TorrentRunningReported = true;
            }
        }

        public void Initialize()
        {

        }
    }
}
