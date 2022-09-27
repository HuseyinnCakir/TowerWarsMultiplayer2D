using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class oyuncu : MonoBehaviour
{
    public GameObject top;
    public GameObject topcikisnokta;
    public ParticleSystem topatisefekt;
    public AudioSource topatmasesi;
    [Header("powerbar ayarlari")]
    Image powerbar;
    float powersayi;
    bool sonageldimi=false;
    Coroutine dongu;
    PhotonView pw;
    float atisyonu;
    bool atesaktifmi=false;
    void Start()
    {
        
        pw = GetComponent<PhotonView>();
        if (pw.IsMine)
        {
            powerbar = GameObject.FindWithTag("powerbar").GetComponent<Image>();

            if (PhotonNetwork.IsMasterClient)
            {
               // gameObject.tag = "oyuncu1";
                transform.position = GameObject.FindWithTag("olusacaknokta1").transform.position;
                transform.rotation = GameObject.FindWithTag("olusacaknokta1").transform.rotation;
                atisyonu = 2f;
            }
            else
            {
               // gameObject.tag = "oyuncu2";
                transform.position = GameObject.FindWithTag("olusacaknokta2").transform.position;
                transform.rotation = GameObject.FindWithTag("olusacaknokta2").transform.rotation;
                atisyonu = -2f;
            }
        }
        InvokeRepeating("oyunbasladimi", 0, 0.5f);
       
    }
    public void oyunbasladimi()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {


            if (pw.IsMine)
            {

                dongu = StartCoroutine(powerbarcalistir());
                CancelInvoke("oyunbasladimi");



            }
            else
            {
                StopAllCoroutines();
            }
        }
    }
        
    
    IEnumerator powerbarcalistir()
    {
        powerbar.fillAmount = 0;
        sonageldimi =false;
        while (true)
        {
      
            if (powerbar.fillAmount < 1 && !sonageldimi)
            {
                powersayi = 0.01f;
                powerbar.fillAmount += powersayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
            }
            else
            {
                sonageldimi = true;
                powersayi = 0.01f;
                powerbar.fillAmount -= powersayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);
                if (powerbar.fillAmount == 0)
                {
                    sonageldimi = false;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (pw.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PhotonNetwork.Instantiate("patlamaefekt", topcikisnokta.transform.position, topcikisnokta.transform.rotation, 0, null);
                topatmasesi.Play();
                
                GameObject obje =PhotonNetwork.Instantiate("bomba", topcikisnokta.transform.position, topcikisnokta.transform.rotation, 0, null);
                obje.GetComponent<PhotonView>().RPC("tagaktar",RpcTarget.All, gameObject.tag);
                Rigidbody2D rg = obje.GetComponent<Rigidbody2D>();
                rg.AddForce(new Vector2(atisyonu, 0f) * powerbar.fillAmount * 16f, ForceMode2D.Impulse);
                StopCoroutine(dongu);

            }
        }

    }
    public void poweroynasin()
    {

        dongu = StartCoroutine(powerbarcalistir());
    }
   

    public void sonuc(int deger)
    {
        
        if (pw.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
           
                if (deger == 1)
                {
                    PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);
                    PlayerPrefs.SetInt("galibiyet", PlayerPrefs.GetInt("galibiyet") + 1);

                    PlayerPrefs.SetInt("toplam_puan", PlayerPrefs.GetInt("toplam_puan") + 50);
                }
                else
                {
                    PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);

                    PlayerPrefs.SetInt("maglubiyet", PlayerPrefs.GetInt("maglubiyet") + 1);
                }
            
        }
        else
        {
           
                if (deger == 2)
                {
                    PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);
                    PlayerPrefs.SetInt("galibiyet", PlayerPrefs.GetInt("galibiyet") + 1);

                    PlayerPrefs.SetInt("toplam_puan", PlayerPrefs.GetInt("toplam_puan") + 50);
                }
                else
                {
                    PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);

                    PlayerPrefs.SetInt("maglubiyet", PlayerPrefs.GetInt("maglubiyet") + 1);
                }
            }
        }
        Time.timeScale = 0;
    }
    }

