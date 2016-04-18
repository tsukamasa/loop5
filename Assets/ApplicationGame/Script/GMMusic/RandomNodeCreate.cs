using UnityEngine;
using System.Collections;
//GMMusicで使われる落ちてくるノードをランダム生成するscript
public class RandomNodeCreate : MonoBehaviour {
	public GameObject randomNodeSprite;
	int random_Num = 0 ;
	int spriteCounter = 0;

	void Start () {
		//１からMaxまでの間でランダムに出てくる数を決める
		random_Num = Random.Range (1,10);
	}

	// Update is called once per frame
	void Update () {
		if (spriteCounter < random_Num) {
			UISprite sprite = Util.InstantiateComponent<UISprite>(randomNodeSprite,this.transform) ;
			spriteCounter++;
		}
	}
}
