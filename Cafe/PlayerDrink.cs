using UnityEngine;

public class PlayerDrink : MonoBehaviour
{
    public GameObject[] drinks;
    public GameObject[] Cups;
    public GameObject[] ices;
    public GameObject[] iceCupPackages;
    public GameObject[] hotCupPackages;
    public GameObject[] additiveSirup;
    int additiveSirupIdx = 0;
    public GameObject drinkParent;
    public GameObject iceCupJacket;

    public static PlayerDrink inst;

    public WaterFill waterFill;
    Material waterMat;
    Color waterColor = new Color32(255, 255, 255, 4);
    Color milkColor = new Color32(255, 255, 255, 255);

    private void Awake()
    {
        inst = this;
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        waterFill = GetComponentInChildren<WaterFill>();
        if(waterFill != null)
        {
            waterMat = waterFill.gameObject.GetComponent<MeshRenderer>().material;
        }
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

    }

    public void ChangeDrink(GlobalData.Drink d)
    {
        drinks[0].gameObject.SetActive(false);
        drinks[(int)d].gameObject.SetActive(true);
    }

    public void ChangeCup(GlobalData.IceHot i)
    {
        Cups[(int)i].gameObject.SetActive(true);
    }

    public void CupActiveFalse()
    {
        for (int i = 0; i < Cups.Length; i++)
            Cups[i].gameObject.SetActive(false);
    }
    
    public void IceSetting(GlobalData.AmountIce ice)
    {
        for(int i = 0; i < 3; i++)
        {
            if (i < (int)ice)
                ices[i].gameObject.SetActive(true);
            else
                ices[i].gameObject.SetActive(false);
        }
    }

    public void Package(GlobalData.IceHot type, int idx)
    {
        if (type == GlobalData.IceHot.Hot)
        {
            hotCupPackages[idx].gameObject.SetActive(true);
        }
        else
            iceCupPackages[idx].gameObject.SetActive(true);
    }

    public void MovePos(bool isOrigin)
    {
        if (isOrigin)
        {
            this.transform.localPosition = new Vector3(0, -0.55f, 0.8f);
            this.transform.localRotation = Quaternion.Euler(-14.28f, 0, 0);
        }
        else
        {
            this.transform.localPosition= new Vector3(-0.26f, -0.22f, 0.86f);
            this.transform.localRotation = Quaternion.Euler(-27, 0, 0);
        }
    }

    public void IceJacketOn()
    {
        iceCupJacket.gameObject.SetActive(true);
    }

    public void DrinkParentActive(bool active)
    {
        drinkParent.gameObject.SetActive(active);
    }

    public void AddSirup()
    {
        if (additiveSirupIdx >= 2)
            return;

        SoundManager.instance.PlayEffSound(GlobalData.hitSounds, 0.5f);
        additiveSirup[additiveSirupIdx].gameObject.SetActive(true);
        additiveSirupIdx++;
    }

    public void PlayerDrinkReset()
    {
        for (int i = 1; i < drinks.Length; i++)
            drinks[i].gameObject.SetActive(false);
        drinks[0].gameObject.SetActive(true);

        for (int i = 0; i < Cups.Length; i++)
            Cups[i].gameObject.SetActive(false);

        for (int i = 0; i < ices.Length; i++)
            ices[i].gameObject.SetActive(false);

        for (int i = 0; i < iceCupPackages.Length; i++)
            iceCupPackages[i].gameObject.SetActive(false);

        for (int i = 0; i < hotCupPackages.Length; i++)
            hotCupPackages[i].gameObject.SetActive(false);

        for (int i = 0; i < additiveSirup.Length; i++)
            additiveSirup[i].gameObject.SetActive(false);

        additiveSirupIdx = 0;

        iceCupJacket.SetActive(false);
        waterFill.ResetFill();
    }

    public void SetBase(GlobalData.DrinkBase b)
    {
        waterFill.ChangeFillImage();

        if(b == GlobalData.DrinkBase.Milk)
        {
            waterMat.color = milkColor;
            waterFill.enabled = true;
        }
        else if(b == GlobalData.DrinkBase.Water)
        {
            waterMat.color = waterColor;
            waterFill.enabled = true;
        }
    }
}