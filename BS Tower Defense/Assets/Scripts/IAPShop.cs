using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

public class IAPShop : MonoBehaviour
{
    [SerializeField]
    public int gems;
    [SerializeField]
    public TMP_Text txtGemsBalance;



    public void OnPurchaseComplete(Product product)
    {


        if (product.definition.id == "1")
        {

        }

        if (product.definition.id == "2")
        {

        }

        if (product.definition.id == "3")
        {

        }

        if (product.definition.id == "4")
        {

        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
