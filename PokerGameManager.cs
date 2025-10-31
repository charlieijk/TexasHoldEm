using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PokerGameManager : Node
{
	public enum GameState
	{
		WaitingToStart,
		DealingCards,
		PreFlop,
		Flop,
		Turn,
		River,
		Showdown,
		RoundEnd
	}
	
	public GameState CurrentState { get; private set; }
	
	private List<Player> players = new List<Player>();
	private int currentPlayerIndex = 0;
	private int dealerPosition = 0;
	
	private Deck deck;
	private List<Card> communityCards = new List<Card>();
	
	private int currentBet = 0;
	private int potSize = 0;
	private int smallBlindAmount = 10;
	private int bigBlindAmount = 20;
	
	private UIManager uiManager;
	
	private Vector2 flopPosition1 = new Vector2(660, 500);
	private Vector2 flopPosition2 = new Vector2(860, 500);
	private Vector2 flopPosition3 = new Vector2(1060, 500);
	private Vector2 turnPosition = new Vector2(1260, 500);
	private Vector2 riverPosition = new Vector2(1460, 500);
	
	// Track if all players have acted this round
	private int playersActedThisRound = 0;
	
	public override void _Ready()
	{
		CurrentState = GameState.WaitingToStart;
		GD.Print("PokerGameManager ready");
	}
	
	public void SetUIManager(UIManager ui)
	{
		uiManager = ui;
		GD.Print("UI Manager connected");
	}
	
	// ========== GAME SETUP ==========
	
	public void AddPlayer(Player player)
	{
		players.Add(player);
		GD.Print($"Added player: {player.PlayerName}");
	}
	
	public void SetupDeck()
	{
		deck = new Deck();
		AddChild(deck);
		deck.InitializeDeck();
		deck.Shuffle();
		GD.Print("Deck created and shuffled");
	}
	
	public void StartGame()
	{
		GD.Print("=== Starting Game ===");
		dealerPosition = 0;
		StartNewRound();
	}
	
	// ========== ROUND MANAGEMENT ==========
	
	public void StartNewRound()
	{
		GD.Print("=== Starting New Round ===");
		
		// Clear previous round
		foreach (Player player in players)
		{
			player.ClearHand();
			player.ResetForNewRound();
		}
		
		// Clear community cards
		foreach (Card card in communityCards)
		{
			card.QueueFree();
		}
		communityCards.Clear();
		
		// Reset deck
		deck.Reset();
		deck.Shuffle();
		
		// Reset betting
		currentBet = 0;
		potSize = 0;
		playersActedThisRound = 0;
		
		// Post blinds
		PostBlinds();
		
		// Deal cards
		CurrentState = GameState.DealingCards;
		DealHoleCards();
		
		// Update UI
		UpdateUI();
	}
	
	private void DealHoleCards()
	{
		GD.Print("Dealing hole cards...");
		
		// Deal 2 cards to each player
		for (int cardNum = 0; cardNum < 2; cardNum++)
		{
			foreach (Player player in players)
			{
				Card card = deck.DealCard();
				player.ReceiveCard(card);
			}
		}
		
		// Show human player's cards, hide AI cards
		foreach (Player player in players)
		{
			if (player.IsAI)
			{
				player.HideCards();
			}
			else
			{
				player.ShowCards();
			}
		}
		
		CurrentState = GameState.PreFlop;
		GD.Print("Cards dealt. State: PreFlop");
		
		// Start betting round
		StartBettingRound();
	}
	
	private void DealFlop()
	{
		GD.Print("Dealing Flop...");
		
		deck.DealCard(); // Burn
		
		Card flop1 = deck.DealCard();
		Card flop2 = deck.DealCard();
		Card flop3 = deck.DealCard();
		
		AddChild(flop1);
		AddChild(flop2);
		AddChild(flop3);
		
		flop1.Position = flopPosition1;
		flop2.Position = flopPosition2;
		flop3.Position = flopPosition3;
		
		flop1.Scale = new Vector2(0.4f, 0.4f);
		flop2.Scale = new Vector2(0.4f, 0.4f);
		flop3.Scale = new Vector2(0.4f, 0.4f);
		
		flop1.FlipUp();
		flop2.FlipUp();
		flop3.FlipUp();
		
		communityCards.Add(flop1);
		communityCards.Add(flop2);
		communityCards.Add(flop3);
		
		CurrentState = GameState.Flop;
		GD.Print("Flop dealt");
		
		playersActedThisRound = 0;
		currentBet = 0;
		StartBettingRound();
	}
	
	private void DealTurn()
	{
		GD.Print("Dealing Turn...");
		
		deck.DealCard(); // Burn
		
		Card turn = deck.DealCard();
		AddChild(turn);
		turn.Position = turnPosition;
		turn.Scale = new Vector2(0.4f, 0.4f);
		turn.FlipUp();
		communityCards.Add(turn);
		
		CurrentState = GameState.Turn;
		GD.Print("Turn dealt");
		
		playersActedThisRound = 0;
		currentBet = 0;
		StartBettingRound();
	}
	
	private void DealRiver()
	{
		GD.Print("Dealing River...");
		
		deck.DealCard(); // Burn
		
		Card river = deck.DealCard();
		AddChild(river);
		river.Position = riverPosition;
		river.Scale = new Vector2(0.4f, 0.4f);
		river.FlipUp();
		communityCards.Add(river);
		
		CurrentState = GameState.River;
		GD.Print("River dealt");
		
		playersActedThisRound = 0;
		currentBet = 0;
		StartBettingRound();
	}
	
	// ========== BETTING LOGIC ==========
	
	private void PostBlinds()
	{
		GD.Print("Posting blinds...");
		
		if (players.Count < 2) return;
		
		int smallBlindIndex = (dealerPosition + 1) % players.Count;
		players[smallBlindIndex].Call(smallBlindAmount);
		potSize += smallBlindAmount;
		
		int bigBlindIndex = (dealerPosition + 2) % players.Count;
		players[bigBlindIndex].Call(bigBlindAmount);
		potSize += bigBlindAmount;
		
		currentBet = bigBlindAmount;
		
		GD.Print($"Blinds posted. Pot: {potSize}");
	}
	
	private void StartBettingRound()
	{
		GD.Print("Starting betting round");
		playersActedThisRound = 0;
		currentPlayerIndex = 0; // Start with first player
		
		UpdateUI();
		
		// If current player is AI, make them act
		if (players[currentPlayerIndex].IsAI)
		{
			ProcessAITurn();
		}
		else
		{
			// Enable buttons for human player
			EnablePlayerButtons();
		}
	}
	
	public void ProcessPlayerAction(string action, int raiseAmount = 0)
	{
		Player currentPlayer = players[currentPlayerIndex];
		
		GD.Print($"{currentPlayer.PlayerName} action: {action}");
		
		switch (action)
		{
			case "fold":
				currentPlayer.Fold();
				uiManager?.ShowMessage($"{currentPlayer.PlayerName} folded");
				break;
				
			case "check":
				uiManager?.ShowMessage($"{currentPlayer.PlayerName} checked");
				break;
				
			case "call":
				int callAmount = currentBet - currentPlayer.CurrentBet;
				if (currentPlayer.Call(callAmount))
				{
					potSize += callAmount;
					uiManager?.ShowMessage($"{currentPlayer.PlayerName} called ${callAmount}");
				}
				break;
				
			case "raise":
				if (currentPlayer.Raise(raiseAmount))
				{
					potSize += raiseAmount;
					currentBet = currentPlayer.CurrentBet;
					playersActedThisRound = 0; // Reset, everyone needs to act again
					uiManager?.ShowMessage($"{currentPlayer.PlayerName} raised ${raiseAmount}");
				}
				break;
		}
		
		playersActedThisRound++;
		
		UpdateUI();
		
		// Check if betting round is complete
		if (IsBettingRoundComplete())
		{
			AdvanceGameState();
		}
		else
		{
			NextPlayer();
		}
	}
	
	private bool IsBettingRoundComplete()
	{
		// All active players have acted and matched the bet
		var activePlayers = GetActivePlayers();
		
		if (activePlayers.Count == 1)
		{
			// Only one player left, they win
			return true;
		}
		
		// Check if all players have matched the current bet
		bool allMatched = activePlayers.All(p => p.CurrentBet == currentBet);
		bool allActed = playersActedThisRound >= activePlayers.Count;
		
		return allMatched && allActed;
	}
	
	private void NextPlayer()
	{
		// Move to next active player
		do
		{
			currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
		}
		while (players[currentPlayerIndex].HasFolded);
		
		UpdateUI();
		
		// If AI, process their turn
		if (players[currentPlayerIndex].IsAI)
		{
			// Delay AI action slightly
			GetTree().CreateTimer(1.0).Timeout += () => ProcessAITurn();
		}
		else
		{
			EnablePlayerButtons();
		}
	}
	
	private void ProcessAITurn()
	{
		Player aiPlayer = players[currentPlayerIndex];
		
		// Simple AI: Random decisions
		Random random = new Random();
		int decision = random.Next(0, 3);
		
		if (decision == 0 && currentBet == aiPlayer.CurrentBet)
		{
			// Check if possible
			ProcessPlayerAction("check");
		}
		else if (decision == 1)
		{
			// Call
			ProcessPlayerAction("call");
		}
		else if (decision == 2 && random.Next(0, 2) == 0)
		{
			// Raise sometimes
			ProcessPlayerAction("raise", 20);
		}
		else
		{
			// Default to call
			ProcessPlayerAction("call");
		}
	}
	
	private void AdvanceGameState()
	{
		GD.Print($"Advancing from state: {CurrentState}");
		
		switch (CurrentState)
		{
			case GameState.PreFlop:
				DealFlop();
				break;
			case GameState.Flop:
				DealTurn();
				break;
			case GameState.Turn:
				DealRiver();
				break;
			case GameState.River:
				Showdown();
				break;
			case GameState.Showdown:
				// Game over for now
				uiManager?.ShowMessage("Round Complete! (Winner detection not implemented yet)");
				break;
		}
	}
	
	private void Showdown()
	{
		GD.Print("=== SHOWDOWN ===");
		CurrentState = GameState.Showdown;
		
		// Show all cards
		foreach (Player player in GetActivePlayers())
		{
			player.ShowCards();
		}
		
		uiManager?.ShowMessage("Showdown! (Hand evaluation coming soon)");
		UpdateUI();
	}
	
	// ========== UI METHODS ==========
	
	private void UpdateUI()
	{
		if (uiManager == null || players.Count < 2) return;
		
		uiManager.UpdateUI(
			potSize,
			currentBet,
			players[0].ChipCount,
			players[1].ChipCount,
			CurrentState.ToString()
		);
	}
	
	private void EnablePlayerButtons()
	{
		if (uiManager == null) return;
		
		Player currentPlayer = players[currentPlayerIndex];
		uiManager.EnablePlayerButtons(currentBet, currentPlayer.CurrentBet);
		uiManager.ShowMessage("Your turn!");
	}
	
	// ========== UTILITY METHODS ==========
	
	private List<Player> GetActivePlayers()
	{
		return players.Where(p => !p.HasFolded).ToList();
	}
	
	public Player GetCurrentPlayer()
	{
		if (players.Count == 0) return null;
		return players[currentPlayerIndex];
	}
	
	// ========== UI CALLBACKS ==========
	
	public void OnFoldPressed()
	{
		ProcessPlayerAction("fold");
	}
	
	public void OnCheckPressed()
	{
		ProcessPlayerAction("check");
	}
	
	public void OnCallPressed()
	{
		ProcessPlayerAction("call");
	}
	
	public void OnRaisePressed(int amount)
	{
		ProcessPlayerAction("raise", amount);
	}
}
