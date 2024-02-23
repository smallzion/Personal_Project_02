using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifeCycle : MonoBehaviour
{
    public float lifetime = 1f; // 파티클 시스템의 수명 (초)

    private void Start()
    {
        // 수명이 다한 후 파티클 시스템을 제거하는 코루틴 시작
        StartCoroutine(DestroyAfterLifetime());
    }

    private IEnumerator DestroyAfterLifetime()
    {
        // 수명이 다한 후 파티클 시스템을 제거
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
