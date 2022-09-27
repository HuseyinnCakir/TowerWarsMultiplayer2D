using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ortadakikutu : MonoBehaviour
{
    float saglik=100;
    public GameObject kutucanvas;
    public Image healtbar;
    GameObject kontrol;
    PhotonView pw;
    AudioSource kutuyokolmasesi;
    void Start()
    {
        kontrol = GameObject.FindWithTag("kontrol");
        pw = GetComponent<PhotonView>();
        kutuyokolmasesi = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update


    [PunRPC]
    public void darbeal(float darbegucu)
    {
        if (pw.IsMine)
        {
            saglik -= darbegucu;
            healtbar.fillAmount = saglik / 100;

            if (saglik <= 0)
            {
                //kontrol.GetComponent<gamekontrol>().ses_Ve_efekt_olustur(2, gameObject);


                PhotonNetwork.Instantiate("kutu_kirilma", transform.position, transform.rotation, 0, null);
                kutuyokolmasesi.Play();
                PhotonNetwork.Destroy(gameObject);

            }
            else
            {
                StartCoroutine(canvascikar());
            }
        }
    }
    IEnumerator canvascikar()
    {
        if (!kutucanvas.activeInHierarchy)
        {
            kutucanvas.SetActive(true);
            yield return new WaitForSeconds(2f);
            kutucanvas.SetActive(false);
        }
    }
}

