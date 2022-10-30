using MelonLoader;
using RenamedButton69;
using RenamedButton69.QuickMenu;
using RenamedButton69.Wings;
using ReMod.Core.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib;
using UnhollowerRuntimeLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using UnityEngine;
using VRC;
using VRC.Animation;
using VRC.Core;
using VRC.DataModel;
using VRC.SDKBase;
using VRC.UI.Elements.Menus;
using QuestMod.TEST;

namespace RenamedButton69
{
    //This ButtonAPI isnt Standalone, its just for HowTo
    public class Main : MelonMod
    {

        // a bunch of this shit aint needed anymore but i cant be arsed figuring out what i stopped using so its here xD
        private bool flytoggle2 = false;
        private VRCMotionState _motionState;
        private Vector3 _originalGravity;
        private Vector3 origvelocity;
        private static readonly Dictionary<OVRInput.Button, float> lastTime = new Dictionary<OVRInput.Button, float>();
        private bool canset;
        private bool loaded;
        private static Func<float> originalwalkSpeed;
        private static Func<float> originalstrafespeed;
        private bool speedup = false;
        public float speed = 3;
        private static Func<float> originalrunspeed;
        private Func<float> originaljump;
        private int originaljump1;
        public int test = 0;
        // private bool flytoggle = false;
        static float jumps = 4;
        static float Speeds = 4;
        static float testwalk = 4;
        static float teststrafe = 4;
        static float testrun = 4;
        private static bool flytoggle;
        private static bool stopmoving;
        private static bool Flighttoggler = false;
        private static bool Immobtoggler = false;
        private static bool Jetpacktoggler = false;
        private static bool speedreset = false;
        private static bool raycaston = false;
        private static bool raydestroy = false;
        static float SavePresetspeeds1;
        static float SavePresetspeeds2;
        static float SavePresetspeeds3;
        static float SavePresetJump1;
        static float SavePresetJump2;
        static float SavePresetJump3;
        public static GameObject UserInterface; //New UI is "Obfuscate" thats why we need first to Grab the GameObject
        private static UiManager _uiManager;
        private static ReMenuButton _menuButton1;
        private static ReMenuPage _waypointsmenu;
        private static ReMenuButton _targetButton1;
        private static ReMenuButton _targetButton2;
        private static ReMenuButton _teleportTargetButton;
        public static ReMirroredWingMenu WingMenu;
        public static ReMirroredWingButton _WingButton;
        public static ReMirroredWingToggle _WingToggle;
        public static ReMirroredWingMenu _wingSubmenu;
        public static ReMirroredWingButton _testsub;
        public static ReMirroredWingMenu _wingSubmenuwaypoint;
        public static ReMirroredWingButton _testsubwaypoint;
        public static Vector3 Positionofplayer1;
        public static Quaternion Rotationofplayer1;
        public static Vector3 Positionofplayer2;
        public static Quaternion Rotationofplayer2;
        public static Vector3 Positionofplayer3;
        public static Quaternion Rotationofplayer3;
        public static Vector3 Positionofplayer4;
        public static Quaternion Rotationofplayer4;
        public static float currentcount = 0;
        private static ReMenuButton _creationtool;
        private static ReMenuButton _creationtool2;
        private static ReMenuButton _deletiontool;
        private static int xx = 0;
        private static GameObject Sphere1;


        private Transform camera()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
        }

