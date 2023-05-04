using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleItemUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	#region Variables
	
	[Header("Components")]
	[SerializeField] private Image backgroundImage;
	[SerializeField] private Image itemImage;
	[SerializeField] private TMP_Text itemTitle;
	[SerializeField] private Image draggableItemImage;
	[SerializeField] private IItemsStorage storage;

	[Header("Properties")]
	[SerializeField] private Color highlightedColor;

	private Color baseColor;
	private ItemStack currentItem;

	#endregion

	#region DragNDrop

	public void OnBeginDrag(PointerEventData eventData)
	{
		Highlight();
		if (draggableItemImage != null)
		{
			draggableItemImage.gameObject.SetActive(true);
			draggableItemImage.sprite = currentItem.itemBase.icon;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (draggableItemImage != null)
			draggableItemImage.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Unhighlight();

		if (draggableItemImage != null)
			draggableItemImage.gameObject.SetActive(false);
	}

	#endregion

	#region Functions

	public void SetDraggableImage(Image newImage)
	{
		draggableItemImage = newImage;
	}

	public void SetItemStack(ItemStack item)
	{
		currentItem = item;
		itemImage.sprite = item.itemBase.icon;
		draggableItemImage.sprite = item.itemBase.icon;

		StringBuilder sb = new StringBuilder();
		sb.Append(item.itemBase.title);
		if (item.itemBase.bStackable)
			sb.Append(" x ").Append(item.count);

		itemTitle.text = sb.ToString();
	}

	public void SetStorage(IItemsStorage newStorage)
	{
		storage = newStorage;
	}

	public ItemStack GetItem()
	{
		return currentItem;
	}

	public IItemsStorage GetStorage()
	{
		return storage;
	}

	private void Highlight()
	{
		baseColor = backgroundImage.color;
		backgroundImage.color = highlightedColor;
	}

	private void Unhighlight()
	{
		backgroundImage.color = baseColor;
	}

	#endregion
}
