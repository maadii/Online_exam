using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Components.Data
{
    public class QuickAssessment : IDisposable

    {
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        Dictionary<int, string> Answers = new Dictionary<int, string>();

        public QuickAssessment(Dictionary<int, string> answers)
        {
            Answers = answers;
        }

        public int Getresults()
        {
            int r = 0;
            if (Answers != null)
            {
                foreach (var item in Answers)
                {
                    int tr = DBInstanc.Instance.GetMultipleChoiceby(item.Key, item.Value);
                    r += tr;
                }
                return r;
            }
            return 0;
        }

    }
}
