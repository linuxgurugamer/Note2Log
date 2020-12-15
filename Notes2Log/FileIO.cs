using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Notes2Log_NS.RegisterToolbar;

namespace Notes2Log_NS
{
    internal class FileIO
    {
        const string NOTES = "notes2log.cfg";
        const string NODENAME = "Notes";
        const string NOTE_NODE_NAME = "NOTE";
        string path {  get {  return KSPUtil.ApplicationRootPath.Replace("\\", "/") + "GameData/Notes2Log/PluginData/" + NOTES; } }
        internal void LoadSettings()
        {
            if (File.Exists(path))
            {
                ConfigNode file = ConfigNode.Load(path);
                Notes2Log.notes.Clear();
                ConfigNode notesNode = file.GetNode(NODENAME);
                if (notesNode != null)
                {
                    var notes = notesNode.GetNodes(NOTE_NODE_NAME);
                    foreach (var n in notes)
                    {
                        string title = n.SafeLoad("title", "");
                        string note = n.SafeLoad("note", "");
                        if (title != "" && note != "")
                        {
                            Notes2Log.notes.Add(title, new Notes2Log.LogNote(title, note));
                        }
                    }
                }
            }
        }

        internal void SaveSettings()
        {
            ConfigNode file = new ConfigNode("LogNotesFile");
            ConfigNode notesNode = new ConfigNode(NODENAME);
            foreach (var n in Notes2Log.notes.Values)
            {
                ConfigNode data = new ConfigNode(n.title);
                data.AddValue("title", n.title);
                data.AddValue("note", n.note);
                notesNode.AddNode(NOTE_NODE_NAME, data);
            }
            file.AddNode(notesNode);
            file.Save(path);
        }
    }
}
