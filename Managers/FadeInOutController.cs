using UnityEngine;

public class FadeInOutController : MonoBehaviour
{
    public enum FadeInOutType
    {
        In,
        Out
    }

    public FadeInOutType type = FadeInOutType.Out;
    Vector3 firstScale;
    public bool fadeStart = false;
    bool fadeEnd = false;

    float curPercent = 1.0f;
    public float fadeTime = 2.0f;

    private void Start() => StartFunc();

    private void StartFunc()
    {
        firstScale = this.transform.localScale;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (!fadeStart)
            return;

        if(!fadeEnd)
        {
            if(type == FadeInOutType.Out)
            {
                curPercent -= Time.deltaTime / fadeTime;

                Vector3 scale = firstScale * curPercent;
                this.transform.localScale = scale;
                if(curPercent <= 0.0f)
                {
                    this.transform.localScale = Vector3.zero;
                    fadeEnd = true;
                }
            }
            else if(type == FadeInOutType.In)
            {

            }
        }
    }
}