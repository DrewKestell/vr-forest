using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopParticleSystems : EditorWindow
{
    string createButton = "Create";

    [MenuItem("MyTools/Stop Particle Systems...")]
    static void Init()
    {
        var window = (StopParticleSystems)GetWindow(typeof(StopParticleSystems), true, "Stop Particle Systems");
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button(createButton))
        {
            // get root objects in scene
            List<GameObject> rootObjects = new List<GameObject>();
            Scene scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(rootObjects);

            // iterate root objects and do something
            for (int i = 0; i < rootObjects.Count; ++i)
            {
                GameObject gameObject = rootObjects[i];
                var particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();

                foreach (var ps in particleSystems)
                {
                    var main = ps.main;
                    main.loop = false;
                    ps.Stop();
                }
            }
        }
    }
}
