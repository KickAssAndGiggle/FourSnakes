namespace _4Snakes
{
    public partial class frmMain : Form
    {

        // Enum for the snake colors
        private enum SnakeColor
        {
            Pink = 0,
            Green = 1,
            Yellow = 2,
            Turquoise = 3
        }

        // Enum for directions a snake can be going
        private enum Direction
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        // Structure for defining a snake
        private struct Snake
        {
            public SnakeColor Color;
            public bool Dead;
            public int Index;
            public SnakePiece[] Pieces;
        }

        // Structure for defining one single segment of a snake
        private struct SnakePiece
        {
            public int X;
            public int Y;
            public bool Locked;  // This is true when a new piece is added to the snake, it won't move on first tick
            public PictureBox Pictue;
        }

        private struct Food
        {
            public int X;
            public int Y;
            public PictureBox Picture;
        }

        // An array of snakes, initialised in StartGame
        private Snake[] _snakes;

        // An arracy of snake directions, initialised in StartGame
        private Direction[] _snakeDirections;

        // What direction were we going in before out last turn (makes selecting a new
        // head rotation more efficient)
        private Direction[] _previousSnakeDirections;

        // A number (between 0 and 3) that will determins which snake is the player's
        private int _playerSnake;

        // Has the player pressed a movement key
        private bool _movementRequested;

        // What direction has the player asked to go this frame
        private Direction _requestedDirectionChange;

        // Boolean to tell the GameLoop to exit
        private bool _loopEnd;

        // Scores
        private int _pinkScore;
        private int _yellowScore;
        private int _greenScore;
        private int _turquoiseScore;

        // Food positions
        private int _mouse1X;
        private int _mouse1Y;
        private int _mouse2X;
        private int _mouse2Y;
        private int _mouse3X;
        private int _mouse3Y;

        // How much total food has been eaten, tracks game speed
        private int _foodEaten;
        // How many milliseconds per game tick, gets smaller every 5 food eaten (to cap of 80)
        private int _gameSpeed;

        // Picture boxes for holding food images
        private PictureBox _mouse1Pic;
        private PictureBox _mouse2Pic;
        private PictureBox _mouse3Pic;

        Random _rnd = new(Environment.TickCount);

        // Game start function will set our variables, so stop Visual Studio warning us about
        // the constructor not setting them
#pragma warning disable CS8618

        public frmMain()
        {
            InitializeComponent();

            // Hook up the Key_Down event
            this.KeyDown += FrmMain_KeyDown;
            lblExit.Click += LblExit_Click;

            // Start the game
            StartGame();

            // As always with Windows Forms, we can't just execute looping code instantly as
            // the form loads, as we won't get a Paint, so we have to fire it off with a timer
            startTimer.Tick += StartTimer_Tick;
            startTimer.Start();

        }

#pragma warning restore CS8618

        // Timer so we can start the game without locking up the UI thread
        private void StartTimer_Tick(object? sender, EventArgs e)
        {
            startTimer.Stop();
            SnakeSelect();
        }

        private void LblExit_Click(object? sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void SnakeSelect()
        {

            frmSnakeSelect selector = new();
            selector.ShowDialog();
            if (selector.Selected == -1)
            {                
                Environment.Exit(0);
            }
            _playerSnake = selector.Selected;
            
            selector.Close();
            selector = null;

            GameLoop();

        }

        // Function that starts a new game
        public void StartGame()
        {

            // Initiate the snakes and their direction arrays
            _snakes = new Snake[4];
            _snakeDirections = new Direction[4];
            _previousSnakeDirections = new Direction[4];

            _foodEaten = 0;
            _gameSpeed = 200;

            // Loop through the 4 snakes, index 0 - 3
            for (int nn = 0; nn <= 3; nn++)
            {

                // Set the snakes initial directions
                _snakeDirections[nn] = nn == 0 ? Direction.North : nn == 1 ? Direction.East :
                    nn == 2 ? Direction.South : Direction.West;
                _previousSnakeDirections[nn] = _snakeDirections[nn];

                // The color enum is 0 - 3 just like the snakes
                _snakes[nn].Color = (SnakeColor)nn;

                // Create an array of 5 pieces as the starting length of each snake
                _snakes[nn].Pieces = new SnakePiece[5];

                // And track the snake index (useful later when we don't want to include the current
                // snake when looping all snakes)
                _snakes[nn].Index = nn;

                // Loop all 5 pieces for the snake
                for (int piece = 0; piece <= 4; piece++)
                {
                    // Make a new SnakePiece for each
                    SnakePiece current = new()
                    {
                        // Initialise the PictureBox that will display the snake piece's image
                        Pictue = new()
                        {
                            SizeMode = PictureBoxSizeMode.Normal,
                            Width = 25,
                            Height = 25,
                            ClientSize = new Size(25, 25)
                        }
                    };

                    // Choose which of the four snakes we are working with, to decide the starting
                    // positions of each block and the color
                    switch (nn)
                    {
                        case 0:

                            current.Pictue.Image = piece == 0 ? picPinkHead.Image : picPinkBody.Image;
                            current.Y = 16 + piece;
                            current.X = 3;
                            if (piece == 0)
                            {
                                RotateHeadForSnake(current.Pictue, Direction.North, Direction.North);
                            }

                            break;

                        case 1:

                            current.Pictue.Image = piece == 0 ? picGreenHead.Image : picGreenBody.Image;
                            current.Y = 3;
                            current.X = 7 - piece;
                            if (piece == 0)
                            {
                                RotateHeadForSnake(current.Pictue, Direction.East, Direction.North);
                            }

                            break;

                        case 2:

                            current.Pictue.Image = piece == 0 ? picYellowHead.Image : picYellowBody.Image;
                            current.Y = 7 - piece;
                            current.X = 28;
                            if (piece == 0)
                            {
                                RotateHeadForSnake(current.Pictue, Direction.South, Direction.North);
                            }

                            break;

                        case 3:

                            current.Pictue.Image = piece == 0 ? picTurquoiseHead.Image : picTurquoiseBody.Image;
                            current.Y = 20;
                            current.X = 25 + piece;
                            if (piece == 0)
                            {
                                RotateHeadForSnake(current.Pictue, Direction.West, Direction.North);
                            }

                            break;

                    }

                    // Set the picture's location on the board (X * 25, Y * 25)
                    current.Pictue.Location = new Point(current.X * 25, current.Y * 25);

                    // Set the correct piece on the correct snake to the SnakePiece we are working on
                    _snakes[nn].Pieces[piece] = current;

                    // Add the PictureBox for the snake into the board panel
                    board.Controls.Add(current.Pictue);
                }

            }

            // Create the picture boxes for the foods
            _mouse1Pic = new()
            {
                SizeMode = PictureBoxSizeMode.Normal,
                Width = 25,
                Height = 25,
                ClientSize = new Size(25, 25),
                Image = picMouse.Image
            };

            _mouse2Pic = new()
            {
                SizeMode = PictureBoxSizeMode.Normal,
                Width = 25,
                Height = 25,
                ClientSize = new Size(25, 25),
                Image = picMouse.Image
            };

            _mouse3Pic = new()
            {
                SizeMode = PictureBoxSizeMode.Normal,
                Width = 25,
                Height = 25,
                ClientSize = new Size(25, 25),
                Image = picMouse.Image
            };

            // Add them into the board
            board.Controls.Add(_mouse1Pic);
            board.Controls.Add(_mouse2Pic);
            board.Controls.Add(_mouse3Pic);

            // Place them (telling the function where we are in the game start process)
            PlaceFood(1, true, false, false);
            PlaceFood(2, false, true, false);
            PlaceFood(3, false, false, true);


        }

        // Places a food in the board
        private void PlaceFood(int foodIndex, bool gameStartMouse1 = false, bool gameStartMouse2 = false, 
            bool gameStartMouse3 = false)
        {

            // Store all the taken squares. Food squares are taken, but at game start, we need to make sure
            // we are only considering foods already placed.

            // Squares are marked as taken using a key: X * 1000 + Y
            List<int> takenCoOrds = new();
            if (!gameStartMouse1 && !gameStartMouse2 && !gameStartMouse3)
            {
                takenCoOrds.Add(_mouse1X * 1000 + _mouse1Y);
                takenCoOrds.Add(_mouse2X * 1000 + _mouse2Y);
                takenCoOrds.Add(_mouse3X * 1000 + _mouse3Y);
            }
            else if (gameStartMouse2)
            {
                takenCoOrds.Add(_mouse1X * 1000 + _mouse1Y);
            }
            else if (gameStartMouse3)
            {
                takenCoOrds.Add(_mouse1X * 1000 + _mouse1Y);
                takenCoOrds.Add(_mouse2X * 1000 + _mouse2Y);
            }

            // And now every square of every snake, mark as taken
            for (int nn = 0; nn <= 3; nn++)
            {
                if (!_snakes[nn].Dead)
                {
                    for (int np = 0; np < _snakes[nn].Pieces.Length; np++)
                    {
                        takenCoOrds.Add(_snakes[nn].Pieces[np].X * 1000 + _snakes[nn].Pieces[np].Y);
                    }
                }
            }

            // Keep generating food positions until we find a non-taken one
            int foodX = -1;
            int foodY = -1;
            do
            {
                foodX = _rnd.Next(0, 32);
                foodY = _rnd.Next(0, 24);
            } while (takenCoOrds.Contains(foodX * 1000 + foodY));

            // Set the correct food variables for the food we are placing
            switch (foodIndex)
            {
                case 1:
                    _mouse1X = foodX;
                    _mouse1Y = foodY;
                    _mouse1Pic.Left = _mouse1X * 25;
                    _mouse1Pic.Top = _mouse1Y * 25;
                    break;
                case 2:
                    _mouse2X = foodX;
                    _mouse2Y = foodY;
                    _mouse2Pic.Left = _mouse2X * 25;
                    _mouse2Pic.Top = _mouse2Y * 25;
                    break;
                case 3:
                    _mouse3X = foodX;
                    _mouse3Y = foodY;
                    _mouse3Pic.Left = _mouse3X * 25;
                    _mouse3Pic.Top = _mouse3Y * 25;
                    break;
            }

        }

        // Main game loop
        private void GameLoop()
        {

            // Loop ends when this is set true (game is over or player wants to exit)
            _loopEnd = false;

            do
            {

                // Track the tick count at loop start
                long startTicks = Environment.TickCount64;

                // Let the AI choose an action for its snakes
                DoAI();

                // Move the snakes based on its current direction. Food collision
                // is also done in this function call
                MoveSnakes();

                // Check if a snake has died
                CheckDeath();

                // Keep a consistent speed by looping until we have passed the correct number
                // of ticks
                do
                {
                    // Give any other thread some time to process so we don't lock up
                    System.Threading.Thread.Sleep(0);
                    // Allow Windows Forms specific time in the thread to paint itself (this is 
                    // another Windows Forms oddity that could be a gotcha)
                    Application.DoEvents();
                } while (Environment.TickCount64 - _gameSpeed < startTicks);

            } while (!_loopEnd);

        }

        // Function that lets the AI do its job steering its snakes
        private void DoAI()
        {
            List<int> takenCoOrds = new();
            for (int nn = 0; nn <= 3; nn++)
            {
                if (!_snakes[nn].Dead)
                {
                    for (int np = 0; np < _snakes[nn].Pieces.Length; np++)
                    {
                        takenCoOrds.Add(_snakes[nn].Pieces[np].X * 1000 + _snakes[nn].Pieces[np].Y);
                    }
                }
            }
            for (int nn = 0; nn < _snakes.Length; nn++)
            {
                if (nn != _playerSnake)
                {
                    // Are we going to hit a wall?
                    if (_snakes[nn].Pieces[0].Y == 0 && _snakeDirections[nn] == Direction.North)
                    {
                        if (_rnd.Next(0, 100) < 50)
                        {
                            if (!takenCoOrds.Contains((_snakes[nn].Pieces[0].X + 1) * 1000 + _snakes[nn].Pieces[0].Y))
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].X < 31 ?
                                    Direction.East : Direction.West);
                                continue;
                            }
                            else
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].X > 0 ?
                                    Direction.West : Direction.East);
                                continue;
                            }
                        }
                        else
                        {
                            if (!takenCoOrds.Contains((_snakes[nn].Pieces[0].X - 1) * 1000 + _snakes[nn].Pieces[0].Y))
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].X > 0 ?
                                    Direction.West : Direction.East);
                                continue;
                            }
                            else
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].X < 31 ?
                                    Direction.East : Direction.West);
                                continue;
                            }
                        }
                    }
                    else if (_snakes[nn].Pieces[0].Y == 23 && _snakeDirections[nn] == Direction.South)
                    {
                        if (_rnd.Next(0, 100) < 50)
                        {
                            if (!takenCoOrds.Contains((_snakes[nn].Pieces[0].X + 1) * 1000 + _snakes[nn].Pieces[0].Y))
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].X < 31 ?
                                    Direction.East : Direction.West);
                                continue;
                            }
                            else
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].X > 0 ?
                                    Direction.West : Direction.East);
                                continue;
                            }
                        }
                        else
                        {
                            if (!takenCoOrds.Contains((_snakes[nn].Pieces[0].X - 1) * 1000 + _snakes[nn].Pieces[0].Y))
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].X > 0 ?
                                    Direction.West : Direction.East);
                                continue;
                            }
                            else
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].X < 31 ?
                                    Direction.East : Direction.West);
                                continue;
                            }
                        }
                    }
                    else if (_snakes[nn].Pieces[0].X == 0 && _snakeDirections[nn] == Direction.West)
                    {
                        if (_rnd.Next(0, 100) < 50)
                        {
                            if (!takenCoOrds.Contains((_snakes[nn].Pieces[0].X) * 1000 + _snakes[nn].Pieces[0].Y - 1))
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].Y > 0 ?
                                    Direction.North : Direction.South);
                                continue;
                            }
                            else
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].Y < 23 ?
                                    Direction.South : Direction.North);
                                continue;
                            }
                        }
                        else
                        {
                            if (!takenCoOrds.Contains((_snakes[nn].Pieces[0].X) * 1000 + _snakes[nn].Pieces[0].Y + 1))
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].Y < 23 ?
                                    Direction.South : Direction.North);
                                continue;
                            }
                            else
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].Y > 0 ?
                                    Direction.North : Direction.South);
                                continue;
                            }
                        }
                    }
                    else if (_snakes[nn].Pieces[0].X == 31 && _snakeDirections[nn] == Direction.East)
                    {
                        if (_rnd.Next(0, 100) < 50)
                        {
                            if (!takenCoOrds.Contains((_snakes[nn].Pieces[0].X) * 1000 + _snakes[nn].Pieces[0].Y - 1))
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].Y > 0 ?
                                    Direction.North : Direction.South);
                                continue;
                            }
                            else
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].Y < 23 ?
                                    Direction.South : Direction.North);
                                continue;
                            }
                        }
                        else
                        {
                            if (!takenCoOrds.Contains((_snakes[nn].Pieces[0].X) * 1000 + _snakes[nn].Pieces[0].Y + 1))
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].Y < 23 ?
                                    Direction.South : Direction.North);
                                continue;
                            }
                            else
                            {
                                _snakeDirections[nn] = (_snakes[nn].Pieces[0].Y > 0 ?
                                    Direction.North : Direction.South);
                                continue;
                            }
                        }
                    }
                    bool takenAbove = takenCoOrds.Contains(_snakes[nn].Pieces[0].X * 1000 +                         
                        (_snakes[nn].Pieces[0].Y - 1));
                    bool takenBelow = takenCoOrds.Contains(_snakes[nn].Pieces[0].X * 1000 +
                        (_snakes[nn].Pieces[0].Y + 1));
                    bool takenRight = takenCoOrds.Contains((_snakes[nn].Pieces[0].X + 1) * 1000 +
                        (_snakes[nn].Pieces[0].Y));
                    bool takenLeft = takenCoOrds.Contains((_snakes[nn].Pieces[0].X - 1) * 1000 +
                        (_snakes[nn].Pieces[0].Y));

                    if (takenAbove && _snakeDirections[nn] == Direction.North)
                    {
                        if (takenLeft)
                        {
                            _snakeDirections[nn] = Direction.East;
                            continue;
                        }
                        else if (takenRight)
                        {
                            _snakeDirections[nn] = Direction.West;
                            continue;
                        }
                        else
                        {
                            _snakeDirections[nn] = _rnd.Next(0, 100) < 50 ? Direction.West : Direction.East;
                            continue;
                        }
                    }
                    else if (takenBelow && _snakeDirections[nn] == Direction.South)
                    {
                        if (takenLeft)
                        {
                            _snakeDirections[nn] = Direction.East;
                            continue;
                        }
                        else if (takenRight)
                        {
                            _snakeDirections[nn] = Direction.West;
                            continue;
                        }
                        else
                        {
                            _snakeDirections[nn] = _rnd.Next(0, 100) < 50 ? Direction.West : Direction.East;
                            continue;
                        }
                    }
                    else if (takenLeft && _snakeDirections[nn] == Direction.West)
                    {
                        if (takenAbove)
                        {
                            _snakeDirections[nn] = Direction.South;
                            continue;
                        }
                        else if (takenBelow)
                        {
                            _snakeDirections[nn] = Direction.North;
                            continue;
                        }
                        else
                        {
                            _snakeDirections[nn] = _rnd.Next(0, 100) < 50 ? Direction.North : Direction.South;
                            continue;
                        }
                    }
                    else if (takenRight && _snakeDirections[nn] == Direction.East)
                    {
                        if (takenAbove)
                        {
                            _snakeDirections[nn] = Direction.South;
                            continue;
                        }
                        else if (takenBelow)
                        {
                            _snakeDirections[nn] = Direction.North;
                            continue;
                        }
                        else
                        {
                            _snakeDirections[nn] = _rnd.Next(0, 100) < 50 ? Direction.North : Direction.South;
                            continue;
                        }
                    }

                    bool preferStraight = false;
                    if (_snakeDirections[nn] == Direction.North || _snakeDirections[nn] == Direction.South)
                    {
                        if (_snakes[nn].Pieces[0].X == _mouse1X || _snakes[nn].Pieces[0].X == _mouse2X ||
                            _snakes[nn].Pieces[0].X == _mouse3X)
                        {
                            preferStraight = true;
                        }
                    }
                    else
                    {
                        if (_snakes[nn].Pieces[0].Y == _mouse1Y || _snakes[nn].Pieces[0].Y == _mouse2Y ||
                            _snakes[nn].Pieces[0].Y == _mouse3Y)
                        {
                            preferStraight = true;
                        }
                    }

                    if (!preferStraight)
                    {
                        if (_snakeDirections[nn] == Direction.East || _snakeDirections[nn] == Direction.West)
                        {
                            if (_mouse1X == _snakes[nn].Pieces[0].X)
                            {
                                if (_mouse1Y < _snakes[nn].Pieces[0].Y && !takenAbove)
                                {
                                    _snakeDirections[nn] = Direction.North;
                                    continue;
                                }
                                else if (_mouse1Y > _snakes[nn].Pieces[0].Y && !takenBelow)
                                {
                                    _snakeDirections[nn] = Direction.South;
                                    continue;
                                }
                            }
                            if (_mouse2X == _snakes[nn].Pieces[0].X)
                            {
                                if (_mouse2Y < _snakes[nn].Pieces[0].Y && !takenAbove)
                                {
                                    _snakeDirections[nn] = Direction.North;
                                    continue;
                                }
                                else if (_mouse2Y > _snakes[nn].Pieces[0].Y && !takenBelow)
                                {
                                    _snakeDirections[nn] = Direction.South;
                                    continue;
                                }
                            }
                            if (_mouse3X == _snakes[nn].Pieces[0].X)
                            {
                                if (_mouse3Y < _snakes[nn].Pieces[0].Y && !takenAbove)
                                {
                                    _snakeDirections[nn] = Direction.North;
                                    continue;
                                }
                                else if (_mouse3Y > _snakes[nn].Pieces[0].Y && !takenBelow)
                                {
                                    _snakeDirections[nn] = Direction.South;
                                    continue;
                                }
                            }
                        }
                        else if (_snakeDirections[nn] == Direction.North || _snakeDirections[nn] == Direction.South)
                        {
                            if (_mouse1Y == _snakes[nn].Pieces[0].Y)
                            {
                                if (_mouse1X < _snakes[nn].Pieces[0].X && !takenLeft)
                                {
                                    _snakeDirections[nn] = Direction.West;
                                    continue;
                                }
                                else if (_mouse1X > _snakes[nn].Pieces[0].X && !takenRight)
                                {
                                    _snakeDirections[nn] = Direction.East;
                                    continue;
                                }
                            }
                            if (_mouse2Y == _snakes[nn].Pieces[0].Y)
                            {
                                if (_mouse2X < _snakes[nn].Pieces[0].X && !takenLeft)
                                {
                                    _snakeDirections[nn] = Direction.West;
                                    continue;
                                }
                                else if (_mouse2X > _snakes[nn].Pieces[0].X && !takenRight)
                                {
                                    _snakeDirections[nn] = Direction.East;
                                    continue;
                                }
                            }
                            if (_mouse3Y == _snakes[nn].Pieces[0].Y)
                            {
                                if (_mouse3X < _snakes[nn].Pieces[0].X && !takenLeft)
                                {
                                    _snakeDirections[nn] = Direction.West;
                                    continue;
                                }
                                else if (_mouse3X > _snakes[nn].Pieces[0].Y && !takenRight)
                                {
                                    _snakeDirections[nn] = Direction.East;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        // Function that checks if any snake is dead (hit another snake, itself, the wall)
        private void CheckDeath()
        {
            for (int nCheck = 0; nCheck < _snakes.Length; nCheck++)
            {
                if (!_snakes[nCheck].Dead)
                {
                    if (_snakes[nCheck].Pieces[0].X < 0 || _snakes[nCheck].Pieces[0].Y < 0 ||
                        _snakes[nCheck].Pieces[0].X > 31 || _snakes[nCheck].Pieces[0].Y > 23)
                    {
                        _snakes[nCheck].Dead = true;
                        DeadenSnake(nCheck);
                        continue;
                    }
                    for (int nCollide = 0; nCollide < _snakes.Length; nCollide++)
                    {
                        if (!_snakes[nCollide].Dead)
                        {
                            int startIndex = 0;
                            if (_snakes[nCheck].Index == _snakes[nCollide].Index)
                            {
                                // If this snake is the one we are checking, exclude its head
                                // from the collision check
                                startIndex = 1;
                            }
                            for (int nn = startIndex; nn < _snakes[nCollide].Pieces.Length; nn++)
                            {
                                if (_snakes[nCheck].Pieces[0].X == _snakes[nCollide].Pieces[nn].X &&
                                    _snakes[nCheck].Pieces[0].Y == _snakes[nCollide].Pieces[nn].Y)
                                {
                                    _snakes[nCheck].Dead = true;
                                    break;
                                }
                            }
                        }
                        if (_snakes[nCheck].Dead)
                        {
                            DeadenSnake(nCheck);
                            continue;
                        }
                    }
                }
            }
        }


        private void DeadenSnake(int index)
        {
            for (int nn = 0; nn < _snakes[index].Pieces.Length; nn++)
            {
                _snakes[index].Pieces[nn].Pictue.Image = null;
                _snakes[index].Pieces[nn].Pictue.BackColor = Color.SlateGray;
                _snakes[index].Pieces[nn].Pictue.SendToBack();
            }
        }


        // Move the snakes
        private void MoveSnakes()
        {

            if (_movementRequested)
            {
                _snakeDirections[_playerSnake] = _requestedDirectionChange;
                _movementRequested = false;
            }
                        
            // Loop each snake
            for (int nn = 0; nn <= 3; nn++)
            {
                // Make sure the current snake is alive
                if (!_snakes[nn].Dead)
                {
                    // Keep the snake array local for ease of code
                    SnakePiece[] pieces = _snakes[nn].Pieces;
                    //Loop each piece in the snake, EXCEPT the head, starting from the tail and
                    // ending at index 1
                    for (int np = pieces.Length - 1; np > 0; np--)
                    {
                        if (!pieces[np].Locked)
                        {
                            // If the piece is not locked, it moves to the preceding sections
                            // location
                            pieces[np].X = pieces[np - 1].X;
                            pieces[np].Y = pieces[np - 1].Y;
                            pieces[np].Pictue.Top = pieces[np].Y * 25;
                            pieces[np].Pictue.Left = pieces[np].X * 25;
                        }
                        else
                        {
                            // If this piece is locked (i.e. it has just been created), unlock it
                            // (but obviously it does not move on this cycle)
                            pieces[np].Locked = false;
                        }
                    }
                    // Now do the head piece (always index 0 for each snake)
                    if (_snakeDirections[nn] == Direction.North)
                    {
                        pieces[0].Y = pieces[0].Y - 1;
                    }
                    else if (_snakeDirections[nn] == Direction.South)
                    {
                        pieces[0].Y = pieces[0].Y + 1;
                    }
                    else if (_snakeDirections[nn] == Direction.East)
                    {
                        pieces[0].X = pieces[0].X + 1;
                    }
                    else if (_snakeDirections[nn] == Direction.West)
                    {
                        pieces[0].X = pieces[0].X - 1;
                    }

                    // Rotate the image for the snake head if needed. Gotta do it before changing the piece direction
                    if (_snakeDirections[nn] != _previousSnakeDirections[nn])
                    {
                        RotateHeadForSnake(pieces[0].Pictue, _snakeDirections[nn], _previousSnakeDirections[nn]);
                        _previousSnakeDirections[nn] = _snakeDirections[nn];
                    }
                    
                    // Set the head's picture box location
                    pieces[0].Pictue.Top = pieces[0].Y * 25;
                    pieces[0].Pictue.Left = pieces[0].X * 25;

                    // Check if we ate food. If we did, add score, place the food again, and call
                    // function to grow the snake
                    if (pieces[0].X == _mouse1X && pieces[0].Y == _mouse1Y)
                    {
                        AddScore(_snakes[nn].Color);
                        PlaceFood(1);
                        GrowSnake(nn);
                    }
                    else if (pieces[0].X == _mouse2X && pieces[0].Y == _mouse2Y)
                    {
                        AddScore(_snakes[nn].Color);
                        PlaceFood(2);
                        GrowSnake(nn);
                    }
                    else if (pieces[0].X == _mouse3X && pieces[0].Y == _mouse3Y)
                    {
                        AddScore(_snakes[nn].Color);
                        PlaceFood(3);
                        GrowSnake(nn);
                    }

                }
            }

        }

        // Function that grows a snake
        private void GrowSnake(int snakeIndex)
        {
            // Have a list for holding the existing pieces for this snake
            List<SnakePiece> existing = new();
            existing.AddRange(_snakes[snakeIndex].Pieces);

            // Make a new snake piece. It starts at the same position as the current last block,
            // but locked so it does not move on the next tick
            SnakePiece newPiece = new()
            {
                X = _snakes[snakeIndex].Pieces[_snakes[snakeIndex].Pieces.Length - 1].X,
                Y = _snakes[snakeIndex].Pieces[_snakes[snakeIndex].Pieces.Length - 1].Y,
                Locked = true,
                Pictue = new()
                {
                    SizeMode = PictureBoxSizeMode.Normal,
                    Width = 25,
                    Height = 25,
                    ClientSize = new Size(25, 25)
                }
            };

            // Choose the picture based on the snake color
            newPiece.Pictue.Image = _snakes[snakeIndex].Color == SnakeColor.Pink ? picPinkBody.Image :
                _snakes[snakeIndex].Color == SnakeColor.Green ? picGreenBody.Image :
                _snakes[snakeIndex].Color == SnakeColor.Yellow ? picYellowBody.Image :
                picTurquoiseBody.Image;

            // Add it to the board and set the PictureBox location
            board.Controls.Add(newPiece.Pictue);
            newPiece.Pictue.Top = newPiece.Y * 25;
            newPiece.Pictue.Left = newPiece.X * 25;

            newPiece.Pictue.BringToFront();

            // Add it to the list, and then set the snakes body array to be the
            // contents of this list (existing + the new piece)
            existing.Add(newPiece);
            _snakes[snakeIndex].Pieces = existing.ToArray();


        }

        // Adds score for a snake, and updates the right score label
        private void AddScore(SnakeColor color)
        {
            switch (color)
            {
                case SnakeColor.Pink:
                    _pinkScore += 1;
                    lblPinkScore.Text = _pinkScore.ToString();
                    break;
                case SnakeColor.Green:
                    _greenScore += 1;
                    lblGreenScore.Text = _greenScore.ToString();
                    break;
                case SnakeColor.Yellow:
                    _yellowScore += 1;
                    lblYellowScore.Text = _yellowScore.ToString();
                    break;
                case SnakeColor.Turquoise:
                    _turquoiseScore += 1;
                    lblTealScore.Text = _turquoiseScore.ToString();
                    break;
            }

            _foodEaten += 1;
            if (_foodEaten % 5 == 0 && _gameSpeed > 80)
            {
                _gameSpeed -= 10;
            }
        }

        // Rotates the snake head, depending on its last direction and current direction
        private void RotateHeadForSnake(PictureBox picBox, Direction newDirection, Direction oldDirection)
        {

            if (newDirection == oldDirection)
            {
                return;
            }

            switch (newDirection)
            {
                case Direction.North:

                    // North can only be reached from East and West, rotate accordingly
                    if (oldDirection == Direction.East)
                    {
                        picBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    else
                    {
                        picBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    break;

                case Direction.East:

                    // East can only be reached from South and North, rotate accordingly
                    if (oldDirection == Direction.North)
                    {
                        picBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else
                    {
                        picBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    break;

                case Direction.South:

                    // South is the exception, as the snake is drawn facing North, and so
                    // we do need to code a North -> South flip for the initial drawing
                    if (oldDirection == Direction.North)
                    {
                        picBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    }
                    else if (oldDirection == Direction.East)
                    {
                        picBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else
                    {
                        picBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    break;

                case Direction.West:

                    // West can only be reached from South and North, rotate accordingly
                    if (oldDirection == Direction.North)
                    {
                        picBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    else
                    {
                        picBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    break;
            }

            // Force the picture box to repaint at the next paint operation (feels to me
            // like I shouldn't need to do this...Windows Forms is kinda old tech)
            picBox.Invalidate();            
            
        }


        // Key press detection
        private void FrmMain_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _loopEnd = true;
                Application.Exit();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (_snakeDirections[_playerSnake] != Direction.South)
                {
                    _requestedDirectionChange = Direction.North;
                    _movementRequested = true;
                }
                else
                {
                    _movementRequested = false;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_snakeDirections[_playerSnake] != Direction.North)
                {
                    _requestedDirectionChange = Direction.South;
                    _movementRequested = true;
                }
                else
                {
                    _movementRequested = false;
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (_snakeDirections[_playerSnake] != Direction.East)
                {
                    _requestedDirectionChange= Direction.West;
                    _movementRequested = true;
                }
                else
                {
                    _movementRequested = false;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (_snakeDirections [_playerSnake] != Direction.West)
                {
                    _requestedDirectionChange = Direction.East;
                    _movementRequested = true;
                }
                else
                {
                    _movementRequested = false;
                }
            }
        }

    }
}
