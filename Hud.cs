using Godot;
using System;

public partial class Hud : CanvasLayer
{
    
    [Signal]
    public delegate void StartGameEventHandler();

    private void OnStartButtonPressed()
    {
        GetNode<Button>("StartButton").Hide();
        EmitSignal(SignalName.StartGame);
    }

    public void ShowStartButton()
    {
        GetNode<Button>("StartButton").Show();
    }
}
