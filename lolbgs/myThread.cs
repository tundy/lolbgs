using System;
using System.Collections.Generic;
using System.Threading;

namespace lolbgs
{
    internal class MyThread
    {
        private int _count;
        private int Count { get { return ++_count; } }
        internal void ResetCounter()
        {
            _count = 0;
        }

        public delegate void JobOutput(string s, int count);
        public event JobOutput NewJobOutput;
        internal virtual void OnJobDone(string s)
        {
            var handler = NewJobOutput;
            if (handler != null)
            {
                handler(s, Count);
            }
        }
        internal virtual void OnJobDone(string s, int count)
        {
            _count = count;
            var handler = NewJobOutput;
            if (handler != null)
            {
                handler(s, count);
            }
        }

        public delegate void JobFinished();
        public event JobFinished NewJobFinished;
        internal virtual void OnJobFinished()
        {
            var handler = NewJobFinished;
            if (handler != null)
            {
                handler();
            }
        }

        internal void Copy(String destinationPath, List<String> images)
        {
            new Thread(new Copy(this, destinationPath, images).Run) { IsBackground = true }.Start();
        }

        internal void CheckDuplicates(String destinationPath, List<String> images)
        {
            new Thread(new Check(this, destinationPath, images).Run) { IsBackground = true }.Start();
        }
    }
}
