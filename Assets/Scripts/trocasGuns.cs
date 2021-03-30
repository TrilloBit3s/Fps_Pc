using UnityEngine;
using System.Collections;

public class trocasGuns : MonoBehaviour {

    public GameObject Gun1, Gun2;

    void Start ()
    {
        Gun1.SetActive(true);
        Gun2.SetActive(false);
    }
 
    void Update () 
    {
        if (Input.GetKeyDown("1"))
        {
            Gun1.SetActive(true);
            Gun2.SetActive(false);
        }
        if (Input.GetKeyDown("2"))
        {
            Gun1.SetActive(false);
            Gun2.SetActive(true);
        }
    }
}