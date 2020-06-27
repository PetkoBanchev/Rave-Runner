using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpawner : MonoBehaviour {

	public GameObject beer;
	public GameObject cigarettes;
	public GameObject coin;
	public GameObject discoball;
	public GameObject loudspeaker;
	public GameObject note;
	public GameObject rock;
	public GameObject table;

	GameObject prefab;
	GameObject rndPrefab;
	GameObject obstacle;
	GameObject rndObstacle;

	Vector3 spawnPosition;
	Vector3 obstacleSpawnPosition;

	SpriteRenderer prefabSpriteRenderer;

	public float minSpawnRate;
	public float minObstacleSpawnRate;

	float nextSpawn = 0f;
	float nextObstacleSpawn = 0f;

	float scaleX;
	int rndBand;
	int rndBand1;

	// Use this for initialization
	void Start () {
		scaleX = transform.localScale.x + 20;
	}
	
	// Update is called once per frame
	void Update () {         

		spawPowerUps();
		spawObstacles();

	}

	void spawPowerUps () {
		if (Time.time > nextSpawn)
		{
		
		int rndInt = Random.Range(0,5);
		switch (rndInt)
		{
			case 0:
				rndBand = 0;
				rndPrefab = beer;
				break;

			case 1:
				rndBand = 7;
				rndPrefab = cigarettes;
				break;

			case 2:
				rndBand = 2;
				rndPrefab = coin;
				break;

			case 3:
				rndBand = 3;
				rndPrefab = note;
				prefabSpriteRenderer = rndPrefab.GetComponent<SpriteRenderer>();
				prefabSpriteRenderer.color = new Color(
					Random.Range(0.5f, 1f), 
      				Random.Range(0.5f, 1f), 
      				Random.Range(0.5f, 1f));
				break;

			case 4:
				rndBand = 5;
				rndPrefab = rock;
				break;
			
		}
		spawnPosition = Camera.main.gameObject.transform.position;
		spawnPosition.x += 10;
		spawnPosition.y = 6* AudioController.audioBandsBuffer[rndBand];
		spawnPosition.z += 12;

		
		
			prefab = Instantiate(rndPrefab, spawnPosition, Quaternion.identity);
			Destroy(prefab, 2);
			PlayerMovement.score += 1;
			nextSpawn = Time.time + minSpawnRate - (AudioController.audioBandsBuffer[rndBand]);
		}
	}
	void spawObstacles () {
		if (Time.time > nextObstacleSpawn)
		{
		
		int rndInt2 = Random.Range(0,3);

		obstacleSpawnPosition = Camera.main.gameObject.transform.position;
		obstacleSpawnPosition.x += 15;
		obstacleSpawnPosition.y = 0;
		obstacleSpawnPosition.z += 12;

		switch (rndInt2)
		{
			case 0:
				rndBand1 = 1;
				rndObstacle = discoball;
				rndObstacle.transform.localScale = 
					new Vector3(transform.localScale.x + 2 + (AudioController.audioBandsBuffer[rndBand1] * 6),
					transform.localScale.y + 2 + (AudioController.audioBandsBuffer[rndBand1] * 6), 
					transform.localScale.z);

				obstacleSpawnPosition.y = 6* AudioController.audioBandsBuffer[rndBand1];
				break;

			case 1:
				rndBand1 = 4;
				rndObstacle = loudspeaker;
				rndObstacle.transform.localScale = 
					new Vector3(transform.localScale.x + 4 + (AudioController.audioBandsBuffer[rndBand1] * 6),
					transform.localScale.y + 4 + (AudioController.audioBandsBuffer[rndBand1] * 6), 
					transform.localScale.z);
				break;

			case 2:
				rndBand1 = 6;
				rndObstacle = table;
				break;

			
		}
		
			obstacle = Instantiate(rndObstacle, obstacleSpawnPosition, Quaternion.identity);
			Destroy(obstacle, 3);
			PlayerMovement.score += 2;
			nextObstacleSpawn = Time.time + minObstacleSpawnRate - (AudioController.audioBandsBuffer[rndBand1]);
		}
	}
}
