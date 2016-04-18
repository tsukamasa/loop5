using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour {

  
   
    // Use this for initialization
    void Start()
    {
        StartCoroutine("DestroyRain");
    }
    // Update is called once per frame
    void Update()
    {
            this.transform.localPosition += Vector3.down * Time.deltaTime * GMAvoidRain.RainSpeed;
     
    }
    IEnumerator DestroyRain()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
