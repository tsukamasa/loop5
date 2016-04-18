using UnityEngine;
using System.Collections;

public class ScreenScale : MonoBehaviour {

	private static class SCREEN {
		public static readonly bool IS_VERTICAL = false;
		private static Vector2 CONTENT_VERTICAL_MIN = new Vector2( 640f, 960f );
		private static Vector2 CONTENT_VERTICAL_MAX = new Vector2( 720f, 1136f );
		private static Vector2 CONTENT_HORIZONTAL_MIN = new Vector2( 960f, 640f );
		private static Vector2 CONTENT_HORIZONTAL_MAX = new Vector2( 1136f, 720f );
		public static Vector2 CONTENT_MIN { get { return IS_VERTICAL ? CONTENT_VERTICAL_MIN : CONTENT_HORIZONTAL_MIN; } }
		public static Vector2 CONTENT_MAX { get { return IS_VERTICAL ? CONTENT_VERTICAL_MAX : CONTENT_HORIZONTAL_MAX; } }
	}

	public bool isFitWidth { get; private set; }
	
	private Vector2 baseSize {
		get { return SCREEN.CONTENT_MIN; }
	}
	private float baseRatio {
		get { return this.baseSize.x / this.baseSize.y; }
	}
	public static Vector2 MANUAL_SIZE = Vector3.one;

	private Vector2 _manualSize;
	public Vector2 manualSize {
		get{ UpdateSize(); return _manualSize; }
		private set{
			// todo refactaling...
			_manualSize = value; 
			MANUAL_SIZE = value;
		}
	}
	private Vector2 _diffSize;
	public Vector2 diffSize {
		get{ UpdateSize(); return _diffSize; }
		private set{ _diffSize = value; }
	}
	private Vector2 _currentSize;
	public Vector2 currentSize {
		get{ UpdateSize(); return _currentSize; }
		private set{ _currentSize = value; }
	}
	private Vector2 _currentRatio;
	public Vector2 currentRatio {
		get{ UpdateSize(); return _currentRatio; }
		private set{ _currentRatio = value; }
	}
	
	/// <summary>
	/// screenから見たlocalscale
	/// </summary>
	/// <value>The size of the screen unit.</value>
	public float screenUnitSize {
		get {
			return this.currentSize.x/this.manualSize.x;
		}
	}
	
	
	private void Awake() {
		UIRoot uiRoot = GetComponent<UIRoot> ();
		this.Init ();
		this.UpdateSize ();
	}

	private void Update() {
		this.UpdateSize ();
	}

	private void Init() {
		this.currentSize = Vector2.zero;
		this.currentRatio = Vector2.one;
		this.manualSize = baseSize;
	}
	
	public void UpdateSize() {
		Vector2 screen = new Vector2( (float) Screen.width, (float) Screen.height );
		if (this._currentSize.x == screen.x
		    && this._currentSize.y == screen.y ) {
			return;
		}
		this.currentSize = screen;
		
		float screenRatio = screen.x / screen.y;
		Vector2 ratio = Vector2.one;
		
		this.isFitWidth = (screenRatio > this.baseRatio);
		if( this.isFitWidth ) { //縦長
			ratio.x = (screenRatio/this.baseRatio);
		} else { //横長
			ratio.y = (this.baseRatio/screenRatio);
		}
		this.currentRatio = ratio;
		
		this.manualSize = this.GetScaleSize( this.baseSize );
		UIRoot uiRoot = GetComponent<UIRoot> ();
		uiRoot.manualHeight = (int)this.manualSize.y;
		uiRoot.minimumHeight = (int)this.manualSize.y;
		uiRoot.maximumHeight = (int)this.manualSize.y;
//		this.diffSize = new Vector2( this._manualSize.x-this.baseSize.x, this._manualSize.y-this.baseSize.y);
		
	}
	
	public Vector3 GetScaleSize( Vector3 size, bool isHorizontal = true, bool isVertical = true) {
		if( isHorizontal ) size.x *= this.currentRatio.x;
		if( isVertical ) size.y *= this.currentRatio.y;
		return size;
	}
	
	public Vector3 GetExtendSize( Vector3 size, bool isHorizontal = true, bool isVertical = true ) {
		UpdateSize();
		if( isHorizontal ) size.x += this.diffSize.x;
		if( isVertical ) size.y += this.diffSize.y;
		return size;
	}
	
	public Vector4 GetExtendRange( Vector4 range, bool isHorizontal = true, bool isVertical = true, UIWidget.Pivot pivot = UIWidget.Pivot.Center ) {
		UpdateSize();
		if( isHorizontal ) {
			range.z += this.diffSize.x;
			switch( pivot ) {
			case UIWidget.Pivot.TopLeft:
			case UIWidget.Pivot.Left:
			case UIWidget.Pivot.BottomLeft:
				range.x += this.diffSize.x/2f;
				break;
			case UIWidget.Pivot.TopRight:
			case UIWidget.Pivot.Right:
			case UIWidget.Pivot.BottomRight:
				range.x -= this.diffSize.x/2f;
				break;
			}
		}
		if( isVertical ) {
			range.w += this.diffSize.y;
			switch( pivot ) {
			case UIWidget.Pivot.TopLeft:
			case UIWidget.Pivot.Top:
			case UIWidget.Pivot.TopRight:
				range.y -= this.diffSize.y/2f;
				break;
			case UIWidget.Pivot.BottomLeft:
			case UIWidget.Pivot.Bottom:
			case UIWidget.Pivot.BottomRight:
				range.y += this.diffSize.y/2f;
				break;
			}
		}
		
		return range;
	}

}
