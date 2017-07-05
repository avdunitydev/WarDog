using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {
	public float speed;
	public Slider slider;
	public Text sliderValueText;
	float timer;
	
	// Use this for initialization
	void Start () {
		timer = 0f;
		//Invoke("LoadMainScene", 27f);
	}

	void LoadMainScene(){
		SceneManager.LoadScene(1);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime * speed; 
		if(timer > 100){
			timer = 100f;
			LoadMainScene();
		}else{
			slider.value = timer;
			sliderValueText.text = (slider.value / 100).ToString("P");
		}
	}
}
