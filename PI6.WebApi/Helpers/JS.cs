using Microsoft.JSInterop;

namespace PI6.WebApi.Helpers
{
    public class JS
    {
        private readonly IJSRuntime JsRuntime;
        public JS(IJSRuntime jSRuntime)
        {
            this.JsRuntime = jSRuntime;
        }

        public async Task LogAsync(string message)
        {
            await JsRuntime.InvokeVoidAsync("console.log", message);
        }
    }
}