using UnityEngine;
using System.Collections;

public class RacketComponent : MonoBehaviour {

	public string axisName;

	public BoxCollider colliderTop;
	public BoxCollider colliderBottom;

	/// <summary>
	/// 敵
	/// </summary>
	public bool isAuto = false;
	private const float SPEED = 4f;

	private float _maxPosTop = 0f;
	/// <summary>
	/// プレイヤーが移動できる最大位置
	/// </summary>
	private float maxPosTop {
		get {
			if( this._maxPosTop.Equals(0f) ) {
				this._maxPosTop = (this.colliderTop.transform.localPosition.y - this.colliderTop.transform.localScale.y/2f - this.transform.localScale.y/2f);
			}
			return this._maxPosTop;
		}
	}

	private float _maxPosBottom = 0f;
	/// <summary>
	/// プレイヤーが移動できる最大位置
	/// </summary>
	private float maxPosBottom {
		get {
			if( this._maxPosBottom.Equals(0f) ) {
				this._maxPosBottom = (this.colliderBottom.transform.localPosition.y + this.colliderBottom.transform.lossyScale.y/2f + this.transform.localScale.y/2f);
			}
			return this._maxPosBottom;
		}
	}

	/// <summary>
	/// 画面上部まで移動しているか
	/// </summary>
	/// <value><c>true</c> if is wall top; otherwise, <c>false</c>.</value>
	public bool isWallTop {
		get {
			return (this.transform.localPosition.y > this.maxPosTop);
		}
	}
	
	/// <summary>
	/// 画面下部までに移動しているか
	/// </summary>
	/// <value><c>true</c> if is wall botton; otherwise, <c>false</c>.</value>
	public bool isWallBotton{
		get {
			return (this.transform.localPosition.y < this.maxPosBottom);
		}
	}

	private PongGamePanelController pongCtrl;

	public void Init( PongGamePanelController pongCtrl ) {
		this.pongCtrl = pongCtrl;
	}

	void Update() {
		if( this.isAuto ) {
			this.Enemy();
		}else{
			float axisValue = Input.GetAxis(this.axisName);
			transform.Translate(Vector3.up * axisValue * SPEED * Time.deltaTime);
		}

		if( this.isWallTop ) {
			this.transform.localPosition = new Vector3( this.transform.localPosition.x, this.maxPosTop, 0f);
			return;
		}
		if( this.isWallBotton ) {
			this.transform.localPosition = new Vector3( this.transform.localPosition.x, this.maxPosBottom, 0f);
			return;
		}
	}


	private void Enemy() {
		if( this.pongCtrl.ballList.Count.Equals(0) ) {
			return;
		}
		BallComponent ball = this.pongCtrl.ballList.First.Value;
		this.transform.position = new Vector3(this.transform.position.x, ball.transform.position.y, 0f );
	}
}
