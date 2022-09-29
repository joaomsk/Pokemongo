using Domain.Collection;

namespace Domain.System;

public static class Collection
{
    public static string? GetCollectionName(this Type documentType)
    {
        return ((BsonCollectionAttribute)documentType
            .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
            .FirstOrDefault()!).CollectionName;
    }
}