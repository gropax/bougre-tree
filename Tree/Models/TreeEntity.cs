using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tree.Contracts;

namespace Tree.Models
{
    public class TreeEntity
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        public Guid Guid { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; }

        public TreeEntity() { }

        public TreeEntity(Guid id, string name, string description, DateTime createdAt, DateTime updatedAt)
        {
            Guid = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static TreeEntity FromDto(TreeDto tree)
        {
            return new TreeEntity(tree.Guid, tree.Name, tree.Description, tree.CreatedAt, tree.UpdatedAt);
        }

        public TreeDto ToDto()
        {
            return new TreeDto(Guid, Name, Description, CreatedAt, UpdatedAt);
        }
    }

    public class NodeEntity
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        public Guid Guid { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid TreeGuid { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid? ParentGuid { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        //public int Position { get; set; }

        public NodeEntity() { }

        public NodeEntity(Guid guid, Guid treeGuid, Guid? parentGuid, string name, string description)
        {
            Guid = guid;
            TreeGuid = treeGuid;
            ParentGuid = parentGuid;
            Name = name;
            Description = description;
        }

        public static NodeEntity FromDto(NodeDto node)
        {
            return new NodeEntity(node.Guid, node.TreeGuid, node.ParentGuid, node.Name, node.Description);
        }

        public NodeDto ToDto()
        {
            return new NodeDto(Guid, TreeGuid, ParentGuid, Name, Description);
        }
    }
}
