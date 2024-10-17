using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int ownedGold = 0;
    internal int goldCollectedInLevel = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
        Instance = this;
    }

    internal void SetCollectedGold(int value) => goldCollectedInLevel += value;
    internal void AddOwnedGold() => ownedGold += goldCollectedInLevel;
}
