using UnityEngine;
using System.Collections;

public class GMButtonCommon : MonoBehaviour {

	[SerializeField] protected float showDulation;
	[SerializeField] protected float destoryDulation;
	[SerializeField] protected UISprite btnSprite;
	[SerializeField] protected UILabel lbl;

	public float scale = 1f;
	public DEFINE.COLOR_ID colorId;
	public object param {
		get;
		private set;
	}
	public bool isClicked = false;
	private System.Action<GMButtonCommon> callback;

	void Awake() {
		this.SetColor (this.colorId);
	}

	void Start() {
		this.PlayShow ();
	}

	public void InitCallBack(System.Action<GMButtonCommon> callback) {
		this.callback = callback;
	}

	public void SetScale( float scale = 1f) {
		this.scale = scale;
		this.transform.localScale = Vector3.one * scale;
	}

	public void SetColor( DEFINE.COLOR_ID colorId ) {
		this.colorId = colorId;
		this.btnSprite.color = DEFINE.COLORS [this.colorId].color;
	}

	public void SetLabel( string str ) {
		this.lbl.text = str;
	}

	public void SetParam( object obj ) {
		this.param = obj;
	}

	public T GetParam<T>() where T:System.IComparable<T> {
		return (T)this.param;
	}

	public void OnClickItem(){
		if (this.isClicked) {
			return;
		}
		this.isClicked = true;
		this.OnClickEvent ();
		this.PlayHide (() => {
			Destroy( this.gameObject );
		});
	}

	protected virtual void OnClickEvent() {
		this.callback (this);
	}

	protected void PlayShow(System.Action cb = null, float delay = 0f){
		// scale
		TweenScale twScale = UITweener.Begin<TweenScale>(gameObject, this.showDulation);
		twScale.from = Vector3.zero;
		twScale.to = Vector3.one * this.scale;
		twScale.delay = delay;

		// alpha
		TweenAlpha twAlpha = UITweener.Begin<TweenAlpha>(gameObject, this.showDulation);
		twAlpha.from = 0f;
		twAlpha.to = 1f;
		twAlpha.delay = delay;

		if (cb != null) {
			StartCoroutine (WaitTweener(new UITweener[]{twScale, twAlpha}, cb));
		}
	}

	protected void PlayHide(System.Action cb = null, float delay = 0f){
		// scale
		TweenScale twScale = UITweener.Begin<TweenScale>(gameObject, this.destoryDulation);
		twScale.from = this.transform.localScale;
		twScale.to = this.transform.localScale * 1.5f;
		twScale.delay = delay;
		
		// alpha
		TweenAlpha twAlpha = UITweener.Begin<TweenAlpha>(gameObject, this.destoryDulation);
		twAlpha.from = 1f;
		twAlpha.to = 0f;
		twAlpha.delay = delay;

		if (cb != null) {
			StartCoroutine (WaitTweener(new UITweener[]{twScale, twAlpha}, cb));
		}
	}

	private IEnumerator WaitTweener(UITweener[] tweeners, System.Action cb) {
		yield return null;
		bool isTween = true;
		while (isTween) {
			isTween  = false;
			foreach (UITweener tweener in tweeners) {
				if( tweener.enabled == true) {
					isTween = true;
				}
			}
			yield return null;
		}
		if (cb != null) {
			cb ();
		}
	}

}
