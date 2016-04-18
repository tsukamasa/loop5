using UnityEngine;
using System.Collections;

public class GMAvoidRain : BaseGameController{
    public GameObject rain;
    public static float RainSpeed;
    private Vector3 moveposition;
    public FieldBottonComponent botton;
    public float GameLevel;
    public Rabbit rabbit;
    public static bool gamehantei = false;
    public int gameLv;
    public int gamespeed;
    public float RainSize;
    // Use this for initialization
    protected override void GameStart () {
		gamehantei = false;
        base.GameStart();
        StartCoroutine("MakeRain");
        botton.Init(this.UpdatePosition);

    }

    // Update is called once per frame
    protected override void Update () {
        //        base.Update();
        gameLv = Mgrs.gameMgr.gameLv;           //ゲームレベル
        gamespeed = Mgrs.gameMgr.gameSpeed; //スピードレベル
        if (!this.isPlaying) return;
        if (gameTimer >= gameSetTime)
        {
            base.GameClear();
        }


        if (gamehantei == true)
        {
            gamehantei = false;
            base.GameOver();
           
        }
        switch (gamespeed)
        {
            case 1:
                GameLevel = 2.0f;
                RainSpeed = 300.0f;
                break;

            case 2:
                GameLevel = 1.6f;
                RainSpeed = 330.0f;
                break;

            case 3:
                GameLevel = 1.2f;
                RainSpeed = 360.0f;
                break;

            case 4:
                GameLevel = 0.8f;
                RainSpeed = 390.0f;
                break;

            case 5:
                GameLevel = 0.4f;
                RainSpeed = 420.0f;
                break;


        }
    }

    private void UpdatePosition(Vector3 pos)
    {
        rabbit.UpdatePosition(pos);
    }

    IEnumerator MakeRain()
    {
        while (true)
        {
            Rain rai = Util.InstantiateComponent<Rain>(rain, this.transform);
            float x = Random.Range(-460.0f, 460.0f);
            RainSize = Random.Range(50.0f,160.0f);
            rai.transform.localPosition = new Vector3(x,500.0f);
            rai.transform.localScale = new Vector3(RainSize,RainSize);
            yield return new WaitForSeconds(GameLevel);
        }
    }



}
