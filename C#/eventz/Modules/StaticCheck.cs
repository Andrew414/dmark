using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class StaticCheck : IAnalyzer
    {
        private bool CheckAppInit()
        {
            return false;
        }

        private bool CheckHosts()
        {
            return false;
        }

        public StaticCheck(AnalyzerForm.ProcessResultDelegate callback)
        {
            resultDelegate = callback;
        }

        private AnalyzerForm.ProcessResultDelegate resultDelegate;
        public void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = "Statick check";
            description = "runs once";
            version = "0.1";
            noSubscription = true;
        }

        public void Initialize()
        {
            if (CheckAppInit())
            {
                resultDelegate(new Result("Appinit changed", "Appinit_dlls key contains unusual entries. Please check it", 2, 80, -1));
            }

            if (CheckHosts())
            {
                resultDelegate(new Result("Hosts file changed", "\"C:\\Windows\\System32\\Drivers\\etc\\hosts\" contains unusual entries. Please check it", 2, 80, -1));
            }
        }

        public void ProcessEvent(Event evt)
        {

        }
    }
}
