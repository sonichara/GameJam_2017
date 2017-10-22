﻿//作成者　たかせ
//プレイヤーの全ての動きを管理するクラス
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase {

    [SerializeField]
    float f_delayTime;
    [SerializeField]
    float magnification = 0.9f;


    bool b_canDamage = true;

    //移動制限用の変数
    float f_limitRight, f_limitLeft, f_limitTop, f_limitBottom;


    private void Start()
    {
        tr_self = transform;
        Camera mainCamera = Camera.main;
        Vector3 limitPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        f_limitRight = limitPos.x * magnification; 
        f_limitLeft = -limitPos.x * magnification;
        f_limitTop = limitPos.y * magnification;
        f_limitBottom = -limitPos.y * magnification;

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
}
