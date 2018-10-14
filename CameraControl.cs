using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CameraControl.cs is a script for camera control in strategy games
public class CameraControl : MonoBehaviour {
	
	public float scrollSpeed; //0.25 is a good default; 0.0-1.0
	public float rotateSens;
	public float zoomSpeed; //0.75 is a good default; 0.0-1.0
	
	public bool invertedRotate;
	
	private Vector3 scrollAnchor;
	private Vector3 rotateAnchor;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currPoint = Input.mousePosition;

		//secondary button for rotation
	// UI design question: should player be allowed to rotate camera freely?
	// or just lock them to specific axes (rotate controls go on kb then)
		if(Input.GetMouseButtonDown(1)) {
			rotateAnchor = currPoint;
		}
		if(Input.GetMouseButton(1)) {
			Vector3 rotateVec = currPoint-rotateAnchor;
			rotateVec = Camera.main.ScreenToViewportPoint(rotateVec);
			float invert = 1.0f;
			if(invertedRotate) invert = -1.0f;
			float temp = -rotateVec.y * invert;
			rotateVec.y = rotateVec.x * invert;
			rotateVec.x = temp;
			Camera.main.transform.eulerAngles = rotateSens * rotateVec;
		}
		
		//middle button for scroll
	// UI design problem: should player be allowed to scroll camera freely?
	// or just lock them to focusing camera on a certain spot in the hexgrid?
		if(Input.GetMouseButtonDown(2)) {
			scrollAnchor = currPoint;
		}
		if(Input.GetMouseButton(2)) {
			Vector3 scrollVec = currPoint-scrollAnchor;
			scrollVec = Camera.main.ScreenToViewportPoint(scrollVec);
			Camera.main.transform.Translate(scrollVec*scrollSpeed);
		}
		
		//scrollwheel for zoom
		Vector3 zoom = new Vector3(0.0f,0.0f,Input.mouseScrollDelta.y);
		Camera.main.transform.Translate(zoom*zoomSpeed);
	}
}
