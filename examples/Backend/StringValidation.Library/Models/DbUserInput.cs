using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StringValidation.Library.Models
{
    /// <summary>
    /// Our Database Model for UserInput
    /// </summary>
    [Table("UserInput")]
    public class DbUserInput
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
		public string Input { get; set; }

		public bool IsValid { get; set; }
	}
}

