using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tree.Contracts;
using Tree.Models;

namespace Tree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreesController : ControllerBase
    {
        private TreeStorage _treeStorage;

        public TreesController(TreeStorage treeStorage)
        {
            _treeStorage = treeStorage;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var trees = _treeStorage.GetAllTrees();
            return Ok(trees);
        }

        [HttpGet("{guid}", Name = "Get")]
        public ActionResult Get(Guid guid)
        {
            var tree = _treeStorage.GetTree(guid);
            if (tree == null)
                return NotFound();
            else
                return Ok(tree);
        }

        [HttpPost]
        public ActionResult Post([FromBody] UpsertTreeDto upsertTree)
        {
            if (!upsertTree.IsValid)
                return BadRequest();

            var tree = _treeStorage.CreateTree(upsertTree);
            return Ok(tree);
        }

        [HttpPut("{guid}")]
        public ActionResult Put(Guid guid, [FromBody] UpsertTreeDto upsertTree)
        {
            if (upsertTree.IsEmpty)
                return BadRequest();

            var tree = _treeStorage.UpdateTree(guid, upsertTree);
            if (tree == null)
                return NotFound();
            else
                return Ok(tree);
        }

        [HttpDelete("{guid}")]
        public ActionResult Delete(Guid guid)
        {
            var tree = _treeStorage.DeleteTree(guid);
            if (tree == null)
                return NotFound();
            else
                return Ok(tree);
        }


        [HttpGet("{treeGuid}/Nodes")]
        public ActionResult GetNodes(Guid treeGuid)
        {
            var nodes = _treeStorage.GetAllNodes(treeGuid);
            if (nodes == null)
                return NotFound();
            else
                return Ok(nodes);
        }

        [HttpGet("Nodes/{guid}", Name = "GetNode")]
        public ActionResult GetNode(Guid guid)
        {
            var node = _treeStorage.GetNode(guid);
            if (node == null)
                return NotFound();
            else
                return Ok(node);
        }

        [HttpPost("{treeGuid}/Nodes")]
        public ActionResult CreateNode(Guid treeGuid, [FromBody] CreateNodeDto createNode)
        {
            if (!createNode.IsValid)
                return BadRequest();

            var node = _treeStorage.CreateNode(treeGuid, createNode);
            if (node == null)
                return NotFound();
            else
                return Ok(node);
        }

        [HttpPut("Nodes/{guid}")]
        public ActionResult PutNode(Guid guid, [FromBody] UpdateNodeDto updateNode)
        {
            if (updateNode.IsEmpty)
                return BadRequest();

            var node = _treeStorage.UpdateNode(guid, updateNode);
            if (node == null)
                return NotFound();
            else
                return Ok(node);
        }

        //[HttpDelete("Nodes/{guid}")]
        //public ActionResult DeleteNode(Guid guid)
        //{
        //    var node = _treeStorage.DeleteNode(guid);
        //    if (node == null)
        //        return NotFound();
        //    else
        //        return Ok(node);
        //}
    }
}
