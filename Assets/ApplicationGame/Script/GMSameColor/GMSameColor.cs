using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GMSameColor : BaseGameController
{

    public int Ran;



	private List<Color> list = new List<Color>();

	private void Awake(){
		list.Add (DEFINE.COLORS[DEFINE.COLOR_ID.ORANGE].color);
		list.Add (DEFINE.COLORS[DEFINE.COLOR_ID.RED].color);
		list.Add (DEFINE.COLORS[DEFINE.COLOR_ID.GREEN].color);
		SetUp ();
    }

	public UISprite[] Buttons;
	[SerializeField]
	private int[]index = new int[5];
	void SetUp(){
		for (int i = 0; i < Buttons.Length; i++) { 
			this.index [i] = Random.Range(0,3);
			this.Buttons[i].color = list [index[i]];

		}
        if (index[0]==index[1])
        {
            index[0]++;
            Color col = list[index[0] % list.Count];
            this.Buttons[0].color = col;
        }
    }

   


    protected override void Update () {
		base.Update();
        if (!this.isPlaying)
            return;

		if (this.Buttons[0].color.Equals(this.Buttons[1].color) &&
			this.Buttons[1].color.Equals(this.Buttons[2].color) &&
			this.Buttons[2].color.Equals(this.Buttons[3].color) &&
			this.Buttons[3].color.Equals(this.Buttons[4].color)) {
            base.GameClear();
        }
       
    }

    public void Botton1ChangeColor()
    {
		++index[0];
		Color col = list[index[0] % list.Count];
		this.Buttons[0].color = col;
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );

    }

    public void Botton2ChangeColor()
    {
		++index[1];
		Color col = list[index[1] % list.Count];
		this.Buttons[1].color = col;
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );

    }
    public void Botton3ChangeColor()
	{
		++index[2];
		Color col = list [index[2] % list.Count];
		this.Buttons[2].color = col;
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );

	}
    public void Botton4ChangeColor()
    {
        ++index[3];
        Color col = list[index[3] % list.Count];
		this.Buttons[3].color = col;
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );

    }
    public void Botton5ChangeColor()
    {
        ++index[4];
        Color col = list[index[4] % list.Count];
		this.Buttons[4].color = col;
		Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );

    }
}
