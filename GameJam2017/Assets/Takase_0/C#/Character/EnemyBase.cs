using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : CharacterBase {

    protected GameObject go_target;

    protected void Start()
    {
        base.Start();
        go_target = GameObject.FindGameObjectWithTag("Player");
    }

}
