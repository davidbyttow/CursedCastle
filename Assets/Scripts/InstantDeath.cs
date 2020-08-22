using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
		var other = collision.collider;
		Debug.Log($"Collided: {other}, {other.gameObject}");
		if (other.TryGetComponent(out Player player)) {
			player.Die();
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log($"Touched: {other}, {other.gameObject}");
		if (other.TryGetComponent(out Player player)) {
			player.Die();
		}	
	}
}
