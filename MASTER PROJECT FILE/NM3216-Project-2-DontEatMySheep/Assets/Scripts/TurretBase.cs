using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour {


	public GameObject turret;
	//public Player player;

	public bool isEmpty = true;
	private bool isPlayerOnBase = false;

	void Start() {
		//player = GameObject.Find ("shepherd").GetComponent<Player> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			isPlayerOnBase = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			isPlayerOnBase = false;
		}
	}

	void Update() {
		//if (isPlayerOnBase && Input.GetKeyDown(KeyCode.E) && player.gold >= 3) {
		if (isPlayerOnBase && Input.GetKeyDown(KeyCode.E) && Player.getGold() >= 3) {
			BuildTurret ();
		}
	}

	void BuildTurret(){
		if (isEmpty) {
			Vector3 basePos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			turret = Instantiate (turret, basePos, transform.rotation);
			isEmpty = false;
			Player.UpdateGold (-3);
			//player.gold -= 3;
		}
	}
}
