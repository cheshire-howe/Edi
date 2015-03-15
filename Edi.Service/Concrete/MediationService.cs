using Edi.Logic.Interfaces;
using Edi.Service.Interfaces;
using System;
using System.IO;

namespace Edi.Service.Concrete
{
    public class MediationService : IMediationService
    {
        private readonly IAcknowledgmentService _acknowledgmentService;
        private readonly IAsnService _asnService;
        private readonly IInvoiceService _invoiceService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IMediationLogic _mediationLogic;

        public MediationService(IAcknowledgmentService acknowledgmentService,
                                IAsnService asnService, IInvoiceService invoiceService,
                                IPurchaseOrderService purchaseOrderService,
                                IMediationLogic mediationLogic)
        {
            _acknowledgmentService = acknowledgmentService;
            _asnService = asnService;
            _invoiceService = invoiceService;
            _purchaseOrderService = purchaseOrderService;
            _mediationLogic = mediationLogic;
        }

        public void DelegateFiles()
        {
            FileInfo[] files;
            try
            {
                files = _mediationLogic.ProcessDirectory();
            }
            catch (Exception)
            {
                Console.WriteLine("Directory does not exist."); // will become log
                return;
            }

            foreach (var file in files)
            {
                try
                {
                    var interchanges = _mediationLogic.GetInterchanges(file.FullName);

                    var ediFileType = _mediationLogic.FindService(interchanges);

                    switch (ediFileType)
                    {
                        case 810:
                            // 810 - Invoice
                            _invoiceService.SaveEdiFile(interchanges);
                            break;
                        case 850:
                            // 850 - PurchaseOrder
                            _purchaseOrderService.SavePOEdiFile(interchanges);
                            break;
                        case 855:
                            // 855 - Acknowledgment
                            _acknowledgmentService.SaveACKEdiFile(interchanges);
                            break;
                        case 856:
                            // 856 - Advanced Shipping Notice
                            _asnService.SaveAsnEdiFile(interchanges);
                            break;
                    }
                    // Made it this far, move to success directory
                    _mediationLogic.MoveFile(file, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.TargetSite);
                    // Something went wrong, move to error directory
                    _mediationLogic.MoveFile(file, false);
                }
            }
        }
    }
}
