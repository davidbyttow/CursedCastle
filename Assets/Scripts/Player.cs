using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class Player : MonoBehaviour {
	
	[SerializeField] private ParticleSystem deathEffect;

	private Rigidbody2D rigidBody;
	private SpriteRenderer sprite;
	private CharacterController2D controller;
	public bool dead = false;
	public bool jumping = false;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		controller = GetComponent<CharacterController2D>();
	}

	void Update() {
		bool jump = Input.GetButtonDown("Jump");
		if (jump) {
			jumping = true;
			controller.Jump();
		}
		if (Input.GetButtonUp("Jump")) {
			jumping = false;
			controller.CancelJump();
		}
	}

	void FixedUpdate() {
		float horizontalInput = Input.GetAxis("Horizontal");
		controller.Move(horizontalInput);
	}

	public void Die() {
		if (dead) {
			return;
		}
		if (Camera.main.TryGetComponent(out CameraShake shake)) {
			shake.Shake();
		}
		dead = true;
		SoundManager.inst.PlayDeath();
		var effect = Instantiate(deathEffect, transform.position, Quaternion.Euler(0, -90, 0));
		effect.Play();
		gameObject.SetActive(false);
		GameManager.global.QueueRestart();
	}
}
