﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SFML.Graphics;

namespace ChendiAdventures
{
    public class HighscoreRecord
    {
        public HighscoreRecord(int score, int level, string difficulty)
        {
            Score = score;
            Level = level;
            Difficulty = difficulty;
        }

        public int Score { get; set; }
        public int Level { get; set; }
        public string Difficulty { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.Append(Score);
            for (var i = 0; i < 8 - Score.ToString().Length; i++)
                str.Append(" ");
            str.Append("LEVEL ");
            str.Append(Level);
            for (var i = 0; i < 4 - Level.ToString().Length; i++)
                str.Append(" ");
            str.Append(Difficulty);
            return str.ToString();
        }
    }

    public class Highscores : Drawable
    {
        public readonly List<HighscoreRecord> Scores;

        public Highscores()
        {
            _path = @".data/highscores.dat";
            _maxAmountOfRecords = 20;
            _highscores = new TextLine("", 50, 470, 70, Color.White);
            _highscores.SetOutlineThickness(5);

            Scores = new List<HighscoreRecord>();
            Scores.Clear();
            LoadHighscores();
        }

        public float X
        {
            get => _highscores.X;
            set => _highscores.X = value;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_highscores, states);
        }

        public void AddNewRecord(HighscoreRecord record)
        {
            for (var i = 0; i < Scores.Count; i++)
                if (record.Score > Scores[i].Score)
                {
                    Scores.Insert(i, record);
                    break;
                }

            while (Scores.Count > _maxAmountOfRecords)
                Scores.RemoveAt(Scores.Count - 1);

            SaveHighscores();
            _highscores.EditText(GetHighscores());
        }

        public void LoadHighscores()
        {
            string[] content;
            try
            {
                content = File.ReadAllLines(_path);
            }
            catch (Exception)
            {
                content = new string[0];
            }

            Scores.Clear();

            foreach (var line in content)
            {
                if (line == "\n" || line == "" || line == " " || line == "\r") break;
                var tmp = line.Split(' ');

                Scores.Add(new HighscoreRecord(int.Parse(tmp[0]), int.Parse(tmp[1]), tmp[2]));
            }

            _highscores.EditText(GetHighscores());
        }

        public string GetHighscores()
        {
            var str = new StringBuilder();

            for (var i = 0; i < Scores.Count; i++)
            {
                str.Append(i + 1);
                if (i + 1 < 10) str.Append(".  ");
                else str.Append(". ");
                str.Append(Scores[i]);
                str.Append("\n");
            }

            return str.ToString();
        }

        public void SaveHighscores()
        {
            var str = new StringBuilder();

            foreach (var record in Scores)
                str.Append(record.Score + " " + record.Level + " " + record.Difficulty + "\n");

            File.WriteAllText(_path, str.ToString());
        }

        private readonly TextLine _highscores;
        private readonly int _maxAmountOfRecords;
        private readonly string _path;
    }
}