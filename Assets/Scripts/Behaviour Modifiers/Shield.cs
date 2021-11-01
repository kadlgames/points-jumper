using UnityEngine;

namespace Behaviour_Modifiers
{
    [AddComponentMenu("Behaviour Modifiers/Shield")]
    public class Shield : MonoBehaviour
    {

        #region Fields

        [SerializeField] private GameObject shieldGO;
        
        #endregion
        // Start is called before the first frame update
        private void Start()
        {
            // Check if this circleGO doesn't have a CircleMover component
            if (GetComponent<CircleMover>() != null) return;
            
            var radius = Mathf.Abs(shieldGO.transform.localPosition.y);
            var target = GameObject.Find("Jumper");
            var position = transform.position;
            var position1 = target.transform.position;
            var distance = Vector2.Distance(position, position1);
            var cos = -(position.x - position1.x) / distance;
            var sin = -Mathf.Sqrt(1 - Mathf.Pow(cos, 2f));
            var newPos = new Vector2 {x = radius * cos, y = radius * sin};
            shieldGO.transform.localPosition = newPos;
            shieldGO.transform.rotation = Quaternion.AngleAxis(90f - Mathf.Acos(cos)*Mathf.Rad2Deg, Vector3.forward);
        }
    }
}
