using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class BoatSeat : MonoBehaviour
{
    [SerializeField]
    private BoatIconCanvas _iconCanvas;
    [SerializeField]
    private BoatButtonCanvas _buttonCanvas;

    private float _embarkRange = 10;
    private bool _isOccupied;

    private void Start()
    {
        Debug.Log(transform.rotation.eulerAngles.y);
        _iconCanvas = transform.GetChild(0).GetComponent<BoatIconCanvas>();
        _iconCanvas.seat = this;
        _iconCanvas.gameObject.SetActive(false);
    }

    public void OnMouseDown()
    {
        if (_isOccupied)
        {
            return;
        }

        GameObject player = PlayerManager.Players.LocalPlayerGo;

        // �̹� �迡 Ÿ�������� �ٸ� �ڸ��� �� Ž. ������ Ÿ�� ��
        if (player.transform.parent != null)
        {
            return;
        }

        // ��� �÷��̾� ������ �Ÿ��� ���� �Ÿ� �ȿ� �־�� �迡 Ż �� ����
        if (InsideEmbarkRange() == false)
        {
            return;
        }

        Embark();
    }

    public void OnMouseEnter()
    {
        if (_isOccupied)
        {
            return;
        }

        if (InsideEmbarkRange() == false)
        {
            return;
        }

        _iconCanvas.Show();
    }

    public void OnMouseExit()
    {
        if (_isOccupied)
        {
            return;
        }

        _iconCanvas.Hide();
    }

    private bool InsideEmbarkRange()
    {
        GameObject player = PlayerManager.Players.LocalPlayerGo;
        bool b = (player.transform.position - transform.position).magnitude < _embarkRange;

        return b;
    }

    private void Embark()
    {
        _isOccupied = true;
        _iconCanvas.Hide();

        GameObject player = PlayerManager.Players.LocalPlayerGo;
        player.transform.parent = gameObject.transform;
        player.transform.localPosition = Vector3.zero;
        player.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        player.GetComponent<ThirdPersonControllerMulti>().enabled = false;
        player.GetComponent<Animator>().SetBool("Grounded", true);
        player.GetComponent<Animator>().SetBool("Sitting", true);

        _buttonCanvas.gameObject.SetActive(true);
    }

    public void Disembark(Vector3 disembarkLocation)
    {
        _isOccupied = false;

        GameObject player = PlayerManager.Players.LocalPlayerGo;
        player.transform.SetParent(null);
        player.GetComponent<ThirdPersonControllerMulti>().enabled = true;
        player.GetComponent<Animator>().SetBool("Sitting", false);
        Debug.Log(player.transform.parent);
        player.transform.position = disembarkLocation;

        _buttonCanvas.gameObject.SetActive(false);
        _buttonCanvas.Hide();
    }
}
