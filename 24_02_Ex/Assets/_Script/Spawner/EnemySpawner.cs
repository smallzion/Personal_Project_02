/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 생성할 적 프리팹
    public Transform[] spawnPoints; // 적이 생성될 위치 배열
    public float spawnInterval = 3f; // 생성 간격 (초)
    public int maxEnemies = 10; // 최대 생성할 적의 수
    private int currentEnemyCount = 0; // 현재 생성된 적의 수
    public float reactiveSpawnPoint = 10.0f;
    int indexSpawnPoint;
    Collider[] spawnPointsCollider;
    private void Start()
    {
        // 주기적으로 적을 생성하는 코루틴 시작
        StartCoroutine(SpawnEnemyRoutine());
    }

    public void Awake()
    {
        spawnPointsCollider = new Collider[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointsCollider[i] = spawnPoints[i].GetComponent<Collider>();
        }
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
    public int GetSpawnPointIndex(int index)
    {
        return index;
    }
    public void DisableSpawnPoints(int index)
    {
          StartCoroutine (ReactivateSpawnPoints(index));
    }

    IEnumerator ReactivateSpawnPoints(int index)
    {
        spawnPoints[index].gameObject.SetActive(false);
        yield return new WaitForSeconds(reactiveSpawnPoint);
        spawnPoints[index].gameObject.SetActive(true);
    }
}
*/
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
    public float reactiveSpawnPoint = 10.0f;
    Collider[] spawnPointsCollider;
    private void Start()
    {
        // 주기적으로 적을 생성하는 코루틴 시작
        StartCoroutine(SpawnEnemyRoutine());
    }

    public void Awake()
    {
        spawnPointsCollider = new Collider[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointsCollider[i] = spawnPoints[i].GetComponent<Collider>();
        }
    }
    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            // 최대 적 수 체크
            if (currentEnemyCount < maxEnemies)
            {
                // 적 생성 위치 랜덤 선택
                int randomIndex = GetRandomSpawnPointIndex();
                Transform spawnPoint = spawnPoints[randomIndex];

                // 적 생성
                if (spawnPoint.gameObject.activeSelf) // 활성화된 스폰 포인트에서만 생성
                {
                    GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                    currentEnemyCount++;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private int GetRandomSpawnPointIndex()
    {
        List<int> activeSpawnIndices = new List<int>();

        // 활성화된 스폰 포인트의 인덱스만 모으기
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].gameObject.activeSelf)
            {
                activeSpawnIndices.Add(i);
            }
        }

        // 활성화된 스폰 포인트 중에서 랜덤으로 선택
        if (activeSpawnIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, activeSpawnIndices.Count);
            return activeSpawnIndices[randomIndex];
        }
        else
        {
            // 모든 스폰 포인트가 비활성화된 경우 첫 번째 스폰 포인트를 반환
            return 0;
        }
    }

    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }

    public void DisableSpawnPoints(int index)
    {
        StartCoroutine(ReactivateSpawnPoints(index));
    }

    IEnumerator ReactivateSpawnPoints(int index)
    {
        spawnPoints[index].gameObject.SetActive(false);
        yield return new WaitForSeconds(reactiveSpawnPoint);
        spawnPoints[index].gameObject.SetActive(true);
    }
}
