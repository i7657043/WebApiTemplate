using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.Libs
{
    public class Word
    {
        public int? Id { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
