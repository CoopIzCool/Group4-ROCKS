using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    /*Reference: https://youtu.be/7sxF8JVR74c?list=PL5AKnriDHZs5a8De2wK_qqrwBUqjZo0hN */

    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeBtwProjectiles;

    [SerializeField] private GameObject _aoeEffectSprite;


    public bool _towerIsPlaced = false;
    public GameObject currentTarget;

    private float _nextTimeToShoot;

    // Start is called before the first frame update
    void Start()
    {
        _nextTimeToShoot = Time.time;
    }

    private void UpdateNearestEnemy()
    {
        GameObject currentNearestEnemy = null;

        float distance = Mathf.Infinity;
        
        foreach(GameObject enemy in Enemies._enemies)
        {
            if (enemy != null)
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
        if (currentTarget != null)
        {
            EnemyBehavior enemyBehavior = currentTarget.GetComponent<EnemyBehavior>();
            enemyBehavior.takeDamage(50);


            StartCoroutine(AOEffect(currentTarget.transform.position));
        }
    }

    public IEnumerator AOEffect(Vector3 pos)
    {
        GameObject sprite = Instantiate(_aoeEffectSprite, pos, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(sprite);
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

