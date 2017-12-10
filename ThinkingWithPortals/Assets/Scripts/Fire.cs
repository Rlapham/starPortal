using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class Fire : MonoBehaviour{

	public GameObject star;

	public Vector3 vectoAdd;

	public Vector3[] myVecs;
	
	//public constellation[] ourStars;

	void Start(){
		Write();
		MakeStars();
	}

	public void MakeStars(){
		Read();
		foreach (Vector3 vec in myVecs){
			Instantiate(star, vec, Quaternion.identity);
		}
	}

	[System.Serializable] public struct constellation{
		public float f1;
		public float f2;
		public float f3;

		public constellation(Vector3 someVec){
			f1 = someVec.x;
			f2 = someVec.y;
			f3 = someVec.z;
		}

	}

	public void Read(){

		string json = "";

		var request = WebRequest.Create("https://star-54791.firebaseio.com/positions.json");

		try{
		 	 request = WebRequest.Create("https://star-54791.firebaseio.com/positions.json");

		}
		catch{
			Debug.Log("nope");
		}
		request = WebRequest.Create("https://star-54791.firebaseio.com/positions.json");
		request.Method = "GET";
		request.ContentType = "application/json";
		var response = request.GetResponse();
		json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
		myVecs = StringToV3(Parse(json));
	}

	public Vector3 GetRandomVector(){
		Read();
		return myVecs[UnityEngine.Random.Range(0, myVecs.Length - 1)];
	}
	
	public void Write () {
		constellation newCon = new constellation(vectoAdd);

		string json = JsonUtility.ToJson(newCon);
	
		var request = WebRequest.Create("https://star-54791.firebaseio.com/positions.json");

		try{
		 	 request = WebRequest.Create("https://star-54791.firebaseio.com/positions.json");

		}
		catch{
			Debug.Log("nope");
		}
		request = WebRequest.Create("https://star-54791.firebaseio.com/positions.json");
		request.Method = "POST";
		request.ContentType = "application/json";
		var buffer = Encoding.UTF8.GetBytes(json);
		request.ContentLength= buffer.Length;
		request.GetRequestStream().Write(buffer, 0, buffer.Length);
		var response = request.GetResponse();
		json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
	}


	public string Parse(string someJSONString){
		someJSONString = someJSONString.Remove(0,26);
		while (NumOccurances(someJSONString, '}') >= 3){
			int nextIndex = someJSONString.IndexOf('}');	
			someJSONString = someJSONString.Remove(nextIndex - 1, 27);
		}
		someJSONString = someJSONString.Replace(":","");
		someJSONString = someJSONString.Replace("}","");
		someJSONString = someJSONString.Replace('"', ' ');
		someJSONString = someJSONString.Replace(',', ' ');
		return someJSONString;

	}

	
	Vector3[] StringToV3 (string s){
		string tempS = s;
		tempS = tempS.Replace("f1", "");
		tempS = tempS.Replace("f2", "");
		tempS = tempS.Replace("f3", "");
		List<int> blankSpaces = FindOccurances(tempS, ' ');

		RegexOptions options = RegexOptions.None;
		Regex regex = new Regex("[ ]{2,}", options);     
		tempS = regex.Replace(tempS, " ");
		tempS = tempS.Remove(0,1);

		List<int> spaceIndices = FindOccurances(tempS, ' ');
		int numVectors = (spaceIndices.Count / 3 ) + 1;
		
		string[] numsStrings = tempS.Split(' ');
		float[] nums = ConvertStringsToNums(numsStrings);
		Debug.Log(numVectors);
		myVecs = new Vector3[numVectors];
		for (int i = 0; i < numVectors; i ++){
			myVecs[i] = new Vector3(nums[i * 3], nums[i * 3 + 1], nums[i * 3 + 2]);
		}
		return myVecs;

		

		//
	}

	float[] ConvertStringsToNums(string[] strings){
		float[] newNums = new float[strings.Length];
		for (int i = 0; i < strings.Length; i ++){
			Debug.Log(strings[i]);
			try{
			newNums[i] = float.Parse(strings[i]);
			}
			catch{

			}
		} 
		return newNums;
	}
	


	int NumOccurances (string s, char c){
		List<int> foundIndexes = new List<int>();

		for (int i = s.IndexOf(c); i > -1; i = s.IndexOf(c, i + 1))
			{
					foundIndexes.Add(i);
			}
			return foundIndexes.Count;
	}

	static List<int> FindOccurances (string s, char c){
		List<int> foundIndexes = new List<int>();

		for (int i = s.IndexOf(c); i > -1; i = s.IndexOf(c, i + 1))
			{
					foundIndexes.Add(i);
			}
			return foundIndexes;
	}
}