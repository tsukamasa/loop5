using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rabbit : MonoBehaviour {
    private Vector2 vec;
    private Vector2 screenPoint;
    private Vector2 offset;
    private Vector3 moveposition;
    //private Camera cam;
    //FieldBottonComponent botton;
    // Use this for initialization
    void Start()
    {
        //botton.Init(this.UpdatePosition);
       // this.cam = GetComponentInParent<Camera>();
    }	
	
	// Update is called once per frame
	void Update () {
        
       
         this.transform.position = Vector2.MoveTowards(transform.position,moveposition,3.0f*Time.deltaTime);
        
    }

    public void UpdatePosition(Vector3 pos)
    {
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
        moveposition = pos;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "shizuku")
        {
            GMAvoidRain.gamehantei = true;
            Destroy(this.gameObject);
        }
    }
   
}
