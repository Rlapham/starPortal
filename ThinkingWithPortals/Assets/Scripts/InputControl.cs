using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InputControl : MonoBehaviour {

	public Camera myCam;
	public Text myText;
	public Text otherText;


	void Awake(){

	}

	void Update(){
		if (Input.touchCount > 0){
			myText.text = "touched";
			otherText.text = Input.GetTouch(0).position.ToString();
			
			ActualInput(new Vector3(Input.GetTouch(0).position.x ,Input.GetTouch(0).position.y, 0)) ;
		}
	}



	void ActualInput(Vector3 inputPos){
			//myText.text = "INSIDE";
						// set up ray
			
			Ray myRay = myCam.ScreenPointToRay(inputPos);
			RaycastHit rayHit = new RaycastHit();
			myText.text = "HIT 1";
			otherText.text = myRay.origin.ToString();


			
			if (Physics.Raycast(myRay.origin, myRay.direction, out rayHit, 1000f)){
				myText.text = "HIT2";
			//	if (rayHit.transform.gameObject.tag == "star"){
					rayHit.transform.gameObject.GetComponent<TileControl>().hasBeenTouched = true;
				//	myText.text = "HIT";
			//}
			}
	}

}