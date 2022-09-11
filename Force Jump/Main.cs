using MelonLoader;
using MelonLoader;
using System;
using HarmonyLib;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Animation;
using VRC.SDKBase;
using System.Reflection;
using VRC.Udon.Wrapper.Modules;

namespace Speed
{
    class Main : MelonMod
    {

        private bool flytoggle = false;
        private bool flytoggle2 = false;
        private VRCMotionState _motionState;
        private Vector3 _originalGravity;

        private bool canset;
        private bool loaded;
        private Func<float> originalwalkSpeed;
        private Func<float> originalstrafespeed;
        private bool speedup = false;
        public int speed = 3;
        private Func<float> originalrunspeed;
        private Func<float> originaljump;
        private int originaljump1;
        public int test = 0;
        public bool jumptoggle = false;
        public int jumps = 4;
        public float Speeds = 4;

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

            var toinstresf0 = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
            var instsresf0 = GameObject.Instantiate(toinstresf0, toinstresf0.parent).gameObject;
            instsresf0.name = "Button Speed Reset";
            var txtsresf0 = instsresf0.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
            txtsresf0.richText = true;
            txtsresf0.text = ($"<color=#0296CD>Open ProMovements</color>");
            GameObject.DestroyImmediate(instsresf0.transform.Find("Container/Icon").gameObject);
            var btnsresf0 = instsresf0.GetComponent<UnityEngine.UI.Button>();
            btnsresf0.onClick.RemoveAllListeners();
            btnsresf0.onClick.AddListener(new System.Action(() =>
            {


                var toinst = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var inst = GameObject.Instantiate(toinst, toinst.parent).gameObject;
                var txt = inst.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txt.richText = true;
                txt.text = ($"<color=#4CB750>Jump++</color>") ;
                GameObject.DestroyImmediate(inst.transform.Find("Container/Icon").gameObject);
                var btn = inst.GetComponent<UnityEngine.UI.Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(new System.Action(() =>
                {
                    MelonCoroutines.Start(unloadui());
                    
                }));
                //------------------------------------------------------------------------------------------------------------------------------------------------------------Jump Button Section - Remove Points
                var toinsts = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var insts = GameObject.Instantiate(toinsts, toinsts.parent).gameObject;
                var txts = insts.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txts.richText = true;
                txts.text = ($"<color=#ED6942>Jump--</color>");
                GameObject.DestroyImmediate(insts.transform.Find("Container/Icon").gameObject);
                var btns = insts.GetComponent<UnityEngine.UI.Button>();
                btns.onClick.RemoveAllListeners();
                btns.onClick.AddListener(new System.Action(() =>
                {
                    MelonCoroutines.Start(unloadui1());

                    

                }));


                //----------------------------------------------------------------------------------------------------------------------------------------------------------------Reset button Speed

                var toinstre = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var instsre = GameObject.Instantiate(toinstre, toinstre.parent).gameObject;
                var txtsre = instsre.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txtsre.richText = true;
                txtsre.text = ($"<color=#ffffffff>Jump Reset</color>");
                GameObject.DestroyImmediate(instsre.transform.Find("Container/Icon").gameObject);
                var btnsre = instsre.GetComponent<UnityEngine.UI.Button>();
                btnsre.onClick.RemoveAllListeners();
                btnsre.onClick.AddListener(new System.Action(() =>
                {

                    MelonCoroutines.Start(unloadui4());
                    //originaljump1 = Convert.ToInt32(originaljump);
                    // jumps = (originaljump1);



                }));
                //------------------------------------------------------------------------------------------------------------------------------------------------------------Speed Button Section - Add Speed
                var toinstss = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var instss = GameObject.Instantiate(toinstss, toinstss.parent).gameObject;
                var txtss = instss.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txtss.richText = true;
                txtss.text = ($"<color=#4CB750>Speed++</color>");
                GameObject.DestroyImmediate(instss.transform.Find("Container/Icon").gameObject);
                var btnss = instss.GetComponent<UnityEngine.UI.Button>();
                btnss.onClick.RemoveAllListeners();
                btnss.onClick.AddListener(new System.Action(() =>
                {
                    MelonCoroutines.Start(unloadui2());

                    

                }));
                //------------------------------------------------------------------------------------------------------------------------------------------------------------Speed Button Section - Remove Speed
                var toinstssd = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var instssd = GameObject.Instantiate(toinstssd, toinstssd.parent).gameObject;
                var txtssd = instssd.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txtssd.richText = true;
                txtssd.text = ($"<color=#ED6942>Speed--</color>");
                GameObject.DestroyImmediate(instssd.transform.Find("Container/Icon").gameObject);
                var btnssd = instssd.GetComponent<UnityEngine.UI.Button>();
                btnssd.onClick.RemoveAllListeners();
                btnssd.onClick.AddListener(new System.Action(() =>
                {
                    //if (test == 0)
                    // {
                    //originalrunspeed = Networking.LocalPlayer.GetRunSpeed;
                    //  Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds - 1);
                    // Networking.LocalPlayer.SetWalkSpeed(Speeds);
                    //    VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
                    //    test = test + 1;
                    // }
                    // else if (test == 1)
                    // {
                    MelonCoroutines.Start(unloadui3());
                    


                }));





                //---------------------------------------------------------------------------------------------------------------------------------------------------------Flight buutton - Toggle Flight -- Thanks to catnotdog for making this possible

                MelonLogger.Msg("Waiting For Ui");
                while (GameObject.Find("UserInterface") == null)


                    while (GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)") == null)


                        MelonLogger.Msg("Ui loaded");

                var toinstd = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var instd = GameObject.Instantiate(toinstd, toinstd.parent).gameObject;
                var txtd = instd.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txtd.richText = true;
                txtd.text = $"<color=#ffffffff>Fly</color>";
                GameObject.DestroyImmediate(instd.transform.Find("Container/Icon").gameObject);
                var btnd = instd.GetComponent<UnityEngine.UI.Button>();
                btnd.onClick.RemoveAllListeners();
                btnd.onClick.AddListener(new System.Action(() => { flytoggle = !flytoggle; _ = flytoggle ? txtd.text = $"<color=#ff0000ff>Fly</color>" : txtd.text = $"<color=#ffffffff>Fly</color>"; VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = !flytoggle; }));


                //-------------------------------------------------------------------------------------------------------------------------------------

                var toinstres = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var instsres = GameObject.Instantiate(toinstres, toinstres.parent).gameObject;
                var txtsres = instsres.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txtsres.richText = true;
                txtsres.text = ($"<color=#ffffffff>Speed Reset</color>");
                GameObject.DestroyImmediate(instsres.transform.Find("Container/Icon").gameObject);
                var btnsres = instsres.GetComponent<UnityEngine.UI.Button>();
                btnsres.onClick.RemoveAllListeners();
                btnsres.onClick.AddListener(new System.Action(() =>
                {
                    MelonCoroutines.Start(unloadui5());

                    //   int originalrunspeed1 = Convert.ToInt16(originalrunspeed);
                    //    Speeds = (originalrunspeed1);

                    

                }));



                ////////////////////////
                ///


                MelonLogger.Msg("Waiting For Ui");
                while (GameObject.Find("UserInterface") == null)


                    while (GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)") == null)


                        MelonLogger.Msg("Ui loaded");

                var toinstda = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var instda = GameObject.Instantiate(toinstda, toinstda.parent).gameObject;
                var txtda = instda.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txtda.richText = true;
                txtda.text = $"<color=#ffffffff>JetPack</color>";
                GameObject.DestroyImmediate(instda.transform.Find("Container/Icon").gameObject);
                var btnda = instda.GetComponent<UnityEngine.UI.Button>();
                btnda.onClick.RemoveAllListeners();
                btnda.onClick.AddListener(new System.Action(() => { jumptoggle = !jumptoggle; _ = jumptoggle ? txtda.text = $"<color=#ff0000ff>JetPack</color>" : txtda.text = $"<color=#ffffffff>JetPack</color>"; }));



                //////////////////////////


                                  
                var toinstresf1 = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
                var instsresf1 = GameObject.Instantiate(toinstresf1, toinstresf1.parent).gameObject;
                var txtsresf1 = instsresf1.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
                txtsresf1.richText = true;
                txtsresf1.text = ($"<color=#0296CD>Close ProMovements</color>");
                GameObject.DestroyImmediate(instsresf1.transform.Find("Container/Icon").gameObject);
                var btnsresf1 = instsresf1.GetComponent<UnityEngine.UI.Button>();
                btnsresf1.onClick.RemoveAllListeners();
                btnsresf1.onClick.AddListener(new System.Action(() =>
                {
                GameObject.DestroyObject(toinst);
                GameObject.DestroyObject(inst);
                GameObject.DestroyObject(txt);
                GameObject.DestroyObject(btn);//

                GameObject.DestroyObject(toinsts);
                GameObject.DestroyObject(insts);
                GameObject.DestroyObject(txts);
                GameObject.DestroyObject(btns);//

                GameObject.DestroyObject(btns);
                GameObject.DestroyObject(instss);
                GameObject.DestroyObject(txtss);
                GameObject.DestroyObject(btnss);//

                GameObject.DestroyObject(toinstssd);
                GameObject.DestroyObject(instssd);
                GameObject.DestroyObject(txtssd);
                GameObject.DestroyObject(btnssd);//

                GameObject.DestroyObject(toinstre);
                GameObject.DestroyObject(instsre);
                GameObject.DestroyObject(txtsre);
                GameObject.DestroyObject(btnsre);//

                GameObject.DestroyObject(toinstd);
                GameObject.DestroyObject(instd);
                GameObject.DestroyObject(txtd);
                GameObject.DestroyObject(btnd);

                GameObject.DestroyObject(toinstres);
                GameObject.DestroyObject(instsres);
                GameObject.DestroyObject(txtsres);
                GameObject.DestroyObject(btnsres);

                GameObject.DestroyObject(toinstda);
                GameObject.DestroyObject(instda);
                GameObject.DestroyObject(txtda);
                GameObject.DestroyObject(btnda);

                GameObject.DestroyObject(toinstresf1);
                GameObject.DestroyObject(instsresf1);
                GameObject.DestroyObject(txtsresf1);
                GameObject.DestroyObject(btnsresf1);




            }));
            }));





        }
        //The Random Shit of how fly works ------------------------------------------- Thanks to CatNotADog :)

