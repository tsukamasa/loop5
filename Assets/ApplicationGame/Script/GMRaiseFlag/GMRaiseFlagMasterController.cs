using UnityEngine;
using System.Collections;

public class GMRaiseFlagMasterController : BaseGMRaiseFlagPlayerController
{
	/// <summary>プレイヤーが既にボタンを押してあげる方を決定している場合</summary>
	[HideInInspector]
	public bool isPlayerChoiced = false;

	private void Update()
	{
		if (this.isFixedChoice || this.isPlayerChoiced)
		{
			return;
		}
		//	ランダムであげる旗の選択をして、基底クラスの旗アニメーションをする
		this.SetFlagAnimation ((Random.Range (0, 100) % 2 == 0) ? FLAG.LEFT : FLAG.RIGHT);
	}
}