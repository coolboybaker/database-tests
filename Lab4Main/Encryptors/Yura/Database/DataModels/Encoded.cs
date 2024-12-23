using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab4Main
{
    public class Encoded
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int KeyId { get; set; }
        public Key Key { get; set; }
        public string EncodedText { get; set; }
    }
}
