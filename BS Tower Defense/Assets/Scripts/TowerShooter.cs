using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeBtwProjectiles;

    public bool _towerIsPlaced = false;

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

    private void ShootProjectiles()
    {
        

        if (currentTarget != null)
        {
            ShootLogic();

            EnemyBehavior enemyBehavior = currentTarget.GetComponent<EnemyBehavior>();
            enemyBehavior.takeDamage(50);
        }
    }

    void ShootLogic()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        (Vector3 direction, float _angleOfInstanciation) =
                ((currentTarget.transform.position - transform.position).normalized,
                Mathf.Atan2((currentTarget.transform.position - transform.position).normalized.y,
                (currentTarget.transform.position - transform.position).normalized.x) * Mathf.Rad2Deg);
        newBullet.transform.rotation = Quaternion.AngleAxis(_angleOfInstanciation, Vector3.forward);
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

        if (!_towerIsPlaced)
        {
            return;
        }
    }
}
