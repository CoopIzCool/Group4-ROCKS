using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSpawners : MonoBehaviour
{
    public MobSpawner mobSpawner1;
    public MobSpawner mobSpawner2;

    public void mobspawners()
    {
        mobSpawner1.activateSpawner();
        mobSpawner2.activateSpawner();
    }
}
