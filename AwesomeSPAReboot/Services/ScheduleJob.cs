using System;
using System.Timers;

namespace AwesomeSPAReboot.Services
{
    public class ScheduleJob
    {
        private readonly Timer _timer;
        private Action _job;

        public ScheduleJob(Action task)
        {
            _timer = new Timer();
            _job = task;
        }

        public void Start(double totalMilliseconds)
        {
            try
            {
                _timer.Interval = totalMilliseconds;

                _timer.Elapsed += _timer_Elapsed;
                _timer.Start();
                IsRunning = true;
            }
            catch (Exception e)
            {
                IsRunning = false;
                throw e;
            }
        }
        public void Stop()
        {
            if (_timer != null)
                _timer.Stop();

            IsRunning = false;
        }
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;

            _job(); //perform the job

            _timer.Enabled = true;
        }
        public bool IsRunning { get; set; }
    }
}