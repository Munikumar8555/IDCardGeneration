using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorWebApp.Services
{
    public class UserSessionService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string UserEmailKey = "currentUserEmail";

        public UserSessionService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetCurrentUserEmailAsync(string email)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserEmailKey, email);
        }

        public async Task<string> GetCurrentUserEmailAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UserEmailKey);
        }

        public async Task RemoveCurrentUserEmailAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UserEmailKey);
        }
    }
}
