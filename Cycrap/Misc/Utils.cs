﻿//using MelonLoader;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using UnityEngine.UI;
//using VRC;
//using VRC.SDK3.Components;
//using VRC.SDKBase;

//namespace Utils
//{
//    class esp
//    {
//        public static bool ESP = false;

//        public static void esprefresh(VRC.Player player)
//        {
//            if (ESP == true && player != null)
//            {
//                if (player.gameObject.transform.Find("SelectRegion"))
//                {
//                    {
//                        var Renderer = player.gameObject.transform.Find("SelectRegion").GetComponent<Renderer>();
//                        HighlightsFX.field_Private_Static_HighlightsFX_0.field_Protected_Material_0.color = Color.red;
//                        HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(Renderer, true);

//                    }
//                }
//            }
//        }


//        public static void espmethod()
//        {
//            foreach (VRC.Player gameObject in PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray())
//            {
//                if (gameObject.transform.Find("SelectRegion"))
//                {
//                    var Renderer = gameObject.transform.Find("SelectRegion").GetComponent<Renderer>();
//                    HighlightsFX.field_Private_Static_HighlightsFX_0.field_Protected_Material_0.color = Color.red;
//                    HighlightsFX.field_Private_Static_HighlightsFX_0.Method_Public_Void_Renderer_Boolean_0(Renderer, ESP);
//                }
//            }
//        }


//    }

   
//}

    

//// Ikari was here
