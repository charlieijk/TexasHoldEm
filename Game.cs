using Godot;
using System;

public partial class Game : Node
{
	private Deck deck;
	
	public override void _Ready()
	{
		GD.Print("=== Texas Hold'em - Testing Deck ===");
		
		// Create and shuffle the deck
		deck = new Deck();
		AddChild(deck);
		deck.InitializeDeck();
		deck.Shuffle();
		
		// Deal 5 cards like a poker hand
		DealPokerHand();
	}
	
	private void DealPokerHand()
	{
		float cardSpacing = 150;
		float startX = 200;
		float y = 300;
		
		GD.Print($"Cards in deck: {deck.CardsRemaining()}");
		
		// Deal 5 cards
		for (int i = 0; i < 5; i++)
		{
			Card card = deck.DealCard();
			
			if (card != null)
			{
				AddChild(card);
				card.Position = new Vector2(startX + (i * cardSpacing), y);
				card.Scale = new Vector2(0.5f, 0.5f);
				card.FlipUp();
			}
		}
		
		GD.Print($"Cards remaining in deck: {deck.CardsRemaining()}");
	}
}