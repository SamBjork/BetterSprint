using GameNetcodeStuff;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BetterSprint.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void ModifiedSprintPatch(ref float ___sprintMeter, ref float ___sprintTime, ref bool ___isSprinting)
        {
            float maxSprintMeter = 1f; // Assuming 1 is the max value of sprint meter
            float sprintRechargeRate = 0.07f; // Increase this value to recharge faster
            ___sprintTime = 31f; // Increase for longer sprint duration

            if (___isSprinting)
            {
                // Sprint meter decreases over time while sprinting
                ___sprintMeter = Mathf.Clamp(___sprintMeter - Time.deltaTime / ___sprintTime, 0f, maxSprintMeter);
            }
            else
            {
                // Sprint meter recharges faster when not sprinting
                ___sprintMeter = Mathf.Clamp(___sprintMeter + Time.deltaTime * sprintRechargeRate, 0f, maxSprintMeter);
            }
        }
    }
}
