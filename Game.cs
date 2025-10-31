using Godot;
using System;

public partial class Game : Node
{
	private PokerGameManager gameManager;
	private UIManager uiManager;
	
	public override void _Ready()
	{
	GD.Print("=== Texas Hold'em Poker ===");
	
	// Create poker table background
	PokerTable table = new PokerTable();
	AddChild(table);
	table.ZIndex = -10;
	
	// Create UI Manager first
	uiManager = new UIManager();
	AddChild(uiManager);
		
		// Create game manager
		gameManager = new PokerGameManager();
		AddChild(gameManager);
		
		// Connect UI to game manager
		uiManager.SetGameManager(gameManager);
		gameManager.SetUIManager(uiManager);
		
		// Create Player 1 (You - bottom of screen)
		Player player1 = new Player();
		AddChild(player1);
		player1.Position = new Vector2(960, 900);
		player1.Initialize("You", 1000, false);
		gameManager.AddPlayer(player1);

		// Create Player 2 (AI - top of screen)
		Player player2 = new Player();
		AddChild(player2);
		player2.Position = new Vector2(960, 250);
		player2.Initialize("AI Opponent", 1000, true);
		gameManager.AddPlayer(player2);
		
		// Setup and start game
		gameManager.SetupDeck();
		gameManager.StartGame();
	}
	
}
