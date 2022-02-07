using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class VoiceChatChannelsPopup : UIPopup
{

    List<PlayerInfo> _playerInfos;
    enum Buttons
    {
        CloseButton
    }

    [SerializeField]
    Transform _content;


    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        // to get a bound gameObject, use GetUIComponent and provide it with UIElementType and UIElementId.
        GetUIComponent<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnClick_Close);
        _playerInfos = RoomManager.Room.GetPlayerInfoList();
        foreach(PlayerInfo info in _playerInfos)
        {
            Debug.Log($"player: {info.PlayerName}");
        }

        _playerInfos.ForEach((info) => {
            Debug.Log($"adding to list: {info.PlayerName}");
            AddItem(info.PlayerName); 
        
        });
    }

    void AddItem(string name)
    {
        AudioChatUserListing listing = Instantiate(Resources.Load<AudioChatUserListing>("UI/Popup/VoiceChatRoom/PlayerListItem"), _content);
        listing.PlayerNameText = name;
    }


    void ClearList()
    {
        while(_content.childCount > 0)
        {
            AudioChatUserListing listing = GetComponentInChildren<AudioChatUserListing>(_content);
            listing.Remove();
        }
    }

    public void OnPlayerJoined_AddItem(PlayerInfo info)
    {
        Debug.Log($"Player Joined {info.PlayerName}");
    }

    public void OnPlayerLeft_RemoveItem(PlayerInfo info)
    {
        Debug.Log($"Player Left {info.PlayerName}");
    }

    public void OnClick_Close(PointerEventData data)
    {
        ClearList();
        ClosePopup();
    }
}

