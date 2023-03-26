using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject _towerObject;
    private GameObject _dummyPlacement;
    private GameObject _hoverTile;

    public Camera _camera;

    public LayerMask _mask;
    public LayerMask _towerMask;

    public bool isBuilding;

    private void Start()
    {
        StartBuilding();
    }
    public void GetCurrentTile()
    {
        Vector2 mousePos = GetMousePosition();

        RaycastHit2D hit = Physics2D.Raycast(mousePos, new Vector2(0, 0), 0.1f, _mask, -100, 100);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject)
            {
                _hoverTile = hit.collider.gameObject;
            }
        }
    }
    public Vector2 GetMousePosition()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool CheckForTower()
    {
        bool towerOnSlot = false;
        Vector2 mousePos = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePos, new Vector2(0, 0), 0.1f, _towerMask, -100, 100);

        if (hit.collider != null)
        {
            towerOnSlot = true;
        }

        return towerOnSlot;
    }


    public void PlaceBuilding()
    {
        if (_hoverTile != null)
        {
            if (CheckForTower() == false)
            {
                GameObject newTowerObj = Instantiate(_towerObject);
                newTowerObj.layer = LayerMask.NameToLayer("Tower");
                newTowerObj.transform.position = _hoverTile.transform.position;

                EndBuilding();
            }
        }
    }

    public void StartBuilding()
    {
        isBuilding = true;
        _dummyPlacement = Instantiate(_towerObject);

        if (_dummyPlacement.GetComponent<Tower>() != null)
        {
            Destroy(_dummyPlacement.GetComponent<Tower>());
        }
        
    }


    public void EndBuilding()
    {
        isBuilding = false;

        if (_dummyPlacement != null)
        {
            Destroy(_dummyPlacement);
        }
    }

    public void Update()
    {
        if (isBuilding = true)
        {
            if (_dummyPlacement != null)
            {
                GetCurrentTile();

                if (_hoverTile != null)
                {
                    _dummyPlacement.transform.position = _hoverTile.transform.position;
                }
            }

        }

       //Debug.Log("MOUSE POS");
    }
}


