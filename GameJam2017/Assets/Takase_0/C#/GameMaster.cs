using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	[SerializeField]
	private GameObject player = null;
	[SerializeField]
	private GameObject[] enemies = null;
	[SerializeField]
	private GameObject[] UI_HPs = null;
	[SerializeField]
	private GameObject UI_Time = null;
	[SerializeField]
	private GameObject destroyEffect = null;
	[SerializeField]
	float EnemySpawnDuration = 5f;
	[SerializeField]
	private GameObject ShowScore = null;


	private GameObject SceneManeger = null;
	private List<GameObject> currentEnemies = new List<GameObject>();
	private List<GameObject> currentItems = new List<GameObject>();
	private float currentTime = 0;
	private int StrikingScore = 0;
	private float enemySpawnDuration = 0f;


	// Use this for initialization
	void Start () {
		SceneManeger = GameObject.Find ("SceneManeger");
	}
	
	// Update is called once per frame
	void Update () {
		ProcessEnemy ();
		ProcessItem ();
	}

	void ProcessEnemy() {
		foreach (GameObject enemy in currentEnemies) {
			Boss boss = enemy.GetComponent<Boss> ();
			if (boss != null && boss.HitPoint <= 0) {
				SaveScore ();
				Destroy (boss, 1f);

				destroyEffect.transform.position = boss.transform.position;
				destroyEffect.GetComponent<ParticleSystem> ().Play ();

				SceneManeger.GetComponent<SceneManeger> ().StartFadeOut ();
			}

			LesserEnemyBase lesser = enemy.GetComponent<LesserEnemyBase> ();
			if (lesser != null && lesser.HitPoint <= 0) {
				StrikingScore++;
				Destroy (lesser, 1f);
				destroyEffect.transform.position = lesser.transform.position;
				destroyEffect.GetComponent<ParticleSystem> ().Play ();
			}
		}

		enemySpawnDuration += Time.deltaTime;
		if (enemySpawnDuration >= EnemySpawnDuration) {
			enemySpawnDuration = 0;
			GameObject enemy = Instantiate (enemies [Random.Range (0, enemies.Length - 1)]);
			enemy.transform.position = new Vector3 (Random.Range(10f, 20f), Random.Range(-10f, 10f), 0f);

			currentEnemies.Add (enemy);
		}

	}

	void ProcessItem() {
		
	}

	void ProcessUI_HP() {
		int hitpoint = player.GetComponent<CharacterBase> ().HitPoint - 1;

		for (int i = 0; i < UI_HPs.Length; i++) {
			UI_HPs [i].SetActive (i < hitpoint ? true : false);
		}
	}

	void ProcessUI_TIME() {
		currentTime += Time.deltaTime;
		
	}
		
	void ShowClear() {
		//ShowScore.GetComponent<ClearUI> ().show ();
	}

	void SaveScore() {
		string data = 
			"timestamp:" + Time.time + 
			"clear_time:" + currentTime + 
			"StrikingScore:" + StrikingScore;
		//PlayerPrefs.SetFloat ("score_data", data);
	}
}
		