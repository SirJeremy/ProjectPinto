﻿using UnityEngine;
public class GateTile : GameTile {
    private bool isOpen = false;
    private bool isWatitingToChange = false;

    private void Start() {
        isOpen = IsTraverseable;
        SetRenderer(!IsTraverseable);
    }
    private void OnEnable() {
        EventManager.OnButtonDown += OnButtonDown;
        EventManager.OnPlayerLocationChange += OnPlayerLocationChange;
    }
    private void OnDisable() {
        EventManager.OnButtonDown -= OnButtonDown;
        EventManager.OnPlayerLocationChange -= OnPlayerLocationChange;
    }

    private void OnButtonDown(EColor color) {
        //Location is from GameTile
        if(colorChannel == color) {
            isOpen = !isOpen;
            if(!isWatitingToChange) {
                if(BoardManager.Instance.GameBoard.CurrentPlayerPosition == Location)
                    isWatitingToChange = true;
                else
                    ChangeTraversability();
            }
        }
    }
    private void OnPlayerLocationChange(IndexVector playerLocation) {
        if(isWatitingToChange && Location != playerLocation) {
            isWatitingToChange = false;
            ChangeTraversability();
        }
    }

    private void ChangeTraversability() {
        IsTraverseable = isOpen;
        SetRenderer(!IsTraverseable);
        EventManager.AnnounceOnTraversabilityChange(Location, IsTraverseable);
    }

}
