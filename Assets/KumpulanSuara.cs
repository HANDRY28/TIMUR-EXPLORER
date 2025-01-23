using UnityEngine;

public class KumpulanSuara : MonoBehaviour
{

    public static KumpulanSuara instance;

    public AudioClip[] Clip;

    public AudioSource Source_sfx;

    public AudioSource source_bgm;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    public void  Panggil_Sfx(int id)
    {
        Source_sfx.PlayOneShot(Clip[id]);
    }
}
