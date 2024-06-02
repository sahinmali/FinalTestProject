namespace FinalTestProject.Services
{
    public class SessionState
    {
        public string UserTCKimlikNo { get; set; }
        public string Ad { get; set; }
        public string UserType { get; set; }

        //public bool IsAuthenticated { get; set; }

        public void ClearSession()
        {
            UserTCKimlikNo = null;
            UserType = null;
            Ad = null;
        }
    }
}
