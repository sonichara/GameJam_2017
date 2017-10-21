//作成者　たかせ
//弾に関する親クラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour {

    [SerializeField]
    int i_bulletAttackPoint;
    [SerializeField]
    int i_bulletRapidPoint;

    [SerializeField]
    float f_destroyDelayTime;


    public int BulletAttackPoint
    {
        set
        {
            i_bulletAttackPoint = value;
        }

        get
        {
            return i_bulletAttackPoint;
        }

    }

    public int BulletRapidPoint
    {
        set
        {
            i_bulletRapidPoint = value;
        }

        get
        {
            return i_bulletRapidPoint;
        }

    }


    // Use this for initialization
    void Start () {
        Destroy(this.gameObject,f_destroyDelayTime);
	}

    void Update()
    {
        Debug.Log(i_bulletRapidPoint);
        transform.Translate(i_bulletRapidPoint * Time.deltaTime, 0, 0);
    }


}
