using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLaser : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeBtwProjectiles;

    private Gradient gradient = new Gradient();

    public GameObject currentTarget;

    private float _nextTimeToShoot;

    public GameObject _laserObject;

    public bool _towerIsPlaced = false;

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


            (_laserRenderer.startColor, _laserRenderer.endColor) = (Color.red, Color.blue);
            LaserGradient();

            _laserRenderer.colorGradient = gradient;

            (_laserRenderer.startWidth, _laserRenderer.endWidth) = (0.3f, 0.2f);

            EnemyBehavior _enemyB = currentTarget.GetComponent<EnemyBehavior>();
            _enemyB.takeDamage(_damage);

            LaserEnabler(_laserShootObject);
            StartCoroutine(LaserDisabler(_laserShootObject, 1f));
        }
    }

    public void LaserGradient()
    {
        /* https://docs.unity3d.com/ScriptReference/LineRenderer-colorGradient.html */


        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.blue, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );

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


        if (!_towerIsPlaced)
        {
            return;
        }
    }
}
