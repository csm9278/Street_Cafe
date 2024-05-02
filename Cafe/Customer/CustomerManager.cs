using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public int stayNum = 0;
    float spawnTimer = 2.0f;
    float delayTime = 0.0f;
    bool delayed = false;

    Queue<Customer> customerQue = new Queue<Customer>();

    //고객 외 차 관리
    float rightOtherSpawnTimer = 0.0f;
    float leftOtherSpawnTimer = 0.0f;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        rightOtherSpawnTimer = Random.Range(1.0f, 10.0f);
        leftOtherSpawnTimer = Random.Range(1.0f, 10.0f);

    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (spawnTimer >= 0.0f && !CafeManager.inst.timeEnd)
        {
            spawnTimer -= Time.deltaTime;
            rightOtherSpawnTimer -= Time.deltaTime;
            leftOtherSpawnTimer -= Time.deltaTime;
        }

        if (delayTime >= 0.0f)
        {
            delayTime -= Time.deltaTime;
            if(delayTime <= 0.0f)
            {
                if (delayed)
                    InCustomer();
            }
        }

        if(rightOtherSpawnTimer <= 0.0f)
            SpawnOther(true);

        if (leftOtherSpawnTimer <= 0.0f)
            SpawnOther(false) ;

        if (spawnTimer <= 0.0f && stayNum < 3)
        {
            InCustomer();
        }

        if(Input.GetKeyDown(KeyCode.Delete) && stayNum > 0)
        {
            OutCustomer();
        }
    }

    public void InCustomer()
    {
        if(delayTime >= 0.0f)
        {
            delayed = true;
            return;
        }

        string s = "Customer" + Random.Range(1, 5).ToString();
        GameObject obj = MemoryPoolManager.instance.GetObject(s);

        if (obj.TryGetComponent(out Customer customer))
        {
            StartCoroutine(customer.CarGoCo());
            customerQue.Enqueue(customer);
        }
        stayNum++;
        spawnTimer = 8.0f;
        delayTime = 2.0f;
    }

    /// <summary>
    /// 강제로 손님 추가 
    /// </summary>
    public void ForceInCustomer()
    {
        if (CafeManager.inst.timeEnd)
            return;

        if (spawnTimer <= 2.0f)
        {
            InCustomer();
        }
        else if (2.0f < spawnTimer && spawnTimer <= 5.0f)
        {
            spawnTimer -= 2.0f;
        }
        else
        {
            spawnTimer -= 5.0f;
        }
    }

    public void OutCustomer()
    {
        Customer cust = customerQue.Dequeue();

        cust.getDrink = true;

        stayNum--;

        if(CafeManager.inst.timeEnd && customerQue.Count <= 0)
        {
            CafeManager.inst.lastCustmoerEnd = true;
        }
    }

    void SpawnOther(bool right)
    {
        string s = "OtherCar" + Random.Range(1, 5).ToString();
        GameObject obj = MemoryPoolManager.instance.GetObject(s);

        if(obj.TryGetComponent(out OtherCar other))
        {
            if(right)
            {
                other.SetOtherCar(new Vector3(45.0f, 1.7f, 8.5f), new Vector3(-25.5f, 1.7f, 8.5f));
                rightOtherSpawnTimer = Random.Range(1.0f, 10.0f);
            }
            else
            {
                other.SetOtherCar(new Vector3(-25.5f, 1.7f, 3.0f), new Vector3(45.0f, 1.7f, 3.0f));
                leftOtherSpawnTimer = Random.Range(1.0f, 10.0f);
            }
        }
    }
}