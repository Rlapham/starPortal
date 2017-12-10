using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Web;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
	static GameManager me;

	void Awake(){
		if (me == null){
			me = this;
		}
		else{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		Debug.Log("starting");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}


	
	void OnDestroy(){
		Debug.Log("ending");

	}

	void SaveData(){

	}

	void LoadData(){

	}

	

}
