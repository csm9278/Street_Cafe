using UnityEngine;

public class Ice : MonoBehaviour
{
    Vector3 firstPos;

    private void OnDisable()
    {
        this.transform.localPosition = firstPos;
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
        firstPos = this.transform.localPosition;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }
}