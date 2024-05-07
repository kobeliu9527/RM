using Microsoft.AspNetCore.SignalR.Client;

namespace Dp.Wasm.Hubs
{
    public class SignalRReconnectInterVal: IRetryPolicy
    {
        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return TimeSpan.FromSeconds(2);
        }
    }
}
