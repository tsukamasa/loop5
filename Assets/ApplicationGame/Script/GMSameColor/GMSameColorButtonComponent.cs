using UnityEngine;
using System.Collections;

public class GMSameColorButtonComponent : MonoBehaviour
{
	public UISprite btnSprite;

	private int btnIndex = 0;
	private Color btnColor = Color.red;
	public int listIndex = 0;
	private System.Action<int, int> callback;

	public void Init(int btnIndex, Color color, int listIndex, System.Action<int, int> callback)
	{
		this.btnIndex = btnIndex;
		this.callback = callback;
		this.ChangeColor (color, listIndex);
	}

	public void ChangeColor(Color color, int listIndex)
	{
		this.btnColor = color;
		this.listIndex = listIndex;
		this.btnSprite.color = this.btnColor;
	}

	private void OnPush()
	{
		this.callback (this.btnIndex, this.listIndex);
	}
}