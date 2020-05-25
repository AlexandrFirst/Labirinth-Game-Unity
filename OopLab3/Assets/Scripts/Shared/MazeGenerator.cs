using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeDll;

    public class MazeGenerator
    {
        public List<List<Point>> maze = new List<List<Point>>();
        Stack<char> doors_keys;

        public int Initial_door_count { get; private set; }
        public int start_x { get; private set; }
        public int start_y { get; private set; }

        int real_size_x;
        int real_size_y;

        public MazeGenerator(int size_x, int size_y)
        {
            real_size_x = size_x;
            real_size_y = size_y;

            FillArea();
            CreateMaze();
        }

        void FillArea()
        {
            for (int i = 0; i < real_size_y; i++)
            {
                List<Point> row = new List<Point>();
                for (int j = 0; j < real_size_x; j++)
                {
                    row.Add(new Point('#', j, i));
                }
                maze.Add(row);
            }

            doors_keys = new Stack<char>();

            //manage the number of keys(custom set)
            doors_keys.Push('a');
            doors_keys.Push('A');
            doors_keys.Push('b');
            doors_keys.Push('B');
            doors_keys.Push('c');
            doors_keys.Push('C');

            Initial_door_count = doors_keys.Count / 2;
        }

        void CreateMaze()
        {
            Random random = new Random();

            int current_x = random.Next(real_size_x);
            int current_y = random.Next(real_size_y);

            start_x = current_x;
            start_y = current_y;

            Stack<Point> visited = new Stack<Point>();
            Point current = maze[current_y][current_x];
            current.symbol = ' ';
            visited.Push(current);
            maze[current_y][current_x] = current;
            while (visited.Count > 0)
            {
                List<int> direction = new List<int>() { 0, 1, 2, 3 }.OrderBy(x => Guid.NewGuid()).ToList();
                bool b = false;

                current_x = visited.Peek().x;
                current_y = visited.Peek().y;

                foreach (int item in direction)
                {
                    if (item == 0)//down
                    {
                        if (current_y + 2 < real_size_y && maze[current_y + 2][current_x].symbol != ' ')
                        {
                            b = false;
                            Point temp = visited.Pop();
                            temp.down = true;
                            visited.Push(temp);
                            current = maze[current_y + 2][current_x];
                            current.symbol = ' ';
                            current.up = true;
                            maze[current_y + 2][current_x] = current;

                            temp = maze[current_y + 1][current_x];
                            temp.symbol = ' ';
                            maze[current_y + 1][current_x] = temp;

                            visited.Push(current);
                            break;
                        }
                        else
                        {
                            b = true;
                            continue;
                        }
                    }
                    else if (item == 1)//up
                    {
                        if (current_y - 2 >= 0 && maze[current_y - 2][current_x].symbol != ' ')
                        {
                            b = false;
                            Point temp = visited.Pop();
                            temp.up = true;
                            visited.Push(temp);
                            current = maze[current_y - 2][current_x];
                            current.symbol = ' ';
                            current.down = true;
                            maze[current_y - 2][current_x] = current;

                            temp = maze[current_y - 1][current_x];
                            temp.symbol = ' ';
                            maze[current_y - 1][current_x] = temp;


                            visited.Push(current);
                            break;
                        }
                        else
                        {
                            b = true;
                            continue;
                        }
                    }
                    else if (item == 2)//left
                    {
                        if (current_x - 2 >= 0 && maze[current_y][current_x - 2].symbol != ' ')
                        {
                            b = false;
                            Point temp = visited.Pop();
                            temp.left = true;
                            visited.Push(temp);
                            current = maze[current_y][current_x - 2];
                            current.symbol = ' ';
                            current.right = true;
                            maze[current_y][current_x - 2] = current;

                            temp = maze[current_y][current_x - 1];
                            temp.symbol = ' ';
                            maze[current_y][current_x - 1] = temp;

                            visited.Push(current);
                            break;
                        }
                        else
                        {
                            b = true;
                            continue;
                        }
                    }
                    else if (item == 3)//right
                    {
                        if (current_x + 2 < real_size_x && maze[current_y][current_x + 2].symbol != ' ')
                        {
                            b = false;
                            Point temp = visited.Pop();
                            temp.right = true;
                            visited.Push(temp);
                            current = maze[current_y][current_x + 2];
                            current.symbol = ' ';
                            current.left = true;
                            maze[current_y][current_x + 2] = current;

                            temp = maze[current_y][current_x + 1];
                            temp.symbol = ' ';
                            maze[current_y][current_x + 1] = temp;

                            visited.Push(current);
                            break;
                        }
                        else
                        {
                            b = true;
                            continue;
                        }
                    }
                }

                if (b)
                {
                    if ((current.left && !current.right && !current.up && !current.down) ||
                        //(!current.left && current.right && !current.up && !current.down) ||
                        (!current.left && !current.right && current.up && !current.down) ||
                        //(!current.left && !current.right && !current.up && current.down) ||
                        (current.left && !current.right && current.up && !current.down))
                    {
                        if (maze[current_y][current_x].door_key == ' ' && (current.y != current_y || current.x != current_x))
                        {
                            if (doors_keys.Count > 0)
                            {
                                Point temp = maze[current_y][current_x];
                                temp.door_key = doors_keys.Pop();
                                maze[current_y][current_x] = temp;
                                //Console.WriteLine(current_y + " " + current_x);

                            }
                        }
                    }
                    current = visited.Pop();
                }
            }
        }
    }
