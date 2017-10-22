using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_B : ItemsControl {
	[SerializeField]
	int Rapid;
	[SerializeField]
	int Size;
	
	public override void ItemEffect (Player player)
	{
		player.RapidPoint = Rapid;
	}
}
