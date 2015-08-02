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


public class YamlDotNetYamlOutputGameObjectYaml : EditorWindow {


	GameObject prefabAsset;

	void OnGUI ()
	{

		GUILayout.Label ("YamlDotNetYamlOutput", EditorStyles.boldLabel);

		/*
		GUILayout.Space (10f);
		prefabAsset = EditorGUILayout.ObjectField ("prefab", prefabAsset, typeof(GameObject), false) as GameObject;
		if (prefabAsset == null) {
			GUILayout.Label ("Set GameObject prefab or other prefab object.");
		}
*/



		GUILayout.Space (20f);
		if (GUILayout.Button ("YamlDotNetYamlOutput", GUILayout.Width (300f))) {
			YamlDotNetYamlOut();
		}

	}


	void YamlDotNetYamlOut()
	{

		//シリアライズするオブジェクトを定義する
		var calendar = new AdventCalendar2();
		calendar.Year = new DateTime(2012, 12, 1);
		calendar.Description = "C# Advent Calender 2012";
		calendar.Contents.Add(new Content2()  {
			Day = 1,
			Author = "gushwell",
			Description = ".NET Framework4.5 での非同期I/O処理 事始め",
			URL = @"http://gushwell.ldblog.jp/archives/52290230.html"
            });
		calendar.Contents.Add(new Content2() {
			Day = 2,
			Author = "KTZ",
			Description = "年末の大掃除",
			URL = @"http://ritalin.github.com/2012/12-02/csharp-advent-2012/"
            });
		calendar.Contents.Add(new Content2() {
			Day = 3,
			Author = "neuecc",
			Description = "MemcachedTranscoder – C#のMemcached用シリアライザライブラリ",
			URL = @"http://neue.cc/2012/12/03_389.html"
            });


		TextWriter textWriter = new StreamWriter(Application.dataPath + "/YamlDotNetForUnityYAML/Editor/YamlDotNetYamlOutputGameObject_yaml.yaml");
		textWriter.WriteLine("%YAML1.1");//無視される。
		textWriter.WriteLine("%TAG !u! tag:unity3d.com,2011:");//無視される。
		//var serializer = new Serializer();
		YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
		/// ※↑でSerializerが存在しないと怒られたら以下ので Genericsを書かないと空のYAMLになるので注意
		//var serializer = new YamlSerializer<AdventCalendar>();
		Debug.Log("calendar="+calendar);
		serializer.Serialize(textWriter,calendar);

		// don't forget Close()
		textWriter.Close();
		AssetDatabase.Refresh();
		//間の!U!タグを入れるために
		//TO 3ファイルつくって合成させる 荒技でいける気がする。めんどくさいのでリードの方からセーブしてみる。
		//%YAML1.1
		//!u!4 &12345
		//GamObject クラス
		//!u!4 &12345
		//Transform クラス
		//!u!4 &12345
		//Prefab クラス
	}
	// Use this for initialization





	////////////////////////////////////////////////////////////////////////////////

	#region Static
	/// <summary>
	/// Open the tool window
	/// </summary>
	[MenuItem("Tools/GameObject/YamlDotNetYamlOutputGameObjectYaml")]
	static public void OpenWindow13 ()
	{
		EditorWindow.GetWindow<YamlDotNetYamlOutputGameObjectYaml> (true, "YamlDotNetYamlOutputGameObjectYaml", true);
	}
	#endregion
}
public class Content2
{
	public int Day { get; set; }
	public string Author { get; set; }
	public string Description { get; set; }
	public string URL { get; set;}   

}


public class AdventCalendar2
{
	public DateTime Year { get; set; }
	public string Description { get; set; }
	public List<Content2> Contents { get; set; }

	public AdventCalendar2() {
		this.Contents = new List<Content2>();
	}

	public AdventCalendar2(List<Content2> contents) {
		this.Contents = contents;
	}
}
