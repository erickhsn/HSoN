using HSoN;
using System;
using System.Linq;
using Xunit;

namespace HSON.Test
{
    public class FieldTest
    {
        [Fact]
        public void AddField_ThrowsException()
        {
            RootNode node = new RootNode(Guid.NewGuid());
            Field field = new Field("4377e120-7167-4eb9-849c-d874c9695493");
            node.AddField(field);

            Action actual = () => node.AddField(field);

            Assert.Throws<Exception>(actual);
        }

        [Fact]
        public void AddNode_ThrowsException()
        {
            RootNode node1 = new RootNode(Guid.NewGuid());
            RootNode node2 = new RootNode(Guid.NewGuid());
            RootNode node3 = new RootNode(Guid.NewGuid());
            node1.AddNode(node2);
            node2.AddNode(node3);

            Assert.Throws<Exception>(() => node3.AddNode(node1));
        }

        [Fact]
        public void GetAllFields_ReturnsCorrectResult()
        {
            int expected = 6;
            RootNode node1 = new RootNode(Guid.NewGuid());
            RootNode node2 = new RootNode(Guid.NewGuid());
            RootNode node3 = new RootNode(Guid.NewGuid());

            node1.AddNode(node2);
            node2.AddNode(node3);

            node1.AddField(new Field(Guid.NewGuid().ToString()));
            node1.AddField(new Field(Guid.NewGuid().ToString()));
            node1.AddField(new Field(Guid.NewGuid().ToString()));
            node2.AddField(new Field(Guid.NewGuid().ToString()));
            node2.AddField(new Field(Guid.NewGuid().ToString()));
            node3.AddField(new Field(Guid.NewGuid().ToString()));


            var result = node1.GetAllFields().ToList();

            Assert.Equal(expected, result.Count);
        }

        [Fact]
        public void GetParentNodeOfField_ReturnsCorrectResult()
        {
            string param = "4377e120-7167-4eb9-849c-d874c9695493";
            RootNode node1 = new RootNode(Guid.NewGuid());
            RootNode node2 = new RootNode(Guid.NewGuid());
            RootNode node3 = new RootNode(Guid.NewGuid());

            node1.AddNode(node2);
            node2.AddNode(node3);

            node1.AddField(new Field(Guid.NewGuid().ToString()));
            node1.AddField(new Field(Guid.NewGuid().ToString()));
            node1.AddField(new Field(Guid.NewGuid().ToString()));
            node2.AddField(new Field(Guid.NewGuid().ToString()));
            node2.AddField(new Field(Guid.NewGuid().ToString()));
            node3.AddField(new Field("4377e120-7167-4eb9-849c-d874c9695493"));

            RootNode expected = node3;


            var result = node1.GetParentNodeOfField(param);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void HasField_ReturnsTrue()
        {
            string id1 = "4377e120-7167-4eb9-849c-d874c9695491";
            string id2 = "4377e120-7167-4eb9-849c-d874c9695492";
            string id3 = "4377e120-7167-4eb9-849c-d874c9695493";

            bool expected = true;

            RootNode node = new RootNode(Guid.NewGuid());
            node.AddField(new Field(id1));
            node.AddField(new Field(id2));
            node.AddField(new Field(id3));

            var result = node.HasField(id2);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void HasField_ReturnsFalse()
        {
            string id1 = "4377e120-7167-4eb9-849c-d874c9695491";
            string id2 = "4377e120-7167-4eb9-849c-d874c9695492";
            string id3 = "4377e120-7167-4eb9-849c-d874c9695493";

            string id4 = "4377e120-7167-4eb9-849c-d874c9695494";

            bool expected = false;

            RootNode node = new RootNode(Guid.NewGuid());
            node.AddField(new Field(id1));
            node.AddField(new Field(id2));
            node.AddField(new Field(id3));

            var result = node.HasField(id4);

            Assert.Equal(expected, result);
        }

    }
}
