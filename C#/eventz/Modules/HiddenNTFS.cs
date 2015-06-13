using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz.Modules
{
    class HiddenNTFS : IAnalyzer
    {
        public HiddenNTFS(AnalyzerForm.ProcessResultDelegate callback)
        {
            resultDelegate = callback;
        }

        private AnalyzerForm.ProcessResultDelegate resultDelegate;
        bool found;

        public void GetInformation(out string name, out string description, out string version, out bool noSubscription)
        {
            name = "Hidden NTFS files";
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

            if (evt.OperationPath.IndexOf("\\") < evt.OperationPath.IndexOf(":"))
            {
                found = true;
                resultDelegate(new Result("Virus found!", "Program \"" + evt.ProcessName + "\" is accessing the hidden NTFS file \"" + evt.OperationPath + "\". Please check the program", 1, 80, evt.id));
            }
        }
    }
}
