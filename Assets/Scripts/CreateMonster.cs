﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour {

	private GameManager gameManager;

	public GameObject respawnSpot1;
	public GameObject respawnSpot2;
	public GameObject respawnSpot3;
	public GameObject respawnSpot4;

	public GameObject monster1Prefab;
	public GameObject monster2Prefab;

	private GameObject monsterPrefab;

	private float lastSpawnTime; // 몬스터 들이 나오는 시간
	private int spawnCount = 0;

    
	void Start() {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		monsterPrefab = monster1Prefab;
		lastSpawnTime = Time.time;

    }

    void Update() {
		if (gameManager.round <= gameManager.totalRound) { // 게임 이 완료 되기 전까지
			float timeGap = Time.time - lastSpawnTime;
			if (( ( spawnCount == 0 && timeGap > gameManager.roundReadyTime ) // 새 라운드가 시작 
				 || timeGap > gameManager.spawnTime ) 
				 && spawnCount < gameManager.spawnNumber) {

				lastSpawnTime = Time.time;
				int respawnSpotNumber = Random.Range(1,5); // (1 ~ n - 1) 까지 의 숫자를 랜덤으로 숫자 를 돌린다.

				GameObject respawnSpot = null;

				if (respawnSpotNumber == 1) {
					respawnSpot = respawnSpot1;
				}
				if (respawnSpotNumber == 2) {
					respawnSpot = respawnSpot2;
				}
				if (respawnSpotNumber == 3) {
					respawnSpot = respawnSpot3;
				}
				if (respawnSpotNumber == 4) {
					respawnSpot = respawnSpot4;
				}
				Instantiate(monsterPrefab,respawnSpot.transform.position,Quaternion.identity); // 랜덤으로 숫자를 돌린곳에 몬스터를 배치 시킴
				spawnCount += 1;
			}
			if (spawnCount == gameManager.spawnNumber &&
				GameObject.FindGameObjectWithTag("Monster") == null) { // 몬스터의 태그를 가지고 온 뒤 몬스터 가 남아있지 않는 경우

				if (gameManager.totalRound == gameManager.round) { // 게임 라운드 를 모두 마쳤을경우
					gameManager.gameClear(); // 승리의 문구 를 출력
					gameManager.round += 1;
					return;
				}
				gameManager.clearRound();
				spawnCount = 0;
				lastSpawnTime = Time.time; // 나오는 시간 을 현재시간 으로 불러옴

				if (gameManager.round == 4) {
					monsterPrefab = monster2Prefab;
					gameManager.spawnTime = 2.0f;
					gameManager.spawnNumber = 10;

				}
			}
		}         
    }
}
