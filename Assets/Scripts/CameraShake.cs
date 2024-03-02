using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public ShakeData playerShakeData;
    public ShakeData enemyShakeData;

    public void PlayerHitShakeCam()
    {
        // Camera Shake
        CameraShakerHandler.Shake(playerShakeData);
    }

    public void EnemyHitShakeCam()
    {
        CameraShakerHandler.Shake(enemyShakeData);
    }

   
}
