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
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, 0f, _mask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.CompareTag("Tile"))
                {
                    _hoverTile = hits[i].collider.gameObject;
                    break;
                }
            }
        }
        else
        {
            _hoverTile = null;
        }
    }

    public bool CheckForTower()
    {
        bool towerOnSlot = false;
        Collider2D[] cols = Physics2D.OverlapPointAll(_camera.ScreenToWorldPoint(Input.mousePosition), _towerMask);

        if (cols.Length > 0)
        {
            foreach (Collider2D collider in cols)
            {
                if (collider.gameObject.CompareTag("Tower"))
                {
                    towerOnSlot = true;
                    break;
                }
            }
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
        if (isBuilding == true)
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

        if (Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();

            //Debug.Log("mouse clicked");
        }

        //Debug.Log("MOUSE POS");
    }
}


