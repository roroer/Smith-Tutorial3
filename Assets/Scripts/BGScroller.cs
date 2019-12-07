﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public float winScrollSpeed;
	public float loseScrollSpeed;
	public float smoothSpeed = 4;

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
			scrollSpeed = Mathf.Lerp(scrollSpeed, winScrollSpeed, Time.deltaTime * smoothSpeed);
		}
		if (gameController.lose == true) {
			scrollSpeed = Mathf.Lerp(scrollSpeed, loseScrollSpeed, Time.deltaTime * smoothSpeed);
		}
	}
}
