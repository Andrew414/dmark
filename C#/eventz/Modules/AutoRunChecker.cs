using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class AutoRunChecker : IAnalyzer
    {
        public AutoRunChecker(AnalyzerForm.ProcessResultDelegate callback)
        {
            resultDelegate = callback;
        }

        private AnalyzerForm.ProcessResultDelegate resultDelegate;

        public void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = "Autorun checker";
            description = "";
            version = "0.1";
            noSubscription = true;
        }

        public void Initialize()
        {

        }

        public void ProcessEvent(Event evt)
        {

        }
    }
}
