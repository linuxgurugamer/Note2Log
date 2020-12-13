using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KSP.UI.Screens;
using ToolbarControl_NS;
using ClickThroughFix;
using static Notes2Log_NS.RegisterToolbar;

namespace Notes2Log_NS
{
    [KSPAddon(KSPAddon.Startup.AllGameScenes, false)]
    public class Notes2Log : MonoBehaviour
    {
        const float WIDTH = 400;
        const float HEIGHT = 200;
        public class LogNote
        {
            public string title;
            public string note;

            public LogNote(string title, string note)
            {
                this.title = title;
                this.note = note;
            }
            public void Clear()
            {
                title = "";
                note = "";
            }
        }

        static internal Dictionary<string, LogNote> notes = new Dictionary<string, LogNote>();
        LogNote activeNote;

        internal const string MODID = "Notes2Log";
        internal const string MODNAME = "Notes2Log";
        static internal ToolbarControl toolbarControl = null;

        Rect _revertRect = new Rect(Screen.width - WIDTH, Screen.height - HEIGHT, WIDTH, HEIGHT);

        FileIO fileIO = new FileIO();

        void Start()
        {
            AddToolbarButton();
            activeNote = new LogNote("", "");
            fileIO.LoadSettings();
        }

        void AddToolbarButton()
        {
            if (toolbarControl == null)
            {
                toolbarControl = gameObject.AddComponent<ToolbarControl>();
                toolbarControl.AddToAllToolbars(GUIButtonToggle, GUIButtonToggle,
                    ApplicationLauncher.AppScenes.ALWAYS,
                    MODID,
                    "logNotesButton",
                    "Notes2Log/PluginData/Textures/icon-38",
                    "Notes2Log/PluginData/Textures/icon-24",
                    MODNAME
                );
            }
        }
        bool modVisible = false;

        void GUIButtonToggle()
        {
            modVisible = !modVisible;
        }


        void OnGUI()
        {
            if (modVisible)
            {
                if (!HighLogic.CurrentGame.Parameters.CustomParams<Notes2Log_Settings>().altSkin)
                    GUI.skin = HighLogic.Skin;
                _revertRect = ClickThruBlocker.GUILayoutWindow(this.GetInstanceID() + 1, _revertRect, new GUI.WindowFunction(NotesWindow), "Notes2Log", new GUILayoutOption[0]);

            }
        }

        Vector2 scrollPosition = new Vector2(0, 0);
        void NotesWindow(int id)
        {
            {
                GUILayout.BeginHorizontal();
                GUILayout.EndHorizontal();
            }
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.BeginVertical();
                    GUILayout.Space(10);
                    GUILayout.EndVertical();
                }
                {
                    GUILayout.BeginVertical(GUILayout.Width(90));
                    HighLogic.CurrentGame.Parameters.CustomParams<Notes2Log_Settings>().altSkin =
                        GUILayout.Toggle(HighLogic.CurrentGame.Parameters.CustomParams<Notes2Log_Settings>().altSkin, "Alt.Skin");
                    //GUILayout.Space(35);
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(HEIGHT - 50));
                    foreach (var n in notes.Values)
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(n.title, GUILayout.Width(80)))
                            activeNote = n;
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndScrollView();
                    //}
                    GUILayout.EndVertical();
                }
                {
                    GUILayout.BeginVertical();
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        GUILayout.EndHorizontal();
                    }
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Note:");
                        activeNote.title = GUILayout.TextField(activeNote.title, GUILayout.Width(200));
                        GUILayout.EndHorizontal();
                    }
                    {
                        GUILayout.BeginHorizontal();
                        activeNote.note = GUILayout.TextArea(activeNote.note, GUILayout.Height(HEIGHT - 70), GUILayout.ExpandWidth(true));
                        GUILayout.EndHorizontal();
                    }
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Write to log"))
                            WriteToLog();
                        if (GUILayout.Button("Save"))
                        {
                            notes.Add(activeNote.title, new LogNote(activeNote.title, activeNote.note));
                            fileIO.SaveSettings();
                        }
                        if (GUILayout.Button("Delete"))
                        {
                            if (notes.ContainsKey(activeNote.title))
                                notes.Remove(activeNote.title);
                        }
                        if (GUILayout.Button("Clear"))
                        {
                            activeNote.Clear();
                        }
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();
            }
            GUI.DragWindow();
        }

        public void WriteToLog()
        {
            UnityEngine.Debug.Log("=====================================================================");
            UnityEngine.Debug.Log(activeNote.note);
            UnityEngine.Debug.Log("=====================================================================");
        }


    }

}
