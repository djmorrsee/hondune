using UnityEngine;
using System.Collections;

public class KillTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		Application.LoadLevel(Application.loadedLevel);
	}
}
