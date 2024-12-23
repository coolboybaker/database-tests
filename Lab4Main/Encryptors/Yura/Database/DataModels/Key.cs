using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab4Main
{
    public class Key
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string KeyText { get; set; }
        public Encoded Encoded { get; set; }
        public Decoded Decoded { get; set; }

        public override bool Equals(object obj)
        {
            return Id == ((Key)obj).Id && KeyText == ((Key)obj).KeyText && Encoded.EncodedText == ((Key)obj).Encoded.EncodedText && Decoded.DecodedText == ((Key)obj).Decoded.DecodedText;
        }
    }
}
