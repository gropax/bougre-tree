using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Tree.Contracts
{
    public static class TreeHelpers
    {
        public static NodeDto[] GetSubforest(IEnumerable<NodeDto> nodes, IList<NodeDto> ancestors)
        {
            var inside = new List<NodeDto>(ancestors);
            var outside = nodes.Where(n => !ancestors.Contains(n)).ToList();

            bool changed;
            do
            {
                changed = false;
                var newInside = new List<NodeDto>(inside);
                var newOutside = new List<NodeDto>(outside);

                foreach (var node in outside)
                {
                    foreach (var forestNode in inside)
                    {
                        if (node.ParentGuid == forestNode.Guid)
                        {
                            newInside.Add(node);
                            newOutside.Remove(node);
                            changed = true;
                            break;
                        }
                    }
                }

                inside = newInside;
                outside = newOutside;

            } while (changed);

            return inside.ToArray();
        }

        public static IEnumerable<NodeDto> GetAncestors(IList<NodeDto> treeNodes, NodeDto node, bool includingSelf = false)
        {
            if (includingSelf)
                yield return node;

            var nodesByGuids = treeNodes.ToDictionary(n => n.Guid, n => n);

            Guid? parentGuid = node.ParentGuid;
            while (parentGuid.HasValue)
            {
                if (!nodesByGuids.TryGetValue(parentGuid.Value, out var parent))
                    throw new TreeModelException("Unknown parent Node [{parentGuid.Value}].");
                else
                {
                    yield return parent;
                    parentGuid = parent.ParentGuid;
                }
            }
        }

        public static Dictionary<NodeDto, ReadOnlyCollection<NodeDto>> GetChildren(IEnumerable<NodeDto> nodes, IList<NodeDto> parentNodes)
        {
            var childrenDict = parentNodes.ToDictionary(n => n, n => new List<NodeDto>());

            foreach (var node in nodes)
                foreach (var parent in parentNodes)
                    if (node.ParentGuid == parent.Guid)
                        childrenDict[parent].Add(node);

            return childrenDict.ToDictionary(kv => kv.Key, kv => kv.Value.AsReadOnly());
        }
    }

    [Serializable]
    internal class TreeModelException : Exception
    {
        public TreeModelException(string message) : base(message)
        {
        }
    }
}
