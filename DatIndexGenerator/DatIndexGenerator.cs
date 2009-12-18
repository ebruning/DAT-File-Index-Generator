using System;
using System.Collections.Generic;
using System.Text;
using Kofax.Eclipse.Base;

namespace DatIndexGenerator
{
    public class DatIndexGenerator : IDocumentIndexGenerator,
                                     IBatchIndexGenerator
    {
        private ReleaseMode _workingMode;

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
            get { return false; }
        }

        public string Name
        {
            get { return "DAT file"; }
        }

        public void DeserializeSettings(System.IO.Stream input) { }

        public void SerializeSettings(System.IO.Stream output) { }

        public void Setup(IDictionary<string, string> releaseData) { }

        #endregion

        #region IBatchIndexGenerator Members

        public void AppendIndex(IDocument document, string outputFileName) { }

        public void EndIndex(object handle, ReleaseResult result, string outputFileName) { }

        public object StartIndex(IBatch batch, IDictionary<string, string> releaseData, string outputFileName) 
        {
            return null;
        }
        #endregion

        #region IDocumentIndexGenerator Members

        public void CreateIndex(IDocument document, IDictionary<string, string> releaseData, string outputFileName)
        {
           
        }

        public bool IsSupported(ReleaseMode mode)
        {
            return true;
        }

        public void SerializeSample(IDictionary<string, string> releaseData, System.IO.Stream output)
        {
            
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
