using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class Tunnel : IAnalyzer
    {
        public Tunnel(AnalyzerForm.ProcessResultDelegate callback)
        {
            resultDelegate = callback;
        }

        private AnalyzerForm.ProcessResultDelegate resultDelegate;

        Dictionary<string, int> total = new Dictionary<string, int>();
        Dictionary<string, int> icmp = new Dictionary<string, int>();
        bool found = false;

        public void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = "Tunnel";
            description = "";
            version = "0.2";
            noSubscription = false;
        }

        public void Initialize()
        {

        }

        private void AddOneToDict(string key, bool alsoIcmp)
        {
            if (total.ContainsKey(key))
                total[key] = total[key] + 1;
            else
                total[key] = 1;

            if (alsoIcmp)
            {
                if (icmp.ContainsKey(key))
                {
                    icmp[key]++;
                }
                else
                {
                    icmp[key] = 1;
                }
            }
        }

        public void ProcessEvent(Event evt)
        {
            if (found || evt.OpClass != OperationClass.Packet)
                return;

            if (evt.OpClass == OperationClass.Packet)
            {
                AddOneToDict(evt.ProcessName, evt.OperationPath.Contains("[icmp]"));
            }
            
            foreach (var i in total.Keys)
            {
                if (total[i] > 500 && icmp[i] > 200)
                {
                    found = true;
                    resultDelegate(new Result("Tunnel application", "A running tunnel application \"" + evt.ProcessName + "\" is found on the PC. Please disable it immediately", 1, 40, evt.id));
                }
            }            
        }

    }
}
