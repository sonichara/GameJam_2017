using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase {

    [SerializeField]
    Transform tr_bulletSpawn2;

    [SerializeField]
    Transform tr_appearancePoint;
    [SerializeField]
    Transform tr_escapePoint;

    [SerializeField]
    int i_nonBattleSpeedPoint;

    [SerializeField]
    float f_limitTop, f_limitBottom;


    int i_changeUPDpwn = 1;

    bool b_canShot = false;

    public enum BossCommand
    {
        appearance, battle,escape,wait
    }

    public BossCommand bosscommand = BossCommand.appearance;

    private void Start()
    {
        base.Start();
        InvokeRepeating("Shot", 0.0f, 0.5f);
    }


    // Update is called once per frame
    void Update () {

        switch (bosscommand)
        {
            case BossCommand.appearance:

                Vector3 moveValue = tr_appearancePoint.position - tr_self.position;
                tr_self.Translate(moveValue.normalized * i_nonBattleSpeedPoint * Time.deltaTime);

                if(tr_appearancePoint.position.y - 0.5f < tr_self.position.y && tr_self.position.y < tr_appearancePoint.position.y + 0.5f)
                {
                    bosscommand = BossCommand.battle;
                }

                break;


            case BossCommand.battle:
                b_canShot = true;

                if(f_limitBottom> tr_self.position.y || tr_self.position.y > f_limitTop)
                {
                    i_changeUPDpwn *= -1;
                }

                tr_self.Translate(0, i_changeUPDpwn * i_speedPoint * Time.deltaTime, 0);

                break;


            case BossCommand.escape:

                b_canShot = false;

                Vector3 moveValue2 = tr_escapePoint.position - tr_self.position;
                tr_self.Translate(moveValue2.normalized * i_nonBattleSpeedPoint * Time.deltaTime);

                if(tr_escapePoint.position.y - 0.5f < tr_self.position.y && tr_self.position.y < tr_escapePoint.position.y + 0.5f)
                {
                    bosscommand = BossCommand.wait;
                }

                break;

            case BossCommand.wait:

                break;
        }

	}

    void Shot()
    {
        if (b_canShot)
        {
            GameObject go = Instantiate(go_bullet, tr_bulletSpawn.position, Quaternion.identity, null) as GameObject;
            go.GetComponent<BulletBase>().BulletAttackPoint = i_attackPoint;
            go.GetComponent<BulletBase>().BulletRapidPoint = i_rapidPoint;

            GameObject go2 = Instantiate(go_bullet, tr_bulletSpawn2.position, Quaternion.identity, null) as GameObject;
            go2.GetComponent<BulletBase>().BulletAttackPoint = i_attackPoint;
            go2.GetComponent<BulletBase>().BulletRapidPoint = i_rapidPoint;

        }
    }
}
