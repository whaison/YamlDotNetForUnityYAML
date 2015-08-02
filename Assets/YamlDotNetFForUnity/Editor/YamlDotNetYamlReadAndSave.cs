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


public class YamlDotNetYamlReadAndSave : EditorWindow {

	UnityEngine.Object textAsset;
	GameObject prefabAsset;

	void OnGUI ()
	{

		GUILayout.Label ("YamlDotNetYamlReadAndSave", EditorStyles.boldLabel);

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
		if (GUILayout.Button ("YamlDotNetYamlReadAndSave", GUILayout.Width (300f))) {
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
		//var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
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
			foreach (YamlDotNet.RepresentationModel.YamlNode yamlNode in yaml.Documents[i].AllNodes) {
				Debug.Log(yamlNode.ToString());
			}
			//foreach (var child in item) {
			//	Debug.Log(((YamlScalarNode)child.Key).Value + "\t" +
			//		((YamlScalarNode)child.Value).Value);
			//}
			
		}
		TextWriter textWriter = new StreamWriter(Application.dataPath + "/Editor/YamlDotNetYamlReadAndSave_yaml.yaml");
		textWriter.WriteLine("%YAML 1.1");//無視される。
		textWriter.WriteLine("%TAG !u! tag:unity3d.com,2011:");//無視される。
		yaml.Save(textWriter);

		textWriter.Close();
		AssetDatabase.Refresh();
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
	[MenuItem("Tools/GameObject/YamlDotNetYamlReadAndSave")]
	static public void OpenWindow14 ()
	{
		EditorWindow.GetWindow<YamlDotNetYamlReadAndSave> (true, "YamlDotNetYamlReadAndSave", true);
	}
	#endregion
}
