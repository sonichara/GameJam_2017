//作成者　たかせ
//プレイヤーの全ての動きを管理するクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : CharacterBase {

    [SerializeField]
    float f_delayTime;
    [SerializeField]
    float magnification = 0.9f;

    [SerializeField]
    float f_powerUpFinishTime;

    bool b_canDamage = true;

    bool b_canReset = true;

    //移動制限用の変数
    float f_limitRight, f_limitLeft, f_limitTop, f_limitBottom;

    //ステータス変更用の変数
    int i_attackPointOrigin;
    float f_speedPointOrigin;
    int i_rapidPointOrigin;

    public GameObject go_redPart;
    public GameObject go_greenPart;

    public GameObject go_changePart;

    Animator anim;

    private void Start()
    {
        tr_self = transform;
        Camera mainCamera = Camera.main;
        Vector3 limitPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        f_limitRight = limitPos.x * magnification; 
        f_limitLeft = -limitPos.x * magnification;
        f_limitTop = limitPos.y * magnification;
        f_limitBottom = -limitPos.y * magnification;
        i_attackPointOrigin = i_attackPoint;
        f_speedPointOrigin = f_speedPoint;
        i_rapidPointOrigin = i_rapidPoint;

        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update () {

        
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        
        if (tr_self.position.x < f_limitLeft && inputH < 0 )
        {
            inputH = 0;
        }
        else if(tr_self.position.x > f_limitRight && inputH > 0 )
        {
            inputH = 0;
        }

        if (tr_self.position.y < f_limitBottom && inputV < 0 )
        {
            inputV = 0;
        }
        else if (tr_self.position.y > f_limitTop && inputV > 0)
        {
            inputV = 0;
        }

        if(inputV != 0)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
        

        tr_self.Translate(inputH * SpeedPoint * Time.deltaTime,inputV * SpeedPoint * Time.deltaTime,0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
		
	}

    /// <summary>
    /// オーバーライドしたDamage関数
    /// </summary>
    public void Damage()
    {
        if (b_canDamage)
        {
            b_canDamage = false;
            HitPoint--;
            Invoke("ReactiveCanDamage",f_delayTime);
        }
    }

    /// <summary>
    /// 再びダメージを受けられるようにする関数
    /// </summary>
    private void ReactiveCanDamage()
    {
        b_canDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            PowerUpFinish();
            other.gameObject.GetComponent<ItemsControl>().ItemEffect(GetComponent<Player>());
            Invoke("PowerUpFinish", f_powerUpFinishTime);
            Destroy(other.gameObject);
        }
    }

    private void PowerUpFinish()
    {

        i_attackPoint = i_attackPointOrigin;
        f_speedPoint = f_speedPointOrigin;
        i_rapidPoint = i_rapidPointOrigin;

        if (go_changePart != null)
        {
            go_changePart.SetActive(false);
            go_changePart = null;
        }
    }
}
