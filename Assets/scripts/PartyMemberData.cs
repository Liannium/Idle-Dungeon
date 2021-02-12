﻿using UnityEngine;

/// <summary>
/// <c>PartyMemberData</c> exists to store the data regarding party members that will need to be
/// saved between plays.
/// </summary>
public class PartyMemberData
{
    public static readonly int INJURED_INDICATOR = 1;
    public static readonly int DEATH_INDICATOR = 2;
    
    public string MemberName { get; private set; }
    public int MaxHealth { get; private set; }
    public int UnlockCost { get; private set; }
    public Sprite Locked { get; private set; }
    public Sprite Active { get; private set; }
    public int CurrentLevel { get; private set; }
    public int LevelCost { get; private set; }
    public int CurrentHealth { get; private set; }

    private float costMultiplier;

    /// <summary>
    /// Initializes the <c>PartyMemberData</c> object to its default values
    /// </summary>
    /// <param name="_MemberName">The name of the party member</param>
    /// <param name="_maxHealth">The party member's initial maximum health</param>
    /// <param name="_unlockCost">The cost to unlock that party member</param>
    /// <param name="_costMultiplier">How much the party member's cost changes each time the level 
    /// up</param>
    public PartyMemberData(string _MemberName, int _maxHealth, int _unlockCost, float _costMultiplier) {
        //generate locations of sprites
        string activeLocation = "partymembers/" + _MemberName.ToLower();
        string lockedLocation = activeLocation + "_blackout";

        MemberName = _MemberName;
        MaxHealth = _maxHealth;
        UnlockCost = _unlockCost;
        CurrentLevel = 0;
        LevelCost = _unlockCost;
        CurrentHealth = _maxHealth;
        costMultiplier = _costMultiplier;

        Active = Resources.Load<Sprite>(activeLocation);
        Locked = Resources.Load<Sprite>(lockedLocation);
	}

    /// <summary>
    /// Updates the relevant data when a party member is unlocked
    /// </summary>
    public void Unlock() {
        CurrentLevel++;
        LevelCost = UnlockCost * (int)Mathf.Pow(costMultiplier, CurrentLevel);
	}

    /// <summary>
    /// Updates the relevant data when a party member takes damage
    /// </summary>
    /// <param name="_amount">How much damage they have taken</param>
    /// <returns><c>INJURED_INDICATOR</c> if the party member is injured. <c>DEATH_INDICATOR</c> if
    /// their hp falls below 0. otherwise, returns 0</returns>
    public int TakeDamage(int _amount) {
        CurrentHealth -= _amount;
        if (CurrentHealth <= MaxHealth / 2 && CurrentHealth > 0) {
            return INJURED_INDICATOR;
		}
        else if (CurrentHealth <= 0) {
            return DEATH_INDICATOR;
		}
        else {
            return 0;
		}
	}
}
