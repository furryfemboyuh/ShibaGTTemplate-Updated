using ShibaGTTemplate.Utilities;
using Photon.Pun;   
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;
using ExitGames.Client.Photon;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using ShibaGTTemplate.UI;
using GorillaNetworking;
using dark.efijiPOIWikjek;
using GorillaLocomotion.Gameplay;
using GorillaExtensions;
using HarmonyLib;
using System.Reflection;
using GorillaTag;
using static UnityEngine.UI.GridLayoutGroup;
using Photon.Pun.UtilityScripts;
using System.IO;
using static MonoMod.Cil.RuntimeILReferenceBag.FastDelegateInvokers;
using Oculus.Interaction.HandGrab;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using GorillaTagScripts;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Voice.Unity;
using System.ComponentModel;
using UnityEngine.UIElements;
using OVRSimpleJSON;
using GTAG_NotificationLib;
using System.Threading;
using UnityEngine.XR;
using UnityEngine.InputSystem.HID;
using System.Runtime.InteropServices;
using TMPro;
using Random = UnityEngine.Random;

namespace ShibaGTTemplate.Backend
{
    internal class Mods : MonoBehaviour
    {
        public static bool oiwefkwenfjk;

        public static void HeadSpin()
        {
            RigShit.GetOwnVRRig().head.trackingRotationOffset.y += 15f;
        }

        public static void nuhuhheadspin()
        {
            RigShit.GetOwnVRRig().head.trackingRotationOffset.y = 0.0f;

        }

        public static bool inSettings = false;

        public static void Settings()
        {
            WristMenu.settingsbuttons[0].enabled = false;
            WristMenu.buttons[0].enabled = false;
            inSettings = !inSettings;
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
        }

        public static bool invisplat = false;
        public static bool stickyplatforms = false;
        public static GameObject funn;
        public static bool fpcc;

        public static void Platforms()
        {
            PlatformsThing(invisplat, stickyplatforms);
        }

        public static GameObject pointer;
        public static void fpc()
        {
            fpcc = true;
            if (GameObject.Find("Third Person Camera") != null)
            {
                funn = GameObject.Find("Third Person Camera");
                funn.SetActive(false);
            }
            if (GameObject.Find("CameraTablet(Clone)") != null)
            {
                funn = GameObject.Find("CameraTablet(Clone)");
                funn.SetActive(false);
            }
        }

        public static void fpcoff()
        {
            fpcc = false;
            if (funn != null)
            {
                funn.SetActive(true);
                funn = null;
            }
        }
        
        public static void Save()
        {
            WristMenu.settingsbuttons[1].enabled = false;
            WristMenu.DestroyMenu();
            WristMenu.instance.Draw();
            List<String> list = new List<String>();
            foreach (ButtonInfo info in WristMenu.settingsbuttons)
            {
                if (info.enabled == true)
                {
                    list.Add(info.buttonText);
                }
            }
            System.IO.Directory.CreateDirectory("TemplatePrefs");
            System.IO.File.WriteAllLines("TemplatePrefs\\templateSavedPrefs.txt", list);
        }

        public static void Load()
        {
            String[] thing = System.IO.File.ReadAllLines("TemplatePrefs\\templateSavedPrefs.txt");
            foreach (String thing2 in thing)
            {
                foreach (ButtonInfo b in WristMenu.settingsbuttons)
                {
                    if (b.buttonText == thing2)
                    {
                        b.enabled = true;
                    }
                }
            }
        }