        private IEnumerator unloadui()
        {
            Networking.LocalPlayer.SetJumpImpulse(jumps = jumps + 1);
            
            VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            throw new NotImplementedException();
        }
        private IEnumerator unloadui1()
        {
            Networking.LocalPlayer.SetJumpImpulse(jumps = jumps - 1);

            VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            throw new NotImplementedException();
        }
        private IEnumerator unloadui2()
        {
            Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds + 1);
            Networking.LocalPlayer.SetWalkSpeed(Speeds);
            Networking.LocalPlayer.SetRunSpeed(Speeds);
            VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            throw new NotImplementedException();
        }
        private IEnumerator unloadui3()
        {
            Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds - 1);
            Networking.LocalPlayer.SetWalkSpeed(Speeds);
            Networking.LocalPlayer.SetRunSpeed(Speeds);
            
            VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            throw new NotImplementedException();
        }
        private IEnumerator unloadui4()
        {
            jumps = 4;
            
            Networking.LocalPlayer.SetJumpImpulse(jumps);
            VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            throw new NotImplementedException();
        }
        private IEnumerator unloadui5()
        {
            Speeds = 3.25f;
            
            Networking.LocalPlayer.SetRunSpeed(Speeds);
            Networking.LocalPlayer.SetWalkSpeed(Speeds);
            Networking.LocalPlayer.SetStrafeSpeed(Speeds);
            VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            throw new NotImplementedException();
        }






        public static bool Jumping()
        {
            return GetJumpValue() != 0f;

        }

        public static float GetJumpValue()
        {
            return VRCInputManager.Method_Public_Static_ObjectPublicStSiBoSiObBoSiObStSiUnique_String_0("Jump").field_Private_Single_0;
        }





        public override void OnUpdate()
        {
            if (jumptoggle)
            {
                if (Jumping() && !Networking.LocalPlayer.IsPlayerGrounded())
                {

                    var GetJump = Networking.LocalPlayer.GetVelocity();
                    GetJump.y = Networking.LocalPlayer.GetJumpImpulse();
                    Networking.LocalPlayer.SetVelocity(GetJump);
                }
            }

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
                _originalGravity = Physics.gravity;
                canset = false;
                loaded = true;
                originalwalkSpeed = Networking.LocalPlayer.GetWalkSpeed;
                originalstrafespeed = Networking.LocalPlayer.GetStrafeSpeed;
                originalrunspeed = Networking.LocalPlayer.GetRunSpeed;
                originaljump = Networking.LocalPlayer.GetJumpImpulse;
            }
        }
    }
}
