﻿namespace HashTableForStudents
{
    internal interface IHashTable<in TKey, in TValue>
    {
        void Add(TKey x, TValue y);

        bool Remove(TKey x);

        bool Contains(TKey x);
    }

    public interface IHash<in TKey>
    {
        int GetHash(TKey key, int i = 0);
    }
}