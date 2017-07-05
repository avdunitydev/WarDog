using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{

	public void Active (RectTransform targetPanel)
	{
		targetPanel.gameObject.SetActive (true);
	}

	public void DeActive (RectTransform targetPanel)
	{
		targetPanel.gameObject.SetActive (false);
	}
}
