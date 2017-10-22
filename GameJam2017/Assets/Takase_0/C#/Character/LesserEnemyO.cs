using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LesserEnemyO : LesserEnemyBase {

    int upDown = 1;

	// Update is called once per frame
	void Update () {
        tr_self.Translate(f_speedPoint * Time.deltaTime, upDown * f_speedPoint * Time.deltaTime, 0);
	}

    new void Shot()
    {

        for (int i = 0; i < 8; i++)
        {
            tr_bulletSpawn.Rotate(0, 0, i*45 );
            GameObject bullet = Instantiate(go_bullet, tr_self.position, tr_bulletSpawn.rotation,null) as GameObject;
            go_bullet.GetComponent<BulletBase>().BulletAttackPoint = i_attackPoint;
            go_bullet.GetComponent<BulletBase>().BulletRapidPoint = i_rapidPoint;
        }

        upDown *= -1;
    }

	public void OnCollisionEnter(Collision collision) { 
		if(collision.gameObject.tag == "PlayerBullet") {
			Damage();
		}
	}
}
