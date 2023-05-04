using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Trader/ItemsLibrary", fileName = "ItemsLibrary")]
public class ItemsLibrarySO : ScriptableObject
{
	public List<ItemSO> itemSOList = new List<ItemSO>();
}
