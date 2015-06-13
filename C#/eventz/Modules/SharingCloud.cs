using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class SharingCloud : IAnalyzer
    {
        public SharingCloud(AnalyzerForm.ProcessResultDelegate callback)
        {
            resultDelegate = callback;
        }

        private AnalyzerForm.ProcessResultDelegate resultDelegate;
        public void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = "Sharing and cloud applications";
            description = "";
            version = "0.1";
            noSubscription = false;
        }

        private bool RunningReported = false;
        private bool TransferReported = false;

        public void Initialize()
        {

        }

        public void ProcessEvent(Event evt)
        {
            if (RunningReported && TransferReported)
            {
                return;
            }

            if ((evt.ProcessName.ToLower().Contains("dropbox.exe")) || (evt.ProcessName.ToLower().Contains("drive.exe")) && evt.OpClass == OperationClass.Packet)
            {
                resultDelegate(new Result("Sharing application", "A running sharing application \"" + evt.ProcessName + "\", transmitting some data, is found on the PC. Please disable it immediately", 0, 100, evt.id));
                RunningReported = TransferReported = true;
            }

            if (RunningReported)
            {
                return;
            }

            if (evt.ProcessName.ToLower().Contains("torrent") && evt.OpClass != OperationClass.Packet)
            {
                resultDelegate(new Result("Torrent application", "A running sharing application \"" + evt.ProcessName + "\" is found on the PC. Please disable it immediately", 1, 80, evt.id));
                RunningReported = true;
            }
        }
    }
}
