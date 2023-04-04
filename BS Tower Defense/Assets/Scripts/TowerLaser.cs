using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLaser : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeBtwProjectiles;



    public GameObject currentTarget;

    private float _nextTimeToShoot;

    public GameObject _laserObject;

    // Start is called before the first frame update
    void Start()
    {
        _nextTimeToShoot = Time.time;
    }

    private void UpdateNearestEnemy()
    {
        GameObject currentNearestEnemy = null;

        float distance = Mathf.Infinity;

        foreach (GameObject enemy in Enemies._enemies)
        {
            if (enemy != null && enemy.activeInHierarchy)
            {
                if ((transform.position - enemy.transform.position).magnitude < distance)
                {
                    distance = (transform.position - enemy.transform.position).magnitude;
                    currentNearestEnemy = enemy;
                }
            }
        }

        if (distance <= _range)
        {
            currentTarget = currentNearestEnemy;
        }
        else
        {
            currentTarget = null;
        }

    }

    public void LaserWeaponShooter()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateNearestEnemy();

        if (Time.time >= _nextTimeToShoot)
        {
            if (currentTarget != null)
            {
                LaserWeaponShooter();
                _nextTimeToShoot = Time.time + _timeBtwProjectiles;
            }
        }
    }
}
