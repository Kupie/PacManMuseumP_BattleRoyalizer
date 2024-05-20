using HarmonyLib;
using UnityEngine;
using System;

namespace BattleRoyalizer

{
    [HarmonyPatch(typeof(BattleroyaleMain), nameof(BattleroyaleMain.CheckCoin))]
    internal class BattleroyaleMain_CheckCoin_Patch
    {
        static GameObject buttonGuideSet;
        static GameObject creditText;
        static bool Prefix()
        {
            //Plugin.D.L("Set GoldPass to true!", "info");
            //GameStateManager.m_bGoldPass = true;
            return true;
        }
        static void Postfix(BattleroyaleMain __instance)
        {
            //Plugin.D.L("CheckCoin ran!", "info");
            if (Plugin.needsFindingButtonGuides)
            {
                Plugin.D.L("Finding ButtonGuide_set...", "info");
                buttonGuideSet = GameObject.Find("ButtonGuide_set");
                if (buttonGuideSet != null)
                {
                    Plugin.D.L("Found Button Guides!", "info");
                    Plugin.needsFindingButtonGuides = false;
                }
            }
            if (!Plugin.needsFindingButtonGuides && buttonGuideSet.activeSelf)
            {
                Plugin.D.L("Hiding ButtonGuide_set!", "info");
                buttonGuideSet.SetActive(false);
            }

            // Hiding Credits
            if (Plugin.needsFindingCreditText)
            {
                Plugin.D.L("Finding credits text...", "info");
                creditText = GameObject.Find("Battleroyale_Credit");
                if (creditText != null)
                {
                    Plugin.D.L("Found credits text!", "info");
                    Plugin.needsFindingCreditText = false;
                }
            }
                if (!Plugin.needsFindingCreditText && creditText.activeSelf)
                {

                    Plugin.D.L("Hiding Credits Text", "info");
                    creditText.SetActive(false);


            }


            if ((int)global::battleRoyale.Constants.tm.inMoney() < 99)
                {
                __instance.ExecInsertCoin();
                }

            
            if (__instance.m_coinUI.gameObject.activeSelf)
            {
                __instance.m_coinUI.gameObject.SetActive(false);
            }
            //GameObject insertCoinsUIObject = BattleroyaleUIManager.Get<PushStartUI>(0);

        }
    }

    [HarmonyPatch(typeof(LogoMain), nameof(LogoMain.Update))]
    internal class LogoMain_Update_Skip_Patch
    {
        private static int numUpdateRan = 0;
        static void Prefix()
        {
            //Plugin.D.L("Set GoldPass to true!", "info");
            //GameStateManager.m_bGoldPass = true;
        //    return false;
        }
        static void Postfix(LogoMain __instance)
        {
            if (numUpdateRan > 30)
            {
                Plugin.D.L("Update Called: " + numUpdateRan, "info");
                SceneLoginXboxMain.SetAddUserOption(isSilent: true);
                SceneChanger.Request(EScene.LOGIN_XBOX, dispLoadingScene: false, fadeIn: false);
                __instance.m_bEnd = true;
            }
            numUpdateRan ++;
        }
    }
    [HarmonyPatch(typeof(GameCenterMain), nameof(GameCenterMain.DispGameTitle))]
    internal class GameCenterMain_DispGametitle_Patch
    {
        private static int numLogoMainRan = 0;
        static void Prefix()
        {
            //Plugin.D.L("Set GoldPass to true!", "info");
            //GameStateManager.m_bGoldPass = true;
            //    return false;
        }
        static void Postfix(GameCenterMain __instance)
        {

            Plugin.D.L("GameCenterMain DispGametitle Ran: " + numLogoMainRan, "info");
            __instance.OnCloseTitle();
            numLogoMainRan++;
        }

        //       [HarmonyPatch(typeof(UIGameInfo), nameof(UIGameInfo.SwitchGameScene))]
        //       internal class SwitchGameScene_Patch
        //       {
        //           private static int numSwitchGameSceneRan = 0;
        //           static void Prefix()
        //           {
        //               //Plugin.D.L("Set GoldPass to true!", "info");
        //               //GameStateManager.m_bGoldPass = true;
        //               //    return false;
        //           }
        //           static void Postfix(UIGameInfo __instance)
        //           {
        //
        //               Plugin.D.L("SwitchGameScene Ran: " + numSwitchGameSceneRan, "info");
        //               numSwitchGameSceneRan++;
        //           }
        //       }
        //
        //   }
        [HarmonyPatch(typeof(UIGameInfo), nameof(UIGameInfo.Update))]
        internal class UIGameInfo_Update_Patch
        {
            private static int numUIGameInfoUpdateRan = 0;
            private static bool needSwitchGameSceneRan = true;
            static void Prefix()
            {
                //Plugin.D.L("Set GoldPass to true!", "info");
                //GameStateManager.m_bGoldPass = true;
                //    return false;
            }
            static void Postfix(UIGameInfo __instance)
            {
                if (needSwitchGameSceneRan)
                {
                    numUIGameInfoUpdateRan++;
                    Plugin.D.L("UIGameInfo.Update: " + numUIGameInfoUpdateRan, "info");
                    if (numUIGameInfoUpdateRan == 2)
                    {
                        __instance.m_gameTitle = GameDefine.EGameTitle.eBATTLEROYEL;
                        __instance.Pressed();
                        numUIGameInfoUpdateRan = 0;
                        Plugin.needsFindingButtonGuides = true;
                        Plugin.needsFindingCreditText = true;
                        
                    }
                }
                
            }
        }
    }

}
