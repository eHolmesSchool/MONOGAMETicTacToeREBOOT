using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MONOGAMETicTacToeREREBOOT.Game1;

namespace TicTacToe
{
    public class Tile //Knows where it is, know if the mouse is on it, know what state its in. DOES NOT need to know game state, textures etc.
    {
        public Rectangle _rectangle { get; private set;}

        public enum TileState
        {
            Blank,
            X,
            O
        }
        public TileState _tileState { get; private set; }



        public Tile(Rectangle rectangle)
        {
            _rectangle = rectangle;
            _tileState = TileState.Blank;
        }

        public Tile(Rectangle tileBounds, TileState tileState)
        {
            _rectangle = tileBounds;
            _tileState = tileState;
        }

        public void Reset()
        {
            _tileState = TileState.Blank;
        }

        public bool TrySetState(Point point, TileState state) // an X and Y co-ordinate and the state we want the Tile to be. We will do the X or O logic in main
        {
            if (_tileState == TileState.Blank && _rectangle.Contains(point))
            {
                _tileState = state;
                return true;
            }
            return false;
        }
    }
}
