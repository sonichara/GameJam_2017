using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    float f_movespeed;

    [SerializeField]
    float f_killX;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(f_movespeed * Time.deltaTime, 0, 0);

        if(transform.position.x < f_killX)
        {
            Destroy(this.gameObject);
        }
	}
}
