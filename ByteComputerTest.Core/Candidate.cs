using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ByteComputerTest.Core
{

    public class Candidate
    {

        [Key]
        public int Id { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "The Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("First Name")]
        [Required(ErrorMessage = "The First Name is required.")]
        [StringLength(50)]

        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// TOOD: check if ok 
        /// </summary>
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Phone Number")]

        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]

        public string? Mobile { get; set; }

        [DisplayName("Selected Degree")]

        [StringLength(50)]
        public string? SelectedDegree { get; set; }

        public byte[]? CV { get; set; }

        [StringLength(150)]

        public string? CVFilename { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

        public ICollection<Degree>? Degrees { get; set; }

    }

}
    