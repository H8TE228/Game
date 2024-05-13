using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Project1.Components;
using Project1.Controls;
using Project1.Enemy;
using Project1.MainChar;
using Project1.States.Quest.firstQuest;
using Project1.States.ToolsState;
using Project1.States;
using Project1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.States.Quest.SecondQuest.Test2.edge
{
    public class edgeUrealFight : State
    {
        private int _archerCount = 0;
        private bool _isReloading = false;

        private int _heroDamageTaken;
        private int _enemyDamageTaken;

        private SoundEffect _knifeIntoSoundEffect;
        private SoundEffect _swordShield;
        private SoundEffect _arrwoEffect;

        private Texture2D _backgroundTexture;
        private List<Component> _components;

        private Texture2D _characterTexture;
        private Texture2D _imageTexture;
        private float _characterScale = 0.5f;

        private SpriteFont _font;
        private Vector2 _healthPosition;
        private string _characterHealthText;
        private string _enemyHealthText;

        private heroOneLvl _hero;
        private warriorOneLvl _enemy;

        #region Hero

        private int _heroDamage;
        private int _heroHP;
        private int _heroArmor;

        #endregion

        #region Enemy

        private int _enemyDamage;
        private int _enemyHP;
        private int _enemyArmor;

        #endregion

        public edgeUrealFight(Game1 game1, GraphicsDevice graphicsDevice, ContentManager content) : base(game1, graphicsDevice, content)
        {
            SoundEffect _knifeOutSoundEffect = _content.Load<SoundEffect>("Sound/knifeOut");
            AudioManager.PlaySoundEffect(_knifeOutSoundEffect);
            _knifeIntoSoundEffect = _content.Load<SoundEffect>("Sound/knifeInto");
            _swordShield = _content.Load<SoundEffect>("Sound/swordShield");
            _arrwoEffect = _content.Load<SoundEffect>("Sound/archer");

            MediaPlayer.IsRepeating = true;
            Song _fightSong = _content.Load<Song>("Sound/Fight");
            MediaPlayer.Play(_fightSong);

            _hero = new heroOneLvl();
            _enemy = new warriorOneLvl();

            #region Hero


            _heroHP = _hero.HealthPoints + 30;
            _heroArmor = _hero.Armor;

            #endregion

            #region Enemy

            _enemyHP = _enemy.HealthPoints + 50;
            _enemyArmor = _enemy.Armor;

            #endregion

            _backgroundTexture = _content.Load<Texture2D>("Background/camp");
            _characterTexture = _content.Load<Texture2D>("ggEnemy/gg");
            _imageTexture = _content.Load<Texture2D>("ggEnemy/bandit");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            _font = buttonFont;

            _healthPosition = new Vector2(_graphicsDevice.Viewport.Width - 350, _graphicsDevice.Viewport.Height - 100);

            var attackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 800),
                Text = "Нанести удар",
                Scale = 0.5f,

            };
            attackButton.Click += attackButton_Click;

            var deffButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(600, 800),
                Text = "Защититься",
                Scale = 0.5f
            };
            deffButton.Click += deffButton_Click;

            var archerButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(1100, 800),
                Text = "Выстрелить из лука",
                Scale = 0.5f
            };
            archerButton.Click += archerButton_Click;

            _characterHealthText = $"Здоровье: {_heroHP}";
            _enemyHealthText = $"Здоровье врагов: {_enemyHP}";

            _components = new List<Component>()
            {
                new CharacterComponent(_characterTexture, new Vector2(0, -100), 0.7f),
                new ImageComponent(_imageTexture, new Vector2(1200, -100), _characterScale),
                new ImageComponent(_imageTexture, new Vector2(1300, -100), _characterScale),
                attackButton, deffButton, archerButton
            };
        }

        private async void archerButton_Click(object sender, EventArgs e)
        {
            if (_isReloading)
            {
                return;
            }

            _archerCount++;
            if (_archerCount % 3 == 0)
            {
                _isReloading = true;
            }

            Random rnd = new Random();
            _enemyDamage = rnd.Next(6, 9) + 20;
            _heroDamage = rnd.Next(9, 13) + 7;

            _enemyDamageTaken = _heroDamage;
            _heroDamageTaken = _enemyDamage - _heroArmor;

            await Task.Delay(1000);
            AudioManager.PlaySoundEffect(_arrwoEffect);

            _enemyHP -= _enemyDamageTaken;

            await Task.Delay(1000);
            AudioManager.PlaySoundEffect(_knifeIntoSoundEffect);

            _heroHP -= _heroDamageTaken;
        }

        private async void deffButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            _enemyDamage = rnd.Next(6, 9) + 20;

            _heroDamageTaken = _enemyDamage - _heroArmor * 2;

            await Task.Delay(1000);
            AudioManager.PlaySoundEffect(_swordShield);

            if (_heroArmor * 2 < _enemyDamage)
                _heroHP -= _enemyDamage - _heroArmor * 2;
            else { _heroDamageTaken = 0; }
        }

        private async void attackButton_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            _enemyDamage = rnd.Next(6, 9) + 20;
            _heroDamage = rnd.Next(9, 13) + 7;

            _enemyDamageTaken = _heroDamage - _enemyArmor;
            _heroDamageTaken = _enemyDamage - _heroArmor;

            await Task.Delay(1000);
            AudioManager.PlaySoundEffect(_knifeIntoSoundEffect);

            _enemyHP -= _enemyDamageTaken;

            await Task.Delay(1000);
            AudioManager.PlaySoundEffect(_knifeIntoSoundEffect);

            _heroHP -= _heroDamageTaken;
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            float scaleX = (float)_graphicsDevice.Viewport.Width / _backgroundTexture.Width;
            float scaleY = (float)_graphicsDevice.Viewport.Height / _backgroundTexture.Height;

            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, new Vector2(scaleX, scaleY), SpriteEffects.None, 0f);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            Vector2 heroHealthPosition = new Vector2(50, 600);
            spriteBatch.DrawString(_font, _characterHealthText, heroHealthPosition, Color.White);

            Vector2 enemyHealthPosition = new Vector2(_graphicsDevice.Viewport.Width - 700, 600);
            spriteBatch.DrawString(_font, _enemyHealthText, enemyHealthPosition, Color.White);


            Vector2 heroDamagePosition = new Vector2(50, 700);
            if (_heroDamageTaken < 0)
                spriteBatch.DrawString(_font, $"Вы получили 0 урона", heroDamagePosition, Color.Red);
            else
                spriteBatch.DrawString(_font, $"Вы получили {_heroDamageTaken} урона", heroDamagePosition, Color.Red);


            Vector2 enemyDamagePosition = new Vector2(_graphicsDevice.Viewport.Width - 700, 700);
            if (_heroDamageTaken < 0)
                spriteBatch.DrawString(_font, $"Враг получил 0 урона", enemyDamagePosition, Color.Red);
            else
                spriteBatch.DrawString(_font, $"Враг получил {_enemyDamageTaken} урона", enemyDamagePosition, Color.Red);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {

            foreach (var component in _components)
                component.Update(gameTime);

            if (_enemyHP <= 0)
            {
                _game.ChangeState(new firstCondition(_game, _graphicsDevice, _content));
            }

            if (_heroHP <= 0)
            {
                _game.ChangeState(new DieState(_game, _graphicsDevice, _content));
            }

            _characterHealthText = $"Ваше здоровье: {_heroHP}";
            _enemyHealthText = $"Здоровье лесных бандитов: {_enemyHP}";
        }
    }
}
