using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tree.Contracts;
using Tree.Models;

namespace Tree.Controllers
{
    public class SetParentNodeCommand
    {
        private TreeStorage _treeStorage;

        public SetParentNodeCommand(TreeStorage treeStorage)
        {
            _treeStorage = treeStorage;
        }

        public long Execute(Guid treeGuid, SetParentNodeDto setParentNode)
        {
            // Read all tree from storage
            var nodes = _treeStorage.GetAllNodes(treeGuid);

            var nodesByGuids = nodes.ToDictionary(n => n.Guid, n => n);

            // Validates existence of nodes to be updated
            var toUpdate = new List<NodeDto>();
            var notFound = new List<Guid>();
            foreach (var nodeGuid in setParentNode.NodeGuids)
            {
                if (nodesByGuids.TryGetValue(nodeGuid, out var node))
                    toUpdate.Add(node);
                else
                    notFound.Add(nodeGuid);
            }

            // Validates existence of parent node
            if (!nodesByGuids.TryGetValue(setParentNode.ParentGuid, out var parent))
                notFound.Add(setParentNode.ParentGuid);

            if (notFound.Count > 0)
                throw new NotFoundException(string.Format("Nodes not found in Tree [{0}] : {1}.",
                    treeGuid, string.Join(", ", notFound.Select(g => $"[{g}]"))));

            // Validates absence of cycles
            var ancestors = TreeHelpers.GetAncestors(nodes, parent, includingSelf: true).ToArray();

            var cyclic = toUpdate.Where(n => ancestors.Contains(n)).ToArray();
            if (cyclic.Length > 0)
                throw new BadRequestException(string.Format("Cycles detected in Tree [{0}] for nodes {1}.",
                    treeGuid, string.Join(", ", cyclic.Select(g => $"[{g.Guid}]"))));

            return _treeStorage.SetParentNode(parent, toUpdate);
        }
    }
}
