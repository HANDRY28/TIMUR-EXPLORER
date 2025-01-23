using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Data
{
    public static int DataLevel, DataScore, Datawaktu, DataDarah;
}

public class Game_System : MonoBehaviour
{

    public static Game_System instance;

    
    int MaxLevel = 5;

    [Header("Data Permainan")]
    public bool GameAktif;
    public bool GameSelesai;
    public bool SistemAcak;
    public int Target,DataSaatIni;
   


    [Header("Komponen UI")]
    public TextMeshProUGUI Teks_Level;
    public TextMeshProUGUI Teks_Waktu,Teks_Score;
    public RectTransform Ui_Darah;


    [Header ("Obj Gui")]
    public GameObject Gui_Pause;
    public GameObject Gui_transisi;


    [System.Serializable]
    public class DataGame
    {
        public string Nama;
        public Sprite Gambar;
        
    }

    [Header("Setingan Standar")]
    public DataGame[] DataPermainan;

    [Space]

    public Obj_TempatDrop[] Drop_Tempat;
    public Obj_Drag[] Drag_Obj;


    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameAktif = false;
        GameSelesai = false;
        AcakSoal();
        Target = Drop_Tempat.Length;
        if(SistemAcak)
            ResetData();

        DataSaatIni = 0;
        GameAktif = true;
    }

    void ResetData()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "game0")
        {
            Data.Datawaktu = 60 * 3;
            Data.DataScore = 0;
            Data.DataDarah = 5;
            Data.DataLevel = 0;
        }
    }

    // Update is called once per frame

    float s;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AcakSoal();

        if (GameAktif && !GameSelesai)
        {
            if(Data.Datawaktu > 0)
            {
                s += Time.deltaTime;
                if(s >= 1)
                {
                    Data.Datawaktu--;
                    s = 0;
                }
            }
            if (Data.Datawaktu <= 0)
            {
                GameAktif = false; // ini yang dimaksud tersebut
                GameSelesai = true;


                KumpulanSuara.instance.Panggil_Sfx(4);
                Gui_transisi.GetComponent<Ui_Control>().Btn_Pindah("GameSelesai");
            }
            if(Data.DataDarah <= 0)
            {
                GameAktif = false; // ini yang dimaksud tersebut
                GameSelesai = true;


                KumpulanSuara.instance.Panggil_Sfx(4);
                Gui_transisi.GetComponent<Ui_Control>().Btn_Pindah("GameSelesai");
            }

            if (DataSaatIni >= Target)
            {
                GameSelesai = true; //perhatikan ini dengan fungsi if tersebut
                GameAktif = false;

                if(Data.DataLevel < (MaxLevel - 1))
                {
                    Data.DataLevel++;

                    UnityEngine.SceneManagement.SceneManager.LoadScene("game" + Data.DataLevel);
                    // Gui_transisi.GetComponent<Ui_Control>().Btn_Pindah("game"+ Data.DataLevel);

                    KumpulanSuara.instance.Panggil_Sfx(3);
                }
                else
                {
                    Gui_transisi.GetComponent<Ui_Control>().Btn_Pindah("GameSelesai");
                    KumpulanSuara.instance.Panggil_Sfx(5);
                    KumpulanSuara.instance.Panggil_Sfx(6);
                }
            }
        }

        SetInfoUI();
    }

    [HideInInspector] public List<int> _AcakSoal = new List<int>();
    [HideInInspector] public List<int> _AcakPos = new List<int>();
    int rand;
    int rand2; // Deklarasi rand2
    public void AcakSoal()
    {
        _AcakSoal.Clear();
        _AcakPos.Clear();

        // Inisialisasi _AcakSoal
        _AcakSoal = new List<int>(new int[Drag_Obj.Length]);

        for (int i = 0; i < _AcakSoal.Count; i++)
        {
            rand = Random.Range(1, DataPermainan.Length);
            while (_AcakSoal.Contains(rand))
                rand = Random.Range(1, DataPermainan.Length);

            _AcakSoal[i] = rand;

            Drag_Obj[i].ID = rand - 1;
            Drag_Obj[i].Teks.text = DataPermainan[rand - 1].Nama;
        }

        // Inisialisasi _AcakPos
        _AcakPos = new List<int>(new int[Drop_Tempat.Length]);


        for (int i = 0; i < _AcakPos.Count; i++)
        {
            rand2 = Random.Range(1, _AcakSoal.Count + 1);
            while (_AcakPos.Contains(rand2))
                rand2 = Random.Range(1, _AcakSoal.Count + 1);

            _AcakPos[i] = rand2;

            Drop_Tempat[i].Drop.ID = _AcakSoal[rand2 - 1] - 1;
            Drop_Tempat[i].Gambar.sprite = DataPermainan[Drop_Tempat[i].Drop.ID].Gambar;

        }
    }


    public void SetInfoUI()
    {
        Teks_Level.text = (Data.DataLevel + 1).ToString();

        int Menit = Mathf.FloorToInt(Data.Datawaktu / 60);// 01
        int Detik = Mathf.FloorToInt(Data.Datawaktu % 60);// 30
        Teks_Waktu.text = Menit.ToString("00") + ":" + Detik.ToString("00");

        Teks_Score.text = Data.DataScore.ToString();

        Ui_Darah.sizeDelta = new Vector2(50f * Data.DataDarah, 44f);
    }

    public void Btn_Pause(bool pause)
    {
        if (pause)
        {
            GameAktif = false;
            Gui_Pause.SetActive(true);
        }
        else
        {
            GameAktif = true;
            Gui_Pause.SetActive(false);
        }
    }

}
