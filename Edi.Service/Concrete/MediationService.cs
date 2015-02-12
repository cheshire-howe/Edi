using Edi.Service.Interfaces;
using OopFactory.X12.Parsing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Service.Concrete
{
    public class MediationService : IMediationService
    {
        private readonly IAcknowledgmentService _acknowledgmentService;
        private readonly IAsnService _asnService;
        private readonly IInvoiceService _invoiceService;
        private readonly IPurchaseOrderService _purchaseOrderService;

        /// <summary>
        /// The directory that the data files get dumped into
        /// </summary>
        public static string IMPORT_DIRECTORY
        {
            get
            {
                return ConfigurationManager.AppSettings["ImportDirectory"];
            }
        }

        public MediationService(IAcknowledgmentService acknowledgmentService,
                                IAsnService asnService, IInvoiceService invoiceService,
                                IPurchaseOrderService purchaseOrderService)
        {
            _acknowledgmentService = acknowledgmentService;
            _asnService = asnService;
            _invoiceService = invoiceService;
            _purchaseOrderService = purchaseOrderService;
        }

        public void ProcessDirectory()
        {
            string sourceDirectory = IMPORT_DIRECTORY;

            try
            {
                //Scan the directory
                DirectoryInfo dirInfo = new DirectoryInfo(sourceDirectory);

                //check directory exist
                if (dirInfo.Exists)
                {
                    //Process Each directory
                    ProcessEachFile(dirInfo);
                }
                else
                {
                    Console.WriteLine("Error: Invalid Directory " + sourceDirectory);
                    throw new Exception("Invalid Directory");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:The process failed: {0}", e.ToString());
                throw;
            }
        }

        /// <summary>
        /// ProcessEachFile
        /// </summary>
        /// <param name="subDirInfo"></param>
        private void ProcessEachFile(DirectoryInfo subDirInfo)
        {
            string processedDirectory = Path.Combine(subDirInfo.FullName, "Processed"); //folder for processed files
            string errorDirectory = Path.Combine(subDirInfo.FullName, "ProcessedError"); //folder for error files                                    

            FileInfo[] Files = subDirInfo.GetFiles("*.txt"); // Getting Text files

            // Scan each file in the directory
            foreach (FileInfo file in Files)
            {
                try
                {
                    Console.WriteLine("ProcessFile::Processing File \t" + file.Name);

                    ExecuteService(file.FullName);

                    // Move file to backup - add to zip
                    MoveFile(file, processedDirectory);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Failed to process File \t" + file.Name);
                    MoveFile(file, errorDirectory);
                    continue;
                }
            } //end for each file
        }

        /// <summary>
        /// Moves the current data file into a zip
        /// </summary>
        /// <param name="fInfo">The file</param>
        /// <param name="destDirectory">The directory where the zip file will be created</param>
        private static void MoveFile(FileInfo fInfo, string destDirectory)
        {
            string sourceFile = fInfo.FullName;
            //string destFile = Path.Combine(destDirectory, filename);

            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }

            Console.WriteLine("MoveFile::{0} to Destination Directory {1}.", sourceFile, destDirectory);
            string destFile = destDirectory + @"\" + fInfo.Name + "_" + DateTime.Now.ToString("dd-MM-yyyy");
            File.Move(fInfo.FullName, destFile);
            Console.WriteLine("MoveFile::{0} was moved to {1}.", sourceFile, destDirectory);
        }

        /// <summary>
        /// ExecuteService
        /// </summary>
        /// <param name="subDirInfo"></param>
        private void ExecuteService(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var parser = new X12Parser();
            var interchanges = parser.ParseMultiple(fs);
            fs.Close();

            // Edi section ISA
            var isa = interchanges[0];
            // Edi section GS
            var gs = isa.FunctionGroups.ToList()[0];
            // Edi section ST
            var st = gs.Transactions[0];

            if (st.GetIntElement(1) == 810)
            {
                // 810 - Invoice
                // Saves an incoming Invoice Edi
                _invoiceService.SaveEdiFile(interchanges);
            }
            else if (st.GetIntElement(1) == 850)
            {
                // 850 - PurchaseOrder
                // Saves an incoming PO Edi
                _purchaseOrderService.SavePOEdiFile(interchanges);
            }
            else if (st.GetIntElement(1) == 855)
            {
                // 855 - Acknowledgment
                // Saves an incoming Acknowledgment Edi
                _acknowledgmentService.SaveACKEdiFile(interchanges);
            }
            else if (st.GetIntElement(1) == 856)
            {
                // 856 - Advanced Shipping Notice
                // Saves an incoming Asn Edi
                _asnService.SaveAsnEdiFile(interchanges);
            }
            else
            {
                Console.WriteLine("Invalid EDI Type");
            }

        }
    }
}