        private static void PlatformsThing(bool invis, bool sticky)
        {
            colorKeysPlatformMonke[0].color = Color.red;
            colorKeysPlatformMonke[0].time = 0f;
            colorKeysPlatformMonke[1].color = Color.green;
            colorKeysPlatformMonke[1].time = 0.3f;
            colorKeysPlatformMonke[2].color = Color.blue;
            colorKeysPlatformMonke[2].time = 0.6f;
            colorKeysPlatformMonke[3].color = Color.red;
            colorKeysPlatformMonke[3].time = 1f;
            bool inputr;
            bool inputl;
                inputr = WristMenu.gripDownR;
                inputl = WristMenu.gripDownL;
            if (inputr)
            {
                if (!once_right && jump_right_local == null)
                {
                    if (sticky)
                    {
                        jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    }
                    else
                    {
                        jump_right_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    }
                    jump_right_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    if (invis)
                    {
                        UnityEngine.Object.Destroy(jump_right_local.GetComponent<Renderer>());
                    }
                    jump_right_local.transform.localScale = scale;
                    jump_right_local.transform.position = new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.position;
                    jump_right_local.transform.rotation = GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.rotation;
                    object[] eventContent = new object[2]
                    {
                    new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.position,
                    GorillaLocomotion.GTPlayer.Instance.RightHand.controllerTransform.rotation
                    };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others
                    };
                    PhotonNetwork.RaiseEvent(70, eventContent, raiseEventOptions, SendOptions.SendReliable);
                    once_right = true;
                    once_right_false = false;
                    ColorChanger colorChanger = jump_right_local.AddComponent<ColorChanger>();
                    Gradient gradient = new Gradient();
                    gradient.colorKeys = colorKeysPlatformMonke;
                    colorChanger.colors = gradient;
                    colorChanger.Start();
                }
            }
            else if (!once_right_false && jump_right_local != null)
            {
                UnityEngine.Object.Destroy(jump_right_local);
                jump_right_local = null;
                once_right = false;
                once_right_false = true;
                RaiseEventOptions raiseEventOptions2 = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(72, null, raiseEventOptions2, SendOptions.SendReliable);
            }
            if (inputl)
            {
                if (!once_left && jump_left_local == null)
                {
                    if (sticky)
                    {
                        jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    }
                    else
                    {
                        jump_left_local = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    }
                    jump_left_local.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                    if (invis)
                    {
                        UnityEngine.Object.Destroy(jump_left_local.GetComponent<Renderer>());
                    }
                    jump_left_local.transform.localScale = scale;
                    jump_left_local.transform.position = new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.position;
                    jump_left_local.transform.rotation = GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.rotation;
                    object[] eventContent2 = new object[2]
                    {
                    new Vector3(0f, -0.0100f, 0f) + GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.position,
                    GorillaLocomotion.GTPlayer.Instance.LeftHand.controllerTransform.rotation
                    };
                    RaiseEventOptions raiseEventOptions3 = new RaiseEventOptions
                    {
                        Receivers = ReceiverGroup.Others
                    };
                    PhotonNetwork.RaiseEvent(69, eventContent2, raiseEventOptions3, SendOptions.SendReliable);
                    once_left = true;
                    once_left_false = false;
                    ColorChanger colorChanger2 = jump_left_local.AddComponent<ColorChanger>();
                    Gradient gradient2 = new Gradient();
                    gradient2.colorKeys = colorKeysPlatformMonke;
                    colorChanger2.colors = gradient2;
                    colorChanger2.Start();
                }
            }
            else if (!once_left_false && jump_left_local != null)
            {
                UnityEngine.Object.Destroy(jump_left_local);
                jump_left_local = null;
                once_left = false;
                once_left_false = true;
                RaiseEventOptions raiseEventOptions4 = new RaiseEventOptions
                {
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(71, null, raiseEventOptions4, SendOptions.SendReliable);
            }
            if (!PhotonNetwork.InRoom)
            {
                for (int i = 0; i < jump_right_network.Length; i++)
                {
                    UnityEngine.Object.Destroy(jump_right_network[i]);
                }
                for (int j = 0; j < jump_left_network.Length; j++)
                {
                    UnityEngine.Object.Destroy(jump_left_network[j]);
                }
            }
        }

        private static Vector3 scale = new Vector3(0.0125f, 0.28f, 0.3825f);

        private static bool once_left;

        private static bool once_right;

        private static bool once_left_false;

        private static bool once_right_false;

        private static bool once_networking;

        private static GameObject[] jump_left_network = new GameObject[9999];

        private static GameObject[] jump_right_network = new GameObject[9999];

        private static GameObject jump_left_local = null;

        private static GameObject jump_right_local = null;

        private static GradientColorKey[] colorKeysPlatformMonke = new GradientColorKey[4];

        private static Vector3? checkpointPos;

    }
}
