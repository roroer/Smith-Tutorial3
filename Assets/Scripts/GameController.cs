using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public int winScore = 100;

	public Text scoreText;
	public Text gameOverText;
	public Text restartText;
	public Text winText;

	private bool gameOver;
	private bool restart;

	public int score;
	void Start()
    {
		gameOver = false;
		restart = false;

		winText.text = "";
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine (SpawnWaves());
    }

	void Update() {
		if (restart) {
			if (Input.GetKeyDown(KeyCode.X)) {
				SceneManager.LoadScene(0);
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			if (gameOver) {
				restartText.text = "Press X to Restart";
				restart = true;
				break;
			}
			yield return new WaitForSeconds(waveWait);

		}
	}

	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}

    void UpdateScore() {
		scoreText.text = "Points: " + score;
		WinScore();
	}

	void WinScore() {
		if (score >= winScore) {
			winText.text = "You win! Game by Alex Smith";
			restartText.text = "Press X to Restart";
			gameOver = true;
			restart = true;
		}
	}

	public void GameOver() {
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
