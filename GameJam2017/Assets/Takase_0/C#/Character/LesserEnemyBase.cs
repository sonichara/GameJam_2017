using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LesserEnemyBase : EnemyBase {

    [SerializeField]
    protected float f_firstShotTime;
    [SerializeField]
    protected float f_loopShotTime;

    // Use this for initialization
    void Start()
    {
        base.Start();
        InvokeRepeating("Shot", f_firstShotTime, f_loopShotTime);
    }
}
