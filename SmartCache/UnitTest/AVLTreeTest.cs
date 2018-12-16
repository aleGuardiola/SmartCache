using AVLTree;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace UnitTest
{
    public class AVLTreeTest
    {
        const int dataSize = 10000000;
        AVLTree<string, string> treeWithLargeData = new AVLTree<string, string>();
        Dictionary<string, string> dictionaryWithLargeData = new Dictionary<string, string>();
        public AVLTreeTest()
        {
            for (int i = 0; i < dataSize; i++)
            {
                treeWithLargeData.Add(i.ToString(), i.ToString() + "value");
                dictionaryWithLargeData.Add(i.ToString(), i.ToString() + "value");
            }
                
        }

        [Theory]
        [MemberData(nameof(DataSet))]
        public void TestInsert(string key, string value)
        {
            AVLTree<string, string> tree = new AVLTree<string, string>();
            tree[key] = value;
        }

        [Theory]
        [MemberData(nameof(DataSet))]
        public void TestInsertAndSearch(string key, string value)
        {
            AVLTree<string, string> tree = new AVLTree<string, string>();
            tree[key] = value;

            var val = tree[key];
            Assert.Equal(value, val);//Check if value is valid

            Assert.False(tree.ContainsKey(key + "test"));
            Assert.True(tree.ContainsKey(key));

            Assert.Throws<KeyNotFoundException>(()=> tree[key + "Test"] );//Test of throw exception if value not exist
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(5000)]
        [InlineData(10000)]
        [InlineData(50000)]
        [InlineData(100000)]
        [InlineData(500000)]
        [InlineData(1000000)]
        [InlineData(5000000)]
        public void TestLargeData(int dataCount)
        {
            AVLTree<string, string> tree = new AVLTree<string, string>();
            for (int i = 1; i <= dataCount; i++)
            {
                tree.Add(i.ToString(), i.ToString() + "value");
            }

            for(int i = dataCount; i >= 1; i--)
            {
                var value = tree[i.ToString()];
                Assert.Equal(i.ToString() + "value", value);
            }

            Assert.Equal(dataCount, tree.Count);
            tree.Clear();
            Assert.Empty(tree);
        }

        [Fact]
        void TestLargeDataSearch()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for(int i = dataSize - 1; i >= 0; i--)
            {
                var value = treeWithLargeData[i.ToString()];
                Assert.Equal(i.ToString() + "value", value);
            }
            sw.Stop();            
        }

        [Fact]
        void TestLargeDataSearchDictionary()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = dataSize - 1; i >= 0; i--)
            {
                var value = dictionaryWithLargeData[i.ToString()];
                Assert.Equal(i.ToString() + "value", value);
            }
            sw.Stop();
        }

        public static List<object[]> DataSet = new List<object[]>()
        {
            new[] { "Alejandro", "Guardiola" }, 
            new[] { "Leo", "Jaime" }, 
            new[] { "Anthony", "Fernandez" }, 
            new[] { "Francisco", "Munoz" }, 
            new[] { "Hilsy", "Rosario" }, 
            new[] { "Asdrubal", "Acosta" }, 
            new[] { "Martha", "Leon" }, 
        };

    }
}
