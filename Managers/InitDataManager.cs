using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitDataManager : MonoBehaviour
{
    enum InitState
    {
        Sound,
        Data,
        end
    }

    InitState initState = InitState.Sound;

    bool soundInitOK = false;

    private void Start() => StartFunc();

    private void StartFunc()
    {

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        switch(initState)
        {
            case InitState.Sound:
                SoundInit();
                break;

            case InitState.Data:
                DataInit();
                break;

            case InitState.end:
                UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
                break;
        }
    }

    public void SoundInit()
    {
        if(!soundInitOK)
        {
            soundInitOK = true;
            StartCoroutine(SoundInitCo());
        }
    }

    IEnumerator SoundInitCo()
    {
        GlobalData.errorBeepClip = Resources.Load("Sounds/ErrorBeep") as AudioClip;
        GlobalData.buySound = Resources.Load("Sounds/BuySound") as AudioClip;
        GlobalData.lobbyBGM = Resources.Load("Sounds/BGM/LobbyBGM") as AudioClip;
        GlobalData.inGameBGM = Resources.Load("Sounds/BGM/InGameBGM") as AudioClip;
        GlobalData.btnClickSound = Resources.Load("Sounds/ClickSound") as AudioClip;
        GlobalData.chatSound = Resources.Load("Sounds/ChatTyping") as AudioClip;
        initState++;
        yield break;
    }

    void DataInit()
    {
        initState++;
    }
}