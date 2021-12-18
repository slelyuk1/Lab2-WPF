using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using TaskManager.Model.Data;

namespace TaskManager.Model.UI
{
    internal class ProcessesInfoModel
    {
        public void UpdateProcesses(ObservableCollection<ReadableProcess> processes)
        {
            foreach (ReadableProcess t in processes)
            {
                if (t.MainProcess != null)
                {
                    t.Update();
                }
            }
        }

        public void RebuildProcesses(ObservableCollection<ReadableProcess> processes)
        {
            var running = new Dictionary<int, Process>();
            foreach (Process process in Process.GetProcesses())
            {
                running.Add(process.Id, process);
            }

            for (var i = 0; i < processes.Count; i++)
            {
                ReadableProcess cur = processes[i];
                if (!running.ContainsKey(cur.Id))
                {
                    processes.RemoveAt(i);
                }
                else
                {
                    running.Remove(cur.Id);
                    cur.Update();
                }
            }

            foreach (Process process in running.Values)
            {
                try
                {
                    processes.Add(new ReadableProcess(process));
                }
                catch (Win32Exception)
                {
                    // todo make normal exception handling
                }
            }
        }
    }
}