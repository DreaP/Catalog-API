using System;

namespace Catalog.Entities
{
  public record Item        // Something like a class.
  {

    public Guid Id {get; init;}

    public string Name {get; init;}

    public decimal Price {get; init;}

    public DateTimeOffset CreatedDate {get; init;}
  }
}