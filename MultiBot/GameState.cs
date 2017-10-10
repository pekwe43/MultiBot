﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enigma.D3.ApplicationModel;
using Enigma.D3.Assets;
using Enigma.D3.AttributeModel;
using Enigma.D3.DataTypes;
using Enigma.D3.Enums;
using Enigma.D3.MemoryModel;
using Enigma.D3.MemoryModel.Core;
using Enigma.D3;
using Enigma.D3.MemoryModel.Controls;
using static Enigma.D3.MemoryModel.Core.UXHelper;

namespace EnvControllers
{
    public class GameState
    {
        //Memory context and Snapshot
        public MemoryContext ctx { get; }
        public ApplicationSnapshot snapshot {
            get
            {
                var _snapshot = new ApplicationSnapshot(this.ctx);
                try
                {
                    _snapshot.Update();
                    inGame = true;
                    inMenu = false;
                    return _snapshot;
                }
                catch
                {
                    if (ctx.DataSegment.LevelArea == null)
                    {
                        inGame = false;
                        inMenu = true;
                    }
                    else
                    {
                        inGame = true;
                        inMenu = false;
                    }
                    return _snapshot;
                }
                
            }
            set { }
        }
        public GameState() {
            ctx = MemoryContext.FromProcess();
            UpdateGameState();
        }

        public void UpdateSnapshot() {
            var _snapshot = new ApplicationSnapshot(this.ctx);
            try
            {
                _snapshot.Update();
                inGame = true;
                inMenu = false;
            }
            catch
            {
                if (ctx.DataSegment.LevelArea == null)
                {
                    inGame = false;
                    inMenu = true;
                }
                else
                {
                    inGame = true;
                    inMenu = false;
                }
            }
            this.snapshot = _snapshot;
        }

        public void UpdateGameState()
        {
            UpdateSnapshot();
            var update0 = openriftUiVisible;
            var update1 = acceptgrUiVisible;
            var update2 = urshiUiVisible;
            var update3 = grcompleteUiVisible;
            var update4 = confirmationUiVisible;
            var update5 = vendorUiVisible;
            var update6 = leavegameUiVisible;
            var update7 = cancelgriftUiVisible;
            var update8 = firstlevelRift;
            var update9 = haveUrshiActor;
        }

        //Properties UX
        public UXControl openriftUiControl { get; set; }
        public bool openriftUiVisible {
            get
            {
                if (inMenu == false)
                {
                    openriftUiControl = UXHelper.GetControl<UXControl>("Root.NormalLayer.rift_dialog_mainPage.LayoutRoot.MainBG");
                    return openriftUiControl.IsVisible();
                }
                else{
                    return false;
                }
            }
        }
        public UXControl acceptgrUiControl { get; set; }
        public bool acceptgrUiVisible
        {
            get
            {
                if (inMenu == false)
                {
                    acceptgrUiControl = UXHelper.GetControl<UXControl>("Root.NormalLayer.rift_join_party_main.stack.wrapper");
                    return acceptgrUiControl.IsVisible();
                }
                else
                {
                    return false;
                }
            }
        }
        public UXControl urshiUiControl { get; set; }
        public bool urshiUiVisible
        {
            get
            {
                if (inMenu == false)
                {
                    urshiUiControl = UXHelper.GetControl<UXControl>("Root.NormalLayer.vendor_dialog_mainPage.riftReward_dialog.LayoutRoot.gemUpgradePane.centerWisp");
                    return urshiUiControl.IsVisible();
                }
                else
                {
                    return false;
                }
            }
        }
        public UXControl grcompleteUiControl { get; set; }
        public bool grcompleteUiVisible
        {
            get
            {
                if (inMenu == false)
                {
                    grcompleteUiControl = UXHelper.GetControl<UXControl>("Root.NormalLayer.GreaterRifts_VictoryScreen.LayoutRoot.PartyContainer.PlayerContainer.Player1.Player1Profile.Player1Banner.Player1Clan");
                    return grcompleteUiControl.IsVisible();
                }
                else
                {
                    return false;
                }
            }
        }
        public UXControl confirmationUiControl { get; set; }
        public bool confirmationUiVisible
        {
            get
            {
                if (inMenu == false)
                {
                    confirmationUiControl = UXHelper.GetControl<UXControl>("Root.TopLayer.confirmation.subdlg");
                    return confirmationUiControl.IsVisible();
                }
                else
                {
                    return false;
                }
            }
        }
        public UXControl vendorUiControl { get; set; }
        public bool vendorUiVisible
        {
            get
            {
                if (inMenu == false)
                {
                    vendorUiControl = UXHelper.GetControl<UXControl>("Root.NormalLayer.vendor_dialog_mainPage");
                    return vendorUiControl.IsVisible();
                }
                else
                {
                    return false;
                }
            }
        }
        public UXControl leavegameUiControl { get; set; }
        public bool leavegameUiVisible
        {
            get
            {
                if (inMenu == false)
                {
                    leavegameUiControl = UXHelper.GetControl<UXControl>("Root.NormalLayer.gamemenu_dialog.gamemenu_bkgrnd.ButtonStackContainer.button_leaveGame");
                    return leavegameUiControl.IsVisible();
                }
                else
                {
                    return false;
                }
            }
        }

        //Other Properties
        public bool cancelgriftUiVisible
        {
            get
            {
                if (confirmationUiVisible == true & vendorUiVisible == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public bool firstlevelRift
        {
            get
            {
                if (ctx.DataSegment.LevelAreaName == "Greater Rift Floor 1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public bool haveUrshiActor
        {
            get
            {
                try
                {
                    if (snapshot.Game.Monsters.FirstOrDefault(ab => ab.ActorSNO == 398682) != null) //Urshi ID
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { return false; }
            }
        }

        //State Properties
        public bool inGame { get; set; }
        public bool inMenu { get; set; }


    }
}
