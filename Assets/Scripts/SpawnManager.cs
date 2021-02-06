using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager Instance;

	SpawnPoint[] spawnPoints;

	void Awake() {
		Instance = this;
		spawnPoints = GetComponentsInChildren<SpawnPoint>();
	}

	public Transform GetSpawnPoitn()  {
		print("Number fo spawnPoints: "+spawnPoints.Length);
		return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
	}
}
