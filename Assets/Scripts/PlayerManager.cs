using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour {
	PhotonView PV;

	GameObject controller;
	CameraFollow cam;
    
	void Awake() {
		PV = GetComponent<PhotonView>();
		cam = GameObject.FindObjectOfType<CameraFollow>();
	}

	void Start() {
		if (PV.IsMine) {
			CreateController();
		}
	}

	void CreateController() {
		Transform spawnPoint = SpawnManager.Instance.GetSpawnPoitn();
		controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation, 0, new object[] { PV.ViewID });
		controller.GetComponent<PlayerController>().SetPlayerManager(this);
		cam.SetPlayer(controller);
	}

	public void Die() {
		PhotonNetwork.Destroy(controller);
		CreateController();
	}
}