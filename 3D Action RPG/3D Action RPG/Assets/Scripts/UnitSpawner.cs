using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnTransform;
    [SerializeField] private Transform enemySpawnTransform;

    private List<GameObject> spawnList = new List<GameObject>();

    public void SpawnUnit(GameObject unit, UnitType type)
    {
        var unitSpawn = Instantiate(unit);

        switch (type)
        {
            case UnitType.Player:
                {
                    unitSpawn.transform.position = playerSpawnTransform.position;
                }
                break;
            case UnitType.Enemy:
                {
                    var unitSpawnScript = unitSpawn.GetComponent<Enemy>();

                    unitSpawnScript.SetStartTransform(enemySpawnTransform);
                    unitSpawn.transform.position = enemySpawnTransform.position;
                }
                break;
        }
    }

    public void ClearUnit()
    {
        foreach (var unit in spawnList)
            if (unit != null)
                Destroy(unit);
        spawnList.Clear();
    }

    public enum UnitType { Player = 0, Enemy = 1 }
}
