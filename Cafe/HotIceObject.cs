using UnityEngine;
using UnityEngine.UI;

public class HotIceObject : MonoBehaviour
{
    public enum HotIceState
    {
        HotIceChoose,
        DrinkBaseChoose,
        IceWater,
        HotWater,

        IceSelect,
        WaterFill
    }

    public HotIceState state = HotIceState.HotIceChoose;

    public GameObject[] Objects;
    public int iceIdx = 0;

    public GameObject WaterObject;
    public GameObject IceObject;

    public GameObject iceCup;

    public Text baseFillText;

    private void Start() => StartFunc();

    private void StartFunc()
    {
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        switch(state)
        {
            case HotIceState.HotIceChoose:
                if(Input.GetKeyDown(KeyCode.H))
                {
                    Objects[(int)state].gameObject.SetActive(false);
                    state = HotIceState.DrinkBaseChoose;
                    Objects[1].gameObject.SetActive(true);
                    CafeManager.inst.makeOrder.iceHotType = GlobalData.IceHot.Hot;
                }
                else if(Input.GetKeyDown(KeyCode.C))
                {
                    //Objects[(int)state].gameObject.SetActive(false);
                    //state = HotIceState.DrinkBaseChoose;
                    //Objects[1].gameObject.SetActive(true);
                    //CafeManager.inst.makeOrder.iceHotType = GlobalData.IceHot.Ice;

                    Objects[(int)state].gameObject.SetActive(false);
                    state = HotIceState.DrinkBaseChoose;
                    Objects[1].gameObject.SetActive(true);
                    CafeManager.inst.makeOrder.iceHotType = GlobalData.IceHot.Ice;
                }
                else if(Input.GetKeyDown(KeyCode.Escape))
                {
                    this.gameObject.SetActive(false);
                    Player.isInteract = false;
                }
                break;

            case HotIceState.DrinkBaseChoose:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    CafeManager.inst.makeOrder.drinkBase = GlobalData.DrinkBase.Water;
                    Objects[1].gameObject.SetActive(false);
                    Objects[2].gameObject.SetActive(true);

                    PlayerDrink.inst.MovePos(false);
                    PlayerDrink.inst.ChangeCup(CafeManager.inst.makeOrder.iceHotType);
                    ChangeFillName();
                    SetIceSelect();
                }
                else if (Input.GetKeyDown(KeyCode.M) && GlobalData.shopUpgradeNumber >= (int)GlobalData.UpgradeState.Milk)
                {
                    CafeManager.inst.makeOrder.drinkBase = GlobalData.DrinkBase.Milk;
                    Objects[1].gameObject.SetActive(false);
                    Objects[2].gameObject.SetActive(true);

                    PlayerDrink.inst.MovePos(false);
                    PlayerDrink.inst.ChangeCup(CafeManager.inst.makeOrder.iceHotType);
                    ChangeFillName();
                    SetIceSelect();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = HotIceState.HotIceChoose;
                    Objects[1].gameObject.SetActive(false);
                    Objects[0].gameObject.SetActive(true);
                    CafeManager.inst.makeOrder.iceHotType = GlobalData.IceHot.None;
                }
                break;

            case HotIceState.IceSelect:
                if(Input.GetKeyDown(KeyCode.P))
                {
                    if(iceIdx < 3)
                    {
                        //Ices[iceIdx].gameObject.SetActive(true);
                        SoundManager.instance.PlayEffSound(GlobalData.iceDropSound);
                        iceIdx++;
                        PlayerDrink.inst.IceSetting((GlobalData.AmountIce)iceIdx);
                    }
                }
                else if(Input.GetKeyDown(KeyCode.M))
                {
                    if(iceIdx > 0)
                    {
                        iceIdx--;
                        //Ices[iceIdx].gameObject.SetActive(false);
                        PlayerDrink.inst.IceSetting((GlobalData.AmountIce)iceIdx);
                    }
                }
                else if(Input.GetKeyDown(KeyCode.Return))
                {
                    PlayerDrink.inst.SetBase(CafeManager.inst.makeOrder.drinkBase);
                    state = HotIceState.WaterFill;
                    WaterObject.SetActive(true);
                    IceObject.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CafeManager.inst.makeOrder.drinkBase = GlobalData.DrinkBase.Water;
                    Objects[2].gameObject.SetActive(false);
                    Objects[1].gameObject.SetActive(true);

                    PlayerDrink.inst.MovePos(true);
                    PlayerDrink.inst.CupActiveFalse();
                    PlayerDrink.inst.waterFill.ResetFill();

                    state = HotIceState.DrinkBaseChoose;
                }
                break;

            case HotIceState.WaterFill:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (!PlayerDrink.inst.waterFill.isEnd)
                        return;

                    CafeManager.inst.makeState = CafeManager.MakeState.MakeAndPackage;
                    Player.isInteract = false;
                    CafeManager.inst.makeOrder.amountIce = (GlobalData.AmountIce)iceIdx;
                    PlayerDrink.inst.ChangeCup(CafeManager.inst.makeOrder.iceHotType);
                    PlayerDrink.inst.IceSetting((GlobalData.AmountIce)iceIdx);
                    PlayerDrink.inst.MovePos(true);
                    this.gameObject.SetActive(false);
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CafeManager.inst.makeOrder.drinkBase = GlobalData.DrinkBase.Water;
                    Objects[2].gameObject.SetActive(false);
                    Objects[1].gameObject.SetActive(true);

                    PlayerDrink.inst.MovePos(true);
                    PlayerDrink.inst.CupActiveFalse();
                    iceIdx = 0;
                    PlayerDrink.inst.IceSetting((GlobalData.AmountIce)iceIdx);
                    PlayerDrink.inst.PlayerDrinkReset();
                    state = HotIceState.DrinkBaseChoose;
                }
                break;

        }
    }
    
    /// <summary>
    /// 물따르기로 전환될 때 음료 베이스에 따른 텍스트 변화함수
    /// </summary>
    void ChangeFillName()
    {
        switch(CafeManager.inst.makeOrder.drinkBase)
        {
            case GlobalData.DrinkBase.Water:
                baseFillText.text = "물 따르기";
                break;

            case GlobalData.DrinkBase.Milk:
                baseFillText.text = "우유 따르기";
                break;
        }
    }

    public void SetIceSelect()
    {
        if (CafeManager.inst.makeOrder.iceHotType == GlobalData.IceHot.Hot)
        {
            PlayerDrink.inst.SetBase(GlobalData.DrinkBase.Water);
            state = HotIceState.WaterFill;
            WaterObject.SetActive(true);
            IceObject.SetActive(false);
            return;
        }

        PlayerDrink.inst.waterFill.enabled = false;
        state = HotIceState.IceSelect;
        WaterObject.SetActive(false);
        IceObject.SetActive(true);
    }

    public void ResetFunc()
    {
        for(int i = 0; i < Objects.Length; i++)
            Objects[i].gameObject.SetActive(false);
        state = HotIceState.HotIceChoose;
        Objects[(int)state].gameObject.SetActive(true);
        CafeManager.inst.makeOrder.iceHotType = GlobalData.IceHot.None;
        CafeManager.inst.makeOrder.amountIce = GlobalData.AmountIce.None;
        iceIdx = 0;
        WaterObject.SetActive(true);
        IceObject.SetActive(false);
    }
}