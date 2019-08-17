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
        public ActionResult  Get()
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
            var tree = _treeStorage.CreateTree(upsertTree);
            return Ok(tree);
        }

        [HttpPut("{guid}")]
        public ActionResult Put(Guid guid, [FromBody] UpsertTreeDto upsertTree)
        {
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
    }
}
