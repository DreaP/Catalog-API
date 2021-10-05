using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DTO;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;




namespace Catalog.Controller
{

  [ApiController]
  [Route("items")]
  public class ItemsController : ControllerBase
  {
      private readonly IItemsRepository repository;

      public ItemsController(IItemsRepository repository)  // Constructor
      {
        this.repository = repository; // Creating a new instance of the item repository.
      }

    // GET /items
    [HttpGet]
    public IEnumerable<ItemDTO> GetItems()
    {
      var items = repository.GetItems().Select( item => new ItemDTO
      {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        CreatedDate = item.CreatedDate 
      });
      return items;
    }

    // GET /items/id
    [HttpGet("{id}")]
    public ActionResult<Item> GetItem(Guid id)
    {
      var item = repository.GetItem(id);

      if (item is null)
      {
        return NotFound();
      }

      return Ok(item);
    }
  }
}