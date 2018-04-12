using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CreateTreeWizard : EditorWindow
{
    static System.Random random = new System.Random();

    GameObject treeSpawner;
    List<GameObject> trees;
    List<GameObject> treesFractured;
    List<Material> treeFoliage;
    List<Material> treeFoliageFade;
    List<Material> treeBark;
    List<Material> treeBarkFade;

    string createButton = "Create";

    [MenuItem("MyTools/Create Tree Wizard...")]
    static void Init()
    {
        var window = (CreateTreeWizard)GetWindow(typeof(CreateTreeWizard), true, "Create Tree Wizard");
        window.Show();
    }

    void OnEnable()
    {
        var treeSpawnerGuid = AssetDatabase.FindAssets("TreeSpawner", new[] { "Assets/Prefabs/Trees" }).Single();
        var treeSpawnerPath = AssetDatabase.GUIDToAssetPath(treeSpawnerGuid);
        treeSpawner = AssetDatabase.LoadAssetAtPath(treeSpawnerPath, typeof(GameObject)) as GameObject;

        // get tree prefabs
        var treeGuids = AssetDatabase.FindAssets("l:TreeWhole", new[] { "Assets/Prefabs/Trees" }).Distinct();
        trees = new List<GameObject>();

        foreach (var guid in treeGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            trees.Add(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject);
        }

        // get treeFractured prefabs
        var treesFracturedGuids = AssetDatabase.FindAssets("l:TreeFractured", new[] { "Assets/Prefabs/Trees" }).Distinct();
        treesFractured = new List<GameObject>();

        foreach (var guid in treesFracturedGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            treesFractured.Add(AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject);
        }
        
        // get tree foliage materials
        var treeFoliageGuids = AssetDatabase.FindAssets("l:TreeFoliageOpaque", new[] { "Assets/Materials" }).Distinct();
        treeFoliage = new List<Material>();

        foreach (var guid in treeFoliageGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            treeFoliage.Add(AssetDatabase.LoadAssetAtPath(path, typeof(Material)) as Material);
        }

        // get tree foliage fade materials
        var treeFoliageFadeGuids = AssetDatabase.FindAssets("l:TreeFoliageFade", new[] { "Assets/Materials" }).Distinct();
        treeFoliageFade = new List<Material>();

        foreach (var guid in treeFoliageFadeGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            treeFoliageFade.Add(AssetDatabase.LoadAssetAtPath(path, typeof(Material)) as Material);
        }
        
        // get tree bark materials
        var treeBarkGuids = AssetDatabase.FindAssets("l:TreeBarkOpaque", new[] { "Assets/Materials" }).Distinct();
        treeBark = new List<Material>();

        foreach (var guid in treeBarkGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            treeBark.Add(AssetDatabase.LoadAssetAtPath(path, typeof(Material)) as Material);
        }

        // get tree bark fade materials
        var treeBarkFadeGuids = AssetDatabase.FindAssets("l:TreeBarkFade", new[] { "Assets/Materials" }).Distinct();
        treeBarkFade = new List<Material>();

        foreach (var guid in treeBarkFadeGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            treeBarkFade.Add(AssetDatabase.LoadAssetAtPath(path, typeof(Material)) as Material);
        }
    }

    void OnGUI()
    {
        if (GUILayout.Button(createButton))
        {
            var treesContainer = GameObject.Find("Trees");
            var newTreeSpawner = Instantiate(treeSpawner, treesContainer.transform);
            newTreeSpawner.transform.position = new Vector3(0, 0, 0);
            newTreeSpawner.name = "TreeSpawner";
            var rand = random.Next(trees.Count());
            var newTree = Instantiate(trees.ElementAt(rand), newTreeSpawner.transform);
            newTree.name = "Tree";
            if (Selection.activeGameObject != null)
                newTreeSpawner.transform.localPosition = Selection.activeGameObject.transform.localPosition;

            var trunk = newTree.transform.Find("Trunk");
            var treeFractureScript = trunk.GetComponent<TreeFracture>();

            treeFractureScript.FracturedPrefab = treesFractured.ElementAt(rand);

            rand = random.Next(treeBark.Count());
            trunk.GetComponent<Renderer>().material = treeBark.ElementAt(rand);
            treeFractureScript.TrunkFadeMaterial = treeBarkFade.ElementAt(rand);

            rand = random.Next(treeFoliage.Count());
            var foliage = newTree.transform.Find("Foliage");
            foliage.GetComponent<Renderer>().material = treeFoliage.ElementAt(rand);
            foliage.GetComponent<FoliageExplode>().FadeMaterial = treeFoliageFade.ElementAt(rand);

            // apply random rotation
            newTree.transform.Rotate(0, random.Next(360), 0);

            // apply random scale
            var randomScale = (float)(random.NextDouble() * 1.3 - 0.4);
            newTree.transform.localScale += new Vector3(randomScale, randomScale, randomScale);

            // set new tree as current selection in editor to make it easier to move
            Selection.SetActiveObjectWithContext(newTreeSpawner, null);
        }
    }
}