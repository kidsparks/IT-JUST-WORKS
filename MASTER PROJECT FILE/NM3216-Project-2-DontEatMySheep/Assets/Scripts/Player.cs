using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float moveSpeed = 1;
	public float rotateSpeed = 12;
	public float playerDamage = 10;
	public static int gold; // why cant i serialize this?
	public int initialGold = 0;
    public Animator animator;

	//Instantiatingf popups
	[SerializeField]
	private Image UIPopup;

	private Image instance;
	private GameObject canvas;

	private bool shouldDestroy = false;

	//Turret Upgrading
	[SerializeField]
	private int currentTurretLvl;

	private bool isPlayerNearTurret = false;

	[Header("Turret Level Prefabs")]
	public GameObject turretL1;
	public GameObject turretL2;
	public GameObject turretL3;
	public GameObject turretL4;

	private GameObject toDestroy;

	[Header("Gold Cost")]
	public int l1Cost = 5;
	public int l2Cost = 7;
	public int l3Cost = 9;
	public int l4Cost = 10;

	void Awake(){
		gold = initialGold;
		canvas = GameObject.Find ("Canvas");
	}
	//	void Start(){
	//		StartCoroutine (DestroyPopUps(instance));
	//	}

	public static void UpdateGold(int newGold){
		gold += newGold;	
	}

	public static int getGold() {
		return gold;
	}

	// Update is called once per frame
	void Update () {
		//float moveVector = Input.GetAxis ("Vertical");
		//float rotateVector = Input.GetAxis ("Horizontal");

		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 3.0f;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		//transform.Rotate(0, x, 0);
		transform.Translate (x, z, 0);
		initialGold = gold;

		if (isPlayerNearTurret && Input.GetKeyDown(KeyCode.E) && Player.getGold() >= 5) {
			//Destroy (toDestroy);
			if(toDestroy != null)
				UpgradeTurret ();
		}
	}
	//this.transform.Translate (0f, moveVector * moveSpeed * Time.deltaTime, 0f);

	//this.transform.Rotate (0f, 0f, rotateVector * (rotateSpeed * 10) * Time.deltaTime);
	IEnumerator DestroyPopUps (Image target){
		Debug.Log ("COROTUINE STARTED");
		yield return new WaitForSeconds (1);

		if (shouldDestroy) {
			Destroy (target);
		}
	}
	void OnTriggerEnter2D (Collider2D target) {
		if (target.gameObject.tag == "TurretL1" || target.gameObject.tag == "TurretBase" || target.gameObject.tag == "TurretL2" || target.gameObject.tag == "TurretL2") {
			//			Debug.Log ("Interact POPUP");
			//			GameObject item = Instantiate (Pop);
			//			Vector2 playerPos = new Vector2 (transform.position.x, transform.position.y);
			//
			//			item.transform.SetParent (transform, false);
			//			item.transform.position = playerPos;

			if (instance == null)
				CreatePopUp ();

			shouldDestroy = true;
			StartCoroutine (DestroyPopUps(instance));
			StopCoroutine (DestroyPopUps (instance));

			toDestroy = target.gameObject;
			if (target.gameObject.tag == "TurretL1") {
				isPlayerNearTurret = true;
				currentTurretLvl = 1;
			}
			else if (target.gameObject.tag == "TurretL2") {
				isPlayerNearTurret = true;
				currentTurretLvl = 2;
			}
			else if (target.gameObject.tag == "TurretL3") {
				isPlayerNearTurret = true;
				currentTurretLvl = 3;
			}
		}
	}

	void OnTriggerExit2D (Collider2D target) {
		if (target.gameObject.tag == "Turret") {
			isPlayerNearTurret = false;
		}
	}

	void Attack(GameObject target){
		target.transform.GetComponent<EnemyL1> ().TakeDamage (playerDamage);
	}
	void CreatePopUp(){
		instance = Instantiate (UIPopup);
		Vector2 playerPos = new Vector2 (transform.position.x, transform.position.y);

		instance.transform.SetParent (canvas.transform, false);
		instance.transform.position = playerPos;
	}

	void UpgradeTurret(){

		Vector3 basePos = new Vector3 (toDestroy.GetComponent<Turret> ().transform.position.x, toDestroy.GetComponent<Turret> ().transform.position.y, toDestroy.GetComponent<Turret> ().transform.position.z);
		GameObject turretToBuild = toDestroy;
		int goldCost = 0;

		if (currentTurretLvl == 1) {
			turretToBuild = turretL2;
			goldCost = l2Cost;

		}
		else if (currentTurretLvl == 2) {
			turretToBuild = turretL3;
			goldCost = l3Cost;
		}
		else if (currentTurretLvl == 3) {
			turretToBuild = turretL4;
			goldCost = l4Cost;
		}

		Destroy (toDestroy);
		turretToBuild = Instantiate (turretToBuild, basePos, transform.rotation);

		Player.UpdateGold (goldCost);
	}
}
//	
