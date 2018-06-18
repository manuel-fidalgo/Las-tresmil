using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	// Use this for initialization

	public GameObject VR;
    public GameObject Desktop;
	public GameObject QuitButton;


	void Start () {

		Button btn; 

		btn = VR.GetComponent<Button>();
        btn.onClick.AddListener(PlayVR);

        btn = QuitButton.GetComponent<Button>();
        btn.onClick.AddListener(Quit);
	}
	

	void Quit(){
		Debug.Log("Quit");
		Application.Quit();
	}

	void PlayVR(){
		Debug.Log("Play VR");
		SceneManager.LoadScene("MainScene VR", LoadSceneMode.Single);
	}

    void PlayDesktop(){
        Debug.Log("Play Desktop");
        SceneManager.LoadScene("MainScene Desktop", LoadSceneMode.Single);
    }
}
