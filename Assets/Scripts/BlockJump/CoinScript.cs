using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{    
    /* ������ ȸ���ϵ��� �մϴ�. */
    /* �÷��̾ ȹ���ϸ� ��������� �մϴ�. */

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(200f, 0f, 0f) * Time.deltaTime); //������ ȸ���ϵ��� ��
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false); //�÷��̾ ����(ȹ��) �� �����
    }

}
