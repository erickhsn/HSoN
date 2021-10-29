using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HSoN
{
    public class RootNode : INode
    {
        public Guid Id { get; }
        
        private Queue<RootNode> Nodes = new Queue<RootNode>();
        
        private LinkedList<IField> Fields = new LinkedList<IField>();

        public RootNode(Guid id)
        {
            Id = id;
        }

        public void AddField(IField field)
        {
            if (!Fields.Contains(field))
                Fields.AddFirst(field);
            else
                throw new Exception("The field already exists!");

        }

        public void AddNode(INode node)
        {
            var tempNode = node as RootNode;
            if (SearchNode(tempNode, this) != null)
                throw new Exception("The node produces loop!");
            else
                Nodes.Enqueue((RootNode)node);
        }

        public IEnumerable<IField> GetAllFields()
        {
            List<IField> allFields = new List<IField>();
            allFields.AddRange(GetAllFields(this));
            return allFields.DistinctBy(f => f.Id);
        }

        public INode GetParentNodeOfField(string fieldId)
        {
            var x = GetParentNodeOfField(this, fieldId);

            return x;
        }

        public bool HasField(string fieldId)
        {
            if (Fields.Where(x => x.Id == fieldId).Any())
                return true;
            else
                return false;
        }

        private RootNode GetParentNodeOfField(RootNode root, string target)
        {
            Queue<RootNode> Q = new Queue<RootNode>();
            HashSet<RootNode> S = new HashSet<RootNode>();
            Q.Enqueue(root);
            S.Add(root);

            while (Q.Count > 0)
            {
                RootNode e = Q.Dequeue();
                if (e.Fields.Where(x => x.Id == target).Any())
                    return e;
                foreach (RootNode child in e.Nodes)
                {
                    if (!S.Contains(child))
                    {
                        Q.Enqueue(child);
                        S.Add(child);
                    }
                }
            }
            return null;
        }

        private RootNode SearchNode(RootNode root, RootNode target)
        {
            if (root == target)
                return root;
            Queue<RootNode> Q = new Queue<RootNode>();
            HashSet<RootNode> S = new HashSet<RootNode>();
            Q.Enqueue(root);
            S.Add(root);

            while (Q.Count > 0)
            {
                RootNode e = Q.Dequeue();
                if (e.Nodes.Where(x => x == target).Any())
                    return e;
                foreach (RootNode child in e.Nodes)
                {
                    if (!S.Contains(child))
                    {
                        Q.Enqueue(child);
                        S.Add(child);
                    }
                }
            }
            return null;
        }

        private List<IField> GetAllFields(RootNode root)
        {
            Queue<RootNode> Q = new Queue<RootNode>();
            HashSet<RootNode> S = new HashSet<RootNode>();
            List<IField> fields = new List<IField>();
            fields.AddRange(root.Fields);
            Q.Enqueue(root);
            S.Add(root);

            while (Q.Count > 0)
            {
                RootNode e = Q.Dequeue();
                foreach (RootNode child in e.Nodes)
                {
                    fields.AddRange(child.Fields);
                    if (!S.Contains(child))
                    {
                        Q.Enqueue(child);
                        S.Add(child);
                    }
                }
            }
            return fields;
        }

    }
}
