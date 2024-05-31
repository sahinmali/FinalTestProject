using System.ComponentModel.DataAnnotations;
using static FinalTestProject.Models.Constants;

namespace FinalTestProject.Models.Accounts
{
    public class Hesap
    {
        public string Ad = "";

        [Key]
        public long TCKimlikNo { get; set; }
        public string Password = "";
        public UyelikType UyelikType;
    }
}
