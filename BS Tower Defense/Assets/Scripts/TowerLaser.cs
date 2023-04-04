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
        GameObject _laserShootObject = Instantiate(_laserObject, transform.position, Quaternion.identity);
        LineRenderer _laserRenderer = _laserObject.GetComponent<LineRenderer>();

        if (currentTarget != null)
        {
            //Vector3 _targetPos = currentTarget.transform.position;

            _laserRenderer.SetPosition(0, transform.position);
            _laserRenderer.SetPosition(1, currentTarget.transform.position);

            EnemyBehavior _enemyB = currentTarget.GetComponent<EnemyBehavior>();
            _enemyB.takeDamage(_damage);

            LaserEnabler(_laserShootObject);
            StartCoroutine(LaserDisabler(_laserShootObject, 1f));
        }
    }

    IEnumerator LaserDisabler(GameObject _laserObj, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        _laserObj.GetComponent<LineRenderer>().enabled = false;
    }

    public void LaserEnabler(GameObject _laser)
    {
        _laser.GetComponent<LineRenderer>().enabled = true;
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
