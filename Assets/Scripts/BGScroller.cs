using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
	public GameController gameController;

	public float normalSpeed;
	public float winSpeed;
	public float loseSpeed;
	private float currentSpeed;
	public float tileLength;

	private Vector3 startPos;

	private void Start() {
		startPos = transform.position;
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	private void Update() {
		if (gameController.win == true) {
			currentSpeed = Mathf.Lerp(normalSpeed, winSpeed, 0.5f);
		} else if (gameController.lose == true) {
			currentSpeed = Mathf.Lerp(normalSpeed, loseSpeed, 0.5f);
		} else {
			currentSpeed = normalSpeed;
		}
		transform.position -= transform.up * currentSpeed * Time.deltaTime;
		if (transform.position.z <= startPos.z - tileLength) {
			transform.position = startPos;
		}
	}



	/*	public float scrollSpeed;
		public float tileSizeZ;
		public float winScrollSpeed;
		public float loseScrollSpeed;
		public float smoothSpeed = 0.2f;

		public GameController gameController;

		private Vector3 startPosition;

		private void Start() {
			startPosition = transform.position;
			GameObject gameControllerObject = GameObject.FindWithTag("GameController");
			if (gameControllerObject != null) {
				gameController = gameControllerObject.GetComponent<GameController>();
			}
		}
		void Update()
		{
			float newPos =  Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
			transform.position = startPosition + Vector3.forward * newPos;
			if(gameController.win == true) {
				scrollSpeed = Mathf.Lerp(scrollSpeed, winScrollSpeed, smoothSpeed);
			}
			if (gameController.lose == true) {
				scrollSpeed = Mathf.Lerp(scrollSpeed, loseScrollSpeed, smoothSpeed);
			} 
		} */

}
