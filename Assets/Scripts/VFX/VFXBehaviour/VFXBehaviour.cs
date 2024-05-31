using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class VFXBehaviour : MonoBehaviour {

    public float lifeTime;
    public bool hasParent;

    private GameObject _parent { get; set; }
    private VisualEffect _vfx;

    void Awake() {
        _vfx = this.GetComponent<VisualEffect>();
    }

    void Start() {        
        StartCoroutine(StartBehaviour());
    }

    void Update() {
        if (hasParent) {
            gameObject.transform.position = _parent.transform.position;
            gameObject.transform.rotation = _parent.transform.rotation;
        }
    }
    private IEnumerator StartBehaviour() {
        _vfx.Play();
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
        yield break;
    }

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(VFXBehaviour)), CanEditMultipleObjects]
    public class VFXBehaviourEditor : Editor {
        public override void OnInspectorGUI() {
            var editor = (VFXBehaviour)target;
            editor.hasParent = EditorGUILayout.Toggle("Follow parent", editor.hasParent);

            if (editor.hasParent) {
                editor._parent = (GameObject)EditorGUILayout.ObjectField("Parent", editor._parent, typeof(GameObject), true);
            }            
            
            editor.lifeTime = EditorGUILayout.Slider("Lifetime", editor.lifeTime, 0.1f, 10f);
        }
    }
#endif
    #endregion

}
