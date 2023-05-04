public interface IItemsStorage
{
	public void OnDragFromInventoryComplete(ItemStack item, IItemsStorage target);

	public void OnDragToInventoryComplete(ItemStack item, IItemsStorage target);

	public void AddToInventory(ItemStack itemToAdd);

	public void RemoveFromInventory(ItemStack itemToRemove);

	public void UpdateInventory();
}
