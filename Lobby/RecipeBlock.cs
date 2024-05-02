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

        //���׷��̵带 �ر��߰� �⺻��ᰡ �ƴ��� üũ�ؼ� �߰� 
        if((!GlobalData.upgraded[GlobalData.recipeUpgradeNumber[recipeNumber, 0]] && GlobalData.recipeUpgradeNumber[recipeNumber, 0] != 0) ||
           (!GlobalData.upgraded[GlobalData.recipeUpgradeNumber[recipeNumber, 1]] && GlobalData.recipeUpgradeNumber[recipeNumber, 1] != 0))
        {
            LobbyManager.inst.recipeAlramText.text = "�� �ر�\n-";

            //ù��° ��� �رݵƴ��� Ȯ��
            bool needTwoUpgrade = false;    // ��ᰡ �ΰ� ������ , ��� ����ϱ⿡
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

            LobbyManager.inst.recipeAlramText.text += " �ʿ�";
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