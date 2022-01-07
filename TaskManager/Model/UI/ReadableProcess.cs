using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Logging;
using Shared.Tool.ViewModel;

namespace TaskManager.Model.UI
{
    public class ReadableProcess : ObservableItem
    {
        private readonly ILogger? _logger;

        public ReadableProcess(Process observedProcess, ILogger? logger = null)
        {
            _logger = logger;
            ObservedProcess = observedProcess;
            UpdateValues();
        }

        public Process ObservedProcess { get; }

        public int? Id => GetValueSafely<int?>(() => ObservedProcess.Id);

        public string? Name => GetValueSafely(() => ObservedProcess.ProcessName);

        public bool? IsActive => GetValueSafely<bool?>(() => !ObservedProcess.HasExited);

        public DateTime? StartTime => GetValueSafely(() => ObservedProcess.StartTime);

        public double? RamUsage => GetValueSafely<double?>(() => (double) ObservedProcess.WorkingSet64 / 1024 / 1024);

        public string? UserName => GetValueSafely(() => ObservedProcess.StartInfo.UserName);

        public string? FileLocation => GetValueSafely(() => Path.GetDirectoryName(ObservedProcess.MainModule?.FileName));

        public ProcessModuleCollection? Modules => GetValueSafely(() => ObservedProcess.Modules);

        public ProcessThreadCollection? Threads => GetValueSafely(() => ObservedProcess.Threads);

        public int? ThreadsCount => Threads?.Count;

        public void Update()
        {
            ObservedProcess.Refresh();
            UpdateValues();
        }

        private void UpdateValues()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(IsActive));
            OnPropertyChanged(nameof(StartTime));
            OnPropertyChanged(nameof(RamUsage));
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(FileLocation));
            OnPropertyChanged(nameof(Modules));
            OnPropertyChanged(nameof(Threads));
        }

        private T? GetValueSafely<T>(Func<T> valueSupplier)
        {
            try
            {
                return valueSupplier.Invoke();
            }
            catch (Exception e)
            {
                _logger?.LogDebug(e, "An expected error occurred during process information retrieval");
                return default;
            }
        }
    }
}