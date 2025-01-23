using UnityEngine;
using UnityEngine.UI;

public class CanvasPengaturan : MonoBehaviour
{

    public Slider Slider_SFX,Slider_BGM;


    private void OnEnable()
    {
        Slider_SFX.value = KumpulanSuara.instance.Source_sfx.volume;
        Slider_BGM.value = KumpulanSuara.instance.source_bgm.volume;
    }

    public void UbahVolume(bool SFX)
    {
        if (SFX)
        {
            KumpulanSuara.instance.Source_sfx.volume = Slider_SFX.value;
        }
        else
        {
            KumpulanSuara.instance.source_bgm.volume = Slider_BGM.value;
        }
    }

}
