using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rigid;
    public float moveSpeed = 5.0f;
    private Animator animator;
    private Transform endLine;
    private float currentSpeed = 0;
    public float hp = 3;
    GameManager gameManager;

    private float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    // EnemySpawner 인스턴스 참조
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        GameObject endLineObject = GameObject.FindGameObjectWithTag("EndLine");
        if (endLineObject != null)
        {
            endLine = endLineObject.transform;
        }
        else
        {
            Debug.LogError("플레이어를 찾을 수 없습니다!");
        }
        currentSpeed = moveSpeed;

        // EnemySpawner 인스턴스 참조
        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner를 찾을 수 없습니다!");
        }
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
        OnMove();
    }

    void OnMove()
    {
        if (endLine != null)
        {
            // 플레이어를 향하는 방향 벡터 계산
            Vector3 moveDirection = (new Vector3(0 ,0, endLine.position.z) - new Vector3(0, 0, transform.position.z)).normalized;

            // 이동
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // 플레이어를 바라보는 방향으로 회전
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);

            // 애니메이션 설정
            animator.SetBool("IsMove", true);
        }
        else
        {
            // 플레이어가 없으면 정지 상태로 애니메이션 설정
            animator.SetBool("IsMove", false);
        }
    }

    public void OnDamage(int damage)
    {
        Debug.Log("함수 호출됨");
        if (Hp - damage > 0)
        {
            animator.SetTrigger("IsHit");
            Hp -= damage;
            moveSpeed = -1;
            Debug.Log("남은체력" + Hp);
            StartCoroutine(MoveStop());
        }
        else
        {
            Debug.Log("사망");
            OnDie();
        }
    }

    public void RestoreSpeed()
    {
        moveSpeed = currentSpeed;
    }

    IEnumerator MoveStop()
    {
        float timer = 0.0f;
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // 애니메이션이 끝날 때까지 대기
        while (timer < animationLength)
        {
            timer += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 애니메이션이 끝나면 이동 속도를 복구
        RestoreSpeed();
    }

    private void OnDie()
    {
        // EnemySpawner 인스턴스를 통해 DecreaseEnemyCount 메소드 호출
        if (enemySpawner != null)
        {
            enemySpawner.DecreaseEnemyCount();
        }

        moveSpeed = 0;
        gameManager.count++;
        animator.SetTrigger("IsDie");
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndLine"))
        {
            other.GetComponent<EndLine>().OnDamage();
            OnDie();
        }
    }
}
