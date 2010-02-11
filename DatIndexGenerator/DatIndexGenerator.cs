using System;
using System.Collections.Generic;
using System.Text;
using Kofax.Eclipse.Base;
using System.IO;

namespace DatIndexGenerator
{
    public class DatIndexGenerator : IDocumentIndexGenerator,
                                     IBatchIndexGenerator
    {
        private ReleaseMode _workingMode;

        //20100211 - Added variable to hold document number
        private int _documentNumner = 1;

        #region IExporter Members

        public string DefaultExtension
        {
            get { return "DAT"; }
        }

        public string Description
        {
            get { return "Dat text file"; }
        }
        
        public Guid Id
        {
            get { return new Guid("{848BEC37-6904-49b7-BE4D-A8C6BA3F128D}"); }
        }

        public bool IsCustomizable
        {
            get { return true; }
        }

        public string Name
        {
            get { return "DAT file"; }
        }

        #region Settings
        public void DeserializeSettings(System.IO.Stream input) { }

        public void SerializeSettings(System.IO.Stream output) { }

        public void Setup(IDictionary<string, string> releaseData) { } 
        #endregion

        #endregion

        #region IBatchIndexGenerator Members

        public void AppendIndex(IDocument document, string outputFileName) 
        {

            using (FileStream fs = new FileStream(outputFileName, FileMode.Append, FileAccess.Write, FileShare.None))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.Unicode))
            {
               for (int indexNumber = 0; indexNumber < document.IndexDataCount; indexNumber++)
                   writer.WriteLine("{0}:{1}", document.GetIndexDataLabel(indexNumber), document.GetIndexDataValue(indexNumber));

                writer.Flush();
                writer.Close();
            }
        }

        public void EndIndex(object handle, ReleaseResult result, string outputFileName) { }

        public object StartIndex(IBatch batch, IDictionary<string, string> releaseData, string outputFileName) 
        {
            return null;
        }
        #endregion

        #region IDocumentIndexGenerator Members

        public void CreateIndex(IDocument document, IDictionary<string, string> exportData, string outputFileName)
        {
            using (FileStream fs = new FileStream(outputFileName, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.ASCII))
            {
                if (exportData != null)
                {
                    for (int indexNumber = 0; indexNumber < document.IndexDataCount; indexNumber++)
                        writer.WriteLine("{0}:{1}", document.GetIndexDataLabel(indexNumber), document.GetIndexDataValue(indexNumber));
                }

                //20100111 - Changed the index to pass the current document number so the correct file is written out
                writer.WriteLine("{0}", exportData[string.Format("DocumentOutputFileName[{0}]", _documentNumner)]);

                _documentNumner++;

                writer.Flush();
                writer.Close();
            }
        }

        public void SerializeSample(IDictionary<string, string> exportData, System.IO.Stream output)
        {
            if (output == null)
                return;

            using (StreamWriter writer = new StreamWriter(output))
            {
                if (exportData != null)
                {
                    writer.WriteLine("Fieldname1:(value1)");
                    writer.WriteLine("Fieldname2:(value2)");
                    writer.WriteLine("Fieldname3:(value3)");
                    writer.WriteLine("{0}", Path.Combine(exportData["ReleasePath"], "image.tif"));
                }
                writer.Flush();
                writer.Close();
            }
        }

        public bool IsSupported(ReleaseMode mode)
        {
            return true;
        }
        
        public ReleaseMode WorkingMode
        {
            get
            {
                return _workingMode;
            }
            set
            {
                _workingMode = value;;
            }
        }

        #endregion
    }
}
