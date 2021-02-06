using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public struct playerType {
    public string shipName;

    public playerType (string n) {
        this.shipName = n;
    }
}

public static class Players {
	public static int EXPLORER = 0;

    public static playerType[] players = new playerType[] {
        //          shipName
        new playerType("Explorer"),
    };
}

public class PlayerController : MonoBehaviour {
	PhotonView PV;
	PlayerManager playerManager;

	#region PlayerStats
	private int maxHealth = 100;
	int currentHealth;
	#endregion

	#region Movement
	Rigidbody2D rb;
	private float maxSpeed = 20f;
	private float accelerationSpeed = 2;
	Vector2 smoothPosition;
	Vector2 velocity;
	private float turnSpeed = 150;
	#endregion

	private void Awake() {
        rb = GetComponent<Rigidbody2D>();
		PV = GetComponent<PhotonView>();
		currentHealth = maxHealth;
	}

    private void Start() {
		if (!PV.IsMine) { 
			Destroy(rb);
		}
    }

	private void FixedUpdate() {
		if (!PV.IsMine) { return; }

		// Movement
		Turn();
		Move();
		rb.MovePosition(smoothPosition);
	}

	#region Movement
	private void Turn() {
		Vector3 rot = transform.eulerAngles;
		rot.z += -Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime; // Turning the player with a & d keys
		transform.eulerAngles = rot;
	}

	private void Move() {
		Vector2 targetPosition = transform.TransformPoint(Vector3.up * (Input.GetKey(KeyCode.W) ? maxSpeed : 0)); // move certain amount if w key is pressed
		smoothPosition = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, accelerationSpeed); // smooth that distance
	}
	#endregion

	// void OnCollisionEnter2D(Collision2D col) {
    //     if (col.gameObject.GetComponent<Bullet>()) {
	// 		Bullet b = col.gameObject.GetComponent<Bullet>();
	// 		// TakeDamge(b.GetDanage());
	// 	}
    // }

	public void TakeDamge(int amount) {
		PV.RPC("RPC_TakeDamage", RpcTarget.All, amount);
	}

	// this is still running on my script, so it will only run if PV.IsMine returns true relative to me, which is my controller
	[PunRPC]
	void RPC_TakeDamage(int amount) {
		if (!PV.IsMine) { return; }

		currentHealth -= amount; // Taking damage

		if (currentHealth <= 0) {
			Die();
		}
	}
	
	private void Die() {
		playerManager.Die();
	}

	public void SetPlayerManager(PlayerManager pm) => playerManager = pm;

	public bool GetIsMine() => PV.IsMine;
}
