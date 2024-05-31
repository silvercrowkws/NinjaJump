using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_04_Stone : TestBase
{
    public StoneSpawner spawner;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        spawner. StoneSpawn();
    }
}
