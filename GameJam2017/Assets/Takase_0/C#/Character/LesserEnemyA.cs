﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserEnemyA : LesserEnemyBase {


	
	
	// Update is called once per frame
	void Update () {

        tr_self.Translate(i_speedPoint * Time.deltaTime,0,0);
	}
}