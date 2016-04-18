using UnityEngine;
using System.Collections;

public class TutorialCreateGameObject : BasePanelController
{

	public GameObject prefab;
	
	public float paddingX = 100;
	public float paddingY = 100;
	
	public float createFrameInterval = 270F;
	public int createMaxCount = 10;
	
	private int createCount = 0;
	
	private int timer = 0;
	
	[HideInInspector]
	public Transform parent;
	
	
	public bool isPositionRandom = false;
	public float posRangeX = 50;
	public float posRangeY = 50;
	
	void Awake()
	{
		this.parent = transform;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if(this.createCount >= this.createMaxCount)
		{
			return;
		}
		
		if(++this.timer > this.createFrameInterval)
		{
			this.timer = 0;
			CreateObject();
			++this.createCount;
		}
	}
	
	private void CreateObject()
	{
//		GameObject go = Resources.Load("Prefab/Tutorial/TutorialCreateObjectOne") as GameObject;
		GameObject child = Instantiate(this.prefab) as GameObject;
		child.transform.localPosition = this.GetCreatePosition();
		
		Vector3 pos = child.transform.localPosition;
		Vector3 size = child.transform.localScale;
		child.transform.SetParent(this.parent);
		child.transform.localPosition = pos;
		child.transform.localScale = size;
	}
	
	private Vector3 GetCreatePosition()
	{
		Vector3 position = Vector3.one;
		if(this.isPositionRandom)
		{
			position = new Vector3 (Random.Range (-this.posRangeX, this.posRangeX), Random.Range (-this.posRangeY, this.posRangeY));
		}
		else
		{
			position = new Vector3(Mathf.Cos((float)(Time.frameCount))*this.paddingX, Mathf.Sin((float)(Time.frameCount))*this.paddingY);
		}
				
		return position;
	}
	
	public bool IsCreateAllEnd()
	{
		return (this.createCount >= this.createMaxCount);
	}

	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/UnityTutorialTitlePanel");
	}
}
