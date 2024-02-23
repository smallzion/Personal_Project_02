using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 생성할 적 프리팹
    public Transform[] spawnPoints; // 적이 생성될 위치 배열
    public float spawnInterval = 3f; // 생성 간격 (초)
    public int maxEnemies = 10; // 최대 생성할 적의 수
    private int currentEnemyCount = 0; // 현재 생성된 적의 수

    private void Start()
    {
        // 주기적으로 적을 생성하는 코루틴 시작
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            // 최대 적 수 체크
            if (currentEnemyCount < maxEnemies)
            {
                // 적 생성 위치 랜덤 선택
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[randomIndex];

                // 적 생성
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                currentEnemyCount++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }
}

