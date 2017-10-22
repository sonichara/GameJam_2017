using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	enum ENEMY_TYPE {
		UNKNOWN,
		LESSER_A,
		LESSER_B,
		BOSS,
	};

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
	private float border_left = 0;
	private float border_right = 0;
	private float border_top = 0;
	private float border_bottom = 0;

	float lastAppearBoss = 0f;
	[SerializeField]
	float BOSS_DURATION = 10f;

	private AudioSource audioSource = null;
	[SerializeField]
	private AudioClip nomalClip = null;
	[SerializeField]
	private AudioClip bossClip = null;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		SceneManeger = GameObject.Find ("SceneManeger");
		for (int i = 0; i < UI_HPs.Length; i++) {
			UI_HPs [i].GetComponent<HP_UI>().On(true);
		}
		Vector3 vec3 = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0));
		border_left = -vec3.x;
		border_right = vec3.x;
		border_top = vec3.y;
		border_bottom = -vec3.y;

		GameObject boss = GameObject.Find ("EnemyBoss");
		currentEnemies.Add (boss);

		audioSource = GetComponent<AudioSource> ();

		for (int i = 0; i < UI_HPs.Length; i++) {
			UI_HPs [i].transform.position = new Vector3 (UI_HPs [i].transform.position.x, Screen.height - 15, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		ProcessEnemy ();
		ProcessItem ();
		ProcessUI_HP ();
	}

	void ProcessEnemy() {
		List<GameObject> destroyed = new List<GameObject> ();
		foreach (GameObject enemy in currentEnemies) {
			Boss boss = enemy.GetComponent<Boss> ();
			LesserEnemyA lesserA = enemy.GetComponent<LesserEnemyA> ();
			LesserEnemyO lesserO = enemy.GetComponent<LesserEnemyO> ();
			if (boss != null) {
				if (boss.HitPoint <= 0) {
					SaveScore ();
					Destroy (enemy, 1f);
					destroyEffect.transform.position = boss.transform.position;
					destroyEffect.GetComponent<ParticleSystem> ().Play ();
					SceneManeger.GetComponent<SceneManeger> ().StartFadeOut ();
					destroyed.Add (enemy);
				}
				lastAppearBoss += Time.deltaTime;
				if (enemy.GetComponent<EnemyBoss> ().bosscommand == EnemyBoss.BossCommand.battle) {
					if (lastAppearBoss >= BOSS_DURATION) {
						audioSource.clip = nomalClip;
						audioSource.Play ();
						lastAppearBoss = 0;
						enemy.GetComponent<EnemyBoss> ().bosscommand = EnemyBoss.BossCommand.escape;
					}
				} else if (enemy.GetComponent<EnemyBoss> ().bosscommand == EnemyBoss.BossCommand.wait) {
					//lastAppearBoss += Time.deltaTime;
					if (lastAppearBoss >= BOSS_DURATION) {
						audioSource.clip = bossClip;
						audioSource.Play ();
						lastAppearBoss = 0;
						enemy.GetComponent<EnemyBoss> ().bosscommand = EnemyBoss.BossCommand.appearance;
					}
				}
			}
				
			if (lesserA != null) {
				if (lesserA.HitPoint <= 0) {
					StrikingScore++;
					Destroy (enemy, 1f);
					destroyEffect.transform.position = lesserA.transform.position;
					destroyEffect.GetComponent<ParticleSystem> ().Play ();
					destroyed.Add (enemy);
				} else if (enemy.transform.position.x < border_left - 5f) {
					Destroy (enemy);
					destroyed.Add (enemy);
				}
			}

			if (lesserO != null) {
				if (lesserO.HitPoint <= 0) {
					StrikingScore++;
					Destroy (enemy, 1f);
					destroyEffect.transform.position = lesserO.transform.position;
					destroyEffect.GetComponent<ParticleSystem> ().Play ();
					destroyed.Add (enemy);
				} else if (enemy.transform.position.x < border_left - 5f) {
					Destroy (enemy);
					destroyed.Add (enemy);
				}
			}
		}
		foreach (GameObject enemy in destroyed) {
			currentEnemies.Remove (enemy);
		}

		enemySpawnDuration += Time.deltaTime;
		if (enemySpawnDuration >= EnemySpawnDuration) {
			enemySpawnDuration = 0;
			GameObject enemy = Instantiate (enemies [Random.Range (0, enemies.Length)]);
			ENEMY_TYPE type = GetEnemyType (enemy);
			int count = GetTypeCount (type);

			switch (type) {
			case ENEMY_TYPE.LESSER_A:
				if (count >= 5) {
					Destroy (enemy);
				} else {
					enemy.transform.position = new Vector3 (Random.Range (border_right, border_right + 5f), Random.Range (border_bottom, border_top), 0f);
					currentEnemies.Add (enemy);
				}
				break;
			case ENEMY_TYPE.LESSER_B:
				if (count >= 2) {
					Destroy (enemy);
				} else {
					enemy.transform.position = new Vector3 (Random.Range (border_right - 5f , border_right - 0.5f), Random.Range (border_bottom, border_top), 0f);
					currentEnemies.Add (enemy);
				}
				break;
			case ENEMY_TYPE.BOSS:
				if (count >= 1) {
					Destroy (enemy);
				} else {
					/*
					enemy.transform.position = new Vector3 (Random.Range (border_right, border_right + 5f), Random.Range (border_bottom, border_top), 0f);
					currentEnemies.Add (enemy);
					*/
				}
				break;
			}
				
		}
	}

	void ProcessItem() {
	}

	ENEMY_TYPE GetEnemyType(GameObject enemy) {
		Boss boss = enemy.GetComponent<Boss> ();
		LesserEnemyA lesserA = enemy.GetComponent<LesserEnemyA> ();
		LesserEnemyO lesserO = enemy.GetComponent<LesserEnemyO> ();
		if (boss != null) {
			return ENEMY_TYPE.BOSS;
		}
		if (lesserA != null) {
			return ENEMY_TYPE.LESSER_A;
		}
		if (lesserO != null) {
			return ENEMY_TYPE.LESSER_B;
		}
		return ENEMY_TYPE.UNKNOWN;
	}

	int GetTypeCount(ENEMY_TYPE type) {
		int count = 0;
		foreach (GameObject enemy in currentEnemies) {
			if (type == GetEnemyType (enemy)) {
				count++;
			}
		}
		return count;
	}

	void ProcessUI_HP() {
		int hitpoint = player.GetComponent<CharacterBase> ().HitPoint;

		for (int i = 0; i < UI_HPs.Length; i++) {
			UI_HPs [i].GetComponent<HP_UI>().On(i < hitpoint ? true : false);
		}

		if (hitpoint <= 0) {
			SceneManeger.GetComponent<SceneManeger> ().StartFadeOut ();
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
		