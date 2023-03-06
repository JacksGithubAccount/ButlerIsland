using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ButlerIsland
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        TileMap myMap;
        TileMap StageOne;
        int squaresAcross = 50;
        int squaresDown = 50;
        int score = 0;
        int timer = 1800000;
        int gameEndTimer = 90000;
        int loseTimer = 150000;
        bool isClear = false;
        bool introEnd = false;
        bool isLosing = false;
        //string level = "Tutorial";
        SpriteAnimation vlad;
        SpriteAnimation troll;
        //SpriteAnimation sloth;
        //SpriteAnimation kidTrollSA;
        Texture2D hilight;
        Texture2D titlescreen;
        Texture2D optionsScreen;
        Texture2D stageSelectScreen;
        Texture2D timeUp;
        Texture2D clear;
        Texture2D fail;
        SpriteAnimation titlescreenArrow;
        TrollBrain trollBrain;
        //TrollBrain slothBrain;
        //TrollBrain kidTroll;
        PlayerBrain playerBrain;
        KeyboardState oldState;
        GameState currentGameState;
        Texture2D whiteTexture;
        ScreenLighting screenLighting;

        //bool isMessage = false;

        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
            oldState = Keyboard.GetState();
            currentGameState = GameState.TitleScreen;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();           
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myMap = new TileMap();
            StageOne = new TileMap(20, 17);
            whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
            whiteTexture.SetData<Color>(new Color[] { Color.White });

            screenLighting = new ScreenLighting(whiteTexture);            

            timer = 1800000;
            gameEndTimer = 90000;
            loseTimer = 150000;
            score = 0;
            
            hilight = Content.Load<Texture2D>(@"Textures\TileSets\hilight");
            Tile.TileSetTexture = Content.Load<Texture2D>(@"Textures\TileSets\incrediblybadmspainttileset");
            titlescreen = Content.Load<Texture2D>(@"Textures\Backgrounds\Titlescreen_18(48)x11(48)");
            optionsScreen = Content.Load<Texture2D>(@"Textures\Backgrounds\Optionsscreen_18(48)x11(48)");
            stageSelectScreen = Content.Load <Texture2D>(@"Textures\Backgrounds\StageSelect_18(48)x11(48)");
            timeUp = Content.Load<Texture2D>(@"Textures\Text\timeUp");
            clear = Content.Load<Texture2D>(@"Textures\Text\clear");
            fail = Content.Load<Texture2D>(@"Textures\Text\fail");
            spriteFont = Content.Load<SpriteFont>(@"SpriteFont");

            Conversation.Initialize(spriteFont, Content.Load<SoundEffect>(@"SoundEffects\ContinueDialogue"), Content.Load<Texture2D>(@"Textures\DialogueBox\DialogueBoxBackground"),
                new Rectangle(50, 50, 400, 100), Content.Load<Texture2D>(@"Textures\DialogueBox\BorderImage"), 5, Color.White, Content.Load<Texture2D>(@"Textures\DialogueBox\ConversationContinueIcon"),
                Content.RootDirectory + @"\Conversations\");
            
            vlad = new SpriteAnimation(Content.Load<Texture2D>(@"Textures\Characters\T_Vlad_Sword_Walking_48x48"));
            troll = new SpriteAnimation(Content.Load<Texture2D>(@"Textures\Characters\Grey_Troll_Walking_48x48"));
            //sloth = new SpriteAnimation(Content.Load<Texture2D>(@"Textures\Characters\Grey_Troll_Walking_48x48"));
            //kidTrollSA = new SpriteAnimation(Content.Load<Texture2D>(@"Textures\Characters\T_Vlad_Sword_Walking_48x48"));
            titlescreenArrow = new SpriteAnimation(Content.Load <Texture2D>(@"Textures\Characters\TitleScreenArrow"));

            Camera.ViewWidth = this.graphics.PreferredBackBufferWidth;
            Camera.ViewHeight = this.graphics.PreferredBackBufferHeight;
            Camera.WorldWidth = ((myMap.MapWidth) * Tile.TileWidth);
            Camera.WorldHeight = ((myMap.MapHeight) * Tile.TileHeight);
            Camera.DisplayOffset = new Vector2(-48, -48);

            titlescreenArrow.AddAnimation("Idle", 0, 48 * 0, 48, 48, 4, 0.1f);
            titlescreenArrow.Position = new Vector2(290, 330);
            titlescreenArrow.DrawOffset = new Vector2(-24, -38);
            titlescreenArrow.CurrentAnimation = "Idle";
            titlescreenArrow.IsAnimating = true;          


            
            trollBrain = new TrollBrain(myMap, troll, "Troll");
            //slothBrain = new TrollBrain(myMap, sloth, "Sloth");
            //kidTroll = new TrollBrain(myMap, kidTrollSA, "Troll");
            playerBrain = new PlayerBrain(myMap, vlad, troll, oldState);
            playerBrain.initialize();
            trollBrain.initialize(vlad);
            //slothBrain.initialize(vlad);
            //kidTroll.initialize(vlad);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState ks = Keyboard.GetState();
            titlescreenArrow.Update(gameTime);
            //keys for fullcreen(doesnb't seem to work though
            if ( oldState.IsKeyDown(Keys.RightAlt))
            {
            }
            else if (ks.IsKeyDown(Keys.RightAlt))
            {
                if (graphics.IsFullScreen == true)
                {
                    graphics.IsFullScreen = false;
                }
                else
                {
                    graphics.IsFullScreen = true;
                }
            }
            //titlescreen interaction            
            if (currentGameState == GameState.TitleScreen)
            {
                
                if (ks.IsKeyDown(Keys.Enter) || ks.IsKeyDown(Keys.Space))
                {

                }
                else if (oldState.IsKeyDown(Keys.Enter) || oldState.IsKeyDown(Keys.Space))
                {
                    if (titlescreenArrow.Position == new Vector2(290, 330))
                    {
                        startGame("Tutorial");     
                    }
                    if (titlescreenArrow.Position == new Vector2(290, 330 + (48 * 1)))
                    {                        
                        currentGameState = GameState.StageSelectScreen;
                        titlescreenArrow.Position = new Vector2(305, 224);
                    }
                    if (titlescreenArrow.Position == new Vector2(290, 330 + (48 * 2)))
                    {
                        currentGameState = GameState.OptionsScreen;
                        titlescreenArrow.Position = new Vector2(100, 224);
                    }
                    if (titlescreenArrow.Position == new Vector2(290, 330 + (48 * 3)))
                    {
                        this.Exit();
                    }  
                }
                if (ks.IsKeyDown(Keys.Up))
                {

                }
                else if (oldState.IsKeyDown(Keys.Up))
                {
                    if (titlescreenArrow.Position != new Vector2(290, 330))
                    {
                        titlescreenArrow.Position += new Vector2(0, -48);
                    }
                }
                if (ks.IsKeyDown(Keys.Down))
                {

                }
                else if (oldState.IsKeyDown(Keys.Down))
                {
                    if (titlescreenArrow.Position != new Vector2(290, 330 + (48 * 3)))
                    {
                        titlescreenArrow.Position += new Vector2(0, 48);
                    }
                }
                //oldState = ks;
            }
            //End Game interactions
            else if (currentGameState == GameState.GameEnded)
            {                
                if (ks.IsKeyDown(Keys.Right))
                {

                }
                else if (oldState.IsKeyDown(Keys.Right))
                {
                    titlescreenArrow.Position = new Vector2(450, 480);
                }
                if (ks.IsKeyDown(Keys.Left))
                {

                }
                else if (oldState.IsKeyDown(Keys.Left))
                {
                    titlescreenArrow.Position = new Vector2(200, 480);
                }
                if (ks.IsKeyDown(Keys.Enter) || ks.IsKeyDown(Keys.Space))
                {

                }
                else if (oldState.IsKeyDown(Keys.Enter) || oldState.IsKeyDown(Keys.Space))
                {
                    
                    if (isClear == true)
                    {
                        currentGameState = GameState.TitleScreen;
                        titlescreenArrow.Position = new Vector2(290, 330);
                        resetGame();
                    }
                    else if (isClear == false)
                    {
                        if (titlescreenArrow.Position == new Vector2(200, 480))
                        {
                            //resetGame();
                            currentGameState = GameState.GameStarted;
                        }
                        if (titlescreenArrow.Position == new Vector2(450, 480))
                        {
                            currentGameState = GameState.TitleScreen;
                            titlescreenArrow.Position = new Vector2(290, 330);
                            //resetGame();
                        }
                    }
                }
                //oldState = ks;
            }
            //stageselect interactions
            else if (currentGameState == GameState.StageSelectScreen)
            {
                titlescreenArrow.Update(gameTime);
                if (ks.IsKeyDown(Keys.Enter) || ks.IsKeyDown(Keys.Space))
                {

                }
                else if (oldState.IsKeyDown(Keys.Enter) || oldState.IsKeyDown(Keys.Space))
                {
                    if (titlescreenArrow.Position == new Vector2(305, 224))
                    {
                        startGame("Tutorial");
                    }
                    if (titlescreenArrow.Position == new Vector2(305, 224 + (48 * 1)))
                    {
                        startGame("StageOne");
                    }
                    if (titlescreenArrow.Position == new Vector2(305, 224 + (48 * 2)))
                    {
                        startGame("StageTwo");
                    }
                    if (titlescreenArrow.Position == new Vector2(305, 224 + (48 * 3)))
                    {
                        startGame("StageThree");
                    }
                    if (titlescreenArrow.Position == new Vector2(305, 224 + (48 * 4)))
                    {
                        startGame("StageFour");
                    }
                    if (titlescreenArrow.Position == new Vector2(305, 224 + (48 * 5)))
                    {
                        startGame("StageFive");
                    }
                    if (titlescreenArrow.Position == new Vector2(305, 224 + (48 * 6)))
                    {
                        titlescreenArrow.Position = new Vector2(290, 330);
                        currentGameState = GameState.TitleScreen;
                    }
                }
                if (ks.IsKeyDown(Keys.Up))
                {

                }
                else if (oldState.IsKeyDown(Keys.Up))
                {
                    if (titlescreenArrow.Position != new Vector2(305, 224))
                    {
                        titlescreenArrow.Position += new Vector2(0, -48);
                    }
                }
                if (ks.IsKeyDown(Keys.Down))
                {

                }
                else if (oldState.IsKeyDown(Keys.Down))
                {
                    if (titlescreenArrow.Position != new Vector2(305, 224 + (48 * 6)))
                    {
                        titlescreenArrow.Position += new Vector2(0, 48);
                    }
                }                             
            }
            //optionsscreen interactions
            if (currentGameState == GameState.OptionsScreen)
            {
                titlescreenArrow.Update(gameTime);
                //at fullscreen
                if (titlescreenArrow.Position == new Vector2(100, 224))
                {
                    if (ks.IsKeyDown(Keys.Right))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Right))
                    {
                        titlescreenArrow.Position = new Vector2(410, 224);
                    }
                    if (ks.IsKeyDown(Keys.Down))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Down))
                    {
                        titlescreenArrow.Position = new Vector2(350, 500);
                    }
                }
                    //at fullscreen YES
                else if (titlescreenArrow.Position == new Vector2(410, 224))
                {
                    if (ks.IsKeyDown(Keys.Right))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Right))
                    {
                        titlescreenArrow.Position = new Vector2(560, 224);                                                    
                    }
                    if (ks.IsKeyDown(Keys.Left))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Left))
                    {
                        titlescreenArrow.Position = new Vector2(100, 224);
                    }
                    if (ks.IsKeyDown(Keys.Space) || ks.IsKeyDown(Keys.Enter))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Space) || oldState.IsKeyDown(Keys.Enter))
                    {
                        graphics.IsFullScreen = true;
                        graphics.ApplyChanges();
                    }

                }
                    //At fullscreen NO
                else if (titlescreenArrow.Position == new Vector2(560, 224))
                {
                    if (ks.IsKeyDown(Keys.Left))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Left))
                    {
                        titlescreenArrow.Position = new Vector2(410, 224);
                    }
                    if (ks.IsKeyDown(Keys.Space) || ks.IsKeyDown(Keys.Enter))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Space) || oldState.IsKeyDown(Keys.Enter))
                    {
                        graphics.IsFullScreen = false;
                        graphics.ApplyChanges();
                    }

                }
                    //At BACK
                else if(titlescreenArrow.Position == new Vector2(350, 500))
                {
                    if (ks.IsKeyDown(Keys.Up))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Up))
                    {
                        titlescreenArrow.Position = new Vector2(100, 224);
                    }
                    if (ks.IsKeyDown(Keys.Space) || ks.IsKeyDown(Keys.Enter))
                    {

                    }
                    else if (oldState.IsKeyDown(Keys.Space) || oldState.IsKeyDown(Keys.Enter))
                    {
                        currentGameState = GameState.TitleScreen;
                        titlescreenArrow.Position = new Vector2(290, 330);
                    }

                }
                //oldState = ks;
            }
            oldState = ks;
            //ingame interaction
            if (currentGameState == GameState.GameStarted)
            {
                if (introEnd == false)
                {
                    introEnd = Conversation.Update(gameTime);
                    screenLighting.changeLighting(255, 96);
                }
                else if (timer <= 0)
                {
                    screenLighting.changeLighting(255, 64);
                    gameEndTimer -= gameTime.TotalGameTime.Milliseconds;
                    if (gameEndTimer <= 0)
                    {
                        isClear = true;
                        currentGameState = GameState.GameEnded;
                        screenLighting.changeLighting(255, 128);
                    }
                }
                else if (loseTimer <= 0)
                {
                    screenLighting.changeLighting(255, 64);
                    gameEndTimer -= gameTime.TotalGameTime.Milliseconds;
                    if (gameEndTimer <= 0)
                    {
                        isClear = false;
                        resetGame();
                        currentGameState = GameState.GameEnded;
                        titlescreenArrow.Position = new Vector2(200, 480);
                        screenLighting.changeLighting(255, 128);
                    }
                }
                else
                {
                    screenLighting.changeLighting(255, 128);
                    //dialogueBox updates
                    //if (isMessage == true)
                    //{
                    Conversation.Update(gameTime);
                    //}
                    //playerbrain here
                    timer -= gameTime.TotalGameTime.Milliseconds;
                    score = playerBrain.rollPlayer(gameTime, score);
                    vlad.Update(gameTime);
                    //trollbrain here
                    //if (myMap.getLevelStage() == "Tutorial")
                    //{
                        trollBrain.rollTroll(gameTime);
                        troll.Update(gameTime);
                    //}
                    //if (myMap.getLevelStage() == "StageOne")
                    //{
                    //    slothBrain.rollTroll(gameTime);
                    //    sloth.Update(gameTime);
                    //}
                    // kidTroll.rollTroll(gameTime);
                    // kidTrollSA.Update(gameTime);
                    isLosing = myMap.updateMap();
                    if (isLosing)
                    {
                        loseTimer -= gameTime.TotalGameTime.Milliseconds;
                        myMap.updateMap();
                    }
                    else
                    {
                        loseTimer = 150000;
                    }
                }
                
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            if (currentGameState == GameState.TitleScreen)
            {
                spriteBatch.Draw(titlescreen, new Vector2(0, 0), Color.White);
                titlescreenArrow.Draw(spriteBatch, 0, 0);
            }
            if (currentGameState == GameState.StageSelectScreen)
            {
                spriteBatch.Draw(stageSelectScreen, new Vector2(0, 0), Color.White);
                titlescreenArrow.Draw(spriteBatch, 0, 0);
            }
            if (currentGameState == GameState.OptionsScreen)
            {
                spriteBatch.Draw(optionsScreen, new Vector2(0, 0), Color.White);
                titlescreenArrow.Draw(spriteBatch, 0, 0);
            }
            if (currentGameState == GameState.GameEnded)
            {
                GraphicsDevice.Clear(Color.Black);
                //drawString("CLEAR!", 300, 200, Color.White, 4.0f);
                if (isClear == true)
                {
                    spriteBatch.Draw(clear, new Vector2(0, 0), Color.White);
                }
                else if (isClear == false)
                {
                    spriteBatch.Draw(fail, new Vector2(0, 0), Color.White);
                    titlescreenArrow.Draw(spriteBatch, 0, 0);
                    }
                    drawString("Score: " + score, 350, 250, Color.White, 2.0f);                
            }
                if (currentGameState == GameState.GameStarted)
                {
                    Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileWidth, Camera.Location.Y / Tile.TileHeight);
                    int firstX = (int)firstSquare.X;
                    int firstY = (int)firstSquare.Y;

                    Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileWidth, Camera.Location.Y % Tile.TileHeight);
                    int offsetX = (int)squareOffset.X;
                    int offsetY = (int)squareOffset.Y;

                    for (int y = 0; y < squaresDown; y++)
                    {
                        for (int x = 0; x < squaresAcross; x++)
                        {
                            int mapx = (firstX + x);
                            int mapy = (firstY + y);
                            if ((mapx >= myMap.MapWidth) || (mapy >= myMap.MapHeight))
                                continue;
                            //draws the base tiles
                            foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                            {
                                spriteBatch.Draw(
                                    Tile.TileSetTexture,
                                    new Rectangle(
                                        (x * Tile.TileWidth) - offsetX, (y * Tile.TileHeight) - offsetY,
                                        Tile.TileWidth, Tile.TileHeight),
                                    Tile.GetSourceRectangle(tileID),
                                    Color.White);
                            }

                            int heightRow = 0;
                            //draws the height tiles
                            foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                            {
                                spriteBatch.Draw(
                                    Tile.TileSetTexture,
                                    new Rectangle(
                                        (x * Tile.TileWidth) - offsetX, (y * Tile.TileHeight) - offsetY,
                                        Tile.TileWidth, Tile.TileHeight),
                                    Tile.GetSourceRectangle(tileID),
                                    Color.White);
                                heightRow++;
                            }
                        }
                        vlad.Draw(spriteBatch, 0, 0);
                        //if (myMap.getLevelStage() == "Tutorial")
                        //{
                        troll.Draw(spriteBatch, 0, 0);
                        //}
                        //if (myMap.getLevelStage() == "StageOne")
                        //{
                        //sloth.Draw(spriteBatch, 0, 0);
                        //}
                        //kidTrollSA.Draw(spriteBatch, 0, 0);
                        drawString("Score: " + score, 700, 30, Color.White, 1.0f);
                        drawString("Time Left: " + timer / 30000, 710, 45, Color.White, 1.0f);

                    }
                }
                spriteBatch.End();
                screenLighting.draw(spriteBatch, GraphicsDevice);
                spriteBatch.Begin();
                if (currentGameState == GameState.GameStarted)
                {
                    Conversation.Draw(spriteBatch);
                    if (timer <= 0 || loseTimer <= 0)
                    {
                        //drawString("Time Up!", 300, 200, Color.White, 4.0f);
                        spriteBatch.Draw(timeUp, new Vector2(0, 0), Color.White);
                    }
                    if (isLosing)
                    {
                        drawString("Lose Time: " + loseTimer / 30000, 710, 60, Color.Red, 1.0f);
                    }
                }
                spriteBatch.End();
                // TODO: Add your drawing code here

                base.Draw(gameTime);
            
        }
        //method that draws a string wherever
        public void drawString(string printLine, int x, int y, Color color, float scale)
        {
            string output = printLine;
            Vector2 FontOrigin = spriteFont.MeasureString(output) / 2;
            spriteBatch.DrawString(spriteFont, output, new Vector2( x, y), color,
                0, FontOrigin, scale, SpriteEffects.None, 0.5f);
        }
        public void resetGame()
        {
            LoadContent();
            Camera.location = new Vector2(0, 0);
        }
        public void startGame(string StageName)
        {
            myMap.changeMap(StageName);
            currentGameState = GameState.GameStarted;
            if (myMap.getLevelStage() == "StageTwo")
            {
                timer = 3600000;
            }
            else
            {
                timer = 1800000;
            }
            try
            {
                Conversation.StartConversation(myMap.getLevelStage());
            }
            catch
            {
                Conversation.RemoveBox();
            }            
        }
    }
}
