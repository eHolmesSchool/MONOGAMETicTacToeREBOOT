using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using TicTacToe;

namespace MONOGAMETicTacToeREREBOOT
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D OhTexture;
        private Texture2D ExTexture;
        private Texture2D BoardTexture;
        //private Rectangle OhRectangle;
        //private Rectangle ExRectangle;
        //private Rectangle BoardRectangle;

        Tile lastTileChanged = new Tile(new Rectangle());

        int MouseX;
        int MouseY;

        int turnsTaken; //Nothing can't be solved without Yet Another Variable :)

        int restartCountdownFrames = 300;

        public enum Victor
        {
            Draw,
            X,
            O
        }
        Victor currentVictor = Victor.Draw;
        public enum WinDirection
        {
            Horizontal,
            Vertical,
            Diagonal1, //Top left to bottom right
            Diagonal2 // Bottom Left to Top right
        }
        WinDirection currentWinDirection = WinDirection.Horizontal; //used for determining which tiles should be concidered the winning ones
        public enum GameState
        {
            Initialize,
            SwapTurn,
            ExecuteTurn,
            EvaluateBoard,
            GameEnd
        }
        GameState currentGameState = GameState.Initialize;

        public enum MouseState
        {
            Released,
            Pressed,
            WasReleasedThisFrame,
            WasPressedThisFrame,
        }
        MouseState currentMouseState;

        public enum Turn
        {
            OTurn,
            XTurn
        }
        Turn currentTurn = Turn.OTurn;

        Tile[,] GameBoardArray = new Tile[3, 3];


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _graphics.PreferredBackBufferHeight = BoardTexture.Height;
            _graphics.PreferredBackBufferWidth = BoardTexture.Width;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            OhTexture = Content.Load<Texture2D>("O");
            ExTexture = Content.Load<Texture2D>("X");
            BoardTexture = Content.Load<Texture2D>("TicTacToeBoard");
        }


        protected override void Update(GameTime gameTime)
        {

            switch (currentMouseState)
            {
                case MouseState.Pressed:
                    if (Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        currentMouseState = MouseState.WasReleasedThisFrame;
                    }
                    break;

                case MouseState.Released:

                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        currentMouseState = MouseState.WasPressedThisFrame;
                    }
                    break;

                default:
                    break;
            }
            switch (currentMouseState) // MouseState2 Electric Boogaloo              BOTH ARE NECCESSARY TRUST ME BRO
            {
                case MouseState.WasReleasedThisFrame:
                    break;

                case MouseState.WasPressedThisFrame:
                    MouseX = Mouse.GetState().X;
                    MouseY = Mouse.GetState().Y;
                    break;

                default:
                    break;
            }

            switch (currentGameState)
            {
                case GameState.Initialize:
                    currentVictor = Victor.Draw;
                    turnsTaken = 0;
                    currentMouseState = MouseState.Released;
                    for (int currentRow = 0; currentRow < 3; currentRow++)
                    {
                        for (int currentCol = 0; currentCol < 3; currentCol++)
                        {
                            GameBoardArray[currentRow, currentCol] =
                                new Tile (new Rectangle(new
                                ((currentCol * 50) + (currentCol * 10),
                                (currentRow * 50) + (currentRow * 10)),
                                new(50, 50)), Tile.TileState.Blank );

                            GameBoardArray[currentRow, currentCol].TilePosX = currentRow;
                            GameBoardArray[currentRow, currentCol].TilePosY = currentCol;

                            GameBoardArray[currentRow, currentCol].Victorious = false ;
                        }
                    }

                    foreach (Tile tile in GameBoardArray)
                    {
                        tile.Reset();
                    }

                    currentGameState = GameState.SwapTurn;
                    break;

                case GameState.SwapTurn:
                    if (currentTurn == Turn.OTurn) {currentTurn = Turn.XTurn;}
                    else if (currentTurn == Turn.XTurn) {currentTurn = Turn.OTurn;}
                    else { Debug.Write("SWAPTURN ERROR");}

                    currentGameState = GameState.ExecuteTurn;

                    break;

                case GameState.ExecuteTurn:

                    if (currentMouseState == MouseState.WasPressedThisFrame)
                    {
                        if(currentTurn == Turn.OTurn)
                        {
                            foreach (Tile currentTile in GameBoardArray )
                            {
                                if (currentTile.TrySet(new Point(MouseX, MouseY)) == true)
                                {
                                    lastTileChanged = currentTile;
                                    turnsTaken++;
                                    currentTile.SetState(Tile.TileState.O);
                                    currentGameState = GameState.EvaluateBoard;
                                }
                            }
                        }
                        else if (currentTurn == Turn.XTurn)
                        {
                            foreach (Tile currentTile in GameBoardArray)
                            {
                                if (currentTile.TrySet(new Point(MouseX, MouseY)) == true)
                                {
                                    lastTileChanged = currentTile;
                                    turnsTaken++;
                                    currentTile.SetState(Tile.TileState.X);
                                    currentGameState = GameState.EvaluateBoard;
                                }
                            }
                        }
                    }

                    break;

                case GameState.EvaluateBoard: /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //lastTileChanged
                    Tile.TileState currentState;

                    int matches=0;
                    bool winner = false; //Draw means no winner
                    
                    if (currentTurn == Turn.OTurn)
                    {
                        currentState = Tile.TileState.O;
                    }
                    else
                    {
                        currentState = Tile.TileState.X;
                    }

                    for (int x = 0; x < 3; x++)   //Up Down Check
                    {
                        if (GameBoardArray[x, lastTileChanged.TilePosY]._tileState == currentState)
                        { //If the tiles in the same row as the last clicked tile match the symbol of the turn taker, increase matches by 1
                            matches++;

                            Debug.WriteLine(matches);
                        }
                    }
                    if (matches == 3)
                    {
                        winner = true;
                        currentWinDirection = WinDirection.Horizontal;
                        currentGameState = GameState.GameEnd;
                    }
                    matches = 0; //reset match numb between checking the different axis
                    for (int y = 0; y < 3; y++)   //Side to Side check
                    {
                        if (GameBoardArray[lastTileChanged.TilePosX, y]._tileState == currentState)
                        {
                            matches++;
                        }
                    }
                    if (matches == 3)
                    {
                        winner = true;
                        currentWinDirection = WinDirection.Vertical;
                        currentGameState = GameState.GameEnd;
                    }
                    matches = 0;
                    for (int y = 0; y < 3; y++)  //Diagonal1 Check
                    {
                        if (GameBoardArray[y, y]._tileState == currentState)
                        {
                            matches++;
                        }
                    }
                    if (matches == 3)
                    {
                        winner = true;
                        currentWinDirection = WinDirection.Diagonal1;
                        currentGameState = GameState.GameEnd;
                    }
                    matches = 0;
                    for (int y = 2; y >-1; y--)  //Diagonal2 Check
                    {
                        int x = 2-y;
                        if (GameBoardArray[x, y]._tileState == currentState)
                        {
                            matches++;
                        }
                    }
                    if (matches == 3)
                    {winner = true;
                        currentWinDirection = WinDirection.Diagonal2;
                        currentGameState = GameState.GameEnd;
                    }


                    if (winner)
                    {
                        if (currentTurn == Turn.OTurn)
                        {
                            currentVictor = Victor.O;
                        }
                        else if (currentTurn == Turn.XTurn)
                        {
                            currentVictor = Victor.X;
                        }
                    }
                    else if (turnsTaken == 9)
                    {
                        currentVictor = Victor.Draw;
                        currentGameState = GameState.GameEnd;
                    }
                    

                    if (currentGameState != GameState.GameEnd)
                    {
                        currentGameState = GameState.SwapTurn;
                    }

                    break;


                case GameState.GameEnd:

                    if (currentVictor == Victor.Draw)
                    {
                        Debug.Write("DRAW");
                        foreach (Tile tile in GameBoardArray)
                        {
                            tile.Victorious = true; //All tiles are Victorious on a draw
                        }
                    }
                    else
                    {

                        if (currentTurn == Turn.OTurn)
                        {
                            currentVictor = Victor.O;
                        } else if (currentTurn == Turn.XTurn)
                        {
                            currentVictor = Victor.X;
                        }

                        if (currentWinDirection == WinDirection.Horizontal)
                        {
                            for (int x = 0; x < 3; x++)
                            {
                                GameBoardArray[x, lastTileChanged.TilePosY].Victorious = true;
                            }
                        }
                        else if (currentWinDirection == WinDirection.Vertical)
                        {
                            for (int y = 0; y < 3; y++)
                            {
                                GameBoardArray[lastTileChanged.TilePosX, y].Victorious = true;
                            }
                        }
                        else if (currentWinDirection == WinDirection.Diagonal1)
                        {
                            for (int y = 0; y < 3; y++)  //Diagonal1 Check
                            {
                                GameBoardArray[y, y].Victorious = true;
                            }
                        }
                        else if (currentWinDirection == WinDirection.Diagonal2)
                        {
                            for (int y = 2; y > -1; y--)  //Diagonal2 Check
                            {
                                int x = 2 - y;
                                GameBoardArray[x, y].Victorious = true;
                            }
                        }
                    }
                    break;

                default:
                    break;
            }


            //Set currentMouseState to either Released or Pressed
            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                currentMouseState = MouseState.Released;
            }else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                currentMouseState = MouseState.Pressed;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            if (currentGameState != GameState.GameEnd)
            {
                DrawGame();
            }
            else if (currentGameState == GameState.GameEnd)
            {
                restartCountdownFrames--;
                if (restartCountdownFrames > 150)
                {
                    foreach (Tile tile in GameBoardArray)
                    {
                        if (tile.Victorious)
                        {
                            if (tile._tileState != Tile.TileState.Blank)
                            {
                                tile._tileState = Tile.TileState.Blank;
                            }
                            else if (currentTurn == Turn.OTurn)
                            {
                                tile._tileState = Tile.TileState.O;
                            }
                            else if (currentTurn == Turn.XTurn)
                            {
                                tile._tileState = Tile.TileState.X;
                            }
                            else
                            {
                                Debug.WriteLine("END OF LEVEL FAILIURE. NO ONE WON BUT WE ARE ALL LOSING     DRAW STEP");
                            }
                        }
                        DrawGame();
                    }
                }
                else if (restartCountdownFrames > 0)
                {
                    if (currentVictor == Victor.O)
                    {
                        _spriteBatch.Draw(OhTexture, new Rectangle(new Point(0, 0), new Point(170, 170)), Color.White); //O WINS
                    }
                    else if (currentVictor == Victor.X)
                    {
                        _spriteBatch.Draw(ExTexture, new Rectangle(new Point(0, 0), new Point(170, 170)), Color.White); //X WINS
                    }
                }
                else if (restartCountdownFrames == 0)
                {
                    restartCountdownFrames = 300;
                    currentGameState = GameState.Initialize;
                }
            }
            
             
            _spriteBatch.End();
            base.Draw(gameTime);
        }



        private void DrawGame()
        {
            _spriteBatch.Draw(BoardTexture, new Vector2(0, 0), Color.White);
            foreach (Tile tile in GameBoardArray)
            {
                if (tile._tileState != Tile.TileState.Blank)
                {
                    if (tile._tileState == Tile.TileState.X)
                    {
                        _spriteBatch.Draw(ExTexture, tile._rectangle, Color.White);
                    }
                    else if (tile._tileState == Tile.TileState.O)
                    {
                        _spriteBatch.Draw(OhTexture, tile._rectangle, Color.White);
                    }
                }
            }
        }
    }
}