using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCar : MemoryPoolObject
{
    Vector3 startPos;
    Vector3 endPos;

    WheelRotate[] wheels;


    //private void Start() => StartFunc();

    //private void StartFunc()
    //{
         
    //}

    //private void Update() => UpdateFunc();

    //private void UpdateFunc()
    //{
        
    //}

    public void SetOtherCar(Vector3 start, Vector3 end)
    {
        startPos = start;
        endPos = end;

        StartCoroutine(GoCo());
    }

    IEnumerator GoCo()
    {
        if (startPos.x < endPos.x)
        {
            this.transform.rotation = Quaternion.Euler(0, 90.0f, 0);
        }
        else
            this.transform.rotation = Quaternion.Euler(0, 270.0f, 0);


        float curTimer = 0.0f;
        while(curTimer < 1)
        {
            this.transform.position = Vector3.Lerp(startPos, endPos, curTimer);
            curTimer += Time.deltaTime * .6f;

            yield return new WaitForEndOfFrame();
        }

        ObjectReturn();
    }

    public override void InitObject()
    {
        wheels = GetComponentsInChildren<WheelRotate>();
    }
}