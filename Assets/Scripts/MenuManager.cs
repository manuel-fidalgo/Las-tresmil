using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	// Use this for initialization

	public GameObject PlayButton;
	public GameObject QuitButton;


	void Start () {

		Button btn; 

		btn = PlayButton.GetComponent<Button>();
        btn.onClick.AddListener(Play);

        btn = QuitButton.GetComponent<Button>();
        btn.onClick.AddListener(Quit);
	}
	

	void Quit(){
		Debug.Log("Quit");
		Application.Quit();
	}

	void Play(){
		Debug.Log("Play");
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}
}
