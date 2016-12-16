using System;

namespace Bcl.Interfaces
{
    public interface IWatcher : IDisposable
    {
        void Start();

        void Stop();
    }
}