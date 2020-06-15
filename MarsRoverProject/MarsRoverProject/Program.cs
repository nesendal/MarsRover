using System;
using System.Collections.Generic;
using System.IO;

namespace MarsRoverProject
{
    class Rover
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Face Face { get; set; }
        public Rover(string name, int x, int y, Face face)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
            this.Face = face;
        }

        private void TurnLeft()
        {
            Face = (Face)(((int)Face + 3) % 4);
        }

        private void TurnRight()
        {
            Face = (Face)(((int)Face + 5) % 4);
        }

        private void Move()
        {
            if (Face == Face.E)
                X++;
            else if (Face == Face.W)
                X--;
            else if (Face == Face.N)
                Y++;
            else if (Face == Face.S)
                Y--;
        }

        public void Do(char komut)
        {
            // Log eklenebilir
            switch (komut)
            {
                case 'L':
                    this.TurnLeft();
                    break;
                case 'R':
                    this.TurnRight();
                    break;
                case 'M':
                    this.Move();
                    break;
            }
        }

        public void Do(string komutSeti)
        {
            foreach (var komut in komutSeti)
            {
                this.Do(komut);
            }
        }

        public string GetInfo()
        {
            return $"{Name} {this.ToString()}";
        }

        public override string ToString()
        {
            return $"{X} {Y} {Face}";
        }
    }

    enum Face
    {
        N,
        E,
        S,
        W
    }

    static class StringExtensions
    {
        public static int ToInt32(this string s)
        {
            return Convert.ToInt32(s);
        }
    }

    class MarsArea
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private List<Rover> Rovers { get; set; }


        public MarsArea(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Rovers = new List<Rover>();
        }

        public void AddRover(Rover r)
        {
            this.Rovers.Add(r);
        }

        public override string ToString()
        {
            string info = "";
            this.Rovers.ForEach(r => info += r.ToString() + "\n");
            return info;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string komutSeti;
            Face face;
            int maxX, maxY, locX, locY;

            string[] instructions = File.ReadAllLines("inputs.txt");

            string[] coordinates = instructions[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            maxX = coordinates[0].ToInt32();
            maxY = coordinates[1].ToInt32();
            MarsArea ma = new MarsArea(maxX, maxY);

            for (int i = 1; i < instructions.Length; i += 2)
            {
                string[] roverPositionInfo = instructions[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                locX = roverPositionInfo[0].ToInt32();
                locY = roverPositionInfo[1].ToInt32();
                face = (Face)Enum.Parse(typeof(Face), roverPositionInfo[2]);

                Rover r = new Rover("Rover #" + (i / 2 + 1), locX, locY, face);
                ma.AddRover(r);

                komutSeti = instructions[i + 1];
                r.Do(komutSeti);
            }

            Console.WriteLine(ma.ToString());
        }
    }
}