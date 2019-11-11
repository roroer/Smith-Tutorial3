using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public Boundary boundary;

	public float speed = 5;
	public float tilt;

	public float fireRate = 1;
	private float nextFire;

	public GameObject shot;
	public Transform shotSpawn;
	Rigidbody rb;

	AudioSource audioSource;

	private void Start() {
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update() {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play();
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
		rb.velocity = movement * speed;
		rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin,boundary.xMax), 0, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
		rb.rotation = Quaternion.Euler(0f, 0f, rb.velocity.x * -tilt);
	}

}
