namespace FinalTestProject.Models.Accounts
{
    public class UserData
    {
        private List<Hesap> HesapList = [];

        private void AddData(Hesap data)
        {
            HesapList.Add(data);
        }

        public List<Hesap> GetAccountList()
        {
            return HesapList;
        }
    }
}
