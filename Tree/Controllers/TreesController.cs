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
            try
            {
                return Ok(_treeStorage.GetTree(guid));
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
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
            try
            {
                return Ok(_treeStorage.UpdateTree(guid, upsertTree));
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }

        [HttpDelete("{guid}")]
        public ActionResult Delete(Guid guid)
        {
            try
            {
                return Ok(_treeStorage.DeleteTree(guid));
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }


        [HttpGet("{treeGuid}/Nodes")]
        public ActionResult GetNodes(Guid treeGuid)
        {
            try
            {
                return Ok(_treeStorage.GetAllNodes(treeGuid));
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }

        [HttpGet("Nodes/{guid}", Name = "GetNode")]
        public ActionResult GetNode(Guid guid)
        {
            try
            {
                return Ok(_treeStorage.GetNode(guid));
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }

        [HttpPost("{treeGuid}/Nodes")]
        public ActionResult CreateNode(Guid treeGuid, [FromBody] CreateNodeDto createNode)
        {
            if (!createNode.IsValid)
                return BadRequest();
            try
            {
                return Ok(_treeStorage.CreateNode(treeGuid, createNode));
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }

        [HttpPut("Nodes/{guid}")]
        public ActionResult PutNode(Guid guid, [FromBody] UpdateNodeDto updateNode)
        {
            if (updateNode.IsEmpty)
                return BadRequest();
            try
            {
                return Ok(_treeStorage.UpdateNode(guid, updateNode));
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }

        [HttpDelete("{treeGuid}/Nodes")]
        public ActionResult DeleteNodes(Guid treeGuid, [FromBody] DeleteNodesDto deleteNodes)
        {
            try
            {
                var cmd = new DeleteNodesCommand(_treeStorage);
                long deleted = cmd.Execute(treeGuid, deleteNodes);
                return Ok(new { Deleted = deleted });
            }
            catch (BadRequestException e)
            {
                return BadRequest(e);
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }

        [HttpPost("{treeGuid}/Nodes/SetParent")]
        public ActionResult SetParentNode(Guid treeGuid, [FromBody] SetParentNodeDto setParentNode)
        {
            if (!setParentNode.IsValid)
                return BadRequest("Invalid parameters");
            try
            {
                var cmd = new SetParentNodeCommand(_treeStorage);
                long updated = cmd.Execute(treeGuid, setParentNode);
                return Ok(new { Updated = updated });
            }
            catch (BadRequestException e)
            {
                return BadRequest(e);
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }
    }
}
