using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRot : MonoBehaviour
{

    public Transform pivot;
    public Transform barrel;

    public TowerShooter tower;

    private void Update()
    {
        if (tower != null)
        {
            if (tower.currentTarget != null)
            {
                Vector2 relative = tower.currentTarget.transform.position - pivot.position;
                float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;


                Vector3 newRot = new Vector3(0, 0, angle);
                pivot.localRotation = Quaternion.Euler(newRot);
            }
        }
    }
}
