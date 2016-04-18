using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
//	public GMBaseballControllor baseball;
    Rigidbody2D rb;
	float levelspeed ;
	float circlespeed = 1.0f;
	float radius = 2.0f;
	float yPosition = 0.5f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
		levelspeed = Mgrs.gameMgr.gameSpeed;

        StartCoroutine("wait");
	}
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        rb.velocity = Vector2.one;
        rb.AddForce(transform.up*100,ForceMode2D.Force);
    }

	//ランダムボール発射処理 発射間隔はランダム	発射スピードは, レベル*random.range(50,250)
    private IEnumerator wait()
    {
		int second = 0;
        for (;;)
        {
            //Debug.Log(Time.deltaTime);
			if (second == 0) {
				yield return new WaitForSeconds(0.5f);
			}

            if (Random.Range(0, 10)%2==0 || second >= 2)
            {
				Debug.Log("ballStart");
				if (levelspeed == 1) {
					rb.AddForce(-transform.up * levelspeed * 10);
				}else{
					rb.AddForce(-transform.up * levelspeed/2 * 11);
				}
                yield break;
            }
			second++;
			yield return new WaitForSeconds(1.0f);
        }   
    }
}
