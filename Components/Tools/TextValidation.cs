using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Components.Tools
{
     public class TextValidation:IDisposable
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
        private string text = "";
        private int validNumber = 0;
        public string Text { get => text; set => text = value; }
        public int ValidNumber { get => validNumber; set => validNumber = value; }
        // Check the validity unique words using thevalidNumber value
        public bool IsValid()
        {
            try
            {

           
            Dictionary<int, string> Answers = new Dictionary<int, string>();
            bool v = false;
            string[] words = Text.Split(' ');
              for (int i= 0;i <words.Length;i++)
                {
                  if(words[i].ToCharArray().Length>3)
                {
                    if(!Answers.ContainsValue(words[i]))
                    {
                        Answers.Add(i, words[i]);
                    }  
                }
               }
              if(Answers.Count>= ValidNumber)
            {
                v = true;
            }
              else
            {
                v = false;
            }
            return v;
            }
            catch (Exception)
            {

                throw e;
            }
        }
    }
}
