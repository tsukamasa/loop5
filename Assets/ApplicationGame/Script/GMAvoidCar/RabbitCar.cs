using UnityEngine;
using System.Collections;

public class RabbitCar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() { 
        { 
            this.transform.localPosition += Vector3.down * Time.deltaTime * GMAvoidCar.rabbitspeed;
		}
	}
    
    public void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Car")
        {
            GMAvoidCar.gamehantei = true;

        }
    }
    
}
