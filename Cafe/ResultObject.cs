using UnityEngine;
using UnityEngine.UI;

public class ResultObject : MonoBehaviour
{
    public Text makeText;
    public Button giveBtn;
    public Button OkBtn;

    private void OnEnable()
    {
        InitResultText();
        PlayerDrink.inst.DrinkParentActive(false);
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        giveBtn.onClick.AddListener(CompareFunc);

        OkBtn.onClick.AddListener(ResetFunc);

        InitResultText();
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (giveBtn.gameObject.activeSelf)
                CompareFunc();
            else if (OkBtn.gameObject.activeSelf)
                ResetFunc();
        }
    }

    void InitResultText()
    {
        makeText.text = "���� ���� ����\n" +
            "���� : " + GlobalData.icehotOrderText[(int)CafeManager.inst.makeOrder.iceHotType] + " " + GlobalData.menuNameText[(int)CafeManager.inst.makeOrder.drinktype] + "\n" +
            "������ : " + GlobalData.iceNameText[(int)CafeManager.inst.makeOrder.amountIce] + "\n";
        if (CafeManager.inst.makeOrder.additiveItem > 0)
            makeText.text += "�߰��ɼ� : �÷� " + (int)CafeManager.inst.makeOrder.additiveItem + "ȸ �߰�";
        else
            makeText.text += "�߰��ɼ� : ����"; 
    }

    void CompareFunc()
    {
        if (CafeManager.inst.Compare())
        {
            int number = CafeManager.inst.orderQue.Dequeue().customerKind;
            makeText.text = GlobalData.successOrderText[number];
            int coinRand = Random.Range(0, 2);
            SoundManager.instance.PlayEffSound(GlobalData.getCoinSounds[coinRand]);
        }
        else
        {
            int number = CafeManager.inst.orderQue.Dequeue().customerKind;
            makeText.text = GlobalData.errorOrderText[number];
            SoundManager.instance.PlayEffSound(GlobalData.errorBeepClip);
        }


        giveBtn.gameObject.SetActive(false);
        OkBtn.gameObject.SetActive(true);
    }

    void ResetFunc()
    {
        //�ڽ� ����
        giveBtn.gameObject.SetActive(true);
        OkBtn.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        Player.isInteract = false;

        CafeManager.inst.ResetCafe();
        PlayerDrink.inst.PlayerDrinkReset();
        PlayerDrink.inst.DrinkParentActive(true);
        CafeManager.inst.custMgr.OutCustomer();
        CafeManager.inst.custMgr.ForceInCustomer();
    }
}