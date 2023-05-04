using System;
using UnityEngine;

public class GameBase : MonoBehaviour
{
	#region HelperClasses

	public class GoldAmountEventArgs : EventArgs
	{
		public int goldAmount;
	}

	#endregion

	#region Variables

	public static GameBase Instance { get; private set; }

	public event EventHandler<GoldAmountEventArgs> onGoldAmountChanged;

	public ItemsLibrarySO itemsLibrary;
	public float SellCoef = 0.75f;

	private int goldAmount;

	#endregion

	#region Functions

	private void Awake()
	{
		Instance = this;
	}

	public void AddGold(int amount)
	{
		goldAmount += amount;
		onGoldAmountChanged?.Invoke(this, new GoldAmountEventArgs { goldAmount = goldAmount });
	}

	public void RemoveGold(int amount)
	{
		goldAmount -= amount;
		onGoldAmountChanged?.Invoke(this, new GoldAmountEventArgs { goldAmount = goldAmount });
	}

	public int GetGold()
	{
		return goldAmount;
	}

	public static int GetBuyPrice(ItemStack item)
	{
		return item.count * item.itemBase.price;
	}

	public static int GetSellPrice(ItemStack item)
	{
		return (int)(item.count * item.itemBase.price * Instance.SellCoef);
	}

	public static int GetBuyPrice(ItemSO item, int count)
	{
		return count * item.price;
	}

	public static int GetSellPrice(ItemSO item, int count)
	{
		return (int)(count * item.price * Instance.SellCoef);
	}

	#endregion
}
