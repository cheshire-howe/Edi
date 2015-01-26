using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.Shared
{
    public class Ref
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// REF01 - Code qualifying the Reference Identification
        /// 61 = VAT number
        /// CO = Customer order number
        /// VN = Vendor order number
        /// </summary>
        [MaxLength(3)]
        public string REF01_ReferenceIdentificationQualifier { get; set; }
        /// <summary>
        /// REF02 - Reference information as defined by the particular Transaction Set
        /// or as specified by the Reference Identification Qualifier
        /// </summary>
        [MaxLength(30)]
        public string REF02_ReferenceIdentification { get; set; }
        /// <summary>
        /// REF03 - Free-form description to clarify the related data elements
        /// and their content
        /// </summary>
        [MaxLength(80)]
        public string REF03_Description { get; set; }
    }
}
