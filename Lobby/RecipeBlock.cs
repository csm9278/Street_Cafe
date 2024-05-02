using UnityEngine;
using UnityEngine.UI;

public class RecipeBlock : MonoBehaviour
{
    Button recipeBtn;

    public int recipeNumber = 0;
    public Text recipeName;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        recipeBtn = GetComponent<Button>();

        if(recipeBtn != null)
        {
            recipeBtn.onClick.AddListener(ShowRecipeInfo);
        }
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    void ShowRecipeInfo()
    {
        LobbyManager.inst.recipeInfoText.text = "";
        LobbyManager.inst.recipeAlramText.text = "";
        for (int i = 0; i < GlobalData.recipeInfoTexts.GetLength(1); i++)
        {
            LobbyManager.inst.recipeInfoText.text += GlobalData.recipeInfoTexts[recipeNumber, i];
        }

        //업그레이드를 해금했고 기본재료가 아닌지 체크해서 추가 
        if((!GlobalData.upgraded[GlobalData.recipeUpgradeNumber[recipeNumber, 0]] && GlobalData.recipeUpgradeNumber[recipeNumber, 0] != 0) ||
           (!GlobalData.upgraded[GlobalData.recipeUpgradeNumber[recipeNumber, 1]] && GlobalData.recipeUpgradeNumber[recipeNumber, 1] != 0))
        {
            LobbyManager.inst.recipeAlramText.text = "미 해금\n-";

            //첫번째 재료 해금됐는지 확인
            bool needTwoUpgrade = false;    // 재료가 두개 없으면 , 찍어 줘야하기에
            if(!GlobalData.upgraded[GlobalData.recipeUpgradeNumber[recipeNumber, 0]])
            {
                LobbyManager.inst.recipeAlramText.text += GlobalData.UpgradeName[GlobalData.recipeUpgradeNumber[recipeNumber,0]];
                needTwoUpgrade = true;
            }

            if (!GlobalData.upgraded[GlobalData.recipeUpgradeNumber[recipeNumber, 1]])
            {
                if (needTwoUpgrade)
                    LobbyManager.inst.recipeAlramText.text += ", ";
                LobbyManager.inst.recipeAlramText.text += GlobalData.UpgradeName[GlobalData.recipeUpgradeNumber[recipeNumber, 1]];
            }

            LobbyManager.inst.recipeAlramText.text += " 필요";
        }
    }

    public void InitRecipe(int recipeNum)
    {
        recipeNumber = recipeNum;
        recipeName.text = GlobalData.menuNameText[recipeNumber];
    }

    public void RefreshRecipe()
    {

    }
}