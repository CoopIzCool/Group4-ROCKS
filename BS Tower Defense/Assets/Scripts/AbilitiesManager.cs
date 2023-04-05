using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
    #region Fields
    private bool _frozen;
    private float _freezeTimer;
    private float _freezeCooldown;
    private float _nukeCooldown;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Fields
        _freezeTimer = 0f;
        _freezeCooldown = 15f;
        _nukeCooldown = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust timers and cooldowns
        _nukeCooldown = _nukeCooldown > 0f ? _nukeCooldown - Time.deltaTime : 0f;
        _freezeCooldown = _freezeCooldown > 0f ? _freezeCooldown - Time.deltaTime : 0f;
        _freezeTimer = _freezeTimer > 0f ? _freezeTimer - Time.deltaTime : 0f;
        
        // If the timer reaches 0 and was frozen last frame
        if (_freezeTimer == 0 && _frozen)
        {
            // Get all enemies
            GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Unfreeze each enemy
            foreach (GameObject _enemy in _enemies)
            {
                _enemy.GetComponent<PathMovement>().Agent.enabled = true;
            }

            _frozen = false;
        }


        // If the user presses Q, has no cooldown, and has the premium funds - initiate NUKE
        // TODO: change this to a clickable UI button function, needs separate method
        if (Input.GetKeyDown(KeyCode.Q) && _nukeCooldown == 0f)
        {
            Nuke();
        }

        // If the user presses E, has no cooldown, and has the premium funds - initiate FREEZE
        // TODO: change this to a clickable UI button function, needs separate method
        if (Input.GetKeyDown(KeyCode.E) && _freezeCooldown == 0f)
        {
            Freeze();
        }
    }

    public void Nuke()
    {
        if (_nukeCooldown == 0f)
        {
            // TODO: Reduce premium funds

            // Get all enemies
            GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Kill each enemy
            foreach (GameObject _enemy in _enemies)
            {
                _enemy.GetComponent<EnemyBehavior>().Death(true);
            }

            // Reset the cooldown
            _nukeCooldown = 30f;
        }
    }

    public void Freeze()
    {
        if (_freezeCooldown == 0f)
        {
            // Start timer
            _freezeTimer = 5f;

            // TODO: Reduce premium funds

            // Get all enemies
            GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");

            // Freeze each enemy
            foreach (GameObject _enemy in _enemies)
            {
                _enemy.GetComponent<PathMovement>().Agent.enabled = false;
            }

            _frozen = true;
        }
    }
}
