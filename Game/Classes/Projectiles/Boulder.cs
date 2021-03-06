﻿using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace ChendiAdventures
{
    public class Boulder : Projectile
    {
        public static Sound sHurl = new Sound(new SoundBuffer(@"sfx/hurl.wav"));

        public Boulder(float x, float y, Texture texture, Movement dir) : base(x, y, texture, dir)
        {
            SpeedX = 0f;
            SpeedY = 0f;
            isAttacking = false;

            Direction = dir;

            _anim = new Animation(this, 0.05f,
                new Vector2i(0, 64),
                new Vector2i(16, 64),
                new Vector2i(32, 64),
                new Vector2i(48, 64),
                new Vector2i(32, 64),
                new Vector2i(16, 64)
            );
        }

        public float SpeedX { get; private set; }
        public float SpeedY { get; private set; }
        public bool isAttacking { get; private set; }
        public Movement Direction { get; set; }

        public void Attack(float x, float y, Movement dir)
        {
            SetPosition(x, y);
            SpeedX = MainGameWindow.Randomizer.Next(5) + 4;
            SpeedY = -1 * MainGameWindow.Randomizer.Next(3) - 4;
            if (dir == Movement.Left) SpeedX *= -1;
            isAttacking = true;
            sHurl.Play();
        }

        public void ResetBoulder(Level level)
        {
            isAttacking = false;
            level.Particles.Add(new ParticleEffect(X - 8, Y - 8, new Color(100, 100, 100)));
            Block.sDestroy.Play();
            SetPosition(-50, 300);
        }

        public void BoulderUpdate(Level level)
        {
            if (isAttacking)
            {
                X += SpeedX;
                Y += SpeedY;
                SpeedY += 0.23f;
                _anim.Animate(16);

                if (level.UnpassableContains(level.GetObstacle(GetCenterPosition().X / 32, GetCenterPosition().Y / 32)
                    .Type)) ResetBoulder(level);
            }
        }

        private readonly Animation _anim;
    }
}