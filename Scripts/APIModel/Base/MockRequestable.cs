using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Momiji
{
	public class MockRequestable : Requestable
	{
		private readonly string DIRECTORY_NAME = "/MockAPI/";

		protected new string HostName
		{
			get
			{
#if UNITY_IPHONE
				return Application.dataPath + "/Raw" + DIRECTORY_NAME;
#elif UNITY_ANDROID
				return "jar:file://" + Application.dataPath + "!/assets" + DIRECTORY_NAME;
#else
				return Application.streamingAssetsPath + DIRECTORY_NAME;
#endif
			}
		}
	}
}