using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;

namespace eventz
{
    public partial class AnalyzerForm : Form
    {
        private bool UseDCommConnection()
        {
            int selectedIndex =
                PSTaskDialog.cTaskDialog.ShowCommandBox(
                    "Source for analyzing",
                    "Please select a source for analyzing", 
                    null,
                    @"You have two options.
The first one is to use the existring log, collected with Process Monitor or Wireshark.
The second option is to start the live capture of the events right now (admin rights are needed)",
                    //"Any expanded content text for the commandbox is shown here and the text will automatically wrap as needed.",
                    null,
                    null,
                    "Existing log|Live capture",
                    true,
                    PSTaskDialog.eSysIcons.Information,
                    PSTaskDialog.eSysIcons.Warning
                );

            return selectedIndex == 1;
        }

        private bool IsRunningAsAdmin()
        {
            bool isElevated;
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);

            return isElevated;
        }

        private bool DriverIsRunning() { return true; }
        private bool DriverIsInstalled() { return true; }
        private void InstallDriver() { }
        private void RunDriver() { }
        private void RunDcomm() 
        {
            System.Diagnostics.Process.Start(@"C:\Users\andre_000\OneDrive\Documents\Visual Studio 2013\Projects\dmark\C++\Debug\dcomm.exe");
        }

