using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	int damage;
    int speed;

	Vector3 prevPos;

	public void SetUp(int damage, int speed) {
		this.damage = damage;
		this.speed = speed;
	}

	void Start() {
		prevPos = transform.position;
	}

    void Update() {
		prevPos = transform.position;
		Move(); // MOve after we set the new position
        RaycastHit2D[] hits = Physics2D.RaycastAll(prevPos, (transform.position - prevPos).normalized, (transform.position - prevPos).magnitude);
		for (int i = 0; i < hits.Length; i++) {
			Hit(hits[i].collider.gameObject);
		}
    }

	private void Hit(GameObject go) {
		go.GetComponent<PlayerController>()?.TakeDamge(damage);
		Destroy(gameObject);
	}

	private void Move() {
		transform.Translate(transform.up * speed);
	}
}
