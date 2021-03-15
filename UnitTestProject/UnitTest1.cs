using System;
using HashTableForStudents;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountIsZeroAfterCreation()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            Assert.AreEqual(0, hashTable.Count);
        }

        [TestMethod]
        public void CountIncreasesAfterAdding()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            hashTable.Add(5, 5);
            Assert.AreEqual(1, hashTable.Count);
        }

        [TestMethod]
        public void CountDecreasesAfterRemoving()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            hashTable.Add(5, 5);
            hashTable.Remove(5);
            Assert.AreEqual(0, hashTable.Count);
        }

        [TestMethod]
        public void RemoveReturnsFalseWhenKeyDoesntExist()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            hashTable.Add(5, 5);
            Assert.AreEqual(false, hashTable.Remove(6));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CantAddTwoPairsWithSameKey()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            hashTable.Add(5, 5);
            hashTable.Add(5, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ExceptionThrownWhenTryingToIndexateWithNonExistantKey()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            int a = hashTable[1];
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ExceptionThrownWhenTryingToIndexateWithNonExistantKeyTwo()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            hashTable.Add(62, 1);
            int a = hashTable[1];
        }

        [TestMethod]
        public void ContainsReturnsCorrectValue()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            hashTable.Add(1, 1);
            Assert.AreEqual(true, hashTable.Contains(1));
            Assert.AreEqual(false, hashTable.Contains(2));
        }

        [TestMethod]
        public void CollectionIsEnumerable()
        {
            ChainHashTable<int, int> hashTable = new ChainHashTable<int, int>();
            for (int i = 0; i < 50; i++)
            {
                hashTable.Add(i, 0);
            }
            foreach (var pair in hashTable)
            {
                Assert.AreEqual(0, pair.Value);
            }
        }
    }
}