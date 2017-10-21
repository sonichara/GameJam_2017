//作成者　たかせ
//プレイヤー、エネミーの親クラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour {

    [SerializeField]
    protected int i_attackPoint;
    [SerializeField]
	protected int i_hitPoint;
    [SerializeField]
	protected int i_speedPoint;
    [SerializeField]
	protected int i_rapidPoint;

    [SerializeField]
    GameObject go_bullet;

    [SerializeField]
    Transform tr_bulletSpawn;

    protected Transform tr_self;

    private void Start()
    {
        tr_self = transform;
    }

    //プロパティ
    #region
    public int AttackPoint
    {
        set
        {
            this.i_attackPoint = value;
        }

        get
        {
            return this.i_attackPoint;
        }
    }

    public int HitPoint
    {
        set
        {
            this.i_hitPoint = value;
        }

        get
        {
            return this.i_hitPoint;
        }
    }

    public int SpeedPoint
    {
        set
        {
            this.i_speedPoint = value;
        }

        get
        {
            return this.i_speedPoint;
        }
    }

    public int RapidPoint
    {
        set
        {
            this.i_rapidPoint = value;
        }

        get
        {
            return this.i_rapidPoint;
        }
    }

    #endregion

    //自キャラの攻撃用関数
    protected void Shot()
    {
        Instantiate(go_bullet, tr_bulletSpawn.position,Quaternion.identity,null);
        go_bullet.GetComponent<BulletBase>().BulletAttackPoint = i_attackPoint;
        go_bullet.GetComponent<BulletBase>().BulletRapidPoint = i_rapidPoint;
        Debug.Log("たまうった");

    }

    //自キャラがダメージを受ける時用関数
    public void Damage()
    {
        i_hitPoint--;

    }

}
