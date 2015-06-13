using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class Eicar : IAnalyzer
    {
        public Eicar(AnalyzerForm.ProcessResultDelegate callback)
        {
            resultDelegate = callback;
        }

        private AnalyzerForm.ProcessResultDelegate resultDelegate;
        private bool found;

        public void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = "Eicar test file finder";
            description = "";
            version = "0.1";
            noSubscription = false;
        }

        public void Initialize()
        {

        }

        public void ProcessEvent(Event evt)
        {
            if (evt.OpClass != OperationClass.File || found)
            {
                return;
            }

            if (evt.OperationPath.ToUpper().Contains("EICAR"))
            {
                found = true;
                resultDelegate(new Result("Virus found!", "A test virus file (eicar) \"" + evt.ProcessName + "\" is found on the PC. Please check your AV software", 1, 80, evt.id));
            }
        }
    }
}
