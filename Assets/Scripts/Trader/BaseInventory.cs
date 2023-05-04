using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseInventory : MonoBehaviour, IItemsStorage, IDropHandler
{
	#region Variables

	[SerializeField] private SingleItemUI itemUIPrefab;
	[SerializeField] private Transform itemsContent;
	[SerializeField] private Image draggableItemImage;

	private List<ItemStack> itemsList = new List<ItemStack>();

	private List<SingleItemUI> itemsUIList = new List<SingleItemUI>();

	#endregion

	#region Functions

	public void UpdateInventory()
	{
		if (itemsList.Count > itemsUIList.Count)
		{
			int countToCreate = itemsList.Count - itemsUIList.Count;

			for (int i = 0; i < countToCreate; i++)
			{
				var itemUI = Instantiate(itemUIPrefab, itemsContent);
				itemUI.SetStorage(this);
				itemUI.SetDraggableImage(draggableItemImage);
				itemsUIList.Add(itemUI);
			}
		}

		for (int i = 0; i < itemsUIList.Count; i++)
		{
			var itemUI = itemsUIList[i];

			if (itemsList.Count > i)
			{
				itemUI.gameObject.SetActive(true);
				itemUI.SetItemStack(itemsList[i]);
				continue;
			}

			itemUI.gameObject.SetActive(false);
		}
	}

	#endregion

	#region DragNDrop

	public void OnDrop(PointerEventData eventData)
	{
		SingleItemUI itemUI = eventData.pointerDrag.GetComponent<SingleItemUI>();

		if (itemUI != null)
		{
			itemUI.GetStorage().OnDragFromInventoryComplete(itemUI.GetItem(), this);
			OnDragToInventoryComplete(itemUI.GetItem(), itemUI.GetStorage());
		}
	}
	#endregion

	#region IItemsStorage

	public void AddToInventory(ItemStack itemToAdd)
	{
		if (itemToAdd.itemBase.bStackable)
		{
			int index = itemsList.FindIndex(x =>
			{
				return x.itemBase == itemToAdd.itemBase;
			});

			//Если предмет уже есть в наличии
			if (index != -1)
			{
				ItemStack itemTemp = itemsList[index];
				itemTemp.count += itemToAdd.count;
				itemsList[index] = itemTemp;
				return;
			}
		}

		itemsList.Add(itemToAdd);
	}

	public void RemoveFromInventory(ItemStack itemToRemove)
	{
		int index = itemsList.FindIndex(x =>
		{
			return x.itemBase == itemToRemove.itemBase;
		});

		if (index == -1)
		{
			Debug.LogError($"Невозможно удалить из инвентаря {name} несуществующий предмет {itemToRemove.itemBase.title}");
			return;
		}

		ItemStack itemTemp = itemsList[index];
		itemTemp.count -= itemToRemove.count;
		if (itemTemp.count < 0)
		{
			Debug.LogError($"Попытка удалить из инвентаря {name} предмет в количестве, превыщающем наличие {itemToRemove.itemBase.title}: {itemsList[index].count}/{itemToRemove.count}");
			return;
		}
		else if (itemTemp.count == 0)
		{
			itemsList.RemoveAt(index);
		}
		else
		{
			itemsList[index] = itemTemp;
		}
	}

	public virtual void OnDragFromInventoryComplete(ItemStack item, IItemsStorage target) { }

	public virtual void OnDragToInventoryComplete(ItemStack item, IItemsStorage target) { }

	#endregion
}
