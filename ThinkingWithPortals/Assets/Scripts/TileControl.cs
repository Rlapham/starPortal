using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour {

	public bool hasBeenTouched;
	MeshRenderer myMesh;
	public float fadeInSpeed;
	public GameObject myComet;


	void Awake () {
		myMesh = GetComponent<MeshRenderer>();
	}
	
	void Update () {
		if (hasBeenTouched){
			myMesh.enabled = true;
			myComet.SetActive(false);
		}

		if (hasBeenTouched && myMesh.material.color.a < 1f){
			myMesh.material.color += new Color (0,0,0,fadeInSpeed * Time.deltaTime);
		}

	}
}
