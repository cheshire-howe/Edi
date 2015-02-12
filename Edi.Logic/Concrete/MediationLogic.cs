using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Edi.Core;
using Edi.Logic.Interfaces;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace Edi.Logic.Concrete
{
    public class MediationLogic : IMediationLogic
    {
        public FileInfo[] ProcessDirectory()
        {
            string sourceDirectory = Settings.ImportDirectory;

            try
            {
                //Scan the directory
                DirectoryInfo dirInfo = new DirectoryInfo(sourceDirectory);

                //check directory exist
                if (dirInfo.Exists)
                {
                    //Process Each directory
                    FileInfo[] files = dirInfo.GetFiles("*.txt"); // Getting Text files

                    return files;
                }
                Console.WriteLine("Error: Invalid Directory " + sourceDirectory);
                throw new Exception("Invalid Directory");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:The process failed: {0}", e.ToString());
                throw;
            }
        }

        public int FindService(List<Interchange> interchanges)
        {
            // Edi section ISA
            var isa = interchanges[0];
            // Edi section GS
            var gs = isa.FunctionGroups.ToList()[0];
            // Edi section ST
            var st = gs.Transactions[0];

            switch (st.GetIntElement(1))
            {
                case 810:
                    return 810;
                case 850:
                    return 850;
                case 855:
                    return 855;
                case 856:
                    return 856;
                default:
                    return 0;
            }
        }

        public List<Interchange> GetInterchanges(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var parser = new X12Parser();
            var interchanges = parser.ParseMultiple(fs);
            fs.Close();
            return interchanges;
        }

        /// <summary>
        /// Moves the current data file into another directory
        /// </summary>
        /// <param name="fInfo">The file</param>
        /// <param name="noErrors">Different things happen if there are errors or not</param>
        public void MoveFile(FileInfo fInfo, bool noErrors)
        {
            string destDirectory = Path.Combine(Settings.ImportDirectory, noErrors ? "Processed" : "ProcessedError");

            // Filename
            string sourceFile = fInfo.FullName;

            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }

            Console.WriteLine("MoveFile::{0} to Destination Directory {1}.", sourceFile, destDirectory);
            string destFile = destDirectory + @"\" + fInfo.Name + "_" + DateTime.Now.ToString("dd-MM-yyyy");
            File.Move(fInfo.FullName, destFile);
            Console.WriteLine("MoveFile::{0} was moved to {1}.", sourceFile, destDirectory);
        }
    }
}
