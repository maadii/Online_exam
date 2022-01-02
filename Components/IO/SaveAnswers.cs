using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Components.IO
{

     public class SaveAnswers:IDisposable
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
        public  void WriteAnswer(string StudentName,string StudentCode,string ExamDate,string DecAnswer,string SpeakingAdresee,string SpeakingfileName)
        {
           try
           {
                // Copy audio file answers object.
                string folderName = @"AnswerFolder";
                string filename = Path.Combine(folderName,StudentName + ".xml");
                Directory.CreateDirectory(folderName);
                string sourceFile = SpeakingAdresee;
                string destFile = Path.Combine(folderName, SpeakingfileName);
                

                File.Copy(sourceFile, destFile, true);
                /* Create an Xml answers object.*/
                XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    "; //  "\t";
            settings.OmitXmlDeclaration = false;
            settings.Encoding = Encoding.UTF8;

                using (XmlWriter writer =XmlWriter.Create(filename, settings))
            {
                writer.WriteStartDocument();
                    writer.WriteStartElement("Answers");
                    writer.WriteElementString("Name",StudentName);
                    writer.WriteElementString("national_code",StudentCode); 
                    writer.WriteElementString("Date",ExamDate);
                    writer.WriteElementString("Descriptive_answers",DecAnswer);
                   writer.WriteElementString("speaking_answers", SpeakingfileName);
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
            } // End Using writer 
           }
           catch (Exception)
          {
              throw;
          }
        }
    }
}
