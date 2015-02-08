using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AcknowledgmentModels
{
    public class AckTd5
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// TD502 - Code designating the system/method of code structure used for the
        /// identification code
        /// </summary>
        [MaxLength(2)]
        public string TD502_IdentificationCodeQualifier { get; set; }
        /// <summary>
        /// TD503 - Code identifying a party or other code
        /// </summary>
        [MaxLength(80)]
        public string TD503_IdentificationCode { get; set; }

        public int AcknowledgmentID { get; set; }
        public Acknowledgment Acknowledgment { get; set; }
    }
}
