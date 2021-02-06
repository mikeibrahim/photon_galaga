using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraFollow : MonoBehaviour {
	// PlayerController[] players;
	GameObject targetPlayer;

	private float dampTime = 0.25f;
    Vector3 velocity;

	public void SetPlayer(GameObject player) {
		targetPlayer = player;
	}

    void Update() {
        if (targetPlayer) {
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(targetPlayer.transform.position);
			Vector3 delta = targetPlayer.transform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			transform.rotation = targetPlayer.transform.rotation;
         }
    }
}
