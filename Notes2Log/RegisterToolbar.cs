
using UnityEngine;
using ToolbarControl_NS;
using KSP_Log;

namespace Notes2Log_NS
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        internal static Log Log = null;

        void Awake()
        {
            if (Log == null)
#if DEBUG
                Log = new Log("LogNotes", Log.LEVEL.INFO);
#else
          Log = new Log("LogNotes", Log.LEVEL.ERROR);
#endif

        }

        void Start()
        {
            ToolbarControl.RegisterMod(Notes2Log.MODID, Notes2Log.MODNAME);
        }
    
    }
}
