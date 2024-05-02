using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Button startBtn;
    public Button settingBtn;
    public Button ExitBtn;
    private void Start() => StartFunc();

    private void StartFunc()
    {
        SoundManager.instance.PlayBgm(GlobalData.lobbyBGM, 0.3f);
        startBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayEffSound(GlobalData.btnClickSound);
            MemoryPoolManager.instance.AllObjectReturn();
            SceneManager.LoadScene("LobbyScene");
        });
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SoundManager.instance.PlayBgm(GlobalData.lobbyBGM);

    }
}