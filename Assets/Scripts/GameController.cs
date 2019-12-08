using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	[SerializeField] AudioClip levelMusic;
	[SerializeField] AudioClip winMusic;
	[SerializeField] AudioClip loseMusic;

	AudioSource audioSource;

	public int winScore = 100;

	public Text scoreText;
	public Text gameOverText;
	public Text restartText;
	public Text winText;
	public Text timerText;
	public Text highScoreText;

	public bool gameOver;
	private bool restart;
	public bool win;
	public bool lose;
	public bool hardMode;

	public int score;
	public static int highScore = 100;
	int timerT;


	void Start()
    {
		audioSource = GetComponent<AudioSource>();
		gameOver = false;
		restart = false;

		highScoreText.text = "High score:" + highScore.ToString();
		winText.text = "";
		restartText.text = "";
		gameOverText.text = "";
		timerText.text = "";
		score = 0;
		UpdateScore();
		StartCoroutine (SpawnWaves());
		audioSource.PlayOneShot(levelMusic);
		if (hardMode) {
			beginTimer();
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.M)) {
			SceneManager.LoadScene(0);
		}
		if (restart) {
			if (Input.GetKeyDown(KeyCode.X)) {
				if (hardMode) {
					SceneManager.LoadScene(2);
				} else {
					SceneManager.LoadScene(1);
				}
			}
		}
		if (!audioSource.isPlaying && gameOver == false) {
			audioSource.PlayOneShot(levelMusic);
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while (!win) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards[UnityEngine.Random.Range(0, hazards.Length)];
				Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

		}
	}

	public void AddScore (int newScoreValue) {
			score += newScoreValue;
			UpdateScore();
	}

	private void beginTimer() {
		StartCoroutine(TimerTextUpdate());
	}

	IEnumerator TimerTextUpdate() {
		while (!gameOver) {
			timerText.text = "Time:" + timerT.ToString();
			yield return new WaitForSeconds(1);
			timerT++;
		}
	}

	void UpdateScore() {
		scoreText.text = "Points: " + score;
		WinScore();
	}

	void WinScore() {
		if (score >= winScore && !hardMode) {
			winText.text = "You win! Game by Alex Smith";
			restartText.text = "Press X to Restart";
			gameOver = true;
			restart = true;
			if (!win) {
				audioSource.Stop();
				audioSource.PlayOneShot(winMusic);
				win = true;
			}
		}
	}

	public void GameOver() {
		lose = true;
		gameOverText.text = "Game Over!";
		gameOver = true;
		restartText.text = "Press X to Restart";
		restart = true;
		if (highScore < score) {
			highScore = score;
			highScoreText.text = "High score:" + highScore.ToString();
		}
		audioSource.Stop();
		audioSource.PlayOneShot(loseMusic);
	}
}
