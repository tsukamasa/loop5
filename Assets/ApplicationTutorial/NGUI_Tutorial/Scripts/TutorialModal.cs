using UnityEngine;
using System.Collections;

public class TutorialModal : BasePanelController
{
	private bool isOpened = false;
	private bool isInitializing = false;
	private bool isOpening = false;
	private bool isClosing = false;
	private bool isAllEnd = false;
	private bool isTweening = false;
	
	public bool isForcePopModal = false;
	public UILabel label;
	
	public System.Action callback;
	
	// Use this for initialization
	void Start () 
	{
		this.isInitializing = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.isAllEnd)
		{
			return;
		}
		
		if (this.isForcePopModal && !this.isOpening && !this.isOpened)
		{
			this.isInitializing = false;
			this.isOpening = true;
			this.isOpened = true;
			this.label.text = "Start Tween - Open";
			this.Tweening(Vector3.zero, Vector3.one, "TweenOpenFinished", true, 0.2F);
		}
	}
	
	private void OnPushClose ()
	{
		if (this.isInitializing || this.isClosing || this.isOpening || this.isAllEnd || this.isTweening)
		{
			return;
		}
		
		this.isClosing = true;
		this.label.text = "Start Tween - Close";
		this.Tweening(Vector3.one, Vector3.zero, "TweenCloseFinished", false, 0.2F);
	}
	
	private void Tweening (Vector3 in_start, Vector3 in_end, string in_callWhenFinished, bool in_isForward, float in_duration)
	{
		if (this.isTweening)
		{
			return;
		}
		this.isTweening = true;
		
		TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
		
		tweenScale.from = in_start;
		tweenScale.to = in_end;
		tweenScale.callWhenFinished = in_callWhenFinished;
		tweenScale.eventReceiver = gameObject;
		tweenScale.duration = in_duration;
		tweenScale.style = UITweener.Style.Once;
		tweenScale.Play(true);
	}
	
	private void TweenOpenFinished()
	{
		this.label.text = "End Tween - TweenOpenFinished";
		this.isOpening = false;
		this.isTweening = false;
	}
	
	private void TweenCloseFinished()
	{
		this.label.text = "End Tween - TweenCloseFinished";
		this.isClosing = false;
		this.isAllEnd = true;
		this.isTweening = false;
		
		if(this.callback != null)
			this.callback();
		
		Destroy(gameObject);
	}
}
