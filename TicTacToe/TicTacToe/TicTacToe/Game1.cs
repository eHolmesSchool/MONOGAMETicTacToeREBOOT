using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MONOGAMETicTacToeREREBOOT
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D OhTexture;
        private Texture2D ExTexture;
        private Texture2D BoardTexture;
        private Rectangle OhRectangle;
        private Rectangle ExRectangle;
        private Rectangle BoardRectangle;
        bool DownLastFrame;

        float MouseX;
        float MouseY;
        float DrawX;
        float DrawY;
        char Player = 'a';
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
        MouseState currentMouseState = MouseState.Released;
        public enum Turn
        {
            OTurn,
            XTurn
        }
        Turn currentTurn = Turn.OTurn;

        public enum RectangleState
        {
            BlankState,
            XState,
            OState
        }

        Rectangle[,] GameBoardArray = new Rectangle[3, 3];

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

            ExRectangle = ExTexture.Bounds;
            OhRectangle = OhTexture.Bounds;
            BoardRectangle = BoardTexture.Bounds;
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
                case MouseState.Released:
                    break;
                case MouseState.Pressed:
                    break;
                case MouseState.WasReleasedThisFrame:
                    break;
                case MouseState.WasPressedThisFrame:
                    break;
                default:
                    break;
            }

            switch (currentGameState)
            {
                case GameState.Initialize:

                    currentMouseState = MouseState.Released;
                    for (int currentRow = 0; currentRow < 3; currentRow++)
                    {
                        for (int currentCol = 0; currentCol < 3; currentCol++)
                        {
                            GameBoardArray[currentRow, currentCol] =
                                new Rectangle(new
                                ((currentCol * 50) + (currentCol * 10),
                                (currentRow * 50) + (currentRow * 10)),
                                new(50, 50));
                        }
                    }

                    break;
                case GameState.SwapTurn:
                    break;
                case GameState.ExecuteTurn:
                    break;
                case GameState.EvaluateBoard:
                    break;
                case GameState.GameEnd:
                    break;
                default:
                    break;
            }


            if (Mouse.GetState().LeftButton == ButtonState.Pressed && DownLastFrame == false)
            {
                Debug.WriteLine(MouseX);
                Debug.WriteLine(MouseY);
                DrawX = MouseX - 25;
                DrawY = MouseY - 25;

                if (currentTurn == Turn.OTurn)
                {
                    currentTurn = Turn.XTurn;
                }
                else
                {
                    currentTurn = Turn.OTurn;
                }

                DownLastFrame = true;
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                DownLastFrame = false;
            }

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            switch (currentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.SwapTurn:
                    if (currentTurn == Turn.OTurn)
                    {
                        currentTurn = Turn.XTurn;
                    }
                    else
                    {
                        currentTurn = Turn.OTurn;
                    }
                    break;
                case GameState.ExecuteTurn:

                    break;
                case GameState.EvaluateBoard:
                    break;
                case GameState.GameEnd:
                    break;
                default:
                    break;
            }
            _spriteBatch.Begin();
            _spriteBatch.Draw(BoardTexture, new Vector2(0, 0), Color.White);

            foreach (Rectangle rectangle in GameBoardArray)
            {
                _spriteBatch.Draw(ExTexture, rectangle, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

