using System.ComponentModel.DataAnnotations;

namespace Edi.Models.InvoiceModels
{
    public class InvoiceNote
    {
        /// <summary>
        /// Database Primary Key
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// NTE01 - Code for identifying the functional area or purpose for
        /// which the note applies
        /// </summary>
        [MaxLength(3)]
        public string NTE01_NoteReferenceCode { get; set; }
        /// <summary>
        /// NTE02 - Free-form description to clarify the related data elements
        /// and their content.
        /// This is not machine readable and should therefore be avoided
        /// </summary>
        [MaxLength(80)]
        public string NTE02_NoteDescription { get; set; }

        public int InvoiceID { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
