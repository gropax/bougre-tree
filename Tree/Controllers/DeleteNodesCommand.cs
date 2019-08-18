using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tree.Contracts;
using Tree.Models;

namespace Tree.Controllers
{
    public class DeleteNodesCommand
    {
        private TreeStorage _treeStorage;

        public DeleteNodesCommand(TreeStorage treeStorage)
        {
            _treeStorage = treeStorage;
        }

        public long Execute(Guid treeGuid, DeleteNodesDto deleteNodes)
        {
            var nodes = _treeStorage.GetAllNodes(treeGuid);

            var toDelete = new List<NodeDto>();
            var notFound = new List<Guid>();
            foreach (var nodeGuid in deleteNodes.NodeGuids)
            {
                var node = nodes.FirstOrDefault(n => n.Guid == nodeGuid);
                if (node == null)
                    notFound.Add(nodeGuid);
                else
                    toDelete.Add(node);
            }

            if (notFound.Count > 0)
                throw new NotFoundException(string.Format("Nodes not found in Tree [{0}] : {1}.",
                    treeGuid, string.Join(", ", notFound.Select(g => $"[{g}]"))));

            var subforest = TreeHelpers.GetSubforest(nodes, toDelete);

            if (!deleteNodes.Recursive && subforest.Length > toDelete.Count)
                throw new BadRequestException("Can't delete nodes that have children (use the `Recursive` option).");
            else
                return _treeStorage.DeleteNodes(subforest);
        }
    }
}
