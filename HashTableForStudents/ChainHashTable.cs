using System;
using System.Collections;
using System.Collections.Generic;

namespace HashTableForStudents
{
    public class ChainHashTable<TKey, TValue> : IHashTable<TKey, TValue>, IEnumerable<Pair<TKey, TValue>>
    {
        private List<Pair<TKey, TValue>>[] _table;
        public int Count { get; private set; }
        private const int MaxChainLength = 10;
        private int _currentChainLength;
        private readonly GetPrimeNumber _primeNumber = new GetPrimeNumber();
        private readonly HashMaker<TKey> _hashMaker;

        public ChainHashTable()
        {
            var capacity = _primeNumber.Next();
            _table = new List<Pair<TKey, TValue>>[capacity];
            _hashMaker = new HashMaker<TKey>(capacity);
        }

        public ChainHashTable(int primeNum)
        {
            var capacity = _primeNumber.Next();
            while (capacity < primeNum)
            {
                capacity = _primeNumber.Next();
            }
            _table = new List<Pair<TKey, TValue>>[primeNum];
            _hashMaker = new HashMaker<TKey>(primeNum);
        }

        public void Add(TKey key, TValue value)
        {
            var h = _hashMaker.ReturnHash(key);

            if (_table[h] == null || !_table[h].Exists(p => p.Key.Equals(key)))
            {
                if (_table[h] == null)
                    _table[h] = new List<Pair<TKey, TValue>>(MaxChainLength);
                var item = new Pair<TKey, TValue>(key, value);
                _table[h].Add(item);
                _currentChainLength = Math.Max(_currentChainLength, _table[h].Count);
                Count++;
            }
            else
            {
                throw new ArgumentException();
            }
            if (_currentChainLength >= MaxChainLength)
            {
                IncreaseTable();
            }
        }

        public bool Remove(TKey key)
        {
            var h = _hashMaker.ReturnHash(key);
            var pair = Find(key);
            if (pair != null)
            {
                _table[h].Remove(pair);
                Count--;
                return true;
            }
            else
                return false;
        }

        private void IncreaseTable()
        {
            int i = _primeNumber.Next();
            _hashMaker.SimpleNumber = i;
            var tempTable = _table;
            _table = new List<Pair<TKey, TValue>>[i];
            Count = 0;
            for (i = 0; i < tempTable.Length; i++)
            {
                if (tempTable[i] == null)
                    continue;
                foreach (var pair in tempTable[i])
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }

        public TValue this[TKey x]
        {
            get
            {
                var h = _hashMaker.ReturnHash(x);
                if (_table[h] == null)
                {
                    throw new KeyNotFoundException();
                }
                foreach (var pair in _table[h])
                {
                    if (pair.Key.Equals(x))
                    {
                        return pair.Value;
                    }
                }
                throw new KeyNotFoundException();
            }

            set
            {
                var pair = Find(x);
                if (pair == null)
                    throw new KeyNotFoundException();
                pair.Value = value;
            }
        }

        private Pair<TKey, TValue> Find(TKey key)
        {
            int h = _hashMaker.ReturnHash(key);
            if (_table[h] == null)
            {
                return null;
            }
            foreach (var pair in _table[h])
            {
                if (pair.Key.Equals(key))
                {
                    return pair;
                }
            }
            return null;
        }

        public bool Contains(TKey key)
        {
            if (Find(key) == null)
                return false;
            return true;
        }

        public IEnumerator<Pair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                if (_table[i] != null)
                {
                    foreach (var pair in _table[i])
                    {
                        yield return pair;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    internal class GetPrimeNumber
    {
        private int _current;
        private readonly int[] _primes = { 61, 127, 257, 523, 1087, 2213, 4519, 9619, 19717, 40009 };

        public int Next()
        {
            if (_current < _primes.Length)
            {
                var value = _primes[_current];
                _current++;
                return value;
            }
            _current++;
            return (_current - _primes.Length) * _primes[_primes.Length - 1];
        }
    }
}