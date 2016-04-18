using UnityEngine;
using System.Collections;

public class FieldBottonComponent : MonoBehaviour {
    private System.Action<Vector3> Callback;
    private Vector3 touchposition;
    private Camera cam;
    // Use this for initialization
    void Start () {
        this.cam = GetComponentInParent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            touchposition = this.cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void Init(System.Action<Vector3> callback)
    {
        Callback = callback;
    }

    public void  OnPush()
    {
        this.Callback(touchposition);
    }
}
