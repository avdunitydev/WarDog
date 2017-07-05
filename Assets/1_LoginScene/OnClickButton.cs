using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickButton : MonoBehaviour
{
	public Button btnSIN;
	public Button btnSUP;
	public InputField playerNameIN, playerEmailIN, playerPassIN, playerNameUP, playerEmailUP, playerPassUP;
	public Text consoleText;
	public RectTransform panelIN;
	public RectTransform panelUP;
	public ChangePanel changePanel;

	CheckInDB.IsValidate validate;

	void setTextToConsole (string text)
	{
		consoleText.text = text;
	}

	//SignIN
	public void OnButtonSignIN ()
	{
		validate = CheckInDB.isFieldOk;
		if (validate == (CheckInDB.IsValidate.email | CheckInDB.IsValidate.login)) {
			StartCoroutine (SignInUser (playerNameIN.text, playerPassIN.text));
		} else
			setTextToConsole ("Errrror validate = CheckInDB.isFieldOk ?????????");
	}

	IEnumerator SignInUser (string name, string password)
	{
		setTextToConsole ("start sign IN ...");
		//Debug.Log ("start sign IN ...");
		WWWForm form = new WWWForm ();
		form.AddField ("name", name);
		form.AddField ("password", password);

		WWW www = new WWW ("http://localhost/test2.site/php/SignInUser.php", form);
		yield return www;

		if (!string.IsNullOrEmpty (www.error)) {
			setTextToConsole ("Error: " + www.error);
			//Debug.LogError ("Error: " + www.error);
			yield break;
		}
		setTextToConsole ("... result >> " + www.text);
		//print ("... result >> " + www.text);
		if (www.text.Length < 24) {
			setTextToConsole ("Вхід виконано успішно !!!");
			//Debug.Log ("Вхід виконано успішно");
			PlayerPrefs.SetInt ("user_id", int.Parse (www.text));
			Invoke ("setTextToConsole (\"Player ID : \" + www.text)", 2);
			//SceneManager.LoadScene (1);   // ---- UnCommit TO DO loading next ...
		} else {
			setTextToConsole ("Не вірний пароль. Pleas try again !!!");
			//Debug.LogError ("Не вірний пароль. Pleas try again !!!");
		}
		yield return null;
	}

	// SignUP
	public void OnButtonSignUP ()
	{
		validate = CheckInDB.isFieldOk;
		if (validate == (CheckInDB.IsValidate.email | CheckInDB.IsValidate.login | CheckInDB.IsValidate.pass | CheckInDB.IsValidate.rePass)) {
			StartCoroutine (AddNewUser (playerNameUP.text, playerEmailUP.text, playerPassUP.text));
		} else
			setTextToConsole ("Errrror validate = CheckInDB.isFieldOk ?????????");
	}

	IEnumerator AddNewUser (string name, string email, string password)
	{
		setTextToConsole ("Start add User to DB ...");
		//print ("Start add User to DB ...");
		WWWForm form = new WWWForm ();
		form.AddField ("name", name);
		form.AddField ("email", email);
		form.AddField ("password", password);

		WWW www = new WWW ("http://localhost/test2.site/php/SignUpUser.php", form);
		yield return www;

		if (!string.IsNullOrEmpty (www.error)) {
			setTextToConsole ("Error: " + www.error);
			//print ("Error: " + www.error);
			yield break;
		}
		setTextToConsole ("... adding COMPLEAT !!!" + www.text);
		//print ("... adding COMPLEAT !!!" + www.text);

		changePanel.Active (panelIN);
		changePanel.DeActive (panelUP);
		yield return null;
	}


}
