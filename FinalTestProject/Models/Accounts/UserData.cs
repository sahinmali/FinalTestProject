using FinalTestProject.Data;

namespace FinalTestProject.Models.Accounts
{
    public class UserData
    {
        private List<Hesap> HesapList = [];
        public string Ad { get; set; } // Örnek olarak Ad özelliði eklendi
        public Danisman Danisman { get; set; } // Danisman özelliði eklendi
        public string Danisman_Ogrler { get; set; }

        public List<Hesap> GetAccountList()
        {
            return HesapList;
        }
    }
}
