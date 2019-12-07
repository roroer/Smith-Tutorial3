using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpeedUp : MonoBehaviour
{
	public GameController gameController;
	public ParticleSystem particleStars;
	public ParticleSystem backParticleStars;
	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
			if (gameControllerObject != null) {
				gameController = gameControllerObject.GetComponent<GameController>();
			}
	}
	void Update() {
		ParticleSystem.MainModule psMain = particleStars.main;
		if (gameController.win) {
			particleStars.Stop();
			particleStars.Clear();
			psMain.startColor = new Color(1, 0, 0, 1);
			particleStars.Play();
		}

		ParticleSystem.MainModule bpsMain = backParticleStars.main;
		if (gameController.win) {
			backParticleStars.Stop();
			bpsMain.startColor = new Color(0, 1, 0, 1);
			backParticleStars.Clear();
			backParticleStars.Play();
		}
	}
}
