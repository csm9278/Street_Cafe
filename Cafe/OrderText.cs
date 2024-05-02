using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GlobalData;

public class OrderText : MonoBehaviour
{
    public enum TextTypingType
    {
        Auto,
        Skip
    }

    IceHot iceHot = IceHot.Hot;
    AmountIce amountIce = AmountIce.Medium;
    public Text orderText;
    bool makingText = false;
    WaitForSeconds typingSeconds = new WaitForSeconds(0.1f);
    public TextTypingType textTypingType = TextTypingType.Auto;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        if(orderText == null)
        {
            orderText = GetComponentInChildren<Text>();
        }
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {

    }


    public IEnumerator makeRandOrder()
    {
        makingText = true;
        orderText.text = "";
        int customerKind = Random.Range(0, GlobalData.firstOrderText.Length);   // ó�� �λ縻     
        int iceHotrand = Random.Range(0, 2);                                    //������ �̰߰� ����
        int menurand = Random.Range(0, GlobalData.playerDrinkUnlockNum);        //�޴� ����
        int additiveRand = Random.Range(-1, GlobalData.playerAdditiveUnlockNum); //�߰� ����

        string order = GlobalData.firstOrderText[customerKind] + GlobalData.icehotOrderText[iceHotrand] + GlobalData.menuOrderText[menurand];

        iceHot = (IceHot)iceHotrand;
        int iceNumber = 0;
        if(iceHot == IceHot.Ice)
        {
            iceNumber = Random.Range(1, 4);
            order += GlobalData.iceOrderText[iceNumber];
        }

        //�߰��ɼ� ���� �ƴҶ�
        int kindRand = 0;
        if(menurand != 0)
        {
            if (additiveRand >= 0)
            {
                kindRand = Random.Range(1, 3);
                order += GlobalData.additiveText[additiveRand] + kindRand + "�� �߰��ؼ�";
            }
        }


        order += GlobalData.lastOrderText[customerKind];


        yield return StartCoroutine(TextTyper(order));

        CafeManager.inst.SetOrder((Drink)menurand, iceHot, (AmountIce)iceNumber, (AdditiveItem)kindRand, customerKind);

        yield break;

    }

    IEnumerator TextTyper(string s)
    {

        SoundManager.instance.PlayChatSound(true);

        for (int i = 0; i < s.Length; i++)
        {
            orderText.text += s[i];
            if(textTypingType == TextTypingType.Skip)
            {
                orderText.text = s;
                textTypingType = TextTypingType.Auto;
                break;
            }

            yield return typingSeconds;
        }
        SoundManager.instance.PlayChatSound(false);

    }
}