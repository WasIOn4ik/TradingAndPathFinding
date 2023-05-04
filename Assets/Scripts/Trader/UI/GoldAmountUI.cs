using System.Collections;
using TMPro;
using UnityEngine;

public class GoldAmountUI : MonoBehaviour
{
	#region Variables

	[Header("Components")]
	[SerializeField] private TMP_Text goldText;

	private int goldAmount;
	private float notAnimatedGoldAmount;

	private Coroutine animationCoroutine;

	#endregion

	#region Fuctions

	private void Start()
	{
		notAnimatedGoldAmount = 0;
		GameBase.Instance.onGoldAmountChanged += GameInstance_onGoldAmountChanged;
	}

	private void OnDestroy()
	{
		GameBase.Instance.onGoldAmountChanged -= GameInstance_onGoldAmountChanged;
	}

	private void GameInstance_onGoldAmountChanged(object sender, GameBase.GoldAmountEventArgs e)
	{
		goldAmount = e.goldAmount;
		if (animationCoroutine == null)
			animationCoroutine = StartCoroutine(AnimateGold());
	}

	private void UpdateUI()
	{
		goldText.text = notAnimatedGoldAmount.ToString("0");
	}

	public IEnumerator AnimateGold()
	{
		while (true)
		{
			int framesCount = 60;
			float increment = Mathf.Max(0.3f, (Mathf.Abs(goldAmount - notAnimatedGoldAmount)) / framesCount);
			notAnimatedGoldAmount = Mathf.MoveTowards(notAnimatedGoldAmount, goldAmount, increment);
			UpdateUI();
			if (notAnimatedGoldAmount == goldAmount)
			{
				animationCoroutine = null;
				yield break;
			}
			yield return null;
		}
	}

	#endregion
}
