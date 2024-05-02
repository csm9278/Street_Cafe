using UnityEngine;
using UnityEngine.UI;

public class WaterFill : MonoBehaviour
{
    public float fillTime = 5.0f;
    float curFillTime = 0.0f;

    float delay = 0.0f;
    public bool isEnd = false;

    public Image fillImage;
    public Text failText;

    AudioSource audio;
    bool isStart = false;

    Color hotColor = new Color(241 / 255f, 172 / 255f, 171 / 255f);
    Color iceColor = new Color(171 / 255f, 227 / 255f, 241 / 255f);

    private void Start() => StartFunc();

    private void StartFunc()
    {
        this.transform.localScale = Vector3.zero;     
        fillImage.fillAmount = 0.0f;

        audio = GetComponent<AudioSource>();
        audio.clip = Resources.Load(GlobalData.waterFillSounds[0]) as AudioClip;
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if (isEnd)
            return;

        if(delay >= 0.0f)
        {
            delay -= Time.deltaTime;
            failText.color = new Color(255, 0, 0, delay / 2);
            return;
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            if(!isStart)
            {
                audio.Play();
                isStart = true;
            }

            curFillTime += Time.deltaTime / fillTime;
            fillImage.fillAmount = curFillTime / 1.1f;
            Vector3 scale = new Vector3(0.75f + (0.25f * curFillTime), curFillTime, (0.75f + (0.25f * curFillTime)));
            this.transform.localScale = scale;
            if(curFillTime > 1.1f)
            {
                audio.Stop();
                SoundManager.instance.PlayEffSound(GlobalData.errorBeepClip);
                Debug.Log("실패");
                delay = 2.0f;
                curFillTime = 0.0f;
                failText.color = Color.red;
                this.transform.localScale = Vector3.zero;
                isStart = false;
            }
        }
        else
        {
            if (curFillTime <= 0.0f)
                return;

            if(curFillTime < 1.00f && curFillTime > 0.9f)
            {
                audio.Stop();
                isEnd = true;
                isStart = false;
                this.enabled = false;
            }
            else
            {
                audio.Stop();
                SoundManager.instance.PlayEffSound(GlobalData.errorBeepClip);
                delay = 2.0f;
                curFillTime = 0.0f;
                failText.color = Color.red;
                fillImage.fillAmount = 0.0f;
                this.transform.localScale = Vector3.zero;
                isStart = false;
            }
        }
    }

    /// <summary>
    /// Hot,Ice 종류에 따라 WaterFill 이미지 칼라값 변경함수
    /// </summary>
    public void ChangeFillImage()
    {
        if (CafeManager.inst.makeOrder.iceHotType == GlobalData.IceHot.Hot)
            fillImage.color = hotColor;
        else
            fillImage.color = iceColor;
    }

    public void ResetFill()
    {
        curFillTime = 0.0f;
        this.transform.localScale = Vector3.zero;
        fillImage.fillAmount = 0.0f;
        isEnd = false;
        isStart = false;
    }
}