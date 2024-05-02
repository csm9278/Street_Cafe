using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CafeManager : MonoBehaviour
{
    public enum MakeState
    {
        GetOrder,
        SelectBase,
        MakeAndPackage,
        packge,
        Give = 4
    }

    public MakeState makeState = MakeState.SelectBase;
    public GameObject[] uiObjects;
    public Transform[] makePositions;

    public GlobalData.Order makeOrder = new GlobalData.Order(GlobalData.Drink.Water, GlobalData.IceHot.None, GlobalData.AmountIce.None, GlobalData.DrinkBase.Water);

    Customer orderCustomer = null;
    public Queue<GlobalData.Order> orderQue = new Queue<GlobalData.Order>();

    public OrderList OrderList;

    public static CafeManager inst;
    //public static int makeIdx = 0;

    public CustomerManager custMgr;
    public Text goldText;
    public Text addGoldText;
    Animator addgoldAnimator;

    public Text timeText;
    int hour, min, sec;
    public bool gameEnd = false;
    public bool gameStart = false;

    public bool isOrderReady = false;   //주문받을 차가 도착했을 시 true 상태
    public Text gameStateText;
    public bool lastCustmoerEnd = false;//영업종료 후 마지막 차가 나갔을 시 true 상태
    public bool timeEnd = false;        //영업종료 시간이 됐을 시 true 상태
    
    //각 파트 오브젝트들 
    HotIceObject hoticeObj;
    MakeObject makeObj;
    OrderManager orderObj;
    PackageObject packageObj;

    [Header("--- ResultObj---")]
    public GameObject resultObject;
    public Text perpectScaleText;
    public Text earnMoneyText;
    public Text dayText;
    public Button goLobbyBtn;
    int getOrderNum = 0, OrderClearNum = 0;
    int earnMoney = 0;

    private void Awake()
    {
        inst = this;
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        GameObject cameraCanvas = GameObject.Find("CameraCanvas");

        custMgr = FindObjectOfType<CustomerManager>();

        if(cameraCanvas != null)
        {
            hoticeObj = cameraCanvas.transform.Find("HotIceObject").gameObject.GetComponent<HotIceObject>();
            makeObj = cameraCanvas.transform.Find("MakeObject").gameObject.GetComponent<MakeObject>();
            orderObj = cameraCanvas.transform.Find("OrderObject").gameObject.GetComponent<OrderManager>();
            packageObj = cameraCanvas.transform.Find("PackageObject").gameObject.GetComponent<PackageObject>();
        }

        addgoldAnimator = addGoldText.GetComponent<Animator>();
        goldText.text = GlobalData.playerGold.ToString() + "$";

        SoundManager.instance.PlayBgm(GlobalData.inGameBGM, 0.3f);
        hour = 7;
        min = 0;
        StartCoroutine(TimeCo());

        goLobbyBtn.onClick.AddListener(() =>
        {
            SoundManager.instance.PlayBgm(GlobalData.lobbyBGM, 0.3f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
        });

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            OrderList.gameObject.SetActive(true);
        }
        else
            OrderList.gameObject.SetActive(false);
    }

    public void ActiveInteractObjects(int idx, bool active)
    {
        uiObjects[idx].gameObject.SetActive(active);
    }

    public void SetOrder(GlobalData.Drink drink, GlobalData.IceHot ih, GlobalData.AmountIce ice, GlobalData.AdditiveItem additive, int customerKind = 0)
    {
        GlobalData.Order newOrder = new GlobalData.Order(drink, ih, ice, GlobalData.DrinkBase.Water, additive , customerKind);


        orderQue.Enqueue(newOrder);

        OrderList.SetOrderList(orderQue);
        getOrderNum++;
    }

    public bool Compare()
    {
        GlobalData.Order compOrder = orderQue.Peek();

        if (compOrder.drinktype != makeOrder.drinktype)
            return false;

        if (compOrder.amountIce != makeOrder.amountIce)
            return false;

        if (compOrder.iceHotType != makeOrder.iceHotType)
            return false;

        if (compOrder.additiveItem != makeOrder.additiveItem)
            return false;

        if (!makeOrder.packaged) 
            return false;

        AddGold(compOrder);
        return true;
    }

    public void AddGold(GlobalData.Order nowOrder)
    {
        addGoldText.text = "+$" + GlobalData.menuPrice[(int)nowOrder.drinktype];

        addgoldAnimator.SetTrigger("AddGold");
        StartCoroutine(AddGoldCo(GlobalData.menuPrice[(int)nowOrder.drinktype]));
        earnMoney += GlobalData.menuPrice[(int)nowOrder.drinktype];
        OrderClearNum++;
    }

    public void ResetCafe()
    {
        makeState = MakeState.SelectBase;
        orderObj.ResetFunc();
        hoticeObj.ResetFunc();
        makeObj.ResetFunc();
        OrderList.ResetFunc();
        packageObj.ResetFunc();

        makeOrder.Reset();
    }

    IEnumerator AddGoldCo(int addGold)
    {
        for(int i = 0; i < addGold; i++)
        {
            GlobalData.playerGold++;
            goldText.text = "$ " + GlobalData.playerGold;
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// 주문할 고객이 도착했을 때
    /// </summary>
    /// <param name="cust">도착한 Customer</param>
    public void ArriveCustomer(Customer cust)
    {
        orderCustomer = cust;
        isOrderReady = true;
    }

    public void Ordered()
    {
        orderCustomer.isOrder = true;
        isOrderReady = false;
        orderCustomer = null;
    }

    IEnumerator TimeCo()
    {
        gameStateText.text = "준비..";
        yield return new WaitForSeconds(2.0f);

        gameStateText.text = "시작!!";
        yield return new WaitForSeconds(1.0f);

        gameStateText.text = "";
        gameStart = true;

        while (hour < 18)
        {
            yield return new WaitForSeconds(1.0f);
            min += 10;
            if(min >= 60)
            {
                hour++;
                min = 0;
            }
            timeText.text = hour.ToString("D2") + " : " + min.ToString("D2");


        }

        timeEnd = true;

        while(!lastCustmoerEnd)
        {
            yield return new WaitForEndOfFrame();
        }
        gameStateText.text = "종료!!";
        yield return new WaitForSeconds(2.0f);
        ResultFunc();
        resultObject.gameObject.SetActive(true);
    }


    void ResultFunc()
    {
        
        perpectScaleText.text = Mathf.FloorToInt((float)OrderClearNum / getOrderNum * 100.0f) + "%";
        earnMoneyText.text = "$ " + earnMoney.ToString();
        dayText.text = "Day " + GlobalData.dayNumber;
        GlobalData.dayNumber++;
    }
}