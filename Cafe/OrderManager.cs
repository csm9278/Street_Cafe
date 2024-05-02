using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public Button orderBtn;
    public Button okBtn;
    OrderText orderText;
    bool ordering = false;

    Player player;
    CafeManager cafeMgr;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        player = FindObjectOfType<Player>();
        cafeMgr = FindObjectOfType<CafeManager>();

        orderText = GetComponentInChildren<OrderText>();

        orderBtn.onClick.AddListener(()=>
        {
            if(!ordering)
                StartCoroutine(orderStart());
        });

        okBtn.onClick.AddListener(() =>
        {
            cafeMgr.Ordered();
            ResetFunc();
            Player.isInteract = false;
        });
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(!okBtn.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if (!ordering)
                    StartCoroutine(orderStart());
                else
                    orderText.textTypingType = OrderText.TextTypingType.Skip;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                cafeMgr.Ordered();
                ResetFunc();
                Player.isInteract = false;
            }
        }
    }

    IEnumerator orderStart()
    {
        ordering = true;
        orderBtn.gameObject.SetActive(false);
        yield return StartCoroutine(orderText.makeRandOrder());
        okBtn.gameObject.SetActive(true);
        ordering = false;

        //cafeMgr.custMgr.OutCustomer();
    }

    public void ResetFunc()
    {
        orderBtn.gameObject.SetActive(true);
        okBtn.gameObject.SetActive(false);
        orderText.orderText.text = "";
        this.gameObject.SetActive(false);
    }
}