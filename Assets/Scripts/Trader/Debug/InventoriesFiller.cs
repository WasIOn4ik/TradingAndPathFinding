using UnityEngine;

public class InventoriesFiller : MonoBehaviour
{
	#region Variables

	[Header("Components")]
	[SerializeField] private PlayerInventoryUI playerInventoryUI;
	[SerializeField] private TraderInventoryUI traderInventoryUI;

	[SerializeField] private ItemsLibrarySO itemsLibrary;

	[Header("Properties")]
	[SerializeField] private int itemsToGenerateToPlayer;
	[SerializeField] private int itemsToGenerateToTrader;
	[SerializeField] private int itemsToGenerateWhenStackable;
	[SerializeField] private int playerStartMoney;

	#endregion

	#region Functions

	private void Start()
	{
		for (int i = 0; i < itemsToGenerateToPlayer; i++)
		{
			playerInventoryUI.AddToInventory(GetRandomItem());
		}
		playerInventoryUI.UpdateInventory();

		for (int i = 0; i < itemsToGenerateToTrader; i++)
		{
			traderInventoryUI.AddToInventory(GetRandomItem());
		}
		traderInventoryUI.UpdateInventory();

		GameBase.Instance.AddGold(playerStartMoney);
	}

	private ItemStack GetRandomItem()
	{
		var itemBase = itemsLibrary.itemSOList[Random.Range(0, itemsLibrary.itemSOList.Count)];
		ItemStack result = new ItemStack() { itemBase = itemBase, count = 1 };
		if (itemBase.bStackable)
		{
			result.count = Random.Range(1, itemsToGenerateWhenStackable);
		}

		return result;
	}

	#endregion
}
