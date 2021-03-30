using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocarArmas : MonoBehaviour
{
    public GameObject ObjectGun;
    public GameObject ObjectGun2;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ObjectGun.SetActive(true);
            ObjectGun2.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ObjectGun.SetActive(false);
            ObjectGun2.SetActive(true);
		   
        }
    }
}