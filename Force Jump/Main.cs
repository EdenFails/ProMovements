using System.Collections;
using MelonLoader;
using UnityEngine;
using VRC.SDKBase;

namespace Speed
{
    class Main : MelonMod
    {
        private bool speedup = false;
        public int speed = 3;

        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(waitforui());
        }
        
                
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
                Networking.LocalPlayer.SetJumpImpulse(jumps = jumps -1);
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


        }

        public override void OnUpdate()
        {
            if (true) return;

            {
//Not Needed Currently
            }

        }

    }
}
