using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
    public static class Utils
    {
        // Is Mouse over a UI Element? Used for ignoring World clicks through UI
        public static bool IsPointerOverUi()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
            else
            {
                var pe = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
                var hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pe, hits);
                return hits.Count > 0;
            }
        }
    }
}