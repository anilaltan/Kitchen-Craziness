using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int plateSpawnedAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < plateSpawnedAmountMax) {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // player is empty handed
            if (platesSpawnedAmount > 0) {
                //there's at leat one plate here
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
