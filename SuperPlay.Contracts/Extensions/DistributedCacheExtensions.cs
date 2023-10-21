using Microsoft.Extensions.Caching.Distributed;
using SuperPlay.Abstractions.Extensions;
using SuperPlay.Contracts.Login;

namespace SuperPlay.Contracts.Extensions
{
    public static class DistributedCacheExtensions
    {
        private const string UserConnectionFormat = "UserConnection:{0}";

        public static async ValueTask<bool> IsUserConnectedAsync(
            this IDistributedCache context, 
            Guid userId, 
            CancellationToken token = default)
        {
            context.ThrowIfNull(nameof(context));
        
            var key = string.Format(UserConnectionFormat, userId);
            var value = await context.GetAsync(key, token);

            return value is not null;
        }

        public static async ValueTask SetUserConnectionAsync(
            this IDistributedCache context,
            LoginRequest request,
            CancellationToken token = default)
        {
            var key = string.Format(UserConnectionFormat, request.Id);
        
            await context.SetEntityAsync(key, request, token);
        }
    }
}