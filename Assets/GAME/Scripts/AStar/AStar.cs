using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {
	
	public class Node {
		public float scoreG;
		public float scoreH;
		public float scoreF;
		public Node parent;
		public TileController tile;
	}
	
	public List<Node> setClosed;
	public List<Node> setOpen;
	
	Node[] nodes;
	int maxNodes;
		
	Node CreateNode(TileController tile) {
		Node n = new Node();
		n.tile = tile;
		return n;
	}
	
	float[] weights;

public List<Node> Resolve(TileController tileStart, TileController tileGoal, bool ignoreEnd) {
		if (tileStart == null || tileGoal == null) return null;
		if (tileStart == tileGoal)
		{
			Debug.Log(tileStart + "=" + tileGoal);
			return null;
		}



		//if ((blockKind & BlockKind.Tower) == BlockKind.Tower)
		//	if (siteGoal.isTower) return null;

		//if ((blockKind & BlockKind.Unexplored) == BlockKind.Unexplored) {
		//	if ((siteStart.exploreState != Site.ExploreState.Explored && siteStart.exploreState != Site.ExploreState.Controlled) /*&& !siteStart.isTower*/) {
		//		Debug.Log ("1 - unexplored " + siteStart.name + " " + siteGoal.name);
		//		return null;
		//	}
		//}
		
		Node start = CreateNode(tileStart);
		start.scoreG = 0;
		start.scoreH = HeuristicCostEstimate(tileStart, tileGoal);
		start.scoreF = start.scoreG + start.scoreH;
		
		setOpen = new List<Node>();
		setClosed = new List<Node>();
		
		setOpen.Add(start);
		Node current;

		while (setOpen.Count != 0) {
			current = GetLowestFInOpenSetAndRemove();
			if (current.tile == tileGoal) {
				return ReconstructPath(current);
			}
			
			setClosed.Add(current);

			for (int i = 0; i < current.tile.connections.Count; i++)
			{
				//Debug.Log("Connections from: " + current.tile + ": TileA " + current.tile.connections[i].tileA + ": TileB " + current.tile.connections[i].tileB);
				Connection neighborConnection = current.tile.connections[i];
				TileController neighbourSite = neighborConnection.GetOpposedSite(current.tile);

				//	if (!(ignoreEnd && neighbourSite == tileGoal)) {
				//		if ((blockKind & BlockKind.Owner) == BlockKind.Owner) 
				//			if (neighbourSite.guild != tileStart.guild && neighbourSite.exploreState != Site.ExploreState.Explored) continue;

				//		if ((blockKind & BlockKind.Unexplored) == BlockKind.Unexplored) 
				//			if (neighbourSite.exploreState != Site.ExploreState.Explored && neighbourSite.exploreState != Site.ExploreState.Controlled) continue;

				//		if ((blockKind & BlockKind.Tower) == BlockKind.Tower)
				//			if (neighbourSite.isTower) continue;

				//		if ((blockKind & BlockKind.Occupied) == BlockKind.Occupied)
				//			if (!neighbourSite.IsEmpty) continue;

				//		// occupied by enemy mage
				//		if (blockKind != BlockKind.None) {
				//			if (!neighbourSite.IsEmpty && neighbourSite.mage.guild != tileStart.guild)
				//				continue;
				//		}
				//	}

				//	if ((blockKind & BlockKind.PathRevealed) == BlockKind.PathRevealed) 
				//		if (!neighborConnection.IsRevealed()) continue;

				float tentativeScoreG = current.scoreG + HeuristicCostEstimate(neighbourSite, current.tile/*, weights[(int)neighborConnection.connectionType]*/);
				float tentativeScoreH = HeuristicCostEstimate(neighbourSite, tileGoal);
				float tentativeScoreF = tentativeScoreG + tentativeScoreH;

				Node tmp = IsNodeInClosedSet(neighbourSite);
				
				if (tmp != null) {
					if (tentativeScoreF >= tmp.scoreF) {
						continue;
					}
					else {
						tmp.parent = current;
						tmp.scoreG = tentativeScoreG;
						tmp.scoreH = tentativeScoreH;
						tmp.scoreF = tmp.scoreG + tmp.scoreH;
						continue;
					}
				}

				if (IsNodeInOpenSet(neighbourSite) == null)
				{
					Node n = CreateNode(neighbourSite);
					n.scoreG = tentativeScoreG;
					n.scoreH = tentativeScoreH;
					n.scoreF = n.scoreG + n.scoreH;
					n.parent = current;
					setOpen.Add(n);
				}
			}
		}
		Debug.Log("ReturnNull");
		return null;
	}
	
	List<Node> ReconstructPath(Node startFrom) {
		List<Node> path = new List<Node>();
		do {
			path.Add(startFrom);
			startFrom = startFrom.parent;
		} while (startFrom != null);

		return path;
	}
	
	
	float deltax;
	float deltay;
	float deltaz;
	
	float HeuristicCostEstimate(TileController start, TileController goal, float weight = 1.0f) 
	{
		deltax = start.transform.localPosition.x - goal.transform.localPosition.x;
		if (deltax <0) deltax = -deltax;
		deltay = start.transform.localPosition.y - goal.transform.localPosition.y;
		if (deltay <0) deltay = -deltay;

		return (deltax + deltay)*weight;
	}
	
	Node IsNodeInClosedSet(TileController site) {
		for (int i=0; i<setClosed.Count; i++) {
			if (setClosed[i].tile == site)
				return setClosed[i];
		}
		return null;
	}
	
	Node IsNodeInOpenSet(TileController site) {
		for (int i=0; i<setOpen.Count; i++) {
			if (setOpen[i].tile == site)
				return setOpen[i];
		}
		return null;
	}
	
	Node GetLowestFInOpenSetAndRemove() {
		int index = 0;
		float val = setOpen[0].scoreF;
		for (int i=1; i<setOpen.Count; i++) {
			if (setOpen[i].scoreF < val) {
				index = i;
				val = setOpen[i].scoreF;
			}
		}
		
		Node n = setOpen[index];
		setOpen.RemoveAt(index);
		return n;
	}
	
	public static List<AStar.Node> FilterPath(List<AStar.Node> path) {
		if (path == null) return null;
		if (path.Count < 2) return null;
		
		List<AStar.Node> filteredPath = new List<AStar.Node>();		

		// invert path
		for (int i=path.Count-1; i>=0; i--) {
			filteredPath.Add(path[i]);			
		}
		
		return filteredPath;
	}
}
