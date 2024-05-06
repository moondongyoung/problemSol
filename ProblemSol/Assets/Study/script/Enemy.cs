using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform playerTransform; // �÷��̾��� Transform�� �޾ƿ� ����

    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // ���� ��ġ���� �÷��̾� �������� ����ĳ��Ʈ�� �߻��մϴ�.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit))
        {
            // ����ĳ��Ʈ�� �÷��̾�� �浹�� ��� �÷��̾� ������ �̵��մϴ�.
            if (hit.collider.CompareTag("Player"))
            {
                Vector3 direction = playerTransform.position - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }
}