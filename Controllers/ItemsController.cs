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
      var items = repository.GetItems().Select( item => item.AsDTO());
      return items;
    }

    // GET /items/id
    [HttpGet("{id}")]
    public ActionResult<ItemDTO> GetItem(Guid id)
    {
      var item = repository.GetItem(id);

      if (item is null)
      {
        return NotFound();
      }

      return item.AsDTO();
    }

    //Post /items
    [HttpPost]
    public ActionResult<ItemDTO> CreateItem(CreateItemDTO itemDTO)
    {
       Item item = new()
       {
          Id = Guid.NewGuid(),
          Name = itemDTO.Name,
          Price = itemDTO.Price,
          CreatedDate = DateTimeOffset.UtcNow
       };

       repository.CreateItem(item);

       return CreatedAtRoute(nameof(GetItem), new {id = item.Id}, item.AsDTO());
    }

    // PUT /items/id
    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDTO itemDTO)
    {
      var exsistingItem = repository.GetItem(id);

      if(exsistingItem is null)
      {
        return NotFound();
      }

      Item updatedItem = exsistingItem with
      {
        Name = itemDTO.Name,
        Price = itemDTO.Price
      };

      repository.UpdateItem(updatedItem);
      return NoContent();
    }

    // Delete /items/id
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
       var exsistingItem = repository.GetItem(id);

      if(exsistingItem is null)
      {
        return NotFound();
      }

      repository.DeleteItem(id);

      return NoContent();
    }
  }
}