using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks {
	public static RoomManager Instance;


    void Awake() {
		if (Instance) { // If other room manager 
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject); // only one room manager
        Instance = this;
    }

	public override void OnEnable() {
		base.OnEnable();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable() {
		base.OnDisable();
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
		if (scene.buildIndex == 1) {
			print("Changed SCene");
			PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
		}
	} 
}
