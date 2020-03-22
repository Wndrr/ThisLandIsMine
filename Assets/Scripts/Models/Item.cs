public class Item
{
    public ItemId Id { get; }
    public string Name { get; }

    public Item(ItemId id, string name)
    {
        Id = id;
        Name = name;
    }
}