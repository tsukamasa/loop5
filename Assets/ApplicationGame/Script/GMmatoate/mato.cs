using UnityEngine;
using System.Collections;

public class mato : MonoBehaviour
{
    public GMmatoateControllor matoate;
    private int level;
    private int speed;
    private float width;
    private int rnd;

    public Animation animation;
    

    // Use this for initialization
    void Start()
    {
        speed = Mgrs.gameMgr.gameSpeed;
        rnd = Random.Range(0, speed);

        foreach(AnimationState state in animation){
			if (speed == 1) {
				state.speed = 0.2f * speed;
			}
			else if (speed == 5) {
				state.speed = 0.5f;
			}
			else {
				state.speed = 0.2f + 0.07f * (speed-1);
			}
            
        }

        switch(rnd){
            case 0:
                animation.Play("storaight");
                break;
            case 1:
                animation.Play("curve");
                break;
            case 2:
                animation.Play("circle");
                break;
            case 3:
                animation.Play("wave");
                break;
            case 4:
                animation.Play("storaight");
                break;
        }
    }

    // Update is called once per frame
    void Update()
	{
    }
	
    void OnTriggerEnter2D(Collider2D cllision)
    {
        matoate.isHit = true;
        GameObject.Destroy(this.gameObject);
    }

}