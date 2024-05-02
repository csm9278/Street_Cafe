using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderList : MonoBehaviour
{
    public GameObject[] orderObjects;
    public Text[] menuName;
    public Text[] OptionList;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        for(int i = GlobalData.maxOrderNum; i < 3; i++)
        {
            orderObjects[i].gameObject.SetActive(false);
        }
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }

    public void SetOrderList(string iceorHot, string menuName, string Option)
    {
        this.menuName[0].text = iceorHot + menuName;
        this.OptionList[0].text = Option;
    }

    public void SetOrderList(Queue<GlobalData.Order> orderList)
    {
        for(int i = 0; i < GlobalData.maxOrderNum; i++)
        {
            menuName[i].text = "";
            OptionList[i].text = "";
        }

        int idx = 0;
        foreach(GlobalData.Order od in orderList)
        {
            menuName[idx].text = GlobalData.icehotOrderText[(int)od.iceHotType] + GlobalData.menuNameText[(int)od.drinktype];
            OptionList[idx].text = GlobalData.iceNameText[(int)od.amountIce] + "\n";
            if (od.additiveItem > 0)
                OptionList[idx].text += "시럽 " + (int)od.additiveItem + "회 추가";
            idx++;
        }
    }

    public void ResetFunc()
    {
        SetOrderList(CafeManager.inst.orderQue);
    }
}