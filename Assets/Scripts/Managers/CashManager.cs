using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    private int coins;
    private string keyCoins = "keyCoins";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void ExchangeProduct(ProductData productData)
    {
        AddCoin(productData.productPrice);
    }
    
    public void AddCoin(int price)
    {
        coins += price;
        DislplayCoins();
    }

    private void SpendCoin(int price)
    {
        coins -= price;
        DislplayCoins();
    }
  
    public bool TryBuyThisUnit(int price)
    {
        if (GetCoins() >= price)
        {
            SpendCoin(price);
            return true;
            //paranı harca 
        }
        return false;
    }
    

    // Start is called before the first frame update
    void Start()
    {
        LoadCash();
        DislplayCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCoins()
    {
        return coins;
    }

    private void DislplayCoins()
    {
        UIManager.instance.ShowCoinCountOnScreen(coins);
        SaveCash();
    }

    private void LoadCash()
    {
        coins = PlayerPrefs.GetInt(keyCoins,0);
    }

    private void SaveCash()
    {
        PlayerPrefs.SetInt(keyCoins,coins);
    }
}
