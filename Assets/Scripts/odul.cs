using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class odul : MonoBehaviour
{
    PhotonView pw;
    // Start is called before the first frame update
    void Start()
    {
        pw = GetComponent<PhotonView>();
        StartCoroutine(yokol());
    }
    IEnumerator yokol()
    {
        yield return new WaitForSeconds(10f);
        if(pw.IsMine)
        PhotonNetwork.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
