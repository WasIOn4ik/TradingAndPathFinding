using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationUI : MonoBehaviour
{
	#region Variables

	[Header("Components")]
	[SerializeField] private Button ConfirmButton;
	[SerializeField] private Button CancelButton;
	[SerializeField] private TMP_Text titleText;
	[SerializeField] private Image itemIcon;
	[SerializeField] private TMP_Text goldAmountText;

	private Action onConfirmAction;

	#endregion

	#region Functions

	private void Awake()
	{
		ConfirmButton.onClick.AddListener(() =>
		{
			onConfirmAction();
			Hide();
		});
		CancelButton.onClick.AddListener(() =>
		{
			Hide();
		});
	}

	public void Show(ItemStack item, bool isBuying, Action onComfirm)
	{
		onConfirmAction = onComfirm;

		gameObject.SetActive(true);

		itemIcon.sprite = item.itemBase.icon;

		titleText.text = isBuying ? "Покупка" : "Продажа";

		goldAmountText.text = (isBuying ? GameBase.GetBuyPrice(item) : GameBase.GetSellPrice(item)).ToString();
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	#endregion
}
