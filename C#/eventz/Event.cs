using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventz
{
    public enum OperationClass { Process, File, Registry, Packet };
    public enum OperationType  { Create, Destroy, Write, Rename };

    public class Event
    {
        public Event(string procName, string userName, string imgPath, string opPath, DateTime time, long flags, long pid, long ppid, long tid, OperationClass opClass, OperationType opType)
        {
            ProcessName = procName;
            UserName = userName;
            ImagePath = imgPath;

            OperationPath = opPath;

            Time = time;
            Flags = flags;

            Pid = pid;
            Ppid = ppid;
            Tid = tid;

            OpClass = opClass;
            OpType = opType;

            id = ++commonId;
        }

        public string ProcessName;
        public string UserName;
        public string ImagePath;

        public string OperationPath;

        public DateTime Time;
        public long Flags;

        public long Pid;
        public long Ppid;
        public long Tid;

        public OperationClass OpClass;
        public OperationType OpType;

        public int id;

        private static int commonId = 0;
    } 
}
