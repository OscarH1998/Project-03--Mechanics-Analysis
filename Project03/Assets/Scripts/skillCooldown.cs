using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillCooldown : MonoBehaviour
{
    public Image imageCooldown;
    public float cooldown = 2;
    bool isCooldown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
                isCooldown = true;
        }

        if (isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            if (imageCooldown.fillAmount >= 1)
            {
                    imageCooldown.fillAmount = 0;
                    isCooldown = false;
            }
        }
    }
}
