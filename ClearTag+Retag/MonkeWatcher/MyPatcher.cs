using BepInEx;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
using HarmonyLib;
using System.Reflection;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using System.Collections;

namespace ClearTag
{
    [BepInPlugin("org.bepinex.plugins.ClearTag", "ClearTag", "1.0.0.0")]




    public class MyPatcher : BaseUnityPlugin
    {


        public void Awake()
        {
            var harmony = new Harmony("org.thunder.monkeytag.cleartag");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        [HarmonyPatch(typeof(GorillaTagManager))]
        [HarmonyPatch("Update", 0)]
        class ClearTag : MonoBehaviour
        {
            static void Prefix(GorillaTagManager __instance)
            {
                bool secondaryDown = false;
                bool primaryDown = false;
                if (!PhotonNetwork.CurrentRoom.IsVisible || !PhotonNetwork.InRoom)
                {
                    List<InputDevice> list = new List<InputDevice>();
                    InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
                    list[0].TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryDown);
                    list[0].TryGetFeatureValue(CommonUsages.primaryButton, out primaryDown);

                    if (secondaryDown)
                    {
                        if (!__instance.isCurrentlyTag || __instance.currentIt != null);
                        {
                            int num = PhotonNetwork.PlayerList.Length;
                            if (num < 4)
                            {
                                ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                                hashtable2.Add("matIndex", 0);
                                if (__instance.currentIt != null)
                                {
                                    __instance.currentIt.SetCustomProperties(hashtable2, null, null);
                                }
                                ExitGames.Client.Photon.Hashtable hashtable3 = new ExitGames.Client.Photon.Hashtable();
                                hashtable3.Add("currentIt", null);
                                hashtable3.Add("lastTag", PhotonNetwork.Time);
                                __instance.currentRoom.SetCustomProperties(hashtable3, null, null);
                                __instance.lastTag = PhotonNetwork.Time;
                                __instance.currentIt = null;
                                __instance.UpdateTagState();
                            }
                            if (num >= 4)
                            {
                                __instance.ClearInfectionState();
                                __instance.lastInfectedPlayer = null;
                                __instance.SetisCurrentlyTag(true);
                                ExitGames.Client.Photon.Hashtable hashtable2 = new ExitGames.Client.Photon.Hashtable();
                                hashtable2.Add("matIndex", 0);
                                if (__instance.currentIt != null)
                                {
                                    __instance.currentIt.SetCustomProperties(hashtable2, null, null);
                                }
                                ExitGames.Client.Photon.Hashtable hashtable3 = new ExitGames.Client.Photon.Hashtable();
                                hashtable3.Add("currentIt", null);
                                hashtable3.Add("lastTag", PhotonNetwork.Time);
                                __instance.currentRoom.SetCustomProperties(hashtable3, null, null);
                                __instance.lastTag = PhotonNetwork.Time;
                                __instance.currentIt = null;
                                __instance.UpdateTagState();
                            }
                        }
                    }
                    if ((primaryDown && __instance.isCurrentlyTag) || (primaryDown && __instance.currentIt == null))
                    {
                        int num = PhotonNetwork.PlayerList.Length;

                        if (num < 4)
                        {
                            __instance.UpdateState();
                        }
                        else if (num >= 4)
                        {
                            __instance.UpdateState();
                        }
                    }
                }
            }
        }
    }
}
