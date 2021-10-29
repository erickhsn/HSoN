using System;
using System.Linq;

namespace HSoN
{
    public class Program
    {
        static void Main(string[] args)
        {
            RootNode node = new RootNode(Guid.NewGuid());
            RootNode node1 = new RootNode(Guid.NewGuid());
            RootNode node2 = new RootNode(Guid.NewGuid());

            node1.AddField(new Field("4377e120-7167-4eb9-849c-d874c9695493"));
            node1.AddNode(node2);
            node2.AddField(new Field("4377e120-7167-4eb9-849c-d874c9695495"));
            node1.AddField(new Field("4377e120-7167-4eb9-849c-d874c9695495"));
            //node1.AddNode(node);
            node.AddNode(node1);


            var field = new Field(Guid.NewGuid().ToString());

            node.AddField(field);
            node.AddField(new Field(Guid.NewGuid().ToString()));
            node.AddField(new Field(Guid.NewGuid().ToString()));
            node.AddField(new Field(Guid.NewGuid().ToString()));
            node.AddField(new Field(Guid.NewGuid().ToString()));

            node.AddField(new Field("4377e120-7167-4eb9-849c-d874c9695492"));

            Console.WriteLine(string.Join(", ", node.GetAllFields().Select(x => x.Id).ToArray()));

            Console.WriteLine(node.HasField("4377e120-7167-4eb9-849c-d874c9695492"));
            Console.WriteLine(node.HasField("3eb5b873-73c0-4cc9-8e9b-04369867e922"));

            node.GetParentNodeOfField("4377e120-7167-4eb9-849c-d874c9695493");
            Console.WriteLine(node.GetParentNodeOfField("4377e120-7167-4eb9-849c-d874c9695494"));
            node.GetParentNodeOfField("4377e120-7167-4eb9-849c-d874c9695495");

        }
    }
}
