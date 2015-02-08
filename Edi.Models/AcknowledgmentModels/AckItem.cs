using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.AcknowledgmentModels
{
    public class AckItem : Item
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// PID01 - Code indicating the format of the description
        /// F -> free form
        /// </summary>
        [MaxLength(1)]
        public string PID01_ItemDescriptionType { get; set; }
        /// <summary>
        /// PID05 - A free-form description to clarify the related data elements
        /// and their content
        /// </summary>
        [MaxLength(80)]
        public string PID05_Description { get; set; }

        public int AcknowledgmentID { get; set; }
        public Acknowledgment Acknowledgment { get; set; }
    }
}
