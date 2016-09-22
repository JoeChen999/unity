using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Random = System.Random;

public class land : MonoBehaviour {
	public static int height=5;
	public static int width=5;
	public int seed=0;
	public GameObject wall;
	public GameObject verWall;
	public GameObject boss;
	private const int N = 1;
	private const int S = 2;
	private const int E = 4;
	private const int W = 8;
	private Random random;
	Dictionary<int, int> DX = new Dictionary<int, int> () {
		{ E, 1 },
		{ W,-1 },
		{ N, 0 },
		{ S, 0 }
	};
	Dictionary<int, int> DY = new Dictionary<int, int> () {
		{ E, 0 },
		{ W, 0 },
		{ N,-1 },
		{ S, 1 }
	};
	Dictionary<int, int> OPPOSITE = new Dictionary<int, int> () {
		{ E, W },
		{ W, E },
		{ N, S },
		{ S, N }
	};
	// Use this for initialization
	void Start () {
		if (seed == 0) {
			Random rd = new Random();
			seed = rd.Next();
		}
		random = new Random (seed);
		int[,] grid = new int[width, height];
		for (int x = 0; x<width; x++) {
			for(int y = 0; y<height; y++){
				grid[x,y]=0;
			}
		}
		carve_passages_from(0,0,grid);
		genMaze (grid);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void carve_passages_from(int cx,int cy, int[,] grid){
		//int[] directions = new int[]{N,S,E,W};
		List<int> directions = new List<int>();
		directions.Add (N);
		directions.Add (S);
		directions.Add (E);
		directions.Add (W);
		directions = RandomSortList<int>(directions);
		for (int i =0; i<4; i++) {
			int nx=cx+DX[directions[i]];
			int ny=cy+DY[directions[i]];
			if((ny>=0 && ny<height) && (nx>=0 && nx<width) && (grid[ny,nx] == 0)){
				grid[cy,cx] |= directions[i];
				grid[ny,nx] |= OPPOSITE[directions[i]];
				carve_passages_from(nx,ny,grid);
			}
		}
	}

	void genMaze(int[,] grid){
		Vector3 pos = wall.transform.position;
		Quaternion rot = wall.transform.rotation;
		for (int i=0; i<width; i++) {
			GameObject mazeWall = Instantiate(wall,pos,wall.transform.rotation) as GameObject;
			mazeWall.SetActive(true);
			pos.x+=10;
		}

		Vector3 verPos=verWall.transform.position;
		Quaternion verRot=verWall.transform.rotation;
		pos=wall.transform.position;
		pos.z -= 10;
		//rot.eulerAngles.y = Mathf.PI * 0.5f;
		Vector3 horPos=pos;
		Quaternion horRot=rot;
		float initXv = verPos.x;
		float initXh = horPos.x; 
		for (int y=0; y<height; y++) {

			GameObject mazeWall = Instantiate(wall,verPos,verRot) as GameObject;
			mazeWall.SetActive(true);
			
			for (int x = 0; x<width; x++){
				if((grid[y,x] & S) == 0){
					GameObject mazeWall1 = Instantiate(wall,horPos,horRot) as GameObject;
					mazeWall1.SetActive(true);
				}
				verPos.x+=10;
				horPos.x+=10;
				if((grid[y,x] & E) != 0){
					if ((((grid[y,x] | grid[y,x+1]) & S) == 0)){
						GameObject mazeWall2 = Instantiate(wall,horPos,horRot) as GameObject;
						mazeWall2.SetActive(true);
					}
				}
				else{
					GameObject mazeWall3 = Instantiate(wall,verPos,verRot) as GameObject;
					mazeWall3.SetActive(true);
				}
			}
			verPos.z-=10;
			horPos.z-=10;
			verPos.x=initXv;
			horPos.x=initXh;
		}
		string ePos = "";
		for (int i=0; i<3; i++) {
			int h;
			int v;
			do{
				h = random.Next (1, width-1);
				v = random.Next (1, height-1);
			}
			while(ePos.Contains(h.ToString()+","+v.ToString()));
			ePos+=h.ToString()+","+v.ToString()+";";

			int x = h*10;
			int z = -v*10;
			Vector3 enemyPos=pos;
			enemyPos.x=x;
			enemyPos.z=z;
			GameObject enemy = Instantiate(boss,enemyPos,rot) as GameObject;
			enemy.SetActive(true);
		}

	}
	
	public List<T> RandomSortList<T>(List<T> ListT)
	{

		List<T> newList = new List<T>();
		foreach (T item in ListT)
		{
			newList.Insert(random.Next(newList.Count + 1), item);
		}
		return newList;
	}
}
