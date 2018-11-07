using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	[Header("Spawn Locations")]
	public RectTransform northSpawn;
	public RectTransform eastSpawn;
	public RectTransform southSpawn;
	public RectTransform westSpawn;

	[Header("Timing")]
	public float secondsBetweenEnemy = 0.5f;
	public float secondsBetweenSpawn = 2.0f;

	[Header("Waves")]
	public List<int> wolf = new List<int> ();
	public List<int> whiteWolf = new List<int> ();
	public List<int> shadowWolf = new List<int> ();
	public List<int> demonWolf = new List<int> ();


	[Header("For Programmer")]
	public List<GameObject> typePrefabs = new List<GameObject> ();
	//different types of enemy to be initiated 
	public GameObject WaveL1;
	public GameObject EnemyL2;
	public GameObject EnemyL3;

	//keeps count on the current wavenumber
	private int waveNumber = -1;

	bool enemiesDead = true;
	private int numEnemies = 0;
	bool shouldSpawnWave = true;

	private List<int> numEnemiesInWave = new List<int> ();
	private List<int> typeOfEnemy = new List<int> ();

	void Start () {
		StartCoroutine ("Spawn");

	}

	void Update () {
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length == 0) {
			enemiesDead = true;
		}
	}

	void SpawnEnemy(GameObject enemy){
		int startPoint = Random.Range (1, 5);
		float minX = 0;
		float maxX = 0;
		float minY = 0;
		float maxY = 0;

		if (startPoint == 1) {
			minX = northSpawn.position.x;
			maxX = minX + northSpawn.rect.width;
			minY = northSpawn.position.y;
			maxY = minY;
		}
		else if (startPoint == 2) {
			minY = eastSpawn.position.y;
			maxY = minY + eastSpawn.rect.height;
			minX = eastSpawn.position.x;
			maxX = minX;
			
		}
		else if (startPoint == 3) {
			minX = southSpawn.position.x;
			maxX = minX + southSpawn.rect.width;
			minY = southSpawn.position.y;
			maxY = minY;
		}
		else if (startPoint == 4) {
			minY = westSpawn.position.y;
			maxY = minY + westSpawn.rect.height;
			minX = westSpawn.position.x;
			maxX = minX;
		}

		Instantiate (enemy, new Vector2 (Random.Range (minX, maxX), Random.Range (minY, maxY)), transform.rotation);
	}
	IEnumerator SpawnWave(){

		waveNumber++;
		shouldSpawnWave = false;

		int enemyCounter = 0;
		int index = 0;

		numEnemiesInWave = new List<int> ();
		typeOfEnemy = new List<int> ();

		numEnemiesInWave.Add (wolf [waveNumber]);
		numEnemiesInWave.Add (whiteWolf [waveNumber]);
		numEnemiesInWave.Add (shadowWolf [waveNumber]);
		numEnemiesInWave.Add (demonWolf [waveNumber]);

		numEnemies = 0;
		for (int i = 0; i < numEnemiesInWave.Count; i++) {
			numEnemies += numEnemiesInWave [i];
		}

		for (int i = 0; i < 4; i++) {
			typeOfEnemy.Add (i);
		}

		for (int i = 0; i < 4; i++) {
			if (numEnemiesInWave[i] <= 0) {
				typeOfEnemy.Remove (i);
			}
		}

		while (numEnemies > 0) {
			int typeSelector = typeOfEnemy[Random.Range (0, typeOfEnemy.Count)];

			SpawnEnemy (typePrefabs [typeSelector]);
			numEnemies--;
			numEnemiesInWave [typeSelector] -= 1;
			if (numEnemiesInWave [typeSelector] == 0) {
				typeOfEnemy.Remove (typeSelector);
			}

			yield return new WaitForSeconds (secondsBetweenEnemy);
		}

		StopCoroutine ("SpawnWave");
		shouldSpawnWave = true;





//		int startPoint = Random.Range (1, 6);
//
//		switch (startPoint){
//
//		case 1:
		//if (waveNumber == 0) {
			//delay (5);
		//}

		//else {
			//delay(2);
//		if (waveNumber < 5) {
//			enemyCounter = Wave1.Count;
//		}
//
//		while (enemyCounter >= 0) {
//			int startPoint = Random.Range (1, 5);
//
//			Debug.Log (startPoint);
//			if (startPoint == 1) {
//				Instantiate (Wave1[index], startEast, transform.rotation);
//			}
//			if (startPoint == 2) {
//				Instantiate (Wave1[index], startSouth, transform.rotation);
//			}
//			if (startPoint == 3) {
//				Instantiate (Wave1[index], startWest, transform.rotation);
//			}
//			if (startPoint == 4) {
//				Instantiate (Wave1[index], new Vector2(Random.Range(startNorth.x,endNorth.x), startNorth.y), transform.rotation);
//			}
//			Debug.Log ("index:" + index);
//			Debug.Log ("Num of enemies left:" + enemyCounter);
//			index++;
//			enemyCounter--;
//		}
		//}

	}

	IEnumerator Spawn(){
		while (true) {
			//Debug.Log (enemiesDead);
			if (waveNumber == 0)
				yield return new WaitForSeconds (0);
			if (waveNumber != 0)
				yield return new WaitForSeconds (1);

			if (enemiesDead && numEnemies == 0 && shouldSpawnWave) {
				enemiesDead = false;
				shouldSpawnWave = false;
				yield return new WaitForSeconds (secondsBetweenSpawn);
				StartCoroutine ("SpawnWave");
				Debug.Log ("Next Wave Imminent!");
			}
		}
	}
}
