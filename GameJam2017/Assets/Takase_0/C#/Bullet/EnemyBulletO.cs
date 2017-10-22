using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletO : BulletBase {
	
	// Update is called once per frame
	void Update () {
        transform.Translate(BulletRapidPoint * Time.deltaTime,0,0);
	}
}
