using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeBtwProjectiles;



    public GameObject currentTarget;

    private float _nextTimeToShoot;

    public GameObject bullet;

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
                float _distance = (transform.position - enemy.transform.position).magnitude;

                if (_distance < distance)
                {
                    distance = _distance;
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

    private void ShootProjectiles()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            float _angleOfInstanciation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newBullet.transform.rotation = Quaternion.AngleAxis(_angleOfInstanciation, Vector3.forward);


            EnemyBehavior enemyBehavior = currentTarget.GetComponent<EnemyBehavior>();
            enemyBehavior.takeDamage(50);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNearestEnemy();

        if (Time.time >= _nextTimeToShoot)
        {
            if (currentTarget != null)
            {
                ShootProjectiles();
                _nextTimeToShoot = Time.time + _timeBtwProjectiles;
            }
        }
    }
}
