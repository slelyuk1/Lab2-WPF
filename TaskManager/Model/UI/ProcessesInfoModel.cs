using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace TaskManager.Model.UI
{
    public class ProcessesInfoModel
    {
        private readonly ILogger<ProcessesInfoModel> _logger;

        public ProcessesInfoModel(ILogger<ProcessesInfoModel> logger)
        {
            _logger = logger;
            ViewedProcesses = new ObservableCollection<ReadableProcess>();
        }

        public ObservableCollection<ReadableProcess> ViewedProcesses { get; }

        public ReadableProcess? SelectedProcess { get; set; }

        public ProcessThreadCollection? SelectedThreads => SelectedProcess?.Threads;

        public ProcessModuleCollection? SelectedModules => SelectedProcess?.Modules;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateProcesses()
        {
            foreach (ReadableProcess viewedProcess in ViewedProcesses)
            {
                viewedProcess.Update();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RebuildProcesses(IEnumerable<Process> runningProcesses)
        {
            IDictionary<int, Process> idToProcess = runningProcesses.ToDictionary(process => process.Id);

            for (var i = 0; i < ViewedProcesses.Count; i++)
            {
                ReadableProcess viewedProcess = ViewedProcesses[i];
                int? viewedProcessId = viewedProcess.Id;
                if (viewedProcessId == null || !idToProcess.ContainsKey((int) viewedProcessId))
                {
                    ViewedProcesses.RemoveAt(i);
                }
                else
                {
                    idToProcess.Remove((int) viewedProcessId);
                }
            }

            foreach (Process process in idToProcess.Values)
            {
                ViewedProcesses.Add(new ReadableProcess(process, _logger));
            }

            UpdateProcesses();
        }
    }
}