﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using KSP.Localization;



namespace Notes2Log_NS
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings
    // HighLogic.CurrentGame.Parameters.CustomParams<Notes2Log_Settings>().
    public class Notes2Log_Settings : GameParameters.CustomParameterNode
    {
        public override string Title { get { return ""; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "Notes2Log"; } }
        public override string DisplaySection { get { return "Notes2Log"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }

        //private bool DebugMode = false;
        //private bool SunDamage = false;
        //private bool GyroDecay = false;
        //private bool AsteroidSpawner = false;

        [GameParameters.CustomParameterUI("Alternate skin",
            toolTip = "Use alternate skin")]
        public bool altSkin = false;

        public override void SetDifficultyPreset(GameParameters.Preset preset)
        { }

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        { return true; }
        public override bool Interactible(MemberInfo member, GameParameters parameters)
        { return true; }
        public override IList ValidValues(MemberInfo member)
        { return null; }
    }

}
