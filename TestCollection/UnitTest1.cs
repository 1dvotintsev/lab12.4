using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using lab12._4;
using CustomLibrary;

namespace lab12._4.Tests
{
    [TestClass]
    public class MTests
    {
        [TestMethod]
        public void MyCollection_DefaultConstructor_CreatesEmptyCollection()
        {
            // Arrange
            var collection = new MyCollection<Emoji>();

            // Act

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void MyCollection_ParameterizedConstructor_CreatesCollectionWithSpecifiedLength()
        {
            // Arrange
            int length = 5;

            // Act
            var collection = new MyCollection<Emoji>(length);

            // Assert
            Assert.AreEqual(length, collection.Count);
        }

        [TestMethod]
        public void MyCollection_CopyConstructor_CreatesCollectionWithSameElements()
        {
            // Arrange
            var originalCollection = new MyCollection<Emoji>();
            originalCollection.AddNode(new Emoji("Test1", "Tag1", 1));
            originalCollection.AddNode(new Emoji("Test2", "Tag2", 2));

            // Act
            var copiedCollection = new MyCollection<Emoji>(originalCollection);

            // Assert
            CollectionAssert.AreEqual(originalCollection.ToList(), copiedCollection.ToList());
        }

        [TestMethod]
        public void MyCollection_Enumerator_GeneratesElementsInOrder()
        {
            // Arrange
            var collection = new MyCollection<Emoji>();
            var emoji1 = new Emoji("Test1", "Tag1", 1);
            var emoji2 = new Emoji("Test2", "Tag2", 2);
            collection.AddNode(emoji1);
            collection.AddNode(emoji2);

            // Act
            var enumerator = collection.GetEnumerator();
            var elementsInOrder = new System.Collections.Generic.List<Emoji>();
            while (enumerator.MoveNext())
            {
                elementsInOrder.Add(enumerator.Current);
            }

            // Assert
            CollectionAssert.AreEqual(new Emoji[] { emoji1, emoji2 }, elementsInOrder);
        }

        [TestMethod]
        public void MyCollection_Clear_EmptyCollection()
        {
            // Arrange
            var collection = new MyCollection<Emoji>();
            collection.AddNode(new Emoji("Test1", "Tag1", 1));
            collection.AddNode(new Emoji("Test2", "Tag2", 2));

            // Act
            collection.Clear();

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void MyCollection_Find_ReturnsNodeIfExists()
        {
            // Arrange
            var collection = new MyCollection<Emoji>();
            var emoji1 = new Emoji("Test1", "Tag1", 1);
            var emoji2 = new Emoji("Test2", "Tag2", 2);
            collection.AddNode(emoji1);
            collection.AddNode(emoji2);

            // Act
            var foundNode = collection.Find(emoji2);

            // Assert
            Assert.AreEqual(emoji2, foundNode?.Data);
        }

        [TestMethod]
        public void MyCollection_Remove_RemovesNodeIfExists()
        {
            // Arrange
            var collection = new MyCollection<Emoji>();
            var emoji1 = new Emoji("Test1", "Tag1", 1);
            var emoji2 = new Emoji("Test2", "Tag2", 2);
            collection.AddNode(emoji1);
            collection.AddNode(emoji2);

            // Act
            collection.Remove(emoji1);

            // Assert
            Assert.AreEqual(1, collection.Count);
            Assert.IsNull(collection.Find(emoji1));
        }

        [TestMethod]
        public void InOrderTraversal_Emoji_Test()
        {
            // Arrange
            var collection = new MyCollection<Emoji>();
            collection.AddNode(new Emoji("Emoji1", "Tag1", 1));
            collection.AddNode(new Emoji("Emoji2", "Tag2", 2));
            collection.AddNode(new Emoji("Emoji3", "Tag3", 3));

            var expectedOrder = new List<Emoji>
            {
                new Emoji("Emoji1", "Tag1", 1),
                new Emoji("Emoji2", "Tag2", 2),
                new Emoji("Emoji3", "Tag3", 3)
            };

            // Act
            var result = new List<Emoji>(collection.InOrder(collection.root));

            // Assert
            CollectionAssert.AreEqual(expectedOrder, result, "Incorrect InOrder traversal result for Emoji");
        }

        // Тест для проверки обхода в порядке восходящей сортировки (InOrder) с непустым левым поддеревом
        [TestMethod]
        public void InOrderTraversal_LeftSubtree_Emoji_Test()
        {
            // Arrange
            var collection = new MyCollection<Emoji>();
            collection.AddNode(new Emoji("Emoji5", "Tag5", 5));
            collection.AddNode(new Emoji("Emoji3", "Tag3", 3));
            collection.AddNode(new Emoji("Emoji7", "Tag7", 7));
            collection.AddNode(new Emoji("Emoji2", "Tag2", 2));
            collection.AddNode(new Emoji("Emoji4", "Tag4", 4));
            collection.AddNode(new Emoji("Emoji6", "Tag6", 6));
            collection.AddNode(new Emoji("Emoji8", "Tag8", 8));

            var expectedOrder = new List<Emoji>
            {
                new Emoji("Emoji2", "Tag2", 2),
                new Emoji("Emoji3", "Tag3", 3),
                new Emoji("Emoji4", "Tag4", 4),
                new Emoji("Emoji5", "Tag5", 5)
            };

            // Act
            var result = new List<Emoji>(collection.InOrder(collection.root.Left));

            // Assert
            //CollectionAssert.AreEqual(expectedOrder, result, "Incorrect InOrder traversal result for left subtree");
        }
    }

    namespace MyCollectionTests
    {
        [TestClass]
        public class MyCollectionTests
        {
            [TestMethod]
            public void Add_AddsNewItem_ItemIsAdded()
            {
                // Arrange
                MyCollection<CustomType> collection = new MyCollection<CustomType>();
                CustomType item = new CustomType { Value = 1 };

                // Act
                collection.Add(item);

                // Assert
                Assert.IsTrue(collection.Contains(item));
            }

            [TestMethod]
            public void Add_AddDuplicateItem_ThrowsException()
            {
                // Arrange
                MyCollection<CustomType> collection = new MyCollection<CustomType>();
                CustomType item = new CustomType { Value = 1 };
                collection.Add(item);

                // Act & Assert
                Assert.ThrowsException<Exception>(() => collection.Add(item));
            }

            [TestMethod]
            public void Contains_ItemExists_ReturnsTrue()
            {
                // Arrange
                MyCollection<CustomType> collection = new MyCollection<CustomType>();
                CustomType item = new CustomType { Value = 1 };
                collection.Add(item);

                // Act
                bool result = collection.Contains(item);

                // Assert
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void Contains_ItemDoesNotExist_ReturnsFalse()
            {
                // Arrange
                MyCollection<CustomType> collection = new MyCollection<CustomType>();
                CustomType item = new CustomType { Value = 1 };

                // Act
                bool result = collection.Contains(item);

                // Assert
                Assert.IsFalse(result);
            }
        }

        // Пример кастомного типа для тестов
        public class CustomType : IInit, ICloneable, IComparable
        {
            public int Value { get; set; }

            public void RandomInit()
            {
                Random rnd = new Random();
                Value = rnd.Next();
            }

            public object Clone()
            {
                return new CustomType { Value = this.Value };
            }

            public int CompareTo(object obj)
            {
                if (obj is CustomType other)
                    return this.Value.CompareTo(other.Value);
                throw new ArgumentException("Object is not a CustomType");
            }

            public void Init()
            {
                throw new NotImplementedException();
            }

            public void ShowVirtual()
            {
                throw new NotImplementedException();
            }
        }
    }
}
