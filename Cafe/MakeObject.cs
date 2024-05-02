using UnityEngine;

public class MakeObject : MonoBehaviour
{
    public enum MakeState
    {
        Before,
        Coffee,
        Tea,
        AdditiveBefore = 99,
        Additive = 3,
        End
    }

    public MakeState makeState = MakeState.Before;
    public GameObject[] makeObjects;

    private void OnEnable()
    {
        PlayerDrink.inst.DrinkParentActive(false);
    }

    private void OnDisable()
    {
        PlayerDrink.inst.DrinkParentActive(true);
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
         
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        switch(makeState)
        {
            case MakeState.Before:
                BeforeFunc();
                break;

            case MakeState.Coffee:
                CoffeeFunc();
                break;

            case MakeState.Tea:
                TeaFunc();
                break;

            case MakeState.AdditiveBefore:
                AdditiveBeforeFunc();
                break;

            case MakeState.Additive:
                AdditiveFunc();
                break;

            case MakeState.End:
                EndFunc();
                break;
        }
    }

    void BeforeFunc()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            makeObjects[(int)makeState].gameObject.SetActive(false);
            makeState = MakeState.Coffee;
            makeObjects[(int)makeState].gameObject.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            makeObjects[(int)makeState].gameObject.SetActive(false);
            makeState = MakeState.Tea;
            makeObjects[(int)makeState].gameObject.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            makeState = MakeState.End;
        }
    }

    void TeaFunc()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (CafeManager.inst.makeOrder.drinkBase == GlobalData.DrinkBase.Water)
            {
                CafeManager.inst.makeOrder.drinktype = GlobalData.Drink.GreenTea;
                PlayerDrink.inst.ChangeDrink(GlobalData.Drink.GreenTea);
            }
            else if (CafeManager.inst.makeOrder.drinkBase == GlobalData.DrinkBase.Milk)
            {
                CafeManager.inst.makeOrder.drinktype = GlobalData.Drink.GreenTeaLatte;
                PlayerDrink.inst.ChangeDrink(GlobalData.Drink.GreenTeaLatte);
            }
            makeObjects[(int)makeState].gameObject.SetActive(false);
            makeState = MakeState.AdditiveBefore;
        }
        else if(Input.GetKeyDown(KeyCode.B) && GlobalData.upgraded[(int)GlobalData.UpgradeState.BlackTea])
        {
            if (CafeManager.inst.makeOrder.drinkBase == GlobalData.DrinkBase.Water)
            {
                CafeManager.inst.makeOrder.drinktype = GlobalData.Drink.BlackTea;
                PlayerDrink.inst.ChangeDrink(GlobalData.Drink.BlackTea);
            }
            else if (CafeManager.inst.makeOrder.drinkBase == GlobalData.DrinkBase.Milk)
            {
                CafeManager.inst.makeOrder.drinktype = GlobalData.Drink.BlackTeaLatte;
                PlayerDrink.inst.ChangeDrink(GlobalData.Drink.BlackTeaLatte);
            }
            makeObjects[(int)makeState].gameObject.SetActive(false);
            makeState = MakeState.AdditiveBefore;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            makeObjects[(int)makeState].gameObject.SetActive(false);
            makeState = MakeState.Before;
            makeObjects[(int)makeState].gameObject.SetActive(true);
        }
    }

    void CoffeeFunc()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(CafeManager.inst.makeOrder.drinkBase == GlobalData.DrinkBase.Water)
            {
                SoundManager.instance.PlayEffSound(GlobalData.hitSounds, 0.5f);
                CafeManager.inst.makeOrder.drinktype = GlobalData.Drink.Americano;
                PlayerDrink.inst.ChangeDrink(GlobalData.Drink.Americano);
            }
            else if(CafeManager.inst.makeOrder.drinkBase == GlobalData.DrinkBase.Milk)
            {
                SoundManager.instance.PlayEffSound(GlobalData.hitSounds, 0.5f);
                CafeManager.inst.makeOrder.drinktype = GlobalData.Drink.CafeLatte;
                PlayerDrink.inst.ChangeDrink(GlobalData.Drink.CafeLatte);
            }
            makeObjects[(int)makeState].gameObject.SetActive(false);
            makeState = MakeState.AdditiveBefore;
        }
        else if(Input.GetKeyDown(KeyCode.C) && GlobalData.upgraded[(int)GlobalData.UpgradeState.ColdBrue])
        {
            if (CafeManager.inst.makeOrder.drinkBase == GlobalData.DrinkBase.Water)
            {
                SoundManager.instance.PlayEffSound(GlobalData.hitSounds, 0.5f);
                CafeManager.inst.makeOrder.drinktype = GlobalData.Drink.ColdBrue;
                PlayerDrink.inst.ChangeDrink(GlobalData.Drink.ColdBrue);
            }
            else if (CafeManager.inst.makeOrder.drinkBase == GlobalData.DrinkBase.Milk)
            {
                SoundManager.instance.PlayEffSound(GlobalData.hitSounds, 0.5f);
                CafeManager.inst.makeOrder.drinktype = GlobalData.Drink.ColdBrueLatte;
                PlayerDrink.inst.ChangeDrink(GlobalData.Drink.ColdBrueLatte);
            }
            makeObjects[(int)makeState].gameObject.SetActive(false);
            makeState = MakeState.AdditiveBefore;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            makeObjects[(int)makeState].gameObject.SetActive(false);
            makeState = MakeState.Before;
            makeObjects[(int)makeState].gameObject.SetActive(true);
        }
    }

    void AdditiveBeforeFunc()
    {
        PlayerDrink.inst.DrinkParentActive(true);

        if (GlobalData.playerAdditiveUnlockNum <= 0)
        {
            makeState = MakeState.End;
            CafeManager.inst.makeState = CafeManager.MakeState.packge;
            return;
        }
        else
        {
            makeState = MakeState.Additive;
            makeObjects[(int)makeState].gameObject.SetActive(true);
            PlayerDrink.inst.MovePos(false);
        }
    }

    void AdditiveFunc()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            CafeManager.inst.makeOrder.additiveItem++;
            PlayerDrink.inst.AddSirup();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            makeState = MakeState.End;
            PlayerDrink.inst.MovePos(true);
        }

    }

    void EndFunc()
    {
        Player.isInteract = false;
        this.gameObject.SetActive(false);
        makeState = MakeState.Before;
    }

    public void ResetFunc()
    {
        for (int i = 1; i < makeObjects.Length; i++)
            makeObjects[i].gameObject.SetActive(false);

        makeObjects[0].gameObject.SetActive(true);
        makeState = MakeState.Before;
    }
}