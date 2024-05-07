using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    public float rotationAmount = 90f; // 회전할 각도
    public float rotationDuration = 1f; // 회전하는 데 걸리는 시간

    private bool isRotating = false; // 회전 중인지 여부
    private float rotationTimer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A와 D 키 입력
        float verticalInput = Input.GetAxis("Vertical"); // W와 S 키 입력

        // 이동 방향 설정
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 플레이어 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (!isRotating)
        {
            // 'o' 키를 눌렀을 때
            if (Input.GetKeyDown(KeyCode.O))
            {
                StartRotation(-rotationAmount);
            }

            // 'p' 키를 눌렀을 때
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartRotation(rotationAmount);
            }
        }
    }
    private void StartRotation(float targetRotation)
    {
        isRotating = true; // 회전 중임을 설정
        rotationTimer = 0f; // 회전 타이머 초기화

        Quaternion startRotation = transform.rotation; // 현재 회전값
        Quaternion targetRotationQuat = Quaternion.Euler(0f, targetRotation, 0f) * startRotation; // 목표 회전값

        // 회전 애니메이션 시작 (코루틴 호출)
        StartCoroutine(RotateOverTime(startRotation, targetRotationQuat));
    }

    // 회전 애니메이션 코루틴
    private IEnumerator RotateOverTime(Quaternion startRotation, Quaternion targetRotation)
    {
        while (rotationTimer < rotationDuration)
        {
            // 회전 타이머 업데이트
            rotationTimer += Time.deltaTime;

            // 회전 진행도 계산 (0 ~ 1)
            float t = Mathf.Clamp01(rotationTimer / rotationDuration);

            // 회전 수행
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null; // 한 프레임 기다림
        }

        isRotating = false; // 회전 완료 후 회전 중이 아님으로 설정
    }
}
