using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ButlerIsland
{
    //class to display dialogue box
    class Conversation
    {
        public static List<Speaker> ConversationSpeakers = new List<Speaker>();
        private static int currentSpeakerIndex = 0;
        public static string ConversationFileLocation;

        public static SpriteFont spriteFont;

        public static SoundEffect soundEffect;

        private static Rectangle textRectangle;
        private static string message;

        private static string revealedMessage;
        private static float messageSpeed = 0.008f;
        private static float messageTimer = 0.0f;
        private static int stringIndex;
        public static bool MessageShown = false;

        private static Texture2D splitIcon;
        private static float splitIconSpeed = 0.4f;
        private static float splitIconTimer = 0.0f;
        private static int splitIconOffsetValue = 5;
        private static bool splitIconOffset = false;

        private static Texture2D backgroundImage;
        private static Rectangle boxRectangle;

        private static Texture2D borderImage;
        private static int borderWidth;
        private static Color borderColor;

        //public static List<Texture2D> Avatars = new List<Texture2D>();
        //private static Rectangle avatarRectangle;

        public static bool Expired = false;

        public static Vector2 BoxPosition
        {
            get { return new Vector2(boxRectangle.X, boxRectangle.Y); }
        }

        public static Vector2 StringPosition
        {
            get { return new Vector2(textRectangle.X, textRectangle.Y); }
        }

        public static float MessageDelay
        {
            get { return messageSpeed; }
            set { messageSpeed = value; }
        }

        public static string Message
        {
            get { return message; }
            set { message = constrainText(value); }
        }
        public static void Initialize(SpriteFont font, SoundEffect sound, Texture2D background, Rectangle initialRectangle, Texture2D bImage, int bWidth, Color bColor, Texture2D sIcon, string path)
        {
            spriteFont = font;
            soundEffect = sound;
            backgroundImage = background;
            boxRectangle = initialRectangle;
            textRectangle = new Rectangle(initialRectangle.X + 10, initialRectangle.Y + 10, initialRectangle.Width - 20, initialRectangle.Height - 20);
            borderImage = bImage;
            borderWidth = bWidth;
            borderColor = bColor;
            splitIcon = sIcon;
            ConversationFileLocation = path;
        }
        public static void StartConversation(string conversationID)
        {
            // TODO: Load ID, then run createbox etc
            currentSpeakerIndex = 0;
            if (conversationID == "Door")
            {
                ConversationSpeakers.Add(new Speaker(constrainText("This is a Door")));
            }
            if (conversationID == "Tutorial")
            {
                ConversationSpeakers.Add(new Speaker(constrainText("Welcome to Butler Island!")));
                ConversationSpeakers.Add(new Speaker(constrainText("You've been recently hired here for your ability to buttle.")));
                ConversationSpeakers.Add(new Speaker(constrainText("Thanks to the season this island is going through, which is chair season, Everyone but you seems eager to sit in pushed in chairs!")));
                ConversationSpeakers.Add(new Speaker(constrainText("It's your job to push in chairs! ")));
                ConversationSpeakers.Add(new Speaker(constrainText("Move by the arrow keys.")));
                ConversationSpeakers.Add(new Speaker(constrainText("Spacebar for everything else!")));
                ConversationSpeakers.Add(new Speaker(constrainText("There will be a person or people running around trying to sit in chairs that are pushed in. For this tutorial level, we will have this troll!")));
                ConversationSpeakers.Add(new Speaker(constrainText("The troll is an agressive person who'll push you out of his way!")));
                ConversationSpeakers.Add(new Speaker(constrainText("For this level, there is a time limit before the troll tires out from sitting. Keep pushing in chairs for the whole duration!")));
                ConversationSpeakers.Add(new Speaker(constrainText("Since this is the tutorial level, there is no lose conditions, so it's a sandbox mode for a whole minute.")));
                ConversationSpeakers.Add(new Speaker(constrainText("Get pushing!")));
            }
            if (conversationID == "StageOne")
            {
                //ConversationSpeakers.Add(new Speaker(constrainText("The Sloth is the Chair sitter of this level.")));
                //ConversationSpeakers.Add(new Speaker(constrainText("Being a Sloth, The Sloth moves really slow and likes to take time sitting.")));
                ConversationSpeakers.Add(new Speaker(constrainText("Win Condition: Don't lose for 60 seconds!")));
                ConversationSpeakers.Add(new Speaker(constrainText("Lose Condition: Have more than five chairs out for five seconds in a row!")));
            }
            if (conversationID == "StageTwo")
            {
                ConversationSpeakers.Add(new Speaker(constrainText("Win Condition: Don't lose for 120 seconds!")));
                ConversationSpeakers.Add(new Speaker(constrainText("Lose Condition: Have more than five chairs out for five seconds in a row!")));
            }
            CreateBox(ConversationSpeakers[currentSpeakerIndex].Message,
                new Rectangle(100, 270, 600, 150),
                new Rectangle(150, 285, 445, 115));

        }
        public static void CreateBox(string msg, Rectangle msgBox, Rectangle textBox, Texture2D background)
        {
            boxRectangle = msgBox;
            textRectangle = textBox;
            //avatarRectangle = avatarBox;
            backgroundImage = background;
            Expired = false;
            stringIndex = 0;
            revealedMessage = "";
            MessageShown = false;

            // Set last so it breaks appropriately to fit the box (textRectangle MUST be set first)
            Message = msg;
        }

        /// Creates a new Conversation Window
        public static void CreateBox(string msg, Rectangle msgBox, Rectangle textBox)
        {
            CreateBox(msg, msgBox, textBox, backgroundImage);
        }
        public static void CreateBox(string msg, Rectangle msgBox)
        {
            CreateBox(msg, msgBox, textRectangle, backgroundImage);
        }
        public static void CreateBox(string msg)
        {
            CreateBox(msg, boxRectangle, textRectangle, backgroundImage);
        }
        /// Removes the Conversation Box
        public static void RemoveBox()
        {
            Expired = true;
        }

        /// Updates an existing Conversation Box
        public static void UpdateBox(string msg)
        {
            Message = msg;
            Expired = false;
            stringIndex = 0;
            revealedMessage = "";
            MessageShown = false;
        }
        public static void ClearConversation()
        {
            ConversationSpeakers.Clear();
        }
        /// <summary>
        /// Breaks up a string so it fits in the box. Will split long messages into two or three if necessary
        /// </summary>
        /// <param name="message">Speaker Message String</param>
        /// <returns>Formatted String</returns>
        private static string constrainText(String message)
        {
            bool filled = false;
            string line = "";
            string returnString = "";
            string[] wordArray = message.Split(' ');

            // Go through each word in string
            foreach (string word in wordArray)
            {
                // If we add the next word to the current line and go beyond the width...
                if (spriteFont.MeasureString(line + word).X > textRectangle.Width)
                {
                    // If adding a new line doesn't put us beyond height
                    if (spriteFont.MeasureString(returnString + line + "\n").Y < textRectangle.Height)
                    {
                        returnString += line + "\n";
                        line = "";
                    }
                    // If adding a new line does put us beyond height
                    else if (!filled)
                    {
                        filled = true;
                        returnString += line;
                        line = "";
                    }
                }
                line += word + " ";
            }

            // We need to add another Speaker Object first
            if (filled)
            {
                ConversationSpeakers.Insert(currentSpeakerIndex + 1, new Speaker(line));
                return returnString;
            }
            else
            {
                return returnString + line;
            }
        }
        private static void HandleKeyboardInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (currentSpeakerIndex + 1 < ConversationSpeakers.Count)
                {
                    soundEffect.Play();
                    currentSpeakerIndex++;
                    revealedMessage = "";
                    stringIndex = 0;
                    ConversationSpeakers[currentSpeakerIndex].Message = constrainText(ConversationSpeakers[currentSpeakerIndex].Message);  
                    
                }
                else
                {
                    ConversationSpeakers.Clear();
                    RemoveBox();
                }
                MessageShown = false;
            }
        }
        public static bool Update(GameTime gameTime)
        {
            try
            {
                if (!Expired)
                {
                    float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                    messageTimer += elapsed;
                    splitIconTimer += elapsed;

                    if (messageTimer >= messageSpeed)
                    {
                        // Typewriter Effect
                        if (stringIndex < ConversationSpeakers[currentSpeakerIndex].Message.Length)
                        {
                            revealedMessage += ConversationSpeakers[currentSpeakerIndex].Message[stringIndex];
                            stringIndex++;
                        }
                        // Full message displayed, handle input
                        else
                        {
                            MessageShown = true;
                            KeyboardState keyboardState = Keyboard.GetState();
                            HandleKeyboardInput(keyboardState);                            
                        }
                        messageTimer = 0.0f;
                    }

                    // Update Continue Reading Icon
                    if (splitIconTimer >= splitIconSpeed)
                    {
                        splitIconOffset = !splitIconOffset;
                        splitIconTimer = 0.0f;
                    }
                }

            }
            catch
            {
                ConversationSpeakers.Clear();
                RemoveBox();
            }
            if (ConversationSpeakers.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Draws the Conversation Box to the Screen
        /// </summary>
        /// <param name="spriteBatch">XNA SpriteBatch</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (!Expired)
                {
                    // Only draw border if specified
                    if (borderImage != null)
                    {
                        spriteBatch.Draw(borderImage,
                            new Rectangle(boxRectangle.X - borderWidth, boxRectangle.Y - borderWidth, boxRectangle.Width + 2 * borderWidth, boxRectangle.Height + 2 * borderWidth),
                            borderColor);
                    }

                    // Only draw Background if specified
                    if (backgroundImage != null)
                    {
                        spriteBatch.Draw(backgroundImage, boxRectangle, Color.White);
                    }

                    // Check to make sure we have the Avatar
                    //if (ConversationSpeakers[currentSpeakerIndex].AvatarIndex < Avatars.Count())
                    //{
                    //    spriteBatch.Draw(Avatars[ConversationSpeakers[currentSpeakerIndex].AvatarIndex], avatarRectangle, Color.White);
                    //}

                    // Draw the Message
                    spriteBatch.DrawString(spriteFont, revealedMessage, StringPosition, Color.White);

                    // Check to see if we need to draw the Continue Reading icon
                    if (MessageShown && currentSpeakerIndex + 1 < ConversationSpeakers.Count())
                    {
                        Rectangle splitRectangle = new Rectangle(boxRectangle.X + boxRectangle.Width - 2 * splitIcon.Width + splitIcon.Width / 2,
                                                                 boxRectangle.Y + boxRectangle.Height - 2 * splitIcon.Height + splitIcon.Height / 2,
                                                                 splitIcon.Width,
                                                                 splitIcon.Height);

                        if (splitIconOffset)
                        {
                            splitRectangle.Y += splitIconOffsetValue;
                        }

                        spriteBatch.Draw(splitIcon, splitRectangle, Color.White);
                    }
                }
            }
            catch
            {
                
            }
        }
    }
}
