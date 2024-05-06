using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Transform playerTransform; // 플레이어의 Transform을 받아올 변수

    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // 적의 위치에서 플레이어 시점으로 레이캐스트를 발사합니다.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit))
        {
            // 레이캐스트가 플레이어와 충돌한 경우 플레이어 쪽으로 이동합니다.
            if (hit.collider.CompareTag("Player"))
            {
                Vector3 direction = playerTransform.position - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }
}