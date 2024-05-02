using UnityEngine;
using UnityEngine.UI;

public class UpgradeBlock : MonoBehaviour
{
    public int itemIdx;
    public Button buyBtn;
    public GameObject lockPanel;
    public int unLockIdx;

    private void Start() => StartFunc();

    private void StartFunc()
    {

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    public void InitFunc()
    {
        if (GlobalData.shopUpgradeNumber < unLockIdx)
            lockPanel.gameObject.SetActive(true);

        Text[] texts = GetComponentsInChildren<Text>();

        if (texts != null)
        {
            texts[0].text = GlobalData.shopInfoTexts[itemIdx];
            texts[1].text = GlobalData.shopExTexts[itemIdx];
        }

        if(buyBtn != null)
        {
            buyBtn.GetComponentInChildren<Text>().text = "<size=30>구매</size>\n<color=yellow>$" + GlobalData.shopCosts[itemIdx] + "</color>";
        }

        if (itemIdx < GlobalData.shopUpgradeNumber)
        {
            buyBtn.gameObject.SetActive(false);
            return;
        }

        buyBtn.onClick.AddListener(() =>
        {
            if (GlobalData.shopCosts[itemIdx] > GlobalData.playerGold)
                return;

            SoundManager.instance.PlayEffSound(GlobalData.buySound, 0.3f);
            buyBtn.gameObject.SetActive(false);
            GlobalData.shopUpgradeNumber++;
            //실제 업글 수치
            GlobalData.playerDrinkUnlockNum += GlobalData.shopDrinkUpgradeNum[itemIdx];
            GlobalData.playerAdditiveUnlockNum += GlobalData.shopAdditiveUpgradeNum[itemIdx];
            GlobalData.maxOrderNum += GlobalData.shopUpgradeMaxOrderNum[itemIdx];

            GlobalData.upgraded[itemIdx + 1] = true;
            LobbyManager.inst.RefreshBlocks();
            StartCoroutine(LobbyManager.inst.DecreaseGoldCo(GlobalData.shopCosts[itemIdx]));
        });
    }

    public void CheckUnLock()
    {
        if(GlobalData.shopUpgradeNumber >= unLockIdx)
        {
            lockPanel.gameObject.SetActive(false);
        }
    }
}