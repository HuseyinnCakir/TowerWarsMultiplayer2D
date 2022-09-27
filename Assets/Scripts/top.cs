using Photon.Pun;
using UnityEngine;

public class top : MonoBehaviour
{
    float darbegucu;

    GameObject kontrol;
    GameObject oyuncu;
    AudioSource topyokolmasesi;
    PhotonView pw;
    int benkimim;
    void Start()
    {
        darbegucu = 20f;
        kontrol = GameObject.FindWithTag("kontrol");
        pw = GetComponent<PhotonView>();
        topyokolmasesi = GetComponent<AudioSource>();
    }
    [PunRPC]
    public void tagaktar(string tag)
    {

        oyuncu = GameObject.FindWithTag(tag);
        if (tag == "oyuncu1")
            benkimim = 1;
        else
        {
            benkimim = 2;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ortadakikutular"))
        {
            collision.gameObject.GetComponent<PhotonView>().RPC("darbeal", RpcTarget.All, darbegucu);

            oyuncu.GetComponent<oyuncu>().poweroynasin();
            
            PhotonNetwork.Instantiate("patlamaefekt", transform.position, transform.rotation, 0, null);
            topyokolmasesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("oyuncu2_kule") || collision.gameObject.CompareTag("oyuncu2"))
        {
            if (benkimim != 2)
            { 
                    kontrol.GetComponent<PhotonView>().RPC("darbever", RpcTarget.All, 2, darbegucu);
            }
                oyuncu.GetComponent<oyuncu>().poweroynasin();


                PhotonNetwork.Instantiate("patlamaefekt", transform.position, transform.rotation, 0, null);
                topyokolmasesi.Play();
                if (pw.IsMine)
                    PhotonNetwork.Destroy(gameObject);
            
        }
        if (collision.gameObject.CompareTag("oyuncu1_kule") || collision.gameObject.CompareTag("oyuncu1"))
        {
            if (benkimim != 1)
            {
                kontrol.GetComponent<PhotonView>().RPC("darbever", RpcTarget.All, 1, darbegucu);

            }
            


                PhotonNetwork.Instantiate("patlamaefekt", transform.position, transform.rotation, 0, null);
                topyokolmasesi.Play();
                if (pw.IsMine)
                    PhotonNetwork.Destroy(gameObject);

            
        }
        if (collision.gameObject.CompareTag("zemin"))
        {

            oyuncu.GetComponent<oyuncu>().poweroynasin();

            
            PhotonNetwork.Instantiate("patlamaefekt", transform.position, transform.rotation, 0, null);
            topyokolmasesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("engel"))
        {

            oyuncu.GetComponent<oyuncu>().poweroynasin();


            PhotonNetwork.Instantiate("patlamaefekt", transform.position, transform.rotation, 0, null);
            topyokolmasesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("odul"))
        {

            
            kontrol.GetComponent<PhotonView>().RPC("saglikdoldur", RpcTarget.All,benkimim);
            PhotonNetwork.Destroy(collision.gameObject);
            oyuncu.GetComponent<oyuncu>().poweroynasin();
            PhotonNetwork.Instantiate("patlamaefekt", transform.position, transform.rotation, 0, null);
            topyokolmasesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);
                

        }
        if(collision.gameObject.CompareTag("top"))
        {

            oyuncu.GetComponent<oyuncu>().poweroynasin();


            PhotonNetwork.Instantiate("patlamaefekt", transform.position, transform.rotation, 0, null);
            topyokolmasesi.Play();
            if (pw.IsMine)
                PhotonNetwork.Destroy(gameObject);

        }
    }
    // Update is called once per frame

}
