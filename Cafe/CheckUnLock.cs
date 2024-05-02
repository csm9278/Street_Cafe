using UnityEngine;

public class CheckUnLock : MonoBehaviour
{
    bool isInit = false;
    public GlobalData.UpgradeState unLock;

    private void OnEnable()
    {
        if(!isInit)
        {
            CheckNumber();
            isInit = true;
        }
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        if (!GlobalData.upgraded[(int)unLock])
        {
            this.gameObject.SetActive(false);
        }
        else
            this.gameObject.SetActive(true);
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    void CheckNumber()
    {

    }
}