using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour   
{
    /* �÷��̾ ��Ͽ� ���� ��, ����� �¿�(z�� ����) �Ǵ� ����(x�� ����)�� �ݺ��Ͽ� �����̰� �մϴ�. */
    /* ����� �̵��� ��, �÷��̾ ��� ������ ����� ���� �Բ� �����̵��� �մϴ�. */
    
    public GameObject EndPosition; //��� ���� ��ȯ ����
    private Vector3 startPosition; //��� �̵� ���� ����
    private GameObject Player;

    public float speed; //��� �̵� �ӵ�
    private bool isInitDir = true; //����� �̵� ���� ���� (�ʱ� �����̸� true, ��ȯ�� �����̸� false)
    private bool isPlayerOnBlock; //�÷��̾��� ��� ���� ����

    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        startPosition = transform.position; //����� �ʱ� ��ġ
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerOnBlock = true;
        Player.transform.parent = transform; //�÷��̾ �̵��ϴ� ��� ������ ��ϰ� �Բ� �����̰� ��
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerOnBlock = false;
        Player.transform.parent = null; //�÷��̾ ��ϰ� ������� �����̰� ��
    }

    void FixedUpdate()
    {
        /* ����� �̵��ϰ� �ϴ� �κ� */
        if(isPlayerOnBlock == true) //�÷��̾ ��� ���� �ִ� ��쿡�� ����
        {
            if (isInitDir) //����� �ʱ� �������� �̵��ؾ� �ϴ� ���
            {
                MoveInitialDirection();
            }
            else //����� ������ ��ȯ�Ͽ� �̵��ؾ� �ϴ� ���
            {
                MoveOppositeDirection();
            }
        }
    }

    void MoveInitialDirection()
    {
        /* ����� �ʱ� �������� �����̵��� �ϴ� �Լ� */
        if (Mathf.Abs(transform.position.x - EndPosition.transform.position.x) > 0.1f //x���� ���� �����̴� �����, �ʱ� ���� �̵� ���� ���
            || Mathf.Abs(transform.position.z - EndPosition.transform.position.z) > 0.1f) //z���� ���� �����̴� �����, �ʱ� ���� �̵� ���� ���
        {
            //EndPosition���� ��� �̵�
            transform.position = Vector3.MoveTowards(transform.position, EndPosition.transform.position, Time.deltaTime * speed);
        }
        else
        {
            isInitDir = false; //EndPosition�� �����ϸ� ���� ��ȯ
        }
    }
    void MoveOppositeDirection()
    {
        /* ����� ��ȯ�� �������� �����̵��� �ϴ� �Լ� */
        if (Mathf.Abs(transform.position.x - startPosition.x) > 0.1f //x���� ���� �����̴� �����, ��ȯ�� �������� �̵� ��
            || Mathf.Abs(transform.position.z - startPosition.z) > 0.1f) //z���� ���� �����̴� �����, ��ȯ�� �������� �̵� ��
        {
            //startPosition���� ��� �̵�
            transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * speed);
        }
        else
        {
            isInitDir = true; //startPosition�� �����ϸ� ���� ��ȯ
        }
    }
}
