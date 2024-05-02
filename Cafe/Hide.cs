using UnityEngine;

public class Hide : MonoBehaviour
{

    private void Start() => StartFunc();

    private void StartFunc()
    {
        Material mat = this.GetComponent<MeshRenderer>().material;
        mat.renderQueue = 3002;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        
    }
}