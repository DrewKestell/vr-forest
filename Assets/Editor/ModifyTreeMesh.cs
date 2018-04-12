using System.Linq;
using UnityEngine;
using UnityEditor;

public class ModifyTreeMesh : EditorWindow
{
    string createButton = "Create";

    [MenuItem("MyTools/Fix Tree Mesh...")]
    static void Init()
    {
        var window = (ModifyTreeMesh)GetWindow(typeof(ModifyTreeMesh), true, "Fix Tree Mesh");
        window.Show();
    }
    
    void OnGUI()
    {
        if (GUILayout.Button(createButton))
        {
            var treesNode = GameObject.Find("TestTrees");

            var testTrees = GameObject.FindGameObjectsWithTag("TestTree");

            foreach (var tree in testTrees)
            {
                tree.transform.Find("Trunk").gameObject.GetComponent<MeshCollider>();
            }
        }
    }
}
