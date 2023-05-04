using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountSelectorUI : MonoBehaviour
{
	#region Variables

	[Header("Components")]
	[SerializeField] private Slider slider;
	[SerializeField] private Image itemIcon;
	[SerializeField] private TMP_Text titleText;
	[SerializeField] private TMP_Text priceText;
	[SerializeField] private TMP_Text currentCountText;
	[SerializeField] private TMP_Text maxCountText;
	[SerializeField] private Button confirmBUtton;
	[SerializeField] private Button cancelButton;

	private ItemStack currentItem;

	private Action<int, int> onConfirmAction;

	private int selectedCount;

	private int goldAmount;

	private bool isBuying;

	#endregion

	#region Functions

	private void Awake()
	{
		confirmBUtton.onClick.AddListener(() =>
		{
			onConfirmAction(selectedCount, goldAmount);
			Hide();
		});

		cancelButton.onClick.AddListener(() =>
		{
			Hide();
		});

		slider.onValueChanged.AddListener(x =>
		{
			selectedCount = (int)x;
			currentCountText.text = selectedCount.ToString("0");
			goldAmount = GetPrice(selectedCount);
			priceText.text = goldAmount.ToString();
		});
	}

	public void Show(ItemStack itemStack, bool buyingState, Action<int, int> onConfirm)
	{
		isBuying = buyingState;
		titleText.text = buyingState ? "Покупка" : "Продажа";
		onConfirmAction = onConfirm;

		currentItem = itemStack;

		slider.value = 1;
		selectedCount = 1;
		goldAmount = GetPrice(selectedCount);

		slider.maxValue = buyingState ?
			Mathf.Min(itemStack.count, GameBase.Instance.GetGold() / itemStack.itemBase.price) : itemStack.count;

		if (slider.maxValue == 0)
		{
			Hide();
			return;
		}

		itemIcon.sprite = itemStack.itemBase.icon;

		currentCountText.text = slider.value.ToString("0");
		maxCountText.text = slider.maxValue.ToString("0");
		priceText.text = goldAmount.ToString();

		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	private int GetPrice(int count)
	{
		return isBuying ? 
			GameBase.GetBuyPrice(currentItem.itemBase, count) : GameBase.GetSellPrice(currentItem.itemBase, count);
	}

	#endregion
}
