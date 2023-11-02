using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LockedUnitController : MonoBehaviour
{ 
    [Header(("Settings"))]
    [SerializeField] private int price;

    [SerializeField] private int ID;

    [Header("Objects")] 
    [SerializeField] private TextMeshPro priceText;

    [SerializeField] private GameObject lockedUnit;

    [SerializeField] private GameObject unLockedUnit;

    private bool isPurchased;
    private string keyUnit ="KeyUnit";
    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price.ToString();
        LoadUnit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !isPurchased)
        {
            UnlockUnit();
            //ürünü paran yeterse aç
        }
    }

    private void UnlockUnit()
    {
        if (CashManager.instance.TryBuyThisUnit(price))
        {
            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
            Unlock();
            Saveunit();
        }
        //para kontrolü yap, yeterli ise aç.
    }

    private void Unlock()
    {
        isPurchased = true;
        lockedUnit.SetActive(false);
        unLockedUnit.SetActive(true);
    }

    private void Saveunit()
    {
        string key = keyUnit + ID.ToString();
        //"KeyUnit1"
        //"KeyUnit2"
        //"KeyUnit3" gibi benzersiz kayıtlar tutmamızı sağladık.
        PlayerPrefs.SetString(key,"saved");
    }

    private void LoadUnit()
    {
        string key = keyUnit + ID.ToString();
        string status = PlayerPrefs.GetString(key);
        if (status.Equals("saved"))
        {
            Unlock();
        }
    }
    
    
}
