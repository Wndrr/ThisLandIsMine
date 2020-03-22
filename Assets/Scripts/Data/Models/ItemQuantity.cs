public class ItemQuantity
{
    public ItemQuantity(ItemId id, int quantity)
    {
        Id = id;
        Quantity = quantity;
    }

    public ItemId Id { get; set; }
    public int Quantity { get; set; }

    public override string ToString()
    {
        return $"{Id.ToString()} - {Quantity}";
    }
}