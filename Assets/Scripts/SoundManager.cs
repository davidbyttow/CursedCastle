using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

	public static SoundManager inst { get; private set; }

	public AudioClip[] jumpSounds;
	public AudioClip[] landSounds;
	public AudioClip[] swooshSounds;
	public AudioClip[] deathSounds;
	public AudioClip[] doorCloseSounds;

	private AudioSource source;

	void Awake() {
		if (!inst) {
			inst = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
		source = GetComponent<AudioSource>();
	}

	public void PlayJump() {
		source.PlayOneShot(Pick(jumpSounds));
	}

	public void PlayLand() {
		source.PlayOneShot(Pick(landSounds));
	}

	public void PlaySwoosh() {
		source.PlayOneShot(Pick(swooshSounds));
	}

	public void PlayDeath() {
		source.PlayOneShot(Pick(deathSounds));
	}

	public void PlayDoorClose() {
		source.PlayOneShot(Pick(doorCloseSounds));
	}

	public AudioClip Pick(AudioClip[] clips) {
		return clips[Random.Range(0, clips.Length)];
	}
}
