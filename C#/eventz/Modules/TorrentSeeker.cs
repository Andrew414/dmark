using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class TorrentSeeker: IAnalyzer
    {
        void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = description = version = "";
            noSubscription = false;
        }
        void ProcessEvent(Event evt)
        {
            
        }
    }
}
