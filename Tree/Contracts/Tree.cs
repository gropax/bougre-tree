using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tree.Contracts
{
    [DataContract(Namespace = "tree")]
    public class TreeDto
    {
        [DataMember]
        public Guid Guid { get; }
        [DataMember]
        public string Name { get; }
        [DataMember]
        public string Description { get; }
        [DataMember]
        public DateTime CreatedAt { get; }
        [DataMember]
        public DateTime UpdatedAt { get; }

        public TreeDto(Guid id, string name, string description, DateTime createdAt, DateTime updatedAt)
        {
            Guid = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }

    public class NodeDto
    {
        [DataMember]
        public Guid Guid { get; }
        [DataMember]
        public Guid ParentId { get; }
        [DataMember]
        public string Label { get; }
        [DataMember]
        public int Position { get; }

        public NodeDto(Guid id, Guid parentId, string label, int position)
        {
            Guid = id;
            ParentId = parentId;
            Label = label;
            Position = position;
        }
    }

    [DataContract(Namespace = "tree")]
    public class UpsertTreeDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
