
using FinalTestProject.Data;

namespace FinalTestProject.Services
{
    public class SessionState
    {
        public Hesap? AssignedHesap { get; set; }

        public bool IsAuthenticated => AssignedHesap != null;

        public void ClearSession()
        {
            AssignedHesap = null;
        }
    }
}
