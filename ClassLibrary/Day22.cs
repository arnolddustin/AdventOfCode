using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day22
    {
        public class Simulator
        {
            enum GameResults
            {
                PlayerWins,
                BossWins,
                Incomplete
            }

            readonly string _playername;
            readonly int _playerhitpoints;
            readonly int _playermana;
            readonly string _bossname;
            readonly int _bosshitpoints;
            readonly int _bossdamage;
            readonly bool _hard;
            readonly IEnumerable<Spell> _allspells;

            int _cheapest;

            public Simulator(string playername, int playerhitpoints, int playermana, string bossname, int bosshitpoints, int bossdamage, bool hard)
            {
                _playername = playername;
                _playerhitpoints = playerhitpoints;
                _playermana = playermana;
                _bossname = bossname;
                _bosshitpoints = bosshitpoints;
                _bossdamage = bossdamage;
                _allspells = Spell.GetAllSpells().OrderBy(x => x.Cost);
                _hard = hard;
            }

            public int FindCheapestPlayerWin()
            {
                _cheapest = int.MaxValue;

                var result = RecursiveBuildRecipe(Enumerable.Empty<Spell>());

                var cleanresults = result.Where(x => x != null);

                var resultTotal = cleanresults.Sum(x => x.Cost);

                return _cheapest;
            }

            IEnumerable<Spell> RecursiveBuildRecipe(IEnumerable<Spell> recipe)
            {
                foreach (var spell in _allspells)
                {
                    if (_cheapest > recipe.Sum(x => x.Cost) + spell.Cost)
                    {
                        var newrecipe = new List<Spell>(recipe);
                        newrecipe.Add(spell);

                        GameResults result;

                        try
                        {
                            result = GetWinner(newrecipe);
                        }
                        catch (SpellAlreadyActiveException)
                        {
                            continue;
                        }
                        catch (NotEnoughManaException)
                        {
                            yield break;
                        }

                        switch (result)
                        {
                            case GameResults.PlayerWins:
                                _cheapest = newrecipe.Sum(x => x.Cost);
                                yield return spell;
                                break;

                            case GameResults.BossWins:
                                yield break;

                            case GameResults.Incomplete:
                                yield return RecursiveBuildRecipe(newrecipe).LastOrDefault();
                                break;
                        }
                    }
                }
            }
            GameResults GetWinner(IEnumerable<Spell> spells)
            {
                var game = new Game(new Player(_playername, _playerhitpoints, _playermana), new Boss(_bossname, _bosshitpoints, _bossdamage), _hard);

                game.Simulate(spells);

                if (game.Winner.HasValue)
                    return (game.Winner.Value == Players.Player) ? GameResults.PlayerWins : GameResults.BossWins;
                else
                    return GameResults.Incomplete;
            }
        }

        public class Game
        {
            public Players? Winner { get; private set; }
            public List<Spell> SpellsCast { get; private set; }

            readonly Player _player;
            readonly Boss _boss;
            readonly bool _hard;

            Spell _activeShield = null;
            Spell _activePoison = null;
            Spell _activeRecharge = null;

            public Game(Player player, Boss boss, bool hard)
            {
                _player = player;
                _player.Die += _player_Die;

                _boss = boss;
                _boss.Die += _boss_Die;

                _hard = hard;

                Winner = null;
                SpellsCast = new List<Spell>();
            }

            public void Simulate(IEnumerable<Spell> spells)
            {
                foreach (var spell in spells)
                {
                    PlayerTurn(spell);

                    if (Winner.HasValue)
                        break;

                    BossTurn();

                    if (Winner.HasValue)
                        break;
                }
            }

            public void TakeTurn(Spell spell)
            {
                PlayerTurn(spell);

                if (Winner.HasValue)
                    return;

                BossTurn();
            }

            void PlayerTurn(Spell spell)
            {
                if (_hard)
                    _player.GetExtraDamage(1);

                if (Winner.HasValue)
                    return;

                ApplyEffects();

                if (Winner.HasValue)
                    return;

                ApplySpell(spell);
            }
            void BossTurn()
            {
                ApplyEffects();

                if (Winner.HasValue)
                    return;

                _player.GetDamagedBy(_boss);
            }

            IEnumerable<Spell> ActiveSpells()
            {
                if (_activePoison != null)
                    yield return _activePoison;

                if (_activeRecharge != null)
                    yield return _activeRecharge;

                if (_activeShield != null)
                    yield return _activeShield;
            }
            void ApplyEffects()
            {
                _player.Mana += ActiveSpells().Sum(x => x.RecurringMana);
                _player.ArmorBoost = ActiveSpells().Sum(x => x.RecurringArmor);

                _boss.GetRecurringDamage(ActiveSpells());

                // decrement
                ActiveSpells().ToList().ForEach(x => x.Decrement());

                // remove expired
                if (_activePoison != null && _activePoison.Remaining == 0)
                    _activePoison = null;
                if (_activeRecharge != null && _activeRecharge.Remaining == 0)
                    _activeRecharge = null;
                if (_activeShield != null && _activeShield.Remaining == 0)
                    _activeShield = null;
            }
            void ApplySpell(Spell spell)
            {
                SpellsCast.Add(spell);

                if (spell.Duration > 0 && ActiveSpells().Any(x => x.Name == spell.Name))
                    throw new SpellAlreadyActiveException(spell.Name);

                _player.CastSpell(spell, _boss);

                if (spell.Duration > 0)
                {
                    spell.Start();

                    if (spell.Name == "Poison")
                        _activePoison = spell;

                    if (spell.Name == "Shield")
                        _activeShield = spell;

                    if (spell.Name == "Recharge")
                        _activeRecharge = spell;
                }
            }

            void _boss_Die(object sender, EventArgs e)
            {
                Winner = Players.Player;
            }
            void _player_Die(object sender, EventArgs e)
            {
                Winner = Players.Boss;
            }
        }

        public class SpellAlreadyActiveException : Exception
        {
            public SpellAlreadyActiveException(string spellname) : base(string.Format("Spell '{0}' is already active.", spellname)) { }
        }

        public class NotEnoughManaException : Exception
        {
            public NotEnoughManaException(string spellname) : base(string.Format("Not enough mana to cast spell '{0}'.", spellname)) { }
        }

        #region Characters

        public abstract class CharacterBase
        {
            public event EventHandler Die;

            protected virtual void OnDie()
            {
                if (Die != null)
                    Die(this, new EventArgs());
            }

            public string Name { get; protected set; }
            public int HitPoints { get; protected set; }
        }

        public enum Players
        {
            Player,
            Boss
        }

        public class Boss : CharacterBase
        {
            public int Damage { get; private set; }

            public Boss(string name, int hitpoints, int damage)
            {
                Name = name;
                HitPoints = hitpoints;
                Damage = damage;
            }

            public void GetInstantDamage(Spell spell)
            {
                HitPoints -= spell.InstantDamage;

                if (HitPoints <= 0)
                    OnDie();
            }
            public void GetRecurringDamage(IEnumerable<Spell> spells)
            {
                HitPoints -= spells.Sum(x => x.RecurringDamage);

                if (HitPoints <= 0)
                    OnDie();
            }

            public override string ToString()
            {
                return string.Format("{0} has {1} hit points", Name, HitPoints);
            }
        }

        public class Player : CharacterBase
        {
            public int Mana { get; set; }
            public int Armor { get; private set; }
            public int ArmorBoost { get; set; }

            public Player(string name, int hitpoints, int mana)
            {
                Name = name;
                HitPoints = hitpoints;
                Mana = mana;
                Armor = 0;
                ArmorBoost = 0;
            }

            public void CastSpell(Spell spell, Boss boss)
            {
                if (spell.Cost > Mana)
                    throw new NotEnoughManaException(spell.Name);

                Mana -= spell.Cost;
                HitPoints += spell.Healing;

                boss.GetInstantDamage(spell);
            }
            public void GetDamagedBy(Boss boss)
            {
                HitPoints -= boss.Damage - (Armor + ArmorBoost);

                if (HitPoints <= 0)
                    OnDie();
            }

            public void GetExtraDamage(int damage)
            {
                HitPoints -= damage;

                if (HitPoints <= 0)
                    OnDie();
            }

            public override string ToString()
            {
                return string.Format("{0} has {1} hit points, {2} armor, {3} mana", Name, HitPoints, Armor + ArmorBoost, Mana);
            }
        }

        #endregion

        public class Spell
        {
            public string Name { get; private set; }
            public int Cost { get; private set; }
            public int InstantDamage { get; private set; }
            public int Healing { get; private set; }
            public int Duration { get; private set; }
            public int RecurringDamage { get; private set; }
            public int RecurringArmor { get; private set; }
            public int RecurringMana { get; private set; }
            public int Remaining { get; private set; }

            private Spell(string name, int cost)
            {
                Name = name;
                Cost = cost;
            }

            public void Start()
            {
                Remaining = Duration;
            }

            public void Decrement()
            {
                Remaining--;
            }

            public static IEnumerable<Spell> GetAllSpells()
            {
                yield return new Spell("Magic Missle", 53) { InstantDamage = 4 };
                yield return new Spell("Drain", 73) { InstantDamage = 2, Healing = 2 };
                yield return new Spell("Shield", 113) { Duration = 6, RecurringArmor = 7 };
                yield return new Spell("Poison", 173) { Duration = 6, RecurringDamage = 3 };
                yield return new Spell("Recharge", 229) { Duration = 5, RecurringMana = 101 };
            }

            public static Spell GetByName(string name)
            {
                return GetAllSpells().FirstOrDefault(x => x.Name == name);
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
