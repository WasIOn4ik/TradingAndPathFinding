using UnityEngine;

public class TraderInventoryUI : BaseInventory
{
	#region Variables

	[SerializeField] protected CountSelectorUI countSelectorUI;
	[SerializeField] protected ConfirmationUI confirmationUI;

	#endregion

	#region Overrides

	public override void OnDragToInventoryComplete(ItemStack item, IItemsStorage target)
	{
		if(target == this)
			return;

		//Продажа торговцу

		if (item.itemBase.bStackable && item.count > 1)
		{
			countSelectorUI.Show(item, false, (itemsSelected, goldAmount) =>
			{
				item.count = itemsSelected;
				target.RemoveFromInventory(item);
				target.UpdateInventory();

				AddToInventory(item);

				UpdateInventory();

				GameBase.Instance.AddGold(goldAmount);
			});
		}
		else
		{
			confirmationUI.Show(item, false, () =>
			{
				target.RemoveFromInventory(item);
				target.UpdateInventory();

				AddToInventory(item);
				UpdateInventory();

				GameBase.Instance.AddGold(GameBase.GetSellPrice(item));
			});
		}
	}

	public override void OnDragFromInventoryComplete(ItemStack item, IItemsStorage target)
	{
		if (target == this)
			return;

		//Покупка у торговца
		if (item.itemBase.bStackable && item.count > 1)
		{
			countSelectorUI.Show(item, true, (itemsSelected, goldAmount) =>
			{
				item.count = itemsSelected;
				RemoveFromInventory(item);
				UpdateInventory();

				target.AddToInventory(item);
				target.UpdateInventory();

				GameBase.Instance.RemoveGold(goldAmount);
			});
		}
		else if (GameBase.GetBuyPrice(item) <= GameBase.Instance.GetGold())
		{
			confirmationUI.Show(item, true, () =>
			{
				RemoveFromInventory(item);
				UpdateInventory();

				target.AddToInventory(item);
				target.UpdateInventory();

				GameBase.Instance.RemoveGold(GameBase.GetBuyPrice(item));
			});
		}
	}

	#endregion
}
