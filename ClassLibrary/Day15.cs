using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Day15
    {
        public int SolutionPart1(params string[] input)
        {
            var scorer = CreateRecipeScorer(input);

            return scorer.GetBestCookieScore();
        }

        public int SolutionPart2(params string[] input)
        {
            var scorer = CreateRecipeScorer(input);

            return scorer.GetBestCookieScoreWithCalories(500);
        }

        RecipeScorer CreateRecipeScorer(string[] input)
        {
            return new RecipeScorer(CreateIngredientsFromInput(input));
        }

        IEnumerable<Ingredient> CreateIngredientsFromInput(string[] input)
        {
            foreach (var line in input)
                yield return new Ingredient(line);
        }

        class RecipeScorer
        {
            readonly List<Ingredient> _ingredients;
            readonly List<Recipe> _recipes;

            public RecipeScorer(IEnumerable<Ingredient> ingredients)
            {
                _ingredients = new List<Ingredient>(ingredients);
                _recipes = new List<Recipe>();
            }

            public int GetBestCookieScoreWithCalories(int calories)
            {
                var recipes = new List<Recipe>();

                foreach (var combo in GetKCombsWithRept(_ingredients,_ingredients.Count))
                    recipes.AddRange(GetPossibleRecipes(combo.ToList()));

                return recipes.Where(x => x.Calories == 500).Max(x => x.Score);
            }

            public int GetBestCookieScore()
            {
                var recipes = new List<Recipe>();

                foreach (var combo in GetKCombsWithRept(_ingredients, _ingredients.Count))
                    recipes.AddRange(GetPossibleRecipes(combo.ToList()));

                return recipes.Max(x => x.Score);
            }

            IEnumerable<IEnumerable<T>> GetKCombsWithRept<T>(IEnumerable<T> list, int length) where T : IComparable
            {
                if (length == 1) return list.Select(t => new T[] { t });
                return GetKCombsWithRept(list, length - 1)
                    .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                        (t1, t2) => t1.Concat(new T[] { t2 }));
            }

            IEnumerable<Recipe> GetPossibleRecipes(IList<Ingredient> ingredients)
            {
                for (int i = 1; i < 100; i++)
                {
                    yield return new Recipe(new RecipeIngredient[] { new RecipeIngredient(ingredients[0], i), new RecipeIngredient(ingredients[1], 100 - i) });
                }

                if (ingredients.Count() > 2)
                {
                    for (int i = 1; i < 100; i++)
                    {
                        for (int j = 1; j < 100 - i; j++)
                        {
                            yield return new Recipe(new RecipeIngredient[] { new RecipeIngredient(ingredients[0], i), new RecipeIngredient(ingredients[1], j), new RecipeIngredient(ingredients[2], 100 - i - j) });
                        }
                    }
                }

                if (ingredients.Count() > 3)
                {
                    for (int i = 1; i < 100; i++)
                    {
                        for (int j = 1; j < 100 - i; j++)
                        {
                            for (int k = 1; k < 100 - j - i; k++)
                            {
                                yield return new Recipe(new RecipeIngredient[] { new RecipeIngredient(ingredients[0], i), new RecipeIngredient(ingredients[1], j), new RecipeIngredient(ingredients[2], k), new RecipeIngredient(ingredients[3], 100 - i - j - k) });
                            }
                        }
                    }
                }
            }
        }

        class Recipe
        {
            public List<RecipeIngredient> Ingredients { get; private set; }

            public Recipe(IEnumerable<RecipeIngredient> ingredients)
            {
                Ingredients = new List<RecipeIngredient>(ingredients);
            }

            int? _score;
            public int Score
            {
                get
                {
                    if (!_score.HasValue)
                        CalculateScore();

                    return _score.Value;
                }
            }

            int? _calories;
            public int Calories
            {
                get
                {
                    if (!_calories.HasValue)
                        CalculateCalories();

                    return _calories.Value;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendFormat("[{0}]->", Score);
                Ingredients.ForEach(x => sb.AppendFormat("{0},", x));
                return sb.ToString();
            }

            void CalculateCalories()
            {
                var calories = 0;
                Ingredients.ForEach(x => calories += x.Ingredient.Calories * x.Amount);
                _calories = calories;
            }

            void CalculateScore()
            {
                var capacity = 0;
                var durability = 0;
                var flavor = 0;
                var texture = 0;
                foreach (var item in Ingredients)
                {
                    capacity += item.Ingredient.Capacity * item.Amount;
                    durability += item.Ingredient.Durability * item.Amount;
                    flavor += item.Ingredient.Flavor * item.Amount;
                    texture += item.Ingredient.Texture * item.Amount;
                }

                if (capacity < 0 || durability < 0 || flavor < 0 || texture < 0)
                    _score = 0;
                else
                    _score = capacity * durability * flavor * texture;
            }
        }

        class RecipeIngredient
        {
            public Ingredient Ingredient { get; private set; }
            public int Amount { get; private set; }

            public RecipeIngredient(Ingredient ingredient, int amount)
            {
                Ingredient = ingredient;
                Amount = amount;
            }

            public override string ToString()
            {
                return string.Format("{0}({1})", Ingredient.Name, Amount);
            }
        }

        class Ingredient : IComparable
        {
            public string Name { get; private set; }
            public int Capacity { get; private set; }
            public int Durability { get; private set; }
            public int Flavor { get; private set; }
            public int Texture { get; private set; }
            public int Calories { get; private set; }

            public Ingredient(string input)
            {
                var parts = input.Split(' ');

                Name = parts[0].TrimEnd(':');
                Capacity = int.Parse(parts[2].TrimEnd(','));
                Durability = int.Parse(parts[4].TrimEnd(','));
                Flavor = int.Parse(parts[6].TrimEnd(','));
                Texture = int.Parse(parts[8].TrimEnd(','));
                Calories = int.Parse(parts[10]);
            }

            public override string ToString()
            {
                return Name;
            }

            public int CompareTo(object obj)
            {
                return string.Compare(Name, ((Ingredient)obj).Name);
            }
        }
    }
}
