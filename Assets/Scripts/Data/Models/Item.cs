using System.Collections.Generic;
using System.Linq;

public class Item
{
    public ItemId Id { get; }
    public string Name { get; }
    public List<ItemQuantity> Recipe { get; }
    public bool IsCraftable => Recipe != null && Recipe.Any();

    public Item(ItemId id, string name, List<ItemQuantity> recipe = null)
    {
        Id = id;
        Name = name;
        Recipe = recipe;
    }

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }
}