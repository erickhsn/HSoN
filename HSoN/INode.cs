using System;
using System.Collections.Generic;
using System.Text;

namespace HSoN
{
    public interface INode
    {
        Guid Id { get; }
        // Should throw exception if a field with the same id already exists in the node
        void AddField(IField field);
        // Returns true if this node or one of its children contains a field with the
        // requested id.
        bool HasField(string fieldId);
        // Should return all fields contained in the node and its children.
        // Remarks:
        // - All return fields must have unique ids.
        IEnumerable<IField> GetAllFields();
        // Adds a new child node.
        // If the new added child will produce a cycle (see restrictions),
        // an exception should occur as the node cannot be added.
        // Note:
        // the added node can already be a child of this node.
        // It needs to be added either way.
        void AddNode(INode node);
        // Given a field id, return its parent node.
        // Should return null if the id no node can be found.
        // Note: Only ids of IField instances returned from the GetAllFields()
        // are supposed to return a node.
        INode GetParentNodeOfField(string fieldId);
    }
}
