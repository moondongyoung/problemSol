using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    public float rotationAmount = 90f; // ȸ���� ����
    public float rotationDuration = 1f; // ȸ���ϴ� �� �ɸ��� �ð�

    private bool isRotating = false; // ȸ�� ������ ����
    private float rotationTimer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A�� D Ű �Է�
        float verticalInput = Input.GetAxis("Vertical"); // W�� S Ű �Է�

        // �̵� ���� ����
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // �÷��̾� �̵�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (!isRotating)
        {
            // 'o' Ű�� ������ ��
            if (Input.GetKeyDown(KeyCode.O))
            {
                StartRotation(-rotationAmount);
            }

            // 'p' Ű�� ������ ��
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartRotation(rotationAmount);
            }
        }
    }
    private void StartRotation(float targetRotation)
    {
        isRotating = true; // ȸ�� ������ ����
        rotationTimer = 0f; // ȸ�� Ÿ�̸� �ʱ�ȭ

        Quaternion startRotation = transform.rotation; // ���� ȸ����
        Quaternion targetRotationQuat = Quaternion.Euler(0f, targetRotation, 0f) * startRotation; // ��ǥ ȸ����

        // ȸ�� �ִϸ��̼� ���� (�ڷ�ƾ ȣ��)
        StartCoroutine(RotateOverTime(startRotation, targetRotationQuat));
    }

    // ȸ�� �ִϸ��̼� �ڷ�ƾ
    private IEnumerator RotateOverTime(Quaternion startRotation, Quaternion targetRotation)
    {
        while (rotationTimer < rotationDuration)
        {
            // ȸ�� Ÿ�̸� ������Ʈ
            rotationTimer += Time.deltaTime;

            // ȸ�� ���൵ ��� (0 ~ 1)
            float t = Mathf.Clamp01(rotationTimer / rotationDuration);

            // ȸ�� ����
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null; // �� ������ ��ٸ�
        }

        isRotating = false; // ȸ�� �Ϸ� �� ȸ�� ���� �ƴ����� ����
    }
}
