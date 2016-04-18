using UnityEngine;
using System.Collections;

public class GMAvoidCar : BaseGameController {

    // Use this for initialization
    public static bool gamehantei = false;
   
    public int[] Ransuu = { -330, -110, 110, 330 };
    public static float rabbitspeed;
    public int gameLv;
    public int gamespeed;
    public float RabLv;
    public GameObject Car;
    public GameObject Rabbit;
    public float nowposition { get { return this.Car.transform.localPosition.x; } }
    public int rabbitpos { get { return 110* Ransuu[Random.Range(0,4)]; } }
    protected override void GameStart() {
		gamehantei = false;
        StartCoroutine("MakeRabbit");
        base.GameStart();
    }


    protected override void Update()
    {
        gameLv = Mgrs.gameMgr.gameLv;           //ゲームレベル
        gamespeed = Mgrs.gameMgr.gameSpeed; //スピードレベル
        //        base.Update();
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
                RabLv = 2.0f;
                rabbitspeed = 400.0f;
                break;

            case 2:
                RabLv = 1.7f;
                rabbitspeed = 450.0f;
                break;

            case 3:
                RabLv = 1.4f;
                rabbitspeed = 500.0f;
                break;

            case 4:
                RabLv = 1.1f;
                rabbitspeed = 550.0f;
                break;

            case 5:
                RabLv = 0.8f;
                rabbitspeed = 600.0f;
                break;
                      

         }


    }

    public void LeftClick()
    {
        if (nowposition > -300.0f) {
			Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
            this.Car.transform.localPosition += new Vector3(-220.0f, 0, 0);
        }
    }

    public void RightClick()
    {
        if (nowposition < 300.0f) {
			Mgrs.audioMgr.PlaySE( DEFINE_AUDIO.SE_TYPE.GAME_TAP );
            this.Car.transform.localPosition += new Vector3(220.0f, 0, 0);
        }
    }

    IEnumerator MakeRabbit()
    {
        while (true)
        {
            yield return new WaitForSeconds(this.RabLv);
            RabbitCar rabi = Util.InstantiateComponent<RabbitCar>(Rabbit, this.transform);
            rabi.transform.localPosition = new Vector2(rabbitpos,400);
           
        }
    }
}
    
    

     



