using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject MENU;
    public GameObject INFO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MENU.SetActive(true);
        INFO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BMULAI(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void BINFO()
    {
        MENU.SetActive(false);
        INFO.SetActive(true);
    }

    public void KEMBALI()
    {
        MENU.SetActive(true);
        INFO.SetActive(false);
    }
}
