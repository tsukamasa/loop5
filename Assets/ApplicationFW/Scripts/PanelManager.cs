using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour {

	public Transform parentTransform;

	private BasePanelController currentPnlCtrl;

	public void Display( string prefabPath, IDictionary<string, object> param = null) {
		BasePanelController pnlCtrl = Util.InstantiateComponent<BasePanelController>( prefabPath, this.parentTransform );
		if( pnlCtrl == null ) {
			Debug.LogError( string.Format("nothing panel ::{0}", prefabPath ) );
			return;
		}
		this.DestroyCurrentController ();
		pnlCtrl.Display( param );
		this.currentPnlCtrl = pnlCtrl;
	}

	public void DestroyCurrentController() {
		if( this.currentPnlCtrl != null ) {
			Destroy( this.currentPnlCtrl.gameObject );
		}
	}

}
