using SuperPlay.Abstractions.Data;
using SuperPlay.Abstractions.Domain;

namespace SuperPlay.Data;

public static class RepositoryExtensions
{

    public static async ValueTask<UserResource> GetWalletAsync(
        this IGenericRepository<int, UserResource> context,
        Guid userId, int key, int value,
        CancellationToken token = default)
    {
        var item = await context.FindAsync(x=>
            x.UserId == userId &&
            x.ResourceType == ResourceTypeEnum.Wallet &&
            x.Key == key, token);

        if (item == null)
        {
            throw new KeyNotFoundException($"Resource Wallet with key {userId}: {key} not found");
        }

        return item;
    }
 
    
    public static async ValueTask<UserResource> UpdateWalletAsync(
        this IGenericRepository<int, UserResource> context, 
        Guid userId, int key, int value,
        CancellationToken token = default)
    {
        var item = await context.GetWalletAsync(userId, key, value, token);
        
        item.Value = value;

        await context.UpdateAsync(item, token);

        return item;
    }
    
    
    public static async ValueTask<UserResource> IncreaseWalletAsync(
        this IGenericRepository<int, UserResource> context, 
        Guid userId, int key, int value,
        CancellationToken token = default)
    {
        var item = await context.GetWalletAsync(userId, key, value, token);
        
        item.Value += value;

        await context.UpdateAsync(item, token);

        return item;
    }

    public static async ValueTask<UserResource> DecreaseWalletAsync(
        this IGenericRepository<int, UserResource> context, 
        Guid userId, int key, int value,
        CancellationToken token = default)
    {
        var item = await context.GetWalletAsync(userId, key, value, token);
        
        item.Value += value;

        await context.UpdateAsync(item, token);

        return item;
    }
}