﻿using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	[SerializeField] private float duration = 0.25f;

	private float timeLeft = 0.0f;
	private float magnitude = 0.7f;
	private Vector3 initialPosition;

	void Update() {
		if (timeLeft > 0) {
			timeLeft -= Time.deltaTime;
			var rand = Random.insideUnitSphere;
			var pos = initialPosition + Random.insideUnitSphere * magnitude;
			transform.position = new Vector3(pos.x, pos.y, initialPosition.z);
			if (timeLeft <= 0) {
				transform.position = initialPosition;
			}
			Debug.Log($"pos={transform.position}");
		}
	}

	public void Shake() {
		if (timeLeft <= 0) {
			timeLeft = duration;
			initialPosition = transform.position;
			Debug.Log($"pos={initialPosition}");
		}
	}
}
