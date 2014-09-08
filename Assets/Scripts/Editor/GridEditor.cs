using UnityEngine;
using UnityEditor;
using System.Collections;

enum DRAWOPTION {select, paint, paintover, erase};


[CustomEditor (typeof(Grid))]
public class GridEditor : Editor
{

	Grid grid;
	private static GameObject parentObj;
	private static GameObject prefabObj;


	private static bool isDraw;
	private static DRAWOPTION selected;

	public void OnEnable()
	{
		grid = (Grid)target;
		SceneView.onSceneGUIDelegate = GridUpdate;
	}

	public void loadGUIParameters()
	{
		GUILayout.BeginHorizontal();
			GUILayout.Label(" Grid Width ");
			grid.width = EditorGUILayout.FloatField(grid.width, GUILayout.Width(50));
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
			GUILayout.Label(" Grid Height ");
			grid.height = EditorGUILayout.FloatField(grid.height, GUILayout.Width(50));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Parent Object");
			parentObj = (GameObject)EditorGUILayout.ObjectField(parentObj, typeof(GameObject),true,GUILayout.Width(150));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Prefab");
			prefabObj = (GameObject)EditorGUILayout.ObjectField(prefabObj, typeof(GameObject),true,GUILayout.Width(150));
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Drawing");
			isDraw = EditorGUILayout.Toggle(isDraw, GUILayout.Width(16));
		GUILayout.EndHorizontal ();

	}

	public void paintGrid()
	{
		if (grid.width > 0 && grid.height > 0)
		{
			SceneView.RepaintAll();
		}
		else
		{
			//DE MOMENTO LO HACEMOS A LO CUTRE Y NOS APAÑAMOS
			grid.width = 0.32f;
			grid.height= 0.32f;
			Debug.Log("Grid Error - Width and height must be positive values.\nDefault values have been restablished");
		}
	}
	public override void OnInspectorGUI()
	{

		loadGUIParameters ();
		paintGrid ();


	
	}
	
	void GridUpdate(SceneView sceneview)
	{
		Event e = Event.current;


		Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
		Vector3 mousePos = r.origin;

		if (isDraw)
		{
			//if (e.isKey && e.character == 'a')	//Clona el objeto seleccionado en la posicion del raton, centrado en el grid.
			if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown) && e.button == 0)
			{
				GameObject obj;
				//Object prefab = EditorUtility.GetPrefabParent(Selection.activeObject);

				Object prefab = prefabObj;
				
				if (prefab)
				{
					//Undo.IncrementCurrentEventIndex(); //NO RECONOCE ESTE METODO
					obj = (GameObject)EditorUtility.InstantiatePrefab(prefab);
					Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x/grid.width)*grid.width + grid.width/2.0f,
					                              Mathf.Floor(mousePos.y/grid.height)*grid.height + grid.height/2.0f, 0.0f);
					obj.transform.position = aligned;
					Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);

					if (parentObj != null)
					{
						obj.transform.parent = parentObj.transform;
					}
				}
			}
		}

	}
}