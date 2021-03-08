﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameData data;

    //ui elements
    [SerializeField]
    private Image hpBar;
    [SerializeField]
    private Image xpBar;
    [SerializeField]
    private Text currentCoins;

    [SerializeField]
    private Transform enemyUIParent;
    [SerializeField]
    private Transform partyMemberUIParent;

    private int partyMemberYOffset;
    private int partyMemberY;

    // Start is called before the first frame update
    void Start()
    {
        data = new GameData();
        LoadEnemy();
        partyMemberYOffset = -133;
        partyMemberY = partyMemberYOffset * -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleEnemyDeath(int coinDrop, int xpDrop) {
        data.HandleEnemyDeath(coinDrop, xpDrop);
        currentCoins.text = data.currentCoins.ToString();
        xpBar.fillAmount = (float)data.xp / data.XP_TO_LEVEL[data.currentFloor - 1];
        if (data.charactersToUnlock.Count > 0) {
            if (data.charactersToUnlock[0].Item1 / 2 <= data.currentCoins)
            {
                GameObject newMember = (GameObject)Instantiate(Resources.Load("partymembers/" + data.charactersToUnlock[0].Item2), partyMemberUIParent);
                newMember.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, partyMemberY, 0);
                partyMemberY += partyMemberYOffset;
                data.charactersToUnlock.RemoveAt(0);
            }
        }
        LoadEnemy();
	}

    public void LoadEnemy() {
        Instantiate(Resources.Load("enemyprefabs/animated-shrub"), enemyUIParent);
	}

    public void UnlockPartyMember(PartyMemberData memberData) {
        data.UnlockPartyMember(memberData);
        currentCoins.text = data.currentCoins.ToString();
	}
}