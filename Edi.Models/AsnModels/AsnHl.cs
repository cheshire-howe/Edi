using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edi.Models.AsnModels
{
    public abstract class AsnHl
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// HL01 - Hierarchical Id number
        /// </summary>
        [MaxLength(12)]
        public string HL01_IdNumber { get; set; }
        /// <summary>
        /// HL02 - Hierarchical Parent Id number. Not used in Shipment Level
        /// </summary>
        [MaxLength(12)]
        public string HL02_ParentId { get; set; }
        /// <summary>
        /// HL03 - Hierarchical level code
        /// </summary>
        [MaxLength(12)]
        public string HL03_LevelCode { get; set; }
        /// <summary>
        /// HL02 - Hierarchical Child Code
        /// </summary>
        [MaxLength(12)]
        public string HL04_ChildCode { get; set; }
    }
}
