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

        public delegate void CopyOutput(string s, int count);
        public event CopyOutput NewCopyOutput;
        internal virtual void OnCopyDone(string s)
        {
            var handler = NewCopyOutput;
            if (handler != null)
            {
                handler(s, Count);
            }
        }
        public delegate void CopyFinished();
        public event CopyFinished NewCopyFinished;
        internal virtual void OnCopyFinished()
        {
            var handler = NewCopyFinished;
            if (handler != null)
            {
                handler();
            }
        }

        internal void Copy(String destinationPath, List<String> images)
        {
            var t = new Thread(new Copy(this, destinationPath, images).Run)
            {
                IsBackground = true
            };
            t.Start();
        }
    }
}
