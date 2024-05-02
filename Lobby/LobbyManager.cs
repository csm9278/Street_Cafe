using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Button shopBtn;
    public Button startBtn;
    public Button recipeBtn;

    public GameObject shopRoot;
    public UpgradeBlock[] upgradeBlocks;

    public GameObject recipeRoot;
    public RecipeBlock[] recipeBlocks;

    public Text goldText;
    public Text goldEffText;
    Animator goldAnimator;

    [Header("--- Recipe ---")]
    public Text recipeInfoText;
    public Text recipeAlramText;

    public static LobbyManager inst;

    private void Awake()
    {
        inst = this;
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        GlobalData.upgraded[0] = true;

        goldText.text = "$ " + GlobalData.playerGold;
        if(goldEffText.text != null)
        {
            goldAnimator = goldEffText.GetComponentInChildren<Animator>();
        }

        shopBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayEffSound(GlobalData.btnClickSound);
            if (shopRoot.activeSelf)
                shopRoot.gameObject.SetActive(false);
            else
            {
                if (recipeRoot.activeSelf)
                    recipeRoot.gameObject.SetActive(false);
                shopRoot.gameObject.SetActive(true);
            }
        });

        recipeBtn.onClick.AddListener(() =>
        {   
            SoundManager.instance.PlayEffSound(GlobalData.btnClickSound);
            if (recipeRoot.activeSelf)
                recipeRoot.gameObject.SetActive(false);
            else
            {
                if (shopRoot.activeSelf)
                    shopRoot.gameObject.SetActive(false);
                recipeRoot.gameObject.SetActive(true);
            }
        });

        startBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayEffSound(GlobalData.btnClickSound);
            UnityEngine.SceneManagement.SceneManager.LoadScene("CafeScene");
        });

        //상점 루트 초기화
        if(shopRoot != null)
        {
            upgradeBlocks = shopRoot.GetComponentsInChildren<UpgradeBlock>();

            for(int i = 0; i < upgradeBlocks.Length; i++)
            {
                upgradeBlocks[i].itemIdx = i;
                upgradeBlocks[i].InitFunc();
            }
            shopRoot.gameObject.SetActive(false);
        }

        //상점 루트 초기화
        if (recipeBlocks != null)
        {
            recipeBlocks = recipeRoot.GetComponentsInChildren<RecipeBlock>();

            for (int i = 0; i < recipeBlocks.Length; i++)
            {
                recipeBlocks[i].InitRecipe(i);
            }
            recipeRoot.gameObject.SetActive(false);
        }

        recipeInfoText.text = "";
        recipeAlramText.text = "";
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    public void RefreshBlocks()
    {
        for(int i = GlobalData.shopUpgradeNumber - 1; i < upgradeBlocks.Length; i++)
        {
            upgradeBlocks[i].CheckUnLock();
        }

        for(int i = 0; i < recipeBlocks.Length;i++)
        {
            recipeBlocks[i].RefreshRecipe();
        }
    }

    public IEnumerator DecreaseGoldCo(int Gold)
    {
        float curGold = GlobalData.playerGold;
        float beforeGold = curGold;
        GlobalData.playerGold -= Gold;
        goldEffText.text = "-$" + Gold;
        goldAnimator.SetTrigger("AddGold");

        float timer = 0.3f;
        float offSet = (GlobalData.playerGold - curGold) / timer;
        while(GlobalData.playerGold <= curGold)
        {
            curGold += offSet * Time.deltaTime;
            goldText.text = "$ " + (int)curGold;

            yield return null;
        }
        goldText.text = "$ " + (beforeGold - Gold);
    }
}