using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using YamlDotNet;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;


public class YamlDotNetYamlReader : EditorWindow {

	UnityEngine.Object textAsset;
	GameObject prefabAsset;

	void OnGUI ()
	{

		GUILayout.Label ("YamlDotNetYamlReader", EditorStyles.boldLabel);

		GUILayout.Space (10f);
		textAsset = EditorGUILayout.ObjectField ("YAML Object", textAsset, typeof(UnityEngine.Object), false) as UnityEngine.Object;
		if (textAsset == null) {
			GUILayout.Label ("Set YAML Data. Extension need .YAML Object");
		}
		/*
		GUILayout.Space (10f);
		prefabAsset = EditorGUILayout.ObjectField ("prefab", prefabAsset, typeof(GameObject), false) as GameObject;
		if (prefabAsset == null) {
			GUILayout.Label ("Set GameObject prefab or other prefab object.");
		}
		*/



		GUILayout.Space (20f);
		if (GUILayout.Button ("YamlDotNetYamlReader", GUILayout.Width (300f))) {
			YamlDotNetYamlRead();
		}

	}


	void YamlDotNetYamlRead()
	{

		//textAsset
		string fileName=AssetDatabase.GetAssetPath(textAsset);
		// open
		var input = new StreamReader(fileName, Encoding.UTF8);
		var yaml = new YamlStream();
		yaml.Load(input);
		var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
		Debug.Log("yaml.Documents.Count="+ yaml.Documents.Count);
		//foreach (YamlMappingNode item in (YamlMappingNode)yaml.Documents) {
		for(int i = 0; i < yaml.Documents.Count; i++){

			string str="";
			//str="(YamlMappingNode)yaml.Documents["+i+"].RootNode= ";
			str=str+(YamlMappingNode)yaml.Documents[i].RootNode+"\n";//Macの場合 optionキーを押しながら¥を押す
			Debug.Log(str);
			//for(int j = 0; j < yaml.Documents[i].AllNodes.Count(); j++){
				//string str1="";
				//str="(YamlMappingNode)yaml.Documents["+i+"].RootNode= ";
			//	str1=str1+(YamlDotNet.RepresentationModel.YamlNode)yaml.Documents[i].AllNodes[j]+"\n";//Macの場合 optionキーを押しながら¥を押す
			//}
			foreach (YamlDotNet.RepresentationModel.YamlNode child in yaml.Documents[i].AllNodes) {
				Debug.Log(child.ToString());
			}
			//foreach (var child in item) {
			//	Debug.Log(((YamlScalarNode)child.Key).Value + "\t" +
			//		((YamlScalarNode)child.Value).Value);
			//}
			
		}
		//var Year = (YamlScalarNode)mapping.Children[new YamlScalarNode("Year")];
		//Debug.Log("Year "+ Year.Value);
		//var Description = (YamlScalarNode)mapping.Children[new YamlScalarNode("Description")];
		//Debug.Log("Desciption "+ Description.Value);
		//Debug.Log("Contents:");
		
		//var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("Contents")];
		//foreach (YamlMappingNode item in items) {
		//	foreach (var child in item) {
		//		Debug.Log(((YamlScalarNode)child.Key).Value + "\t" +
		//			((YamlScalarNode)child.Value).Value);
		//	}
		//}
		
	}






	////////////////////////////////////////////////////////////////////////////////

	#region Static
	/// <summary>
	/// Open the tool window
	/// </summary>
	[MenuItem("Tools/GameObject/YamlDotNetYamlReader")]
	static public void OpenWindow13 ()
	{
		EditorWindow.GetWindow<YamlDotNetYamlReader> (true, "YamlDotNetYamlReader", true);
	}
	#endregion
}
