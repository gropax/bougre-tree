using Tree.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tree.Models
{
    public class TreeStorage
    {
        private IMongoDatabase _database;

        private IMongoCollection<TreeEntity> _treesCollections;
        private IMongoCollection<NodeEntity> _nodesCollections;

        private const string TREES_COLLECTION_NAME = "trees";
        private const string NODES_COLLECTION_NAME = "nodes";

        public TreeStorage(IMongoDatabase database)
        {
            _database = database;
            _treesCollections = _database.GetCollection<TreeEntity>(TREES_COLLECTION_NAME);
            _nodesCollections = _database.GetCollection<NodeEntity>(NODES_COLLECTION_NAME);
        }

        public TreeDto[] GetAllTrees()
        {
            return _treesCollections.Find(_ => true).ToEnumerable()
                .Select(e => e.ToDto()).ToArray();
        }

        public TreeDto GetTree(Guid guid)
        {
            return _treesCollections.Find(t => t.Guid == guid).FirstOrDefault()?.ToDto();
        }

        public TreeDto CreateTree(UpsertTreeDto tree)
        {
            var entity = new TreeEntity(
                Guid.NewGuid(),
                tree.Name,
                tree.Description,
                createdAt: DateTime.Now,
                updatedAt: DateTime.Now);

            _treesCollections.InsertOne(entity);
            return entity.ToDto();
        }

        public TreeDto UpdateTree(Guid guid, UpsertTreeDto upsert)
        {
            var tree = _treesCollections.FindOneAndUpdate<TreeEntity>(
                t => t.Guid == guid,
                Builders<TreeEntity>.Update
                    .Set(t => t.Name, upsert.Name)
                    .Set(t => t.Description, upsert.Description)
                    .Set(t => t.UpdatedAt, DateTime.Now),
                new FindOneAndUpdateOptions<TreeEntity> { ReturnDocument = ReturnDocument.After });

            return tree?.ToDto();
        }

        public TreeDto DeleteTree(Guid guid)
        {
            return _treesCollections.FindOneAndDelete(t => t.Guid == guid)?.ToDto();
        }
    }
}
