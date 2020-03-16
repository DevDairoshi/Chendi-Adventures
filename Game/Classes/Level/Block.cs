﻿using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace ChendiAdventures
{
    public class Block : Entity
    {
        public static Sound sCrush = new Sound(new SoundBuffer(@"sfx/crush.wav"));
        public static Sound sPurify = new Sound(new SoundBuffer(@"sfx/petrifier.wav"));
        public static Sound sHard = new Sound(new SoundBuffer(@"sfx/hard.wav"));
        public static Sound sDestroy = new Sound(new SoundBuffer(@"sfx/destroyed.wav")) { Volume = 60 };
        public static Sound sLever = new Sound(new SoundBuffer(@"sfx/lever.wav"));
        public Clock DefaultTimer;
        public static Clock LeverTimer;

        public Block(float x, float y, Texture texture, BlockType type = BlockType.None, int hintNumber = 0) : base(x,
            y, texture)
        {
            OriginalPos = new Vector2f(x, y);
            Type = type;
            IsDestroyed = false;
            HintNumber = hintNumber;
            SetBlock(type);
        }
        public Vector2f OriginalPos { get; }
        public Animation BlockAnimation { get; private set; }
        public TextLine Hint { get; set; }
        public int HintNumber { get; set; }
        public bool IsDestroyed { get; private set; }
        public bool IsStomped { get; set; }
        public int Health { get; set; }
        public BlockType Type { get; set; }

        public void SetBlock(BlockType type)
        {
            switch (type)
            {
                case BlockType.Brick:
                {
                    SetTextureRectanlge(0, 0, 32, 32);
                    break;
                }
                case BlockType.TransparentBrick:
                {
                    SetTextureRectanlge(0, 160,32,32);
                    break;
                }
                case BlockType.HardBlock:
                {
                    SetTextureRectanlge(0, 192, 32, 32);
                    Health = 100;
                    break;
                }
                case BlockType.SteelGate:
                {
                    SetTextureRectanlge(96, 160, 32, 32);
                    Level.SteelGates.Add(this);
                    break;
                }
                case BlockType.Lever:
                {
                    SetTextureRectanlge(32, 160, 32, 32);
                    LeverTimer = new Clock();
                    Level.IsLeverOn = false;
                    Level.Levers.Add(this);
                    break;
                }
                case BlockType.EnergyBall:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 224),
                        new Vector2i(32, 224),
                        new Vector2i(64, 224),
                        new Vector2i(96, 224),
                        new Vector2i(64, 224),
                        new Vector2i(32, 224)
                    );
                    SetTextureRectanlge(0, 224, 32, 32);
                    break;
                }
                case BlockType.Spike:
                {
                    SetTextureRectanlge(0, 32, 32, 32);
                    break;
                }
                case BlockType.Enterance:
                {
                    SetTextureRectanlge(32, 64, 32, 32);
                    break;
                }
                case BlockType.Shop:
                {
                    SetTextureRectanlge(32, 32, 32, 32);
                    break;
                }
                case BlockType.Purifier:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(64, 64),
                        new Vector2i(96, 64)
                    );
                    SetTextureRectanlge(64, 64, 32, 32);
                    break;
                    }
                case BlockType.Coin:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 0),
                        new Vector2i(32, 0),
                        new Vector2i(64, 0),
                        new Vector2i(96, 0),
                        new Vector2i(64, 0),
                        new Vector2i(32, 0)
                    );
                    SetTextureRectanlge(0, 0, 32, 32);
                    break;
                }
                case BlockType.SackOfGold:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 320),
                        new Vector2i(32, 320),
                        new Vector2i(64, 320),
                        new Vector2i(96, 320)
                    );
                    SetTextureRectanlge(0, 320, 32, 32);
                    break;
                }
                case BlockType.Life:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 96),
                        new Vector2i(32, 96),
                        new Vector2i(64, 96),
                        new Vector2i(96, 96),
                        new Vector2i(64, 96),
                        new Vector2i(32, 96)
                    );
                    SetTextureRectanlge(0, 96, 32, 32);
                    break;
                }
                case BlockType.Arrow:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 160),
                        new Vector2i(32, 160),
                        new Vector2i(64, 160),
                        new Vector2i(96, 160),
                        new Vector2i(64, 160),
                        new Vector2i(32, 160)
                    );
                    SetTextureRectanlge(0, 160, 32, 32);
                    break;
                }
                case BlockType.TripleArrow:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 192),
                        new Vector2i(32, 192),
                        new Vector2i(64, 192),
                        new Vector2i(96, 192),
                        new Vector2i(64, 192),
                        new Vector2i(32, 192)
                    );
                    SetTextureRectanlge(0, 192, 32, 32);
                    break;
                }
                case BlockType.TripleMana:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 288),
                        new Vector2i(32, 288),
                        new Vector2i(64, 288),
                        new Vector2i(96, 288),
                        new Vector2i(64, 288),
                        new Vector2i(32, 288)
                    );
                    SetTextureRectanlge(0, 288, 32, 32);
                    break;
                }
                case BlockType.Score1000:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 224),
                        new Vector2i(32, 224),
                        new Vector2i(64, 224),
                        new Vector2i(96, 224),
                        new Vector2i(64, 224),
                        new Vector2i(32, 224)
                    );
                    SetTextureRectanlge(0, 224, 32, 32);
                    break;
                }
                case BlockType.Mana:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 256),
                        new Vector2i(32, 256),
                        new Vector2i(64, 256),
                        new Vector2i(96, 256),
                        new Vector2i(64, 256),
                        new Vector2i(32, 256)
                    );
                    SetTextureRectanlge(0, 256, 32, 32);
                    break;
                }
                case BlockType.Score5000:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 128),
                        new Vector2i(32, 128),
                        new Vector2i(64, 128),
                        new Vector2i(96, 128),
                        new Vector2i(64, 128),
                        new Vector2i(32, 128)
                    );
                    SetTextureRectanlge(0, 128, 32, 32);
                    break;
                }
                case BlockType.Stone:
                {
                    SetTextureRectanlge(64, 0, 32, 32);
                    break;
                }
                case BlockType.Illusion:
                {
                    SetTextureRectanlge(0, 0, 32, 32);
                    break;
                }
                case BlockType.Wood:
                {
                    SetTextureRectanlge(96, 0, 32, 32);
                    IsDestroyed = false;
                    break;
                }
                case BlockType.Trampoline:
                {
                    DefaultTimer = new Clock();
                    SetTextureRectanlge(64, 32, 32, 32);
                    break;
                }
                case BlockType.Exit:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 64),
                        new Vector2i(0, 96)
                    );
                    SetTextureRectanlge(0, 64, 32, 32);
                        break;
                }
                //doors
                case BlockType.SilverDoor:
                {
                    SetTextureRectanlge(64, 96, 32, 32);
                    break;
                }
                case BlockType.SilverKey:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 32),
                        new Vector2i(32, 32),
                        new Vector2i(64, 32),
                        new Vector2i(96, 32)
                    );
                    SetTextureRectanlge(0, 32, 32, 32);
                    break;
                }
                case BlockType.GoldDoor:
                {
                    SetTextureRectanlge(96, 96, 32, 32);
                    break;
                }
                case BlockType.GoldenKey:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 64),
                        new Vector2i(32, 64),
                        new Vector2i(64, 64),
                        new Vector2i(96, 64)
                    );
                    SetTextureRectanlge(0, 64, 32, 32);
                    break;
                }
                case BlockType.CrystalDoor:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 256),
                        new Vector2i(32, 256),
                        new Vector2i(64, 256),
                        new Vector2i(96, 256)
                    );
                    SetTextureRectanlge(0, 256, 32, 32);
                    break;
                    }
                case BlockType.CrystalKey:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 352),
                        new Vector2i(32, 352),
                        new Vector2i(64, 352),
                        new Vector2i(96, 352)
                    );
                    SetTextureRectanlge(0, 352, 32, 32);
                    break;
                }
                //teleports
                case BlockType.Teleport1:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 128),
                        new Vector2i(32, 128),
                        new Vector2i(64, 128),
                        new Vector2i(96, 128),
                        new Vector2i(64, 128),
                        new Vector2i(32, 128)
                    );
                        SetTextureRectanlge(0, 128, 32, 32);
                    break;
                }
                case BlockType.Teleport2:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 128),
                        new Vector2i(32, 128),
                        new Vector2i(64, 128),
                        new Vector2i(96, 128),
                        new Vector2i(64, 128),
                        new Vector2i(32, 128)
                    );
                    SetTextureRectanlge(0, 128, 32, 32);
                        break;
                }
                case BlockType.Teleport3:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 128),
                        new Vector2i(32, 128),
                        new Vector2i(64, 128),
                        new Vector2i(96, 128),
                        new Vector2i(64, 128),
                        new Vector2i(32, 128)
                    );
                    SetTextureRectanlge(0, 128, 32, 32);
                    break;
                }
                case BlockType.Teleport4:
                {
                    BlockAnimation = new Animation(this, 0.05f,
                        new Vector2i(0, 128),
                        new Vector2i(32, 128),
                        new Vector2i(64, 128),
                        new Vector2i(96, 128),
                        new Vector2i(64, 128),
                        new Vector2i(32, 128)
                    );
                    SetTextureRectanlge(0, 128, 32, 32);
                    break;
                }
                ///////
                case BlockType.Warning:
                {
                    SetTextureRectanlge(0, 0, 32, 32);
                    break;
                }
                case BlockType.Hint:
                {
                    Hint = new TextLine("", 8, -100, -100, new Color(255, 255, 255, 0));
                    Hint.SetOutlineThickness(0.8f);
                    SetTextureRectanlge(32, 0, 32, 32);
                    break;
                }
                case BlockType.LSpiderweb:
                {
                    SetTextureRectanlge(64, 0, 32, 32);
                    break;
                }
                case BlockType.RSpiderweb:
                {
                    SetTextureRectanlge(96, 0, 32, 32);
                    break;
                }
                case BlockType.Torch:
                {
                    BlockAnimation = new Animation(this, 0.1f,
                        new Vector2i(0, 32),
                        new Vector2i(32, 32),
                        new Vector2i(64, 32),
                        new Vector2i(32, 32)
                    );
                    SetTextureRectanlge(0, 32, 32, 32);
                    break;
                }
                case BlockType.EvilEyes:
                {
                    SetTextureRectanlge(96, 32, 32, 16);
                    break;
                }
                case BlockType.Grass:
                {
                    SetTextureRectanlge(128, 32, 32, 32);
                    break;
                    }
                case BlockType.None:
                {
                    SetTextureRectanlge(32, 96, 32, 32);
                    break;
                }
                default:
                {
                    SetTextureRectanlge(32, 96, 32, 32);
                    break;
                }
            }
        }

        public void DeleteObstacle()
        {
            if (Type == BlockType.Stone)
            {
                sCrush.Play();
                DefaultTimer.Dispose();
            }

            Type = BlockType.None;
            IsDestroyed = true;
            SetTextureRectanlge(32, 0, 32, 32);
        }

        public void DeletePickup()
        {
            Type = BlockType.None;
            SetTextureRectanlge(128, 128, 32, 32);
        }

        public void StoneUpdate()
        {
            if (Type == BlockType.Stone && IsStomped)
                if (DefaultTimer.ElapsedTime.AsSeconds() > 1)
                    DeleteObstacle();
        }

        public void Stomp()
        {
            if (Type == BlockType.Stone && !IsStomped)
            {
                IsStomped = true;
                DefaultTimer = new Clock();
            }
        }

        public void HitHardblock(MainCharacter character)
        {
            if (character.IsDownAttacking)
            {
                Health -= (int) character.SpeedY;
            }
            else
            {
                Health--;
            }

            if (sHard.Status != SoundStatus.Playing) sHard.Play();

            if (Health < 75) { SetTextureRectanlge(32,192); }
            if (Health < 50) { SetTextureRectanlge(64, 192); }
            if (Health < 25) { SetTextureRectanlge(96, 192); }
        }

        public static void FlipLever()
        {
            if (Level.IsLeverOn == false)
            {
                Level.IsLeverOn = true;
                LeverTimer.Restart();
                sLever.Play();

                foreach (Block gate in Level.SteelGates)
                {
                    gate.Type = BlockType.None;
                }

                foreach (Block lever in Level.Levers)
                {
                    lever.SetTextureRectanlge(64, 160);
                }

            }
        }
        public static void LeverMechanismUpdate()
        {
            if (Level.IsLeverOn == false)
            {
                foreach (var gate in Level.SteelGates)
                {
                    if (gate.Y < gate.OriginalPos.Y) gate.Y += 0.002f;
                }
            }
            else if (Level.IsLeverOn == true && LeverTimer.ElapsedTime.AsSeconds() < Level.LeverInterval)
            {
                foreach (var gate in Level.SteelGates)
                {
                    if (gate.Y > gate.OriginalPos.Y - 32) gate.Y -= 0.002f;
                }
            }
            else
            {
                Level.IsLeverOn = false;
                sLever.Play();
                foreach (Block gate in Level.SteelGates)
                {
                    gate.Type = BlockType.SteelGate;
                }
                foreach (var lever in Level.Levers)
                {
                    lever.SetTextureRectanlge(32, 160);
                }
            }
        }
    }
}