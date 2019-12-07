using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue = 15;
	public GameController gameController;
	public int health = 1;

	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	void OnTriggerEnter(Collider other) {

		if(other.CompareTag ("Boundary") ||other.CompareTag("Enemy")) {
			return;
		}
		if (explosion != null) {
			Instantiate(explosion, transform.position, transform.rotation);
		}
		health--;
		Destroy(other.gameObject);
		if (health == 0) {
			Destroy(gameObject);
			gameController.AddScore(scoreValue);
		}
		if (other.tag == "Player") {
			Instantiate(explosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
	}
}
