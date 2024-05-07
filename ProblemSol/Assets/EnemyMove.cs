using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public RandomObjectGenerator rg;
    Vector3 maxEnemyPosition; // ���� �ִ� �̵� ��ġ
    public float moveSpeed = 3f; // �̵� �ӵ�

    private bool isMoving = false;

    // �ִ� �̵� ������ ���� �����ϴ� �Լ�
    void Start()
    {
        // EnemyGen ���� ������Ʈ�� ã�Ƽ� rg ������ �Ҵ��մϴ�.
        rg = GameObject.Find("EnemyGen").GetComponent<RandomObjectGenerator>();
        StartEnemyMovement();
    }
    public void SetMaxEnemyPosition(Vector3 maxPosition)
    {
        maxPosition = rg.randomPosition;
        maxEnemyPosition = maxPosition;
    }

    // ������ �������� �̵��ϴ� �ڷ�ƾ �Լ�
    IEnumerator MoveRandomDirection()
    {
        isMoving = true;

        while (isMoving)
        {
            // ������ ������ �����մϴ�.
            Vector3 randomDirection = Random.insideUnitSphere.normalized;

            // �̵� ���͸� ����մϴ�.
            Vector3 moveVector = randomDirection * moveSpeed * Time.deltaTime;

            // ���� ��ġ�� ����մϴ�.
            Vector3 nextPosition = transform.position + moveVector;

            // �ִ� �̵� ������ �� nextPosition.x = Mathf.Clamp(nextPosition.x, -maxEnemyPosition.x, maxEnemyPosition.x);
            nextPosition.y = Mathf.Clamp(nextPosition.y, -maxEnemyPosition.y, maxEnemyPosition.y);
            nextPosition.z = Mathf.Clamp(nextPosition.z, -maxEnemyPosition.z, maxEnemyPosition.z);

            // ���� �̵��մϴ�.
            transform.position = nextPosition;

            yield return null;
        }
    }

    // ���� ���� ��ġ�� �ʱ�ȭ�ϴ� �Լ�
    public void ResetEnemyPosition()
    {
        transform.position = Vector3.zero;
    }

    // ���� �̵���Ű�� �Լ�
    public void StartEnemyMovement()
    {
        StartCoroutine(MoveRandomDirection());
    }

    // ���� �̵��� ���ߴ� �Լ�
    public void StopEnemyMovement()
    {
        isMoving = false;
    }
}
