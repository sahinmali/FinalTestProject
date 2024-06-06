using Microsoft.AspNetCore.Components;

namespace FinalTestProject.Services
{
    public class LogoutService
    {
        private readonly SessionState _sessionState;
        private readonly NavigationManager _navigationManager;

        public LogoutService(SessionState sessionState, NavigationManager navigationManager)
        {
            _sessionState = sessionState;
            _navigationManager = navigationManager;
        }

        public async Task Logout()
        {
            _sessionState.ClearSession();
            _navigationManager.NavigateTo("/", true);
        }
    }
}
