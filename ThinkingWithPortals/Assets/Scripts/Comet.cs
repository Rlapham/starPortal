using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour {

	public Transform rotatePoint;
	public float angularVelocity;

	void FixedUpdate () {
		transform.RotateAround(rotatePoint.position, Vector3.forward, angularVelocity * Time.fixedDeltaTime);
	}
}
