using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_A : ItemsControl {
	[SerializeField]
	int Attack;
	[SerializeField]
	float Speed;

	public override void ItemEffect (Player player)
	{
		player.AttackPoint = Attack;
		player.SpeedPoint = Speed;
        player.go_redPart.SetActive(true);
        player.go_changePart = player.go_redPart;
	}
}
