﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    GameObject enemySprite;
    
    // Start is called before the first frame update
    void Start()
    {
        enemySprite = GameObject.Find("enemy");
        Debug.Log("Found enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
