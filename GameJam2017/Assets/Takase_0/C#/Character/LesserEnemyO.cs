using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserEnemyO : LesserEnemyBase {

	
	// Update is called once per frame
	void Update () {
		
	}

    new void Shot()
    {
        Vector3 bulletSpawnRotation = new Vector3(0, 0, 0);

        for (int i = 0; i < 8; i++)
        {
            tr_bulletSpawn.Rotate(0, 0, i*45 );
            GameObject bullet = Instantiate(go_bullet, tr_self.position, tr_bulletSpawn.rotation,null) as GameObject;
            go_bullet.GetComponent<BulletBase>().BulletAttackPoint = i_attackPoint;
            go_bullet.GetComponent<BulletBase>().BulletRapidPoint = i_rapidPoint;
        }
    }
}
