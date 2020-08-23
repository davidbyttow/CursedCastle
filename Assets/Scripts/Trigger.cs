using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {

	[SerializeField] private UnityEvent onTrigger;
	[SerializeField] private bool once = true;

	private bool triggered = false;

	void OnTriggerEnter2D(Collider2D other) {
		if (once && triggered) {
			return;
		}
		if (other.TryGetComponent(out Player player)) {
			triggered = true;
			onTrigger.Invoke();
		}
	}
}
