using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MemoryPoolObject
{
    public float moveTimer = 5.0f;

    Vector3 resultVec = new Vector3(-7.5f, 1.7f, 16.5f);
    Vector3 goOutVec = new Vector3(-20.5f, 1.7f, 16.5f);
    Vector3 arriveVec = new Vector3(35.0f, 1.7f, 16.5f);
    WheelRotate[] wheels;

    public bool isOrder = false;    //�ֹ� �Ϸ��ߴ���?
    bool orderReady = true;         //�ֹ� �غ� �Ǿ�����?
    bool isWheelRotate = false;     //���� ȸ�� ����
    public bool getDrink = false;   //���� ���� ����

    public bool isDelay = false;    //���� �ѹ�������
    Ray ray;

    WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

    //�ֹ� �� �γ��� �ð�
    public float angerTimer = 20.0f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, Vector3.left * 5);
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        CheckFrontCar();

        //if(isOrder)
        //{
        //    WheelFunc(true);
        //    if(curMoveTimer < 1.0f)
        //    {
        //        if (isDelay)
        //            return;

        //        if (!isMakeEnd)
        //            curMoveTimer += Time.deltaTime / moveTimer;
        //        else
        //            curMoveTimer += Time.deltaTime;

        //        this.transform.position = Vector3.Lerp(orderVec, resultVec, curMoveTimer);
        //    }
        //}
    }

    void CheckFrontCar()
    {
        RaycastHit hit;
        ray = new Ray(transform.position, Vector3.left);
        if(Physics.Raycast(ray, out hit, 5))
        {
            isDelay = true;
            return;
        }
        
        isDelay = false;
    }

    void WheelFunc(bool isActive)
    {
        if (isActive == isWheelRotate)
            return;

        if(isActive)
        {
            for (int i = 0; i < wheels.Length; i++)
                wheels[i].enabled = true;
            isWheelRotate = true;
        }
        else
        {
            for (int i = 0; i < wheels.Length; i++)
                wheels[i].enabled = false;
            isWheelRotate = false;
        }

    }

    public IEnumerator CarGoCo()
    {
        this.transform.localPosition = arriveVec;
        float curTimer = 0.0f;

        // �ֹ�
        while(curTimer < 1)
        {
            if (isOrder)
                angerTimer -= Time.deltaTime;

            if (isDelay)    // �� �տ� �ٸ� ���� ���� �� Delay ����
            {
                WheelFunc(false);
                yield return endOfFrame;
                continue;
            }

            if(isOrder) //�ֹ� ���� ��
            {
                WheelFunc(true);
                curTimer += Time.deltaTime * .25f;
                yield return endOfFrame;
            }
            else    //�ֹ� ���� �ޱ� ���� �� 
            {
                if(curTimer >= 0.55f)
                {
                    if(orderReady)  //ó�� �ֹ��� ������ �������� ��
                    {
                        SoundManager.instance.PlayEffSound(GlobalData.carHornSounds);
                        WheelFunc(false);
                        CafeManager.inst.ArriveCustomer(this);
                        orderReady = false;
                    }

                    yield return endOfFrame;
                    continue;
                }

                WheelFunc(true);
                curTimer += Time.deltaTime * .25f;
                yield return endOfFrame;
            }
            this.transform.position = Vector3.Lerp(arriveVec, resultVec, curTimer);
        }

        WheelFunc(false);

        //���� ���� �����
        curTimer = 0;
        while(curTimer < 1)
        {
            if (!getDrink)  // �ֹ��� ���Ḧ ���� ���� ������ ��
            {
                angerTimer -= Time.deltaTime;
                if(angerTimer <= 0.0f)  //���� ȭ������ �߰�
                {

                    //break;
                }

                yield return endOfFrame;
                continue;
            }

            WheelFunc(true);
            curTimer += Time.deltaTime;
            yield return endOfFrame;
            this.transform.position = Vector3.Lerp(resultVec, goOutVec, curTimer);
        }

        isOrder = false;
        orderReady = true;
        getDrink = false;
        isWheelRotate = false;
        ObjectReturn();
    }



    IEnumerator RePositionCo()
    {
        CafeManager.inst.isOrderReady = false;
        WheelFunc(true);
        float curtimer  = 0.0f;
        while (curtimer < 1)
        {
            curtimer += Time.deltaTime;
            this.transform.position = Vector3.Lerp(resultVec, goOutVec, curtimer);

            yield return new WaitForEndOfFrame();
        }

        if(CafeManager.inst.timeEnd)
        {
            CafeManager.inst.lastCustmoerEnd = true;
            yield break;
        }
    }

    public void ResetFunc()
    {
        isOrder = false;
        StartCoroutine(RePositionCo());
    }

    public override void InitObject()
    {
        wheels = GetComponentsInChildren<WheelRotate>();
    }
}