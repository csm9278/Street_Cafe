using UnityEngine;
using UnityEngine.UI;

public class PackageObject : MonoBehaviour
{
    public Text upArrowText;

    bool isDown = false;
    bool isUp = false;
    private void OnEnable()
    {
        PlayerDrink.inst.MovePos(false);
        if (CafeManager.inst.makeOrder.iceHotType == GlobalData.IceHot.Hot)
            upArrowText.text = "Ä¿¹ö";
        else if(CafeManager.inst.makeOrder.iceHotType == GlobalData.IceHot.Ice)
        {
            PlayerDrink.inst.IceJacketOn();
            upArrowText.text = "»¡¶§";
        }
    }

    private void OnDisable()
    {
        PlayerDrink.inst.MovePos(true);
    }

    private void Start() => StartFunc();

    private void StartFunc()
    {
         
    }

    private void Update() => UpdateFunc();

    private void UpdateFunc()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundManager.instance.PlayEffSound(GlobalData.hitSounds, 0.5f);
            PlayerDrink.inst.Package(CafeManager.inst.makeOrder.iceHotType, 0);

            isDown = true;
            if (isUp)
                CafeManager.inst.makeOrder.packaged = true;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.instance.PlayEffSound(GlobalData.hitSounds, 0.5f);
            PlayerDrink.inst.Package(CafeManager.inst.makeOrder.iceHotType, 1);

            isUp = true;
            if (isDown)
                CafeManager.inst.makeOrder.packaged = true;
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            this.gameObject.SetActive(false);
            CafeManager.inst.makeState = CafeManager.MakeState.Give;
            Player.isInteract = false;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            Player.isInteract = false;
        }
    }

    public void ResetFunc()
    {
        isUp = false;
        isDown = false;
    }
}