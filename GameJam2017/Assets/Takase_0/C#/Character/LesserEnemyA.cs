using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserEnemyA : LesserEnemyBase {


	
	
	// Update is called once per frame
	void Update () {

        tr_self.Translate(f_speedPoint * Time.deltaTime,0,0);
	}

	public void OnCollisionEnter(Collision collision) { 
		if(collision.gameObject.tag == "PlayerBullet") {
			Damage();
		}
	}
}
