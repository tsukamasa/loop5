using System.Collections;
using System.Collections.Generic;

public class ScoreManager {

	public IDictionary<int, int> score { get; private set; }
	private int matchPoint;
	public int winnerPlayerId = 0;

	public bool isGameSet {
		get {
			foreach(KeyValuePair<int, int> kvp in this.score ) {
				if( kvp.Value >= this.matchPoint ) {
					this.winnerPlayerId = kvp.Key;
					return true;
				}
			}
			return false;
		}
	}

	public ScoreManager(int matchPoint) {
		this.matchPoint = matchPoint;
		this.Init();
	}

	private void Init() {
		this.winnerPlayerId = 0;
		this.score = new Dictionary<int, int> {
			{1, 0},
			{2, 0},
		};
	}

	public void AddScore( int playerId, int addScore=1 ) {
		this.score[playerId] += addScore;
	}


}
