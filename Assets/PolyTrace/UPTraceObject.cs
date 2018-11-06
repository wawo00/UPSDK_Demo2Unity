using UnityEngine;
using System.Collections;
namespace UPTrace {
	public class UPTraceObject : MonoBehaviour
	{


		private static UPTraceObject instance = null;
		public static readonly string GameObject_Callback_Name = "UPTraceSDK_Callback_Object";
		public static readonly string Java_Callback_Function = "onTargetCallback";


		public static UPTraceObject getInstance()
		{
			if (instance == null) {
				GameObject polyCallback = new GameObject (GameObject_Callback_Name);
				polyCallback.hideFlags = HideFlags.HideAndDontSave;
				DontDestroyOnLoad (polyCallback);

				instance = polyCallback.AddComponent<UPTraceObject> ();
			}
			return instance;
		}

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}
	}
}


