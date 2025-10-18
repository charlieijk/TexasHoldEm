using Godot;
using System;

public partial class UIManager : CanvasLayer
{
	// UI Elements
	private Label potLabel;
	private Label currentBetLabel;
	private Label player1ChipsLabel;
	private Label player2ChipsLabel;
	private Label gameStateLabel;
	private Label actionMessageLabel;
	
	// Betting buttons
	private Button foldButton;
	private Button checkButton;
	private Button callButton;
	private Button raiseButton;
	
	// Raise slider
	private HSlider raiseSlider;
	private Label raiseAmountLabel;
	
	// Reference to game manager
	private PokerGameManager gameManager;
	
	public override void _Ready()
	{
		CreateUI();
	}
	
	public void SetGameManager(PokerGameManager manager)
	{
		gameManager = manager;
	}
	
	private void CreateUI()
{
	// Create background panel for buttons - BOTTOM RIGHT
	Panel buttonPanel = new Panel();
	AddChild(buttonPanel);
	buttonPanel.Position = new Vector2(850, 450);  // Higher and more to the right
	buttonPanel.Size = new Vector2(380, 120);
	
	// Style the panel
	var styleBox = new StyleBoxFlat();
	styleBox.BgColor = new Color(0.2f, 0.2f, 0.2f, 0.9f);  // Dark semi-transparent
	styleBox.CornerRadiusTopLeft = 10;
	styleBox.CornerRadiusTopRight = 10;
	styleBox.CornerRadiusBottomLeft = 10;
	styleBox.CornerRadiusBottomRight = 10;
	buttonPanel.AddThemeStyleboxOverride("panel", styleBox);
	
	// Fold Button
	foldButton = new Button();
	buttonPanel.AddChild(foldButton);
	foldButton.Text = "FOLD";
	foldButton.Position = new Vector2(10, 15);
	foldButton.Size = new Vector2(85, 45);
	foldButton.Pressed += OnFoldPressed;
	
	// Check Button
	checkButton = new Button();
	buttonPanel.AddChild(checkButton);
	checkButton.Text = "CHECK";
	checkButton.Position = new Vector2(105, 15);
	checkButton.Size = new Vector2(85, 45);
	checkButton.Pressed += OnCheckPressed;
	
	// Call Button
	callButton = new Button();
	buttonPanel.AddChild(callButton);
	callButton.Text = "CALL";
	callButton.Position = new Vector2(10, 65);
	callButton.Size = new Vector2(85, 45);
	callButton.Pressed += OnCallPressed;
	
	// Raise Button
	raiseButton = new Button();
	buttonPanel.AddChild(raiseButton);
	raiseButton.Text = "RAISE";
	raiseButton.Position = new Vector2(105, 65);
	raiseButton.Size = new Vector2(85, 45);
	raiseButton.Pressed += OnRaisePressed;
	
	// Rest of your labels stay the same...
	
	// Pot Label (move to LEFT of AI cards)
	potLabel = new Label();
	AddChild(potLabel);
	potLabel.Position = new Vector2(250, 20);  // CHANGED: moved left
	potLabel.AddThemeColorOverride("font_color", Colors.Yellow);
	potLabel.AddThemeFontSizeOverride("font_size", 28);

	// Current Bet Label (below pot label)
	currentBetLabel = new Label();
	AddChild(currentBetLabel);
	currentBetLabel.Position = new Vector2(250, 55);  // CHANGED: moved left
	currentBetLabel.AddThemeColorOverride("font_color", Colors.White);
	currentBetLabel.AddThemeFontSizeOverride("font_size", 20);
	
	// Player 1 Chips (bottom left)
	player1ChipsLabel = new Label();
	AddChild(player1ChipsLabel);
	player1ChipsLabel.Position = new Vector2(50, 600);
	player1ChipsLabel.AddThemeColorOverride("font_color", Colors.LightGreen);
	player1ChipsLabel.AddThemeFontSizeOverride("font_size", 22);
	
	// Player 2 Chips (top left)
	player2ChipsLabel = new Label();
	AddChild(player2ChipsLabel);
	player2ChipsLabel.Position = new Vector2(50, 50);
	player2ChipsLabel.AddThemeColorOverride("font_color", Colors.LightGreen);
	player2ChipsLabel.AddThemeFontSizeOverride("font_size", 22);
	
	// Game State Label
	gameStateLabel = new Label();
	AddChild(gameStateLabel);
	gameStateLabel.Position = new Vector2(50, 350);
	gameStateLabel.AddThemeColorOverride("font_color", Colors.Cyan);
	gameStateLabel.AddThemeFontSizeOverride("font_size", 18);
	
	// Action Message Label (center)
	actionMessageLabel = new Label();
	AddChild(actionMessageLabel);
	actionMessageLabel.Position = new Vector2(450, 450);
	actionMessageLabel.AddThemeColorOverride("font_color", Colors.White);
	actionMessageLabel.AddThemeFontSizeOverride("font_size", 24);
	
	// Initially disable all buttons
	DisableAllButtons();
}
	
	// Update UI displays
	public void UpdateUI(int potSize, int currentBet, int player1Chips, int player2Chips, string gameState)
	{
		potLabel.Text = $"POT: ${potSize}";
		currentBetLabel.Text = $"Current Bet: ${currentBet}";
		player1ChipsLabel.Text = $"Your Chips: ${player1Chips}";
		player2ChipsLabel.Text = $"AI Chips: ${player2Chips}";
		gameStateLabel.Text = $"State: {gameState}";
	}
	
	public void ShowMessage(string message)
	{
		actionMessageLabel.Text = message;
	}
	
	public void EnablePlayerButtons(int currentBet, int playerCurrentBet)
	{
		foldButton.Disabled = false;
		
		// Can only check if no bet to call
		if (currentBet == playerCurrentBet)
		{
			checkButton.Disabled = false;
			callButton.Disabled = true;
		}
		else
		{
			checkButton.Disabled = true;
			callButton.Disabled = false;
			callButton.Text = $"CALL ${currentBet - playerCurrentBet}";
		}
		
		raiseButton.Disabled = false;
	}
	
	public void DisableAllButtons()
	{
		foldButton.Disabled = true;
		checkButton.Disabled = true;
		callButton.Disabled = true;
		raiseButton.Disabled = true;
	}
	
	// Button callbacks
	private void OnFoldPressed()
	{
		GD.Print("Fold button pressed");
		DisableAllButtons();
		gameManager?.OnFoldPressed();
	}
	
	private void OnCheckPressed()
	{
		GD.Print("Check button pressed");
		DisableAllButtons();
		gameManager?.OnCheckPressed();
	}
	
	private void OnCallPressed()
	{
		GD.Print("Call button pressed");
		DisableAllButtons();
		gameManager?.OnCallPressed();
	}
	
	private void OnRaisePressed()
	{
		GD.Print("Raise button pressed");
		DisableAllButtons();
		// For now, raise by 20 (we'll add slider later)
		gameManager?.OnRaisePressed(20);
	}
}
