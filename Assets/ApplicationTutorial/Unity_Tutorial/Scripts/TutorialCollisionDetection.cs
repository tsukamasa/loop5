using UnityEngine;
using System.Collections;

public class TutorialCollisionDetection : BasePanelController {
	
	public int intAddForceValue = 0;
	
	private void Update (){
		
		//スペースキーを入力
		if(Input.GetKeyDown(KeyCode.Space)){
			
			Rigidbody rigidbody = this.GetComponent<Rigidbody>();
			
			//オブジェクトに力を加える
			//Rigidbodyの物理演算機能を使用　※AddForceメソッド
			rigidbody.AddForce(new Vector3(0f, this.intAddForceValue, 0f));
		}
		
		/*
		//スペースキーを入力 ※ボタンを押した瞬間
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log("ボタンを押しました。");
		}
		//スペースキーを入力 ※ボタンを押している間
		if(Input.GetKey(KeyCode.Space)){
			Debug.Log("ボタンを押しています");
		}
		//スペースキーを入力 ※ボタンを離した瞬間
		if(Input.GetKeyUp(KeyCode.Space)){
			Debug.Log("ボタンを離しました。");
		}
		*/
	}
	
	/// <summary>
	/// オブジェクトと接触した時呼ばれるメソッド
	/// </summary>
	/// <param name="collision">Collision.</param>
	public void OnCollisionEnter(Collision collision){
		//床にボールが接地したら
		//オブジェクトに設定している Tag を使用してどのオブジェクトを当たっているかを判定
		if(collision.gameObject.CompareTag("TutorialCollision_Top")){
			Debug.Log("Top::床に接地しました。");
		}
		
		if(collision.gameObject.CompareTag("TutorialCollision_Bottom")){
			Debug.Log("Bottom::床に接地しました。");
		}
		
	}
	
	/*
	/// <summary>
	/// オブジェクトとの接触が離れた時呼ばれるメソッド
	/// </summary>
	/// <param name="Collision">Collision.</param>
	public void OnCollisionExit(Collision Collision) {
		Debug.Log("床から離れました。");
		
		//オブジェクトの色を変更
	 	MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
		meshRenderer.material.color = Color.white;	
	}
	*/
	
	private void OnBackButton()
	{
		Mgrs.pnlMgr.Display("Prefabs/UnityTutorialTitlePanel");
	}
}
