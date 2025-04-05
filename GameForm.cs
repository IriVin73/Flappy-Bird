using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDP_FlappyBird
{
    public partial class GameForm : Form
    { //default game attributes
        //game variables
        private int score = 0;
        //pipe variables
        private Pipe[] pipes = new Pipe[2];//array to store the 2 pipe objects
        private int pipeSpeed = 2;
        //bird variables
        private Image bird = Image.FromFile("bird.png");
        private int birdX = 73;
        private int birdY = 226;
        private int birdHeight = 53;
        private int birdWidth = 70;
        private int gravity = 5;
        private bool spacepressed = false;

        public GameForm()
        {
            InitializeComponent();
            this.Height = 603;
            this.Width = 438;
            this.BackgroundImage = Image.FromFile("bg.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;

            //create top and bottom pipes
            pipes[0] = new Pipe(326, 0, "pipedown.png");
            pipes[1] = new Pipe(212, 350, "pipe.png");
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {

            this.Invalidate();//refreshes the form to redraw images in new locations
            // show the current score on the score text label
            lblScoreText.Text = "Score: " + score;

            //link the  pipes left position to the pipe speed integer,
            //it will reduce the pipe speed value from the left position of the pipe image so it will move left
            foreach (Pipe pipeitem in pipes)
            {
                //USE THE PIPE CLASS GETTER TO GET THE X POSITION AND STORE IN A NEW VARIABLE
                int xPos = pipeitem.getPositionX();
                //TAKE AWAY THE PIPE SPEED FROM THE NEW VARIABLE
                xPos = xPos - pipeSpeed;
                //USE THE PIPE CLASS SETTER TO CHANGE THE X POSITION OF THE PIPE
                pipeitem.setPositionX(xPos);

            }

            //// below we are checking if any of the pipes have left the screen
            if (pipes[0].getPositionX() < -80)
            {// if the top pipes X position is -80 then we will reset it back to 700 and add 1 to the score
                 pipes[0].setPositionX(700);
                    score += 1;
                
            }
            if (pipes[1].getPositionX() < -50)
            { // if the bottom pipes location is -50 then we will reset it back to 600 and add 1 to the score
              pipes[1].setPositionX(600);
                    score += 1;
                

            }

            //// link the flappy bird image to the gravity,
            //// += means it will add the speed of gravity to the images Y location so it will move down
            //ADD CODE HERE TO INCREASE BIRDS Y POSITION USING THE GRAVITY ATTRIBUTE
            if (spacepressed)
            {
                birdY -= gravity;
            }
            else
            { 
                birdY += gravity; 
            }

            if (birdY < 0 || birdY > 603)
            { endGame(); }


            checkCollision();//call method to detect if bird has hit a pipe
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {// this is the game key is down event thats linked to the main form
            //if the space key is pressed then set spacepressed attribute is set to false
            if (e.KeyCode == Keys.Space)
            {
                spacepressed = false;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {// this is the game key is down event thats linked to the main form
         // if the space key is pressed then set spacepressed attribute is set to true
            if (e.KeyCode == Keys.Space)
            {
                spacepressed = true;
            }
        }

        private void FormPaintEvent(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            foreach (Pipe pipeitem in pipes)
            {//loop for array of Pipe images
             //ADD CODE HERE TO DRAW THE PIPE IMAGE FROM THE ARRAY ONTO THE SCREEN
                Canvas.DrawImage(pipeitem.getItemImage(), pipeitem.getPositionX(), pipeitem.getPositionY(), pipeitem.getWidth(), pipeitem.getHeight());
                
            }
            //ADD CODE HERE TO DRAW THE BIRD IMAGE ONTO THE SCREEN USING BIRD ATTRIBUTES
            Canvas.DrawImage(bird, birdX, birdY, birdWidth, birdHeight);
        }

        private bool detectCollision(int object1X, int object1Y, int object1Width, int object1Height, int object2X, int object2Y, int object2Width, int object2Height)
        {//detect if two image objects collide with each other by checking the X and Y coordinates.
            if (object1X + object1Width <= object2X || object1X >= object2X + object2Width || object1Y + object1Height <= object2Y || object1Y >= object2Y + object2Height)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void checkCollision()
        {//check for collision between the bird and pipes by calling the detectCollision method

            //UPDATE THIS METHOD SO THAT THAT IT ITERATES THROUGH THE PIPES ARRAY TO CHECK FOR COLLISION WITH EACH PIPE ITEM
            //USE THE DETECTCOLLISION METHOD TO DETECT IF A COLLISION HAS HAPPENED (HINT: you will need to use the pipe class getters to get the values to pass in)
            //IF A TRUE IS RETURNED THEN CALL THE ENDGAME() METHOD
            foreach (Pipe pipeitem in pipes)
            {
                if (detectCollision(pipeitem.getPositionX(), pipeitem.getPositionY(), pipeitem.getWidth() , pipeitem.getHeight(), birdX, birdY, birdWidth, birdHeight))
                {
                    endGame();
                }
            }
        }
        private void endGame()
        {// this is the game end method, this method will execute when the bird touches the ground or the pipes
            GameTimer.Stop(); // stop the main timer

            // show the game over text on the score text,
            // += is used to add the new string of text next to the score instead of overriding it
            lblScoreText.Text += " Game over!!!";
        }

    }
}