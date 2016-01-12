using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day21
    {
        public class Simulator
        {
            public int LeastAmountOfGoldRequiredToWinFight(int playerHitpoints, Player boss)
            {
                var combos = GetValidEquipmentCombinations().ToList();

                foreach (var loadout in combos.OrderBy(x => x.Sum(y=>y.Cost)))
                {
                    var p1 = new EquippedPlayer("Me", playerHitpoints, 0, 0, loadout);
                    var p2 = new Player(boss.Name, boss.HitPoints, boss.Damage, boss.Armor);

                    var battle = new Battle(p1, p2);

                    var winner = battle.Fight();

                    if (winner == p1)
                        return p1.Equipment.Sum(x => x.Cost);
                }

                return int.MaxValue;
            }

            public int MostAmountRequiredToLoseFight(int playerHitpoints, Player boss)
            {
                var combos = GetValidEquipmentCombinations().ToList();

                foreach (var loadout in combos.OrderByDescending(x => x.Sum(y=>y.Cost)))
                {
                    var p1 = new EquippedPlayer("Me", playerHitpoints, 0, 0, loadout);
                    var p2 = new Player(boss.Name, boss.HitPoints, boss.Damage, boss.Armor);

                    var battle = new Battle(p1, p2);

                    var winner = battle.Fight();

                    if (winner == p2)
                        return p1.Equipment.Sum(x => x.Cost);
                }

                return int.MinValue;
            }

            public IEnumerable<IEnumerable<Equipment>> GetValidEquipmentCombinations()
            {
                foreach (var weapon in Equipment.AllWeapons().OrderBy(x => x.Cost))
                {
                    // weapon only
                    yield return new Equipment[] { weapon };

                    // with armor, with and without rings
                    foreach (var armor in Equipment.AllArmor().OrderBy(x => x.Cost))
                    {
                        // weapon and armor only
                        yield return new Equipment[] { weapon, armor };

                        foreach (var ring in Equipment.AllRings().OrderBy(x => x.Cost))
                        {
                            // one ring
                            yield return new Equipment[] { weapon, armor, ring };

                            foreach (var ring2 in Equipment.AllRings().Where(r => r != ring).OrderBy(x => x.Cost))
                            {
                                // two rings
                                yield return new Equipment[] { weapon, armor, ring, ring2 };
                            }
                        }
                    }

                    // no armor - rings only
                    foreach (var ring in Equipment.AllRings().OrderBy(x => x.Cost))
                    {
                        // one ring
                        yield return new Equipment[] { weapon, ring };

                        foreach (var ring2 in Equipment.AllRings().Where(r => r != ring).OrderBy(x => x.Cost))
                        {
                            // two rings
                            yield return new Equipment[] { weapon, ring, ring2 };
                        }
                    }
                }
            }
        }

        public class Battle
        {
            public Player Player { get; private set; }
            public Player Boss { get; private set; }
            public IList<string> Log { get; private set; }

            public Battle(Player player, Player boss)
            {
                Player = player;
                Boss = boss;
                Log = new List<string>();
            }

            public Player Fight()
            {
                while (true)
                {
                    Log.Add(string.Format("{0} => {1}", Player, Boss));
                    Player.Attack(Boss);
                    if (Boss.HitPoints <= 0)
                        return Player;

                    Log.Add(string.Format("{0} => {1}", Boss, Player));
                    Boss.Attack(Player);
                    if (Player.HitPoints <= 0)
                        return Boss;
                }
            }
        }

        public class Player
        {
            public string Name { get; protected set; }
            public int HitPoints { get; protected set; }
            public virtual int Damage { get; protected set; }
            public virtual int Armor { get; protected set; }

            public Player(string name, int hitpoints, int damage, int armor)
            {
                Name = name;
                HitPoints = hitpoints;
                Damage = damage;
                Armor = armor;
            }

            public void Attack(Player player)
            {
                player.DefendAgainst(this);
            }
            public void DefendAgainst(Player player)
            {
                var damage = player.Damage - this.Armor;

                if (damage < 1)
                    damage = 1;

                HitPoints -= damage;
            }

            public override string ToString()
            {
                return string.Format("{0} ({1})", Name, HitPoints);
            }
        }

        public class EquippedPlayer : Player
        {
            public IList<Equipment> Equipment { get; private set; }
            public override int Damage
            {
                get
                {
                    return base.Damage + Equipment.Sum(x => x.Damage);
                }
            }
            public override int Armor
            {
                get
                {
                    return base.Armor + Equipment.Sum(x => x.Armor);
                }
            }

            public EquippedPlayer(string name, int hitpoints, int damage, int armor) : base(name, hitpoints, damage, armor) 
            {
                Equipment = new List<Equipment>();
            }
            public EquippedPlayer(string name, int hitpoints, int damage, int armor, IEnumerable<Equipment> equipment)
                : base(name, hitpoints, damage, armor)
            {
                Equipment = new List<Equipment>(equipment);
            }
        }

        public class Equipment
        {
            public string Name { get; private set; }
            public int Cost { get; private set; }
            public int Damage { get; private set; }
            public int Armor { get; private set; }

            public Equipment(string name, int cost, int damage, int armor)
            {
                Name = name;
                Cost = cost;
                Damage = damage;
                Armor = armor;
            }

            public static IEnumerable<Equipment> AllWeapons()
            {
                yield return new Equipment("Dagger", 8, 4, 0);
                yield return new Equipment("Shortsword", 10, 5, 0);
                yield return new Equipment("Warhammer", 25, 6, 0);
                yield return new Equipment("Longsword", 40, 7, 0);
                yield return new Equipment("Greataxe", 74, 8, 0);
            }

            public static IEnumerable<Equipment> AllArmor()
            {
                yield return new Equipment("Leather", 13, 0, 1);
                yield return new Equipment("Chainmail", 31, 0, 2);
                yield return new Equipment("Splintmail", 53, 0, 3);
                yield return new Equipment("Bandedmail", 75, 0, 4);
                yield return new Equipment("Platemail", 102, 0, 5);
            }

            public static IEnumerable<Equipment> AllRings()
            {
                yield return new Equipment("Damage Ring 1", 25, 1, 0);
                yield return new Equipment("Damage Ring 2", 50, 2, 0);
                yield return new Equipment("Damage Ring 3", 100, 3, 0);
                yield return new Equipment("Defense Ring 1", 20, 0, 1);
                yield return new Equipment("Defense Ring 2", 40, 0, 2);
                yield return new Equipment("Defense Ring 3", 80, 0, 3);
            }
        }
    }
}
