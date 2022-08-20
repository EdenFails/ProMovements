using System.Collections;
using MelonLoader;
using UnityEngine;
using VRC.SDKBase;
using System;
using System.Runtime.CompilerServices;
using MelonLoader;
using TMPro;
using UnityEngine.UI;
using VRC;
using VRC.Animation;

namespace Speed
{
    class Main : MelonMod
    {
        private bool flytoggle = false;
        private VRCMotionState _motionState;
        private Vector3 _originalGravity;
        private bool canset;
        private bool loaded;
        private bool speedup = false;
        public int speed = 3;

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Credits to: catnotadog, Cyril XD, xox-Toxic");
            MelonCoroutines.Start(waitforui());
        }
        private Transform camera()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private IEnumerator waitforui()
        {
            MelonLogger.Msg("Waiting For Ui");
            while (GameObject.Find("UserInterface") == null)
                yield return null;

            while (GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)") == null)
                yield return null;

            MelonLogger.Msg("Ui loaded");
            //----------------------------------------------------------------------------------------------------------------------------------------------------------Jump Button Section - AddPoints
            int jumps = 4;
            int Speeds = 4;
            var toinst = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
            var inst = GameObject.Instantiate(toinst, toinst.parent).gameObject;
            inst.name = "Button jump";
            var txt = inst.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
            txt.richText = true;
            txt.text = ($"<color=#000080ff>Jump++</color>");
            GameObject.DestroyImmediate(inst.transform.Find("Container/Icon").gameObject);
            var btn = inst.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(new System.Action(() =>
            {
                Networking.LocalPlayer.SetJumpImpulse(jumps = jumps + 1);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------Jump Button Section - Remove Points
            var toinsts = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
            var insts = GameObject.Instantiate(toinsts, toinsts.parent).gameObject;
            insts.name = "Button jump";
            var txts = insts.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
            txts.richText = true;
            txts.text = ($"<color=#000080ff>Jump--</color>");
            GameObject.DestroyImmediate(insts.transform.Find("Container/Icon").gameObject);
            var btns = insts.GetComponent<UnityEngine.UI.Button>();
            btns.onClick.RemoveAllListeners();
            btns.onClick.AddListener(new System.Action(() =>
            {
                Networking.LocalPlayer.SetJumpImpulse(jumps = jumps - 1);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------Speed Button Section - Add Speed
            var toinstss = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
            var instss = GameObject.Instantiate(toinstss, toinstss.parent).gameObject;
            instss.name = "Button jump";
            var txtss = instss.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
            txtss.richText = true;
            txtss.text = ($"<color=#000080ff>Speed++</color>");
            GameObject.DestroyImmediate(instss.transform.Find("Container/Icon").gameObject);
            var btnss = instss.GetComponent<UnityEngine.UI.Button>();
            btnss.onClick.RemoveAllListeners();
            btnss.onClick.AddListener(new System.Action(() =>
            {
                Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds + 1);
                Networking.LocalPlayer.SetWalkSpeed(Speeds);
                Networking.LocalPlayer.SetRunSpeed(Speeds);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------Speed Button Section - Remove Speed
            var toinstssd = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
            var instssd = GameObject.Instantiate(toinstssd, toinstssd.parent).gameObject;
            instssd.name = "Button jump";
            var txtssd = instssd.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
            txtssd.richText = true;
            txtssd.text = ($"<color=#000080ff>Speed--</color>");
            GameObject.DestroyImmediate(instssd.transform.Find("Container/Icon").gameObject);
            var btnssd = instssd.GetComponent<UnityEngine.UI.Button>();
            btnssd.onClick.RemoveAllListeners();
            btnssd.onClick.AddListener(new System.Action(() =>
            {
                Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds - 1);
                Networking.LocalPlayer.SetWalkSpeed(Speeds);
                Networking.LocalPlayer.SetRunSpeed(Speeds);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }));
            //---------------------------------------------------------------------------------------------------------------------------------------------------------Flight buutton - Toggle Flight -- Thanks to catnotdog for making this possible

            MelonLogger.Msg("Waiting For Ui");
            while (GameObject.Find("UserInterface") == null)
                yield return null;

            while (GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)") == null)
                yield return null;

            MelonLogger.Msg("Ui loaded");

            var toinstd = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
            var instd = GameObject.Instantiate(toinstd, toinstd.parent).gameObject;
            var txtd = instd.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
            txtd.richText = true;
            txtd.text = $"<color=#000080ff>Fly</color>";
            GameObject.DestroyImmediate(instd.transform.Find("Container/Icon").gameObject);
            var btnd = instd.GetComponent<UnityEngine.UI.Button>();
            btnd.onClick.RemoveAllListeners();
            btnd.onClick.AddListener(new System.Action(() => { flytoggle = !flytoggle; _ = flytoggle ? txtd.text = $"<color=#ff0000ff>Fly</color>" : txtd.text = $"<color=#000080ff>Fly</color>"; VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = !flytoggle; }));
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------







        }
        //The Random Shit of how fly works ------------------------------------------- Thanks to CatNotADog :)
        public override void OnUpdate()
        {
            if (!flytoggle)

            {
                if (this.loaded)
                {
                    Physics.gravity = this._originalGravity;
                }
            }
            else if (flytoggle && !(Physics.gravity == Vector3.zero))
            {
                this._originalGravity = Physics.gravity;
                Physics.gravity = Vector3.zero;
                return;
            }
            float num = Input.GetKey((KeyCode)304) ? (Time.deltaTime * 15f) : (Time.deltaTime * 10f);
            if (this.flytoggle && !(Player.prop_Player_0.gameObject == null))
            {
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < -0.5f)
                {
                    Player.prop_Player_0.gameObject.transform.position -= camera().up * num;
                }
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0.5f)
                {
                    Player.prop_Player_0.gameObject.transform.position += camera().up * num;
                }
                if (Input.GetAxis("Vertical") != 0f)
                {
                    Player.prop_Player_0.gameObject.transform.position += camera().forward * (num * Input.GetAxis("Vertical"));
                }
                if (Input.GetAxis("Horizontal") != 0f)
                {
                    Player.prop_Player_0.gameObject.transform.position += camera().transform.right * (num * Input.GetAxis("Horizontal"));
                    return;
                }
            } //UnityEngine.KeyCode

            // prop_Player_0.gameObject

        }
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (buildIndex == -1)
            {
                this._originalGravity = Physics.gravity;
                this.canset = false;
                this.loaded = true;
            }
        }
    }
}
