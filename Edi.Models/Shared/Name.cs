using System.ComponentModel.DataAnnotations;
using Edi.Models.InvoiceModels;

namespace Edi.Models.Shared
{
    public class Name
    {
        /// <summary>
        /// Database Primary Key
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// N101  - BT Bill-to-party
        ///         BY Buying Party (Purchaser)
        ///         CA Carrier
        ///         DU Reseller
        ///         EN End User
        ///         II Issuer of Invoice
        ///         PR Payer
        ///         SE Selling Party
        ///         SO Sold To if different from Bill To
        ///         ST Ship To
        /// </summary>
        [MaxLength(3)]
        public string N101_EntityIdentifierCode { get; set; }
        /// <summary>
        /// N102 - Free-form name
        /// </summary>
        [MaxLength(60)]
        public string N102_Name { get; set; }
        /// <summary>
        /// N103 - Optional
        /// Code designating the system/method of code structure used for
        /// Identifcation Code
        /// </summary>
        [MaxLength(2)]
        public string N103_IdentificationCodeQualifier { get; set; }
        /// <summary>
        /// N104 - Code identifying a party or other code
        /// </summary>
        [MaxLength(80)]
        public string N104_IdentificationCode { get; set; }
        /// <summary>
        /// N201 - Free-form name
        /// </summary>
        [MaxLength(60)]
        public string N201_Name { get; set; }
        /// <summary>
        /// N202 - Free-form name
        /// </summary>
        [MaxLength(60)]
        public string N202_Name { get; set; }
        /// <summary>
        /// N301 - Address information
        /// </summary>
        [MaxLength(55)]
        public string N301_Address { get; set; }
        /// <summary>
        /// N302 - Address information
        /// </summary>
        [MaxLength(55)]
        public string N302_Address { get; set; }
        /// <summary>
        /// N401 - City name
        /// </summary>
        [MaxLength(30)]
        public string N401_City { get; set; }
        /// <summary>
        /// N402 - State or Province code
        /// </summary>
        [MaxLength(2)]
        public string N402_State { get; set; }
        /// <summary>
        /// N403 - Zipcode or postal code
        /// </summary>
        [MaxLength(15)]
        public string N403_PostalCode { get; set; }
        /// <summary>
        /// N404 - Country code
        /// Not recommended for US and Canada
        /// </summary>
        [MaxLength(3)]
        public string N404_Country { get; set; }
    }
}
