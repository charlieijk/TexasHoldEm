using Godot;
using System;

public partial class Card : Sprite2D
{
	// Card properties
	public enum Suit { Hearts, Diamonds, Clubs, Spades }
	public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
	
	public Suit CardSuit { get; private set; }
	public Rank CardRank { get; private set; }
	public bool IsFaceUp { get; private set; }
	
	private Texture2D frontTexture;
	private Texture2D backTexture;
	
	public override void _Ready()
	{
		// Try to load card back - if it doesn't exist, that's okay
		string backPath = "res://Playing Cards/PNG-cards-1.3/card_back.png";
		if (ResourceLoader.Exists(backPath))
		{
			backTexture = GD.Load<Texture2D>(backPath);
		}
		else
		{
			GD.Print("Card back not found - cards will show face up");
		}
	}
	
	// Initialize the card
	public void Initialize(Rank rank, Suit suit)
	{
		CardRank = rank;
		CardSuit = suit;
		IsFaceUp = false;
		
		// Build the correct path for this card
		string suitName = suit.ToString().ToLower(); // hearts, diamonds, clubs, spades
		string rankName = GetRankString(rank); // 2, 3, ... jack, queen, king, ace
		string texturePath = $"res://Playing Cards/PNG-cards-1.3/{rankName}_of_{suitName}.png";
		
		GD.Print($"Loading: {texturePath}");
		
		// Load the texture
		if (ResourceLoader.Exists(texturePath))
		{
			frontTexture = GD.Load<Texture2D>(texturePath);
			GD.Print($"Successfully loaded: {texturePath}");
		}
		else
		{
			GD.PrintErr($"FAILED to load: {texturePath}");
		}
		
		// Show card back initially (or front if no back exists)
		if (backTexture != null && !IsFaceUp)
			Texture = backTexture;
		else
			Texture = frontTexture;
	}
	
	// Convert rank enum to the correct file name
	private string GetRankString(Rank rank)
	{
		return rank switch
		{
			Rank.Ace => "ace",
			Rank.Two => "2",
			Rank.Three => "3",
			Rank.Four => "4",
			Rank.Five => "5",
			Rank.Six => "6",
			Rank.Seven => "7",
			Rank.Eight => "8",
			Rank.Nine => "9",
			Rank.Ten => "10",
			Rank.Jack => "jack",
			Rank.Queen => "queen",
			Rank.King => "king",
			_ => "unknown"
		};
	}
	
	// Flip the card face up
	public void FlipUp()
	{
		IsFaceUp = true;
		Texture = frontTexture;
	}
	
	// Flip the card face down
	public void FlipDown()
	{
		IsFaceUp = false;
		if (backTexture != null)
			Texture = backTexture;
	}
	
	// Toggle between face up and down
	public void Flip()
	{
		if (IsFaceUp)
			FlipDown();
		else
			FlipUp();
	}
}
