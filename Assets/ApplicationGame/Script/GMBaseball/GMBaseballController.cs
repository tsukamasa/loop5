using UnityEngine;
using System.Collections;

public class GMBaseballController : BaseGameController {
    public GameObject ball;
    public GameObject bat;

	public bool flag = false;
	public bool batcollisionflag = false;

	protected override void GameStart()
	{
		base.GameStart();
	}

    protected override void Update()
    {
        base.Update();
		if (!isPlaying) return;
        if (flag) {
			GameOver();
		}
		else if(gameTimer >= gameSetTime)
        {
			GameOver();
        }
    }	
}