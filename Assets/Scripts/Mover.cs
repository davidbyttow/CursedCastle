using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private float duration = 0.25f;

	private Vector2 startPosition = Vector2.zero;
	private Vector2 targetPosition = Vector2.zero;
	private float elapsed = 0;
	private bool triggered;

	void Start() {
		startPosition = transform.position;
		targetPosition = target.position;
	}

	public void Trigger() {
		if (triggered) {
			return;
		}
		triggered = true;
		elapsed = 0;
	}

	void Update() {
		if (!triggered) {
			return;
		}
		if (elapsed < duration) {
			elapsed += Time.deltaTime;
			if (elapsed >= duration) {
				elapsed = duration;
			}
			transform.position = Vector2.Lerp(startPosition, targetPosition, elapsed / duration);
		}
	}

	void OnDrawGizmosSelected() {
		if (target) {
			Gizmos.DrawWireSphere(target.position, 0.2f);
		}	
	}
}
