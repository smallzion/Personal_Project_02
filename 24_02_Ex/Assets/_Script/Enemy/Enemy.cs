using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rigid;
    public float moveSpeed = 5.0f;
    public Transform playerTransform;
    Animator animator;

    public float hp = 3;

    private float Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        OnMove();
    }

    void OnMove()
    {
        if (playerTransform != null)
        {
            // 계산된 방향으로 이동
            Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
            rigid.velocity = moveSpeed * Time.deltaTime * moveDirection;

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
        if(Hp - damage > 0)
        {
            animator.SetTrigger("IsHit");
            Hp -= damage;
            Debug.Log("남은체력" + Hp);
        }
        else
        {
            Debug.Log("사망");
            OnDie();
        }
    }
    private void OnDie()
    {
        moveSpeed = 0;
        animator.SetTrigger("IsDie");
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
