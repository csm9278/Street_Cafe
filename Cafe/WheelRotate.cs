using UnityEngine;

public class WheelRotate : MonoBehaviour
{

    private void Start() => StartFunc();

    private void StartFunc()
    {
         
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        this.transform.Rotate(Vector3.right);
    }
}