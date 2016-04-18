using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
	[SerializeField]
	private GameObject prefab;
	[SerializeField]
	private Transform parent;

	private List<Color> colorList = new List<Color> ()
	{
		Color.red,
		Color.blue,
		Color.green,
		Color.cyan,
		Color.black,
		Color.yellow,
	};

	private const int btnMax = 3;

	private Dictionary<int, GMSameColorButtonComponent> btnList = new Dictionary<int, GMSameColorButtonComponent>();

	private void Awake()
	{
		this.Init ();
	}

	private void Init()
	{
		this.btnList = new Dictionary<int, GMSameColorButtonComponent> ();
		for (int index = 0; index < btnMax; index++)
		{
			GMSameColorButtonComponent component = Util.InstantiateComponent<GMSameColorButtonComponent> (this.prefab, this.parent);//botton生成
			int listIndex = index % this.colorList.Count;//colorList.count=6
			component.Init (index, this.colorList [listIndex], listIndex, this.ChangeColor);
			this.btnList.Add (index, component);
		}
	}

	private bool isClear = false;
	private void ChangeColor(int btnIndex, int listIndex)
	{
		if (this.isClear)
		{
			return;
		}

		bool isLastIndex = ((listIndex + 1) >= this.colorList.Count);
		int newIndex = isLastIndex ? 0 : ++listIndex;
		this.btnList [btnIndex].ChangeColor (this.colorList [newIndex], newIndex);

		this.isClear = this.Judge ();
		if (this.isClear)
		{
			// GameClear
		}
	}

	private bool Judge()
	{
		for (int index = 0; index < (this.btnList.Count - 1); index++)
		{
			if (this.btnList [index].listIndex != this.btnList [index + 1].listIndex)
			{
				
				return false;
			}
		}
	
		return true;
	}
}
