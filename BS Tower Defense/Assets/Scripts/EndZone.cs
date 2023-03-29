using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Developed by Ryan Cooper 2021
public class EndZone : MonoBehaviour
{
    #region Fields
    public GameManager gm;
    #endregion

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            gm.breach(collision.gameObject.GetComponent<EnemyBehavior>().damage);
            collision.gameObject.GetComponent<EnemyBehavior>().Death(false);
        }
    }
}
