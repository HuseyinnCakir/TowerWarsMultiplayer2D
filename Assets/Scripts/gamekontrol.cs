using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamekontrol : MonoBehaviour
{
   
   
    [Header("can ayarlari ve islemler")]
    public Image oyuncu1_saglik_bar;
    public Image oyuncu2_saglik_bar;
    float oyuncu1_Saglik=100;
    float oyuncu2_Saglik=100;
    PhotonView pw;

    bool basladikmi;
    int limit;
    int olusturmasayisi;
    float beklemesuresi;
    public GameObject[] noktalar;
    GameObject oyuncu1;
    GameObject oyuncu2;
    bool oyunbittimi=false;

    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();
        basladikmi = false;
        limit = 4;
        beklemesuresi = 5;
    }
    IEnumerator canolustur()
    {
        olusturmasayisi = 0;
        while (true && basladikmi)
        {
            if (limit == olusturmasayisi)
                basladikmi = false;
            yield return new WaitForSeconds(15f);
            int olusandeger = Random.Range(0, 7);
            PhotonNetwork.Instantiate("odul",noktalar[olusandeger].transform.position,noktalar[olusandeger].transform.rotation, 0, null);
            Debug.Log("olustu");
            olusturmasayisi++;
        }
    }

    [PunRPC]
    public void darbever(int no,float darbegucu)
    {
        switch (no)
        {
            case 1:
                
                    oyuncu1_Saglik -= darbegucu;
                    oyuncu1_saglik_bar.fillAmount = oyuncu1_Saglik / 100;
                    if (oyuncu1_Saglik <= 0)
                    {
                    foreach (GameObject objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                    {
                        if (objem.gameObject.CompareTag("oyunsonupanel"))
                        {
                            objem.gameObject.SetActive(true);
                            GameObject.FindWithTag("oyunsonubilgi").GetComponent<TextMeshProUGUI>().text = "ikinci oyuncu galip";
                        }

                    }


                    kazanan(2);
                    /*
                        oyuncu1 = GameObject.FindWithTag("oyuncu1");
                        oyuncu2 = GameObject.FindWithTag("oyuncu2");
                        oyuncu1.GetComponent<PhotonView>().RPC("galip", RpcTarget.All);
                        oyuncu2.GetComponent<PhotonView>().RPC("maglup", RpcTarget.All);*/
                      
                    }
           break;
            case 2:
                
                    oyuncu2_Saglik -= darbegucu;
                    oyuncu2_saglik_bar.fillAmount = oyuncu2_Saglik / 100;
                    if (oyuncu2_Saglik<= 0)
                    {
                    foreach (GameObject objem in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                    {
                        if (objem.gameObject.CompareTag("oyunsonupanel"))
                        {
                            objem.gameObject.SetActive(true);
                            GameObject.FindWithTag("oyunsonubilgi").GetComponent<TextMeshProUGUI>().text = "birinci oyuncu galip";
                        }

                    }
                    kazanan(1);
                    /*
                    oyuncu1 = GameObject.FindWithTag("oyuncu1");
                    oyuncu2 = GameObject.FindWithTag("oyuncu2");
                    oyuncu2.GetComponent<PhotonView>().RPC("galip", RpcTarget.All);
                    oyuncu1.GetComponent<PhotonView>().RPC("maglup", RpcTarget.All);*/
                    
                }
                break;
        }
    }
    [PunRPC]
    public void saglikdoldur(int hangioyuncu)
    {
        switch (hangioyuncu)
        {
            case 1:

                oyuncu1_Saglik += 30;
                if (oyuncu1_Saglik >100)
                {
                    oyuncu1_Saglik = 100;
                    oyuncu1_saglik_bar.fillAmount = 1;
                }
                else
                oyuncu1_saglik_bar.fillAmount = oyuncu1_Saglik / 100;
              
                break;
            case 2:
                oyuncu2_Saglik += 30;
                if (oyuncu2_Saglik >100)
                {
                    oyuncu2_Saglik = 100;
                    oyuncu2_saglik_bar.fillAmount = 1;
                }
                else
                oyuncu2_saglik_bar.fillAmount = oyuncu2_Saglik / 100;
               
                break;
        }
    }
    [PunRPC]
    public void basla()
    {
        Debug.Log("basladan geliyorm");
        if (PhotonNetwork.IsMasterClient)
            basladikmi = true;
            StartCoroutine(canolustur());
        
    }
    public void anamenu()
    {
        GameObject.FindWithTag("sunucuyonetim").GetComponent<sunucuyonetim>().butonlami = true;
        PhotonNetwork.LoadLevel(0);
    }
    public void normalcikis()
    {
        
        PhotonNetwork.LoadLevel(0);
    }
    void kazanan(int deger)
    {
        if (!oyunbittimi)
        {
            GameObject.FindWithTag("oyuncu1").GetComponent<oyuncu>().sonuc(deger);
            GameObject.FindWithTag("oyuncu2").GetComponent<oyuncu>().sonuc(deger);
            oyunbittimi = true;
        }
       
    }

}
