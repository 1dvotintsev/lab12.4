using CustomLibrary;
namespace lab12._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int answer = 0;
            MyCollection<Emoji> tree = new MyCollection<Emoji>(1);
            int number = 0;
            int currentTree = 0;

            while (true)
            {
                number = 0;
                Console.WriteLine("Данная программа демонстрирует действия над коллекциями деревьев.");
                Console.WriteLine("Список инициализированных деревеьев:");
                foreach (var item in MyCollection<Emoji>.collections)
                {
                    if (item != null)
                    {
                        Console.WriteLine($"{number + 1}) Дерево#{item.Id}");
                        number++;
                    }
                }

                
                if (number > 0)
                {
                    Console.WriteLine($"{number + 1}) Создать пустую коллекцию");
                    Console.WriteLine("Выберете над каким деревом будете совершать действия:");
                    currentTree = ChooseAnswer(1, number + 1);
                    Console.Clear();
                    if (currentTree < number + 1)
                    {
                        if (tree.Count > 0)
                        {
                            Console.Write($"На данный момент в дереве {MyCollection<Emoji>.collections[currentTree - 1].Count} элементов\n");
                        }
                        else
                        {
                            Console.Write($"На данный момент в коллекции нет элементов\n");
                        }
                        Console.WriteLine("Выберите одно из действий над таблицей\n");

                        Console.WriteLine("1) Распечатать коллекцию как дерево");
                        Console.WriteLine("2) Инициализиоровать случайными элементами ИСД");
                        Console.WriteLine("3) Конвертировать в дерево поиска");
                        Console.WriteLine("4) Удалить объект (предварительно нужно конвертировать в дерево поиска");
                        Console.WriteLine("5) Посчитать глубину дерева");
                        Console.WriteLine("6) Удалить коллекцию");
                        Console.WriteLine("7) Распечатать перебором");
                        Console.WriteLine("8) Создать клон через foreach (повторяющиеся элементы не будут учитываться, конечный результат - дерево поиска)");
                        Console.WriteLine("9) Проверка метода Contains");
                        Console.WriteLine("10) Трансформировать в массив и распечатать");
                        Console.WriteLine("11) Очистить коллекцию");

                        answer = ChooseAnswer(1, 8);
                        switch (answer)
                        {
                            case 1:
                                Console.Clear();
                                MyCollection<Emoji>.list[currentTree - 1].Show(MyCollection<Emoji>.collections[currentTree - 1].root);
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("На сколько элементов построить дерево?");
                                answer = ChooseAnswer(0, 1000);
                                MyCollection<Emoji>.collections[currentTree - 1].Init(answer);
                                Console.Clear();
                                Console.WriteLine("Дерево успешно проинициализировано");
                                break;

                            case 3:
                                Console.Clear();
                                MyCollection<Emoji> newTree = new MyCollection<Emoji>(0);
                                newTree.ToSearchTree(MyCollection<Emoji>.collections[currentTree - 1]);
                                break;

                            case 4:
                                Console.Clear();
                                Console.WriteLine("Введите имя объекта для удаления:");
                                string name = Console.ReadLine();
                                Emoji target = new Emoji();
                                target.RandomInit();
                                target.Name = name;
                                if (MyCollection<Emoji>.collections[currentTree - 1].Remove(target))
                                {
                                    Console.WriteLine("Объект удален");
                                }
                                else
                                {
                                    Console.WriteLine("Объекта с таким именем нет в дереве.");
                                }
                                break;
                            case 5:
                                Console.Clear();
                                Console.WriteLine($"Глубина текущего дерева равна {tree.Depth(tree.root)}");
                                break;

                            case 6:
                                Console.Clear();
                                MyCollection<Emoji>.collections.RemoveAt(currentTree - 1);
                                Console.WriteLine("Дерево удалено");
                                break;
                            case 7:
                                Console.Clear();
                                foreach (var item in MyCollection<Emoji>.collections[currentTree - 1])
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case 8:
                                Console.Clear();
                                MyCollection<Emoji> newCollection = new MyCollection<Emoji>(MyCollection<Emoji>.collections[currentTree - 1]);
                                Console.WriteLine("Клон создан");
                                break;
                            case 9:                               
                                Console.Clear();
                                Console.WriteLine("Введите имя объекта для поиска:");
                                string name_ = Console.ReadLine();
                                Emoji target_ = new Emoji();
                                target_.RandomInit();
                                target_.Name = name_;
                                if (MyCollection<Emoji>.collections[currentTree - 1].Contains(target_))
                                {
                                    Console.WriteLine("Элемент с таким именем есть");
                                }
                                else
                                {
                                    Console.WriteLine("Элемента с таким именем нет");
                                }
                                break;
                            case 10:
                                Console.Clear();
                                Emoji[] array = new Emoji[MyCollection<Emoji>.collections[currentTree - 1].Count];
                                MyCollection<Emoji>.collections[currentTree - 1].CopyTo(array, 0);
                                foreach(var item in array)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case 11:
                                Console.Clear();
                                MyCollection<Emoji>.collections[currentTree - 1].Clear();
                                Console.WriteLine("Коллекция очищена от элементов");
                                break;

                            default: break;
                        }
                    }
                    else
                    {
                        MyCollection<Emoji> emptyCollection = new MyCollection<Emoji>();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Нет созданных коллекций. Для продолжения работы создайте новую коллекцию(просто укажите её длину):");
                    int lenght = ChooseAnswer(0, 100);
                    MyCollection<Emoji> tree1 = new MyCollection<Emoji>(lenght);
                }
            }
        }

        static int ChooseAnswer(int a, int b)   //выбор действия из целых
        {
            int answer = 0;
            bool checkAnswer;
            do
            {
                checkAnswer = int.TryParse(Console.ReadLine(), out answer);
                if ((answer > b || answer < a) || (!checkAnswer))
                {
                    Console.WriteLine("Вы некорректно ввели число, повторите ввод еще раз. Обратите внимание на то, что именно нужно ввести.");
                }
            } while ((answer > b || answer < a) || (!checkAnswer));

            return answer;
        }
    }
}
