using UnityEngine;
using System.Collections;

public class GMListPanelController : BasePanelController {

	public GameObject prefabMenu;
	public UIGrid gridMenu;
	public UIDraggablePanel dragPanel;

	public override void Display (System.Collections.Generic.IDictionary<string, object> param) {
		base.Display (param);

		foreach (DEFINE_GAMES.GameInfo gameInfo in DEFINE_GAMES.GAMES) {
			GMListComponent button = Util.InstantiateComponent<GMListComponent>( this.prefabMenu, gridMenu.transform );
			button.Init( gameInfo );
		}
		this.gridMenu.Reposition ();
		this.dragPanel.ResetPosition ();
	}


	public void OnTitle() {
		Mgrs.pnlMgr.Display(DEFINE.PREFAB_PATH_TITLE_PANEL);
	}
}
