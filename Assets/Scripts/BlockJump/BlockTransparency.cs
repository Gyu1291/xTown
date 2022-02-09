using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTransparency : MonoBehaviour
{
    /* �÷��̾ ��Ͽ� ���� ��, ����� ���������� �մϴ�. */
    /* �÷��̾ �������� ������ �ٽ� �������ϰ� ���ƿɴϴ�. */

    Material Mat;
    public float fadeSpeed; //��� ����ȭ �ӵ�
    private bool isPlayerOnBlock = false; //�÷��̾��� ��� ���� ����

    void Start()
    {
        Mat = GetComponent<Renderer>().material;
        if(fadeSpeed == 0)
        {
            fadeSpeed = 0.01f; //����ȭ �ӵ� �⺻��
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isPlayerOnBlock = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isPlayerOnBlock = false; //�÷��̾ ��Ͽ� ���� ���� �ƴ� ���,
        StopAllCoroutines(); //����ȭ�� �ߴ��ϰ�,
        StartCoroutine(FadeInObject()); //�ٽ� ������ȭ
    }

    void FixedUpdate()
    {
        if (isPlayerOnBlock == true)
        {
            //�÷��̾ ��� ���� ��, ��� ����ȭ
            StartCoroutine(FadeOutObject());
        }
    }

    IEnumerator FadeOutObject()
    {
        /* ��� ����ȭ�ϴ� �Լ� */
        while (Mat.color.a > 0.01f) //����� �������� ���
        {
            //��� ���İ� ����
            Color ObjectColor = Mat.color;
            float fadeAmount = ObjectColor.a - (fadeSpeed * Time.deltaTime);

            //���ҵ� ���İ����� ��� �� �缳��
            ObjectColor = new Color(ObjectColor.r, ObjectColor.g, ObjectColor.b, fadeAmount);
            Mat.color = ObjectColor;
            
            yield return null;
        }
        if (Mat.color.a <= 0.01f) //����� �������� ���
        {
            //��� �ݶ��̴��� ��Ȱ��ȭ�Ͽ� �÷��̾ �߶��ϰ� ��
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator FadeInObject()
    {
        /* ����� �ٽ� ������������ �ϴ� �Լ� */
        gameObject.GetComponent<BoxCollider>().enabled = true;
        while (Mat.color.a <= 0.5f) //�ʱ� �������� ���ƿ��� ������
        {
            // ��� ���İ� ����
            Color objectColor = Mat.color;
            float fadeAmount = objectColor.a + (Time.deltaTime);
            //������ ���İ����� ��� �� �缳��
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            Mat.color = objectColor;

            yield return null;
        }
    }
}
