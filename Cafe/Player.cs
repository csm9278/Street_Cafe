using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform[] MovePosition;
    public int moveIdx = 0;

    public bool isMove = false;
    public static bool isInteract = false;
    float moveTime = .1f;
    float curMoveTime = 0;

    CafeManager cafeMgr;

    public GameObject drink;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        cafeMgr = FindObjectOfType<CafeManager>();
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (cafeMgr.gameEnd)
            return;
        if (!cafeMgr.gameStart)
            return;

        InteractUpdate();
        MoveUpdate();   
    }

    void InteractUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (moveIdx == 0)
            {
                //손님이 아직 도착하지 않아 받을 상태가 아니면 리턴
                if (!CafeManager.inst.isOrderReady || CafeManager.inst.orderQue.Count >= GlobalData.maxOrderNum)
                    return;

                cafeMgr.ActiveInteractObjects(moveIdx, true);
                isInteract = true;
            }

            if (moveIdx == (int)cafeMgr.makeState)
            {
                cafeMgr.ActiveInteractObjects(moveIdx, true);
                isInteract = true;
            }
            else if(moveIdx == 3 && cafeMgr.makeState == CafeManager.MakeState.MakeAndPackage)  //패키지로 바로 넘어갈수 있게
            {
                cafeMgr.ActiveInteractObjects(moveIdx, true);
                isInteract = true;
            }
        }
        
    }

    public void ExitBtn()
    {
        cafeMgr.ActiveInteractObjects(moveIdx, false);
        isInteract = false;
        CafeManager.inst.makeState = CafeManager.MakeState.SelectBase;
    }

    void MoveUpdate()
    {
        if (isMove || isInteract)
            return;

        if(Input.GetKeyDown(KeyCode.A))
        {
            if(moveIdx > 0)
            {
                moveIdx--;
                StartCoroutine(MoveCo(this.transform, MovePosition[moveIdx]));
            }
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (moveIdx + 1 < MovePosition.Length)
            {
                moveIdx++;
                StartCoroutine(MoveCo(this.transform, MovePosition[moveIdx]));
            }
        }
    }

    IEnumerator MoveCo(Transform now, Transform target)
    {
        isMove = true;
        while(curMoveTime < 1)
        {
            curMoveTime += Time.deltaTime / moveTime;
            this.transform.position = Vector3.Lerp(now.position, target.position, curMoveTime);

            yield return new WaitForEndOfFrame();
        }
        curMoveTime = 0;
        isMove = false;
        yield break;
    }
}