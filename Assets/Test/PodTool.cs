using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;


namespace XXPod {

	public class PodTool {

		//获取所有路径
		public static void fixPathWithSetConfigurationFile(){
			//目标文件路径
			string appPath = System.IO.Path.Combine (Application.dataPath, "PolyADSDK/Plugins/PolyADSDK.projmods");
			//文件路径
			string filePath = System.IO.Path.Combine (Application.dataPath, "PolyADSDK/Plugins/IOS/frameworks");
			if(Directory.Exists(appPath)){
				Directory.Delete (appPath);
			}

			//创建可变数组存储路径
			List<string> files = new List<string> (); 
			deepFindFilePath (files, filePath);
//			DirectoryInfo dir = new DirectoryInfo(filePath);  
//			foreach (DirectoryInfo dChild in dir.GetDirectories()) {
//				string path = dChild.FullName;
//				Debug.Log ("path:" + path);
//				if (path.EndsWith ("framework")) {
//					string[] frameworkStr = Regex.Split (path, "Plugins", RegexOptions.IgnoreCase);
//					files.Add (frameworkStr [1]);
//				} else {
//					files = getFilePath (files, path);
//				}
//			}

			//数据字典
			Dictionary<string, object> dic = new Dictionary<string, object>();
			dic = wirteKeyWithValueToDic (dic);
			dic.Add ("files", files);

			//字典转JSON
			//		string json = JsonConvert.SerializeObject( dic );
			//		Debug.Log (json);
			string json = "{\n";
			json = json + getString(dic);
			if (json.Length > 0) {
				json = json.Substring (0, json.Length - 2);
			}
			json = json + "\n" + "}";
			//初始化文件操作相关函数
			FileStream fs = new FileStream(appPath, FileMode.Create);
			StreamWriter sw = new StreamWriter(fs);
			//写入
			sw.Write(json);
			//清空缓冲区
			sw.Flush();
			//关闭流
			sw.Close();
			fs.Close();
		}

		private static void deepFindFilePath(List<string> array, string path){
			string [] files = System.IO.Directory.GetFiles (path);
		

			//DirectoryInfo dir = new DirectoryInfo(path);
			//FileInfo[] fileinfos = dir.GetFiles;
			if (files != null) {
				foreach(string file in files){
					Debug.Log ("===> file: " + file);
					if (file.EndsWith (".framework") || file.EndsWith (".h") || file.EndsWith (".a")) {
						string[] frameworkStr = Regex.Split (file, "Plugins", RegexOptions.IgnoreCase);
						array.Add (frameworkStr [1]);
					}
				}
			}


			string [] dirs = System.IO.Directory.GetDirectories (path);
			if (dirs != null) {
				foreach(string dir in dirs){
					Debug.Log ("===> dir: " + dir);
					if (!dir.EndsWith (".framework")) {
						deepFindFilePath (array, dir);
					} else {
						string[] frameworkStr = Regex.Split (dir, "Plugins", RegexOptions.IgnoreCase);
						array.Add (frameworkStr [1]);
					}

				}
			}
		}

		//获取路径信息
		private static List<string> getFilePath(List<string> array, string path){
			DirectoryInfo dir = new DirectoryInfo(path);  


			foreach (FileInfo dChild in dir.GetFiles("*.h", SearchOption.AllDirectories)) {
				string[] resultString = Regex.Split (dChild.FullName, "Plugins", RegexOptions.IgnoreCase);
				array.Add (resultString [1]);
			}
			return array;
		}
		//设置固定配置
		private static Dictionary<string, object> wirteKeyWithValueToDic(Dictionary<string, object> dic){
			dic.Add ("group","PolyADSDK");

			string[] libs = {"libsqlite3.tbd", "libxml2.tbd", "libz.tbd", "libstdc++.tbd", "libc++.tbd"};
			dic.Add ("libs", libs);

			string[] frameworks = {"WebKit.framework","JavaScriptCore.framework","CoreMotion.framework","GLKit.framework",
				"SafariServices.framework","CoreLocation.framework","MessageUI.framework","EventKit.framework",
				"EventKitUI.framework","WatchConnectivity.framework", "StoreKit.framework", "Social.framework",
				"CoreTelephony.framework", "AdSupport.framework", "AVFoundation.framework", "AudioToolbox.framework",
				"MobileCoreServices.framework", "MediaPlayer.framework", "CoreMedia.framework", "CoreGraphics.framework",
				"CFNetwork.framework","SystemConfiguration.framework"};
			
			dic.Add ("frameworks", frameworks);

			string[] headerpaths = { };
			dic.Add ("headerpaths", headerpaths);

			string[] folders = {"IOS/resources/"};
			dic.Add ("folders", folders);

			string[] excludes = {"^.*.meta$", "^.*.mdown$", "^.*.pdf$"};
			dic.Add ("excludes", excludes);

			string[] compiler_flags = { };
			dic.Add ("compiler_flags", compiler_flags);

			string[] linker_flags = { };
			dic.Add ("linker_flags", linker_flags);

			string[] embed_binaries = { };
			dic.Add ("embed_binaries", embed_binaries);

			Dictionary<string, object> plist = new Dictionary<string, object>();
			plist.Add ("NSCalendarsUsageDescription" , "Some ad content may access calendar");
			plist.Add ("NSRemindersUsageDescription" , "Some ad content may access calendar");
			plist.Add ("NSCameraUsageDescription" , "Some ad content may access camera to take picture.");
			plist.Add ("NSPhotoLibraryUsageDescription" , "Some ad content may require access to the photo library.");

			dic.Add ("plist", plist);

			return dic;
		}

		private static string getString(Dictionary<string, object> dic){
			string str = "";
			foreach(var item in dic){
				if(item.Value is Dictionary<string, object>){
					str = str + "\"" + item.Key + "\"" + ":" + "{" + "\n";
					Dictionary<string, object> dicItem = item.Value as Dictionary<string, object>;
					string strMessage = getString (dicItem);
					str = str + strMessage + "," + "\n";
				}else if(item.Value is string[]){
					str = str + "\"" + item.Key + "\"" + ":" + "[";
					string listMessage = "";
					string[] strItem = item.Value as string[];
					foreach(string listItem in strItem){
						listMessage = listMessage + "\"" + listItem + "\"" + ",";
					}
					if (listMessage.Length > 1) {
						listMessage = listMessage.Substring(0, listMessage.Length -1);
					}
					str = str + listMessage + "]" + "," + "\n";
				}else if(item.Value is List<String>){
					str = str + "\"" + item.Key + "\"" + ":" + "[";
					string listMessage = "";
					List<String> arrayItem = item.Value as List<String>;
					foreach(string listItem in arrayItem){
						listMessage = listMessage + "\"" + listItem + "\"" + "," + "\n";
					}
					if (listMessage.Length > 1) {
						listMessage = listMessage.Substring(0, listMessage.Length -2);
					}
					str = str + listMessage + "]" + "," + "\n";
				}else{
					str = str + "\"" + item.Key + "\"" + ":" + "\"" + item.Value + "\"" + "," + "\n";
				}
			}
			if (str.EndsWith (",\n")) {
				str = str.Substring(0, str.Length-2) + "\n";
			}
			str = str + "}";
			return str;
		}
	}

}

