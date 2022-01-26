using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JK
{
    public class CueScript : MonoBehaviour
    {
        Vector3 cueDirection;
        Vector3 offsetPos = new Vector3(0.2f, -0.7f, -0.2f); //ť�� �ʱ� ��ġ ������
        public static Vector3 cuePosition = Vector3.zero;

        public GameObject whiteBall;
        public static Rigidbody rb;
        public static bool isCueMoving = false; //true �߿��� ť�� ��ġ�� �ʱ�ȭ���� �ʵ��� �ϴ� ����

        // Start is called before the first frame update
        void Start()
        {
            whiteBall = GameObject.Find("Ball_0");
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            cuePosition = transform.position;

            if (GameManager.isBallStop.Sum() == 16 && isCueMoving == false) // ��� ���� ������ ���� ��� & ť�� �������� ���� ��
            {
                //ť�� ��ġ �ʱ�ȭ
                transform.position = Camera.main.transform.position + offsetPos; //ȭ�鿡 ������ ���̵��� ��ġ
                //transform.rotation = Camera.main.transform.localRotation;
                
                transform.LookAt(whiteBall.transform); //�� ���� �ٶ󺸰� �Ѵ�
                transform.rotation = Quaternion.LookRotation(transform.position - whiteBall.transform.position ); //ť�� ������ �ݴ뿩�� ����
                //transform.rotation = Quaternion.Euler(0, -180, 0);
            }

            if (CueScript.isCueMoving == true) //ť�� �̵�
            {
                //transform.Translate(-BallMovement.ballDirection * Time.deltaTime * BallMovement.power);
                transform.Translate(-BallMovement.ballDirection * Time.deltaTime * BallMovement.power * 0.03f);
            }

        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject == whiteBall)
            {
                isCueMoving = false;
            }
        }


    }
}
