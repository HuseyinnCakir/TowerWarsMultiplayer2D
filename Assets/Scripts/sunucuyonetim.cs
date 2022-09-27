using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class sunucuyonetim : MonoBehaviourPunCallbacks
{
    GameObject serverbilgi;
    public bool butonlami;
    // Start is called before the first frame update
    void Start()
    {
        serverbilgi = GameObject.FindWithTag("serverbilgi");
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
    }
    public override void OnConnectedToMaster()
    {
        serverbilgi.GetComponent<TextMeshProUGUI>().text = "servere baglandi";
        
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        serverbilgi.GetComponent<TextMeshProUGUI>().text = "lobiye baglandi";
        
    }
    public void randomgirisyap()
    {
        PhotonNetwork.LoadLevel(1);
        PhotonNetwork.JoinRandomRoom();
    }
    public void kurvegirisyap()
    {
        PhotonNetwork.LoadLevel(1);
        string odaadi = Random.Range(0, 100123).ToString();
        PhotonNetwork.JoinOrCreateRoom(odaadi, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }
    public override void OnJoinedRoom()
    {
        InvokeRepeating("bilgilerikontrolet", 0, 1);
        GameObject objem = PhotonNetwork.Instantiate("oyuncu", Vector3.zero, Quaternion.identity, 0, null);
        objem.GetComponent<PhotonView>().Owner.NickName = PlayerPrefs.GetString("kullaniciadi");
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            objem.gameObject.tag = "oyuncu2";
            GameObject.FindWithTag("kontrol").gameObject.GetComponent<PhotonView>().RPC("basla", RpcTarget.All);
        }
     
    }
    public override void OnLeftRoom()
    {
        /* PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac")+1);

         PlayerPrefs.SetInt("maglubiyet", PlayerPrefs.GetInt("maglubiyet") + 1);
         */
        if (butonlami)
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();
            PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);

            PlayerPrefs.SetInt("maglubiyet", PlayerPrefs.GetInt("maglubiyet") + 1);
        }
        Debug.Log("sen cýktýn");
    }
    public override void OnLeftLobby()
    {
        Debug.Log("lobiyee baglandi");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)

    {
        if (butonlami)
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Time.timeScale = 1;
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("rakip cýktý");
            PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);
            PlayerPrefs.SetInt("galibiyet", PlayerPrefs.GetInt("galibiyet") + 1);
        }
        /*
        Debug.Log("rakip cýktý");
        PlayerPrefs.SetInt("toplam_mac", PlayerPrefs.GetInt("toplam_mac") + 1);
        PlayerPrefs.SetInt("galibiyet", PlayerPrefs.GetInt("galibiyet") + 1);
        
        PlayerPrefs.SetInt("toplam_puan", PlayerPrefs.GetInt("toplam_puan") + 50);*/
        InvokeRepeating("bilgilerikontrolet", 0, 1);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        serverbilgi.GetComponent<TextMeshProUGUI>().text = "random odaya girilemedi";
    }
    public override void OnJoinRoomFailed(short returnCode, string message)

    {
        serverbilgi.GetComponent<TextMeshProUGUI>().text = "odaya girilemedi";
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        serverbilgi.GetComponent<TextMeshProUGUI>().text = "oda olusturulamadi";
    }
    public void bilgilerikontrolet()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            GameObject.FindWithTag("oyuncubekleniyor").SetActive(false);
            GameObject.FindWithTag("oyuncu1isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("oyuncu2isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[1].NickName;
            CancelInvoke("bilgilerikontrolet");
        }
        else
        {
            GameObject.FindWithTag("oyuncubekleniyor").SetActive(true);
            GameObject.FindWithTag("oyuncu1isim").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList[0].NickName;
            GameObject.FindWithTag("oyuncu2isim").GetComponent<TextMeshProUGUI>().text = "...";
        }
    }
}