        public static bool HasDoubleClicked(OVRInput.Button keyCode, float threshold)
        {
            bool flag = !OVRInput.GetDown(keyCode, OVRInput.Controller.Touch);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = !Main.lastTime.ContainsKey(keyCode);
                if (flag2)
                {
                    Main.lastTime.Add(keyCode, Time.time);
                }
                bool flag3 = Time.time - Main.lastTime[keyCode] <= threshold;
                if (flag3)
                {
                    Main.lastTime[keyCode] = threshold * 2f;
                    result = true;
                }
                else
                {
                    Main.lastTime[keyCode] = Time.time;
                    result = false;
                }
            }
            return result;

        }

        public override void OnApplicationStart()
        {
            MelonLogger.Msg("Credits to: catnotadog, Cyril XD, xox-Toxic for assistance and contributions to the Promovements mod");
            ClassInjector.RHelperRegisterTypeInIl2Cpp<EnableDisableListener>();
            MelonCoroutines.Start(WaitForUI());
            IEnumerator WaitForUI()
            {
                while (ReferenceEquals(VRCUiManager.field_Private_Static_VRCUiManager_0, null)) yield return null; // wait till VRCUIManger isnt null
                foreach (var GameObjects in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (GameObjects.name.Contains("UserInterface"))
                    {
                        UserInterface = GameObjects; 
                    }
                }

                while (ReferenceEquals(QuickMenuEx.Instance, null)) yield return null;
                MenuStart();
                yield break;
            }
        }

        public static void MenuStart()
        {
            MelonLogger.Msg("Initializing UI...");


            //This is how you create a tab and its base page
            _uiManager = new UiManager("Promovements", null);
            //A mirrored wing menu
            WingMenu = ReMirroredWingMenu.Create("ProMovements", "Opens ProMovements", null);
            


            //This is a demo Button within the root tab
            _uiManager.MainMenu.AddButton($"<color=#ffffffff>Speed ++</color>", "Makes you faster", new Action(() =>
            {
                Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds + 1);
                Networking.LocalPlayer.SetWalkSpeed(Speeds);
                Networking.LocalPlayer.SetRunSpeed(Speeds);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null );

            _uiManager.MainMenu.AddButton($"<color=#ffffffff>Speed --</color>", "Slows you down", new Action(() =>
            {
                Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds - 1);
                Networking.LocalPlayer.SetWalkSpeed(Speeds);
                Networking.LocalPlayer.SetRunSpeed(Speeds);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);

            _uiManager.MainMenu.AddButton($"<color=#ffffffff>Speed Reset</color>", "Resets your speed", new Action(() =>
            {

                speedreset = true; 
                Networking.LocalPlayer.SetRunSpeed(testrun);
                Networking.LocalPlayer.SetWalkSpeed(testwalk);
                Networking.LocalPlayer.SetStrafeSpeed(teststrafe);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);
            _uiManager.MainMenu.AddButton($"<color=#ffffffff>Jump ++</color>", "Jump higher", new Action(() =>
            {
                jumps = jumps + 1;
                Networking.LocalPlayer.SetJumpImpulse(jumps);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);

            _uiManager.MainMenu.AddButton($"<color=#ffffffff>Jump --</color>", "Jump lower", new Action(() =>
            {
                jumps = jumps - 1;
                Networking.LocalPlayer.SetJumpImpulse(jumps);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);

            _uiManager.MainMenu.AddButton($"<color=#ffffffff>Jump Reset</color>", "Jump reset", new Action(() =>
            {
                jumps = 3;
                Networking.LocalPlayer.SetJumpImpulse(jumps);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);

            //THis is how toggles work
            _uiManager.MainMenu.AddToggle("Flight", "Turns on Flight", Flighttoggle);
            _uiManager.MainMenu.AddToggle("Jetpack", "Turns on Jetpack", JetpackToggle);
            _uiManager.MainMenu.AddToggle("Immobilise", "Turns on Immobilise (stop moving)", Immobolise);

            //Promovements Wings

            _WingButton = WingMenu.AddButton($"<color=#ffffffff>Speed ++</color>", "Makes you faster", new Action(() =>
            {
                Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds + 1);
                Networking.LocalPlayer.SetWalkSpeed(Speeds);
                Networking.LocalPlayer.SetRunSpeed(Speeds);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);

            _WingButton = WingMenu.AddButton($"<color=#ffffffff>Speed --</color>", "Slows you down", new Action(() =>
            {
                Networking.LocalPlayer.SetStrafeSpeed(Speeds = Speeds - 1);
                Networking.LocalPlayer.SetWalkSpeed(Speeds);
                Networking.LocalPlayer.SetRunSpeed(Speeds);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);

            _WingButton = WingMenu.AddButton($"<color=#ffffffff>Speed Reset</color>", "Resets your speed", new Action(() =>
            {
                speedreset = true;
                Networking.LocalPlayer.SetRunSpeed(testrun);
                Networking.LocalPlayer.SetWalkSpeed(testwalk);
                Networking.LocalPlayer.SetStrafeSpeed(teststrafe);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);
            _WingButton = WingMenu.AddButton($"<color=#ffffffff>Jump ++</color>", "Jump higher", new Action(() =>
            {
                jumps = jumps + 1;
                Networking.LocalPlayer.SetJumpImpulse(jumps);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);

            _WingButton = WingMenu.AddButton($"<color=#ffffffff>Jump --</color>", "Jump lower", new Action(() =>
            {
                jumps = jumps - 1;
                Networking.LocalPlayer.SetJumpImpulse(jumps);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);

            _WingButton = WingMenu.AddButton($"<color=#ffffffff>Jump Reset</color>", "Resets Jump", new Action(() =>
            {
                jumps = 3;
                Networking.LocalPlayer.SetJumpImpulse(jumps);
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }), null);
            // Waypoints menu wings

            _wingSubmenuwaypoint = WingMenu.AddSubMenu("Waypoint", "Save and set waypoints", null);
            _testsubwaypoint = _wingSubmenuwaypoint.AddButton($"<color=#ffffffff>TP waypoint 1</color>", "Teleport to waypoint position 1", new Action(() =>
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(Positionofplayer1, Rotationofplayer1);
            }), null);
            _testsubwaypoint = _wingSubmenuwaypoint.AddButton($"<color=#ffffffff>TP waypoint 2</color>", "Teleport to waypoint position 2", new Action(() =>
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(Positionofplayer2, Rotationofplayer2);
            }), null);
            _testsubwaypoint = _wingSubmenuwaypoint.AddButton($"<color=#ffffffff>TP waypoint 3</color>", "Teleport to waypoint position 3", new Action(() =>
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(Positionofplayer3, Rotationofplayer3);
            }), null);
            _testsubwaypoint = _wingSubmenuwaypoint.AddButton($"<color=#ffffffff>TP waypoint 4</color>", "Teleport to waypoint position 4", new Action(() =>
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(Positionofplayer4, Rotationofplayer4);
            }), null);


            _testsubwaypoint = _wingSubmenuwaypoint.AddButton($"<color=#ffffffff>Save waypoint 1</color>", "Save waypoint position 1", new Action(() =>
            {
                Positionofplayer1 = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetPosition();
                Rotationofplayer1 = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetRotation();
            }), null);
            _testsubwaypoint = _wingSubmenuwaypoint.AddButton($"<color=#ffffffff>Save waypoint 2</color>", "Save waypoint position 2", new Action(() =>
            {
                Positionofplayer2 = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetPosition();
                Rotationofplayer2 = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetRotation();
            }), null);
            _testsubwaypoint = _wingSubmenuwaypoint.AddButton($"<color=#ffffffff>Save waypoint 3</color>", "Save waypoint position 3", new Action(() =>
            {
                Positionofplayer3 = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetPosition();
                Rotationofplayer3 = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetRotation();
            }), null);
            _testsubwaypoint = _wingSubmenuwaypoint.AddButton($"<color=#ffffffff>Save waypoint 4</color>", "Save waypoint position 4", new Action(() =>
            {
                Positionofplayer4 = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetPosition();
                Rotationofplayer4 = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetRotation();
            }), null);







            //And a wing toggle
            _WingToggle = WingMenu.AddToggle("Flight", "Toggles Flight", Flighttoggle, false);
            _WingToggle = WingMenu.AddToggle("Jetpack", "Toggles Jetpack", JetpackToggle, false);
            _WingToggle = WingMenu.AddToggle("Immobolize", "Toggles Immpbolize", Immobolise, false);



            // _-_-_-_-_-_-_-_-_-_-_-_-Cyril sent me this teleport-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-
            _uiManager.TargetMenu.AddMenuPage("Target Page", "Target page", null);
            //  var targetPageMenu = _uiManager.MainMenu.GetMenuPage("Target Page");
            var targetMenu = _uiManager.TargetMenu;
            //Example of how to use it
            _teleportTargetButton = targetMenu.AddButton("Teleport", "Teleports to target.", () =>
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(IUserExtension.GetVRCPlayer().transform.position, IUserExtension.GetVRCPlayer().transform.rotation);
            }, null);



            //Later when player wants to use waypoint



            //Save Preset menu ------------------------------------------------------------------------
            _wingSubmenu = WingMenu.AddSubMenu("Presets", null, null);
            _testsub = _wingSubmenu.AddButton($"<color=#ffffffff>Save 1</color>", "Save current jump and speed", new Action(() =>
            {
                SavePresetspeeds1 = Speeds; 
                SavePresetJump1 = jumps;
                
            }), null);
            _testsub = _wingSubmenu.AddButton($"<color=#ffffffff>Save 2</color>", "Save current jump and speed", new Action(() =>
            {
                SavePresetspeeds2 = Speeds;
                SavePresetJump2 = jumps;
            }), null);
            _testsub = _wingSubmenu.AddButton($"<color=#ffffffff>Save 3</color>", "Save current jump and speed", new Action(() =>
            {
                SavePresetspeeds3 = Speeds;
                SavePresetJump3 = jumps;
            }), null);
            _testsub = _wingSubmenu.AddButton($"<color=#ffffffff>Use 1</color>", "Uses preset 1 speed and jump", new Action(() =>
            {
                jumps = SavePresetJump1;
                Speeds = SavePresetspeeds1;
                Networking.LocalPlayer.SetRunSpeed(SavePresetspeeds1);
                Networking.LocalPlayer.SetWalkSpeed(SavePresetspeeds1);
                Networking.LocalPlayer.SetStrafeSpeed(SavePresetspeeds1);
                Networking.LocalPlayer.SetJumpImpulse(SavePresetJump1);
            }), null);
            _testsub = _wingSubmenu.AddButton($"<color=#ffffffff>Use 2</color>", "Uses preset 2 speed and jump", new Action(() =>
            {
                jumps = SavePresetJump2;
                Speeds = SavePresetspeeds2;
                Networking.LocalPlayer.SetRunSpeed(SavePresetspeeds2);
                Networking.LocalPlayer.SetWalkSpeed(SavePresetspeeds2);
                Networking.LocalPlayer.SetStrafeSpeed(SavePresetspeeds2);
                Networking.LocalPlayer.SetJumpImpulse(SavePresetJump2);
            }), null);
            _testsub = _wingSubmenu.AddButton($"<color=#ffffffff>Use 3</color>", "Uses preset 3 speed and jumpn", new Action(() =>
            {
                jumps = SavePresetJump3;
                Speeds = SavePresetspeeds3;
                Networking.LocalPlayer.SetRunSpeed(SavePresetspeeds3);
                Networking.LocalPlayer.SetWalkSpeed(SavePresetspeeds3);
                Networking.LocalPlayer.SetStrafeSpeed(SavePresetspeeds3);
                Networking.LocalPlayer.SetJumpImpulse(SavePresetJump3);
            }), null);
            _testsub = _wingSubmenu.AddButton($"<color=#ffffffff>WhAt ThE HeCk</color>", "4.2 Crazy SpEeD and crazy hEiGhT", new Action(() =>
            {
                jumps = 200;
                Speeds = 400;
                Networking.LocalPlayer.SetRunSpeed(400);
                Networking.LocalPlayer.SetWalkSpeed(400);
                Networking.LocalPlayer.SetStrafeSpeed(400);
                Networking.LocalPlayer.SetJumpImpulse(200);
            }), null);


            _uiManager.MainMenu.AddCategoryPage("Waypoints", null, null);
        var _waypointsmenu = _uiManager.MainMenu.GetCategoryPage("Waypoints");
        _waypointsmenu.AddCategory("Manage points");
            _waypointsmenu.AddCategory("Saves");
            var _resavemanager = _waypointsmenu.GetCategory("Saves");
            _waypointsmenu.AddCategory("Positions");
        var _pointsmanager = _waypointsmenu.GetCategory("Manage points");
        var _posmanager = _waypointsmenu.GetCategory("Positions");
            _pointsmanager.AddSpacer(null);
            _creationtool = _pointsmanager.AddButton("Create Saved Position", null, () =>
        {
            currentcount = currentcount + 1;
            Vector3 PositionofplayerGenerateButton;
            Quaternion RotationofplayergenerateButton;
            PositionofplayerGenerateButton = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetPosition();
            RotationofplayergenerateButton = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetRotation();
            _posmanager.AddButton($"Position" + currentcount, null, () =>
        {
            VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.TeleportTo(PositionofplayerGenerateButton, RotationofplayergenerateButton);
        });
            _resavemanager.AddButton($"ReSave Position" + currentcount, null, () =>
            {
                PositionofplayerGenerateButton = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetPosition();
                RotationofplayergenerateButton = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0.GetRotation();
            });

        });
            //Deletion thing gonna go here


        }
      
        public static void Flighttoggle(bool value)
        {
            Flighttoggler = !Flighttoggler;
        }

        public static void JetpackToggle(bool value)
        {
            Jetpacktoggler = !Jetpacktoggler;
        }


        public static void Immobolise(bool value)
        {
            Immobtoggler = Immobtoggler;
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


           

            
            if (speedreset = true) { speedreset = false; Speeds = testrun; }
            flytoggle2 = Flighttoggler;
            if (Jetpacktoggler)
            {
                if (Jumping() && !Networking.LocalPlayer.IsPlayerGrounded())
                {

                    var GetJump = Networking.LocalPlayer.GetVelocity();
                    GetJump.y = Networking.LocalPlayer.GetJumpImpulse();
                    Networking.LocalPlayer.SetVelocity(GetJump);
                }
            }
            if (!this.flytoggle2)
            {
                if (this.loaded)
                {
                    Physics.gravity = this._originalGravity;
                }
            }
            else if (this.flytoggle2 && !(Physics.gravity == Vector3.zero))
            {
                this._originalGravity = Physics.gravity;
                Physics.gravity = Vector3.zero;
                return;
            }
            if (this.flytoggle2 && !(Player.prop_Player_0 == null))
            {
                float num = Input.GetKey(KeyCode.LeftShift) ? (Time.deltaTime * 15f) : (Time.deltaTime * 10f);
                if (Player.prop_Player_0.field_Private_VRCPlayerApi_0.IsUserInVR())
                {
                    if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < -0.5f)
                    {
                        Player.prop_Player_0.transform.position -= this.camera().up * num;
                    }
                    if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0.5f)
                    {
                        Player.prop_Player_0.transform.position += this.camera().up * num;
                    }
                    if (Input.GetAxis("Vertical") != 0f)
                    {
                        Player.prop_Player_0.transform.position += this.camera().forward * (num * Input.GetAxis("Vertical"));

                    }
                    if (Input.GetAxis("Vertical") == 0f)
                    {
                        Networking.LocalPlayer.SetVelocity(origvelocity);

                    }
                    if (Input.GetAxis("Horizontal") != 0f)
                    {
                        Player.prop_Player_0.transform.position += this.camera().transform.right * (num * Input.GetAxis("Horizontal"));
                        return;
                    }
                }
            } //UnityEngi


        }
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {

            if (buildIndex == -1)
            {
                this._originalGravity = Physics.gravity;
                
                canset = false;
                this.loaded = true;
                origvelocity = Networking.LocalPlayer.GetVelocity();
                originaljump = Networking.LocalPlayer.GetJumpImpulse;
                testwalk = Networking.LocalPlayer.GetWalkSpeed();
                teststrafe = Networking.LocalPlayer.GetStrafeSpeed();
                testrun = Networking.LocalPlayer.GetRunSpeed();
            }
        }

    }
}
