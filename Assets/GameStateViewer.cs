using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateViewer : MonoBehaviour
{

    public enum GameState { idle,jump,teleport,invincible,super,die,normal}

    public GameState gameState;
}