        private Event EventFromProcMonString(string line)
        {
            string[] str = line.Split(new char[] { ',' });

            string time = str[0].Replace("\"", "");
            string proc = str[1].Replace("\"", "");
            string pid = str[2].Replace("\"", "");
            string oper = str[3].Replace("\"", "");
            string path = str[4].Replace("\"", "");
            string result = str[5].Replace("\"", "");
            string details = str[6].Replace("\"", "");

            if (result != "SUCCESS")
            {
                return null;
            }

            if (oper != "WriteFile" && oper != "RegSetInfoKey" && oper != "RegSetValue" && oper != "RegCreateKey"
                && oper != "RegDeleteValue" && oper != "Process Create" && oper != "Process Exit" && oper != "UDP Send" && oper != "UDP Receive"
                && oper != "TCP Send" && oper != "TCP Receive" && oper != "TCP Connect" && oper != "CreateFile")
            {
                return null;
            }

            Event evt;
            string[] strTime = time.Split(new char[] {'.'})[0].Split(new char[] {':'});
            int timeHour = int.Parse(strTime[0]);
            int timeMin = int.Parse(strTime[1]);
            int timeSec = int.Parse(strTime[2]);

            switch(oper)
            {
                case "RegSetInfoKey":
                case "RegSetValue":
                case "RegCreateKey":
                case "RegDeleteValue":
                evt = new Event(proc, "unknown", proc, path, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timeHour, timeMin, timeSec), 0, int.Parse(pid), -1, 0, OperationClass.Registry, 
                    (oper == "RegSetInfoKey" || oper == "RegSetInfoKey") ? OperationType.Write : (oper == "RegDeleteValue") ? OperationType.Destroy : OperationType.Create);
                break;
                case "WriteFile":
                evt = new Event(proc, "unknown", proc, path, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timeHour, timeMin, timeSec), 0, int.Parse(pid), -1, 0, OperationClass.File, 
                   OperationType.Write);
                break;
                case "CreateFile":
                evt = details.Contains("Delete On Close") ? new Event(proc, "unknown", proc, path, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timeHour, timeMin, timeSec), 0, int.Parse(pid), -1, 0, OperationClass.File,
                    OperationType.Destroy) : null;
                break;
                case "UDP Send":
                case "UDP Receive":
                case "TCP Send":
                case "TCP Receive":
                evt = ((path.Count(c => c == ':')) == 2) ? new Event(proc, "unknown", proc, path, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timeHour, timeMin, timeSec), 0, int.Parse(pid), -1, 0, OperationClass.Packet,
                    OperationType.Write) : null;
                break;
                case "TCP Connect":
                evt = ((path.Count(c => c == ':')) == 2) ? new Event(proc, "unknown", proc, path, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timeHour, timeMin, timeSec), 0, int.Parse(pid), -1, 0, OperationClass.Packet,
                    OperationType.Create) : null;
                break;
                case "Process Create":
                evt = new Event(path, "unknown", path, path, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timeHour, timeMin, timeSec), 0, int.Parse(details.Split(new char[] {','})[0].Substring("PID: ".Length)), int.Parse(pid), 0, OperationClass.Process,
                    OperationType.Create);
                break;
                case "Process Exit":
                evt = new Event(proc, "unknown", proc, path, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, timeHour, timeMin, timeSec), 0, int.Parse(pid), -1, 0, OperationClass.Process,
                    OperationType.Destroy);
                break;
                default:
                evt = null;
                break;
            }

            return evt;
            
        }

        private string OpClassToString(OperationClass opclass)
        {
            switch (opclass)
            {
                case OperationClass.File:
                    return "File";
                case OperationClass.Packet:
                    return "Network";
                case OperationClass.Process:
                    return "Process";
                case OperationClass.Registry:
                    return "Registry";
                default:
                    return "Unknown";
            }
        }

        private string OpTypeToString(OperationClass opclass, OperationType optype)
        {
            switch (opclass)
            {
                case OperationClass.File:
                case OperationClass.Process:
                case OperationClass.Registry:
                    switch(optype)
                    {
                        case OperationType.Create:
                            return "Create";
                        case OperationType.Destroy:
                            return (opclass == OperationClass.Process) ? "Exit" : "Delete";
                        case OperationType.Rename:
                            return "Rename";
                        case OperationType.Write:
                            return "Modify";
                        default:
                            return "Unknown";
                    }
                case OperationClass.Packet:
                    switch(optype)
                    {
                        case OperationType.Create:
                            return "Connect";
                        case OperationType.Destroy:
                            return "Disconnect";
                        case OperationType.Write:
                            return "Transmit";
                        default:
                            return "Unknown";
                    }
                default:
                    return "Unknown";
            }
        }

        private int count = 0;

        private void LoadData()
        {
            if (UseDCommConnection())
            {
                if (!IsRunningAsAdmin())
                {
                    MessageBox.Show("You are not running as admin!\nPlease restart the program with admin rights.\nThe program will be closed now.",
                        "Admin rights needed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else
                {
                    if (!DriverIsRunning())
                    {
                        if (!DriverIsInstalled())
                        {
                            InstallDriver();
                        }
                        RunDriver();
                    }
                    RunDcomm();

                    NamedPipe pipe = new NamedPipe(@"\\.\pipe\dcommconnection", 0, this);
                }
            }
            else
            {
                if (openLog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //try
                    //{
                    var reader = new System.IO.StreamReader(openLog.FileName);
                    string line;
                    while (!String.IsNullOrWhiteSpace(line = reader.ReadLine()))
                    {
                        Application.DoEvents();
                        try
                        {
                            totalCount++;
                            if (totalCount % 100000 == 0)
                            {
                                break;
                            }
                            Event evt = EventFromProcMonString(line);
                            if (evt != null)
                            {
                                ProcessEvent(evt);
                            }
                        }
                        catch (Exception) { }
                    }
                    /*}
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }*/
                }
            }
        }

        private void AddEventToEventList(Event evt)
        {
            if ((count % 10000 )== 0)
            {
                //MessageBox.Show("Jey!");
                eventView.Items.Clear();
            }

            if (!evt.ProcessName.ToUpper().Contains((filterBox.Text == "filter...") ? "" : filterBox.Text.ToUpper()))
                return;

            string[] subitems = new string[10];
            subitems[0] = (evt.Time.Hour < 10 ? "0" : "") + evt.Time.Hour.ToString() + ":" + 
                (evt.Time.Minute < 10 ? "0" : "") + evt.Time.Minute.ToString() + ":" +
                (evt.Time.Second < 10 ? "0" : "") + evt.Time.Second.ToString();
            subitems[1] = evt.ProcessName;
            subitems[2] = evt.Pid.ToString();
            subitems[3] = evt.Ppid.ToString();
            subitems[4] = OpClassToString(evt.OpClass) + " (" + OpTypeToString(evt.OpClass, evt.OpType) + ")";
            subitems[5] = evt.OperationPath;
            subitems[6] = evt.Tid.ToString();
            subitems[7] = evt.ImagePath;
            subitems[8] = evt.Flags.ToString();
            subitems[9] = evt.UserName;

            eventView.Items.Add(subitems);
            if (eventView.Items.Count > 0)
            {
                eventView.SelectedValue = eventView.Items.Last();
            }
            count++;

            eventMore.Items.Clear();
            eventMore.Items.Add("Processed: " + eventView.Items.Count.ToString() + " events");
            eventMore.Items.Add("Found: " + accidentsView.Items.Count.ToString() + " accidents");
        }

        private int totalCount = 0;

        public AnalyzerForm()
        {
            
            InitializeComponent();
            InitializeAllModules();

            //System.Threading.Thread dataThread = new System.Threading.Thread(LoadData);
            //dataThread.Start();

            AnalyzerForm_Resize(this, null);
        }

        private List<IAnalyzer> analyzis = new List<IAnalyzer>();

        public delegate void ProcessResultDelegate(Result res);

        public void ProcessResult(Result res) 
        {
            var item = new ComponentOwl.BetterListView.BetterListViewItem(new string[] {res.Header, res.Description} );
            item.Group = accidentsView.Groups[res.Level];
            item.BackColor = res.Level == 0 ? System.Drawing.Color.Orange : res.Level == 1 ? System.Drawing.Color.Yellow :
                res.Level == 2 ? System.Drawing.Color.YellowGreen : System.Drawing.Color.LightCyan;
            accidentsView.Items.Add(item);
        }

        private void InitializeAllModules()
        {
            analyzis.Add(new eventz.Modules.AutoRunChecker(ProcessResult));
            analyzis.Add(new eventz.Modules.BotTrojan(ProcessResult));
            analyzis.Add(new eventz.Modules.Eicar(ProcessResult));
            analyzis.Add(new eventz.Modules.HiddenNTFS(ProcessResult));

            analyzis.Add(new eventz.Modules.SharingCloud(ProcessResult));
            analyzis.Add(new eventz.Modules.StaticCheck(ProcessResult));
            analyzis.Add(new eventz.Modules.TorrentSeeker(ProcessResult));
            analyzis.Add(new eventz.Modules.Tunnel(ProcessResult));


            foreach (var i in analyzis)
            {
                i.Initialize();
                string useless = null;
                bool delete = false;
                i.GetInformation(out useless, out useless, out useless, out delete);
                if (delete) ;
                    //analyzis[analyzis.IndexOf(i)] = null;
            }
        }

        public void ProcessEvent(Event evt)
        {
            foreach(var i in analyzis)
            {
                if (i != null)
                {
                    i.ProcessEvent(evt);
                }
            }

            AddEventToEventList(evt);
        }

        private double eventRatio = 0.80;

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //MessageBox.Show((this.Width * eventRatio).ToString());
        }

        private void AnalyzerForm_Resize(object sender, EventArgs e)
        {
            leftPanel.Width = (int)(this.Width * eventRatio);
            eventPanel.Height = this.Height - 180;
        }
        
        private void verticalSplitter_SplitterMoved(object sender, SplitterEventArgs e)
        {
            eventRatio = (double)(leftPanel.Width) / (double)(this.Width);
        }

        private void eventView_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventMore.Items.Clear();

            if (eventView.SelectedItems.Count == 0)
                return;

            eventMore.Items.Add(eventView.SelectedItems.First().SubItems[0].ToString());
        }

        private void AnalyzerForm_Load(object sender, EventArgs e)
        {
            //LoadData();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void filterBox_TextChanged(object sender, EventArgs e)
        {
            var left = eventView.Items.Where(x => x.SubItems[1].Text.ToUpper().Contains(filterBox.Text.ToUpper())).ToList();
            eventView.Items.Clear();
            eventView.Items.AddRange(left);
        }
    }
}
