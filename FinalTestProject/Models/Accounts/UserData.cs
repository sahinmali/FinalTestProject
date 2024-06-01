using FinalTestProject.Data;

namespace FinalTestProject.Models.Accounts
{
    public class UserData
    {
        private List<Hesap> HesapList = [];
        public string Ad { get; set; } // �rnek olarak Ad �zelli�i eklendi
        public Danisman Danisman { get; set; } // Danisman �zelli�i eklendi
        public string Danisman_Ogrler { get; set; }

        public List<Hesap> GetAccountList()
        {
            return HesapList;
        }
    }
}
