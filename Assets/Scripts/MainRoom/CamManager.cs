using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public bool IsCurrentFp = false;

    private GameObject _player;

    public GameObject FirstPersonCamObj;
    public GameObject ThirdPersonCamObj;

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
        {
            //_player = GameObject.FindWithTag("Player");
            if (!PhotonNetwork.InRoom) return;
            else if (!PhotonNetwork.CurrentRoom.Name.Contains("MainWorld")) return;

            _player = PlayerManager.Players.LocalPlayerGo;
            FirstPersonCamObj = _player.transform.Find("FirstPersonCam").gameObject;
        }
        else
        {
            CamChange();
        }
    }

    private void CamChange()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            IsCurrentFp = !IsCurrentFp;
            FirstPersonCamObj.SetActive(IsCurrentFp);

            ThirdPersonCamObj.SetActive(!IsCurrentFp);
        }
    }
}
