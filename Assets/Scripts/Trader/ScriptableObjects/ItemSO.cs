using UnityEngine;

[CreateAssetMenu(menuName = "Trader/Item", fileName = "NewItem")]
public class ItemSO : ScriptableObject
{
	public Sprite icon;

	public string title;

	public int price;

	public bool bStackable;
}
