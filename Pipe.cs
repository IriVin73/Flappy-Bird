using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDP_FlappyBird
{
    internal class Pipe
    {//attributes
        private int positionX;
        private int positionY;
        private Image item_image;
        private int height;
        private int width;

        public Pipe(int X, int Y, string imageName)
        {//constructor
            this.item_image = Image.FromFile(imageName);//default image for item
            this.positionX = X;//identifies random coordinates to spawn pipe
            this.positionY = Y;
            this.height = 228;//specifies size of item
            this.width = 75;
        }
        //getter and setters (accessors and mutators)
        public int getPositionX() { return this.positionX; }
        public int getPositionY() { return this.positionY; }
        public int getHeight() { return this.height; }
        public int getWidth() { return this.width; }
        public Image getItemImage() { return this.item_image; }
        public void setPositionX(int X) { this.positionX = X; }
        public void setPositionY(int Y) {  this.positionY = Y; }
        

    }
}
