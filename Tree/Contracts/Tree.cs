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
        public Guid TreeGuid { get; }
        [DataMember]
        public Guid? ParentGuid { get; }
        [DataMember]
        public string Name { get; }
        [DataMember]
        public string Description { get; }

        public NodeDto(Guid guid, Guid treeGuid, Guid? parentGuid, string name, string description)
        {
            Guid = guid;
            TreeGuid = treeGuid;
            ParentGuid = parentGuid;
            Name = name;
            Description = description;
        }
    }

    [DataContract(Namespace = "tree")]
    public class UpsertTreeDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }

        public bool IsEmpty => Name == null && Description == null;
        public bool IsValid => Name != null;
    }

    [DataContract(Namespace = "tree")]
    public class CreateNodeDto
    {
        [DataMember]
        public Guid? ParentGuid { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }

        public bool IsValid => Name != null && Description != null;
    }

    [DataContract(Namespace = "tree")]
    public class UpdateNodeDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }

        public bool IsEmpty => Name == null && Description == null;
    }

    [DataContract(Namespace = "tree")]
    public class DeleteNodesDto
    {
        [DataMember]
        public Guid[] NodeGuids { get; set; }
        [DataMember]
        public bool Recursive { get; set; }

        public bool IsValid => NodeGuids.Length > 0;
    }

    [DataContract(Namespace = "tree")]
    public class SetParentNodeDto
    {
        [DataMember]
        public Guid ParentGuid { get; set; }
        [DataMember]
        public Guid[] NodeGuids { get; set; }

        public bool IsValid => NodeGuids.Length > 0;
    }
}
