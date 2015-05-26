using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz
{
    interface IAnalyzer
    {
        void GetInformation(out string name, out string description, out string version, out bool noSubscription);
        void ProcessEvent(Event evt);
        void Initialize();
    }
}
