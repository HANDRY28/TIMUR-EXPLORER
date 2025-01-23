using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Obj_Drag : MonoBehaviour
{
    [HideInInspector]public Vector2 savePosition;
    [HideInInspector]public bool IsDiAtasObj;

    Transform SaveObj;

    public int ID;
    public TextMeshProUGUI Teks;

    [Space]

    public UnityEvent OnDragBenar;

    void Start()
    {
        // Menyimpan posisi awal objek
        savePosition = transform.position;
    }

    private void OnMouseDown()
    {
        // Opsional: Tambahkan efek visual saat objek di-drag
        Debug.Log("Mouse down on object!");
        KumpulanSuara.instance.Panggil_Sfx(0);
    }

    private void OnMouseDrag()
    {

      

        // Menghitung posisi kursor dalam world space
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z; // Tetapkan z sesuai objek
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Memindahkan objek ke posisi kursor
        transform.position = worldPosition;
    }

    private void OnMouseUp()
    {
        // Mengembalikan objek ke posisi awal jika diperlukan
        //transform.position = savePosition;
        if(IsDiAtasObj)
        {
            int ID_TempatDrop = SaveObj.GetComponent<Tempat_Drop>().ID;

            if(ID == ID_TempatDrop)
            {
                transform.SetParent(SaveObj);
                transform.localPosition = Vector3.zero;
                transform.localScale = new Vector2(1.09f, 1.09f);

                SaveObj.GetComponent<Rigidbody2D>().simulated = false;
                SaveObj.GetComponent<BoxCollider2D>().enabled = false;

                gameObject.GetComponent<BoxCollider2D>().enabled = false;

                OnDragBenar.Invoke();

                Game_System.instance.DataSaatIni++;
                Data.DataScore += 200;

                KumpulanSuara.instance.Panggil_Sfx(1);
            }
            else
            {
                transform.position = savePosition;

                Data.DataDarah--;
                KumpulanSuara.instance.Panggil_Sfx(2);
            }


        }
        else
        {
            transform.position = savePosition;
        }
    }

    private void OnTriggerStay2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("Drop"))
        {
            IsDiAtasObj = true;
            SaveObj = trig.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Drop"))
        {
            IsDiAtasObj = false;
        }
    }
}
