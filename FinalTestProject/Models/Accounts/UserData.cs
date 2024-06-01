using FinalTestProject.Data;

namespace FinalTestProject.Models.Accounts
{
    public class UserData
    {
        private List<Hesap> HesapList = [];

        public List<Hesap> GetAccountList()
        {
            return HesapList;
        }
    }
}
