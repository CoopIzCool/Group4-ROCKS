using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementManager : MonoBehaviour
{
    public GameObject _towerObject;
    public GameObject _towerAOE2;
    public GameObject _towerLaser3;

    public Button _towerObjectButton;
    public Button _towerAOEButton;
    public Button _towerLaserButton;


    private int _towerSelectedCost;
    private GameObject _towerObjectSelected;

    private GameObject _dummyPlacement;
    private GameObject _hoverTile;

    public GameManager _gameManager;

    public Camera _camera;

    public LayerMask _mask;
    public LayerMask _towerMask;

    public bool isBuilding;

    private List<Vector2> _posTowers = new List<Vector2>();

    private void Start()
    {
        StartBuilding();
    }

    public void GetCurrentTile() //mouse position is being updated and if the mouse position
                                 //and its raycast hits a tile with a collider
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, 0f, _mask);
        

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.CompareTag("Tile")) //comparing with the gameobject that has the tag of "Tile"
                {
                    _hoverTile = hits[i].collider.gameObject; //if yes, then hits with the collider (the raycast)
                    break;
                }
            }
        }
        else
        {
            _hoverTile = null;
        }
    }

    public bool CheckForTower() //checking if a tower already exists in a tile, if not then you can place it
    {
        bool towerOnSlot = false; // bool for if the tower is on a slot/tile or not
        Collider2D[] cols = Physics2D.OverlapPointAll(_camera.ScreenToWorldPoint(Input.mousePosition), _towerMask);

        if (cols.Length > 0)
        {
            foreach (Collider2D collider in cols)
            {
                if (collider.gameObject.CompareTag("Tower"))
                {
                    Tower toower = collider.gameObject.GetComponent<Tower>();

                    if (toower != null)
                    {
                        //checking for the number of towers in vicinity to each other
                        Vector2 posTower = collider.transform.position;

                        foreach (Vector2 posExisting in _posTowers)
                        {
                            if (Vector2.Distance(posTower, posExisting) < 0.5f) //if they are closer than 0.5f, then there
                                                                                //is a tower on the slot/tile, therefore dont place a tower there
                            {
                                towerOnSlot = true;
                                break;
                            }
                        }

                        if (!towerOnSlot)
                        {
                            _posTowers.Add(posTower);
                        }

                        break;
                    }
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
                if (_gameManager.Money >= _towerSelectedCost) // if the gamemanger money tracking variable has more money then the cost for the tower, then start the logic
                {
                    GameObject newTowerObj = Instantiate(_towerObjectSelected); //instanciating the selected prefab of the tower
                    newTowerObj.layer = LayerMask.NameToLayer("Tower");
                    newTowerObj.transform.position = _hoverTile.transform.position;
                    _gameManager.moneyEarned(-_towerSelectedCost);
                } 
                else
                {
                    Debug.Log("You do not have enough money to place this tower");
                }
            }
        }
    }

    public void DistBtwnTower()
    {

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

    

    private void TowerSelect(GameObject towerPrefab, int towerCost)
    {

        // logic for the tower select buttons, when the buttons are clicked the
        // dummyplacement is referenced with the selected tower object prefab variable and is thus instantiated
        _towerObjectSelected = towerPrefab;
        _towerSelectedCost = towerCost;

        if (_dummyPlacement != null)
        {
            Destroy(_dummyPlacement);
        }

        _dummyPlacement = Instantiate(_towerObjectSelected);

        if (_dummyPlacement.GetComponent<Tower>() != null)
        {
            Destroy(_dummyPlacement.GetComponent<Tower>());
        }

         _towerObjectSelected = towerPrefab;
    }


    private void OnEnable()
    {
        //on button enable
        TowerButtonListeners();
    }

    private void OnDisable()
    {
        //on button disable
        Tower1ButtonRemoveListener();
        Tower2ButtonRemoveListener();
        Tower3ButtonRemoveListener();
    }


    public void TowerButtonListeners()
    {
        // button listener methods for the tower buttons
        Tower1ButtonListener();
        TowerAOE2ButtonListener();
        TowerLaser3ButtonListener();
    }
    public void Tower1ButtonListener()
    {
        _towerObjectButton.onClick.AddListener(() =>
        TowerSelect(_towerObject, _gameManager._costTower1)); // in this case the player selects button 1
    }

    public void Tower1ButtonRemoveListener()
    {
        _towerObjectButton.onClick.RemoveAllListeners();
    }

    public void TowerAOE2ButtonListener()
    {
        _towerAOEButton.onClick.AddListener(() =>
      TowerSelect(_towerAOE2, _gameManager._costTower2)); // in this case the player selects button 2
    }

    public void Tower2ButtonRemoveListener()
    {
        _towerAOEButton.onClick.RemoveAllListeners();
    }

    public void TowerLaser3ButtonListener()
    {
        _towerLaserButton.onClick.AddListener(() =>
      TowerSelect(_towerLaser3, _gameManager._costTower3)); // in this case the player selects button 3
    }

    public void Tower3ButtonRemoveListener()
    {
        _towerLaserButton.onClick.RemoveAllListeners();
    }
}


