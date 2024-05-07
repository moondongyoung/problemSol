using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public RandomObjectGenerator rg;
    Vector3 maxEnemyPosition; // 적의 최대 이동 위치
    public float moveSpeed = 3f; // 이동 속도

    private bool isMoving = false;

    // 최대 이동 포지션 값을 설정하는 함수
    void Start()
    {
        // EnemyGen 게임 오브젝트를 찾아서 rg 변수에 할당합니다.
        rg = GameObject.Find("EnemyGen").GetComponent<RandomObjectGenerator>();
        StartEnemyMovement();
    }
    public void SetMaxEnemyPosition(Vector3 maxPosition)
    {
        maxPosition = rg.randomPosition;
        maxEnemyPosition = maxPosition;
    }

    // 랜덤한 방향으로 이동하는 코루틴 함수
    IEnumerator MoveRandomDirection()
    {
        isMoving = true;

        while (isMoving)
        {
            // 랜덤한 방향을 설정합니다.
            Vector3 randomDirection = Random.insideUnitSphere.normalized;

            // 이동 벡터를 계산합니다.
            Vector3 moveVector = randomDirection * moveSpeed * Time.deltaTime;

            // 다음 위치를 계산합니다.
            Vector3 nextPosition = transform.position + moveVector;

            // 최대 이동 포지션 값 nextPosition.x = Mathf.Clamp(nextPosition.x, -maxEnemyPosition.x, maxEnemyPosition.x);
            nextPosition.y = Mathf.Clamp(nextPosition.y, -maxEnemyPosition.y, maxEnemyPosition.y);
            nextPosition.z = Mathf.Clamp(nextPosition.z, -maxEnemyPosition.z, maxEnemyPosition.z);

            // 적을 이동합니다.
            transform.position = nextPosition;

            yield return null;
        }
    }

    // 적을 시작 위치로 초기화하는 함수
    public void ResetEnemyPosition()
    {
        transform.position = Vector3.zero;
    }

    // 적을 이동시키는 함수
    public void StartEnemyMovement()
    {
        StartCoroutine(MoveRandomDirection());
    }

    // 적의 이동을 멈추는 함수
    public void StopEnemyMovement()
    {
        isMoving = false;
    }
}
