﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

	private const float groundCheckRadius = 0.2f;
	private const float ceilingCheckRadius = 0.2f;

	[Range(0, 0.3f)] [SerializeField] private float movementSmoothing;
	[SerializeField] private LayerMask groundMask;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private Transform ceilingCheck;
	[SerializeField] private float horizontalSpeed = 10f;
	[SerializeField] private float inAirSpeedScale = 0.8f;
	[SerializeField] private float jumpForce = 400f;
	[SerializeField] private float jumpingGravityScale = 3f;
	[SerializeField] private float gravityScale = 4f;

	private float inAirStartTime = 0;
	private Rigidbody2D rigidBody;
	private CapsuleCollider2D capsule;

	private Vector3 velocity = Vector3.zero;
	private bool isGrounded;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.gravityScale = gravityScale;
		rigidBody.isKinematic = false;
		rigidBody.freezeRotation = true;

		capsule = GetComponent<CapsuleCollider2D>();
	}

	void Start() {
		inAirStartTime = Time.time;
	}

	void FixedUpdate() {
		CheckGround();

		if (!isGrounded && rigidBody.velocity.y < 0) {
			rigidBody.gravityScale = gravityScale;
		}
	}

	private void CheckGround() {
		bool wasGrounded = isGrounded;
		isGrounded = false;

		// TODO: Sphere cast or something

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundMask);
		foreach (Collider2D collider in colliders) {
			if (collider.gameObject != gameObject) {
				isGrounded = true;
				break;
			}
		}

		if (wasGrounded && !isGrounded) {
			inAirStartTime = Time.time;
		}

		if (isGrounded && !wasGrounded) {
			if (Time.time - inAirStartTime > 0.2f) {
				// Re-using the jump sound
				SoundManager.inst.PlayJump();
			}
		}
	}

	public void Move(float horizontalInput) {
		var horizontalVelocity = horizontalInput * horizontalSpeed * (isGrounded ? 1f : inAirSpeedScale);
		Vector3 targetVelocity = new Vector2(horizontalVelocity, rigidBody.velocity.y);
		rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);
	}

	public void Jump() {
		if (isGrounded) {
			SoundManager.inst.PlayJump();
			isGrounded = false;
			rigidBody.AddForce(new Vector2(0, jumpForce));
			rigidBody.gravityScale = jumpingGravityScale;
			inAirStartTime = Time.time;
		}
	}

	public void CancelJump() {
		if (!isGrounded) {
			rigidBody.gravityScale = gravityScale;
			var damping = rigidBody.velocity.y > 0 ? 0.5f : 1f;
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * damping);
		}
	}

	void OnDrawGizmosSelected() {
		if (groundCheck) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
		}
	}
}
