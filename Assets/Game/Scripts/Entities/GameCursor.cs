using System;
using UnityEngine;

namespace Game.Scripts
{
    public class GameCursor : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(this);
        }
    }
}