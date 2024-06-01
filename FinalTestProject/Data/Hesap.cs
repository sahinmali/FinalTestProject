using System.ComponentModel.DataAnnotations;
using static FinalTestProject.Models.Constants;

namespace FinalTestProject.Data
{
    public class Hesap
    {
        public string? Ad { get; set; }

        [Key]
        public long TCKimlikNo { get; set; }
        public string? Password { get; set; }
        public UyelikType UyelikType { get; set; }
    }
}
