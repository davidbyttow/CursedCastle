using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class Player : MonoBehaviour {

	private CharacterController2D controller;

	void Awake() {
		controller = GetComponent<CharacterController2D>();
	}

	void Update() {
		bool jump = Input.GetButtonDown("Jump");
		if (jump) {
			controller.Jump();
		}
	}

	void FixedUpdate() {
		float horizontalInput = Input.GetAxis("Horizontal");
		controller.Move(horizontalInput);
	}

	public void Die() {
		Debug.Log("I'm DEAD");
		gameObject.SetActive(false);
	}
}
