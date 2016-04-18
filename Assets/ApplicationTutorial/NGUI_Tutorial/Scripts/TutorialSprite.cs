using UnityEngine;
using System.Collections;

public class TutorialSprite : BasePanelController
{
	public UISprite sprite;
	
	public UISprite[] plateSpriteArray = new UISprite[8];
	private string[] plateSpriteNameArray = new string[8];
	
	private const string UNACTIVE_BUTTON_NAME = "button1";
	private const string ACTIVE_BUTTON_NAME = "button2";
	
	// Use this for initialization
	void Start () {
		this.InitSpriteNameArray();
		this.sprite.enabled = true;
		this.OnPushButton1();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnPushButton1 ()
	{
		this.SetSpriteName(1);
	}
	private void OnPushButton2 ()
	{
		this.SetSpriteName(2);
	}
	private void OnPushButton3 ()
	{
		this.SetSpriteName(3);
	}
	private void OnPushButton4 ()
	{
		this.SetSpriteName(4);
	}
	private void OnPushButton5 ()
	{
		this.SetSpriteName(5);
	}
	private void OnPushButton6 ()
	{
		this.SetSpriteName(6);
	}
	private void OnPushButton7 ()
	{
		this.SetSpriteName(7);
	}
	private void OnPushButton8 ()
	{
		this.SetSpriteName(8);
	}
	
	private void InitSpriteNameArray ()
	{
		for (int index = 0; index < this.plateSpriteNameArray.Length; index++)
		{
			this.plateSpriteNameArray[index] = "plate_" + (index+1).ToString();
		}
	}
	
	private void ResetSpriteArray ()
	{
		foreach (UISprite sprite in this.plateSpriteArray)
		{
			sprite.spriteName = UNACTIVE_BUTTON_NAME;
		}
	}
	
	private void SetSpriteName (int in_plateNum)
	{
		if ((in_plateNum - 1) > this.plateSpriteNameArray.Length)
		{
			return;
		}
		
		if ((in_plateNum - 1) > this.plateSpriteArray.Length)
		{
			return;
		}
		
		this.ResetSpriteArray();
		this.plateSpriteArray[in_plateNum - 1].spriteName = ACTIVE_BUTTON_NAME;
		this.sprite.spriteName = this.plateSpriteNameArray[in_plateNum - 1];
	}

	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/NGUITutorialTitlePanel");
	}
}
