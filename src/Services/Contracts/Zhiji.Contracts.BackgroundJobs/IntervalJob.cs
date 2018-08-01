using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Zhiji.Contracts.BackgroundJobs
{
    public abstract class IntervalJob : BackgroundService
    {
        public TimeSpan Interval { get; }
        private Task _task;
        private CancellationTokenSource _cts;

        protected IntervalJob(TimeSpan interval)
        {
            this.Interval = interval;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            if (_task == null)
            {
                _cts = new CancellationTokenSource();
                _task = ExecuteAsync(_cts.Token);
            }

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_task == null) return;

            try
            {
                _task = null;
                _cts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_task, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await this.ExecutePeriodicallyAsync(stoppingToken);
                await Task.Delay(this.Interval, stoppingToken);
            }
        }

        protected abstract Task ExecutePeriodicallyAsync(CancellationToken stoppingToken);
    }
}
