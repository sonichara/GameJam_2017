﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_A : ItemsControl {
	[SerializeField]
	int Attack;
	[SerializeField]
	int Speed;

	public override void ItemEffect (Player player)
	{
		player.AttackPoint = Attack;
		player.SpeedPoint = Speed;
	}
}
