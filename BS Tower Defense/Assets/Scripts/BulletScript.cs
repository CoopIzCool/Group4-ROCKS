using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void Update()
    {
        transform.position += transform.right * 0.25f;
    }
}
