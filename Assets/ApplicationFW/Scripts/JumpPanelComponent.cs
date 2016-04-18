using UnityEngine;
using System.Collections;

public class JumpPanelComponent {

	public string panelPath = "";

	public void Done() {
		Mgrs.pnlMgr.Display( this.panelPath );
	}
}
