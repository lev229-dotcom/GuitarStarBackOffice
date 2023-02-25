namespace Portal.ModelConstants
{
    using System.ComponentModel.DataAnnotations;


    public class AddressesRequestModel
    {
        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string PostalCode { get; set; }

        [Required]
        //[MinLength(MinPhoneNumberLength)]
        //[MaxLength(MaxPhoneNumberLength)]
        //[RegularExpression(PhoneNumberRegularExpression)]
        public string PhoneNumber { get; set; }
    }
}
