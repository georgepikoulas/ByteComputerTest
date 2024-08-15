using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteComputerTest.Core
{
    public class Degree
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "The Name is required.")]
        [StringLength(50)]
        [DisplayName("Degree Name")]

        public string Name { get; set; } = string.Empty;

        [DisplayName("Creation Time Degree")]

        public DateTime CreationTime { get; set; } = DateTime.Now;

        public int CandidateId { get; set; }

        [DisplayName("Select Candidate for Degree")]

        public Candidate Candidate { get; set; }
    }
}
    