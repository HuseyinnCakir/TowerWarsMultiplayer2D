using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class anamenukontrol : MonoBehaviour
{
    public GameObject ilkpanel;
    public GameObject ikincipanel;
    public InputField kullaniciadi;
    public Text varolankullaniciadi;
    public TextMeshProUGUI[] istatistik;
    public TextMeshProUGUI serverbilgi;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("kullaniciadivarmi"))
        {
            PlayerPrefs.SetInt("toplam_mac", 0);
            PlayerPrefs.SetInt("galibiyet", 0);
            PlayerPrefs.SetInt("maglubiyet", 0);
            PlayerPrefs.SetInt("toplam_puan", 0);
            ilkpanel.SetActive(true);
            degerleriyaz();
        }
        else
        {
            ikincipanel.SetActive(true);
            varolankullaniciadi.text = PlayerPrefs.GetString("kullaniciadi");
            degerleriyaz();
        }
    }
        
    

    public void kullaniciadikaydet()
    {
        PlayerPrefs.SetInt("kullaniciadivarmi", 1);
        PlayerPrefs.SetString("kullaniciadi", kullaniciadi.text);
        ilkpanel.SetActive(false);
        ikincipanel.SetActive(true);
        varolankullaniciadi.text = PlayerPrefs.GetString("kullaniciadi");
    }
    void degerleriyaz()
    {
        istatistik[0].text = PlayerPrefs.GetInt("toplam_mac").ToString();
        istatistik[1].text = PlayerPrefs.GetInt("galibiyet").ToString();
        istatistik[2].text = PlayerPrefs.GetInt("maglubiyet").ToString();
        istatistik[3].text = PlayerPrefs.GetInt("toplam_puan").ToString();
    }
}
