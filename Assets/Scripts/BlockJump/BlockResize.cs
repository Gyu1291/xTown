using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockResize : MonoBehaviour
{
    /* �÷��̾ ��Ͽ� ���� ��, ����� ���� ũ��� �پ��� �մϴ� */

    public float resizeDuration = 2f; //��� ũ�� ��� �ҿ� �ð�
    private float t; //��� �ð� ��� ���� ���� ����

    private bool isPlayerOnBlock = false; //�÷��̾��� ��� ���� ����
    private Vector3 originalScale; //����� �ʱ� ũ��
    private Vector3 targetScale; //����� ��ҵ� ũ��


    void Start()
    {
        originalScale = transform.localScale;
        targetScale = new Vector3(originalScale.x / 2f, originalScale.y, originalScale.z / 2f);
        t = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        isPlayerOnBlock = true;
    }

    void Update()
    {
        if (isPlayerOnBlock) //�÷��̾ ��Ͽ� ������ ���
        {
            ShrinkObject(); //��� ũ�� ���
            if(transform.localScale == targetScale)
            {
                //����� ���� ũ�Ⱑ �Ǹ� ��� �ߴ�
                isPlayerOnBlock = false;
            }
        }
    }

    void ShrinkObject()
    {
        /* ��� ũ�⸦ ��ҽ�Ű�� �Լ� */
        t += Time.deltaTime / resizeDuration;
        Vector3 newScale = Vector3.Lerp(originalScale, targetScale, t);
        transform.localScale = newScale;
    }
}

